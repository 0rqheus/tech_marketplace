$(function () {

    let url = new URL(document.URL);

    let pageNumber = 1;
    let category = url.searchParams.get("category");
    let sort = "";

    $("#filter-collapse-toggle").click(() => {
        $("#sort-collapse").collapse("hide");
    })

    $("#sort-collapse-toggle").click(() => {
        $("#filter-collapse").collapse("hide");
    })

    subscribe();

    function subscribe() {
        $("#sortForm").submit((event) => {
            event.preventDefault();
            getAds();
        });

        $("#filterForm").submit((event) => {
            event.preventDefault();
            getAds();
        });

        $("#previousPageBtn").click(() => {

            if (pageNumber != 1) {
                pageNumber--;
                getAds();
            }
        });

        $("#nextPageBtn").click(() => {

            if (pageNumber != Number($("#pagesAmount").text())) {
                pageNumber++;
                getAds();
            }
        });
    }

    function getAds() {
        sort = $("#sortType option:checked").val();

        $.get(`/Home/AdsPartial?page=${pageNumber}&category=${category}&sort=${sort}&${$("#filterForm").serialize()}`, (data) => {
            $("#collection-container").html(data);
            subscribe();
        });
    }
})