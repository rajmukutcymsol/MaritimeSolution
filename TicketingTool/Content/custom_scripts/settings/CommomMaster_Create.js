$(document).ready(function () {
   
    $("#btnSave_add").prop("disabled", true);
    $("#is_active").prop("checked", true);
    $('#is_active_add').prop("checked", true);
    //loadPicData();
})

$("#CountryID").change(function () {
    BindState();
})
$("#stateid").change(function () {
    BindCity();
})
function BindState() {
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
function BindCity() {
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

$('body').on('click', '.mybtn', function () {
    if ($("#mainid").val() == "") {
        var res = validateMadatoryFields();
        if (res == false) {
            return false;
        }
    }
    var is_active = false;
    if ($('#is_active_add').is(':checked'))
        is_active = true;

    var projectRequirement = new FormData();

    projectRequirement.append("auto_id", $("#mainid").val());
    projectRequirement.append("MastersCategoryID", $("#category_name").val());
    projectRequirement.append("PicName", $("#PicName").val());
    projectRequirement.append("PicDesignation", $("#PicDesignation").val());
    projectRequirement.append("PicPhone", $("#PicPhone").val());
    projectRequirement.append("PicEmail", $("#PicEmail").val());
    projectRequirement.append("is_active", is_active);
    $.ajax({
        url: '/CommonMaster/SavePicInformation',
        type: 'POST',
        data: projectRequirement,
        processData: false,  // tell jQuery not to process the data
        contentType: false,  // tell jQuery not to set contentType
        success: function (data) {
            swal(data.Title, data.Message, data.Type).then(function () {

                $("#btnSave").prop("disabled", true);
                $("#category_name").prop("disabled", true);
                $("#category_name").prop("disabled", true);
                $("#CompanyName").prop("disabled", true);
                $("#personincharge").prop("disabled", true);
                $("#Address").prop("disabled", true);
                $("#CountryID").prop("disabled", true);
                $("#stateid").prop("disabled", true);
                $("#cityid").prop("disabled", true);
                $("#MobileNumber").prop("disabled", true);
                $("#Fax").prop("disabled", true);
                $("#Email").prop("disabled", true);
                $("#is_active").prop("disabled", true);
                loadPicData();
            });
        },
        failure: function (errorMessage) {
            swal("Oops!", errorMessage, "failure");
        }
    });
});

$('body').on('click', '#btnSave', function () {
   
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
    
    $.ajax({
        url: '/CommonMaster/Save',
        type: 'POST',
        data: projectRequirement,
        processData: false,  // tell jQuery not to process the data
        contentType: false,  // tell jQuery not to set contentType
        success: function (data) {
            swal(data.Title, data.Message, data.Type).then(function () {
                $("#mainid").val(data.auto_id);
                $("#btnSave_add").prop("disabled", false);
                $("#btnSave").prop("disabled", true);
                $("#category_name").prop("disabled", true);
                $("#category_name").prop("disabled", true);
                $("#CompanyName").prop("disabled", true);
                $("#personincharge").prop("disabled", true);
                $("#Address").prop("disabled", true);
                $("#CountryID").prop("disabled", true);
                $("#stateid").prop("disabled", true);
                $("#cityid").prop("disabled", true);
                $("#MobileNumber").prop("disabled", true);
                $("#Fax").prop("disabled", true);
                $("#Email").prop("disabled", true);
                $("#is_active").prop("disabled", true);
               

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

function loadPicData() {
    
    var request = {
        auto_id: $("#mainid").val()
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
        //$('#tbl_Vendor').DataTable({
        //    "processing": true, // for show progress bar
        //    "serverSide": false, // for process server side
        //    "filter": true, // this is for disable filter (search box)
        //    "orderMulti": false, // for disable multiple column at once
        //    "searching": true,
        //    "destroy": true,
        //    "data": data,
        //    "columns": [
        //        {
        //            "data": "id",
        //            "orderable": false,
        //            "render": function (data, type, row, meta) {
        //                return '<input type="hidden" value=' + data + '> <a href="#" class="btnEdit" style="margin-right: 5px;"><i class="fa fa-edit"></i></a> | &nbsp <a href="#" class="btnDelete" style="margin-right: 5px;"><i class="fa fa-trash"></i></a>';
        //            },
        //        },
        //        { 'data': 'PicName' },
        //        { 'data': 'PicDesignation' },
        //        { 'data': 'PicPhone' },
        //        { 'data': 'PicEmail' },
        //        { 'data': 'PicName' },
        //        {
        //            data: "is_active",
        //            "searchable": false,
        //            "orderable": false,
        //            "render": function (data, type, row) {
        //                if (data == false)
        //                    return '<span class="label label-danger">In-Active</span>';
        //                else
        //                    return '<span class="label label-success">Active</span>';
        //            }

        //        }


        //    ],
        //    "aoColumnDefs": [{
        //        'bSortable': false,
        //        'aTargets': [0],
        //    },
        //    {
        //        "targets": 2,
        //        "className": "text-center",
        //    }],

        //});
   
}