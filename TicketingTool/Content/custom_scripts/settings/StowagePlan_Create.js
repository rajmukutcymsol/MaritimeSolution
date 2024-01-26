$(document).ready(function () {
    $("#btnSave").show();
    $("#btnEdit").hide();
    $("#btnCancel").hide();
    // Handle the button click event to save data
    $("#btnSave").click(function () {
        if (validateFields() == false)
            return;
        else
        var urlParts = window.location.href.split('/');
        var guidFromUrl = urlParts[urlParts.length - 1];
        if (guidFromUrl.endsWith('#')) {
            guidFromUrl = guidFromUrl.slice(0, -1); // Remove the last character
        }
       
        // Extract data from input fields
        var destination = $("#destination").val();
        var hold = $("#hold").val();
        var holdQuantity = parseFloat($("#holdQuantity").val()) || 0;
        var marks_and_nos_name = $("#marks_and_nos_name").val();
        var cargo_type_name = $("#cargo_type_name").val();
        var receiver_name = $("#receiver_name").val();
        var otherinfo = $("#otherinfo").val();


        // Create an object to send to the server
        var data = {
            destination: destination,
            hold: hold,
            holdQuantity: holdQuantity,
            marks_and_nos_name: marks_and_nos_name,
            cargo_type_name: cargo_type_name,
            receiver_name: receiver_name,
            otherinfo: otherinfo,
            auto_id: $("#auto_id").val(),
            id_ref: guidFromUrl
        };
        // Send the data to the server using an AJAX request
        $.ajax({
            url: '/InBound/Save_Stowage_info', // Replace with your server endpoint
            type: 'POST',
            data: JSON.stringify(data),
            contentType: 'application/json',
            success: function (response) {
                swal(response.Title, response.Message, response.Type).then(function () {
                    $("#holdQuantity").val('');
                    $("#otherinfo").val('');
                    loadData();
                    $("#btnSave").show();
                    $("#btnEdit").hide();
                    $("#btnCancel").hide();
                });
            },
            error: function (error) {
                // Handle any errors here
                console.error(error);
                alert('Error occurred while saving data.');
            }
        });
    }

    );
    loadData();
});

function loadData() {
    $.get("/InBound/GetAllStowage_info?auto_id="+$("#auto_id").val() , function (data) {
        $('#PlanInfo').DataTable({
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
                        return '<input type="hidden" value=' + data + '> <a href="#" class="btnEdit" style="margin-right: 5px;"><i class="fa fa-edit"></i></a> | &nbsp <a href="#" class="btnDelete" style="margin-right: 5px;"><i class="fa fa-trash"></i></a>';
                    },
                },
                { 'data': 'destination' },
                { 'data': 'hold' },
                { 'data': 'holdQuantity' },
                { 'data': '_marks_and_nos_name' },
                { 'data': '_cargo_type_name' },
                { 'data': '_receiver_name' },
                { 'data': 'otherinfo' },

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
$('body').on('click', '.btnEdit', function () {
    $("#stwid").val($(this).closest('tr').find('td:eq(0) input').val());
    $.get("/InBound/Edit_Stowage_Info", { id: $(this).closest('tr').find('td:eq(0) input').val() }, function (data) {
        $("#destination").val(data.destination);
        $("#hold").val(data.hold);
        $("#holdQuantity").val(data.holdQuantity);
        $("#marks_and_nos_name").val(data.marks_and_nos_name);
        $("#cargo_type_name").val(data.cargo_type_name);
        $("#receiver_name").val(data.receiver_name);
        $("#otherinfo").val(data.otherinfo);

        $("#btnSave").hide();
        $("#btnEdit").show();
        $("#btnCancel").show();
    });
});
$("#btnCancel").click(function () {
    $("#btnSave").show();
    $("#btnEdit").hide();
    $("#btnCancel").hide();

    $("#destination").val('');
    $("#hold").val(data.hold);
    $("#holdQuantity").val(data.holdQuantity);
    $("#marks_and_nos_name").val(data.marks_and_nos_name);
    $("#cargo_type_name").val(data.cargo_type_name);
    $("#receiver_name").val(data.receiver_name);
    $("#otherinfo").val('');
});
$("#btnEdit").click(function () {

    var urlParts = window.location.href.split('/');
    var guidFromUrl = urlParts[urlParts.length - 1];
    if (guidFromUrl.endsWith('#')) {
        guidFromUrl = guidFromUrl.slice(0, -1); // Remove the last character
    }

    var destination = $("#destination").val();
    var hold = $("#hold").val();
    var holdQuantity = parseFloat($("#holdQuantity").val()) || 0;
    var marks_and_nos_name = $("#marks_and_nos_name").val();
    var cargo_type_name = $("#cargo_type_name").val();
    var receiver_name = $("#receiver_name").val();
    var otherinfo = $("#otherinfo").val();
    var id= $("#stwid").val();

    var data = {
        destination: destination,
        hold: hold,
        holdQuantity: holdQuantity,
        marks_and_nos_name: marks_and_nos_name,
        cargo_type_name: cargo_type_name,
        receiver_name: receiver_name,
        otherinfo: otherinfo,
        auto_id: $("#auto_id").val(),
        id_ref: guidFromUrl,
        Id:id
    };

    $.ajax({
        url: '/InBound/Update_Stowage_info', // Replace with your server endpoint for updating
        type: 'POST',
        data: JSON.stringify(data),
        contentType: 'application/json',
        success: function (response) {
            swal(response.Title, response.Message, response.Type).then(function () {
                $("#holdQuantity").val('');
                $("#otherinfo").val('');
                $("#hold").val('');
                $("#holdQuantity").val('');
                $("#marks_and_nos_name").val('');
                $("#cargo_type_name").val('');
                $("#receiver_name").val('');

                $("#btnSave").show();
                $("#btnEdit").hide();
                $("#btnCancel").hide();
                loadData(); // This function may load the updated data
            });
        },
        error: function (error) {
            // Handle any errors here
            console.error(error);
            alert('Error occurred while updating data.');
        }
    });
});
$('#PlanInfo').on('click', '.btnDelete', function (e) {
    e.preventDefault(); // Prevent the default click behavior (hyperlink)
    swal({
        title: "Are you sure?",
        text: "Once deleted, you will not be able to recover this role",
        icon: "warning",
        buttons: true,
        dangerMode: true,
    })
        .then((willDelete) => {
            if (willDelete) {
                var item = $(this).closest('tr').find('td:eq(0) input').val();
                $.get("/InBound/DeleteStowsInfo", { id: item }, function (data) {
                    swal(data.Title, data.Message, data.Type).then(function () {
                        loadData();
                    });
                });
            }
        });
});

function validateFields() {
    var isValid = true;

    // Validate the destination field
    var destination = $("#destination").val().trim();
    if (destination === '') {
        alert('Please enter a destination.');
        isValid = false;
    }

    // Validate the hold field
    var hold = $("#hold").val().trim();
    if (hold === '') {
        alert('Please enter a hold.');
        isValid = false;
    }

    // Validate the holdQuantity field
    var holdQuantity = parseFloat($("#holdQuantity").val()) || 0;
    if (holdQuantity <0) {
        alert('Please enter a valid hold quantity.');
        isValid = false;
    }

    // Add more validations for other fields as needed

    // Example: Validate receiver_name (assuming it's a dropdown)
    var receiver_name = $("#receiver_name").val();
    if (receiver_name === '' || receiver_name === null) {
        alert('Please select a receiver.');
        isValid = false;
    }

    // You can add more validation rules for other fields in a similar manner

    return isValid;
}

function validate_save() {
    var isValid = true;
    if ($('#arrival_date').val().trim() == "") {
        $('#arrival_date').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#arrival_date').css('border-color', 'lightgrey');
    }
    if ($('#sailedon_date').val().trim() == "") {
        $('#sailedon_date').css('border-color', 'Red');
        isValid = false;

    }
    else {
        $('#sailedon_date').css('border-color', 'lightgrey');
    }
    return isValid;
}

$('body').on('click', '.btnsaves', function () {

    var urlParts = window.location.href.split('/');
    var guidFromUrl = urlParts[urlParts.length - 1];
    if (guidFromUrl.endsWith('#')) {
        guidFromUrl = guidFromUrl.slice(0, -1); // Remove the last character
    }
    var res = validate_save();
    if (res == false)
        return false;

    var projectRequirement = new FormData();
    projectRequirement.append("auto_id", $("#auto_id").val());
    projectRequirement.append("vesselname", $("#vesselname").val());
    projectRequirement.append("loabeamdeapth", $("#loabeamdeapth").val());
    projectRequirement.append("capacities", $("#capacities").val());
    projectRequirement.append("deadweight", $("#deadweight").val());
    projectRequirement.append("arrival_date", $("#arrival_date").val());
    projectRequirement.append("sailedon_date", $("#sailedon_date").val());
    projectRequirement.append("gross_weight_of_cargo", $("#gross_weight_of_cargo").val());
    projectRequirement.append("id_ref", guidFromUrl);

    $.ajax({
        url: '/InBound/Stowage_Main_Info',
        type: 'POST',
        data: projectRequirement,
        processData: false,  // tell jQuery not to process the data
        contentType: false,  // tell jQuery not to set contentType
        success: function (data) {
            swal(data.Title, data.Message, data.Type).then(function () {
                // Handle success
            });
        },
        failure: function (errorMessage) {
            swal("Oops!", errorMessage, "failure");
        }
    });
});

$('#btnApprove').on('click', function (e) {
    e.preventDefault(); // Prevent the default link behavior

    // Extract the GUID from the URL
    var urlParts = window.location.href.split('/');
    var guidFromUrl = urlParts[urlParts.length - 1];
    if (guidFromUrl.endsWith('#')) {
        guidFromUrl = guidFromUrl.slice(0, -1); // Remove the last character
    }

    swal({
        title: "Are you sure?",
        text: "Want to Approve?",
        icon: "warning",
        buttons: true,
        dangerMode: true,
    }).then((willApprove) => {
        if (willApprove) {
            // If the user confirms the approval, send an AJAX request
            $.get("/InBound/ApprovePlanInfo", { id: guidFromUrl, auto_id: $("#auto_id").val() }, function (data) {
                swal(data.Title, data.Message, data.Type).then(function () {
                    $('#btnPrint').show();
                });
            });
        }
    });
});


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
        var serviceUrl = "/InBound/Stowage_Plan_Approval_Report";
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
                window.open("/Reports/approve_stowage_plan.aspx?id_ref=" + data.id_ref, "_newtab");
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

