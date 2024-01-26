
$(document).ready(function () {
    loadData();
    SetPermission();
})

function SetPermission() {
    if ($('#CanUpdate').val() == 'true') {
       
    }
    else {
        $(".btnsave").hide();
    }
    if ($('#CanDelete').val() == 'true') {

    }
    else {

    }
    if ($('#CanVerify').val() == 'true') {

    }
    else {

    }

}

function loadData() {
    $.get("/InBound/GetAll", function (data) {
        //alert($("#CanUpdate").val());
        //var edit = JSON.parse($('#CanUpdate').val());
        //alert(edit);

        $('#tbl_InBound').DataTable({
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
                        //return '<input type="hidden" value=' + data + '> <a href="#" class="btnEdit" style="margin-right: 5px;"><i class="fa fa-edit"></i></a>|&nbsp <a href="#" class="btnDelete" style="margin-right: 5px;"><i class="fa fa-trash"></i></a>|<a>  <div class="margin"> <div class="btn-group"> <button type="button" class="btn btn-success dropdown-toggle" data-toggle="dropdown" aria-expanded="false"><span class="sr-only">Toggle Dropdown</span></button> <div class="dropdown-menu" role="menu" style=""><a class="dropdown-item" href="#">Action</a><a class="dropdown-item" href="#">Another action</a><a class="dropdown-item" href="#">Something else here</a> <div class="dropdown-divider"></div><a class="dropdown-item" href="#">Separated link</a></div></div></div >';
                        var editLink = '';
                        if ($('#CanUpdate').val() == 'true') {
                            editLink = '&nbsp <a href="/InBound/Edit/' + data + '" class="btnEdit" style="margin-right: 5px;"><i class="fa fa-edit"></i></a>|&nbsp';
                        }

                        var DeleteLink = '';
                        if ($('#CanDelete').val() == 'true') {
                            DeleteLink = '&nbsp <a href="#" class="btnDelete" style="margin-right: 5px;"><i class="fa fa-trash"></i></a>|&nbsp';
                        }

                        //var ApprovalLink = '';
                        //if ($('#CanVerify').val() == 'true') {
                        //    ApprovalLink = '<a href="/InBound/Edit/' + data + '" class="btnApproval" style="margin-right: 5px;"><i class="fa fa-check-circle"></i></a>|&nbsp';
                        //}
                        //else {

                        //}

                        return '<input type="hidden" value=' + data + '> ' +
                            editLink +
                            //ApprovalLink+
                          //'<a href="#" class="btnApproval" style="margin-right: 5px;"><i class="fa fa-check-circle"></i></a>|&nbsp*/' +
                            DeleteLink 
                        /*'<a href="#" class="btnDelete" style="margin-right: 5px;"><i class="fa fa-trash"></i></a>' +*/
                            //'<div class="btn-group" style="float: left;margin-top: 2px;">' +
                            //'<button type="button" class="btn btn-info dropdown-toggle dropdown-icon" data-toggle="dropdown" aria-expanded="false">' +
                            //'<span class="sr-only">Toggle Dropdown</span></button>' +
                            //'<div class="dropdown-menu" style="border-color:#dcafaf; margin-left: 25px;padding-left: 7px; BACKGROUND-COLOR: antiquewhite;" role="menu">' +
                            //'<a class="dropdown-item" id="btnLoadReport" href="#' + data + '">Details</a></br>' +
                            //'<a class="dropdown-item" href="/InBound/Manifest/' + data + '" target="_blank">Cargo Manifest</a></br>' +
                            //'<div class="dropdown-divider"></div>' +
                            //'</div></div>';
                    },
                },
                {
                    "data": "is_approved",
                    "orderable": false,
                    "visible": function (data, type, row, meta) {
                        return data === true; // Hide if is_approved is false
                    },
                    "render": function (data, type, row) {
                        if (data == false)
                            return '<div class="approved">Not Approved</div>';
                        else
                            return '<div class="btn-group" style="float: left;margin-top: 2px;">' +
                                '<button type="button" class="btn btn-info dropdown-toggle dropdown-icon" data-toggle="dropdown" aria-expanded="false">' +
                                '<span class="sr-only">Toggle Dropdown</span>Approved</button>' +
                                '<div class="dropdown-menu" style="border-color:#dcafaf; margin-left: 25px;padding-left: 7px; BACKGROUND-COLOR: antiquewhite;" role="menu">' +
                                '<a class="dropdown-item" id="btnLoadReport" href="#' + row.id + '">Details</a></br>' +
                                '<a class="dropdown-item" id="btnManifest" href="#' + row.id + '">Cargo Manifest</a></br>' +
                                '<a class="dropdown-item" id="btnStowagePlan" href="/InBound/CreateStowagePlan/' + row.id + '">Cargo Stowage Plan</a></br>' +
                                '<a class="dropdown-item" id="btnArrivalCondition"   href="/InBound/ArrivalCondition/' + row.id + '">Arrival/Sailing </a></br>' +
                                '<a class="dropdown-item" id="btnDepartureCondition"   href="/InBound/DepartureCondition/' + row.id + '">Departure/Sailing </a></br>' +

                                '<a class="dropdown-item" id="btnDailyReport"   href="/InBound/DailyReport/' + row.id + '">Daily Report </a></br>' +
                                '<a class="dropdown-item" id="btnStatementofFact"   href="/InBound/StatementofFact/' + row.id + '">Statement of Fact </a></br>' +

                                '<a class="dropdown-item" id="btnManifest"   href="#"' + row.id + '">LIGHTER ALONGSIDE </a></br>' +


                                '<div class="dropdown-divider"></div>' +
                            '</div></div>';
                    },
                },
                { 'data': 'auto_id' },
                { 'data': 'EAT_Date' },
                { 'data': 'vessel' },
                { 'data': 'discharge_port_name' },
                { 'data': 'refNo' },
                { 'data': 'rcn' },
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
            "initComplete": function () {
                var api = this.api();

                // Add filter dropdown box to the footer for "requester" column
                var columnRequester = api.column(3); // "requester" column
                var footerRequester = $(columnRequester.footer());
                footerRequester.html('<label for="vessel"></label>');
                var selectFooterRequester = $('<select style="color:#666060;" id="vessel"></select>').appendTo(footerRequester);

                var uniqueValuesRequester = columnRequester.data().unique().sort().toArray();

                selectFooterRequester.append('<option value="">All</option>'); // Add "All" option on top

                $.each(uniqueValuesRequester, function (index, value) {
                    selectFooterRequester.append('<option value="' + value + '">' + value + '</option>');
                });

                selectFooterRequester.on('change', function () {
                    var val = $.fn.dataTable.util.escapeRegex($(this).val());
                    columnRequester.search(val ? '^' + val + '$' : '', true, false).draw();
                });

                var columnRequester = api.column(4); // "requester" column
                var footerRequester = $(columnRequester.footer());
                footerRequester.html('<label for="discharge_port_name"></label>');
                var selectFooterRequester = $('<select style="color:#666060;" id="discharge_port_name"></select>').appendTo(footerRequester);

                var uniqueValuesRequester = columnRequester.data().unique().sort().toArray();

                selectFooterRequester.append('<option value="">All</option>'); // Add "All" option on top

                $.each(uniqueValuesRequester, function (index, value) {
                    selectFooterRequester.append('<option value="' + value + '">' + value + '</option>');
                });

                selectFooterRequester.on('change', function () {
                    var val = $.fn.dataTable.util.escapeRegex($(this).val());
                    columnRequester.search(val ? '^' + val + '$' : '', true, false).draw();
                });

                var columnRequester = api.column(2); // "requester" column
                var footerRequester = $(columnRequester.footer());
                footerRequester.html('<label for="EAT_Date"></label>');
                var selectFooterRequester = $('<select style="color:#666060;" id="EAT_Date"></select>').appendTo(footerRequester);

                var uniqueValuesRequester = columnRequester.data().unique().sort().toArray();

                selectFooterRequester.append('<option value="">All</option>'); // Add "All" option on top

                $.each(uniqueValuesRequester, function (index, value) {
                    selectFooterRequester.append('<option value="' + value + '">' + value + '</option>');
                });

                selectFooterRequester.on('change', function () {
                    var val = $.fn.dataTable.util.escapeRegex($(this).val());
                    columnRequester.search(val ? '^' + val + '$' : '', true, false).draw();
                });

            },
        });
    })

}

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
                $.get("/InBound/Delete_InBound", { id: $(this).closest('tr').find('td:eq(0) input').val() }, function (data) {
                    swal(data.Title, data.Message, data.Type).then(function () {
                        loadData();
                    });
                });
            }
        });
});
$('body').on('click', '#btnLoadReport', function () {
    var id = $(this).closest('tr').find('td:eq(0) input').val();
    ReportManager.LoadReport(id);
});
var ReportManager = {
    LoadReport: function (id) {
        var serviceUrl = "/InBound/Details";
        ReportManager.GetReport(serviceUrl, id);
    },
    GetReport: function (serviceUrl, id) {
        var jsonParams = {
            id: id
        };

        $.ajax({
            url: serviceUrl,
            data: JSON.stringify(jsonParams),
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (data) {
                console.log(data);
                window.open("../Reports/Details.aspx?id_ref=" + data.id_ref + "", "_newtab");
            },
            error: function (xhr, status, error) {
                ReportManager.onFailed(error);
            }
        });
    },
    onFailed: function (errorMessage) {
        alert("Error: " + errorMessage);
    }
};

$('body').on('click', '#btnManifest', function () {
    var id = $(this).closest('tr').find('td:eq(0) input').val();
    ReportManagerManifest.LoadReportManifest(id);
});

var ReportManagerManifest = {
    LoadReportManifest: function (id) {
        var serviceUrl = "/InBound/Manifest";
        ReportManagerManifest.GetReportManifest(serviceUrl, id);
    },
    GetReportManifest: function (serviceUrl, id) {
        var jsonParams = {
            id: id
        };

        $.ajax({
            url: serviceUrl,
            data: JSON.stringify(jsonParams),
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (data) {
                window.open("../Reports/Manifest.aspx?id_ref=" + data.id_ref + "","_newtab");
            },
            error: function (xhr, status, error) {
                ReportManagerManifest.onFailed(error);
            }
        });
    },
    onFailed: function (errorMessage) {
        alert("Error: " + errorMessage);
    }
};

