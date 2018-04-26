import models from '../models';
import emit from './emitters.socket';
import logging from '../helpers/logging.helper';
import events from './events';

const logger = logging();

// Set up models references
var User = models.user;
var Presentation = models.presentation;
var Slide = models.slide;
var SlideModel = models.slide_model;

let currentPresentation = {};


function connect(socket) {
    if (currentPresentation.currentSlide) {
        console.log("Emitting current slide");
        socket.emit(events.emit.slideChange, currentPresentation.currentSlide + "");
        for (let slide of currentPresentation.slides) {
            if (slide.id === currentPresentation.currentSlide) {
                for (let model of slide.models) {
                    console.log("Emitting update for model on connect");
                    socket.emit(events.emit.update, {
                        model: model.id,
                        position: model.transform.position,
                        rotation: model.transform.rotation,
                        scale: model.transform.scale
                    });
                }
            }
        }
    }
}

// Listens to 'update' socket event
// Sends update event to a presentation model
function update(data) {
    _log("Update", data);

    if (currentPresentation.slides) {
        for (let slide of currentPresentation.slides) {
            for (let model of slide.models) {
                if (model.id = data.model) {
                    model.transform = {
                        position: data.position,
                        rotation: data.rotation,
                        scale: data.scale
                    };
                }
            }
        }
    }
    // _log("IO object:", io);
    if (typeof data === "string") {
        data = JSON.parse(data);
    }
    emit.transform_update(data);
    // emit('TRANSFORM_UPDATE', data);
}

// Sends a new slide event
function slideChange(data) {
    _log("Slide Change", data);
    Slide.findById(data).then(res => {
        console.log(res);

        emit.slideChange(data);
        currentPresentation.currentSlide = parseInt(data);
        updateModelsForSlide(data);
    }).catch(err => {
        logger.error("Error retrieving slide from db with id of " + data);
        logger.error(err);
    })
}

function updateModelsForSlide(data) {
    for (let slide of currentPresentation.slides) {
        if (slide.id === data) {
            for (let model of slide.models) {
                emit.transform_update({
                    model: parseInt(data),
                    position: model.transform.position,
                    rotation: model.transform.rotation,
                    scale: model.transform.scale
                });
            }
        }
    }
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

            getAllPresentationData(res.id).then((allData) => {
                currentPresentation = allData.presentation;
                currentPresentation.currentSlide = allData.presentation.slides[0].id;
            });

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
            currentPresentation = {};
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


async function getAllPresentationData(id) {
    let presentation = await Presentation.findById(id).catch((err) => {
        this.handleError(next, "Failed to retrieve presentation");
    });

    let slides = await presentation.getSlides();

    // Prepare response payload
    let data = {};
    data.presentation = {};
    data.presentation.id = presentation.id;
    data.presentation.name = presentation.name;
    data.presentation.description = presentation.description;
    data.presentation.is_live = presentation.is_live;
    data.presentation.user_id = presentation.user_id;
    data.presentation.slides = [];

    // Get slide models for each slide
    for (let slide of slides) {
        let models = await slide.getModels();
        let plainSlide = slide.get({
            plain: true
        });

        plainSlide.models = [];
        for (let model of models) {
            let trans = JSON.parse(model.transform);
            plainSlide.models.push({
                id: model.id,
                poly_id: model.poly_id,
                transform: trans,
                created_at: model.created_at,
                updated_at: model.updated_at
            });
        }
        data.presentation.slides.push(plainSlide);

    }

    return data;
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
    presentationEnd: presentationEnd,
    connect
}
