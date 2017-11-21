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

    async sendResponse(res, data, ...metadata) {
        let status = 200;
        if (!res || !data) {
            throw "Response and Data required";
        }
        if (metadata.length > 0) {
            if (typeof(metadata[0] === "number")) {
                status = metadata[0];
                metadata.unshift();
            }
            for (const x of metadata) {
                if (!x.name || !x.data) {
                    throw "Improper header formatting";
                }
                res.set(x.name, x.data);
            }
        }
        res.status(status).send(data);
    }
}
