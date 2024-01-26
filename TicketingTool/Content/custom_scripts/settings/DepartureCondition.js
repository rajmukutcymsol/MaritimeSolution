$(document).ready(function () {
    $('#pilot_on_board_departure').inputmask('99/99/9999', {
        placeholder: 'DD/MM/YYYY',
        alias: 'datetime',
        inputFormat: 'dd/mm/yyyy',
    });
    $('#departue_from').inputmask('99/99/9999', {
        placeholder: 'DD/MM/YYYY',
        alias: 'datetime',
        inputFormat: 'dd/mm/yyyy',
    });
})

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
    //alert($("#ETA_Next_Port_Date").val());
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
        pilot_on_board_departure: $("#pilot_on_board_departure").val(),
        departue_from: $("#departue_from").val(),
        draft_on_departure: $("#draft_on_departure").val(),
        bunker_rob_on_departure: $("#bunker_rob_on_departure").val(),
        bunker_fuel_oil: $("#bunker_fuel_oil").val(),
        bunker_diesel_oil: $("#bunker_diesel_oil").val(),
        bunker_fresh_water: $("#bunker_fresh_water").val(),
        bunker_eta_next_port: $("#bunker_eta_next_port").val(),
        other_watch_man: $("#other_watch_man").val(),
        other_police_man: $("#other_police_man").val(),
        other_cash_advance: $("#other_cash_advance").val(),


        fwd: $("#fwd").val(),
        aft: $("#aft").val(),
        fo: $("#fo").val(),
        doo: $("#doo").val(),
        fw: $("#fw").val(),

        //readiness_tendered: $("#readiness_tendered").val(),
        //readiness_accepted: $("#readiness_accepted").val(),
        //arrival_at: $("#arrival_at").val(),
        //pilot_on_board_arrival: $("#pilot_on_board_arrival").val(),
        //dropped_anchor: $("#dropped_anchor").val(),
        //draft_on_arrival: $("#draft_on_arrival").val(),
        //bunker_rob_on_arrival: $("#bunker_rob_on_arrival").val(),
        //commenced_discharge_cargo: $("#commenced_discharge_cargo").val(),
        //completed_discharge_cargo: $("#completed_discharge_cargo").val(),
        //lat: $("#lat").val(),
        //longt: $("#longt").val(),

        created_by: $("#created_by").val(),
        updated_by: $("#updated_by").val(),
        updated_date: $("#updated_date").val(),
        created_date: $("#created_date").val(),

        EAT_HH_pilot_on_board_departure: $("#EAT_HH_pilot_on_board_departure").val(),
        EAT_MM_pilot_on_board_departure: $("#EAT_MM_pilot_on_board_departure").val(),
        EAT_HH_departure_from: $("#EAT_HH_departure_from").val(),
        EAT_MM_departure_from: $("#EAT_MM_departure_from").val(),
        departue_from_port_name: $("#departue_from_port_name").val(),

        ETA_Next_Port_Date: $("#ETA_Next_Port_Date").val(),
        ETA_Next_Port_MM: $("#ETA_Next_Port_MM").val(),
        ETA_Next_Port_HH: $("#ETA_Next_Port_HH").val(),
        ETA_Next_Port_AMPM: $("#ETA_Next_Port_AMPM").val()

        //is_active: is_active,
        //EAT_MM_pilot_on_board_arrival: $("#EAT_MM_pilot_on_board_arrival").val(),
        //EAT_HH_dropped_anchor: $("#EAT_HH_dropped_anchor").val(),
        //EAT_MM_dropped_anchor: $("#EAT_MM_dropped_anchor").val(),
        //EAT_HH_commenced_discharge_cargo: $("#EAT_HH_commenced_discharge_cargo").val(),
        //EAT_MM_commenced_discharge_cargo: $("#EAT_MM_commenced_discharge_cargo").val(),
        //EAT_HH_completed_discharge_cargo: $("#EAT_HH_completed_discharge_cargo").val(),
        //EAT_MM_completed_discharge_cargo: $("#EAT_MM_completed_discharge_cargo").val(),
        //EAT_HH_pilot_on_board_arrival: $("#EAT_HH_pilot_on_board_arrival").val(),
        //EAT_MM_pilot_on_board_arrival: $("#EAT_MM_pilot_on_board_arrival").val(),
        ////pilot_on_board_departure: $("#pilot_on_board_departure").val(),
        //departue_from: $("#departue_from").val(),
        //draft_on_departure: $("#draft_on_departure").val(),
        //bunker_rob_on_departure: $("#bunker_rob_on_departure").val(),
        //bunker_fuel_oil: $("#bunker_fuel_oil").val(),
        //bunker_diesel_oil: $("#bunker_diesel_oil").val(),
        //bunker_fresh_water: $("#bunker_fresh_water").val(),
        //bunker_eta_next_port: $("#bunker_eta_next_port").val(),
        //other_watch_man: $("#other_watch_man").val(),
        //other_police_man: $("#other_police_man").val(),
        //other_cash_advance: $("#other_cash_advance").val(),
        // Add other properties as needed

    };
    console.log(request);
    $.ajax({
        url: '/InBound/Save_DepartureSailingCondition', // Replace with the actual URL
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
$("#btnsaves").click(function () {
    save();
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
        var serviceUrl = "/InBound/Departure_Approval_Report";
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
                window.open("/Reports/Departure_Approval_Report.aspx?id_ref=" + data.id_ref, "_newtab");
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
