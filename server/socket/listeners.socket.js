import emit from './emitters.socket';
import logging from '../helpers/logging.helper';
const logger = logging();

/*
 * Defines listeners
 * (Hololens adapter)
 * 
 * Maps 'socket.on' listeners to functions
 */
export default {
    update: update,
    startPresentation: startPresentation,
    slideChange: slideChange
}


// Listens to 'update' socket event
// Sends update event to a presentation model
function update(data) {
    _log("Update", data);
    // _log("IO object:", io);
    emit.transform_update(data);
    // emit('TRANSFORM_UPDATE', data);
}

// Sends a new slide event
function slideChange(data) {
    _log("Slide Change", data);
}

// Starts a live presentation
function startPresentation(data) {
    _log("Start Presentation", data);
}


// Convenience function for logging with Logger
function _log(msg, data) {
    if (data != null) {
        logger.info("=== Listeners - " + msg + ": ", data);
    } else {
        logger.info("=== Listeners - " + msg);
    }
}
