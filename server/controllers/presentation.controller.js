import BaseController from './base.controller';

export default class extends BaseController {
  constructor(app) {
    super(app);

    // Create a new Presentation object
    this.router.post('/', (req, res) => {

    });

    // Returns all the slides belonging to Presentation
    // with the id of :id
    this.router.get('/:id/slides', (req, res) => {

    });

    // Returns object for Presentation with the id of :id
    this.router.get('/:id', (req, res) => {

    });

    // Updates Presentation with id of :id
    this.router.put('/:id', (req, res) => {

    });

    // Deletes Presentation with id of :id
    this.router.delete('/:id', (req, res) => {

    });

  }

}
