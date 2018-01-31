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

    }

    // Create a new presentation
    async createPresentation(req, res, next) {
        let user = req.user;
        if (process.env.FAKE_USER_AUTHENTICATION === "true") {
            user = {};
            user.id = parseInt(process.env.FAKE_USER_ID);
        }

        let data = req.body;

        console.log(user);
        console.log("DATA: ", data);
        // Sanitize input
        // Assume request can only get here if user exists
        let sanitizedData = {}
        if (!data.name || typeof data.name !== 'string') {
            // data.name cannot be null and must be a string
            next({
                status: this.HttpStatus.BAD_REQUEST,
                message: "Invalid input"
            });
        } else {
            sanitizedData.name = data.name;
        }

        if (data.description && typeof data.description !== 'string') {
            // data.description must be a string
            next({
                status: this.HttpStatus.BAD_REQUEST,
                message: "Invalid input"
            });
        } else if (data.description) {
            sanitizedData.description = data.description;
        }

        // Create presentation
        let presentation = await this.Presentation.create({
            user_id: user.id,
            name: sanitizedData.name,
            description: sanitizedData.description
        }).catch((err) => {
            next({
                status: this.HttpStatus.INTERNAL_SERVER_ERROR,
                message: "Error creating presentation object"
            });
        });

        this.logger.info("Successfully created presentation")
        this.sendResponse(res, presentation);
    }

    // Find all presentations associated with user
    async getUsersPresentation(req, res, next) {
        // Assume user exists if they got to this point
        let user = req.user;
        if (process.env.FAKE_USER_AUTHENTICATION === "true") {
            user = {};
            user.id = parseInt(process.env.FAKE_USER_ID);
        }

        // Search database for presentations
        let presentations = await this.Presentation.findAll({
            where: {
                user_id: user.id
            }
        }).catch((err) => {
            next({
                status: HttpStatus.INTERNAL_SERVER_ERROR,
                message: "Error finding presentations"
            });
        });

        this.logger.info("Successfully received presentations");
        this.sendResponse(res, presentations);
    }

    // Retrieve all slides for a given presentation
    async getSlides(req, res, next) {
        let user = req.user;
        if (process.env.FAKE_USER_AUTHENTICATION === "true") {
            user = {};
            user.id = parseInt(process.env.FAKE_USER_ID);
        }

        let presentation = await this.Presentation.findById(req.params.id).catch((err) => {
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

        // Get slide objects
        let slides = await this.Slide.findAll({
            where: {
                presentation_id: req.params.id
            }
        }).catch((err) => {
            next({
                status: this.HttpStatus.INTERNAL_SERVER_ERROR,
                message: "Could not retrive slides"
            });
        });

        let responseData = {};
        responseData.slides = slides;

        this.sendResponse(res, responseData);
    }

    // Returns a presentation object with the given id
    async getPresentation(req, res, next) {
        let user = req.user;
        if (process.env.FAKE_USER_AUTHENTICATION === "true") {
            user = {};
            user.id = parseInt(process.env.FAKE_USER_ID);
        }

        console.log(user);
        let presentation = await this.Presentation.findById(req.params.id).catch((err) => {
            next({
                status: this.HttpStatus.INTERNAL_SERVER_ERROR,
                message: "Could not retreive presentation"
            });
        });

        // Check if presentaiton is null
        if (!presentation) {
            next({
                status: this.HttpStatus.NOT_FOUND,
                message: "Presentation not found"
            });
        }

        // User does not have access to slides
        if (user.id != presentation.user_id) {
            next({
                status: this.HttpStatus.UNAUTHORIZED,
                message: "You don't have access to that!"
            });
        }

        this.sendResponse(res, presentation);
    }

    // Update a presentation with the given id
    async updatePresentation(req, res, next) {
        let user = req.user;
        if (process.env.FAKE_USER_AUTHENTICATION === "true") {
            user = {};
            user.id = parseInt(process.env.FAKE_USER_ID);
        }

        let data = req.body;

        let presentation = await this.Presentation.findById(req.params.id).catch((err) => {
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

        if (data.name && typeof data.name === "string") {
            presentation.name = data.name;
        }
        if (data.description && typeof data.description === "string") {
            presentation.description = data.description;
        }

        await presentation.save().catch((err) => {
            next({
                status: this.HttpStatus.INTERNAL_SERVER_ERROR,
                message: "Could not update presentation"
            });
        });

        this.sendResponse(res, presentation);
    }

    // Delete a presentation object with a given id
    async deletePresentation(req, res, next) {
        let user = req.user;
        if (process.env.FAKE_USER_AUTHENTICATION === "true") {
            user = {};
            user.id = parseInt(process.env.FAKE_USER_ID);
        }

        let presentation = await this.Presentation.findById(req.params.id).catch((err) => {
            next({
                status: this.HttpStatus.INTERNAL_SERVER_ERROR,
                message: "Error retrieving presentation"
            });
        });

        // User does not have access to slides
        console.log(user + "\n" + presentation);
        if (user.id != presentation.user_id) {
            next({
                status: this.HttpStatus.UNAUTHORIZED,
                message: "You are not authorized to perform this action."
            });
        }

        await presentation.destroy();

        this.sendResponse(res, "Successfully deleted presentation");
    }
}
