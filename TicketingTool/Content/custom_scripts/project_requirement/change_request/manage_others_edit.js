$(document).ready(function () {
    BindToolName();
    
    loadData();
    LaodAttachements();
    if ($("#project_name").val() != "" || $("#project_name").val() == null) {
        BindRegion();
    }
});


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

        if (data.tool_solution_name != null) {
            $("#tool_solution_name").val(data.tool_solution_name.toString());
        }
        //BindUseCases_select(data.use_case_name.toString());
        //BindToolName(data.tool_solution_name);
        //BindToolName(data.tool_solution_name,data.use_case_name);
        BindToolName(data.tool_solution_name, data.use_case_name.toString());

        BindRegion_select(data.region);
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

                    if (data2.status_name == "Request Submitted" || data2.status_name == "Acknowledge" || data2.status_name == "In-Progress" || data2.status_name == "Validation" || data2.status_name == "Completed" || data2.status_name == "Rejected") {
                        //$('#btnSave').prop('disabled', true);
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
                        $('#domain').prop('disabled', true);
                        $('#cli_ui_name').prop('disabled', true);

                        $("#btnSave").hide();
                    }
                    else {

                    }
                }
            });

        }
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
    if ($("#domain").val() == "") {
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
        //alert(res);
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
    projectRequirement.append("domain", $("#domain").val());
    projectRequirement.append("cli_ui_name", $("#cli_ui_name").val());

    $.ajax({
        url: '/ChangeRequest/UpdateOthers',
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
                //op += "<option value='0'>" + '--select--';
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
    if ($("#tool_solution_name").val() != "") {
        //alert('bind for use case ' + $("#tool_solution_name").val());
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
function BindToolName(selecttool, use_case_name) {
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
               // op += "<option value=''>--select--</option>";
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
                    op += "<option value='" + item.id.toString() + "'>" + item.use_case_name;
                });

                $("#use_case_name").html('');
                $("#use_case_name").append(op);

                $("#use_case_name").val(use_case_name);
            }
        });
    }
}

function BindRegion() {
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
            }
        });
    }

}