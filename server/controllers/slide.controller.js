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

  async createSlide(req, res){
    let user = req.user;
    let data = req.body;

    // Santize input and create slide object
    // TODO
    let slide = new this.Slide({
      presentation_id: data.presentation_id
    }).catch((err) => {
      throw "Error creating slide object"
    });

    // TODO
    await slide.save().catch((err) => {
      throw {
        status: this.HttpStatus.INTERNAL_SERVER_ERROR,
        message: "Could not create slide"
      }
    });

    this.sendResponse(res, slide);
  }

  async getSlideModels(req, res){
    let user = req.user;

    // TODO
    let presentation = await presentation.find(req.params.id).catch((err) => {
      throw {
        status: this.HttpStatus.INTERNAL_SERVER_ERROR,
        message: "Could not retrieve presentation"
      }
    });

    // User does not have access to the slides
    if (user.id != presentation.user_id){
      this.sendResponse(res, "You don't have access to that!", this.HttpStatus.UNAUTHORIZED);
    }

    // Get slide_3d_model objects
    // TODO
    let models = await SlideObjects.find({slide_id: req.params.id}).catch((err) => {
      throw {
        status: this.HttpStatus.INTERNAL_SERVER_ERROR,
        message: "Could not retrieve slide's models"
      }
    });

    this.sendResponse(req, slides);
  }

  async getSlide(req, res){
    // TODO
    // Hella Metaaa
    
  }

  async deleteSlide(req, res){
    let user = req.user;
    // TODO
    let slide = await Slide.find(req.params.id).catch((err) => {
      throw {
        status: this.HttpStatus.INTERNAL_SERVER_ERROR,
        message: "Could not find slide"
      }
    });

    // TODO
    let presentation = Presentation.find(req.params.id).catch((err) => {
      throw {
        status: this.HttpStatus.INTERNAL_SERVER_ERROR,
        message: "Could not find presentation"
      };
    });

    // User does not have access to slides
    if(user.id != presentation.user_id){
      this.sendResponse(res, "You don't have access to that!", this.HttpStatus.UNAUTHORIZED);
    }

    // TODO
    await Slide.delete(slide.id); // TODO

    this.sendResponse(res, "Success");
  }
}