$(function () {

    let url = new URL(document.URL);

    let pageNumber = 1;
    let category = url.searchParams.get("category");
    let sort = "";

    $("#filterCollapseToggle").click(() => {
        $("#sort-collapse").collapse("hide");
    })

    $("#sortCollapseToggle").click(() => {
        $("#filter-collapse").collapse("hide");
    })

    subscribe();

    function subscribe() {
        $("#adsForm").submit((event) => {
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
        search = $("#searchInput").val();
        sort = $("#sortType option:checked").val();
        filter = $("#filterForm").serialize();

        $.get(`/Home/AdsPartial?page=${pageNumber}&category=${category}&search=${search}&sort=${sort}&${filter}`, (data) => {
            $("#collection-container").html(data);
            subscribe();
        });
    }
})