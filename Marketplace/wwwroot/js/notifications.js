$(function () {
    let pageNumber = 1;
    let sort = "";

    subscribe();

    function subscribe() {
        $("#sortForm").submit((event) => {
            event.preventDefault();
            getNotification();
        });

        $("#previousPageBtn").click(() => {

            if (pageNumber != 1) {
                pageNumber--;
                getNotification();
            }
        });

        $("#nextPageBtn").click(() => {

            if (pageNumber != Number($("#pagesAmount").text())) {
                pageNumber++;
                getNotification();
            }
        });

        $('[data-toggle="popover"]').popover();
    }

    function getNotification() {
        sort = $("#sortType option:checked").val();

        $.get(`/Account/NotificationsPartial?sort=${sort}&page=${pageNumber}`, (data) => {
            $("#collection-container").html(data);
            subscribe();
        });
    }
});