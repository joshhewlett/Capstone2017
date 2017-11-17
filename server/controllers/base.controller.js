/**
 * Imports
 */
import models from '../models';
import db from '../db';
import config from '../config';

export default class {
    constructor(app) {
        this.models = models;
        this.db = db;
        this.config = config;
    }
}
