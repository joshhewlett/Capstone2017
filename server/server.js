/**
 * Node module imports
 */
import express from 'express';
import http from 'http';

/**
 * Project imports
 */
import helpers from './helpers';
import models from './models';
import db from './db';
import config from './config';

const logger = helpers.logging();

logger.debug("Server logger created");

/**
 * Initialize App with globals
 */
var app = express();
app.logger = logger;
app.models = models;
app.db = db;
app.config = config;

/**
 * Configure Passport
 */
helpers.passport(app);


/**
 * Configure Middleware
 */
helpers.middleware(app);


/**
 * Setup routing
 */
helpers.routing(app);


/**
 * Start HTTP Server
 */
var server = http.Server(app);
var port = process.env.SERVER_PORT;
server.listen(port, () => {
    logger.info("Server listening on port ", port);
});
