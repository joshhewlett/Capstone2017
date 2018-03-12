import io from './instance.socket';
import logging from '../helpers/logging.helper';
import events from './events';
const logger = logging();
let emit = events.emit;


// Sends a transform update to a model
function update(data) {
    io().sockets.emit(emit.update, data);
}

// Sends an event marking a presentation as live
function presentationStart(id) {
    _log("Presentation is live", id);
    io().sockets.emit(emit.presentationStart, id);
}

// Sends an event marking a presentation as no longer live
function presentationEnd(id) {
    _log("Presentation is no longer live", id);
    io().sockets.emit(emit.presentationEnd, id);
}

// Convenience function for logging with Logger
function _log(msg, data) {
    if (data != null) {
        logger.info("=== Emitters - " + msg + ": ", data);
    } else {
        logger.info("=== Emitters - " + msg);
    }
}

/* 
 * Defines emitters
 * (iOS adapter)
 * 
 * Maps 'socket.emit' emmiters to functions
 */
export default {
    transform_update: update,
    presentationStart: presentationStart,
    presentationEnd: presentationEnd
}
