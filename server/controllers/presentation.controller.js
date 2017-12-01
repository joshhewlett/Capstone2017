import BaseController from './base.controller';

export default class extends BaseController {
    constructor(app) {
        super(app);

        // Create a new Presentation object
        this.router.post('/', (req, res) => {
            this.createPresentation(req, res);
        });

        // Get all presentations associated with user
        this.router.get('/', (req, res) => {
            this.getUsersPresentation(req, res);
        })

        // Returns all the slides belonging to Presentation
        // with the id of :id
        this.router.get('/:id/slides', (req, res) => {
            this.getSlides(req, res);
        });

        // Returns object for Presentation with the id of :id
        this.router.get('/:id', (req, res) => {
            this.getPresentation(req, res);
        });

        // Updates Presentation with id of :id
        this.router.put('/:id', (req, res) => {
            this.updatePresentation(req, res);
        });

        // Deletes Presentation with id of :id
        this.router.delete('/:id', this.app.middleware.authenticated, (req, res) => {
            this.deletePresentation(req, res);
        });

    }

    // Create a new presentation
    async createPresentation(req, res) {
        let user = req.user;
        let data = req.body;

        console.log(user);
        console.log("DATA: ", data);
        // Sanitize input
        // Assume request can only get here if user exists
        let sanitizedData = {}
        if (!data.name || data.name !== 'string') {
            // data.name cannot be null and must be a string
            throw {
                status: this.HttpStatus.BAD_REQUEST,
                message: "Invalid input"
            }
        } else {
            sanitizedData.name = data.name;
        }

        if (data.description && typeof data.description !== 'string') {
            // data.description must be a string
            throw {
                status: this.HttpStatus.BAD_REQUEST,
                message: "Invalid input"
            }
        } else if (data.description) {
            sanitizedData.description = data.description;
        }

        // Create presentation
        let presentation = this.Presentation.create({
            user_id: user.id,
            name: sanitizedData.name,
            description: sanitizedData.description
        }).catch((err) => {
            throw {
                status: this.HttpStatus.INTERNAL_SERVER_ERROR,
                message: "Error creating presentation object"
            }
        });

        this.logger.info("Successfully created presentation")
        this.sendResponse(res, presentation);
    }

    // Find all presentations associated with user
    async getUsersPresentation(req, res) {
        // Assume user exists if they got to this point
        let user = req.user;

        // Search database for presentations
        let presentations = await this.Presentation.findAll({
            where: {
                user_id: user.id
            }
        }).catch((err) => {
            throw "Error finding presentations"
        });

        this.logger.info("Successfully received presentations");
        this.sendResponse(res, presentations);
    }

    // Retrieve all slides for a given presentation
    async getSlides(req, res) {
        let user = req.user;

        let presentation = await this.Presentation.findById(req.params.id).catch((err) => {
            throw {
                status: this.HttpStatus.INTERNAL_SERVER_ERROR,
                message: "Could not retrieve presentation"
            };
        });

        // User does not have access to slides
        if (user.id != presentation.user_id) {
            throw {
                status: this.HttpStatus.UNAUTHORIZED,
                message: "You are not authorized to perform this action!"
            }
        }

        // Get slide objects
        let slides = await this.Slides.find({
            where: {
                presentation_id: req.params.id
            }
        }).catch((err) => {
            throw {
                status: this.HttpStatus.INTERNAL_SERVER_ERROR,
                message: "Could not retrive slides"
            };
        });

        this.sendResponse(res, slides);
    }

    // Returns a presentation object with the given id
    async getPresentation(req, res) {
        let user = req.user;
        console.log(user);
        let presentation = await this.Presentation.findById(req.params.id).catch((err) => {
            throw {
                status: this.HttpStatus.INTERNAL_SERVER_ERROR,
                message: "Could not retreive presentation"
            };
        });

        // User does not have access to slides
        if (user.id != presentation.user_id) {
            throw {
                status: this.HttpStatus.UNAUTHORIZED,
                message: "You don't have access to that!"
            }
        }

        this.sendResponse(res, presentation);
    }

    // Update a presentation with the given id
    async updatePresentation(req, res) {
        let user = req.user;
        let data = req.body;

        let presentation = await this.Presentation.findById(req.params.id).catch((err) => {
            throw {
                status: this.HttpStatus.INTERNAL_SERVER_ERROR,
                message: "Could not find presentation"
            };
        });

        // User does not have access to slides
        if (user.id != presentation.user_id) {
            throw {
                status: this.HttpStatus.UNAUTHORIZED,
                message: "You don't have access to that!"
            }
        }

        if (data.name && typeof data.name === "string") {
            presentation.name = data.name;
        }
        if (data.description && typeof data.description === "string") {
            presentation.description = data.description;
        }

        await presentation.save().catch((err) => {
            throw {
                status: this.HttpStatus.INTERNAL_SERVER_ERROR,
                message: "Could not update presentation"
            };
        });

        this.sendResponse(res, presentation);
    }

    // Delete a presentation object with a given id
    async deletePresentation(req, res) {
        let user = req.user;

        let presentation = this.Presentation.findById(req.params.id).catch((err) => {
            throw {
                status: this.HttpStatus.INTERNAL_SERVER_ERROR,
                message: "Could not find presentation"
            };
        });

        // User does not have access to slides
        if (user.id != presentation.user_id) {
            this.sendResponse(res, "You don't have access to that!", this.HttpStatus.UNAUTHORIZED);
        }

        await presentation.destroy();

        this.sendResponse(res, "Success");
    }
}
