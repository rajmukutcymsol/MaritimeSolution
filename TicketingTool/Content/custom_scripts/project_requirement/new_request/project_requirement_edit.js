//const { console } = require("../../../bower_components/inputmask/dist/inputmask/global/window");

$(document).ready(function () {
    var sessionValue = $('#sessionValue').val();
    //alert(sessionValue);
    if ($("#efficiency").val() == NaN || $("#efficiency").val() == '' || $("#efficiency").val() == null)
        $("#efficiency_value_section").hide();
    loadData();
    LaodAttachements();
    $("#request_status").change(function () {
        var selectedText = $('#request_status option:selected').text();
        if (selectedText == "Completed") {
            $("#shairpoint").attr("placeholder", "Enter Shairpoint URL");
            $("#shairpoint").show();
            if ($("#shairpoint").val() == "") {
                $("#shairpointLink").remove();
            }
        }
        else {
            //alert("com");
            $("#shairpoint").attr("css", "display:none");
            $("#shairpoint").hide();
        }
        //if (selectedText == "In-Progress") {
        //    var rowCount = $('#DevTable tr').length;
        //    if (rowCount == "1")
        //    {
        //        alert("Assign Task to Team Member's");
        //        return;
        //    }
        //}
        if (selectedText == "In-Progress" || selectedText == "Completed") {
            $("#task_head").show();
            $("#task").show();
        }
        else {
            $("#task_head").hide();
            $("#task").hide();
        }
    })

    var shairpointUrl = $("#ShairpointValue").val();
    if (shairpointUrl != "" || shairpointUrl != null) {
        $("#shairpointLink").attr("href", shairpointUrl);
    }
    $("#expected_time_effort_date").datepicker({
        dateFormat: "mm/DD/yyyy" // Set the desired date format
    });
    BindRegion();
    GetRequestersInfo();
});

//$("#expected_time_effort_date").click(function () {
//    var currentValue = $("#expected_time_effort_date").val();
//    $("#expected_time_effort_date").val(currentValue);
//    sessionStorage.setItem("currentValue", currentValue);

//    var storedValue = sessionStorage.getItem("currentValue");
//    if (storedValue !== null) {
//        $("#expected_time_effort_date").val(storedValue);
//    }
//});
//$(document).click(function () {
//    var storedValue = sessionStorage.getItem("currentValue");
//    if (storedValue !== null && $("#expected_time_effort_date").val() == "") {
//        $("#expected_time_effort_date").val(storedValue);
//    }
//    else {

//    }
//});
$("#efficiency").change(function () {
    if ($("#efficiency").val() == NaN || $("#efficiency").val() == '' || $("#efficiency").val() == null)
        $("#efficiency_value_section").attr("css", "display:none");
    else {
        $("#efficiency_value").val('');
        $("#efficiency_value").attr("placeholder", "Enter " + $("#efficiency option:selected").text());
        $("#efficiency_label").text($("#efficiency option:selected").text());
        $("#efficiency_value_section").show();
    }
})

function GetRequestersInfo() {
    $("#requester").autocomplete({
        source: function (request, response) {
            $.ajax({
                url: '/NewRequest/GetUserList/',
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
                    if (data.employee_id != "" || data.employee_id != null) {
                        var _employeeid = data.employee_id;
                        $("#request_for").val(_employeeid);
                    }
                    else {
                        alert('Select correct requester');

                    }
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

function loadData() {
    $.get("/NewRequest/GetRequirementDetail/" + $("#id").val(), function (data, status) {
        $("#auto_id").val(data.auto_id);
        $("#requirement_type").val(data.requirement_type);
        $("#project_name").val(data.project_name);
        $("#customer").val(data.customer);
        $("#request_title").val(data.request_title);
        $("#request_description").val(data.request_description);
        $("#request_notes").val(data.request_notes);
        $("#domain").val(data.domain);
        $("#vendor").val(data.vendor);
        $("#technology").val(data.technology);
        $("#request_priority").val(data.request_priority);
        $("#node_type").val(data.node_type);
        $("#requester").val(data.requester);
        $("#lm_of_requested").val(data.lm_of_requested);
        if (data.date_of_request != "" && data.date_of_request != null) {
            $("#date_of_request").val(parseJsonDate_d(data.date_of_request));
        }
       
        $("#mop_or_sop").val(data.mop_or_sop);
        $("#connectivity_availability").val(upperCase(data.connectivity_availability.toString()));
        $("#crediential_availability").val(upperCase(data.crediential_availability.toString()));
        $("#function_id").val(data.function_id);
        $("#function_level").val(data.function_level);
        $("#region").val(data.region);
        $("#expected_time_effort_hr").val(data.expected_time_effort_hr);
        $("#cost_value").val(data.cost_value);
        if (data.request_status != "" && data.request_status != null)
            $("#request_status").val(data.request_status);
        else
            $("#request_status").prop("selectedIndex", 0).val();
        $("#solution_architect").val(data.solution_architect);
        $("#tester_name_sit").val(data.tester_name_sit);
        $("#developer_name").val(data.developer_name);
        $("#project_manager").val(data.project_manager);
        $("#expected_cost_of_development").val(data.expected_cost_of_development);
        $("#expected_time_of_development_hour").val(data.expected_time_of_development_hour);
        if (data.sdlc_status != null)
            $("#sdlc_status").val(data.sdlc_status);
        if (data.is_uat_offered != null)
            $("#is_uat_offered").val(upperCase(data.is_uat_offered.toString()));
        if (data.is_correction_during_uat != null)
            $("#is_correction_during_uat").val(upperCase(data.is_correction_during_uat.toString()));
        if (data.is_enhancement_during_uat != null)
            $("#is_enhancement_during_uat").val(upperCase(data.is_enhancement_during_uat.toString()));
        if (data.is_live != null)
            $("#is_live").val(upperCase(data.is_live.toString()));

        if (data.efficiency != null) {
            $("#efficiency").val(data.efficiency);
            $("#efficiency_value").val(data.efficiency_value);
            $("#efficiency_value_section").show();
        }
        if (data.is_resolution_offered != null) {
            $("#is_resolution_offered").val(upperCase(data.is_resolution_offered.toString()));
        }
        if (data.shairpoint_url != null) {
           
            $("#shairpoint_url").val(data.shairpoint_url);
            $("#shairpoint").show();
        }
        $("#project_manager_name").val(data.project_manager_name);
        $("#master_solution_architect_name").val(data.master_solution_architect_name);
        $("#tester_name").val(data.tester_name);
       

        LoadStatusHistory(data.auto_id);
        LoadHistory(data.auto_id);
        //alert(data.auto_id);
        LoadDevelopersTask(data.auto_id);

        
        var request = {
            auto_id: data.auto_id
        }
        $.ajax({
            url: "/NewRequest/GetChatData",
            data: JSON.stringify(request),
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (data) {
                var htmlCode = data.html;
                $('#tblChatDate').html(htmlCode);
            }
        });
        var RequestStatus = $('#request_status option:selected').text();
        if (RequestStatus == "In-Progress" || RequestStatus == "Completed") {
            $("#task_head").show();
            $("#task").show();
        }
        else {
            $("#task_head").hide();
            $("#task").hide();
        }
        $("#expected_time_effort_date").val(parseJsonDateIN_DDMMYYYY(data.expected_time_effort_date));
        BindRegion_select(data.region);
        $("#request_for").val(data.request_for);
        $("#cli_ui_name").val(data.cli_ui_name);
    })
}
function BindRegion_select(region) {
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
                $("#region").val(region)
            }
        });
    }

}
function parseJsonDateIN_DDMMYYYY(jsonDate) {
    if (jsonDate === null) {
        return ""; // or any other appropriate value or error handling
    }
    var parsedDate = new Date(parseInt(jsonDate.substr(6)));
    var day = parsedDate.getDate();
    var month = parsedDate.getMonth() + 1;
    var year = parsedDate.getFullYear();

    return month + '/' + day + '/' + year;
}
function parseJsonDate_d(jsonDate) {
    var milliseconds = parseInt(jsonDate.replace(/[^0-9]/g, ''));
    var date = new Date(milliseconds);

    var day = ('0' + date.getDate()).slice(-2);
    var month = ('0' + (date.getMonth() + 1)).slice(-2);
    var year = date.getFullYear();
    var formattedDate = day + '/' + month + '/' + year;

    var hours = ('0' + date.getHours()).slice(-2);
    var minutes = ('0' + date.getMinutes()).slice(-2);
    var seconds = ('0' + date.getSeconds()).slice(-2);
    var formattedTime = hours + ':' + minutes + ':' + seconds;

    return formattedDate + ' ' + formattedTime;
}
$("#request_status").change(function () {
    openModal();
})
function openModal() {
    var dropdown = document.getElementById("request_status");
    var selectedOption = dropdown.options[dropdown.selectedIndex];
    var selectedText = selectedOption.text;
    if (selectedText == "Draft - Pending Requestor Action") {
        $('#myModal').modal('show');

        var messageText = new FormData();
        messageText.append("auto_id", $("#auto_id").val());

        $.ajax({
            url: '/NewRequest/GetUpdateMessage',
            type: 'POST',
            data: messageText,
            processData: false,  // tell jQuery not to process the data
            contentType: false,  // tell jQuery not to set contentType
            success: function (data) {
                $("#message").val(data.messages);
            },
            failure: function (errorMessage) {
                $("#message").val('');
            }
        });
    }
}

//Load Attachements 
function LaodAttachements() {
    var request = {
        id: $("#id").val()
    }
    $.ajax({
        url: "/NewRequest/GetAllAttachements",
        data: JSON.stringify(request),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (data) {
           // alert('hif');
            var sessionValue = $('#sessionValue').val();
            //alert(sessionValue);
          
                var op = '<table class="table table-hover text-nowrap">' +
                    '<thead>' +
                    '<tr>' +
                    /*'<th>id</th>' +*/
                    '<th>Attachement Name</th>' +
                    '<th>Delete</th>' +
                    '</tr> ' +
                    '</thead> ' +
                    '<tbody>';

                $.each(data, function (i, item) {
                    var filename = item.AttachementName;
                    var arr = filename.split('/');
                    op += "<tr>";
                    op += " <td style=\"display: none;\" class=\"nr\">";
                    op += "" + item.id + "";
                    op += "</td>";

                    op += "<td>";
                    op += "<a href=\"" + filename + "\" target=\"_blank\" style=\"margin-right:5px;\"><i class=fa fa-edit\">" + arr[5].toString() + "</i></a>";
                    op += "</td>";

                    op += " <td>";
                    op += "<input type=\"hidden\" value='" + item.id + "'/> <a href=\"/ChangeRequest/Edit/'" + item.id + "'\" style=\"margin-right:5px;\"><a href=\"#\" class=\"btnDeleteAttachement\" style=\"margin-right: 5px;\"><i class=\"fa fa-trash\"></i></a>";
                    op += "</td>";
                    op += "</tr>";

                });
                op += '</tbody>' +
                    '</table>';
                $("#tbldata").html('');
                $("#tbldata").append(op);
            
            var request = {
                id: $("#id").val()
            }
            $.ajax({
                url: "/ChangeRequest/GetRequestedStatus",
                data: JSON.stringify(request),
                type: "POST",
                contentType: "application/json;charset=utf-8",
                dataType: "json",
                success: function (data2) {

                    if (data2.status_name == "Completed" || data2.status_name=="Rejected") {
                        $('#btnSave').prop('disabled', true);
                        $('#requester').prop('disabled', true);
                        $('#lm_of_requested').prop('disabled', true);
                        $('#request_priority').prop('disabled', true);
                        $('#request_title').prop('disabled', true);
                        $('#project_name').prop('disabled', true);
                        $('#tool_solution_name').prop('disabled', true);
                        $('#use_case').prop('disabled', true);
                        $('#connectivity_availability').prop('disabled', true);
                        $('#crediential_availability').prop('disabled', true);
                        $('#region').prop('disabled', true);
                        $('#cost_value').prop('disabled', true);
                        $('#expected_time_of_resolution_hour').prop('disabled', true);
                        $('#business_benifit').prop('disabled', true);
                        $('#support_engineer').prop('disabled', true);
                        $('#support_manager').prop('disabled', true);
                        $('#is_resolution_offered').prop('disabled', true);
                        $('#function_id').prop('disabled', true);
                        $('#function_level').prop('disabled', true);
                        $('#expected_time_effort_hr').prop('disabled', true);
                        $('#request_description').prop('disabled', true);
                        $('#request_notes').prop('disabled', true);
                        $('#mop_or_sop').prop('disabled', true);
                        $('.btnDeleteAttachement').prop('disabled', true);
                        $('#use_case_name').prop('disabled', true);
                        $('#mop_sop_attachment_path').prop('disabled', true);
                        $('#btnAddDevTask').prop('disabled', true);
                        $('.btnDeleteTask').prop('disabled', true);
                        $("#project_manager_name").prop('disabled', true);
                        $("#master_solution_architect_name").prop('disabled', true);
                        $("#tester_name").prop('disabled', true);
                        $("#employee_id").prop('disabled', true);
                        $("#task_details").prop('disabled', true);
                        $("#DevTable button").prop("disabled", true);
                        $("#expected_cost_of_development").prop("disabled", true);
                        $("#actual_time_of_development_hour").prop("disabled", true);
                        $("#expected_time_of_development_hour").prop("disabled", true);
                        $("#sdlc_status").prop("disabled", true);
                        $("#solution_architect").prop("disabled", true);
                        $("#is_uat_offered").prop("disabled", true);
                        $("#is_correction_during_uat").prop("disabled", true);
                        $("#is_enhancement_during_uat").prop("disabled", true);
                        $("#is_live").prop("disabled", true);
                        $("#is_resolution_offered").prop("disabled", true);
                        $("#efficiency").prop("disabled", true);
                        $("#request_status").prop("disabled", true);
                        $("#efficiency_value_section").prop("disabled", true);
                        $("#shairpoint_url").prop("disabled", true);
                        $("#efficiency_value").prop("disabled", true);
                        $("#project_manager_name").prop("disabled", true);
                        $("#master_solution_architect_name").prop("disabled", true);
                        $("#tester_name").prop("disabled", true);
                        $("#employee_id").prop("disabled", true);
                        $("#btnAddDevTask").prop("disabled", true);
                        $("#task_details").prop("disabled", true);
                        $("#customer").prop("disabled", true);
                        $("#vendor").prop("disabled", true);
                        $("#technology").prop("disabled", true);
                        $("#node_type").prop("disabled", true);
                        $("#technology").prop("disabled", true);
                        $("#domain").prop("disabled", true);
                        $("#expected_time_effort_date").prop("disabled", true);
                        $("#cli_ui_name").prop("disabled", true);
                    }
                    else {

                    }
                }
            });
        }
    });
}


function parseJsonDate(jsonDateString) {
    return new Date(parseInt(jsonDateString.replace('/Date(', ''))).toLocaleDateString("fr-CA");;
}


function upperCase(str) {
    str = str.toLowerCase().replace(/\b[a-z]/g, function (letter) {
        return letter.toUpperCase();
    });
    return str;
}

$('body').on('click', '#btnsent', function () {
    var res = validatemessage();
    if (res == false) {
        return false;
    }
    //alert($("#request_status").val());
    var messageData = new FormData();
    messageData.append("auto_id", $("#auto_id").val());
    messageData.append("message", $("#message").val());
    messageData.append("request_status", $("#request_status").val());
    messageData.append("request_title", $("#request_title").val());
    messageData.append("RequirementId", $("#id").val());
    $.ajax({
        url: '/NewRequest/UpdateMessageStatus',
        type: 'POST',
        data: messageData,
        processData: false,  // tell jQuery not to process the data
        contentType: false,  // tell jQuery not to set contentType
        success: function (data) {
            swal(data.Title, data.Message, data.Type).then(function () {
                window.location.reload();
            });
        },
        failure: function (errorMessage) {
            swal("Oops!", errorMessage, "failure");
        }
    });
    });

function validatemessage() {
    if ($("#message").val() == "") {
        $('#message').css('border-color', 'Red');
        return false;
    }
    else {
        $('#message').css('border-color', 'lightgrey');
        return true;
    }
}

$('body').on('click', '#btnSave', function () {

    var res = validateMadatoryFields();
    if (res == false) {
        return false;
    }

    var projectRequirement = new FormData();
    //if ($('#mop_sop_attachment_path').get(0).files.length > 0) {
    //    projectRequirement.append('mop_sop_attachment_path', $('#mop_sop_attachment_path')[0].files[0]);
    //}
    var files = $("#mop_sop_attachment_path").get(0).files;
    for (var i = 0; i < files.length; i++) {

        projectRequirement.append(files[i].name, files[i]);
    }
    projectRequirement.append("id", $("#id").val());
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
    projectRequirement.append("cost_value", $("#cost_value").val());
    projectRequirement.append("request_status", $("#request_status").val());
    projectRequirement.append("date_of_state_changed", $("#date_of_state_changed").val());
    projectRequirement.append("node_type", $("#node_type").val());
    projectRequirement.append("requester", $("#requester").val());
    projectRequirement.append("lm_of_requested", $("#lm_of_requested").val());
    projectRequirement.append("date_of_request", $("#date_of_request").val());
    projectRequirement.append("project_manager", $("#project_manager").val());
    projectRequirement.append("solution_architect", $("#solution_architect").val());
    projectRequirement.append("expected_cost_of_development", $("#expected_cost_of_development").val());
    projectRequirement.append("expected_time_of_development_hour", $("#expected_time_of_development_hour").val());
    projectRequirement.append("developer_name", $("#developer_name").val());
    projectRequirement.append("tester_name_sit", $("#tester_name_sit").val());
    projectRequirement.append("is_uat_offered", $("#is_uat_offered").val());
    projectRequirement.append("is_correction_during_uat", $("#is_correction_during_uat").val());
    projectRequirement.append("is_enhancement_during_uat", $("#is_enhancement_during_uat").val());
    projectRequirement.append("mop_or_sop", $("#mop_or_sop").val());
    projectRequirement.append("updated_mop", $("#updated_mop").val());
    projectRequirement.append("connectivity_availability", $("#connectivity_availability").val());
    projectRequirement.append("crediential_availability", $("#crediential_availability").val());
    projectRequirement.append("attachment_for_mop_or_sop", $("#attachment_for_mop_or_sop").val());
    projectRequirement.append("is_live", $("#is_live").val());
    projectRequirement.append("sdlc_status", $("#sdlc_status").val());
    projectRequirement.append("business_benifit", $("#business_benifit").val());
    projectRequirement.append("function_id", $("#function_id").val());
    projectRequirement.append("function_level", $("#function_level").val());
    projectRequirement.append("region", $("#region").val());
    projectRequirement.append("expected_time_effort_hr", $("#expected_time_effort_hr").val());
    projectRequirement.append("efficiency", $("#efficiency").val());
    projectRequirement.append("efficiency_value", $("#efficiency_value").val());
    projectRequirement.append("actual_time_of_development_hour", $("#actual_time_of_development_hour").val());
    projectRequirement.append("technology", $("#technology").val());
    projectRequirement.append("is_resolution_offered", $("#is_resolution_offered").val());
    projectRequirement.append("shairpoint_url", $("#shairpoint_url").val());
    projectRequirement.append("project_manager_name", $("#project_manager_name").val());
    projectRequirement.append("master_solution_architect_name", $("#master_solution_architect_name").val());
    projectRequirement.append("tester_name", $("#tester_name").val());
    projectRequirement.append("expected_time_effort_date", $("#expected_time_effort_date").val());
    projectRequirement.append("request_for", $("#request_for").val());
    projectRequirement.append("cli_ui_name", $("#cli_ui_name").val());
    
    $.ajax({
        url: '/NewRequest/Update',
        type: 'POST',
        data: projectRequirement,
        processData: false,  // tell jQuery not to process the data
        contentType: false,  // tell jQuery not to set contentType
        success: function (data) {
            swal(data.Title, data.Message, data.Type).then(function () {
                window.location.reload();
            });
        },
        failure: function (errorMessage) {
            swal("Oops!", errorMessage, "failure");
        }
    });
});
$('body').on('click', '.btnDeleteAttachement', function () {
    swal({
        title: "Are you sure?",
        text: "Once deleted, you will not be able to recover",
        icon: "warning",
        buttons: true,
        dangerMode: true,
    })
        .then((willDelete) => {
            if (willDelete) {
                var $item = $(this).closest("tr")   // Finds the closest row <tr> 
                    .find(".nr")     // Gets a descendent with class="nr"
                    .text();
                $.get("/NewRequest/DeleteAttachement", { id: $item}, function (data) {
                    swal(data.Title, data.Message, data.Type).then(function () {
                        LaodAttachements();
                    });
                });
            }
        });
});
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
    if ($('#node_type').val() == "0" || $('#node_type').val() == null || $('#node_type').val() == "") {
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

    if ($('#technology').val() == "" || $('#technology').val() == null || $('#technology').val() == "0") {
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

    if ($('#function_level').val() == "" || $('#function_level').val() == null || $('#function_level').val() == "0") {
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


    if ($('#expected_time_effort_hr').val() == "") {
        $('#expected_time_effort_hr').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#expected_time_effort_hr').css('border-color', 'lightgrey');
    }

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
    if ($("#request_status").val() == null || $("#request_status").val() == "" || $("#request_status").val() == "00000000-0000-0000-0000-000000000000" || $("#request_status").val() == "--select--") {
        $('#request_status').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#request_status').css('border-color', 'lightgrey');
    }
    if ($("#cost_value").val() == ""){
        $('#cost_value').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#cost_value').css('border-color', 'lightgrey');
    }
    var completedStatus = $('#request_status option:selected').text();
    if (completedStatus == "In-Progress" || completedStatus == "Completed") {
        if ($("#project_manager_name").val() == null || $("#project_manager_name").val() == "" || $("#project_manager_name").val() == "00000000-0000-0000-0000-000000000000" || $("#project_manager_name").val() == "--select--") {
            $('#project_manager_name').css('border-color', 'Red');
            isValid = false;
        }
        else {
            $('#project_manager_name').css('border-color', 'lightgrey');
        }
        if ($("#master_solution_architect_name").val() == null || $("#master_solution_architect_name").val() == "" || $("#master_solution_architect_name").val() == "00000000-0000-0000-0000-000000000000" || $("#master_solution_architect_name").val() == "--select--") {
            $('#master_solution_architect_name').css('border-color', 'Red');
            isValid = false;
        }
        else {
            $('#master_solution_architect_name').css('border-color', 'lightgrey');
        }
        if ($("#tester_name").val() == null || $("#tester_name").val() == "" || $("#tester_name").val() == "0" || $("#tester_name").val() == "--select--") {
            $('#tester_name').css('border-color', 'Red');
            isValid = false;
        }
        else {
            $('#tester_name').css('border-color', 'lightgrey');
        }
    }
    else {
        $('#project_manager_name').css('border-color', 'lightgrey');
        $('#master_solution_architect_name').css('border-color', 'lightgrey');
        $('#tester_name').css('border-color', 'lightgrey');
    }
    if ($("#cli_ui_name").val() == "" || $("#cli_ui_name").val() == null || $("#cli_ui_name").val() == "00000000-0000-0000-0000-000000000000" || $("#cli_ui_name").val() == "--Select--") {
        $('#cli_ui_name').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#cli_ui_name').css('border-color', 'lightgrey');
    }
    return isValid;
}
//function validateMadatoryFields() {
//    var isValid = true;
//    if ($('#auto_id').val().trim() == "") {
//        $('#auto_id').css('border-color', 'Red');
//        isValid = false;
//    }
//    else {
//        $('#auto_id').css('border-color', 'lightgrey');
//    }
//    if ($('#date_of_request').val().trim() == "") {
//        $('#date_of_request').css('border-color', 'Red');
//        isValid = false;
//    }
//    else {
//        $('#date_of_request').css('border-color', 'lightgrey');
//    }
//    if ($('#request_priority').val().trim() == "") {
//        $('#request_priority').css('border-color', 'Red');
//        isValid = false;
//    }
//    else {
//        $('#request_priority').css('border-color', 'lightgrey');
//    }
//    if ($('#request_title').val().trim() == "") {
//        $('#request_title').css('border-color', 'Red');
//        isValid = false;
//    }
//    else {
//        $('#request_title').css('border-color', 'lightgrey');
//    }

//    if ($('#node_type').val().trim() == "") {
//        $('#node_type').css('border-color', 'Red');
//        isValid = false;
//    }
//    else {
//        $('#node_type').css('border-color', 'lightgrey');
//    }


//    if ($('#technology').val().trim() == "") {
//        $('#technology').css('border-color', 'Red');
//        isValid = false;
//    }
//    else {
//        $('#technology').css('border-color', 'lightgrey');
//    }

//    if ($('#function_id').val().trim() == "") {
//        $('#function_id').css('border-color', 'Red');
//        isValid = false;
//    }
//    else {
//        $('#function_id').css('border-color', 'lightgrey');
//    }

//    if ($('#function_level').val().trim() == "") {
//        $('#function_level').css('border-color', 'Red');
//        isValid = false;
//    }
//    else {
//        $('#function_level').css('border-color', 'lightgrey');
//    }

//    if ($('#region').val().trim() == "") {
//        $('#region').css('border-color', 'Red');
//        isValid = false;
//    }
//    else {
//        $('#region').css('border-color', 'lightgrey');
//    }

//    if ($('#requester').val().trim() == "") {
//        $('#requester').css('border-color', 'Red');
//        isValid = false;
//    }
//    else {
//        $('#requester').css('border-color', 'lightgrey');
//    }


//    if ($('#lm_of_requested').val().trim() == "") {
//        $('#lm_of_requested').css('border-color', 'Red');
//        isValid = false;
//    }
//    else {
//        $('#lm_of_requested').css('border-color', 'lightgrey');
//    }


//    if ($('#expected_time_effort_hr').val().trim() == "") {
//        $('#expected_time_effort_hr').css('border-color', 'Red');
//        isValid = false;
//    }
//    else {
//        $('#expected_time_effort_hr').css('border-color', 'lightgrey');
//    }
//    var completedStatus = $('#request_status option:selected').text();
//    var shairpoint_value_url = $('#shairpoint_url').val();
//    if (completedStatus == "Completed") {
//        if (shairpoint_value_url == "") {
//            $('#shairpoint_url').css('border-color', 'Red');
//            isValid = false;
//        }
//    }
//    else {
//        $('#shairpoint_url').css('border-color', 'lightgrey');
//    }
//    //if ($('#project_manager_name').val() == null && completedStatus=="In-Progress") {
//    //    $('#project_manager_name').css('border-color', 'Red');
//    //    isValid = false;
//    //}
//    //else {
//    //    $('#project_manager_name').css('border-color', 'lightgrey');
//    //}
//    //if ($('#master_solution_architect_name').val() == null && completedStatus == "In-Progress") {
//    //    $('#master_solution_architect_name').css('border-color', 'Red');
//    //    isValid = false;
//    //}
//    //else {
//    //    $('#master_solution_architect_name').css('border-color', 'lightgrey');
//    //}
    
//    return isValid;
//}

function LoadHistory(auto_id) {
    var request = {
        auto_id: auto_id
    }
    
    $.ajax({
        url: "/NewRequest/GetHistoryByDate",
        data: JSON.stringify(request),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (data) {
            var htmlCode = data.html;

            // Update the desired element with the HTML code
            $('#HistoryData').html(htmlCode);
            //console.log(data);
            //for (var key in data) {
            //    if (data.hasOwnProperty(key)) {
            //        var item = data[key];

            //        console.log(data[key]);
            //        console.log(item.update_date);
            //        var request1 = {
            //            auto_id: item.auto_id,
            //            DateofUpdate: item.update_date
            //        }
            //        $.ajax({
            //            url: "/NewRequest/GetUpdatedHistoryByAutoId",
            //            data: JSON.stringify(request1),
            //            type: "POST",
            //            contentType: "application/json;charset=utf-8",
            //            dataType: "json",
            //            success: function (data1) {
            //                for (var i in data1) {
            //                    if (data1.hasOwnProperty(i)) {
            //                        var item1 = data1[i];
            //                        console.log(item1);
            //                    }
            //                }
                           
            //            }
            //        });




            //    }
            //}
        }
    });
}
function LoadStatusHistory(auto_id) {
    var request = {
        auto_id: auto_id
    }
    $.ajax({
        url: "/NewRequest/GetStatusHistory",
        data: JSON.stringify(request),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (data) {
            var htmlCode = data.html;
            $('#StatusHistory').html(htmlCode);
        }
    });
}

function LoadDevelopersTask(auto_id) {
    //alert(auto_id);
    var request = {
        auto_id: auto_id
    }
    $.ajax({
        url: "/NewRequest/GetDeveloperTaskData",
        data: JSON.stringify(request),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (data) {
            var sessionValue = $('#sessionValue').val();
            var op = '<div class="card-body p-0"><table class="table table-sm" style="border: #141e57 1px solid;" >' +
                '<thead>' +
                '<tr style="background-color:aliceblue;">' +
                /*'<th>id</th>' +*/
                '<th>Employee Name</th>' +
                '<th>Task Details</th>' +
                '<th>Status</th>' +
                '<th>Date</th>' +
                '<th>Action</th>' +
                '</tr> ' +
                '</thead> ' +
                '<tbody>';

            $.each(data, function (i, item) {
                op += "<tr>";
                op += " <td style=\"display: none;\" class=\"nr\">";
                op += "" + item.Taskid + "";
                op += "</td>";

                op += "<td>";
                op += item.employee_name;
                op += "</td>";

                op += "<td>";
                op +=  item.task_details ;
                op += "</td>";

                op += "<td>";
                op += item.status ;
                op += "</td>";

                op += "<td  style=\"padding: 5px;\">";
                op +=  item.dateoftask ;
                op += "</td>";

                if (sessionValue == "admin") {
                    op += "<td>";
                    op += "<input type=\"hidden\" value='" + item.Taskid + "'/> <a   onclick=\"openEditModal('" + item.Taskid + "')\" style=\"margin-right: 5px;\"><i class=\"fa fa-edit\"></i></a><a href=\"#\" class=\"btnDeleteTask\" style=\"margin-right: 5px;\"><i class=\"fa fa-trash\"></i></a>";
                    op += "</td>";
                }
                else {
                    op += "<td>";
                    op += "<input type=\"hidden\" value='" + item.Taskid + "'/> <a  onclick=\"openEditModal('" + item.Taskid + "')\" style=\"margin-right: 5px;\"><i class=\"fa fa-edit\"></i></a>";
                    op += "</td>";
                }
            });
            op += '</tbody>' +
                '</table></div>';
            $("#DevTable").html('');
            $("#DevTable").append(op);

            $("#task_details").val('');

            var request = {
                id: $("#id").val()
            }
            $.ajax({
                url: "/ChangeRequest/GetRequestedStatus",
                data: JSON.stringify(request),
                type: "POST",
                contentType: "application/json;charset=utf-8",
                dataType: "json",
                success: function (data2) {

                    if (data2.status_name == "Completed") {
                        $('#DevTable tr').each(function () {
                            $(this).find('td:last, th:last').remove();
                        });
                    }
                    else {

                    }
                }
            });
            

        }
    });
    
    //var request = {
    //    auto_id: $("#auto_id").val()
    //};
    //alert($("#auto_id").val());

    //$('#DevTable').DataTable({
    //    "ajax": {
    //        "url": "/NewRequest/GetDeveloperTaskData",
    //        "type": "GET",
    //        "datatype": "json",
    //        "data": request // Remove JSON.stringify() as DataTables handles serialization internally
    //    },
    //    "columns": [
    //        { "data": "Taskid" },
    //        { "data": "employee_name" },
    //        { "data": "task_details" },
    //        { "data": "status" },
    //        { "data": "dateoftask" },
    //        //{
    //        //    "data": null,
    //        //    "render": function (data, type, row) {
    //        //        return '<button onclick="editRow(' + row.Taskid + ')">Edit</button>' + // Fix Taskid property name
    //        //            '<button onclick="deleteRow(' + row.Taskid + ')">Delete</button>'; // Fix Taskid property name
    //        //    }
    //        //}
    //    ]
    //});
}

function editRow(id) {
    // Implement your logic for editing the row with the given id
    // You can display a modal or navigate to an edit page
    window.location.href = '/DeveloperTask/Edit/' + id;
}

function deleteRow(id) {
    // Implement your logic for deleting the row with the given id
    if (confirm("Are you sure you want to delete this task?")) {
        $.ajax({
            url: '/DeveloperTask/Delete/' + id,
            type: 'POST',
            success: function (response) {
                if (response.success) {
                    alert(response.message);
                    // Refresh the DataTable
                   // $('#DevTable').DataTable().ajax.reload();
                } else {
                    alert(response.message);
                }
            },
            error: function () {
                alert("An error occurred while deleting the task.");
            }
        });
    }
}
$(document).ready(function () {
    $("#employee_id").change(function () {
        var selectedValue = $(this).val();
    });
});
$('body').on('click', '#btnAddDevTask', function () {

    var selectedText = $('#request_status option:selected').text();
    if (selectedText == "In-Progress") {
        $('#request_status').css('border-color', 'lightgrey');
    }
    else {
        swal("Oops!", "Save Status as In-Progress before assigning task to developers", "failure");
        return;
    }
    if ($("#task_details").val() == "") {
        $('#task_details').css('border-color', 'Red');
        return;
    }
    else {
        $('#task_details').css('border-color', 'lightgrey');
    }
    
    if ($("#employee_id").val() == null) {
        $('#employee_id').css('border-color', 'Red');
        return;
    }
    else {
        $('#employee_id').css('border-color', 'lightgrey');
    }
    //if ($("#project_manager_name").val() == null) {
    //    $('#project_manager_name').css('border-color', 'Red');
    //    return;
    //}
    //else {
    //    $('#project_manager_name').css('border-color', 'Red');
    //}
    //if ($("#master_solution_architect_name").val() == null) {
    //    $('#master_solution_architect_name').css('border-color', 'Red');
    //    return;
    //}
    //else {
    //    $('#master_solution_architect_name').css('border-color', 'Red');

    //}
    
    var projectvalidateDevTask = new FormData();
    projectvalidateDevTask.append("auto_id", $("#auto_id").val());
    projectvalidateDevTask.append("employee_id", $("#employee_id").val());
    projectvalidateDevTask.append("task_details", $("#task_details").val());

    $.ajax({
        url: '/NewRequest/SaveDevTask',
        type: 'POST',
        data: projectvalidateDevTask,
        processData: false,  
        contentType: false,  
        success: function (data) {
            LoadDevelopersTask($("#auto_id").val());
        },
        failure: function (errorMessage) {
            swal("Oops!", errorMessage, "failure");
        }
    });
});
function validateDevTask() {
    var isValid = true;
    if ($('#auto_id').val().trim() == "") {
        $('#auto_id').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#auto_id').css('border-color', 'lightgrey');
    }
    if ($('#employee_id').val().trim() == "") {
        $('#employee_id').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#employee_id').css('border-color', 'lightgrey');
    }
    if ($('#task_details').val().trim() == "") {
        $('#task_details').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#task_details').css('border-color', 'lightgrey');
    }
    return isValid;
}
function openEditModal(taskId) {
    $.ajax({
        url: '/NewRequest/GetDevTaskByID',
        type: 'POST',
        contentType:"application/json; charset=utf-8",
        data: '{"TaskId":"' + taskId + '"}',
        dataType:"json",
        success: function (data) {
            $("#taskdetails").val(data.taskdetails);
            $("#taskstatus").val(data.taskstatus);
            $("#employeeid").val(data.employeeid);
            $("#TaskComments").val(data.TaskComments);
            $("#Taskid").val(data.Taskid);
        },
        failure: function (errorMessage) {
            $("#message").val('');
        }
    });
    $('#DevModel').modal('show');
}
$('body').on('click', '#btnTaskUpdate', function () {
   // var selectedValue = $("#Taskid").val();
    //alert(selectedValue);
    if ($("#taskdetails").val() == "") {
        alert('Ente Task Details');
        return;
    }
   
    var projectUpdateTask = new FormData();
    projectUpdateTask.append("Taskid", $("#Taskid").val());
    projectUpdateTask.append("employee_id", $("#employeeid").val());
    projectUpdateTask.append("task_details", $("#taskdetails").val());
    projectUpdateTask.append("status", $("#taskstatus").val());
    projectUpdateTask.append("TaskComments", $("#TaskComments").val());

    $.ajax({
        url: '/NewRequest/UpdateDevTask',
        type: 'POST',
        data: projectUpdateTask,
        processData: false,  // tell jQuery not to process the data
        contentType: false,  // tell jQuery not to set contentType
        success: function (data) {
            swal(data.Title, data.Message, data.Type).then(function () {
                window.location.reload();
            });
        },
        failure: function (errorMessage) {
            swal("Oops!", errorMessage, "failure");
        }
    });
});

$("body").on('click', '#btnChatSent', function () {
    if ($("#ChatMessage").val() == "") {
        return;
    }
    LaodChat();
})
function LaodChat() {
   // alert($("#sessionUserName").val());
    var autoid = $("#auto_id").val();
    var ChatData = new FormData();
    ChatData.append("auto_id", $("#auto_id").val());
    ChatData.append("ChatMessage", $("#ChatMessage").val());
    ChatData.append("ChatCreatedBy", $("#sessionUserName").val());
    $.ajax({
        url: '/NewRequest/PullChat',
        type: 'POST',
        data: ChatData,
        processData: false,  // tell jQuery not to process the data
        contentType: false,  // tell jQuery not to set contentType
        success: function (data) {
            $("#ChatMessage").val('');
            $('#tblChatDate').html('');
            GetChatHistory(autoid);
        },
        failure: function (errorMessage) {
            swal("Oops!", errorMessage, "failure");
        }
    });
    function GetChatHistory(auto_id) {
       // alert(auto_id);
        var request = {
            auto_id: auto_id
        }

        $.ajax({
            url: "/NewRequest/GetChatData",
            data: JSON.stringify(request),
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (data) {
                var htmlCode = data.html;
                $('#tblChatDate').html(htmlCode);
            }
        });
    }
}

$('body').on('click', '.btnDeleteTask', function () {
    var auto_id= $("#auto_id").val();
    swal({
        title: "Are you sure?",
        text: "Once deleted, you will not be able to recover",
        icon: "warning",
        buttons: true,
        dangerMode: true,
    })
        .then((willDelete) => {
            if (willDelete) {
                var $item = $(this).closest("tr")   // Finds the closest row <tr> 
                    .find(".nr")     // Gets a descendent with class="nr"
                    .text();
                $.get("/NewRequest/DeleteTask", { TaskId: $item }, function (data) {
                    swal(data.Title, data.Message, data.Type).then(function () {
                        LoadDevelopersTask(auto_id);
                    });
                });
            }
        });
});



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

function openModal_Rejection() {
    var dropdown = document.getElementById("request_status");
    var selectedOption = dropdown.options[dropdown.selectedIndex];
    var selectedText = selectedOption.text;
    if (selectedText == "Rejected") {
        $('#Rejection').modal('show');

        var messageText = new FormData();
        messageText.append("auto_id", $("#auto_id").val());

        $.ajax({
            url: '/ChangeRequest/GetUpdateMessage',
            type: 'POST',
            data: messageText,
            processData: false,  // tell jQuery not to process the data
            contentType: false,  // tell jQuery not to set contentType
            success: function (data) {
                //alert(data.messages);
                $("#message_Rejection").val(data.messages);
            },
            failure: function (errorMessage) {
                $("#message_Rejection").val('');
            }
        });
    }
}
$('body').on('click', '#btnsent_Rejection', function () {
    var res = validatemessage_rejected();
    if (res == false) {
        return false;
    }
    //alert($("#request_status").val());
    var messageData = new FormData();
    messageData.append("auto_id", $("#auto_id").val());
    messageData.append("message", $("#message_rejection").val());
    messageData.append("request_status", $("#request_status").val());
    messageData.append("request_title", $("#request_title").val());
    messageData.append("RequirementId", $("#id").val());
    $.ajax({
        url: '/NewRequest/UpdateMessageStatus',
        type: 'POST',
        data: messageData,
        processData: false,  // tell jQuery not to process the data
        contentType: false,  // tell jQuery not to set contentType
        success: function (data) {
            swal(data.Title, data.Message, data.Type).then(function () {
                window.location.reload();
            });
        },
        failure: function (errorMessage) {
            swal("Oops!", errorMessage, "failure");
        }
    });
});
function validatemessage_rejected() {
    if ($("#message_rejection").val() == "") {
        $('#message_rejection').css('border-color', 'Red');
        return false;
    }
    else {
        $('#message_rejection').css('border-color', 'lightgrey');
        return true;
    }
}

$("#request_status").change(function () {
    openModal_Rejection();
})

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