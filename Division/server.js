var io = require('socket.io')({
	transports: ['websocket'],
});

io.attach(4567);

console.log("running");

io.on('connection', function(socket){

	console.log("connection");

	socket.on('input', function(data){
		socket.emit('input2server', data);
	});
})
