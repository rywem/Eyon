var dataTable;

$(document).ready(function () {
    //loadDataTable();
});


function loadDataTable(searchString) {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/user/community/searchCommunities?searchString=" + searchString,
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "communityName", "width": "15%" },
            { "data": "stateName", "width": "15%" },
            { "data": "countryName", "width": "15%" },            
            {
                "data": "communityId",
                "render": function (data) {
                    return `<div class="text-center">
                                <input type="button" class='badge badge-secondary text-white' style='cursor:pointer; width:100px' onclick="chooseCommunity(${data})">
                                    <i class="fas fa-hand-pointer"></i> Select Community
                                </input>                                
                            </div>
                            `;
                }, "width": "30%"
            }
        ],
        "language": {
            "emptyTable": "No records found"
        },
        "width": "100%"
    });
}

function chooseCommunity(communityId) {
    alert('not implemented');
}