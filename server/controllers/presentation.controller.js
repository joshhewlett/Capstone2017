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
    this.router.delete('/:id', (req, res) => {
      this.deletePresentation(req, res);
    });

  }

  async createPresentation(req, res){
    let user = req.user;
    let data = req.body;

    console.log(user);
    console.log("DATA: ", data);
    // Sanitize input
    // Assume request can only get here if user exists
    let sanitizedData = {}
    if(!data.name){
      // data.name cannot be null
      throw {
        status: this.HttpStatus.BAD_REQUEST,
        message: "Invalid input"
      }
    }else if(typeof data.name === 'String'){
      sanitizedData.name = data.name;
    }
    if(typeof data.description === 'String'){
      sanitizedData.description = data.description;
    }

    // Create presentation
    await this.Presentation.create({
      user_id: user.id,
      name: sanitizedData.name,
      description: sanitizedData.description
    }).then((presentation) => {
      this.logger.info("Successfully created presentation")
      // this.sendResponse(res, presentation);
    }).catch((err) => {
      throw "Error creating presentation object";
    });
    this.sendResponse(res, presentation);
  }

  async getUsersPresentation(req, res){
    res.status(200).send("We in the clear, boys");
  }

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
