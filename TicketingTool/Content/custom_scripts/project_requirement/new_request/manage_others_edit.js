
$(document).ready(function () {
    BindProject();
    //BindCustomers();
    if ($("#efficiency").val() == NaN || $("#efficiency").val() == '' || $("#efficiency").val() == null)
        $("#efficiency_value_section").hide();
    BindRegion();
});

function BindCustomers() {
    if ($("#project_name").val() != "") {
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
    loadDataforproject();
  
}
function loadDataforcustomer() {
    $.get("/NewRequest/GetRequirementDetail/" + $("#id").val(), function (data, status) {
        $("#auto_id").val(data.auto_id);
        $("#customer").val(data.customer);
        BindVendor();
    })
}
function loadDataforproject() {
    $.get("/NewRequest/GetRequirementDetail/" + $("#id").val(), function (data, status) {
        $("#auto_id").val(data.auto_id);
        $("#project_name").val(data.project_name);
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
                loadDataforcustomer();
            }
        });
    })
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
    //alert("till vendot"+$("#project_name").val());
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
           // alert('vendor finish');
            loadDataforvendor();
        }
    });
}
function loadDataforvendor() {
    $.get("/NewRequest/GetRequirementDetail/" + $("#id").val(), function (data, status) {
        $("#auto_id").val(data.auto_id);
        $("#vendor").val(data.vendor);
        //alert('ready for tecjh');
        BindTechnology();
        //loadDataforTech();
    })
}
function loadDataforTech() {
    $.get("/NewRequest/GetRequirementDetail/" + $("#id").val(), function (data, status) {
        $("#auto_id").val(data.auto_id);
        $("#technology").val(data.technology);
        BindNodeType();
    })
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
            loadDataforTech();
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
            loadDatafornode_type();
        }
    });
}
function loadDatafornode_type() {
    $.get("/NewRequest/GetRequirementDetail/" + $("#id").val(), function (data, status) {
        $("#auto_id").val(data.auto_id);
        $("#node_type").val(data.node_type);
    })
    loadDataAll();
}

function loadDataAll() {
    $.get("/NewRequest/GetRequirementDetail/" + $("#id").val(), function (data, status) {
        $("#auto_id").val(data.auto_id);
        $("#requirement_type").val(data.requirement_type);
        //$("#project_name").val(data.project_name);
        //$("#customer").val(data.customer);
        $("#request_title").val(data.request_title);
        $("#request_description").val(data.request_description);
        $("#request_notes").val(data.request_notes);
        $("#domain").val(data.domain);
        //$("#vendor").val(data.vendor);
        //$("#technology").val(data.technology);
        $("#request_priority").val(data.request_priority);
       // $("#node_type").val(data.node_type);
        $("#requester").val(data.requester);
        $("#lm_of_requested").val(data.lm_of_requested);
        if (data.date_of_request != "" && data.date_of_request != null) {
            $("#date_of_request").val(parseJsonDate_d(data.date_of_request));
        }

        $("#mop_or_sop").val(data.mop_or_sop);
       
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
        //if (data.sdlc_status != null)
        //    $("#sdlc_status").val(data.sdlc_status);
        //if (data.is_uat_offered != null)
        //    $("#is_uat_offered").val(upperCase(data.is_uat_offered.toString()));
        //if (data.is_correction_during_uat != null)
        //    $("#is_correction_during_uat").val(upperCase(data.is_correction_during_uat.toString()));
        //if (data.is_enhancement_during_uat != null)
        //    $("#is_enhancement_during_uat").val(upperCase(data.is_enhancement_during_uat.toString()));
        if (data.is_live != null)
            $("#is_live").val(data.is_live.toString().charAt(0).toUpperCase() + data.is_live.toString().slice(1));

        if (data.efficiency != null) {
            $("#efficiency").val(data.efficiency);
            $("#efficiency_value").val(data.efficiency_value);
            $("#efficiency_value_section").show();
        }

        
        $("#connectivity_availability").val(data.connectivity_availability.toString().charAt(0).toUpperCase() + data.connectivity_availability.toString().slice(1));
        $("#crediential_availability").val(data.crediential_availability.toString().charAt(0).toUpperCase() + data.crediential_availability.toString().slice(1));
        if (data.is_resolution_offered != null) {
            $("#is_resolution_offered").val(data.is_resolution_offered.toString().charAt(0).toUpperCase() + data.is_resolution_offered.toString().slice(1));
        }
       // LoadStatusHistory(data.auto_id);
       // LoadHistory(data.auto_id);
        LaodAttachements();
        var request = {
            id: $("#id").val()
        }
        $.ajax({
            url: "/NewRequest/GetRequestedStatus",
            data: JSON.stringify(request),
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (data2) {

                if (data2.status_name == "Request Submitted" || data2.status_name == "Acknowledge" || data2.status_name == "In-Progress" || data2.status_name == "Validation" || data2.status_name == "Completed" || data2.status_name == "Rejected")
                {
                    //$('#btnSave').prop('disabled', true);
                    $('#request_priority').prop('disabled', true);
                    $('#project_name').prop('disabled', true);
                    $('#domain').prop('disabled', true);
                    $('#connectivity_availability').prop('disabled', true);
                    $('#crediential_availability').prop('disabled', true);
                    $('#function_level').prop('disabled', true);
                    $('#region').prop('disabled', true);
                    $('#expected_time_effort_hr').prop('disabled', true);
                    $('#efficiency').prop('disabled', true);
                    $('#Value').prop('disabled', true);
                    $('#request_description').prop('disabled', true);
                    $('#request_notes').prop('disabled', true);
                    $('#mop_or_sop').prop('disabled', true);
                    $('#mop_sop_attachment_path').prop('disabled', true);
                    $('#request_title').prop('disabled', true);

                    $('#customer').prop('disabled', true);
                    $('#vendor').prop('disabled', true);
                    $('#technology').prop('disabled', true);
                    $('#node_type').prop('disabled', true);
                    $('#function_id').prop('disabled', true);
                    $('#is_resolution_offered').prop('disabled', true);
                    $('#efficiency_value').prop('disabled', true);
                    $('#btnDeleteAttachement').prop('disabled', true);
                    $('#cli_ui_name').prop('disabled', true);
                    $("#btnSave").hide();
                }
                else {
                    
                }
            }
        });
        BindRegion_select(data.region);
        $("#cli_ui_name").val(data.cli_ui_name);
        //$("#node_type").val(data.node_type);
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
$("#efficiency").change(function () {
    if ($("#efficiency").val() == NaN || $("#efficiency").val() == '' || $("#efficiency").val() == null)
        $("#efficiency_value_section").attr("css", "display:none");
    else {
        $("#efficiency_value").attr("placeholder", "Enter " + $("#efficiency option:selected").text());
        $("#efficiency_label").text($("#efficiency option:selected").text());
        $("#efficiency_value_section").show();
    }
})

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
    projectRequirement.append("cli_ui_name", $("#cli_ui_name").val());

    $.ajax({
        url: '/NewRequest/UpdateOthers',
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

    if ($('#node_type').val().trim() == "") {
        $('#node_type').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#node_type').css('border-color', 'lightgrey');
    }


    if ($('#technology').val().trim() == "") {
        $('#technology').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#technology').css('border-color', 'lightgrey');
    }

    if ($('#function_id').val().trim() == "") {
        $('#function_id').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#function_id').css('border-color', 'lightgrey');
    }

    if ($('#function_level').val().trim() == "") {
        $('#function_level').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#function_level').css('border-color', 'lightgrey');
    }

    if ($('#region').val().trim() == "") {
        $('#region').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#region').css('border-color', 'lightgrey');
    }

    if ($('#requester').val().trim() == "") {
        $('#requester').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#requester').css('border-color', 'lightgrey');
    }


    if ($('#lm_of_requested').val().trim() == "") {
        $('#lm_of_requested').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#lm_of_requested').css('border-color', 'lightgrey');
    }


    //if ($('#expected_time_effort_hr').val().trim() == "") {
    //    $('#expected_time_effort_hr').css('border-color', 'Red');
    //    isValid = false;
    //}
    //else {
    //    $('#expected_time_effort_hr').css('border-color', 'lightgrey');
    //}

    if ($('#efficiency').val().trim() == "") {
        $('#efficiency').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#efficiency').css('border-color', 'lightgrey');
    }
    //alert("ef - " + $('#efficiency').val().trim());
    if ($('#efficiency_value').val().trim() == "") {
        $('#efficiency_value').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#efficiency_value').css('border-color', 'lightgrey');
    }
    //alert("ef - "+$('#efficiency_value').val().trim());
    if ($('#request_description').val().trim() == "") {
        $('#request_description').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#request_description').css('border-color', 'lightgrey');
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
    projectRequirement.append("cli_ui_name", $("#cli_ui_name").val());

    $.ajax({
        url: '/NewRequest/Save',
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


        }
    });
}
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