
$(document).ready(function () {
   // $("#btnRegister").attr("disabled", true);
})



$('body').on('click', '#btnRegister', function () {


    var res = validateRegistration();
    if (res == false) {
        return false;
    }

    var employee = {
        employee_id: $("#employee_id").val(),
        password: $("#password").val(),
        user_role: $("#user_role").val(),
        email_address: $("#email_address").val(),
        mobile: $("#mobile").val(),
        location: $("#location").val(),
        display_name: $("#display_name").val(),
        display_name: $("#display_name").val(),
        //manager_username: $("#manager_username").val(),
        //project_name: $("#project_name").val()
    }
    $.ajax({
        url: '/UserAuthentication/Register',
        data: JSON.stringify(employee),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (data) {
            swal(data.Title, data.Message, data.Type);
        },
        failure: function (errorMessage) {
            swal("Oops!", errorMessage, "failure");
        }
    });


})

$('body').on('click', '#btnvarify', function () {

    var res = validateVarify();
    if (res == false) {
        return false;
    }

    var request = {
        UsrEmplyoeeCode: $("#employee_id").val(),
        UsrPassword: $("#password").val(),
        UsrRole: $("#user_role").val()
    }


    $.ajax({
        url: '/UserAuthentication/VarifyUser',
        data: JSON.stringify(request),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.Result == 1) {
                $("#display_name").val(data.Name);
                $("#email_address").val(data.EmailId);
                $("#department").val(data.Department);
                $("#manager_name").val(data.ManagerName);
                $("#btnRegister").attr("disabled", false);
                $("#btnvarify").attr("disabled", true);
            }
            else if (data.Result == 0) {
                swal({
                    title: "Oops!",
                    text: data.Response,
                    icon: "warning",
                    button: "Ok",
                });
            }
        },
        failure: function (errorMessage) {
            swal("Oops!", errorMessage, "failure");
        }
    });

    
})


function validateVarify() {
    var isValid = true;
    if ($('#employee_id').val().trim() == "") {
        $('#employee_id').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#employee_id').css('border-color', 'lightgrey');
    }
    if ($('#user_role').val().trim() == "") {
        $('#user_role').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#user_role').css('border-color', 'lightgrey');
    }

    if ($('#password').val().trim() == "") {
        $('#password').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#password').css('border-color', 'lightgrey');
    }
    return isValid;
}

function validateRegistration() {
    var isValid = true;
    if ($('#employee_id').val().trim() == "") {
        $('#employee_id').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#employee_id').css('border-color', 'lightgrey');
    }
    if ($('#user_role').val().trim() == "") {
        $('#user_role').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#user_role').css('border-color', 'lightgrey');
    }
    if ($('#mobile').val().trim() == "") {
        $('#mobile').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#mobile').css('border-color', 'lightgrey');
    }
    if ($('#location').val().trim() == "") {
        $('#location').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#location').css('border-color', 'lightgrey');
    }

    if ($('#password').val().trim() == "") {
        $('#password').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#password').css('border-color', 'lightgrey');
    }
    //if ($("#chkProject").prop('checked') == true) {
    //    //do something
    //}
    //else {
    //    swal("Oops!", "Select Projects", "failure");
    //    isValid = false;
    //}

    return isValid;
}


$('body').on('click', 'input', function () {
    $(this).css('border-color', 'lightgrey');
});

$('body').on('click', 'select', function () {
    $(this).css('border-color', 'lightgrey');
});