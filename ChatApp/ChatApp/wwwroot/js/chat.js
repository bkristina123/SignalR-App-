var connection = new signalR.HubConnectionBuilder()
    .withUrl("/chatHub")
    .build();

var _connectionId = '';

connection.on("RecieveMessage", function (data) {
    console.log(data);
});


var joinRoom = function () {
    var url = '/Chat/JoinRoom/' + _connectionId + '/ @Model.Name'
    axios.post(url, null)
        .then(res => {
            console.log("Room Joined", res);
        }).catch(err => {
            console.log("Failed to join room", err)
        })
};

connection.start()
    .then(function () {
        connection.invoke('getConnectionId')
            .then(function (connectionId) {
                _connectionId = connectionId;
                joinRoom();
            })
    })
    .catch(function (err) {
        console.log(err)
    });