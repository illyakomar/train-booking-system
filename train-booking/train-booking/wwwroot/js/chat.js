const connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

const UserId = $("#my-user-id").val();

document.addEventListener("DOMContentLoaded", function () {
    const chatContainer = document.getElementById("chat-container");
    chatContainer.scrollTop = chatContainer.scrollHeight;
});

connection.on("ReceiveMessage", function (user, message) {
    const isMine = UserId == user.id;

    if (isMine) {

        $('#messages-container').append(
            `
            <div class="media w-50 ml-auto mb-3">
                <div class="media-body">
                    <div class="bg-light rounded py-2 px-3 mb-2">
                        <p class="text-small mb-0 text-dark">${ message}</p>
                    </div>
                    <p class="small text-muted">${ new Date().toLocaleString()}</p>
                </div>
            </div>
            `
        );
    } else {
        $('#messages-container').append(
            `<div class="media w-50 mb-3">
                    <div class="media-body ml-3">
                        <p class="small text-muted">${user.lastName || ''} ${user.firstName || ''} ${user.middleName || ''}</p>
                        <div class="bg-light rounded py-2 px-3 mb-2">
                            <p class="text-small mb-0 text-muted">${ message}</p>
                        </div>
                        <p class="small text-muted">${ new Date().toLocaleString()}</p>
                    </div>
                </div>`
        );
    }
});

connection.on("UserConnected", function (user) {
    const isMine = UserId == user.id;

    if (isMine) {
        if ($(`[data-user-id].active`).length > 0) {
            $(`[data-user-id].active`).each(function () {
                $(this).remove();
            });
        }
    }
    console.log($(`[data-user-id].active`))

});

connection.on("UserDisconnected", function (user) {
    $('a[data-user-id="' + user.id + '"]').remove();
});

connection.start()
    .then(function () {

    })
    .catch(function (err) {
        return console.error(err.toString());
    });

$('.chat-form').submit(function (e) {
    e.preventDefault();
    const message = $('#message-box').val().trim();

    if (message && message.length > 0) {
        connection.invoke("SendMessage", UserId, message).then(() => $('#message-box').val(''));
    }

});
