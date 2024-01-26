$('body').on('click', '#btnSearch', function () {
    $("#modal-default").modal('show');
});

$(document).ready(function () {
    LoadData();
    $('#request_status').change(function () {
       
        var selectedValues = $(this).val();
        //alert(selectedValues);
        if (selectedValues == "00000000-0000-0000-0000-000000000000") {
            LoadData();
        }
        else {
            var request = [];

            for (var i = 0; i < selectedValues.length; i++) {
                request.push(selectedValues[i]);
            }
            $.ajax({
                url: "/NewRequest/GetAll_Filter",
                data: JSON.stringify(request),
                type: "POST",
                contentType: "application/json;charset=utf-8",
                dataType: "json",
                success: function (data) {
                    //alert(sessionValue);
                    var isTru = JSON.parse($("#isAdminTruValue").data("admin-tru"));

                    if (isTru) {
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
                                        return '<input type="hidden" value=' + data + '> <a href="/NewRequest/Edit/' + data + '" style="margin-right: 5px;"><i class="fa fa-edit"></i></a>| &nbsp <a href="#" class="btnDelete" style="margin-right: 5px;"><i class="fa fa-trash"></i></a>';
                                    },
                                },
                                { 'data': 'auto_id' },
                                { 'data': 'project_name' },
                                { 'data': 'request_title' },

                                { 'data': 'date_of_request' },
                                { 'data': 'priority_name' },
                                { 'data': 'status_name' },
                                { 'data': 'requester' },// "orderable": false },
                                {
                                    data: "is_resolution_offered",
                                    "searchable": false,
                                    "orderable": false,
                                    "render": function (data, type, row) {
                                        if (data == "No")
                                            return '<span class="label label-danger">' + data + '</span>';
                                        else
                                            return '<span class="label label-success">' + data + '</span>';
                                    }

                                }


                            ],
                            "aoColumnDefs": [{
                                'bSortable': false,
                                'aTargets': [0],
                                "className": "text-center",
                            },
                            {
                                "targets": 7,
                                "className": "text-center",
                            }],
                            "initComplete": function () {
                                var api = this.api();

                                // Add filter dropdown box to the footer for "requester" column
                                var columnRequester = api.column(7); // "requester" column
                                var footerRequester = $(columnRequester.footer());
                                footerRequester.html('<label for="requester"></label>');
                                var selectFooterRequester = $('<select style="color:#666060;" id="requester"></select>').appendTo(footerRequester);

                                var uniqueValuesRequester = columnRequester.data().unique().sort().toArray();

                                selectFooterRequester.append('<option value="">All</option>'); // Add "All" option on top

                                $.each(uniqueValuesRequester, function (index, value) {
                                    selectFooterRequester.append('<option value="' + value + '">' + value + '</option>');
                                });

                                selectFooterRequester.on('change', function () {
                                    var val = $.fn.dataTable.util.escapeRegex($(this).val());
                                    columnRequester.search(val ? '^' + val + '$' : '', true, false).draw();
                                });

                                // Add filter dropdown box to the footer for "project_name" column
                                var columnProjectName = api.column(2); // "project_name" column
                                var footerProjectName = $(columnProjectName.footer());
                                footerProjectName.html('<label for="project_name"></label>');
                                var selectFooterProjectName = $('<select style="color:#666060;" id="project_name"></select>').appendTo(footerProjectName);

                                var uniqueValuesProjectName = columnProjectName.data().unique().sort().toArray();

                                selectFooterProjectName.append('<option value="">All</option>'); // Add "All" option on top

                                $.each(uniqueValuesProjectName, function (index, value) {
                                    selectFooterProjectName.append('<option value="' + value + '">' + value + '</option>');
                                });

                                selectFooterProjectName.on('change', function () {
                                    var val = $.fn.dataTable.util.escapeRegex($(this).val());
                                    columnProjectName.search(val ? '^' + val + '$' : '', true, false).draw();
                                });

                                // Add filter dropdown box to the footer for "priority_name" column
                                var columnPriorityName = api.column(5); // "priority_name" column
                                var footerPriorityName = $(columnPriorityName.footer());
                                footerPriorityName.html('<label for="priority_name"></label>');
                                var selectFooterPriorityName = $('<select style="color:#666060;" id="priority_name"></select>').appendTo(footerPriorityName);

                                var uniqueValuesPriorityName = columnPriorityName.data().unique().sort().toArray();

                                selectFooterPriorityName.append('<option value="">All</option>'); // Add "All" option on top

                                $.each(uniqueValuesPriorityName, function (index, value) {
                                    selectFooterPriorityName.append('<option value="' + value + '">' + value + '</option>');
                                });

                                selectFooterPriorityName.on('change', function () {
                                    var val = $.fn.dataTable.util.escapeRegex($(this).val());
                                    columnPriorityName.search(val ? '^' + val + '$' : '', true, false).draw();
                                });

                                // Add filter dropdown box to the footer for "status_name" column
                                var columnStatusName = api.column(6); // "status_name" column
                                var footerStatusName = $(columnStatusName.footer());
                                footerStatusName.html('<label for="status_name"></label>');
                                var selectFooterStatusName = $('<select style="color:#666060;" id="status_name"></select>').appendTo(footerStatusName);

                                var uniqueValuesStatusName = columnStatusName.data().unique().sort().toArray();

                                selectFooterStatusName.append('<option value="">All</option>'); // Add "All" option on top

                                $.each(uniqueValuesStatusName, function (index, value) {
                                    selectFooterStatusName.append('<option value="' + value + '">' + value + '</option>');
                                });

                                selectFooterStatusName.on('change', function () {
                                    var val = $.fn.dataTable.util.escapeRegex($(this).val());
                                    columnStatusName.search(val ? '^' + val + '$' : '', true, false).draw();
                                });
                            },


                        });
                    }
                    else {
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
                                        return '<input type="hidden" value=' + data + '> <a href="/NewRequest/OthersEdit/' + data + '" style="margin-right: 5px;"><i class="fa fa-edit"></i></a>';
                                        //return '<input type="hidden" value=' + data + '> <a href="/NewRequest/OthersEdit/' + data + '" style="margin-right: 5px;"><i class="fa fa-edit"></i></a>';
                                    },
                                },
                                { 'data': 'auto_id' },
                                { 'data': 'project_name' },
                                { 'data': 'request_title' },

                                { 'data': 'date_of_request' },
                                { 'data': 'priority_name' },
                                { 'data': 'status_name' },
                                { 'data': 'requester' },// "orderable": false },
                                {
                                    data: "is_resolution_offered",
                                    "searchable": false,
                                    "orderable": false,
                                    "render": function (data, type, row) {
                                        if (data == "No")
                                            return '<span class="label label-danger">' + data + '</span>';
                                        else
                                            return '<span class="label label-success">' + data + '</span>';
                                    }

                                }


                            ],
                            "aoColumnDefs": [{
                                'bSortable': false,
                                'aTargets': [0],
                                "className": "text-center",
                            },
                            {
                                "targets": 7,
                                "className": "text-center",
                            }],
                            "initComplete": function () {
                                var api = this.api();

                                // Add filter dropdown box to the footer for "requester" column
                                var columnRequester = api.column(7); // "requester" column
                                var footerRequester = $(columnRequester.footer());
                                footerRequester.html('<label for="requester"></label>');
                                var selectFooterRequester = $('<select style="color:#666060;" id="requester"></select>').appendTo(footerRequester);

                                var uniqueValuesRequester = columnRequester.data().unique().sort().toArray();

                                selectFooterRequester.append('<option value="">All</option>'); // Add "All" option on top

                                $.each(uniqueValuesRequester, function (index, value) {
                                    selectFooterRequester.append('<option value="' + value + '">' + value + '</option>');
                                });

                                selectFooterRequester.on('change', function () {
                                    var val = $.fn.dataTable.util.escapeRegex($(this).val());
                                    columnRequester.search(val ? '^' + val + '$' : '', true, false).draw();
                                });

                                // Add filter dropdown box to the footer for "project_name" column
                                var columnProjectName = api.column(2); // "project_name" column
                                var footerProjectName = $(columnProjectName.footer());
                                footerProjectName.html('<label for="project_name"></label>');
                                var selectFooterProjectName = $('<select style="color:#666060;" id="project_name"></select>').appendTo(footerProjectName);

                                var uniqueValuesProjectName = columnProjectName.data().unique().sort().toArray();

                                selectFooterProjectName.append('<option value="">All</option>'); // Add "All" option on top

                                $.each(uniqueValuesProjectName, function (index, value) {
                                    selectFooterProjectName.append('<option value="' + value + '">' + value + '</option>');
                                });

                                selectFooterProjectName.on('change', function () {
                                    var val = $.fn.dataTable.util.escapeRegex($(this).val());
                                    columnProjectName.search(val ? '^' + val + '$' : '', true, false).draw();
                                });

                                // Add filter dropdown box to the footer for "priority_name" column
                                var columnPriorityName = api.column(5); // "priority_name" column
                                var footerPriorityName = $(columnPriorityName.footer());
                                footerPriorityName.html('<label for="priority_name"></label>');
                                var selectFooterPriorityName = $('<select style="color:#666060;" id="priority_name"></select>').appendTo(footerPriorityName);

                                var uniqueValuesPriorityName = columnPriorityName.data().unique().sort().toArray();

                                selectFooterPriorityName.append('<option value="">All</option>'); // Add "All" option on top

                                $.each(uniqueValuesPriorityName, function (index, value) {
                                    selectFooterPriorityName.append('<option value="' + value + '">' + value + '</option>');
                                });

                                selectFooterPriorityName.on('change', function () {
                                    var val = $.fn.dataTable.util.escapeRegex($(this).val());
                                    columnPriorityName.search(val ? '^' + val + '$' : '', true, false).draw();
                                });

                                // Add filter dropdown box to the footer for "status_name" column
                                var columnStatusName = api.column(6); // "status_name" column
                                var footerStatusName = $(columnStatusName.footer());
                                footerStatusName.html('<label for="status_name"></label>');
                                var selectFooterStatusName = $('<select style="color:#666060;" id="status_name"></select>').appendTo(footerStatusName);

                                var uniqueValuesStatusName = columnStatusName.data().unique().sort().toArray();

                                selectFooterStatusName.append('<option value="">All</option>'); // Add "All" option on top

                                $.each(uniqueValuesStatusName, function (index, value) {
                                    selectFooterStatusName.append('<option value="' + value + '">' + value + '</option>');
                                });

                                selectFooterStatusName.on('change', function () {
                                    var val = $.fn.dataTable.util.escapeRegex($(this).val());
                                    columnStatusName.search(val ? '^' + val + '$' : '', true, false).draw();
                                });
                            },

                        });
                    }
                }
            });
        }
    });
});

var fromDate = new Date();
fromDate.setDate(fromDate.getDate() - 180);

$('#fromDate').datepicker({
    format: 'mm/dd/yyyy',
    "autoclose": true
}).datepicker("setDate", fromDate);


$('#toDate').datepicker({
    format: 'mm/dd/yyyy',
    "autoclose": true
}).datepicker("setDate", 'now');

$(document).ready(function () {
    LoadData();
})



$("body").on('click', '#btnSearchInModal', function () {
    LoadData();
    $("#modal-default").modal('hide');
})


$("#btnClear").click(function () {

    $('input[type="text"]').val('');
    $('textarea').val('');
    $("select").prop("selectedIndex", 0).val();
    $('#fromDate').datepicker({
        format: 'mm/dd/yyyy',
        "autoclose": true
    }).datepicker("setDate", fromDate);


    $('#toDate').datepicker({
        format: 'mm/dd/yyyy',
        "autoclose": true
    }).datepicker("setDate", 'now');

    LoadData();
})

function LoadData() {
    var request = {
        request_title: $("#request_title").val(),
        auto_id: $("#auto_id").val(),
        request_priority: $("#request_priority").val(),
        request_status: $("#request_status").val(),
        fromDate: $("#fromDate").val(),
        toDate: $("#toDate").val(),
        user_name: $('#sessionUserName').val(),
        access_role: $('#sessionValue').val()
    }


    $.ajax({
        url: "/NewRequest/GetAll",
        data: JSON.stringify(request),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (data) {
            //alert(sessionValue);
            var isTru = JSON.parse($("#isAdminTruValue").data("admin-tru"));

            if (isTru) {
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
                                return '<input type="hidden" value=' + data + '> <a href="/NewRequest/Edit/' + data + '" style="margin-right: 5px;"><i class="fa fa-edit"></i></a>| &nbsp <a href="#" class="btnDelete" style="margin-right: 5px;"><i class="fa fa-trash"></i></a>';
                            },
                        },
                        { 'data': 'auto_id' },
                        { 'data': 'project_name' },
                        { 'data': 'request_title' },

                        { 'data': 'date_of_request' },
                        { 'data': 'priority_name' },
                        { 'data': 'status_name' },
                        { 'data': 'requester' },// "orderable": false },
                        {
                            data: "is_resolution_offered",
                            "searchable": false,
                            "orderable": false,
                            "render": function (data, type, row) {
                                if (data == "No")
                                    return '<span class="label label-danger">' + data + '</span>';
                                else
                                    return '<span class="label label-success">' + data + '</span>';
                            }

                        }


                    ],
                    "aoColumnDefs": [{
                        'bSortable': false,
                        'aTargets': [0],
                        "className": "text-center",
                    },
                    {
                        "targets": 7,
                        "className": "text-center",
                        }],
                    "initComplete": function () {
                        var api = this.api();

                        // Add filter dropdown box to the footer for "requester" column
                        var columnRequester = api.column(7); // "requester" column
                        var footerRequester = $(columnRequester.footer());
                        footerRequester.html('<label for="requester"></label>');
                        var selectFooterRequester = $('<select style="color:#666060;" id="requester"></select>').appendTo(footerRequester);

                        var uniqueValuesRequester = columnRequester.data().unique().sort().toArray();

                        selectFooterRequester.append('<option value="">All</option>'); // Add "All" option on top

                        $.each(uniqueValuesRequester, function (index, value) {
                            selectFooterRequester.append('<option value="' + value + '">' + value + '</option>');
                        });

                        selectFooterRequester.on('change', function () {
                            var val = $.fn.dataTable.util.escapeRegex($(this).val());
                            columnRequester.search(val ? '^' + val + '$' : '', true, false).draw();
                        });

                        // Add filter dropdown box to the footer for "project_name" column
                        var columnProjectName = api.column(2); // "project_name" column
                        var footerProjectName = $(columnProjectName.footer());
                        footerProjectName.html('<label for="project_name"></label>');
                        var selectFooterProjectName = $('<select style="color:#666060;" id="project_name"></select>').appendTo(footerProjectName);

                        var uniqueValuesProjectName = columnProjectName.data().unique().sort().toArray();

                        selectFooterProjectName.append('<option value="">All</option>'); // Add "All" option on top

                        $.each(uniqueValuesProjectName, function (index, value) {
                            selectFooterProjectName.append('<option value="' + value + '">' + value + '</option>');
                        });

                        selectFooterProjectName.on('change', function () {
                            var val = $.fn.dataTable.util.escapeRegex($(this).val());
                            columnProjectName.search(val ? '^' + val + '$' : '', true, false).draw();
                        });

                        // Add filter dropdown box to the footer for "priority_name" column
                        var columnPriorityName = api.column(5); // "priority_name" column
                        var footerPriorityName = $(columnPriorityName.footer());
                        footerPriorityName.html('<label for="priority_name"></label>');
                        var selectFooterPriorityName = $('<select style="color:#666060;" id="priority_name"></select>').appendTo(footerPriorityName);

                        var uniqueValuesPriorityName = columnPriorityName.data().unique().sort().toArray();

                        selectFooterPriorityName.append('<option value="">All</option>'); // Add "All" option on top

                        $.each(uniqueValuesPriorityName, function (index, value) {
                            selectFooterPriorityName.append('<option value="' + value + '">' + value + '</option>');
                        });

                        selectFooterPriorityName.on('change', function () {
                            var val = $.fn.dataTable.util.escapeRegex($(this).val());
                            columnPriorityName.search(val ? '^' + val + '$' : '', true, false).draw();
                        });

                        // Add filter dropdown box to the footer for "status_name" column
                        var columnStatusName = api.column(6); // "status_name" column
                        var footerStatusName = $(columnStatusName.footer());
                        footerStatusName.html('<label for="status_name"></label>');
                        var selectFooterStatusName = $('<select style="color:#666060;" id="status_name"></select>').appendTo(footerStatusName);

                        var uniqueValuesStatusName = columnStatusName.data().unique().sort().toArray();

                        selectFooterStatusName.append('<option value="">All</option>'); // Add "All" option on top

                        $.each(uniqueValuesStatusName, function (index, value) {
                            selectFooterStatusName.append('<option value="' + value + '">' + value + '</option>');
                        });

                        selectFooterStatusName.on('change', function () {
                            var val = $.fn.dataTable.util.escapeRegex($(this).val());
                            columnStatusName.search(val ? '^' + val + '$' : '', true, false).draw();
                        });
                    },


                });
            }
            else {
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
                                return '<input type="hidden" value=' + data + '> <a href="/NewRequest/OthersEdit/' + data + '" style="margin-right: 5px;"><i class="fa fa-edit"></i></a>';
                                //return '<input type="hidden" value=' + data + '> <a href="/NewRequest/OthersEdit/' + data + '" style="margin-right: 5px;"><i class="fa fa-edit"></i></a>';
                            },
                        },
                        { 'data': 'auto_id' },
                        { 'data': 'project_name' },
                        { 'data': 'request_title' },
                       
                        { 'data': 'date_of_request' },
                        { 'data': 'priority_name' },
                        { 'data': 'status_name' },
                        { 'data': 'requester' },// "orderable": false },
                        {
                            data: "is_resolution_offered",
                            "searchable": false,
                            "orderable": false,
                            "render": function (data, type, row) {
                                if (data == "No")
                                    return '<span class="label label-danger">' + data + '</span>';
                                else
                                    return '<span class="label label-success">' + data + '</span>';
                            }

                        }


                    ],
                    "aoColumnDefs": [{
                        'bSortable': false,
                        'aTargets': [0],
                        "className": "text-center",
                    },
                    {
                        "targets": 7,
                        "className": "text-center",
                    }],
                    "initComplete": function () {
                        var api = this.api();

                        // Add filter dropdown box to the footer for "requester" column
                        var columnRequester = api.column(7); // "requester" column
                        var footerRequester = $(columnRequester.footer());
                        footerRequester.html('<label for="requester"></label>');
                        var selectFooterRequester = $('<select style="color:#666060;" id="requester"></select>').appendTo(footerRequester);

                        var uniqueValuesRequester = columnRequester.data().unique().sort().toArray();

                        selectFooterRequester.append('<option value="">All</option>'); // Add "All" option on top

                        $.each(uniqueValuesRequester, function (index, value) {
                            selectFooterRequester.append('<option value="' + value + '">' + value + '</option>');
                        });

                        selectFooterRequester.on('change', function () {
                            var val = $.fn.dataTable.util.escapeRegex($(this).val());
                            columnRequester.search(val ? '^' + val + '$' : '', true, false).draw();
                        });

                        // Add filter dropdown box to the footer for "project_name" column
                        var columnProjectName = api.column(2); // "project_name" column
                        var footerProjectName = $(columnProjectName.footer());
                        footerProjectName.html('<label for="project_name"></label>');
                        var selectFooterProjectName = $('<select style="color:#666060;" id="project_name"></select>').appendTo(footerProjectName);

                        var uniqueValuesProjectName = columnProjectName.data().unique().sort().toArray();

                        selectFooterProjectName.append('<option value="">All</option>'); // Add "All" option on top

                        $.each(uniqueValuesProjectName, function (index, value) {
                            selectFooterProjectName.append('<option value="' + value + '">' + value + '</option>');
                        });

                        selectFooterProjectName.on('change', function () {
                            var val = $.fn.dataTable.util.escapeRegex($(this).val());
                            columnProjectName.search(val ? '^' + val + '$' : '', true, false).draw();
                        });

                        // Add filter dropdown box to the footer for "priority_name" column
                        var columnPriorityName = api.column(5); // "priority_name" column
                        var footerPriorityName = $(columnPriorityName.footer());
                        footerPriorityName.html('<label for="priority_name"></label>');
                        var selectFooterPriorityName = $('<select style="color:#666060;" id="priority_name"></select>').appendTo(footerPriorityName);

                        var uniqueValuesPriorityName = columnPriorityName.data().unique().sort().toArray();

                        selectFooterPriorityName.append('<option value="">All</option>'); // Add "All" option on top

                        $.each(uniqueValuesPriorityName, function (index, value) {
                            selectFooterPriorityName.append('<option value="' + value + '">' + value + '</option>');
                        });

                        selectFooterPriorityName.on('change', function () {
                            var val = $.fn.dataTable.util.escapeRegex($(this).val());
                            columnPriorityName.search(val ? '^' + val + '$' : '', true, false).draw();
                        });

                        // Add filter dropdown box to the footer for "status_name" column
                        var columnStatusName = api.column(6); // "status_name" column
                        var footerStatusName = $(columnStatusName.footer());
                        footerStatusName.html('<label for="status_name"></label>');
                        var selectFooterStatusName = $('<select style="color:#666060;" id="status_name"></select>').appendTo(footerStatusName);

                        var uniqueValuesStatusName = columnStatusName.data().unique().sort().toArray();

                        selectFooterStatusName.append('<option value="">All</option>'); // Add "All" option on top

                        $.each(uniqueValuesStatusName, function (index, value) {
                            selectFooterStatusName.append('<option value="' + value + '">' + value + '</option>');
                        });

                        selectFooterStatusName.on('change', function () {
                            var val = $.fn.dataTable.util.escapeRegex($(this).val());
                            columnStatusName.search(val ? '^' + val + '$' : '', true, false).draw();
                        });
                    },

                });
            }
            }
    });
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
                $.get("/NewRequest/Delete", { id: $(this).closest('tr').find('td:eq(0) input').val() }, function (data) {
                    swal(data.Title, data.Message, data.Type).then(function () {
                        LoadData();
                    });
                });
            }
        });
});

function parseJsonDate(jsonDateString) {
    return new Date(parseInt(jsonDateString.replace('/Date(', ''))).toLocaleDateString();
}

