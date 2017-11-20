/**
 * Imports
 */
import compression from 'compression';
import helmet from 'helmet';
import bodyParser from 'body-parser';
import session from 'express-session';

export default (app) => {
    app.logger.debug("Middleware Initialized");
    app.use(helmet());
    app.use(compression());
    app.use(bodyParser.json());
    app.use(bodyParser.urlencoded({
        extended: false
    }));
    app.use(session({
        secret: process.env.SESSION_SECRET,
        resave: false,
        saveUninitialized: false,
        name: "session",
        rolling: true,
        cookie: {
            httpOnly: true,
            maxAge: process.env.SESSION_MAX_AGE,
            secure: false
        }
    }));
}
