import on from './listeners.socket';
import instance from './instance.socket';
import emit from './emitters.socket';
import events from './events';

/*
 * Socket module index
 * 
 * on = listeners
 * emit = emitters
 * instance = singleton instance of our app's socket
 */
export default {
    on,
    emit,
    instance,
    events
};
