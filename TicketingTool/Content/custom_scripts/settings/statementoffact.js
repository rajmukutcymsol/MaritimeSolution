$(document).ready(function () {
    $("#departure_date").datepicker({
        dateFormat: 'dd/mm/yy',
        changeMonth: true,
        changeYear: true
    });
    $("#ETA_Next_Port_Date").datepicker({
        dateFormat: 'dd/mm/yy',
        changeMonth: true,
        changeYear: true
    });
    GetRowData();
   
});

function GetRowData() {
    var urlParts = window.location.href.split('/');
    var guidFromUrl = urlParts[urlParts.length - 1];
    if (guidFromUrl.endsWith('#')) {
        guidFromUrl = guidFromUrl.slice(0, -1); // Remove the last character
    }

    var id = guidFromUrl;
    $.get("/InBound/GetRequirementDetail/" + id, function (data, status) {
        $("#auto_id").val(data.auto_id);
    });
    var auto = $("#auto_id").val();
    //alert(auto);
    GetData(auto, id);
}
function GetData(auto, id) {
    $.get("/InBound/GetsofData/" + id + "?auto_id=" + auto, function (data, status) {
        $("#departure_from_port").val(data.departure_from_port);
        $("#departure_date").val(data.departure_date);
        $("#departure_date_HH").val(data.departure_date_HH);
        $("#departure_date_MM").val(data.departure_date_MM);
        $("#ETA_Next_Port_Name").val(data.ETA_Next_Port_Name);
        $("#ETA_Next_Port_Date").val(data.ETA_Next_Port_Date);
        $("#ETA_Next_Port_MM").val(data.ETA_Next_Port_MM);
        $("#ETA_Next_Port_HH").val(data.ETA_Next_Port_HH);
        $("#ETA_Next_Port_AMPM").val(data.ETA_Next_Port_AMPM);
    });
}
$('body').on('click', '#btnPrint', function () {

    
    var urlParts = window.location.href.split('/');
    var guidFromUrl = urlParts[urlParts.length - 1];
    if (guidFromUrl.endsWith('#')) {
        guidFromUrl = guidFromUrl.slice(0, -1); // Remove the last character
    }

    var id = guidFromUrl;
    var auto_id = "";
    
    ReportManagerApproved.LoadReportApproved(id, auto_id);
});

var ReportManagerApproved = {
    LoadReportApproved: function (id, auto_id) {
        var serviceUrl = "/InBound/Statementof_Fact";
        ReportManagerApproved.GetReportApproved(serviceUrl, id, auto_id); // Pass 'id' here
    },
    GetReportApproved: function (serviceUrl, id, auto_id) { // Add 'id' as a parameter
        var jsonParams = {
            id: id,
            auto_id: auto_id
        };
        var request = {
            departure_from_port: $("#departure_from_port").val(),
            departure_date: $("#departure_date").val(),
            departure_date_HH: $("#departure_date_HH").val(),
            departure_date_MM: $("#departure_date_MM").val(),
            ETA_Next_Port_Name: $("#ETA_Next_Port_Name").val(),
            ETA_Next_Port_Date: $("#ETA_Next_Port_Date").val(),
            ETA_Next_Port_MM: $("#ETA_Next_Port_MM").val(),
            ETA_Next_Port_HH: $("#ETA_Next_Port_HH").val(),
            ETA_Next_Port_AMPM: $("#ETA_Next_Port_AMPM").val(),
            id_ref: id,
            auto_id: $("#auto_id").val()
        }
        console.log(request);
        $.ajax({
            url: '/Inbound/save_sof',
            data: JSON.stringify(request),
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (data) {
                swal(data.Title, data.Message, data.Type).then(function () {
                    //alert('h');
                });
            },
            failure: function (errorMessage) {
                swal("Oops!", errorMessage, "failure");
            }
        });

        $.ajax({
            url: serviceUrl,
            data: JSON.stringify(jsonParams),
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (data) {
                window.open("/Reports/Statement_of_Fact.aspx?id_ref=" + data.id_ref, "_newtab");
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