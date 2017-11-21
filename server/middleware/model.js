import models from '../models';
import helpers from '../helpers';

const logger = helpers.logging();


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
