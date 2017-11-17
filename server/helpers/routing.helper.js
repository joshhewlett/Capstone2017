import config from '../config';

/**
 * Import controllers here
 */
import controllers from '../controllers';

export default (app) => {
    app.logger.debug("Routing Initialized");
    // app.use(config.routing.path.user, new controllers.Controller(app).router)
}
