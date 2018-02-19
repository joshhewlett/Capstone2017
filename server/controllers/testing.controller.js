import BaseController from './base.controller';
import path from 'path';

export default class extends BaseController {
    constructor(app) {
        super(app);
        this.staticDir = path.resolve() + '/public/';

        // Get main test page
        this.router.get('/', (req, res) => {
            this.getMainPage(req, res);
        });

        this.router.get('/socket', (req, res) => {
            this.getSocketPage(req, res);
        });

    }

    async getMainPage(req, res) {
        res.sendFile(this.staticDir + 'index.html');

    }

    async getSocketPage(req, res) {
        res.sendFile(this.staticDir + 'socketTest.html');
    }

}
