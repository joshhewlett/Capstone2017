// import emitters from './emitters';
import logging from '../helpers/logging.helper';
const logger = logging();

function update(data) {
    _log("Update", data);
}

function startPresentation(data) {
    _log("Start Presentation", data);
}

function slideChange(data) {
    _log("Slide Change", data);
}



function _log(msg, data) {
    if (data != null) {
        logger.info("=== Listeners - " + msg + ": ", data);
    } else {
        logger.info("=== Listeners - " + msg);
    }
}

// Export functions
export default {
    update: update,
    startPresentation: startPresentation,
    slideChange: slideChange
}
