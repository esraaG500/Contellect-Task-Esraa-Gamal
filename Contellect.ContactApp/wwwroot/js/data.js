"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/DataHub").build();

connection.on("ReceiveMessage", function (user, rowID, pendingUpdateStatus) {
    if (pendingUpdateStatus == 0) {
        $('.disabledRow .btn').removeAttr('disabled');
        $('#Row' + rowID).removeClass("disabledRow");

    } else {
        $('#Row' + rowID).addClass("disabledRow");
        $('.disabledRow .btn').prop('disabled', true);
    }
});

connection.start().then(function () {
}).catch(function (err) {
    return console.error(err.toString());
});

//in the edit time
function sendButton(btn) {
    if ($('#ContactID').val() == "0") {
        console.log("click");
        var user = "1";
        var rowID = $(btn).attr("row-id");
        connection.invoke("SendMessage", user, rowID, 1).catch(function (err) {
            return console.error(err.toString());
        });
        $.get("/Contact/GetByID?id=" + rowID, function (result) {
            $('#ContactName').val(result.data["contactName"]);
            $('#ContactID').val(result.data["contactID"]);
            $('#ContactPhone').val(result.data["contactPhone"]);
            $('#ContactAddress').val(result.data["contactAddress"]);
            $('#ContactNotes').val(result.data["contactNotes"]);
        })
    }
}

//after editing
function sendChange(rowId) {
    var connection = new signalR.HubConnectionBuilder().withUrl("/DataHub").build();

    connection.start().then(function () {
        connection.invoke("SendMessage", "1", rowId ,0).catch(function (err) {
            return console.error(err.toString());
        });
    });
}