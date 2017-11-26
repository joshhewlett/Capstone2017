import models from '../models';
import helpers from '../helpers';

const logger = helpers.logging();

/**
 * Route specific middleware to ensure that the req body
 * has well formed data. Compares req body to the 
 * model whose name is passed as a parameter
 */
export default (modelName) => {
    return (req, res, next) => {
        var Model = models[modelName];
        if (!Model) {
            logger.warn("wrong model name");
            throw {
                status: 500,
                message: "Internal Server Error"
            };
        }
        try {
            req.model = new Model(req.body);
            next();
        } catch (err) {
            throw {
                status: 400,
                message: "Malformed Data"
            };
        }
    };
}
