$(function () {

    const createToast = (title, text) => (
        `<div class="toast" role="alert" aria-live="assertive" aria-atomic="true" data-delay="4000">
            <div class="toast-header">
                <strong class="mr-auto">${title}</strong>
                <button type="button" class="ml-2 mb-1 close" data-dismiss="toast" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                </button>
            </div>
            <div class="toast-body">${text}</div>
        </div>`);


    // hubs
    const hubConnection = new signalR.HubConnectionBuilder()
        .withUrl("/notifications")
        .build();

    hubConnection.on("Notify", (title, message) => {
        const htmlStr = createToast(title, message);

        console.log(htmlStr);

        $("body").append(htmlStr);
        $('.toast').toast('show');
    });


    hubConnection.start();
})