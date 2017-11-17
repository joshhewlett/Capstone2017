/**
 * Imports
 */
import express from 'express';
import HttpStatus from 'http-status-codes';

export default class {
    constructor(app) {
        this.app = app;
        this.router = express.Router();
        this.HttpStatus = HttpStatus;
    }

    async sendResponse(res, data, status = HttpStatus.OK, headers) {
        if (!res || !data) {
            throw "Response and Data required";
        }
        for (const x of headers) {
            if (!x.name || !x.data) {
                throw "Improper header formatting";
            }
            res.set(x.name, x.data);
        }
        res.status(status).send(data);
    }
}
