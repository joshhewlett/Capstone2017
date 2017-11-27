import BaseController from './base.controller';

export default class extends BaseController {
  constructor(app) {
    super(app);

    // Create a new Presentation object
    this.router.post('/', (req, res) => {
      this.createPresentation(req, res);
    });

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
    this.router.delete('/:id', (req, res) => {
      this.deletePresentation(req, res);
    });

  }


  async createPresentation(req, res){
    let user = req.user;
    let data = req.body;

    // Sanitize input and create presentation object
    // TODO
    let presentation = new this.Presentation({
      user: user.id,
      name: data.name,
      description: data.description
    }).catch((err) => {
       throw "Error creating presentation object";
    });

    // TODO
    await presentation.save().catch((err) => {
      throw {
        status: this.HttpStatus.INTERNAL_SERVER_ERROR,
        message: "Could not create presentation"
      };
    })

    this.sendResponse(res, presentation);
  });


  async getSlides(req, res){
    let user = req.user;
    // TODO
    let presentation = await Presentation.find(req.params.id).catch((err) => {
      throw {
        status: this.HttpStatus.INTERNAL_SERVER_ERROR,
        message: "Could not retrieve presentation"
      };
    });

    // User does not have access to slides
    if(user.id != presentation.user_id){
      this.sendResponse(res, "You don't have access to that!", this.HttpStatus.UNAUTHORIZED);
    }

    // Get slide objects
    // TODO
    let slides = await Slides.find({presentation_id: req.params.id}).catch((err) => {
      throw {
        status: this.HttpStatus.INTERNAL_SERVER_ERROR,
        message: "Could not retrive slides"
      };
    });

    this.sendResponse(res, slides);
  }

  async getPresentation(req, res){
    let user = req.user;
    // TODO
    let presentation = await Presentation.find(req.params.id).catch((err) => {
      throw {
        status: this.HttpStatus.INTERNAL_SERVER_ERROR,
        message: "Could not retreive presentation"
      };
    });

    // User does not have access to slides
    if(user.id != presentation.user_id){
      this.sendResponse(res, "You don't have access to that!", this.HttpStatus.UNAUTHORIZED);
    }

    this.sendResponse(res, slides);
  }

  async updatePresentation(req, res){
    let user = req.user;
    let data = req.body;
    // TODO
    let presentation = await Presentation.find(req.params.id).catch((err) => {
      throw {
        status: this.HttpStatus.INTERNAL_SERVER_ERROR,
        message: "Could not find presentation"
      };
    });

    // User does not have access to slides
    if(user.id != presentation.user_id){
      this.sendResponse(res, "You don't have access to that!", this.HttpStatus.UNAUTHORIZED);
    }

    if(data.name){
      // TODO: Validate type string
      presentation.name = data.name;
    }
    if(data.description){
      // TODO: Valideate type string
      presentation.description = data.description;
    }
    // TODO
    await presentation.save().catch((err) => {
      throw {
        status: this.HttpStatus.INTERNAL_SERVER_ERROR,
        message: "Could not update presentation"
      };
    });

    this.sendResponse(res, "Success");
  }

  async deletePresentation(req, res){
    let user = req.user;
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
    await Presentation.delete(presentation.id); // TODO

    this.sendResponse(res, "Success");
  }
}
