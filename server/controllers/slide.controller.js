import BaseController from './base.controller';

export default class extends BaseController {
    constructor(app) {
        super(app);

        // Create a new Slide object
        this.router.post('/', (req, res) => {
            this.createSlide(req, res);
        });

        // Returns all the models belonging to Slide
        // with the id of :id
        this.router.get('/:id/models', (req, res) => {
            this.getSlideModels(req, res);
        });

        // Returns object for Slide with the id of :id
        this.router.get('/:id', (req, res) => {
            this.getSlide(req, res);
        });

        // Updates Slide with id of :id
        this.router.put('/:id', (req, res) => {
            this.updateSlide(req, res);
        });

        // Deletes Slide with id of :id
        this.router.delete('/:id', (req, res) => {
            this.deleteSlide(req, res);
        });

    }

    // Create a new slide for a given presentation
    async createSlide(req, res) {
        let user = req.user;
        let data = req.body;

        // Validate input
        if (!data.presentation_id) {
            throw {
                status: this.HttpStatus.BAD_REQUEST,
                message: "Slide must be added to a presentation."
            }
        }

        // Find presentation user is trying to add to
        let presentation = await this.Presentation.findById(data.presentation_id).catch((err) => {
            throw {
                status: this.HttpStatus.INTERNAL_SERVER_ERROR,
                message: "Error retrieving presentation."
            }
        });

        // Presentation does not exist
        if (!presentation) {
            throw {
                status: this.HttpStatus.NOT_FOUND,
                message: "Presentation does not exist."
            }
        }

        // User is not unauthorized.Does not own presentation
        if (user.id != presentation.user_id) {
            throw {
                status: this.HttpStatus.UNAUTHORIZED,
                message: "You don't have permission to do that!"
            }
        }

        // Calculate sequence number for new slide
        let sequenceNumber = await this.Slide.count({
            where: {
                presentation_id: presentation.id
            }
        }).catch((err) => {
            throw {
                status: this.HttpStatus.INTERNAL_SERVER_ERROR,
                message: "Error calculating sequence."
            }
        });

        // Create slide
        let slide = await this.Slide.create({
            presentation_id: data.presentation_id,
            sequence: sequenceNumber
        }).catch((err) => {
            throw {
                status: this.HttpStatus.INTERNAL_SERVER_ERROR,
                message: "Error creating slide."
            }
        });

        this.logger.info("Successfully created slide");
        this.sendResponse(res, slide);
    }

    // Get all models for a slide
    // Anyone can access
    async getSlideModels(req, res) {

        // Validate input
        if (!req.params.id) {
            throw {
                status: this.HttpStatus.BAD_REQUEST,
                message: "Invalid request."
            }
        }

        // Get slide_3d_model objects
        // let models = await SlideModel.find({
        //     slide_id: req.params.id
        // }).catch((err) => {
        //     throw {
        //         status: this.HttpStatus.INTERNAL_SERVER_ERROR,
        //         message: "Could not retrieve slide's models"
        //     }
        // });

        let models = await sequelize.query("SELECT * FROM slide_3d_model JOIN 3d_model ON slide_3d_model.model_id=3d_model.id WHERE slide_3d_model.slide_id=" + req.params.id).catch((err) => {
            throw {
                status: this.HttpStatus.INTERNAL_SERVER_ERROR,
                message: "Could not retrieve slide models."
            }
        });

        this.logger.info("Successfully received slide models");
        this.sendResponse(req, models);
    }

    // Get SlideModels for Slide
    async getSlide(req, res) {
        // TODO What is going to be different than the getModels?
    }

    // Delete a given slide.
    // Must have permission
    async deleteSlide(req, res) {
        let user = req.user;

        let slide = await this.Slide.find(req.params.id).catch((err) => {
            throw {
                status: this.HttpStatus.INTERNAL_SERVER_ERROR,
                message: "Could not retrieve slide."
            }
        });

        let presentation = this.Presentation.find(slide.presentation_id).catch((err) => {
            throw {
                status: this.HttpStatus.INTERNAL_SERVER_ERROR,
                message: "Could not retrieve presentation"
            }
        });

        // User does not have access to slides
        if (user.id != presentation.user_id) {
            throw {
                status: this.HttpStatus.UNAUTHORIZED,
                message: "You are not authorized to perform this action!"
            }
        }

        await slide.destroy();

        this.logger.info("Successfully deleted slide");
        this.sendResponse(res, "Successfully deleted slide");
    }
}
