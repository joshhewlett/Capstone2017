/**
 * Imports
 */
import compression from 'compression';
import helmet from 'helmet';
import bodyParser from 'body-parser';

export default (app) => {
    app.logger.debug("Middleware Initialized");
    app.use(helmet());
    app.use(compression());
    app.use(bodyParser.json());
    app.use(bodyParser.urlencoded({
        extended: false
    }));
}
