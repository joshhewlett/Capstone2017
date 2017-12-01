import BaseController from './base.controller';

export default class extends BaseController {
    constructor(app) {
        super(app);

        // Create a new Model object
        this.router.post('/', (req, res) => {
            this.uploadModel(req, res);
        });

        // Returns the name of Model with id of :id
        this.router.get('/:id/name', (req, res) => {
            this.getModelName(req, res);
        });

        // Returns object for Model with the id of :id
        this.router.get('/:id', (req, res) => {
            this.getModel(req, res);
        });

        // Returns paginated list of all Models belonging
        // to User with id of :id
        this.router.get('/user/:id', (req, res) => {
            this.getModelsByUser(req, res);
        });

        // Deletes Model with id of :id
        this.router.delete('/:id', (req, res) => {
            this.deleteModel(req, res);
        });
    }

    async uploadModel(req, res) {

    }

    async getModelName(req, res) {

    }

    async getModel(req, res) {

    }

    async getModelsByUser(req, res) {

    }

    // Delete a model from db
    async deleteModel(req, res) {
        let user = req.user;

        let model = this.Model.findById(req.params.id).catch(err => {
            throw {
                status: this.HttpStatus.INTERNAL_SERVER_ERROR,
                message: "Error retrieving model"
            };
        });

        // User does not have access to slides
        if (user.id != model.user_id) {
            throw {
                status: this.HttpStatus.UNAUTHORIZED,
                message: "You are not authorized to perform this action."
            };
        }

        await model.destroy();

        this.sendResponse(res, "Successfully deleted model");
    }
}
