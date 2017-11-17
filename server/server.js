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
import compression from 'compression';
import helmet from 'helmet';
import bodyParser from 'body-parser';


/**
 * Project imports
 */
import helpers from './helpers';

const logger = helpers.logging();

logger.debug("Server logger created");


/**
 * Server starts here
 */
var app = express();

/**
 * Configure Middleware
 */
app.use(helmet());
app.use(compression());
app.use(bodyParser.json());
app.use(bodyParser.urlencoded({
    extended: false
}));

var server = http.Server(app);
var port = process.env.SERVER_PORT;
server.listen(port, () => {
    logger.info("Server listening on port ", port);
});
