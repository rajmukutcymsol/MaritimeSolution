$(document).ready(function () {
    loadData();
})

function loadData() {
    $.get("/Status/GetAll", function (data) {
        $('#tblStatus').DataTable({
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
                        return '<input type="hidden" value=' + data + '> <a href="#" class="btnEdit" style="margin-right: 5px;"><i class="fa fa-edit"></i></a> | &nbsp <a href="#" class="btnDelete" style="margin-right: 5px;"><i class="fa fa-trash"></i></a>';
                    },
                },
                { 'data': 'status_name' },
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
    if ($("#btnSave").text().trim() == "Save")
        save();
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
        status_name: $("#status_name").val(),
        is_active: is_active
    }

    $.ajax({
        url: '/Status/Save',
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
    if ($('#is_active').is(':checked'))
        is_active = true;

    var request = {
        status_name: $("#status_name").val(),
        is_active: is_active,
        id: $("#id").val()
    }

    $.ajax({
        url: '/Status/Update',
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
    $.get("/Status/GetById", { id: $(this).closest('tr').find('td:eq(0) input').val() }, function (data) {
        $("#status_name").val(data.status_name);
        if (data.is_active == true)
            $('#is_active').attr("checked", true);
        else
            $('#is_active').attr("checked", false);

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
                $.get("/Status/Delete", { id: $(this).closest('tr').find('td:eq(0) input').val() }, function (data) {
                    swal(data.Title, data.Message, data.Type).then(function () {
                        loadData();
                    });
                });
            }
        });
});




function validate() {
    var isValid = true;
    if ($('#status_name').val().trim() == "") {
        $('#status_name').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#status_name').css('border-color', 'lightgrey');
    }
    return isValid;
}
