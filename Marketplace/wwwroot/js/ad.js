$(function () {

    const url = new URL(document.URL);
    const adId = url.searchParams.get("id");
    const category = url.searchParams.get("category");

    const params = {
        adId: adId,
        category: category
    };

    $('#buy-btn').click(() => {
        $.post('/Home/Buy', params, (data) => {
            $('#buyModal').modal('show');
        });
    })

    $('#track-btn').click((event) => {
        $.post('/Home/SubscribeOnPriceToggle', params, data => {
            $('#track-btn').toggleClass("tracked");
        });
    })

    $('#freeze-trigger').click(() => {
        $.post('/Home/FreezeAdToggle', params, data => {
            if ($('#freeze-trigger').text() == "Freeze")
                $('#freeze-trigger').text("Defreeze");
            else
                $('#freeze-trigger').text("Freeze");
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