$(document).ready(function () {
    //loadData();
    $(".mybtn_Edit").hide();
    $(".mybtn_Edit_Cancel").hide();

    $('#pilot_on_board_departure').inputmask('99/99/9999', {
        placeholder: 'DD/MM/YYYY',
        alias: 'datetime',
        inputFormat: 'dd/mm/yyyy',
    });
    GetRemarksDate();
   //*********************

    $(document).ready(function () {
        var allIds = "#gang_daytime, #daytime_hold1_out, #daytime_hold2_out, #daytime_hold3_out, #daytime_hold4_out, #daytime_hold5_out, #daytime_total_out, #gang_first, #first_hold1_out, #first_hold2_out, #first_hold3_out, #first_hold4_out, #first_hold5_out, #first_total_out, #gang_second, #second_hold1_out, #second_hold2_out, #second_hold3_out, #second_hold4_out, #second_hold5_out, #second_total_out, #gang_total, #total_hold1_out, #total_hold2_out, #total_hold3_out, #total_hold4_out, #total_hold5_out, #total_total, #gang_previous, #previous_hold1_out, #previous_hold2_out, #previous_hold3_out, #previous_hold4_out, #previous_hold5_out, #previous_total, #gang_grand_total, #grand_hold1_out, #grand_hold2_out, #grand_hold3_out, #grand_hold4_out, #grand_hold5_out, #grand_total, #balance_cargo_hold1, #balance_cargo_hold2, #balance_cargo_hold3, #balance_cargo_hold4, #balance_cargo_hold5, #balance_total";
        $(allIds).on("input", function () {
            var inputValue = $(this).val();

            if (/^\d*\.?\d*$/.test(inputValue)) {
            } else {
                $(this).val(inputValue.slice(0, -1));
            }
        });
    });

    function updateGangTotal() {
        var gangDaytimeValue = parseFloat($("#gang_daytime").val()) || 0;
        var gangFirstValue = parseFloat($("#gang_first").val()) || 0;
        var gangSecondValue = parseFloat($("#gang_second").val()) || 0;

        var totalValue = gangDaytimeValue + gangFirstValue + gangSecondValue;

        $("#gang_total").val(totalValue);
    }
    $("#gang_daytime, #gang_first, #gang_second").on("input", function () {
        updateGangTotal();
    });

    // dattime 

    function updateDaytimeTotalOut() {
        var hold1Value = parseFloat($("#daytime_hold1_out").val()) || 0;
        var hold2Value = parseFloat($("#daytime_hold2_out").val()) || 0;
        var hold3Value = parseFloat($("#daytime_hold3_out").val()) || 0;
        var hold4Value = parseFloat($("#daytime_hold4_out").val()) || 0;
        var hold5Value = parseFloat($("#daytime_hold5_out").val()) || 0;

        var totalValue = (hold1Value + hold2Value + hold3Value + hold4Value + hold5Value).toFixed(3);

        $("#daytime_total_out").val(totalValue);
    }
    $("#daytime_hold1_out, #daytime_hold2_out, #daytime_hold3_out, #daytime_hold4_out, #daytime_hold5_out").on("input", function () {
        updateDaytimeTotalOut();
    });

    //hold 1 column sum 
    function updateTotalHold1Out() {
        var secondHold1Value = parseFloat($("#second_hold1_out").val()) || 0;
        var firstHold1Value = parseFloat($("#first_hold1_out").val()) || 0;
        var daytimeHold1Value = parseFloat($("#daytime_hold1_out").val()) || 0;

        var totalHold1Value = (secondHold1Value + firstHold1Value + daytimeHold1Value).toFixed(3);

        $("#total_hold1_out").val(totalHold1Value);
    }

    $("#second_hold1_out, #first_hold1_out, #daytime_hold1_out").on("input", function () {
        updateTotalHold1Out();
    });

    // second hold
    function updateTotalHold2Out() {
        var secondHold1Value = parseFloat($("#second_hold2_out").val()) || 0;
        var firstHold1Value = parseFloat($("#first_hold2_out").val()) || 0;
        var daytimeHold1Value = parseFloat($("#daytime_hold2_out").val()) || 0;

        var totalHold1Value = (secondHold1Value + firstHold1Value + daytimeHold1Value).toFixed(3);

        $("#total_hold2_out").val(totalHold1Value);
    }

    $("#second_hold2_out, #first_hold2_out, #daytime_hold2_out").on("input", function () {
        updateTotalHold2Out();
    });

    // thrird hold
    function updateTotalHold3Out() {
        var secondHold1Value = parseFloat($("#second_hold3_out").val()) || 0;
        var firstHold1Value = parseFloat($("#first_hold3_out").val()) || 0;
        var daytimeHold1Value = parseFloat($("#daytime_hold3_out").val()) || 0;

        var totalHold1Value = (secondHold1Value + firstHold1Value + daytimeHold1Value).toFixed(3);

        $("#total_hold3_out").val(totalHold1Value);
    }

    $("#second_hold3_out, #first_hold3_out, #daytime_hold3_out").on("input", function () {
        updateTotalHold3Out();
    });

    // forth hold
    function updateTotalHold4Out() {
        var secondHold1Value = parseFloat($("#second_hold4_out").val()) || 0;
        var firstHold1Value = parseFloat($("#first_hold4_out").val()) || 0;
        var daytimeHold1Value = parseFloat($("#daytime_hold4_out").val()) || 0;

        var totalHold1Value = (secondHold1Value + firstHold1Value + daytimeHold1Value).toFixed(3);

        $("#total_hold4_out").val(totalHold1Value);
    }

    $("#second_hold4_out, #first_hold4_out, #daytime_hold4_out").on("input", function () {
        updateTotalHold4Out();
    });

    // fifth hold
    function updateTotalHold5Out() {
        var secondHold1Value = parseFloat($("#second_hold5_out").val()) || 0;
        var firstHold1Value = parseFloat($("#first_hold5_out").val()) || 0;
        var daytimeHold1Value = parseFloat($("#daytime_hold5_out").val()) || 0;

        var totalHold1Value = (secondHold1Value + firstHold1Value + daytimeHold1Value).toFixed(3);

        $("#total_hold5_out").val(totalHold1Value);
    }

    $("#second_hold5_out, #first_hold5_out, #daytime_hold5_out").on("input", function () {
        updateTotalHold5Out();
    });

    //total_total
    function updateTotalTotal() {
        var totalHold1Value = parseFloat($("#total_hold1_out").val()) || 0;
        var totalHold2Value = parseFloat($("#total_hold2_out").val()) || 0;
        var totalHold3Value = parseFloat($("#total_hold3_out").val()) || 0;
        var totalHold4Value = parseFloat($("#total_hold4_out").val()) || 0;
        var totalHold5Value = parseFloat($("#total_hold5_out").val()) || 0;

        var totalTotalValue = (totalHold1Value + totalHold2Value + totalHold3Value + totalHold4Value + totalHold5Value).toFixed(3);

        $("#total_total").val(totalTotalValue);
    }
    $("#total_hold1_out, #total_hold2_out, #total_hold3_out, #total_hold4_out, #total_hold5_out").on("input change", function () {
        updateTotalTotal();
    });


    // Get All PriInfo
});


// first
function updateDaytimeTotalOut_first() {
    // Get the values of daytime_hold1_out, daytime_hold2_out, daytime_hold3_out, daytime_hold4_out, daytime_hold5_out
    var hold1Value = parseFloat($("#first_hold1_out").val()) || 0;
    var hold2Value = parseFloat($("#first_hold2_out").val()) || 0;
    var hold3Value = parseFloat($("#first_hold3_out").val()) || 0;
    var hold4Value = parseFloat($("#first_hold4_out").val()) || 0;
    var hold5Value = parseFloat($("#first_hold5_out").val()) || 0;

    // Calculate the sum with three decimal places
    var totalValue = (hold1Value + hold2Value + hold3Value + hold4Value + hold5Value).toFixed(3);

    // Update daytime_total_out with the calculated sum
    $("#first_total_out").val(totalValue);
}

// Bind the input event to daytime_hold1_out, daytime_hold2_out, daytime_hold3_out, daytime_hold4_out, and daytime_hold5_out
$("#first_hold1_out, #first_hold2_out, #first_hold3_out, #first_hold4_out, #first_hold5_out").on("input", function () {
    // Call the updateDaytimeTotalOut function when any of these text boxes change
    updateDaytimeTotalOut_first();
});


// second
function updateDaytimeTotalOut_second() {
    // Get the values of daytime_hold1_out, daytime_hold2_out, daytime_hold3_out, daytime_hold4_out, daytime_hold5_out
    var hold1Value = parseFloat($("#second_hold1_out").val()) || 0;
    var hold2Value = parseFloat($("#second_hold2_out").val()) || 0;
    var hold3Value = parseFloat($("#second_hold3_out").val()) || 0;
    var hold4Value = parseFloat($("#second_hold4_out").val()) || 0;
    var hold5Value = parseFloat($("#second_hold5_out").val()) || 0;

    // Calculate the sum with three decimal places
    var totalValue = (hold1Value + hold2Value + hold3Value + hold4Value + hold5Value).toFixed(3);

    // Update daytime_total_out with the calculated sum
    $("#second_total_out").val(totalValue);
}

// Bind the input event to daytime_hold1_out, daytime_hold2_out, daytime_hold3_out, daytime_hold4_out, and daytime_hold5_out
$("#second_hold1_out, #second_hold2_out, #second_hold3_out, #second_hold4_out, #second_hold5_out").on("input", function () {
    // Call the updateDaytimeTotalOut function when any of these text boxes change
    updateDaytimeTotalOut_second();
});


    //*********************************
   


$(document).ready(function () {
    $('#StatusHistory').on('click', '.btn-success_s', function () {
        var buttonId = $(this).attr('id');
        var ref_id = getIdFromUrl();

        //console.log('Button ID: ' + buttonId);
        var request = {
            auto_id: $("#auto_id").val(),
            id_ref: ref_id,
            //date_of_action: $("#date_of_action").val(),
            //HH_date_of_action: $("#HH_date_of_action").val(),
            //MM_date_of_action: $("#MM_date_of_action").val(),
            //remarks_comments: $("#remarks_comments").val(),
            //to_HH_date_of_action: $("#to_HH_date_of_action").val(),
            //to_MM_date_of_action: $("#to_MM_date_of_action").val(),
            //report_date: $("#report_date").val(),
            //HH_report_date: $("#HH_report_date").val(),
            //MM_report_date: $("#MM_report_date").val(),
            id: buttonId
        };

        $.ajax({
            url: '/InBound/GetRemarksByID', // Replace with the actual URL
            data: JSON.stringify(request),
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                console.log(result);
                $("#remarks_comments").val(result.remarks_comments);
                $("#date_of_action").val(result.date_of_action);

                $("#HH_date_of_action").val(result.HH_date_of_action);
                $("#MM_date_of_action").val(result.MM_date_of_action);

                $("#to_HH_date_of_action").val(result.to_HH_date_of_action);
                $("#to_MM_date_of_action").val(result.to_MM_date_of_action);
                $("#addRemarks").hide();
                $("#myEditbtn").show();
                $("#myEditCancel").show();
                $("#remark_id").val(buttonId);
                $('.myReMarkDelete').show();
            },
            error: function (xhr, textStatus, errorThrown) {
                swal("Oops!", "An error occurred: " + errorThrown, "error");
            }
        });

    });
});
function getIdFromUrl() {
    // Get the current URL
    var url = window.location.href;

    // Remove fragment identifier if present
    var urlWithoutFragment = url.split('#')[0];

    // Find the last occurrence of "/"
    var lastSlashIndex = urlWithoutFragment.lastIndexOf("/");

    // Check if there's a match
    if (lastSlashIndex !== -1) {
        // Extract everything after the last "/"
        var id = urlWithoutFragment.substring(lastSlashIndex + 1);
        return id;
    } else {
        return null; // No match found
    }
}

function save() {
    //var res = validate();
    //if (res == false)
    //    return false;
    var ref_id = getIdFromUrl();
    var is_active = false;
    if ($('#is_active').is(':checked'))
        is_active = true;

    var request = {
        id: ref_id,
        auto_id: $("#auto_id").val(),
        id_ref: ref_id,
        vessel_name: $("#vessel_name").val(),
        flags: $("#flags").val(),
        type_of_cargo: $("#type_of_cargo").val(),
        gross_weight_of_cargo: $("#gross_weight_of_cargo").val(),
        qtt_name: $("#qtt_name").val(),
        readiness_tendered: $("#readiness_tendered").val(),
        readiness_accepted: $("#readiness_accepted").val(),
        arrival_at: $("#arrival_at").val(),
        pilot_on_board_arrival: $("#pilot_on_board_arrival").val(),
        dropped_anchor: $("#dropped_anchor").val(),
        draft_on_arrival: $("#draft_on_arrival").val(),
        bunker_rob_on_arrival: $("#bunker_rob_on_arrival").val(),
        commenced_discharge_cargo: $("#commenced_discharge_cargo").val(),
        completed_discharge_cargo: $("#completed_discharge_cargo").val(),
        lat: $("#lat").val(),
        longt: $("#longt").val(),
        created_by: $("#created_by").val(),
        updated_by: $("#updated_by").val(),
        updated_date: $("#updated_date").val(),
        created_date: $("#created_date").val(),
        is_active: is_active,
        EAT_HH_pilot_on_board_arrival: $("#EAT_HH_pilot_on_board_arrival").val(),
        EAT_MM_pilot_on_board_arrival: $("#EAT_MM_pilot_on_board_arrival").val(),
        EAT_HH_dropped_anchor: $("#EAT_HH_dropped_anchor").val(),
        EAT_MM_dropped_anchor: $("#EAT_MM_dropped_anchor").val(),
        EAT_HH_commenced_discharge_cargo: $("#EAT_HH_commenced_discharge_cargo").val(),
        EAT_MM_commenced_discharge_cargo: $("#EAT_MM_commenced_discharge_cargo").val(),
        EAT_HH_completed_discharge_cargo: $("#EAT_HH_completed_discharge_cargo").val(),
        EAT_MM_completed_discharge_cargo: $("#EAT_MM_completed_discharge_cargo").val(),
        EAT_HH_pilot_on_board_arrival: $("#EAT_HH_pilot_on_board_arrival").val(),
        EAT_MM_pilot_on_board_arrival: $("#EAT_MM_pilot_on_board_arrival").val(),

    };

    $.ajax({
        url: '/InBound/Save_ArrivalSailingCondition', // Replace with the actual URL
        data: JSON.stringify(request),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (data) {
            swal(data.Title, data.Message, data.Type).then(function () {
                //loadData();
                $("#modal-default").modal('hide');
            });
        },
        error: function (xhr, textStatus, errorThrown) {
            swal("Oops!", "An error occurred: " + errorThrown, "error");
        }
    });
}
//$("#btnsaves").click(function () {
//    save();
//})
$("#myEditbtn").click(function () {
    alert('d');
    update();
})

$('body').on('click', '#btnApprove', function () {
    swal({
        title: "Are you sure?",
        text: "Once Approved, you will not be able to change.",
        icon: "warning",
        buttons: true,
        dangerMode: true,
    })
        .then((willDelete) => {
            if (willDelete) {
                var ref_id = getIdFromUrl();
                var request = {
                    auto_id: $("#auto_id").val(),
                    id: ref_id
                }
                $.ajax({
                    url: '/InBound/Approved_Arrival', // Replace with the actual URL
                    data: JSON.stringify(request),
                    type: "POST",
                    contentType: "application/json;charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        swal(data.Title, data.Message, data.Type).then(function () {
                            $('#btnPrint').show();
                        });
                        //swal(data.Title, data.Message, data.Type).then(function () {
                        //   // window.location.href = '/InBound/ArrivalCondition';
                        //    location.reload(true);
                        //});
                    },
                    error: function (xhr, textStatus, errorThrown) {
                        swal("Oops!", "An error occurred: " + errorThrown, "error");
                    }
                });
            }
        });
})

$('body').on('click', '#btnPrint', function () {
    var urlParts = window.location.href.split('/');
    var guidFromUrl = urlParts[urlParts.length - 1];
    if (guidFromUrl.endsWith('#')) {
        guidFromUrl = guidFromUrl.slice(0, -1); // Remove the last character
    }

    var id = guidFromUrl;
    var auto_id = $("#auto_id").val();
    ReportManagerApproved.LoadReportApproved(id, auto_id);
});

var ReportManagerApproved = {
    LoadReportApproved: function (id, auto_id) {
        var serviceUrl = "/InBound/Arrival_Approval_Report";
        ReportManagerApproved.GetReportApproved(serviceUrl, id, auto_id); // Pass 'id' here
    },
    GetReportApproved: function (serviceUrl, id, auto_id) { // Add 'id' as a parameter
        var jsonParams = {
            id: id,
            auto_id: auto_id
        };

        $.ajax({
            url: serviceUrl,
            data: JSON.stringify(jsonParams),
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (data) {
                window.open("/Reports/Arrival_Approval_Report.aspx?id_ref=" + data.id_ref, "_newtab");
            },
            error: function (xhr, status, error) {
                ReportManagerApproved.onFailed(error);
            }
        });
    },
    onFailed: function (errorMessage) {
        alert("Error: " + errorMessage);
    }
};
$('body').on('click', '.mybtn', function () {
    var selectTime;
    if ($('#daytime').is(':checked')) {
        selectTime = "d"
    }
    else if ($('#firstHN').is(':checked')) {
        selectTime = "f";
    }
    else if ($('#secondHN').is(':checked')) {
        selectTime = "s";
    }
    var ref_id = getIdFromUrl();
    var cargoStatus = {
        selectTime: selectTime,
        from_HH_cargo_status_daytime: parseInt($("#from_HH_cargo_status_daytime").val()),
        to_MM_cargo_status_daytime: parseInt($("#to_MM_cargo_status_daytime").val()),

        from_HH_cargo_status_first: parseInt($("#from_HH_cargo_status_first").val()),
        to_MM_cargo_status_first: parseInt($("#to_MM_cargo_status_first").val()),

        gang: $("#gang").val(),
        hold: $("#hold").val(),
        total_out: $("#total_out").val(),
        id_ref: ref_id,
        auto_id: $("#auto_id").val(),
    };

    $.ajax({
        url: '/InBound/Save_CargoDayStatus',
        type: 'POST',
        data: JSON.stringify(cargoStatus),
        contentType: 'application/json',
        success: function (data) {
            swal(data.Title, data.Message, data.Type).then(function () {
                // Your success logic here
                loadData();
            });
        },
        error: function (errorMessage) {
            swal("Oops!", errorMessage, "failure");
        }
    });
});
function loadData() {
    $.get("/InBound/GetGargoByID", { auto_id: $("#auto_id").val() }, function (data) {
        $('#tbl_InBoundManiFest').DataTable({
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
                        return '<input type="hidden" value=' + data + '> <a href="#' + data + '" class="btnEdit" style="margin-right: 5px;"><i class="fa fa-edit"></i></a>| &nbsp <a href="#" class="btnDelete" style="margin-right: 5px;"><i class="fa fa-trash"></i></a>';
                    },
                },
                { 'data': 'main' },
                { 'data': 'gang' },
                { 'data': 'hold' },
                { 'data': 'total_out' },
                //{ 'data': 'marks_and_nos' },
                //{ 'data': 'quantity' },
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

$('body').on('click', '.btnEdit', function () {
    $.get("/InBound/Edit_CargoStatusDay", { id: $(this).closest('tr').find('td:eq(0) input').val() }, function (data) {
        $("#from_HH_cargo_status_daytime").val(data.from_HH_cargo_status_daytime);
        $("#to_MM_cargo_status_daytime").val(data.to_MM_cargo_status_daytime);
        $("#from_HH_cargo_status_first").val(data.from_HH_cargo_status_first);
        $("#to_MM_cargo_status_first").val(data.to_MM_cargo_status_first);
        $("#gang").val(data.gang);
        $("#hold").val(data.hold);
        $("#total_out").val(data.total_out);
        $("#id").val(data.id);
        if (data.selectTime == 'd') {
            $('#daytime').prop('checked', true);
        }
        else if (data.selectTime == 'f') {
            $('#firstHN').prop('checked', false);
        }
        if (data.selectTime == 's') {
            $('#secondHN').prop('checked', false);
        }
        $("#mybtn").hide();
        $(".mybtn_Edit").show();
        $(".mybtn_Edit_Cancel").show();
    });
});
$('body').on('click', '.mybtn_Edit_Cancel', function () {
    $("#mybtn").show();
    $(".mybtn_Edit").hide();
    $(".mybtn_Edit_Cancel").hide();

    $("#from_HH_cargo_status_daytime").val('0');
    $("#to_MM_cargo_status_daytime").val('');
    $("#from_HH_cargo_status_first").val('');
    $("#to_MM_cargo_status_first").val('');
    $("#gang").val('');
    $("#hold").val('');
    $("#total_out").val('');
})
$('body').on('click', '.mybtn_Edit', function () {
    //var res = savemanifest();
    //var isChecked = $("#is_cleanonBoard").prop("checked");
    //if (isChecked) {
    //    isChecked = true;
    //} else {
    //    isChecked = false;
    //}
    var selectTime;
    if ($('#daytime').is(':checked')) {
        selectTime = "d"
    }
    else if ($('#firstHN').is(':checked')) {
        selectTime = "f";
    }
    else if ($('#secondHN').is(':checked')) {
        selectTime = "s";
    }

    var projectRequirement = new FormData();
    projectRequirement.append("id", $("#id").val());
    projectRequirement.append("auto_id", $("#auto_id").val());
    projectRequirement.append("from_HH_cargo_status_daytime", $("#from_HH_cargo_status_daytime").val());
    projectRequirement.append("to_MM_cargo_status_daytime", $("#to_MM_cargo_status_daytime").val());
    projectRequirement.append("from_HH_cargo_status_first", $("#from_HH_cargo_status_first").val());
    projectRequirement.append("to_MM_cargo_status_first", $("#to_MM_cargo_status_first").val());
    projectRequirement.append("gang", $("#gang").val());
    projectRequirement.append("hold", $("#hold").val());
    projectRequirement.append("total_out", $("#total_out").val());
    projectRequirement.append("selectTime", selectTime);

    $.ajax({
        url: '/InBound/UpdateCargoStatusDay',
        type: 'POST',
        data: projectRequirement,
        processData: false,  // tell jQuery not to process the data
        contentType: false,  // tell jQuery not to set contentType
        success: function (result) {
            swal(result.Title, result.Message, result.Type).then(function () {
                loadData();
                $("#mybtn").show();
                $(".mybtn_Edit").hide();
                $(".mybtn_Edit_Cancel").hide();
            });
        },
        failure: function (errorMessage) {
            swal("Oops!", errorMessage, "failure");
        }
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
                $.get("/InBound/Delete_CargoStatusDay", { id: $(this).closest('tr').find('td:eq(0) input').val() }, function (data) {
                    swal(data.Title, data.Message, data.Type).then(function () {
                        loadData();
                    });
                });
            }
        });
});
$("#addRemarks").click(function () {
    save_remarks();
})

function update() {
    var ref_id = getIdFromUrl();

    var request = {
        auto_id: $("#auto_id").val(),
        id_ref: ref_id,
        date_of_action: $("#date_of_action").val(),
        HH_date_of_action: $("#HH_date_of_action").val(),
        MM_date_of_action: $("#MM_date_of_action").val(),
        remarks_comments: $("#remarks_comments").val(),
        to_HH_date_of_action: $("#to_HH_date_of_action").val(),
        to_MM_date_of_action: $("#to_MM_date_of_action").val(),
        report_date: $("#report_date").val(),
        HH_report_date: $("#HH_report_date").val(),
        MM_report_date: $("#MM_report_date").val(),
        id: $("#remark_id").val()

    };
    console.log(request);

    $.ajax({
        url: '/InBound/UpdateRemarks', // Replace with the actual URL
        data: JSON.stringify(request),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (data) {
            swal(data.Title, data.Message, data.Type).then(function () {
                $('#btnPrint').show();
                $('#myEditbtn').hide();
                $('#myEditCancel').show();
                $('#addRemarks').show();
                $('#remarks_comments').val('');
                $('#date_of_action').val('');
                $('.myReMarkDelete').hide();
                
                GetRemarksDate();
            });
        },
        error: function (xhr, textStatus, errorThrown) {
            swal("Oops!", "An error occurred: " + errorThrown, "error");
        }
    });
}
function save_remarks() {
    var ref_id = getIdFromUrl();

    var request = {
        auto_id: $("#auto_id").val(),
        id_ref: ref_id,
        date_of_action: $("#date_of_action").val(),
        HH_date_of_action: $("#HH_date_of_action").val(),
        MM_date_of_action: $("#MM_date_of_action").val(),
        remarks_comments: $("#remarks_comments").val(),
        to_HH_date_of_action: $("#to_HH_date_of_action").val(),
        to_MM_date_of_action: $("#to_MM_date_of_action").val(),

        report_date: $("#report_date").val(),
        HH_report_date: $("#HH_report_date").val(),
        MM_report_date: $("#MM_report_date").val()
    };

    $.ajax({
        url: '/InBound/Save_Remarks', // Replace with the actual URL
        data: JSON.stringify(request),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (data) {
            swal(data.Title, data.Message, data.Type).then(function () {
                GetRemarksDate();
                $('.myReMarkDelete').hide();
            });
        },
        error: function (xhr, textStatus, errorThrown) {
            swal("Oops!", "An error occurred: " + errorThrown, "error");
        }
    });
}


function GetRemarksDate() {

    var ref_id = getIdFromUrl();

    var request = {
        auto_id: $("#auto_id").val(),
        id: ref_id,
      report_date: $("#report_date").val()

    }
    $.ajax({
        url: "/InBound/GetRemarksData",
        data: JSON.stringify(request),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            var htmlCode = result.html;
            $('#StatusHistory').html(htmlCode);
        }
    });
}

function save_daily_report() {
    var res = validate();
    if (res == false)
        return false;

    var id = getIdFromUrl();
    //var is_active = $('#is_active').is(':checked');

    var request = {
        id: id,
        auto_id: $("#auto_id").val(),
        vessel_name: $("#vessel_name").val(),
        flags: $("#flags").val(),
        discharge_port_name: $("#type_of_cargo").val(),
        lat: $("#lat").val(),
        longt: $("#longt").val(),
        weather: $("#weather").val(),
        report_date: $("#report_date").val(),
        HH_report_date: $("#HH_report_date").val(),
        MM_report_date: $("#MM_report_date").val(),

        from_daily_discharge_cargo: $("#from_daily_discharge_cargo").val(),
        from_HH_daily_discharge_cargo: $("#from_HH_daily_discharge_cargo").val(),
        from_MM_daily_discharge_cargo: $("#from_MM_daily_discharge_cargo").val(),
        to_daily_discharge_cargo: $("#to_daily_discharge_cargo").val(),
        to_HH_daily_discharge_cargo: $("#to_HH_daily_discharge_cargo").val(),
        to_MM_daily_discharge_cargo: $("#to_MM_daily_discharge_cargo").val(),

        fwt: $("#fwt").val(),
        ft: $("#ft").val(),
        fo: $("#fo").val(),
        doo: $("#doo").val(),
        fw: $("#fw").val(),

        // from here
        loading_text: $("#loading_text").val(),
        loading_text_date: $("#loading_text_date").val(),
        from_HH_loading_text: $("#from_HH_loading_text").val(),
        to_MM_loading_text: $("#to_MM_loading_text").val(),
        loading_text_until: $("#loading_text_until").val(),

        comp_loading: $("#comp_loading").val(),
        from_HH_comp_loading: $("#from_HH_comp_loading").val(),
        from_MM_comp_loading: $("#from_MM_comp_loading").val(),
        to_comp_loading: $("#to_comp_loading").val(),
        to_HH_comp_loading: $("#to_HH_comp_loading").val(),
        to_MM_comp_loading: $("#to_MM_comp_loading").val(),

        comm_loading: $("#comm_loading").val(),
        from_HH_comm_loading: $("#from_HH_comm_loading").val(),
        from_MM_comm_loading: $("#from_MM_comm_loading").val(),
        to_comm_loading: $("#to_comm_loading").val(),
        to_HH_comm_loading: $("#to_HH_comm_loading").val(),
        to_MM_comm_loading: $("#to_MM_comm_loading").val(),

        hold_1_total: $("#hold_1_total").val(),
        hold_2_total: $("#hold_2_total").val(),
        hold_3_total: $("#hold_3_total").val(),
        hold_4_total: $("#hold_4_total").val(),
        hold_5_total: $("#hold_5_total").val(),

        from_HH_daytime: $("#from_HH_daytime").val(),
        from_MM_daytime: $("#from_MM_daytime").val(),
        to_HH_daytime: $("#to_HH_daytime").val(),
        to_MM_daytime: $("#to_MM_daytime").val(),

        gang_daytime: $("#gang_daytime").val(),
        daytime_hold1_out: $("#daytime_hold1_out").val(),
        daytime_hold2_out: $("#daytime_hold2_out").val(),
        daytime_hold3_out: $("#daytime_hold3_out").val(),
        daytime_hold4_out: $("#daytime_hold4_out").val(),
        daytime_hold5_out: $("#daytime_hold5_out").val(),
        daytime_total_out: $("#daytime_total_out").val(),

        from_HH_first: $("#from_HH_first").val(),
        from_MM_first: $("#from_MM_first").val(),
        to_HH_first: $("#to_HH_first").val(),
        to_MM_first: $("#to_MM_first").val(),

        gang_first: $("#gang_first").val(),
        first_hold1_out: $("#first_hold1_out").val(),
        first_hold2_out: $("#first_hold2_out").val(),
        first_hold3_out: $("#first_hold3_out").val(),
        first_hold4_out: $("#first_hold4_out").val(),
        first_hold5_out: $("#first_hold5_out").val(),
        first_total_out: $("#first_total_out").val(),

        from_HH_second: $("#from_HH_second").val(),
        from_MM_second: $("#from_MM_second").val(),
        to_HH_second: $("#to_HH_second").val(),
        to_MM_second: $("#to_MM_second").val(),

        gang_second: $("#gang_second").val(),
        second_hold1_out: $("#second_hold1_out").val(),
        second_hold2_out: $("#second_hold2_out").val(),
        second_hold3_out: $("#second_hold3_out").val(),
        second_hold4_out: $("#second_hold4_out").val(),
        second_hold5_out: $("#second_hold5_out").val(),
        second_total_out: $("#second_total_out").val(),

        gang_total: $("#gang_total").val(),
        total_hold1_out: $("#total_hold1_out").val(),
        total_hold2_out: $("#total_hold2_out").val(),
        total_hold3_out: $("#total_hold3_out").val(),
        total_hold4_out: $("#total_hold4_out").val(),
        total_hold5_out: $("#total_hold5_out").val(),
        total_total: $("#total_total").val(),

        gang_previous: $("#gang_previous").val(),
        previous_hold1_out: $("#previous_hold1_out").val(),
        previous_hold2_out: $("#previous_hold2_out").val(),
        previous_hold3_out: $("#previous_hold3_out").val(),
        previous_hold4_out: $("#previous_hold4_out").val(),
        previous_hold5_out: $("#previous_hold5_out").val(),
        previous_total: $("#previous_total").val(),

        gang_grand_total: $("#gang_grand_total").val(),
        grand_hold1_out: $("#grand_hold1_out").val(),
        grand_hold2_out: $("#grand_hold2_out").val(),
        grand_hold3_out: $("#grand_hold3_out").val(),
        grand_hold4_out: $("#grand_hold4_out").val(),
        grand_hold5_out: $("#grand_hold5_out").val(),
        grand_total: $("#grand_total").val(),

        balance_cargo_hold1: $("#balance_cargo_hold1").val(),
        balance_cargo_hold2: $("#balance_cargo_hold2").val(),
        balance_cargo_hold3: $("#balance_cargo_hold3").val(),
        balance_cargo_hold4: $("#balance_cargo_hold4").val(),
        balance_cargo_hold5: $("#balance_cargo_hold5").val(),
        balance_total: $("#balance_total").val(),

        working_time: $("#working_time").val(),
        working_through: $("#working_through").val()

    };

    $.ajax({
        url: '/InBound/UpdateDailyReport', // Replace with the actual URL
        data: JSON.stringify(request),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (data) {
            swal(data.Title, data.Message, data.Type).then(function () {
                
            });
        },
        error: function (xhr, textStatus, errorThrown) {
            swal("Oops!", "An error occurred: " + errorThrown, "error");
        }
    });
}
$(".btnSave").click(function () {
    save_daily_report();
})

function validate() {
    var isValid = true;
    if ($('#report_date').val().trim() == "") {
        $('#report_date').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#report_date').css('border-color', 'lightgrey');
    }
    if ($('#HH_report_date').val().trim() == "") {
        $('#HH_report_date').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#HH_report_date').css('border-color', 'lightgrey');
    }
    if ($('#MM_report_date').val().trim() == "") {
        $('#MM_report_date').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#MM_report_date').css('border-color', 'lightgrey');
    }
    if ($('#from_daily_discharge_cargo').val().trim() == "") {
        $('#from_daily_discharge_cargo').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#from_daily_discharge_cargo').css('border-color', 'lightgrey');
    }

    if ($('#from_HH_daily_discharge_cargo').val().trim() == "") {
        $('#from_HH_daily_discharge_cargo').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#from_HH_daily_discharge_cargo').css('border-color', 'lightgrey');
    }
    if ($('#from_MM_daily_discharge_cargo').val().trim() == "") {
        $('#from_MM_daily_discharge_cargo').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#from_MM_daily_discharge_cargo').css('border-color', 'lightgrey');
    }

    if ($('#to_daily_discharge_cargo').val().trim() == "") {
        $('#to_daily_discharge_cargo').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#to_daily_discharge_cargo').css('border-color', 'lightgrey');
    }

    if ($('#to_HH_daily_discharge_cargo').val().trim() == "") {
        $('#to_HH_daily_discharge_cargo').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#to_HH_daily_discharge_cargo').css('border-color', 'lightgrey');
    }
    if ($('#to_MM_daily_discharge_cargo').val().trim() == "") {
        $('#to_MM_daily_discharge_cargo').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#to_MM_daily_discharge_cargo').css('border-color', 'lightgrey');
    }



    if ($('#from_HH_daytime').val().trim() == "") {
        $('#from_HH_daytime').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#from_HH_daytime').css('border-color', 'lightgrey');
    }

    if ($('#from_MM_daytime').val().trim() == "") {
        $('#from_MM_daytime').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#from_MM_daytime').css('border-color', 'lightgrey');
    }


    if ($('#to_HH_daytime').val().trim() == "") {
        $('#to_HH_daytime').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#to_HH_daytime').css('border-color', 'lightgrey');
    }


    if ($('#to_MM_daytime').val().trim() == "") {
        $('#to_MM_daytime').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#to_MM_daytime').css('border-color', 'lightgrey');
    }
    if ($('#gang_daytime').val().trim() == "") {
        $('#gang_daytime').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#gang_daytime').css('border-color', 'lightgrey');
    }



    if ($('#daytime_hold1_out').val().trim() == "") {
        $('#daytime_hold1_out').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#daytime_hold1_out').css('border-color', 'lightgrey');
    }
    if ($('#daytime_hold2_out').val().trim() == "") {
        $('#daytime_hold2_out').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#daytime_hold2_out').css('border-color', 'lightgrey');
    }

    if ($('#daytime_hold3_out').val().trim() == "") {
        $('#daytime_hold3_out').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#daytime_hold3_out').css('border-color', 'lightgrey');
    }

    if ($('#daytime_hold4_out').val().trim() == "") {
        $('#daytime_hold4_out').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#daytime_hold4_out').css('border-color', 'lightgrey');
    }

    if ($('#daytime_hold5_out').val().trim() == "") {
        $('#daytime_hold5_out').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#daytime_hold5_out').css('border-color', 'lightgrey');
    }

    if ($('#daytime_total_out').val().trim() == "") {
        $('#daytime_total_out').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#daytime_total_out').css('border-color', 'lightgrey');
    }

    // 
    if ($('#loading_text').val().trim() == "") {
        $('#loading_text').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#loading_text').css('border-color', 'lightgrey');
    }

    if ($('#from_HH_loading_text').val().trim() == "") {
        $('#from_HH_loading_text').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#from_HH_loading_text').css('border-color', 'lightgrey');
    }

    if ($('#to_MM_loading_text').val().trim() == "") {
        $('#to_MM_loading_text').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#to_MM_loading_text').css('border-color', 'lightgrey');
    }


    if ($('#loading_text_until').val().trim() == "") {
        $('#loading_text_until').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#loading_text_until').css('border-color', 'lightgrey');
    }

    if ($('#loading_text_date').val().trim() == "") {
        $('#loading_text_date').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#loading_text_date').css('border-color', 'lightgrey');
    }


    return isValid;
}

$("#myEditbtn").click(function () {
    $("#addRemarks").show();
    $("#myEditbtn").show();
    $("#myEditCancel").show();

})

$('body').on('click', '.myReMarkDelete', function () {
    swal({
        title: "Are you sure?",
        text: "Once deleted, you will not be able to recover this role",
        icon: "warning",
        buttons: true,
        dangerMode: true,
    })
        .then((willDelete) => {
            if (willDelete) {
                $.get("/InBound/DeleteRemarks", { id: $("#remark_id").val() }, function (data) {
                    swal(data.Title, data.Message, data.Type).then(function () {
                        $('#btnPrint').show();
                        $('#myEditbtn').hide();
                        $('#myEditCancel').show();
                        $('#addRemarks').show();
                        $('#remarks_comments').val('');
                        $('#date_of_action').val('');
                        $('.myReMarkDelete').hide();
                        GetRemarksDate();
                    });
                });
            }
        });
});


