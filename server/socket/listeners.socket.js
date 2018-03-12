import models from '../models';
import emit from './emitters.socket';
import logging from '../helpers/logging.helper';

const logger = logging();

// Set up models references
var User = models.user;
var Presentation = models.presentation;
var Slide = models.slide;
var SlideModel = models.slide_model;



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
function presentationStart(data) {
    _log("Start Presentation", data);

    // Get presentation from DB and
    // set is_live to true
    Presentation.findById(data).then((res) => {
        // Emit 'presentation_start' event if presentation is not
        // already live
        if (!res.is_live) {
            res.is_live = true;
            res.save();
            emit.presentationStart(data);
            _log("Presentation now live");
        }
    }).catch(err => {
        logger.error("Error retrieving presentation from db with id of " + data);
        logger.error(err);
    });
}

function presentationEnd(data) {
    _log("End Presentation", data);

    // Get presentation from DB and 
    // set is_live to false
    Presentation.findById(data).then((res) => {
        // Emit 'presentation_end' event if 
        // presentation is already live
        if (res.is_live) {
            res.is_live = false;
            res.save();
            emit.presentationEnd(data);
            _log("Presentation no longer live")
        }
    }).catch(err => {
        logger.error("Error retrieving presentation from db with id of " + data);
        logger.error(err);
    });
}


// Convenience function for logging with Logger
function _log(msg, data) {
    if (data != null) {
        logger.info("=== Listeners - " + msg + ": ", data);
    } else {
        logger.info("=== Listeners - " + msg);
    }
}

/*
 * Defines listeners
 * (Hololens adapter)
 * 
 * Maps 'socket.on' listeners to functions
 */
export default {
    update: update,
    presentationStart: presentationStart,
    slideChange: slideChange,
    presentationEnd: presentationEnd
}
