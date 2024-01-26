////const { window } = require("../../../bower_components/inputmask/dist/inputmask/global/window");

$(document).ready(function () {
    BindProject();
    BindCustomers();
    BindRegion();
    if ($("#efficiency").val() == NaN || $("#efficiency").val() == '' || $("#efficiency").val() == null)
        $("#efficiency_value_section").hide();

    GetRequestersInfo();
});
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
                url: '/NewRequest/GetUserByName/',
                data: JSON.stringify({ employee_name: $("#requester").val() }),
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    var _employeeid = data.employee_id;
                    //alert(_employeeid);
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
function BindCustomers() {
    //alert($("#project_name").val().toUpperCase());
    if ($("#project_name").val() != "") {
       // alert('auto');
        var request = {
            project_name: $("#project_name").val().toUpperCase()
        }
        $.ajax({
            url: "/NewRequest/ProjectCustomers",
            data: JSON.stringify(request),
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (data) {
                var op = "";
                $.each(data, function (i, item) {
                    op += "<option value='" + item.id + "'>" + item.customer_name;
                });
                $("#customer").html('');
                $("#customer").append(op);
            }
        });
    }
}
function BindProject() {
    var request = {
        project_name: $("#project_name").val().toUpperCase()
    }
    $.ajax({
        url: "/NewRequest/ProjectCustomers",
        data: JSON.stringify(request),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (data) {
            var op = "";
            $.each(data, function (i, item) {
                op += "<option value='" + item.id + "'>" + item.customer_name;
            });

            $("#customer").html('');
            $("#customer").append(op);
            BindVendor();

        }
    });
}
$("#project_name").change(function () {
    BindProject();
    $("#node_type").html('');
    $("#technology").html('');
    BindRegion();
 })
$("#vendor").change(function () {
    BindTechnology();
})
$("#technology").change(function () {
    BindNodeType();
})

function BindVendor() {
    var request = {
        project_name: $("#project_name").val().toUpperCase()
    }
    $.ajax({
        url: "/NewRequest/ProjectVendors",
        data: JSON.stringify(request),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (data) {
            var op = "";
            op += "<option value='0'>--Select--";
            $.each(data, function (i, item) {
                op += "<option value='" + item.vendor + "'>" + item.vendor_name;
            });

            $("#vendor").html('');
            $("#vendor").append(op);
            BindTechnology();

        }
    });
}
function BindTechnology() {
    var request = {
        project_name: $("#project_name").val(),
        vendor: $("#vendor").val()
    }
    $.ajax({
        url: "/NewRequest/ProjectTechnology",
        data: JSON.stringify(request),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (data) {
            var op = "";
            op += "<option value='0'>--Select--";
            $.each(data, function (i, item) {
                op += "<option value='" + item.technology + "'>" + item.technology_name;
            });

            $("#technology").html('');
            $("#technology").append(op);

        }
    });
}
function BindNodeType() {
    var request = {
        project_name: $("#project_name").val(),
        vendor: $("#vendor").val(),
        technology: $("#technology").val()
    }
    $.ajax({
        url: "/NewRequest/ProjectNodeType",
        data: JSON.stringify(request),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (data) {
            var op = "";
            op += "<option value='0'>--Select--";
            $.each(data, function (i, item) {
                op += "<option value='" + item.node_type + "'>" + item.node_type_name;
            });

            $("#node_type").html('');
            $("#node_type").append(op);

        }
    });
}
$("#efficiency").change(function () {
    if ($("#efficiency").val() == NaN || $("#efficiency").val() == '' || $("#efficiency").val() == null)
        $("#efficiency_value_section").attr("css", "display:none");
    else {
        $("#efficiency_value").attr("placeholder","Enter "+ $("#efficiency option:selected").text());
        $("#efficiency_label").text($("#efficiency option:selected").text());
        $("#efficiency_value_section").show();
    }
})
var currentDate = new Date();
var day = ('0' + currentDate.getDate()).slice(-2);
var month = ('0' + (currentDate.getMonth() + 1)).slice(-2);
var year = currentDate.getFullYear();
var formattedDate = month + '/' + day + '/' + year;

var formattedTime = currentDate.toLocaleTimeString([], { hour: '2-digit', minute: '2-digit', second: '2-digit' });
var dateTimeString = formattedDate + ' ' + formattedTime;

$("#date_of_request").val(dateTimeString);

//$("#date_of_request").val((new Date()).toISOString().split('T')[0]);
$('body').on('click', '#btnSave', function () {
    var res = validateMadatoryFields();
    if (res == false) {
        return false;
    }

    var projectRequirement = new FormData();
    var files = $("#mop_sop_attachment_path").get(0).files;
    for (var i = 0; i < files.length; i++) {
      
        projectRequirement.append(files[i].name, files[i]);
    }
    projectRequirement.append("auto_id", $("#auto_id").val());
    projectRequirement.append("requirement_type", $("#requirement_type").val());
    projectRequirement.append("project_name", $("#project_name").val());
    projectRequirement.append("customer", $("#customer").val());
    projectRequirement.append("request_title", $("#request_title").val());
    projectRequirement.append("request_description", $("#request_description").val());
    projectRequirement.append("request_notes", $("#request_notes").val());
    projectRequirement.append("domain", $("#domain").val());
    projectRequirement.append("vendor", $("#vendor").val());
    projectRequirement.append("request_priority", $("#request_priority").val());
    projectRequirement.append("node_type", $("#node_type").val());
    projectRequirement.append("requester", $("#requester").val());
    projectRequirement.append("lm_of_requested", $("#lm_of_requested").val());
    projectRequirement.append("date_of_request", $("#date_of_request").val());
    projectRequirement.append("mop_or_sop", $("#mop_or_sop").val());
    projectRequirement.append("connectivity_availability", $("#connectivity_availability").val());
    projectRequirement.append("crediential_availability", $("#crediential_availability").val());
    projectRequirement.append("attachment_for_mop_or_sop", $("#attachment_for_mop_or_sop").val());
    projectRequirement.append("function_id", $("#function_id").val());
    projectRequirement.append("function_level", $("#function_level").val());
    projectRequirement.append("region", $("#region").val());
    projectRequirement.append("expected_time_effort_hr", $("#expected_time_effort_hr").val());
    projectRequirement.append("efficiency", $("#efficiency").val());
    projectRequirement.append("efficiency_value", $("#efficiency_value").val());
    projectRequirement.append("is_resolution_offered", $("#is_resolution_offered").val());
    projectRequirement.append("technology", $("#technology").val());
    projectRequirement.append("request_for", $("#request_for").val());
    projectRequirement.append("cli_ui_name", $("#cli_ui_name").val());

    $.ajax({
        url: '/NewRequest/Save',
        type: 'POST',
        data: projectRequirement,
        processData: false,  // tell jQuery not to process the data
        contentType: false,  // tell jQuery not to set contentType
        success: function (data) {
            swal(data.Title, data.Message, data.Type).then(function () {
                //window.location.reload();
                window.location.href = "/NewRequest/Index";
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

function parseJsonDate(jsonDateString) {
    return new Date(parseInt(jsonDateString.replace('/Date(', ''))).toLocaleDateString();
}

function validateMadatoryFields() {
    var isValid = true;
    if ($('#auto_id').val().trim() == "") {
        $('#auto_id').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#auto_id').css('border-color', 'lightgrey');
    }
    if ($('#date_of_request').val().trim() == "") {
        $('#date_of_request').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#date_of_request').css('border-color', 'lightgrey');
    }
    if ($('#request_priority').val().trim() == "") {
        $('#request_priority').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#request_priority').css('border-color', 'lightgrey');
    }
    if ($('#request_title').val().trim() == "") {
        $('#request_title').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#request_title').css('border-color', 'lightgrey');
    }
    
    if ($('#vendor').val() == "0" || $('#vendor').val() == null || $('#vendor').val() == "") {
        $('#vendor').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#vendor').css('border-color', 'lightgrey');
    }
    if ($('#node_type').val() =="0"||$('#node_type').val()==null||$('#node_type').val() == "") {
        $('#node_type').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#node_type').css('border-color', 'lightgrey');
    }

    if ($('#request_priority').val() == "" || $('#request_priority').val() == null || $('#request_priority').val() == "00000000-0000-0000-0000-000000000000") {
        $('#request_priority').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#request_priority').css('border-color', 'lightgrey');
    }

    if ($('#technology').val() == ""||$('#technology').val() == null || $('#technology').val()=="0") {
        $('#technology').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#technology').css('border-color', 'lightgrey');
    }

    if ($('#function_id').val() == "" || $('#function_id').val() == null || $('#function_id').val() == "0") {
        $('#function_id').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#function_id').css('border-color', 'lightgrey');
    }

    if ($('#function_level').val() == "" || $('#function_level').val() == null || $('#function_level').val()=="0") {
        $('#function_level').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#function_level').css('border-color', 'lightgrey');
    }

    if ($('#region').val() == "") {
        $('#region').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#region').css('border-color', 'lightgrey');
    }

    if ($('#requester').val() == "") {
        $('#requester').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#requester').css('border-color', 'lightgrey');
    }


    if ($('#lm_of_requested').val() == "") {
        $('#lm_of_requested').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#lm_of_requested').css('border-color', 'lightgrey');
    }


    //if ($('#expected_time_effort_hr').val() == "") {
    //    $('#expected_time_effort_hr').css('border-color', 'Red');
    //    isValid = false;
    //}
    //else {
    //    $('#expected_time_effort_hr').css('border-color', 'lightgrey');
    //}

    if ($('#efficiency').val() == "") {
        $('#efficiency').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#efficiency').css('border-color', 'lightgrey');
    }
    //alert("ef - " + $('#efficiency').val().trim());
    if ($('#efficiency_value').val() == "") {
        $('#efficiency_value').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#efficiency_value').css('border-color', 'lightgrey');
    }
    //alert("ef - "+$('#efficiency_value').val().trim());
    if ($('#request_description').val() == "") {
        $('#request_description').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#request_description').css('border-color', 'lightgrey');
    }
    if ($("#requester").val() == "") {
        $('#requester').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#requester').css('border-color', 'lightgrey');
    }
    if ($("#cli_ui_name").val() == "" || $("#cli_ui_name").val() == null || $("#cli_ui_name").val() == "00000000-0000-0000-0000-000000000000" || $("#cli_ui_name").val() == "--Select--") {
        $('#cli_ui_name').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#cli_ui_name').css('border-color', 'lightgrey');
    }
    //alert("ef - "+$('#request_description').val().trim());
    return isValid;
}

$('body').on('click', '#btndraft', function () {
    var res = validateMadatoryFields();
    if (res == false) {
        return false;
    }
    //alert('draft')
    var projectRequirement = new FormData();
    var files = $("#mop_sop_attachment_path").get(0).files;
    for (var i = 0; i < files.length; i++) {

        projectRequirement.append(files[i].name, files[i]);
    }
    projectRequirement.append("auto_id", $("#auto_id").val());
    projectRequirement.append("requirement_type", $("#requirement_type").val());
    projectRequirement.append("project_name", $("#project_name").val());
    projectRequirement.append("customer", $("#customer").val());
    projectRequirement.append("request_title", $("#request_title").val());
    projectRequirement.append("request_description", $("#request_description").val());
    projectRequirement.append("request_notes", $("#request_notes").val());
    projectRequirement.append("domain", $("#domain").val());
    projectRequirement.append("vendor", $("#vendor").val());
    projectRequirement.append("request_priority", $("#request_priority").val());
    projectRequirement.append("node_type", $("#node_type").val());
    projectRequirement.append("requester", $("#requester").val());
    projectRequirement.append("lm_of_requested", $("#lm_of_requested").val());
    projectRequirement.append("date_of_request", $("#date_of_request").val());
    projectRequirement.append("mop_or_sop", $("#mop_or_sop").val());
    projectRequirement.append("connectivity_availability", $("#connectivity_availability").val());
    projectRequirement.append("crediential_availability", $("#crediential_availability").val());
    projectRequirement.append("attachment_for_mop_or_sop", $("#attachment_for_mop_or_sop").val());
    projectRequirement.append("function_id", $("#function_id").val());
    projectRequirement.append("function_level", $("#function_level").val());
    projectRequirement.append("region", $("#region").val());
    projectRequirement.append("expected_time_effort_hr", $("#expected_time_effort_hr").val());
    projectRequirement.append("efficiency", $("#efficiency").val());
    projectRequirement.append("efficiency_value", $("#efficiency_value").val());
    projectRequirement.append("is_resolution_offered", $("#is_resolution_offered").val());
    projectRequirement.append("technology", $("#technology").val());
    projectRequirement.append("request_status", "aa102745-6f04-ee11-99ad-9da677dd1a69".toString());
    projectRequirement.append("request_for", $("#request_for").val());
    projectRequirement.append("cli_ui_name", $("#cli_ui_name").val());

    $.ajax({
        url: '/NewRequest/Save',
        type: 'POST',
        data: projectRequirement,
        processData: false,  // tell jQuery not to process the data
        contentType: false,  // tell jQuery not to set contentType
        success: function (data) {
            swal(data.Title, data.Message, data.Type).then(function () {
                window.location.href = "/NewRequest/Index";
                //window.location.reload();

            });
        },
        failure: function (errorMessage) {
            swal("Oops!", errorMessage, "failure");
        }
    });
});

function BindRegion() {
    if ($("#project_name").val() != "") {
        var request = {
            project: $("#project_name").val().toUpperCase()
        }
        $.ajax({
            url: "/NewRequest/GetRegionByProjectID",
            data: JSON.stringify(request),
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (data) {
                var op = "";
                op += "<option>" + '--select--';
                $.each(data, function (i, item) {
                    op += "<option value='" + item.region + "'>" + item.region_name;
                });

                $("#region").html('');
                $("#region").append(op);
            }
        });
    }

}