//const { connectLogger } = require("log4js");

$(document).ready(function () {
    
    //BindUseCases();
    BindToolName();
    loadData();
    LaodAttachements();
    $("#request_status").change(function () {
        var selectedText = $('#request_status option:selected').text();
        if (selectedText == "Completed") {
            $("#is_resolution_offered").val("True");
            $("#shairpoint").attr("placeholder", "Enter Shairpoint URL");
            $("#shairpoint").show();

            $("#resolutioncategorys").show();
            $("#resolution").show();


        }
        else {
            //alert("com");
            $("#is_resolution_offered").val("False");
            $("#shairpoint").attr("css", "display:none");
            $("#shairpoint").hide();
            $("#resolutioncategorys").attr("css", "display:none");
            $("#resolutioncategorys").hide();
            $("#resolution").hide();
        }
        if (selectedText == "In-Progress" || selectedText == "Completed") {
            $("#task_head").show();
            $("#task").show();
        }
        else {
            $("#task_head").hide();
            $("#task").hide();
        }
    })

    $("#expected_time_effort_date").datepicker({
        dateFormat: "dd/mm/yy" // Set the desired date format
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
//$("#expected_time_effort_date").click(function () {
//    $("#expected_time_effort_date").blur();
//});
function GetRequestersInfo() {
    $("#requester").autocomplete({
        source: function (request, response) {
            $.ajax({
                url: '/ChangeRequest/GetUserList/',
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
                url: '/ChangeRequest/GetUserByName/',
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
    $.get("/ChangeRequest/GetRequirementDetail/" + $("#id").val(), function (data, status) {
       
        $("#auto_id").val(data.auto_id);
        $("#requirement_type").val(data.requirement_type);
        $("#request_title").val(data.request_title);
        $("#request_description").val(data.request_description);
        $("#request_notes").val(data.request_notes);
        $("#request_priority").val(data.request_priority);
        $("#requester").val(data.requester);
        $("#lm_of_requested").val(data.lm_of_requested);
        if (data.date_of_request != "" && data.date_of_request != null) {
            $("#date_of_request").val(parseJsonDate_d(data.date_of_request));
        }
        if (data.connectivity_availability != null) {
            $("#connectivity_availability").val(upperCase(data.connectivity_availability.toString()));
        }
        if (data.crediential_availability != null) {
            $("#crediential_availability").val(upperCase(data.crediential_availability.toString()));
        }
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
        
        $("#sdlc_status").val(data.sdlc_status);
        $("#expected_time_of_resolution_hour").val(data.expected_time_of_resolution_hour);
        $("#business_benifit").val(data.business_benifit);
        $("#support_engineer").val(data.support_engineer);
        $("#support_manager").val(data.support_manager);
        if (data.is_resolution_offered != "" && data.is_resolution_offered != null)
        $("#business_benifit").val(data.business_benifit);

        if (data.is_resolution_offered != null) {
            $("#is_resolution_offered").val(upperCase(data.is_resolution_offered.toString()));
        }

        if (data.is_uat_offered != null) {
            $("#is_uat_offered").val(upperCase(data.is_uat_offered.toString()));
        }
        if (data.is_correction_during_uat != null) {
            $("#is_correction_during_uat").val(upperCase(data.is_correction_during_uat.toString()));
        }
        if (data.is_enhancement_during_uat != null) {
            $("#is_enhancement_during_uat").val(upperCase(data.is_enhancement_during_uat.toString()));
        }

        if (data.is_live != null) {
            $("#is_live").val(upperCase(data.is_live.toString()));
        }
       
        if (data.project_name != null) {
            $("#project_name").val(data.project_name.toString());
        }
        $("#mop_or_sop").val(data.mop_or_sop);
        
        if (data.shairpoint_url != null) {

            $("#shairpoint_url").val(data.shairpoint_url);
            $("#shairpoint").show();
        }
        $("#project_manager_name").val(data.project_manager_name);
        $("#master_solution_architect_name").val(data.master_solution_architect_name);
        $("#tester_name").val(data.tester_name);
        //alert(data.expected_time_effort_date);
       

        if (data.resolution_category != null && data.resolution_category != "00000000-0000-0000-0000-000000000000" && data.resolution_category != "") {

            $("#resolution_category").val(data.resolution_category);
            $("#resolution_category_comments").val(data.resolution_category_comments);
            $("#resolutioncategorys").show();
            $("#resolution").show();
        }
        
        if (data.tool_solution_name != null) {
            $("#tool_solution_name").val(data.tool_solution_name.toString());
        }
    
        if (data.use_case_name != null) {
            $("#use_case_name").val(data.use_case_name);
        }
        //BindUseCases(data.use_case_name.toString());
        BindToolNames(data.tool_solution_name, data.use_case_name.toString());

        LoadStatusHistory(data.auto_id);
        GetChatHistory(data.auto_id);
        LoadDevelopersTask(data.auto_id);

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
        $("#calvalue").val(parseJsonDateIN_DDMMYYYY(data.expected_time_effort_date));

        BindRegion_select(data.region);
        $("#request_for").val(data.request_for);
        $("#domain").val(data.domain);
        $("#cli_ui_name").val(data.cli_ui_name);
    })
}
function BindRegion_select(region) {
    if ($("#project_name").val() != "") {
        var request = {
            project: $("#project_name").val().toUpperCase()
        }

        $.ajax({
            url: "/ChangeRequest/GetRegionByProjectID",
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
//Load Attachements 
function LaodAttachements() {
    var request = {
        id: $("#id").val()
    }
    $.ajax({
        url: "/ChangeRequest/GetAllAttachements",
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
                        $("#resolution_category").prop("disabled", true);
                        $("#resolution_category_comments").prop("disabled", true);
                        $("#request_status").prop("disabled", true);
                        $("#shairpoint_url").prop("disabled", true);
                        $("#project_manager_name").prop("disabled", true);
                        $("#master_solution_architect_name").prop("disabled", true);
                        $("#tester_name").prop("disabled", true);
                        $("#employee_id").prop("disabled", true);
                        $("#btnAddDevTask").prop("disabled", true);
                        $("#task_details").prop("disabled", true);
                        $('#btnSave').hide();
                        $("#expected_time_effort_date").prop("disabled", true);
                        $("#domain").prop("disabled", true);
                        $("#cli_ui_name").prop("disabled", true);

                        $('#btnSave').show();

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
    projectRequirement.append("id", $("#id").val());
    projectRequirement.append("auto_id", $("#auto_id").val());
    projectRequirement.append("requirement_type", $("#requirement_type").val());
    projectRequirement.append("request_title", $("#request_title").val());
    projectRequirement.append("request_description", $("#request_description").val());
    projectRequirement.append("request_notes", $("#request_notes").val());
    projectRequirement.append("request_priority", $("#request_priority").val());
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
    projectRequirement.append("support_manager", $("#support_manager").val());
    projectRequirement.append("expected_time_of_resolution_hour", $("#expected_time_of_resolution_hour").val());
    projectRequirement.append("cost_value", $("#cost_value").val());
    projectRequirement.append("mop_or_sop", $("#mop_or_sop").val());
    projectRequirement.append("business_benifit", $("#business_benifit").val());
    projectRequirement.append("function_id", $("#function_id").val());
    projectRequirement.append("function_level", $("#function_level").val());
    projectRequirement.append("region", $("#region").val());
    projectRequirement.append("expected_time_effort_hr", $("#expected_time_effort_hr").val());
    projectRequirement.append("project_name", $("#project_name").val());
    projectRequirement.append("shairpoint_url", $("#shairpoint_url").val());
    projectRequirement.append("project_manager_name", $("#project_manager_name").val());
    projectRequirement.append("master_solution_architect_name", $("#master_solution_architect_name").val());
    projectRequirement.append("tester_name", $("#tester_name").val());
    projectRequirement.append("resolution_category", $("#resolution_category").val());
    projectRequirement.append("resolution_category_comments", $("#resolution_category_comments").val());
    projectRequirement.append("request_title", $("#request_title").val());
    projectRequirement.append("expected_time_effort_date", $("#expected_time_effort_date").val());
    projectRequirement.append("request_for", $("#request_for").val());
    projectRequirement.append("domain", $("#domain").val());
    projectRequirement.append("cli_ui_name", $("#cli_ui_name").val());

    $.ajax({
        url: '/ChangeRequest/Update',
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
        text: "Once deleted, you will not be able to recover this role",
        icon: "warning",
        buttons: true,
        dangerMode: true,
    })
        .then((willDelete) => {
            if (willDelete) {
                var $item = $(this).closest("tr")   // Finds the closest row <tr> 
                    .find(".nr")     // Gets a descendent with class="nr"
                    .text();
                $.get("/NewRequest/DeleteAttachement", { id: $item }, function (data) {
                    swal(data.Title, data.Message, data.Type).then(function () {
                        LaodAttachements();
                    });
                });
            }
        });
});
$("#tool_solution_name").change(function () {
    BindUseCases();
})
$("#project_name").change(function () {
    $("#use_case_name").html('');
    BindToolName();
    BindRegion();
})

function BindToolName() {
    if ($("#project_name").val() != "") {
        var request = {
            project_name: $("#project_name").val()
        }
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
function BindToolNames(selecttool, use_case_name) {
    if ($("#project_name").val() != "") {
        var request = {
            project_name: $("#project_name").val()
        }
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
                $("#tool_solution_name").val(selecttool);
                BindUseCases_select(use_case_name, selecttool);
            }
        });
    }
}
function BindUseCases_select(use_case_name, selecttool) {
    if ($("#tool_solution_name").val() != "") {

        var request = {
            tool_id: selecttool,
            project_id: $("#project_name").val()
        }
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
                    op += "<option value='" + item.id.toString() + "'>" + item.use_case_name;
                });

                $("#use_case_name").html('');
                $("#use_case_name").append(op);

                $("#use_case_name").val(use_case_name);
            }
        });
    }
}
function BindUseCases() {
    //alert($("#project_name").val().toUpperCase());
    if ($("#tool_solution_name").val() != "") {
        //alert('bind for use case ' + $("#tool_solution_name").val());
        var request = {
            tool_id: $("#tool_solution_name").val(),
            project_id: $("#project_name").val()
        }
        //console.log(request);
        $.ajax({
            url: "/ChangeRequest/GetUseCasesByToolNameId",
            data: JSON.stringify(request),
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (data) {
                var op = "";
                $.each(data, function (i, item) {
                    op += "<option value='" + item.id + "'>" + item.use_case_name;
                });

                $("#use_case_name").html('');
                $("#use_case_name").append(op);
            }
        });
    }
}

function BindUseCases(use_case_id) {
    //alert($("#project_name").val().toUpperCase());
    if ($("#tool_solution_name").val() != "") {
        //alert('bind for use case ' + $("#tool_solution_name").val());
        var request = {
            tool_id: $("#tool_solution_name").val(),
            project_id: $("#project_name").val()
        }
       
       // console.log(request);
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
                $("#use_case_name").val(use_case_id);

            }
        });
    }
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
            url: '/ChangeRequest/GetUpdateMessage',
            type: 'POST',
            data: messageText,
            processData: false,  // tell jQuery not to process the data
            contentType: false,  // tell jQuery not to set contentType
            success: function (data) {
                //alert(data.messages);
                $("#message").val(data.messages);
                //swal(data.Title, data.Message, data.Type).then(function () {
                //    alert(data.messages);
                //    $("#message").val(data.messages);
                //});
            },
            failure: function (errorMessage) {
                $("#message").val('');
            }
        });
    }
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
        url: '/ChangeRequest/UpdateMessageStatus',
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
function validateMadatoryFields() {
    var isValid = true;
    var completedStatus = $('#request_status option:selected').text();
    var shairpoint_value_url = $('#shairpoint_url').val();
    var resolution_category = $('#resolution_category').val();

    if ($("#business_benifit").val() == "") {
        $('#business_benifit').css('border-color', 'Red');
        //alert('s');
        isValid = false;
    }
    else {
        $('#business_benifit').css('border-color', 'lightgrey');
    }
   
    if (completedStatus == "Completed") {
        if (shairpoint_value_url == "") {
            $('#shairpoint_url').css('border-color', 'Red');
            isValid = false;
        }
    }
    else {
        $('#shairpoint_url').css('border-color', 'lightgrey');
    }
    if ($("#tool_solution_name").val() == "0" || $("#tool_solution_name").val() == "" || $("#tool_solution_name").val() == "--select--") {
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

    if (completedStatus == "Completed") {
        if (resolution_category == null || resolution_category == "" || resolution_category == "00000000-0000-0000-0000-000000000000") {
            $('#resolution_category').css('border-color', 'Red');
            isValid = false;
        }
        if ($('#resolution_category_comments').val() == "") {
            $('#resolution_category_comments').css('border-color', 'Red');
            isValid = false;
        }
        else {
            $('#resolution_category_comments').css('border-color', 'lightgrey');
        }
    }
    else {
        $('#resolution_category').css('border-color', 'lightgrey');
    }
    if ($("#region").val() == "00000000-0000-0000-0000-000000000000" || $("#region").val() == "" || $("#region").val() == "--select--") {
        $('#region').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#region').css('border-color', 'lightgrey');
    }
    if ($("#function_id").val() == "00000000-0000-0000-0000-000000000000" || $("#function_id").val() == "" || $("#function_id").val() == "--select--") {
        $('#function_id').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#function_id').css('border-color', 'lightgrey');
    }
    if ($("#function_level").val() == "00000000-0000-0000-0000-000000000000" || $("#function_level").val() == "" || $("#function_level").val() == "--select--") {
        $('#function_level').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#function_level').css('border-color', 'lightgrey');
    }
    if ($("#request_status").val() == "00000000-0000-0000-0000-000000000000" || $("#request_status").val() == "" || $("#request_status").val() == "--select--") {
        $('#request_status').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#request_status').css('border-color', 'lightgrey');
    }
    //
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
        if ($("#expected_time_effort_date").val() == null || $("#expected_time_effort_date").val() == "" || $("#expected_time_effort_date").val() == "0") {
            $('#expected_time_effort_date').css('border-color', 'Red');
            isValid = false;
        }
        else {
            $('#expected_time_effort_date').css('border-color', 'lightgrey');
        }
    }
    else {
        $('#project_manager_name').css('border-color', 'lightgrey');
        $('#master_solution_architect_name').css('border-color', 'lightgrey');
        $('#tester_name').css('border-color', 'lightgrey');
    }
    if ($("#requester").val() == "") {
        $('#requester').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#requester').css('border-color', 'lightgrey');
    }
    if ($("#domain").val() == "" || $("#domain").val() == null) {
        $('#domain').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#domain').css('border-color', 'lightgrey');
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
$("body").on('click', '#btnChatSent', function () {
    //alert('d');
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

}
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
$('body').on('click', '#btnAddDevTask', function () {

    var res = validateTask()
    if (res == false) {
        return false;
    }
    var projectvalidateDevTask = new FormData();
    projectvalidateDevTask.append("auto_id", $("#auto_id").val());
    projectvalidateDevTask.append("employee_id", $("#employee_id").val());
    projectvalidateDevTask.append("task_details", $("#task_details").val());

    $.ajax({
        url: '/ChangeRequest/SaveDevTask',
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
function validateTask() {
    //alert($("#master_solution_architect_name").val());

    var isValid = true;
    var selectedText = $('#request_status option:selected').text();
    if (selectedText == "In-Progress") {
        $('#request_status').css('border-color', 'lightgrey');
    }
    else {
        $('#request_status').css('border-color', 'Red');
        isValid = false;
    }
    if ($("#task_details").val() == "") {
        $('#task_details').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#task_details').css('border-color', 'lightgrey');
    }

    if ($("#employee_id").val() == null) {
        $('#employee_id').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#employee_id').css('border-color', 'lightgrey');
    }
    if ($("#project_manager_name").val() == "" || $("#project_manager_name").val() == "00000000-0000-0000-0000-000000000000" || $("#project_manager_name").val() == "--select--") {
        $('#project_manager_name').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#project_manager_name').css('border-color', 'lightgrey');
    }
    if ($("#master_solution_architect_name").val() == "" || $("#master_solution_architect_name").val() == "00000000-0000-0000-0000-000000000000" || $("#master_solution_architect_name").val() == "--select--") {
        $('#master_solution_architect_name').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#master_solution_architect_name').css('border-color', 'lightgrey');
    }
    if ($("#tester_name").val() == "" || $("#tester_name").val() == "0" || $("#tester_name").val() == "--select--" || $("#tester_name").val() ==null) {
        $('#tester_name').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#tester_name').css('border-color', 'lightgrey');
    }
    return isValid;
}

$('body').on('click', '.btnDeleteTask', function () {
    var auto_id = $("#auto_id").val();
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
                $.get("/ChangeRequest/DeleteTask", { TaskId: $item }, function (data) {
                    swal(data.Title, data.Message, data.Type).then(function () {
                        LoadDevelopersTask(auto_id);
                    });
                });
            }
        });
});
function LoadDevelopersTask(auto_id) {
    var request = {
        auto_id: auto_id
    }
    $.ajax({
        url: "/ChangeRequest/GetDeveloperTaskData",
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
                op += item.task_details;
                op += "</td>";

                op += "<td>";
                op += item.status;
                op += "</td>";

                op += "<td  style=\"padding: 5px;\">";
                op += item.dateoftask;
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
        url: '/ChangeRequest/UpdateDevTask',
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
function openEditModal(taskId) {
    $.ajax({
        url: '/ChangeRequest/GetDevTaskByID',
        type: 'POST',
        contentType: "application/json; charset=utf-8",
        data: '{"TaskId":"' + taskId + '"}',
        dataType: "json",
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
        url: '/ChangeRequest/UpdateMessageStatus',
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
        console.log(request);

        $.ajax({
            url: "/ChangeRequest/GetRegionByProjectID",
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