import BaseController from './base.controller';
import fs from 'fs';
import path from 'path';

export default class extends BaseController {
    constructor(app) {
        super(app);

        // Create a new Model object
        this.router.post('/', (req, res, next) => {
            this.createModel(req, res, next);
        });

        this.router.get('/:id', (req, res, next) => {
            this.getModel(req, res, next);
        })

        this.router.put('/:id', (req, res, next) => {
            this.updateModel(req, res, next);
        });

        // Deletes Model with id of :id
        this.router.delete('/:id', (req, res, next) => {
            this.deleteModel(req, res, next);
        });
    }

    async getModel(req, res, next) {
        let model = await this.SlideModel.findById(req.params.id).catch(err => {
            next({
                status: this.HttpStatus.INTERNAL_SERVER_ERROR,
                message: "Failed to get model"
            });
        });

        this.logger.info("Successfully got model");
        this.sendResponse(res, model);
    }

    async createModel(req, res, next) {
        let user = req.user;
        if (process.env.FAKE_USER_AUTHENTICATION === "true") {
            user = {};
            user.id = parseInt(process.env.FAKE_USER_ID);
        }

        let data = req.body;
        data.slide_id = parseInt(data.slide_id);
        data.transform = JSON.parse(data.transform);

        let sanitizedData = {}
        if (!data.slide_id || typeof data.slide_id !== 'number') {
            next({
                status: this.HttpStatus.BAD_REQUEST,
                message: "Invalid input"
            });
        } else {
            sanitizedData.slide_id = data.slide_id;
        }

        if (data.poly_id && typeof data.poly_id !== 'string') {
            // data.poly_id must be a string
            next({
                status: this.HttpStatus.BAD_REQUEST,
                message: "Invalid input"
            });
        } else if (data.poly_id) {
            sanitizedData.poly_id = data.poly_id;
        }

        if (data.transform && typeof data.transform !== 'object') {
            // data.transform must be a string
            next({
                status: this.HttpStatus.BAD_REQUEST,
                message: "Invalid input"
            });
        } else if (data.transform) {
            sanitizedData.transform = JSON.stringify(data.transform);
        }

        // Create model
        let model = await this.SlideModel.create({
            slide_id: sanitizedData.slide_id,
            poly_id: sanitizedData.poly_id,
            transform: sanitizedData.transform
        }).catch((err) => {
            next({
                status: this.HttpStatus.INTERNAL_SERVER_ERROR,
                message: "Error creating model object"
            });
        });

        this.logger.info("Successfully created model")
        this.sendResponse(res, model);
    }

    async updateModel(req, res, next) {
        let user = req.user;
        if (process.env.FAKE_USER_AUTHENTICATION === "true") {
            user = {};
            user.id = parseInt(process.env.FAKE_USER_ID);
        }

        let data = req.body;

        let model = await this.SlideModel.findById(req.params.id).catch(err => {
            next({
                status: this.HttpStatus.INTERNAL_SERVER_ERROR,
                message: "Could not find model"
            });
        });

        let presentation = await this.Presentation.findById(model.presentation_id).catch(err => {
            next({
                status: this.HttpStatus.INTERNAL_SERVER_ERROR,
                message: "Could not find presentation"
            });
        });

        // User does not have access to slides
        if (user.id != presentation.user_id) {
            next({
                status: this.HttpStatus.UNAUTHORIZED,
                message: "You don't have access to that!"
            })
        }

        if (data.slide_id && typeof data.slide_id === "number") {
            model.slide_id = data.slide_id;
        }

        if (data.poly_id && typeof data.poly_id === "string") {
            model.poly_id = data.poly_id;
        }

        if (data.transform && typeof data.transform === "string") {
            model.transform = data.transform;
        }

        await model.save().catch((err) => {
            next({
                status: this.HttpStatus.INTERNAL_SERVER_ERROR,
                message: "Could not update model"
            });
        });

        this.sendResponse(res, model);
    }

    // Delete a model from db
    async deleteModel(req, res, next) {
        let user = req.user;
        if (process.env.FAKE_USER_AUTHENTICATION === "true") {
            user = {};
            user.id = parseInt(process.env.FAKE_USER_ID);
        }

        this.logger.debug("Retrieving model from database");
        let model = this.sequelize.query(
            `SELECT slide_id, presentation_id, poly_id, transform, user_id 
        FROM slide_3d_models JOIN presentations ON slide_3d_models.presentation_id = presentations.id 
        WHERE slide_3d_models.id=` + req.params.id).catch(err => {
            next({
                status: this.HttpStatus.INTERNAL_SERVER_ERROR,
                message: "Could not retrieve slide models."
            });
        });

        // User does not have access to slides
        this.logger.debug("Validating user authorization");

        if (user.id != model.user_id) {
            next({
                status: this.HttpStatus.UNAUTHORIZED,
                message: "You are not authorized to perform this action."
            });
        }

        // Delete object from SQL DB
        this.logger.debug("Deleting model from S3 storage");
        await this.SlideModel.destroy({
            where: {
                id: req.params.id
            }
        }).catch(err => {
            next({
                status: this.HttpStatus.INTERNAL_SERVER_ERROR,
                message: "Could not delete model"
            });
        });

        this.logger.info("Successfully deleted model");
        this.sendResponse(res, "Successfully deleted model");
    }
}
