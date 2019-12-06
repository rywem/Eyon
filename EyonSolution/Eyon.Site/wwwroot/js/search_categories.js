$(document).ready(function () {
   
    $("#btnFilter").click(function () {
        var _filter = $("#filter").val();

        if (_filter != '') {
            $.ajax({
                async: true,
                url: '/Admin/Category/SearchCategories?filter=' + _filter,
                type: 'GET',                
                success: function (data) {
                    $.each(data.categories, function (index, item) {
                        $("#bodyResults").append('<tr>' +
                            '<th scope="row">' + item.id + '</th>' +
                            '<td><input type="checkbox" /></td>' +
                            '<td>' + item.name + '</td>' +
                            '<td>' + item.displayOrder + '</td>' +
                            '<td><img src="' + item.image + '" title="' + item.imageTitle+'" alt="' + item.imageAlt + '" class="inherit" style="border-radius:5px; border:1px solid #bbb9b9" width="100%" /></td>' +                            
                            '</tr>');
                    });
                },
                error: function (xhr, ajaxOptions, thrownError) { alert(xhr.status); alert(thrownError); }

            });
            //https://stackoverflow.com/questions/27267942/submitting-partial-view-data-from-parent-view
        }
    });
});