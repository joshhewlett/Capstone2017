// Loads development environment variables from Capstone2017/server/.env file
import dotenv from 'dotenv'
if (process.env.NODE_ENV !== 'production') {
    dotenv.load();
}

/**
 * Node module imports
 */
import express from 'express';
import http from 'http';

/**
 * Project imports
 */
import helpers from './helpers';

const logger = helpers.logging();

logger.debug("Server logger created");


var app = express();
app.logger = logger;

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
