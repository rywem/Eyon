$(document).ready(function () {
    $("#btnFilter").click(function () {
        var _filter = $("#filter").val();
        var categories = buildCategoriesArray();
        if (_filter != '') {
            $.ajax({
                async: true,
                url: '/Admin/Category/SearchCategories?filter=' + _filter,
                type: 'GET',
                success: function (data) {
                    $.each(data.categories, function (index, item) {
                        var checkboxChecked = "";
                        if (typeof categories !== "undefined" && categories !== null) {
                            if (categories.includes(item.id))
                                checkboxChecked = 'checked="checked"';
                        }
                        $("#bodyResults").append('<tr>' +
                            '<th scope="row">' + item.id + '</th>' +
                            '<td><input type="checkbox" ' + checkboxChecked + ' id="cbCategory" onclick="handleValue(' + item.id + ')" /></td>' +
                            '<td>' + item.name + '</td>' +
                            '<td>' + item.displayOrder + '</td>' +
                            '<td><img src="' + item.image + '" title="' + item.imageTitle + '" alt="' + item.imageAlt + '" class="inherit" style="border-radius:5px; border:1px solid #bbb9b9" width="100%" /></td>' +
                            '</tr>');
                    });
                },
                error: function (xhr, ajaxOptions, thrownError) { alert(xhr.status); alert(thrownError); }

            });
            //https://stackoverflow.com/questions/27267942/submitting-partial-view-data-from-parent-view
        }
    });
    ////$("input[type=checkbox]").change(function () {


});

//https://stackoverflow.com/questions/1328723/how-to-generate-a-simple-popup-using-jquery
function deselect(e) {
    $('.pop').slideFadeToggle(function () {
        e.removeClass('selected');
    });
}

$(function () {
    $('#addCategories').on('click', function () {
        if ($(this).hasClass('selected')) {
            deselect($(this));
        } else {
            $(this).addClass('selected');
            $('.pop').slideFadeToggle();
        }
        return false;
    });

    $('.close').on('click', function () {
        deselect($('#addCategories'));
        return false;
    });
});

$.fn.slideFadeToggle = function (easing, callback) {
    return this.animate({ opacity: 'toggle', height: 'toggle' }, 'fast', easing, callback);
};

function handleValue(value) {
    var val = value;


    var categories = buildCategoriesArray();

    if (typeof categories !== "undefined" && categories !== null) {
        if (categories.includes(val)) {
            var categories = categories.filter(function (value, index, arr) {
                return value != val;
            });
        }
        else {
            categories.push(val);
        }
    }
    else 
        categories = [val];
    
    document.getElementById("CategoryIds").value = categories.toString();
}

function buildCategoriesArray() {
    var categories = [];
    var currentCats = document.getElementById("CategoryIds").value;
    if (currentCats !== "undefined" && currentCats !== '') {
        var tmpCats = currentCats.split(',');
        for (var i = 0; i < tmpCats.length; i++) {
            var newVal = parseInt(tmpCats[i]);
            if (typeof categories !== "undefined" && categories !== null) {
                if(categories.includes(newVal) == false)
                    categories.push(newVal);
            }
            else
                categories = [newVal];
        }
    }
    return categories;
}