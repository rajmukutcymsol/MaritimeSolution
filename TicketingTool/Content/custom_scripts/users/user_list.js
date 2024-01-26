$(document).ready(function () {
    //$('input[type="text"]').attr("readonly", true);
    //$('textarea').attr("readonly", true);
    LoadData();
});

function LoadData() {
    $.get("/Users/GetUserList", function (data) {
        var sessionValue = $('#sessionValue').val();
        if (sessionValue == "admin") {
            $('#tblNewRequest').DataTable({
                "processing": true, // for show progress bar
                "serverSide": false, // for process server side
                "filter": true, // this is for disable filter (search box)
                "orderMulti": false, // for disable multiple column at once
                "searching": true,
                "destroy": true,
                "data": data,
                "columns": [
                    {
                        "data": "employee_id",
                        "orderable": false,
                        "render": function (data, type, row, meta) {
                            return '<input type="hidden" value=' + data + '> <a href="#" onclick="return edit(' + data + ');" style="margin-right: 5px;"><i class="fa fa-edit"></i></a>| &nbsp <a href="#" class="btnDelete" style="margin-right: 5px;"><i class="fa fa-trash"></i></a>';
                        },
                    },
                    { 'data': 'employee_id' },
                    { 'data': 'employee_name' },
                    { 'data': 'user_role' },
                    { 'data': 'access_role' },
                    { 'data': 'email_address' },
                    { 'data': 'location' },
                  /*  { 'data': 'manager_name' },*/
                    {
                        data: "state",
                        "searchable": false,
                        "orderable": false,
                        "render": function (data, type, row) {
                            if (data == 0)
                                return '<span class="label label-danger">In-Active</span>';
                            else
                                return '<span class="label label-success">Active</span>';
                        }

                    }


                ],
                "aoColumnDefs": [{
                    'bSortable': false,
                    'aTargets': [0],
                    "className": "text-center",
                },
                {
                    "targets": 7,
                    "className": "text-center",
                }],

            });
        }
        else {
            $('#tblNewRequest').DataTable({
                "processing": true, // for show progress bar
                "serverSide": false, // for process server side
                "filter": true, // this is for disable filter (search box)
                "orderMulti": false, // for disable multiple column at once
                "searching": true,
                "destroy": true,
                "data": data,
                "columns": [
                    {
                        "data": "employee_id",
                        "orderable": false,
                        "render": function (data, type, row, meta) {
                            return '<a href="#" onclick="return edit(' + data + ');" style="margin-right: 5px;"><i class="fa fa-edit"></i></a>';
                        },
                    },
                    { 'data': 'employee_id' },
                    { 'data': 'employee_name' },
                    { 'data': 'user_role' },
                    { 'data': 'access_role' },
                    { 'data': 'email_address' },
                    { 'data': 'location' },
                   /* { 'data': 'manager_name' },*/
                    {
                        data: "state",
                        "searchable": false,
                        "orderable": false,
                        "render": function (data, type, row) {
                            if (data == 0)
                                return '<span class="label label-danger">In-Active</span>';
                            else
                                return '<span class="label label-success">Active</span>';
                        }

                    }


                ],
                "aoColumnDefs": [{
                    'bSortable': false,
                    'aTargets': [0],
                    "className": "text-center",
                },
                {
                    "targets": 7,
                    "className": "text-center",
                }],

            });
        }
    })
}


function edit(id) {
    $.get("/Users/GetUserById", { employeeId: id}, function (data) {
        $("#employee_id").val(data.employee_id);
        $("#employee_name").val(data.employee_name);
        $("#user_role").val(data.user_role);
        $("#display_name").val(data.display_name);
        $("#email_id").val(data.email_address);
        $("#username").val(data.username);
        $("#location").val(data.location);
        $("#password").val(data.password);
        $("#username").val(data.employee_id);
        $("#email_id").val(data.email_address);
        //$("#manager_employee_id").val(data.manager_employee_id);
        //$("#manager_username").val(data.manager_username);
        //$("#manager_name").val(data.manager_name);
        //$("#cost_center").val(data.cost_center);
        //$("#department").val(data.department);
        if (data.state == 1)
            $('#state').attr("checked", true);
        else
            $('#state').attr("checked", false);
        if (data.access_role_id != null && data.access_role_id != '')
            $("#access_role_id").val(data.access_role_id);

    });
   
    $("#modal-default").modal('show');
}


$('body').on('click', '#btnSave', function () {
    var state = 0;
    if ($('#state').is(':checked'))
        state = 1;

    var res = validate();
    if (res == false) {
        return false;
    }

    var request = {
        access_role_id: $("#access_role_id").val(),
        state: state,
        employee_id: $("#employee_id").val(),
        password: $("#password").val(),
        user_role: $("#user_role").val(),
        email_address: $("#email_id").val(),
        //mobile: $("#mobile").val(),
        location: $("#location").val(),
        display_name: $("#display_name").val(),
        employee_name: $("#employee_name").val()

    }

    $.ajax({
        url: '/Users/UpdateUser',
        data: JSON.stringify(request),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (data) {
            swal(data.Title, data.Message, data.Type).then(function () {
                LoadData();
                $("#modal-default").modal('hide');
            });
        },
        failure: function (errorMessage) {
            swal("Oops!", errorMessage, "failure");
        }
    });
})


function validate() {
    var isValid = true;
    if ($('#access_role_id').val().trim() == "") {
        $('#access_role_id').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#access_role_id').css('border-color', 'lightgrey');
    }
    if ($('#password').val().trim() == "") {
        $('#password').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#password').css('border-color', 'lightgrey');
    }
    if ($('#employee_name').val().trim() == "") {
        $('#employee_name').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#employee_name').css('border-color', 'lightgrey');
    }
    if ($('#user_role').val().trim() == "") {
        $('#user_role').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#user_role').css('border-color', 'lightgrey');
    }
    if ($('#display_name').val().trim() == "") {
        $('#display_name').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#display_name').css('border-color', 'lightgrey');
    }
    if ($('#email_id').val().trim() == "") {
        $('#email_id').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#email_id').css('border-color', 'lightgrey');
    }
    if ($('#password').val().trim() == "") {
        $('#password').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#password').css('border-color', 'lightgrey');
    }
    if ($('#location').val().trim() == "") {
        $('#location').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#location').css('border-color', 'lightgrey');
    }
    return isValid;
}

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
                $.get("/Users/Delete", { employee_id: $(this).closest('tr').find('td:eq(0) input').val() }, function (data) {
                    swal(data.Title, data.Message, data.Type).then(function () {
                        LoadData();
                    });
                });
            }
        });
});

$('body').on('click', 'select', function () {
    $(this).css('border-color', 'lightgrey');
});