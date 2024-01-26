$(document).ready(function () {
    loadData();

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

function loadData() {
    var ref_id = getIdFromUrl();
    var auto_id = "";
    $.get("/InBound/GetAllDailyReports", { id_ref: ref_id}, function (data) {
        $('#tblNewRequest').DataTable({
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
                        return '<input type="hidden" value=' + data + '> <a href="/InBound/EditDailyReport/' + data + '" style="margin-right: 5px;"><i class="fa fa-edit"></i></a>| &nbsp <a href="#" class="btnDelete" style="margin-right: 5px;"><i class="fa fa-trash"></i></a> <button class="btnPrint" style="margin-right: 5px;"><i class="fa fa-print"></i></button>';
                    },
                },
                { 'data': 'report_date' },
                { 'data': 'vessel_name' },
                { 'data': 'flags' },

                {
                    data: "is_active",
                    "searchable": false,
                    "orderable": false,
                    "render": function (data, type, row) {
                        if (data == false)
                            return '<span class="label label-danger">In-Active</span>';
                        else
                            return '<span class="label label-success">Active</span>';
                    }

                }


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

$('body').on('click', '.btnPrint', function () {
    var closestTr = $(this).closest('tr');

    // Find the hidden input within the closest 'tr' and get its value
    var id = closestTr.find('input[type="hidden"]').val();

    // Your existing code here...
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
                window.open("/Reports/Daily_Report.aspx?id_ref=" + data.id_ref, "_blank");
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
                $.get("/Inbound/DeleteDailyReport", { id: $(this).closest('tr').find('td:eq(0) input').val() }, function (data) {
                    swal(data.Title, data.Message, data.Type).then(function () {
                        loadData();
                    });
                });
            }
        });
});