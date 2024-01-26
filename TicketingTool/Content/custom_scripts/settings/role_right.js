$(document).ready(function () {
    loadData();
})
function loadData() {
    $.get("/Roles/GetRoles", function (data) {
        $('#tblRoles').DataTable({
            "processing": true, // for show progress bar
            "serverSide": false, // for process server side
            "filter": true, // this is for disable filter (search box)
            "orderMulti": false, // for disable multiple column at once
            "searching": true,
            "destroy": true,
            "data": data,
            "columns": [
                {
                    "data": "id",
                    "orderable": false,
                    "render": function (data, type, row, meta) {
                        return '<input type="hidden" value=' + data + '> <a href="#" class="btnEdit" style="margin-right: 5px;"><i class="fa fa-edit"></i></a>';
                    },
                },
                { 'data': 'role_name' },
                {
                    data: "is_active",
                    "searchable": false,
                    "orderable": false,
                    "render": function (data, type, row) {
                        if (data == false)
                            return '<span class="label label-danger">In-Active</span>';
                        else
                            return '<span class="label label-success">Active</span>';
                    }

                }


            ],
            "aoColumnDefs": [{
                'bSortable': false,
                'aTargets': [0],
            },
            {
                "targets": 2,
                "className": "text-center",
            }],

        });
    })

}


$('body').on('click', '#btnAdd', function () {
    $('#modal-default').modal('show');
})


$("#btnSave").click(function () {
    //if ($("#btnSave").text().trim() == "Save")
    //    save();
    if ($("#btnSave").text().trim() == "Update")
        update();
})



function save() {

    var res = validate();
    if (res == false)
        return false;

    var is_active = false;
    if ($('#is_active').is(':checked'))
        is_active = true;

    var request = {
        RoleID: $("#role_name").val(),
        is_active: is_active
    }

    $.ajax({
        url: '/RoleRight/Save',
        data: JSON.stringify(request),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (data) {
            swal(data.Title, data.Message, data.Type).then(function () {
                loadData();
                $("#modal-default").modal('hide');
            });
        },
        failure: function (errorMessage) {
            swal("Oops!", errorMessage, "failure");
        }
    });
}


function update() {
    var res = validate();
    if (res == false)
        return false;
    var is_active = false;

    var IsView = false;
    var IsView = $("#IsView").prop("checked");
    if (IsView) {
        IsView = true;
    } else {
        IsView = false;
    }

    var is_update = false;
    var is_update = $("#IsUpdate").prop("checked");
    if (is_update) {
        IsUpdate = true;

    } else {
        IsUpdate = false;
    }

    var IsDelete = false;
    var IsDelete = $("#IsDelete").prop("checked");
    if (IsDelete) {
        IsDelete = true;

    } else {
        IsDelete = false;
    }

    var IsPrint = false;
    var IsPrint = $("#IsPrint").prop("checked");
    if (IsPrint) {
        IsPrint = true;

    } else {
        IsPrint = false;
    }

    var IsDownload = false;
    var IsDownload = $("#IsPrint").prop("checked");
    if (IsDownload) {
        IsDownload = true;

    } else {
        IsDownload = false;
    }

    var IsVerify = false;
    var IsVerify = $("#IsVerify").prop("checked");
    if (IsVerify) {
        IsVerify = true;

    } else {
        IsVerify = false;
    }
    
    if ($('#is_active').is(':checked'))
        is_active = true;

    var request = {
        RoleID: $("#id").val(),
        CanUpdate: is_update,
        CanDelete: IsDelete,
        CanPrint: IsPrint,
        IsDownload: IsDownload,
        CanVerify: IsVerify,
        CanView: IsView,
        is_active: is_active
    }
    console.log(request);
    $.ajax({
        url: '/RoleRight/Update',
        data: JSON.stringify(request),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (data) {

            swal(data.Title, data.Message, data.Type).then(function () {
                loadData();
                $("#btnSave").text('Save');
                $("#modal-default").modal('hide');
            });
        },
        failure: function (errorMessage) {
            swal("Oops!", errorMessage, "failure");
        }
    });
}



$('body').on('click', '.btnEdit', function () {
    $("#id").val($(this).closest('tr').find('td:eq(0) input').val());
    $.get("/Roles/GetRoleById", { id: $(this).closest('tr').find('td:eq(0) input').val() }, function (data) {
        $("#role_name").val(data.role_name);
        if (data.is_active == true)
            $('#is_active').attr("checked", true);
        else
            $('#is_active').attr("checked", false);

        $("#btnSave").text('Update');
        $("#modal-default").modal('show');
    });
    $.get("/RoleRight/GetRoleById", { id: $(this).closest('tr').find('td:eq(0) input').val() }, function (data) {
        // Uncheck all checkboxes
        $('#IsView').prop("checked", false);
        $('#IsUpdate').prop("checked", false);
        $('#IsDelete').prop("checked", false);
        $('#IsPrint').prop("checked", false);
        $('#IsDownload').prop("checked", false);
        $('#IsVerify').prop("checked", false);

        // Set checked state based on data
        $('#IsView').prop("checked", data.CanView);
        $('#IsUpdate').prop("checked", data.CanUpdate);
        $('#IsDelete').prop("checked", data.CanDelete);
        $('#IsPrint').prop("checked", data.CanPrint);
        $('#IsDownload').prop("checked", data.IsDownload);
        $('#IsVerify').prop("checked", data.CanVerify);

        $("#btnSave").text('Update');
        $("#modal-default").modal('show');
    });
});



$('body').on('click', '.btnDelete', function () {
    swal({
        title: "Are you sure?",
        text: "Once deleted, you will not be able to recover this role",
        icon: "warning",
        buttons: true,
        dangerMode: true,
    })
        .then((willDelete) => {
            if (willDelete) {
                $.get("/Roles/DeleteRole", { id: $(this).closest('tr').find('td:eq(0) input').val() }, function (data) {
                    swal(data.Title, data.Message, data.Type).then(function () {
                        loadData();
                    });
                });
            }
        });
});




function validate() {
    var isValid = true;
    if ($('#role_name').val().trim() == "") {
        $('#role_name').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#role_name').css('border-color', 'lightgrey');
    }
    return isValid;
}

