import config from '../config';

/**
 * Import controllers here
 */
import controllers from '../controllers';

export default (app) => {
    app.use(config.routing.auth, new controllers.AuthController(app).router);
    app.logger.debug("Routing Initialized");
}
