import BaseController from './base.controller';

export default class extends BaseController {
    constructor(app) {
        super(app);

        // Create a new Slide object
        this.router.post('/', (req, res, next) => {
            this.createSlide(req, res, next);
        });

        // Returns all the models belonging to Slide
        // with the id of :id
        this.router.get('/:id/models', (req, res, next) => {
            this.getSlideModels(req, res, next);
        });

        // Returns object for Slide with the id of :id
        this.router.get('/:id', (req, res, next) => {
            this.getSlide(req, res, next);
        });

        // Updates Slide with id of :id
        this.router.put('/:id', (req, res, next) => {
            this.updateSlide(req, res, next);
        });

        // Deletes Slide with id of :id
        this.router.delete('/:id', (req, res, next) => {
            this.deleteSlide(req, res, next);
        });

    }

    // Create a new slide for a given presentation
    async createSlide(req, res, next) {
        let user = req.user;
        if (process.env.FAKE_USER_AUTHENTICATION === "true") {
            user = {};
            user.id = parseInt(process.env.FAKE_USER_ID);
        }

        let data = req.body;

        // Validate input
        if (!data.presentation_id) {
            next({
                status: this.HttpStatus.BAD_REQUEST,
                message: "Slide must be added to a presentation."
            });
        }

        // Find presentation user is trying to add to
        let presentation = await this.Presentation.findById(data.presentation_id).catch((err) => {
            next({
                status: this.HttpStatus.INTERNAL_SERVER_ERROR,
                message: "Error retrieving presentation."
            });
        });

        // Presentation does not exist
        if (!presentation) {
            next({
                status: this.HttpStatus.NOT_FOUND,
                message: "Presentation does not exist."
            });
        }

        // User is not unauthorized.Does not own presentation
        if (user.id != presentation.user_id) {
            next({
                status: this.HttpStatus.UNAUTHORIZED,
                message: "You don't have permission to do that!"
            });
        }

        // Calculate sequence number for new slide
        let sequenceNumber = await this.Slide.count({
            where: {
                presentation_id: presentation.id
            }
        }).catch((err) => {
            next({
                status: this.HttpStatus.INTERNAL_SERVER_ERROR,
                message: "Error calculating sequence."
            });
        });

        // Create slide
        let slide = await this.Slide.create({
            presentation_id: data.presentation_id,
            sequence: sequenceNumber
        }).catch((err) => {
            next({
                status: this.HttpStatus.INTERNAL_SERVER_ERROR,
                message: "Error creating slide."
            });
        });

        this.logger.info("Successfully created slide");
        this.sendResponse(res, slide);
    }

    // Get all models for a slide
    // Anyone can access
    async getSlideModels(req, res, next) {

        // Validate input
        if (!req.params.id) {
            next({
                status: this.HttpStatus.BAD_REQUEST,
                message: "Invalid request."
            });
        }

        // Get slide_3d_model objects
        // let models = await SlideModel.find({
        //     slide_id: req.params.id
        // }).catch((err) => {
        //     next({
        //         status: this.HttpStatus.INTERNAL_SERVER_ERROR,
        //         message: "Could not retrieve slide's models"
        //     }
        // });

        let models = await sequelize.query("SELECT * FROM slide_3d_model JOIN 3d_model ON slide_3d_model.model_id=3d_model.id WHERE slide_3d_model.slide_id=" + req.params.id).catch((err) => {
            next({
                status: this.HttpStatus.INTERNAL_SERVER_ERROR,
                message: "Could not retrieve slide models."
            });
        });

        this.logger.info("Successfully received slide models");
        this.sendResponse(req, models);
    }

    // Get SlideModels for Slide
    async getSlide(req, res, next) {
        // TODO What is going to be different than the getModels?
    }

    // Delete a given slide.
    // Must have permission
    async deleteSlide(req, res, next) {
        let user = req.user;
        if (process.env.FAKE_USER_AUTHENTICATION === "true") {
            user = {};
            user.id = parseInt(process.env.FAKE_USER_ID);
        }

        let slide = await this.Slide.find(req.params.id).catch((err) => {
            next({
                status: this.HttpStatus.INTERNAL_SERVER_ERROR,
                message: "Could not retrieve slide."
            });
        });

        let presentation = this.Presentation.find(slide.presentation_id).catch((err) => {
            next({
                status: this.HttpStatus.INTERNAL_SERVER_ERROR,
                message: "Could not retrieve presentation"
            });
        });

        // User does not have access to slides
        if (user.id != presentation.user_id) {
            next({
                status: this.HttpStatus.UNAUTHORIZED,
                message: "You are not authorized to perform this action!"
            });
        }

        await slide.destroy();

        this.logger.info("Successfully deleted slide");
        this.sendResponse(res, "Successfully deleted slide");
    }
}
