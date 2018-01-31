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
import middleware from './middleware';

const logger = helpers.logging();

logger.debug("Server logger created");

/**
 * Initialize App with globals
 */
let app = express();
app.logger = logger;
app.models = models;
app.db = db;
app.config = config;
app.middleware = middleware;

app.get("/", (req, res) => {
    res.send("Hello, Doug!");
});


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

app.use((err, req, res, next) => {
    console.log(err);
    res.status(500).send(err);
})
/**
 * Start HTTP Server
 */
var server = http.Server(app);
var port = process.env.SERVER_PORT;
server.listen(port, () => {
    logger.info("Server listening on port ", port);
});
