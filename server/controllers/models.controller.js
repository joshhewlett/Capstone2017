import BaseController from './base.controller';

export default class extends BaseController {
  constructor(app) {
    super(app);

    // Create a new Model object
    this.router.post('/', (req, res) => {

    });

    // Returns the name of Model with id of :id
    this.router.get('/:id/name', (req, res) => {

    });

    // Returns object for Model with the id of :id
    this.router.get('/:id', (req, res) => {

    });

    // Returns paginated list of all Models belonging
    // to User with id of :id
    this.router.get('/user/:id', (req, res) => {

    });

    // Updates Model with id of :id
    this.router.put('/:id', (req, res) => {

    });

    // Deletes Model with id of :id
    this.router.delete('/:id', (req, res) => {

    });

  }

}
