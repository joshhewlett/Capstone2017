import socket from '../socket';
let on = socket.events.on;

/*
 * Defines events Socket.IO listens for
 * and forwards them to our 'socket' module
 */
export default (client) => {

    socket.on.connect(client);

    client.on(on.update, (data) => {
        socket.on.update(data);
    });

    client.on(on.slideChange, (data) => {
        socket.on.slideChange(data);
    });

    client.on(on.presentationStart, (data) => {
        socket.on.presentationStart(data);
    });

    client.on(on.presentationEnd, (data) => {
        socket.on.presentationEnd(data);
    })
}
