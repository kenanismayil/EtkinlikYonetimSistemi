"use strict"

$(document).ready(function () {
    var connectionCreate = new signalR.HubConnectionBuilder().withUrl("/changeType").build();

    setTimeout(function () {
        connectionCreate.on("load", function () {
            location.reload();
        });
    }, 2000);
    connectionCreate.start().catch(function (err) {
        return console.error(err.toString());
    });
});