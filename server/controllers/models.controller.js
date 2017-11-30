import BaseController from './base.controller';

export default class extends BaseController {
  constructor(app) {
    super(app);

    // Create a new Model object
    this.router.post('/', (req, res) => {
      this.createModel(req, res);
    });

    // Returns the name of Model with id of :id
    this.router.get('/:id/name', (req, res) => {
      this.getModelName(req, res);
    });

    // Returns object for Model with the id of :id
    this.router.get('/:id', (req, res) => {
      this.getModel(req,res);
    });

    // Returns paginated list of all Models belonging
    // to User with id of :id
    this.router.get('/user/:id', (req, res) => {
      this.getModelsUser(req, res);
    });

    // Updates Model with id of :id
    this.router.put('/:id', (req, res) => {
      this.updateModel(req, res);
    });

    // Deletes Model with id of :id
    this.router.delete('/:id', (req, res) => {
      this.deleteModel(req, res);
    });
  }

  async createModel(req, res){
    
  }

  async getModelName(req, res){

  }

  async getModel(req, res){

  }

  async getModelsUser(req, res){

  }

  async updateModel(req, res){

  }

  async deleteModel(req, res){
    
  }
}
