import BaseController from './base.controller';
import path from 'path';
import ax from 'axios';

export default class extends BaseController {

    constructor(app) {
        super(app);

        // Proxy route for grabbing asset information from Poly
        this.router.get('/asset/:id', (req, res) => {
            this.getPolyAsset(req, res);
        });

    }

    /**
     * Proxy endpoint for [Poly.com]/assets/:id
     * @param {Number} req.params.id Id of Poly asset to retrieve
     */
    async getPolyAsset(req, res) {

        ax.get("https://poly.googleapis.com/v1/assets/" + req.params.id + "?key=AIzaSyAQrOikhKk6HZLYc61FoOIrVdhtkXr9ltU").then(result => {
            res.send(result.data);
        });
    }
}
