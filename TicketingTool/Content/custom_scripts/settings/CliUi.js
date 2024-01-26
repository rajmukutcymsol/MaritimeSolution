$(document).ready(function () {
    loadData();
})

function loadData() {
    $.get("/CliUi/GetAll", function (data) {
        $('#tbl_cliui').DataTable({
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
                { 'data': 'cli_ui_name' },
                { 'data': 'fulladdress' },
                { 'data': 'phone' },
                { 'data': 'fax' },
                { 'data': 'email' },
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

    var formData = new FormData(); // Create a FormData object to hold the form data
    //var files = $("#upload").get(0).files;
    var files = $("#upload").get(0).files;

    for (var i = 0; i < files.length; i++) {

        formData.append(files[i].name, files[i]);
    }
    formData.append("cli_ui_name", $("#cli_ui_name").val());
    formData.append("is_active", is_active);
    formData.append("fulladdress", $("#fulladdress").val());
    formData.append("fax", $("#fax").val());
    formData.append("email", $("#email").val());
    formData.append("phone", $("#phone").val());


    $.ajax({
        url: '/CliUi/Save',
        data: formData, // Pass the FormData object as data
        type: "POST",
        contentType: false, // Important! Don't set content type
        processData: false, // Important! Don't process data
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

// Update data function
function update() {
    var res = validate();
    if (res == false)
        return false;

    var is_active = false;
    if ($('#is_active').is(':checked'))
        is_active = true;

    var formData = new FormData(); // Create a FormData object to hold the form data
    //var files = $("#upload").get(0).files;
    var files = $("#upload").get(0).files;

    for (var i = 0; i < files.length; i++) {

        formData.append(files[i].name, files[i]);
    }
    formData.append("cli_ui_name", $("#cli_ui_name").val());
    formData.append("is_active", is_active);
    formData.append("fulladdress", $("#fulladdress").val());
    formData.append("fax", $("#fax").val());
    formData.append("email", $("#email").val());
    formData.append("phone", $("#phone").val());
    formData.append("id", $("#id").val());

    //var request = {
    //    cli_ui_name: $("#cli_ui_name").val(),
    //    is_active: is_active,
    //    fulladdress: $("#fulladdress").val(),
    //    fax: $("#fax").val(),
    //    email: $("#email").val(),
    //    logoupload: $("#logoupload").val(),
    //    id: $("#id").val(),
    //    phone: $("#phone").val()

    //};

    $.ajax({
        url: '/CliUi/Update',
        data: formData, // Pass the FormData object as data
        type: "POST",
        contentType: false, // Important! Don't set content type
        processData: false, // Important! Don't process data
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
    console.log($("#id").val($(this).closest('tr').find('td:eq(0) input').val()));
    $.get("/CliUi/GetById", { id: $(this).closest('tr').find('td:eq(0) input').val() }, function (data) {
        $("#cli_ui_name").val(data.cli_ui_name);
        $("#fulladdress").val(data.fulladdress);
        $("#fax").val(data.fax);
        $("#email").val(data.email);
        $("#phone").val(data.phone);
        var imageUrl = "/uploads/uploads/" + data.logoupload;

        // Get the <img> element by its ID and set the src attribute
        $("#logo").attr("src", imageUrl);

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
                $.get("/CliUi/Delete", { id: $(this).closest('tr').find('td:eq(0) input').val() }, function (data) {
                    swal(data.Title, data.Message, data.Type).then(function () {
                        loadData();
                    });
                });
            }
        });
});




function validate() {
    var isValid = true;
    if ($('#cli_ui_name').val().trim() == "") {
        $('#cli_ui_name').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#cli_ui_name').css('border-color', 'lightgrey');
    }
    if ($('#fulladdress').val().trim() == "") {
        $('#fulladdress').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#fulladdress').css('border-color', 'lightgrey');
    }
    if ($('#fax').val().trim() == "") {
        $('#fax').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#fax').css('border-color', 'lightgrey');
    }
    if ($('#email').val().trim() == "") {
        $('#email').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#email').css('border-color', 'lightgrey');
    }
    //if ($('#logoupload').val().trim() == "") {
    //    $('#logoupload').css('border-color', 'Red');
    //    isValid = false;
    //}
    //else {
    //    $('#logoupload').css('border-color', 'lightgrey');
    //}
    if ($('#phone').val().trim() == "") {
        $('#phone').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#phone').css('border-color', 'lightgrey');
    }
    return isValid;
}
