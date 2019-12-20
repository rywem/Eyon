var dataTable;

$(document).ready(function () {
    loadDataTable();
});


function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/seller/organization/GetAll",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "name", "width": "15%" },
            { "data": "type", "width": "15%" },            
            {
                "data": "website",
                "render": function (data) {
                    if (data !== null ) {
                        return ` <div class="badge badge-dark text-white">
                                <a href="#"  onClick="window.open('${data}', '_blank')" class="inherit">View Website</a>
                            </div>
                            `;
                    }
                    else 
                        return "<div />"

                }, "width": "15%"
            },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="text-center">
                                <a href="/Seller/Organization/Upsert/${data}" class='btn btn-success text-white' style='cursor:pointer; width:100px'>
                                    <i class='far fa-edit'></i> Edit
                                </a>
                                &nbsp;
                                <a onclick=Delete("/Seller/Organization/Delete/${data}") class='btn btn-danger text-white' style='cursor:pointer; width:100px'>
                                    <i class='far fa-trash-alt'></i> Delete
                                </a>
                                &nbsp;
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

function Delete(url) {
    // Use Sweet Alert to give popup confirming delete.

    swal({
        title: "Are you sure you want to delete?",
        text: "You will not be able to restore the content!",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "Yes, Delete it.",
        closeOnconfirm: true
    }, function () {
        $.ajax({
            type: 'DELETE',
            url: url,
            success: function (data) {
                if (data.success) {
                    toastr.success(data.message);
                    dataTable.ajax.reload();
                }
                else {
                    toastr.error(data.message);
                }
            }
        });
    });
}