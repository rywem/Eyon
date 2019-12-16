$(document).ready(function () {
    var createEnabled = true;
    $("#Community_CountryId").change(function () {
        var countryId = $("#Community_CountryId :selected").val();        
        var hasStates = false;
        // Clear the State picklist, re-add the states if states exist.
        $("#StateId").empty(); 
        $("#StateId").append('<option value="0" selected="selected">None</option>');
        $.ajax({
            async: true,
            url: "/Admin/Community/GetStates?countryId=" + countryId,
            type: "GET",
            success: function (data) {
                var count = Object.keys(data.data).length;                
                if (count > 0) {
                    $("#States").removeClass("hidden");
                    $("#States").addClass("visible");
                    $.each(data.data, function (k, v) {
                        $("#StateId").append('<option value="' + v.id + '">' + v.name + '</option>');
                    });
                    hasStates = true;
                    $(":submit").attr("disabled", true);
                }
                else {
                    $("#States").addClass("hidden");
                    $("#States").removeClass("visible");                                        
                    hasStates = false;
                    $(":submit").attr("disabled", false);
                }
            }
        });        
    });
    $("#StateId").change(function () {
        var selectedValue = $("#StateId").val();
        if (selectedValue == 0) {
            $(":submit").attr("disabled", true);
        }
        else {
            $(":submit").attr("disabled", false);
        }
    });
});