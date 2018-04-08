import config from '../config';

/**
 * Import controllers here
 */
import controllers from '../controllers';

export default (app) => {
    app.use(config.routing.auth, new controllers.AuthController(app).router);
    app.use(config.routing.presentation, new controllers.PresentationController(app).router);
    app.use(config.routing.slide, new controllers.SlideController(app).router);
    app.use(config.routing.model, new controllers.ModelController(app).router);
    app.use(config.routing.testing, new controllers.TestingController(app).router);
    app.use(config.routing.proxy, new controllers.ProxyController(app).router);
    app.logger.debug("Routing Initialized");
}
