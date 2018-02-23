import io from './instance.socket';
import logging from '../helpers/logging.helper';
import events from './events';
const logger = logging();
let emit = events.emit;

/* 
 * Defines emitters
 * (iOS adapter)
 * 
 * Maps 'socket.emit' emmiters to functions
 */
export default {
    transform_update: update
}

// Sends a transform update to a model
function update(data) {
    io().sockets.emit(emit.update, data);
}
