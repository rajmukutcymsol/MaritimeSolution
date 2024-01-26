//$("#date_of_request").val((new Date()).toISOString().split('T')[0]);
$(document).ready(function () {
    BindToolName();
    GetRequestersInfo();
});


//$("#date_of_request").val((new Date()).toISOString().split('T')[0]);
var currentDate = new Date();
var day = ('0' + currentDate.getDate()).slice(-2);
var month = ('0' + (currentDate.getMonth() + 1)).slice(-2);
var year = currentDate.getFullYear();
var formattedDate = month + '/' + day + '/' + year;

var formattedTime = currentDate.toLocaleTimeString([], { hour: '2-digit', minute: '2-digit', second: '2-digit' });
var dateTimeString = formattedDate + ' ' + formattedTime;

$("#date_of_request").val(dateTimeString);

function GetRequestersInfo() {
    $("#requester").autocomplete({
        source: function (request, response) {
            $.ajax({
                url: '/IssueRequest/GetUserList/',
                data: JSON.stringify({ prefix: request.term }),
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    response($.map(data, function (item) {
                        return {
                            label: item.requester,
                            value: item.requester// + '(' + item.employee_id + ')'
                        };
                    }));
                },
                error: function (response) {
                    // alert(response.responseText);
                },
                failure: function (response) {
                    // alert(response.responseText);
                }
            });
        },
        select: function (event, ui) {

            event.preventDefault(); // Prevent the default behavior of replacing the input value with the selected value.
            $("#requester").val(ui.item.value);

            // to get the user id
            $.ajax({
                url: '/IssueRequest/GetUserByName/',
                data: JSON.stringify({ employee_name: $("#requester").val() }),
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    var _employeeid = data.employee_id;
                    $("#request_for").val(_employeeid);
                },
                error: function (response) {
                    //alert(response.responseText);
                },
                failure: function (response) {
                    //alert(response.responseText);
                }
            });

        },
        minLength: 1
    });
}
function validateMadatoryFields() {
    var isValid = true;

    if ($("#business_benifit").val() == "") {
        $('#business_benifit').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#business_benifit').css('border-color', 'lightgrey');
    }
    if ($("#request_priority").val() == "00000000-0000-0000-0000-000000000000" || $("#request_priority").val() == "" || $("#request_priority").val() == "--select--") {
        $('#request_priority').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#request_priority').css('border-color', 'lightgrey');
    }
    if ($("#request_title").val() == "") {
        $('#request_title').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#request_title').css('border-color', 'lightgrey');
    }
    if ($("#tool_solution_name").val() == null || $("#tool_solution_name").val() == "0" || $("#tool_solution_name").val() == "" || $("#tool_solution_name").val() == "--select--" || $("#tool_solution_name").val() == "00000000-0000-0000-0000-000000000000") {
        $('#tool_solution_name').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#tool_solution_name').css('border-color', 'lightgrey');
    }
    if ($("#expected_time_effort_hr").val() == "") {
        $('#expected_time_effort_hr').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#expected_time_effort_hr').css('border-color', 'lightgrey');
    }
    if ($("#request_description").val() == "") {
        $('#request_description').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#request_description').css('border-color', 'lightgrey');
    }
    if ($("#use_case_name").val() == null || $("#use_case_name").val() == "" || $("#use_case_name").val() == "0" || $("#use_case_name").val() == "--select--") {
        $('#use_case_name').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#use_case_name').css('border-color', 'lightgrey');
    }
    if ($("#project_name").val() == null || $("#project_name").val() == "" || $("#project_name").val() == "0" || $("#project_name").val() == "--select--" || $("#project_name").val() == "00000000-0000-0000-0000-000000000000") {
        $('#project_name').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#project_name').css('border-color', 'lightgrey');
    }
    if ($("#requester").val() == "") {
        $('#requester').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#requester').css('border-color', 'lightgrey');
    }
    if ($("#cli_ui_name").val() == "") {
        $('#cli_ui_name').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#cli_ui_name').css('border-color', 'lightgrey');
    }
    return isValid;
}
$('body').on('click', '#btnSave', function () {
    //alert($("#date_of_request").val());
    var res = validateMadatoryFields();
    if (res == false) {
        //alert(res);
        return false;
    }
    var projectRequirement = new FormData();
    var files = $("#mop_sop_attachment_path").get(0).files;
    for (var i = 0; i < files.length; i++) {

        projectRequirement.append(files[i].name, files[i]);
    }
    projectRequirement.append("auto_id", $("#auto_id").val());
    projectRequirement.append("requirement_type", $("#requirement_type").val());
    projectRequirement.append("request_title", $("#request_title").val());
    projectRequirement.append("request_description", $("#request_description").val());
    projectRequirement.append("request_notes", $("#request_notes").val());
    projectRequirement.append("request_priority", $("#request_priority").val());
    projectRequirement.append("cost_value", $("#cost_value").val());
    projectRequirement.append("request_status", $("#request_status").val());
    projectRequirement.append("requester", $("#requester").val());
    projectRequirement.append("lm_of_requested", $("#lm_of_requested").val());
    projectRequirement.append("date_of_request", $("#date_of_request").val());
    projectRequirement.append("connectivity_availability", $("#connectivity_availability").val());
    projectRequirement.append("crediential_availability", $("#crediential_availability").val());
    projectRequirement.append("tool_solution_name", $("#tool_solution_name").val());
    projectRequirement.append("use_case_name", $("#use_case_name").val());
    projectRequirement.append("is_resolution_offered", $("#is_resolution_offered").val());
    projectRequirement.append("support_engineer", $("#support_engineer").val());
    //projectRequirement.append("support_manager", $("#support_manager").val());
    projectRequirement.append("expected_time_of_resolution_hour", $("#expected_time_of_resolution_hour").val());
    projectRequirement.append("business_benifit", $("#business_benifit").val());
    projectRequirement.append("cost_value", $("#cost_value").val());
    projectRequirement.append("mop_or_sop", $("#mop_or_sop").val());
    projectRequirement.append("project_name", $("#project_name").val());
    projectRequirement.append("expected_time_of_resolution_hourtext", $("#expected_time_of_resolution_hour").val());
    projectRequirement.append("request_for", $("#request_for").val());
    projectRequirement.append("domain", $("#domain").val());
    projectRequirement.append("cli_ui_name", $("#cli_ui_name").val());


    $.ajax({
        url: '/IssueRequest/Save',
        type: 'POST',
        data: projectRequirement,
        processData: false,  // tell jQuery not to process the data
        contentType: false,  // tell jQuery not to set contentType
        success: function (data) {
            swal(data.Title, data.Message, data.Type).then(function () {
                window.location.href = "/IssueRequest/Index";
            });
        },
        failure: function (errorMessage) {
            swal("Oops!", errorMessage, "failure");
        }
    });
});


function resetControlValue() {
    $('input[type="text"]').val('');
    $('textarea').val('');
    $("select").prop("selectedIndex", 0).val();
}
$("#tool_solution_name").change(function () {
    BindUseCases();
})
$("#project_name").change(function () {
    $("#use_case_name").html('');
    BindToolName();
})
$("#request_priority").change(function () {
    $("#expected_time_of_resolution_hour").html('');
    //alert($("#request_priority").val());
    var request = {
        request_priority: $("#request_priority").val(),
    }
    $.ajax({
        url: "/IssueRequest/GetResolutionHr",
        data: JSON.stringify(request),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (data) {
           // alert(data);
            $("#expected_time_of_resolution_hour").val(data);
        }
    });
    
})
function BindToolName() {
    //alert($("#project_name").val().toUpperCase());
    if ($("#project_name").val() != "") {
        var request = {
            project_name: $("#project_name").val().toUpperCase()
        }
        // console.log(request);
        $.ajax({
            url: "/ChangeRequest/GetToolNameByProjectID",
            data: JSON.stringify(request),
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (data) {
                var op = "";
                op += "<option value='0'>" + '--select--';
                $.each(data, function (i, item) {
                    op += "<option value='" + item.id + "'>" + item.tool_name;
                });

                $("#tool_solution_name").html('');
                $("#tool_solution_name").append(op);
            }
        });
    }

}
function BindUseCases() {
    //alert($("#project_name").val().toUpperCase());
   // alert('ToolID ' + $("#tool_solution_name").val());
    //alert('Project id ' + $("#project_name").val());
    if ($("#tool_solution_name").val() != "") {
        //alert('auto');
        var request = {
            tool_id: $("#tool_solution_name").val(),
            project_id: $("#project_name").val()
        }
        console.log(request);
        $.ajax({
            url: "/ChangeRequest/GetUseCasesByToolNameId",
            data: JSON.stringify(request),
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (data) {
                var op = "";
                op += "<option value=''>--select--</option>";
                $.each(data, function (i, item) {
                    op += "<option value='" + item.id + "'>" + item.use_case_name;
                });

                $("#use_case_name").html('');
                $("#use_case_name").append(op);
            }
        });
    }
}
