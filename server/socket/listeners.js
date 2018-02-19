// import emitters from './emitters';

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
        console.log("=== Listeners - " + msg + ": " + data);
    } else {
        console.log("=== Listeners - " + msg);
    }
}

// Export functions
export default {
    update: update,
    startPresentation: startPresentation,
    slideChange: slideChange
}
