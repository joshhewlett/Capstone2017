import socket from '../socket';


export default (client) => {
    // client.on('update', function(client) {
    //     console.log("Hello?");
    // });
    client.on('update', (data) => {
        socket.on.update(data);
    });

}
