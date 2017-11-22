import BaseController from './base.controller';

export default class extends BaseController {
  constructor(app) {
    super(app);

    // Create a new Slide object
    this.router.post('/', (req, res) => {

    });

    // Returns all the models belonging to Slide
    // with the id of :id
    this.router.get('/:id/models', (req, res) => {

    });

    // Returns object for Slide with the id of :id
    this.router.get('/:id', (req, res) => {

    });

    // Updates Slide with id of :id
    this.router.put('/:id', (req, res) => {

    });

    // Deletes Slide with id of :id
    this.router.delete('/:id', (req, res) => {

    });

  }

}
