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
import socketIO from 'socket.io';
import path from 'path';

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


app.use(express.static(path.resolve() + '/node_modules'));
app.get("/", (req, res) => {
    res.send("Welcome to [HoloDeck]");
});


/**
 * Configure Passport
 */
helpers.passport(app);


/**
 * Configure Middleware
 */
helpers.middleware(app);
app.use((req, res, next) => {
    if (process.env.FAKE_USER_AUTHENTICATION === "true") {
        let id = parseInt(process.env.FAKE_USER_ID);
        models.user.findById(id).then(user => {
            req.user = user;
            next();
        });
    } else {
        next();
    }
});

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

let io = new socketIO(server);
io.on('connection', (client) => {
    helpers.socket(client);
    console.log("Hello");
});
