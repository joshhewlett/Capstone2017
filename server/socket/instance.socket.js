import socketIO from 'socket.io';

let instance;

/*
 * Singleton instance of our app's socket
 * 
 * Pass in app instance for instantiation
 * (done by server.js)
 */
export default (server) => {
    if (!instance && server) {
        instance = new socketIO(server);
    }
    return instance;
};
