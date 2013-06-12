var argv = require('optimist').argv;
var rest = require('restler');

var handleResponse = function(data, response) {
	
	    if (data instanceof Error) {
           console.log('Error:');
		   console.log(data);
		   return;
        }		
        
		if (response.statusCode == 404) {
		    console.log('Error:');
		    console.log('REST endpoint not found');
			return;
		}
		
		console.log(data);
	};

var hostUrl = 'http://10.22.180.82/EventCommunicator/';
	
if (argv.postChange){

    if(!argv.id || !argv.addValue)
	{
	    console.log('Invalid arguments. Requires the following:');
		console.log('--id --addValue');
		return;
	}

    // rest.post('http://localhost/EventPlayerCommunicator', {
	rest.post(hostUrl + 'Command/', {
	    data: { 
		    modelId: argv.id, 
			commandData : '{"$type":"EventPlayer.Communicator.Models.Command.StubAddCommand, EventPlayer.Communicator","AddValue":' + argv.addValue + '}'
	    }
	}).on('complete', handleResponse );
}

if(argv.get){
    if(!argv.id)
	{
	    console.log('Invalid arguments. Requires the following:');
		console.log('--id');
		return;
	}
    rest.get(hostUrl + 'Command/Get?modelId=' + argv.id).on('complete', handleResponse );
}

if(argv.getChanges){
    if(!argv.id)
	{
	    console.log('Invalid arguments. Requires the following:');
		console.log('--id');
		return;
	}
    rest.get(hostUrl + 'Command/GetChanges?modelId=' + argv.id).on('complete', handleResponse );
}

if(argv.clearAllChanges){
    rest.post(hostUrl + 'Command/ClearAllChanges/').on('complete', handleResponse );
}