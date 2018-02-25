import BaseController from './base.controller';
import path from 'path';
import ax from 'axios';

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

        this.router.get('/asset/:id', (req, res) => {
            this.getPolyAsset(req, res);
        })

    }

    async getMainPage(req, res) {
        res.sendFile(this.staticDir + 'index.html');

    }

    async getSocketPage(req, res) {
        res.sendFile(this.staticDir + 'socketTest.html');
    }

    async getPolyAsset(req, res) {

        ax.get("https://poly.googleapis.com/v1/assets/" + req.params.id + "?key=AIzaSyAQrOikhKk6HZLYc61FoOIrVdhtkXr9ltU").then(result => {
            res.send(result.data);
        });
    }

}
