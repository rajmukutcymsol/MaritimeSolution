$(document).ready(function () {
    loadData();
})

function loadData() {
    $.get("/CommonMaster/GetRequirementDetail/" + $("#id").val(), function (data, status) {

        $("#category_name").val(data.MastersCategoryID);
        $("#CompanyName").val(data.CompanyName);
        $("#personincharge").val(data.personincharge);
        $("#Address").val(data.Address);
        $("#MobileNumber").val(data.MobileNumber);
        $("#Fax").val(data.Fax);
        $("#CountryID").val(data.CountryID);
        $("#Email").val(data.Email);
        if (data.is_active == true)
            $('#is_active').attr("checked", true);
        else
            $('#is_active').attr("checked", false);

        loadPicData(data.auto_id);
        BindState(data.stateid);
        BindCity(data.CountryID, data.stateid, data.cityid);

    })
}
function BindState(stateid) {
    if ($("#CountryID").val() != "") {
        var request = {
            CountryID: $("#CountryID").val()
        }

        $.ajax({
            url: "/CommonMaster/GetStateByID",
            data: JSON.stringify(request),
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (data) {
                var op = "";
                op += "<option>" + '--select--';
                $.each(data, function (i, item) {
                    op += "<option value='" + item.stateid + "'>" + item.StateName;
                });

                $("#stateid").html('');
                $("#stateid").append(op);
                $("#stateid").val(stateid);
            }
        });
    }

}
function BindCity(CountryID, stateid, cityid) {
    if ($("#CountryID").val() != "") {
        var request = {
            CountryID: CountryID,
            stateid: stateid
        }

        $.ajax({
            url: "/CommonMaster/GetCityByID",
            data: JSON.stringify(request),
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (data) {
                var op = "";
                op += "<option>" + '--select--';
                $.each(data, function (i, item) {
                    op += "<option value='" + item.cityid + "'>" + item.CityName;
                });

                $("#cityid").html('');
                $("#cityid").append(op);
                $("#cityid").val(cityid);

            }
        });
    }

}
function loadPicData(auto_id) {

    var request = {
        auto_id: auto_id
    }
    $.ajax({
        url: "/CommonMaster/GetPicData",
        data: JSON.stringify(request),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (data) {

            $('#tbl_Vendor').DataTable({
                "processing": true,
                "serverSide": false,
                "filter": true,
                "orderMulti": false,
                "searching": true,
                "destroy": true,
                "data": data,
                "columns": [
                    {
                        "data": "id",
                        "orderable": false,
                        "render": function (data, type, row, meta) {
                            return '<input type="hidden" value=' + data + '> <a href="#" class="btnDelete" style="margin-right: 5px;"><i class="fa fa-trash"></i></a>';
                        },
                    },
                    { 'data': 'PicName' },
                    { 'data': 'PicDesignation' },
                    { 'data': 'PicPhone' },
                    { 'data': 'PicEmail' },
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
                    "className": "text-center",
                },
                {
                    "targets": 4,
                    "className": "text-center",
                }],
            });

        }
    });
}

$('body').on('click', '.mybtn', function () {
    var projectRequirement = new FormData();

    projectRequirement.append("auto_id", $("#mainid").val());
    projectRequirement.append("MastersCategoryID", $("#category_name").val());
    projectRequirement.append("PicName", $("#PicName").val());
    projectRequirement.append("PicDesignation", $("#PicDesignation").val());
    projectRequirement.append("PicPhone", $("#PicPhone").val());
    projectRequirement.append("PicEmail", $("#PicEmail").val());
    $.ajax({
        url: '/CommonMaster/SavePicInformation',
        type: 'POST',
        data: projectRequirement,
        processData: false,  // tell jQuery not to process the data
        contentType: false,  // tell jQuery not to set contentType
        success: function (data) {
            swal(data.Title, data.Message, data.Type).then(function () {
                $("#PicName").val('');
                $("#PicDesignation").val('');
                $("#PicPhone").val('');
                $("#PicEmail").val('');
            });
            loadData();
        },
        failure: function (errorMessage) {
            swal("Oops!", errorMessage, "failure");
        }
    });
});

$('body').on('click', '#btnSave', function () {
    //alert($("#id").val());
    var res = validateMadatoryFields();
    if (res == false) {
        return false;
    }
    var is_active = false;
    if ($('#is_active').is(':checked'))
        is_active = true;

    var projectRequirement = new FormData();

    projectRequirement.append("MastersCategoryID", $("#category_name").val());
    projectRequirement.append("CompanyName", $("#CompanyName").val());
    projectRequirement.append("personincharge", $("#personincharge").val());
    projectRequirement.append("Address", $("#Address").val());
    projectRequirement.append("CountryID", $("#CountryID").val());
    projectRequirement.append("stateid", $("#stateid").val());
    projectRequirement.append("cityid", $("#cityid").val());
    projectRequirement.append("MobileNumber", $("#MobileNumber").val());
    projectRequirement.append("Fax", $("#Fax").val());
    projectRequirement.append("Email", $("#Email").val());
    projectRequirement.append("is_active", is_active);
    projectRequirement.append("id", $("#id").val());

    $.ajax({
        url: '/CommonMaster/Update',
        type: 'POST',
        data: projectRequirement,
        processData: false,  // tell jQuery not to process the data
        contentType: false,  // tell jQuery not to set contentType
        success: function (data) {
            swal(data.Title, data.Message, data.Type).then(function () {
                $("#mainid").val(data.auto_id);
            });
        },
        failure: function (errorMessage) {
            swal("Oops!", errorMessage, "failure");
        }
    });
});
function validateMadatoryFields() {
    var isValid = true;

    if ($("#category_name").val() == "00000000-0000-0000-0000-000000000000") {
        $('#category_name').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#category_name').css('border-color', 'lightgrey');
    }

    if ($("#CountryID").val() == "0" || $("#CountryID").val() == "" || $("#CountryID").val() == "--select--" || $("#CountryID").val() == "") {
        $('#CountryID').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#CountryID').css('border-color', 'lightgrey');
    }

    if ($("#cityid").val() == null || $("#cityid").val() == "0" || $("#cityid").val() == "" || $("#cityid").val() == "--select--" || $("#cityid").val() == "00000000-0000-0000-0000-000000000000") {
        $('#cityid').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#cityid').css('border-color', 'lightgrey');
    }
    if ($("#stateid").val() == null || $("#stateid").val() == "0" || $("#stateid").val() == "" || $("#stateid").val() == "--select--" || $("#stateid").val() == "00000000-0000-0000-0000-000000000000") {
        $('#stateid').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#stateid').css('border-color', 'lightgrey');
    }
    if ($("#cityid").val() == null || $("#cityid").val() == "0" || $("#cityid").val() == "" || $("#cityid").val() == "--select--" || $("#cityid").val() == "00000000-0000-0000-0000-000000000000") {
        $('#cityid').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#cityid').css('border-color', 'lightgrey');
    }
    if ($("#CompanyName").val() == "") {
        $('#CompanyName').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#CompanyName').css('border-color', 'lightgrey');
    }
    if ($("#personincharge").val() == "") {
        $('#personincharge').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#personincharge').css('border-color', 'lightgrey');
    }
    //if ($("#Address").val() == "") {
    //    $('#Address').css('border-color', 'Red');
    //    isValid = false;
    //}
    //else {
    //    $('#Address').css('border-color', 'lightgrey');
    //}
    if ($("#MobileNumber").val() == "") {
        $('#MobileNumber').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#MobileNumber').css('border-color', 'lightgrey');
    }


    //if ($("#Fax").val() == "") {
    //    $('#Fax').css('border-color', 'Red');
    //    isValid = false;
    //}
    //else {
    //    $('#Fax').css('border-color', 'lightgrey');
    //}
    if ($("#Email").val() == "") {
        $('#Email').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#Email').css('border-color', 'lightgrey');
    }

    return isValid;
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
                //alert($(this).closest('tr').find('td:eq(0) input').val());
                $.get("/CommonMaster/DeletePic", { id: $(this).closest('tr').find('td:eq(0) input').val() }, function (data) {
                    swal(data.Title, data.Message, data.Type).then(function () {
                        loadPicData($("#mainid").val());
                    });
                });
            }
        });
});
$("#CountryID").change(function () {
    BindStateMaster();
})
$("#stateid").change(function () {
    BindCityMaster();
})
function BindStateMaster() {
    if ($("#CountryID").val() != "") {
        var request = {
            CountryID: $("#CountryID").val()
        }

        $.ajax({
            url: "/CommonMaster/GetStateByID",
            data: JSON.stringify(request),
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (data) {
                var op = "";
                op += "<option>" + '--select--';
                $.each(data, function (i, item) {
                    op += "<option value='" + item.stateid + "'>" + item.StateName;
                });

                $("#stateid").html('');
                $("#stateid").append(op);
            }
        });
    }

}
function BindCityMaster() {
    if ($("#CountryID").val() != "") {
        var request = {
            CountryID: $("#CountryID").val(),
            stateid: $("#stateid").val()
        }

        $.ajax({
            url: "/CommonMaster/GetCityByID",
            data: JSON.stringify(request),
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (data) {
                var op = "";
                op += "<option>" + '--select--';
                $.each(data, function (i, item) {
                    op += "<option value='" + item.cityid + "'>" + item.CityName;
                });

                $("#cityid").html('');
                $("#cityid").append(op);
            }
        });
    }

}