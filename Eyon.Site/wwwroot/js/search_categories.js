$(document).ready(function () {
    $("#btnFilter").click(function () {
        var _filter = $("#filter").val();
        var categories = buildCategoriesArray();
        if (_filter != '') {
            var rowCount = $('#bodyResults tr').length;
            if (rowCount > 1) {
                $("#bodyResults tr").remove();
            }
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
                            '<td><input type="checkbox" ' + checkboxChecked + ' id="cbCategory" onclick="updateSelection('+item.id+ ', \'' + item.name +'\')" /></td>' +
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


function updateSelection(id, name) {
    handleValue(id);
    handleName(name);
}

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

function handleName(value) {
    var val = value;
    var categories = buildCategoryNamesArray();    
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

    if (typeof categories !== "undefined" && categories !== null) {
        var newNames = "";
        for (var i = 0; i < categories.length; i++) {
            if (i == 0)
                newNames += "#" + categories[i];
            else
                newNames += " #" + categories[i];
        }
        document.getElementById("categoryNames").innerHTML = newNames;
    }
}

function buildCategoryNamesArray() {
    var categoryNames = [];
    var currentCats = document.getElementById("categoryNames").innerHTML;

    if (currentCats !== "undefined" && currentCats !== '') {
        var currentCats = (currentCats[0] == ' ') ? currentCats.substr(1) : currentCats;
        var currentCats = (currentCats[0] == '#') ? currentCats.substr(1) : currentCats;
        currentCats = currentCats.replace(/ #/g, ',');
        var tmpCats = currentCats.split(',');
        for (var i = 0; i < tmpCats.length; i++) {
            var newVal = tmpCats[i];
            //newVal = newVal[0] == ' ' ? newVal.substr(1) : newVal;
            if (typeof categoryNames !== "undefined" && categoryNames !== null) {
                if (categoryNames.includes(newVal) == false)
                    categoryNames.push(newVal);
            }
            else
                categoryNames = [newVal];
        }
    }
    return categoryNames;
}