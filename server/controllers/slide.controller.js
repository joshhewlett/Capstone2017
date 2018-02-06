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
        let data = req.body;

        // Validate input
        if (!data.presentation_id) {
            this.handleError(next, "Slide needs a presentation", this.HttpStatus.BAD_REQUEST);
        }

        // Find presentation user is trying to add to
        let presentation = await this.Presentation.findById(data.presentation_id).catch((err) => {
            this.handleError(next, "Failed to retrieve presentaiton");
        });

        // Presentation does not exist
        if (!presentation) {
            this.handleError(next, "Presentation not found", this.HttpStatus.NOT_FOUND);
        }

        // User is not unauthorized.Does not own presentation
        if (user.id != presentation.user_id) {
            this.handleError(next, "You don't have permission to do that!", this.HttpStatus.UNAUTHORIZED);
        }

        // Calculate sequence number for new slide
        let sequenceNumber = await this.Slide.count({
            where: {
                presentation_id: presentation.id
            }
        }).catch((err) => {
            this.handleError(next, "Failed to calculate sequence");
        });

        // Create slide
        let slide = await this.Slide.create({
            presentation_id: data.presentation_id,
            sequence: sequenceNumber
        }).catch((err) => {
            this.handleError(next, "Failed to create slide");
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

        let models;
        try {
            let slide = await this.Slide.findById(req.params.id);
            models = await slide.getModels()
        } catch (err) {
            this.handleError(next, "Failed to retrieve slide models");
        }

        this.logger.info("Successfully retrieved slide models");
        this.sendResponse(res, models);
    }

    // Get SlideModels for Slide
    async getSlide(req, res, next) {
        // TODO What is going to be different than the getModels?
        this.sendResponse(res, "Retrieved slide with id of " + req.params.id);
    }

    // Delete a given slide.
    // Must have permission
    async deleteSlide(req, res, next) {

        let user = req.user;

        let slide = await this.Slide.findById(req.params.id).catch((err) => {
            this.handleError(next, "Failed to retrieve slide");
        });

        let presentation = await slide.getPresentation().catch((err) => {
            this.handleError(next, "Failed to retrieve presentaiton");
        });

        // User does not have access to slides
        if (user.id != presentation.user_id) {
            this.handleError(next, "You are not authorized to perform this action!", this.HttpStatus.UNAUTHORIZED);
        }

        await slide.destroy();

        this.logger.info("Successfully deleted slide");
        this.sendResponse(res, "Successfully deleted slide");
    }
}
