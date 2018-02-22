import BaseController from './base.controller';

export default class extends BaseController {
    constructor(app) {
        super(app);

        // Create a new Presentation object
        this.router.post('/', (req, res, next) => {
            this.createPresentation(req, res, next);
        });

        // Get all presentations associated with user
        this.router.get('/', (req, res, next) => {
            this.getUsersPresentation(req, res, next);
        })

        // Returns all the slides belonging to Presentation
        // with the id of :id
        this.router.get('/:id/slides', (req, res, next) => {
            this.getSlides(req, res, next);
        });

        // Returns object for Presentation with the id of :id
        this.router.get('/:id', (req, res, next) => {
            this.getPresentation(req, res, next);
        });

        // Updates Presentation with id of :id
        this.router.put('/:id', (req, res, next) => {
            this.updatePresentation(req, res, next);
        });

        // Deletes Presentation with id of :id
        this.router.delete('/:id', this.app.middleware.authenticated, (req, res, next) => {
            this.deletePresentation(req, res, next);
        });

        this.router.get('/:id/all', (req, res, next) => {
            this.getAllPresentationData(req, res, next);
        });

    }

    // Create a new presentation
    async createPresentation(req, res, next) {
        let user = req.user;
        let data = req.body;

        // Sanitize input
        // Assume request can only get here if user exists
        let sanitizedData = {}
        if (!data.name || typeof data.name !== 'string') {
            // data.name cannot be null and must be a string
            this.handleError(next, "Invalid input", this.HttpStatus.BAD_REQUEST);
        } else {
            sanitizedData.name = data.name;
        }

        if (data.description && typeof data.description !== 'string') {
            // data.description must be a string
            this.handleError(next, "Invalid input", this.HttpStatus.BAD_REQUEST);
        } else if (data.description) {
            sanitizedData.description = data.description;
        }

        // Create presentation
        let presentation = await this.Presentation.create({
            user_id: user.id,
            name: sanitizedData.name,
            description: sanitizedData.description
        }).catch((err) => {
            this.handleError(next, "Failed to create presentation");
        });

        this.logger.info("Successfully created presentation")
        this.sendResponse(res, presentation);
    }

    // Find all presentations associated with user
    async getUsersPresentation(req, res, next) {
        // Assume user exists if they got to this point
        let user = req.user;

        // Search database for presentations
        let presentations = await user.getPresentations().catch((err) => {
            this.handleError(next, "Failed to find presentations");
        });

        this.logger.info("Successfully received presentations");
        this.sendResponse(res, presentations);
    }

    // Retrieve all slides for a given presentation
    async getSlides(req, res, next) {
        let user = req.user;

        let presentation = await this.Presentation.findById(req.params.id).catch((err) => {
            this.handleError("Failed to retrieve presentation");
        });

        // User does not have access to slides
        if (user.id != presentation.user_id) {
            this.handleError(next, "You are not authorized to perform this action!", this.HttpStatus.UNAUTHORIZED);
        }

        // Get slide objects
        let slides = await presentation.getSlides().catch((err) => {
            this.handleError(next, "Failed to retrieve slides");
        });

        let responseData = {};
        responseData.slides = slides;

        this.sendResponse(res, responseData);
    }

    // Returns a presentation object with the given id
    async getPresentation(req, res, next) {
        let user = req.user;

        console.log(user);
        let presentation = await this.Presentation.findById(req.params.id).catch((err) => {
            this.handleError(next, "Failed to retrieve presentation");
        });

        // Check if presentaiton is null
        if (!presentation) {
            this.handleError(next, "Presentation not found", this.HttpStatus.NOT_FOUND);
        }

        // User does not have access to slides
        if (user.id != presentation.user_id) {
            this.handleError(next, "You don't have access to that!", this.HttpStatus.UNAUTHORIZED);
        }

        this.sendResponse(res, presentation);
    }

    // Update a presentation with the given id
    async updatePresentation(req, res, next) {
        let user = req.user;
        let data = req.body;

        let presentation = await this.Presentation.findById(req.params.id).catch((err) => {
            this.handleError(next, "Failed to retrieve presentation");
        });

        // User does not have access to slides
        if (user.id != presentation.user_id) {
            this.handleError(next, "You don't have access to that!", this.HttpStatus.UNAUTHORIZED);
        }

        if (data.name && typeof data.name === "string") {
            presentation.name = data.name;
        }
        if (data.description && typeof data.description === "string") {
            presentation.description = data.description;
        }

        await presentation.save().catch((err) => {
            this.handleError(next, "Failed to update presentation");
        });

        this.sendResponse(res, presentation);
    }

    // Delete a presentation object with a given id
    async deletePresentation(req, res, next) {
        let user = req.user;

        let presentation = await this.Presentation.findById(req.params.id).catch((err) => {
            this.handleError(next, "Failed to retrieve presentation");
        });

        // User does not have access to slides
        if (user.id != presentation.user_id) {
            this.handleError(next, "You are not authorized to perform this action", this.HttpStatus.UNAUTHORIZED);
        }

        await presentation.destroy();

        this.sendResponse(res, "Successfully deleted presentation");
    }

    // Get all data for initializing presentations on clients
    async getAllPresentationData(req, res, next) {
        // Get requested presentation object
        let presentation = await this.Presentation.findById(req.params.id).catch((err) => {
            this.handleError(next, "Failed to retrieve presentation");
        });

        let slides = await presentation.getSlides();

        // Prepare response payload
        let data = {};
        data.presentation = {};
        data.presentation.id = presentation.id;
        data.presentation.name = presentation.name;
        data.presentation.description = presentation.description;
        data.presentation.is_live = presentation.is_live;
        data.presentation.user_id = presentation.user_id;
        data.presentation.slides = [];

        // Get slide models for each slide
        for (let slide of slides) {
            let models = await slide.getModels();
            let plainSlide = slide.get({
                plain: true
            });

            plainSlide.models = [];
            for (let model of models) {
                let trans = JSON.parse(model.transform);
                plainSlide.models.push({
                    id: model.id,
                    poly_id: model.poly_id,
                    transform: trans,
                    created_at: model.created_at,
                    updated_at: model.updated_at
                });
            }
            data.presentation.slides.push(plainSlide);

        }

        this.sendResponse(res, data);
    }
}
