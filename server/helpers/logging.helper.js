import winston from 'winston';

import config from '../config';

let instance;

export default () => {
    if (!instance) {
        instance = new Logger();
    }
    return instance;
};



function Logger() {
    var transports;

    if (process.env.NODE_ENV === "test") {
        transports = [
            new winston.transports.Console({
                level: config.logging.level.test,
                json: false,
                colorize: true
            })
        ];
    } else if (process.env.NODE_ENV === "prod") {
        transports = [
            new winston.transports.Console({
                level: config.logging.level.prod,
                json: false,
                colorize: true
            })
        ];
    } else {
        transports = [
            new winston.transports.Console({
                level: config.logging.level.dev,
                json: false,
                colorize: true
            })
        ];
    }

    var logger = new winston.Logger({
        transports: transports,
        exitOnError: false
    });

    logger.stream = {
        write: function(message, encoding) {
            logger.info(message);
        }
    };

    return logger;
}
