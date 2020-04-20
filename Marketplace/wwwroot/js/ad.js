$(function () {

    const url = new URL(document.URL);

    $('#buy-btn').click(() => {
        $.post('/Home/Buy', { adId: url.searchParams.get("id"), category: url.searchParams.get("category") }, (data) => {
            $('#buyModal').modal('show');
        });
    })

    $('#track-btn').click((event) => {
        $.post('/Home/SubscribeOnPriceToggle', { id: url.searchParams.get("id"), category: url.searchParams.get("category") }, data => {
            $('#track-btn').toggleClass("tracked");
        });
    })

    $('#freeze-trigger').click(() => {
        $.post('/Home/FreezeAdToggle', { adId: url.searchParams.get("id"), category: url.searchParams.get("category") }, data => {
            if ($('#freeze-trigger').text() == "Freeze") {
                $('#freeze-trigger').text("Defreeze");
            }
            else {
                $('#freeze-trigger').text("Freeze");
            }
        });
    })

    $('#reportForm').submit((event) => {
        event.preventDefault();

        $.post('/Home/ReportAd', Object.fromEntries(new FormData(event.target)), (data) => {
            $("#reportModalLabel").text(data);
            $("#reportModal .modal-body").html('<p></p>');
        });
    })
});