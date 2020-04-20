$(function () {
    $('.radio-category').change((event) => {
        let category = $('.radio-category:checked').val();
        $('#CreateAdForm').load('/Home/CreatesPartial?category=' + category);
        $('#createHeader').text('Create ad');
    })
});