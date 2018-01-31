import BaseController from './base.controller';
import fs from 'fs';
import path from 'path';

export default class extends BaseController {
    constructor(app) {
        super(app);

        // Create a new Model object
        this.router.post('/', (req, res, next) => {
            this.uploadModel(req, res, next);
        });

        // Returns the name of Model with id of :id
        this.router.get('/:id/name', (req, res, next) => {
            this.getModelName(req, res, next);
        });

        // Returns object for Model with the id of :id
        this.router.get('/:id', (req, res, next) => {
            this.getModel(req, res, next);
        });

        // Returns paginated list of all Models belonging
        // to User with id of :id
        this.router.get('/user/:id', (req, res, next) => {
            this.getModelsByUser(req, res, next);
        });

        // Deletes Model with id of :id
        this.router.delete('/:id', (req, res, next) => {
            this.deleteModel(req, res, next);
        });
    }

    async uploadModel(req, res, next) {
        this.logger.debug("Recieving object from user and sending to S3");
        await this.S3.upload(request, req.path).error(err => {
            next({
                status: this.HttpStatus.INTERNAL_SERVER_ERROR,
                message: "Could not upload object to S3"
            });
        });

        this.logger.info("Successfully uploaded object");
        this.sendResponse(res, "Successfully uploaded object");
    }

    async getModelName(req, res, next) {
        // Retrieve model from database
        this.logger.debug("Retrieving model from database");
        let model = this.Model.findById(req.params.id).catch(err => {
            next({
                status: this.HttpStatus.INTERNAL_SERVER_ERROR,
                message: "Could not retrieve model"
            });
        });

        this.logger.info("Successfully retrieved model name");
        this.sendResponse(res, model.name);
    }

    // Returns an object for Model with id of :id
    async getModel(req, res, next) {
        // Retrieve model from database
        this.logger.debug("Retrieving model from database");
        let model = this.Model.findById(req.params.id).catch(err => {
            next({
                status: this.HttpStatus.INTERNAL_SERVER_ERROR,
                message: "Could not retrieve model"
            });
        });

        // Retrieve model from S3 storage
        this.logger.debug("Retrieving model from S3 storage");
        let modelObject = await this.S3.download('example.txt').catch(err => {
            next({
                status: this.HttpStatus.INTERNAL_SERVER_ERROR,
                message: "Could not retrieve model"
            });
        });

        this.logger.info("Send response with Model data");
        res.writeHead(200, {
            "Content-Type": "application/model",
            "Content-Disposition": "attachment; filename=" + "example.txt"
        });
        res.write(modelObject.Body);
        res.end(null, 'binary');
    }

    async getModelsByUser(req, res, next) {
        // TODO
    }

    // Delete a model from db
    async deleteModel(req, res, next) {
        let user = req.user;
        if (process.env.FAKE_USER_AUTHENTICATION === "true") {
            user = {};
            user.id = parseInt(process.env.FAKE_USER_ID);
        }

        this.logger.debug("Retrieving model from database");
        let model = this.Model.findById(req.params.id).catch(err => {
            next({
                status: this.HttpStatus.INTERNAL_SERVER_ERROR,
                message: "Error retrieving model"
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

        // Delete object from S3 storage
        this.logger.debug("Deleting model from S3 storage");
        await this.S3.deleteObject(model.path).error(err => {
            next({
                status: this.HttpStatus.INTERNAL_SERVER_ERROR,
                message: "Could not delete model"
            });
        });

        // Delete object from database
        this.logger.debug("Deleting model from database");
        await model.destroy().error(err => {
            next({
                status: this.HttpStatus.INTERNAL_SERVER_ERROR,
                message: "Could not delete model"
            });
        });

        this.logger.info("Successfully deleted model");
        this.sendResponse(res, "Successfully deleted model");
    }
}
