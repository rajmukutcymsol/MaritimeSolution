$(document).ready(function () {
    GetRowData();
    $(".mybtn_Edit").hide();
    $(".mybtn_Edit_Cancel").hide();

    $("#quantity").on("keypress", function (event) {
        var charCode = (event.which) ? event.which : event.keyCode;
        if (
            (charCode >= 48 && charCode <= 57) || // Digits 0-9
            charCode === 46 // Dot (.)
        ) {
            if (charCode === 46 && $(this).val().indexOf('.') !== -1) {
                event.preventDefault();
            }
        } else {
            event.preventDefault();
        }
    });
    SetPermission();

    $('#eta_notice').inputmask('99/99/9999', {
        placeholder: 'DD/MM/YYYY',
        alias: 'datetime',
        inputFormat: 'dd/mm/yyyy',
    });
    $('#commence_to_discharge_cargo').inputmask('99/99/9999', {
        placeholder: 'DD/MM/YYYY',
        alias: 'datetime',
        inputFormat: 'dd/mm/yyyy',
    });
    $('#est_to_complete_loading_cargo').inputmask('99/99/9999', {
        placeholder: 'DD/MM/YYYY',
        alias: 'datetime',
        inputFormat: 'dd/mm/yyyy',
    });
    $('#plan_of_sailing').inputmask('99/99/9999', {
        placeholder: 'DD/MM/YYYY',
        alias: 'datetime',
        inputFormat: 'dd/mm/yyyy',
    });
    $('#nor_notice').inputmask('99/99/9999', {
        placeholder: 'DD/MM/YYYY',
        alias: 'datetime',
        inputFormat: 'dd/mm/yyyy',
    });
});
function SetPermission() {
    if ($('#CanUpdate').val() == 'true') {

    }
    else {
        $("#btnSave").hide();
        $("#mybtn").hide();
        $("#myEditbtn").hide();
    }
    if ($('#CanDelete').val() == 'true') {

    }
    else {

    }
    if ($('#CanVerify').val() == 'true') {

    }
    else {
        $("#btnApprove").hide();
    }

}
function GetRowData() {
    $.get("/InBound/GetRequirementDetail/" + $("#id").val(), function (data, status) {
        $("#auto_id").val(data.auto_id);
        $("#vessel_name").val(data.vessel_name);
        $("#discharge_port").val(data.discharge_port);
        $("#EAT_HH").val(data.EAT_HH); // Assuming data.EAT_HH is the selected option's value
        $("#EAT_MM").val(data.EAT_MM); // Assuming data.EAT_MM is the selected option's value
        $("#EAT_Date").val(data.EAT_Date);
        $("#refNo").val(data.refNo);
        $("#rcn").val(data.rcn);
        $("#LoadPort").val(data.LoadPort);
        $("#mastername").val(data.mastername);
        $("#mastercontactnumber").val(data.mastercontactnumber);
        $("#masteremail").val(data.masteremail);
        $("#owners_name").val(data.owners_name);
        $("#receiver_name").val(data.receiver_name);
        $("#surveyor_name").val(data.surveyor_name);
        $("#stevedore_name").val(data.stevedore_name);
        $("#shipping_name").val(data.shipping_name);
        $("#checker_name").val(data.checker_name);
        $("#VoyageNo").val(data.VoyageNo);
        $("#LastPortofCall").val(data.LastPortofCall);
        $("#FC_Stevedore_Name").val(data.FC_Stevedore_Name);

        $("#eta_notice").val(data.eta_notice);
        $("#EAT_HH_eta_notice").val(data.EAT_HH_eta_notice);
        $("#EAT_MM_eta_notice").val(data.EAT_MM_eta_notice);
        $("#commence_to_discharge_cargo").val(data.commence_to_discharge_cargo);
        $("#EAT_HH_commence_to_discharge_cargo").val(data.EAT_HH_commence_to_discharge_cargo);
        $("#EAT_MM_commence_to_discharge_cargo").val(data.EAT_MM_commence_to_discharge_cargo);
        $("#est_to_complete_loading_cargo").val(data.est_to_complete_loading_cargo);
        $("#plan_of_sailing").val(data.plan_of_sailing);
        $("#vessels_stay").val(data.vessels_stay);
        $("#lat").val(data.lat);
        $("#longt").val(data.longt);

        $("#nor_notice").val(data.nor_notice);
        $("#EAT_HH_nor_notice").val(data.EAT_HH_nor_notice);
        $("#EAT_MM_nor_notice").val(data.EAT_MM_nor_notice);

        loadData();
    })
}
function loadData() {
    $.get("/InBound/GetManiFestByAutoId", { auto_id: $("#auto_id").val() }, function (data) {
        $('#tbl_InBoundManiFest').DataTable({
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
                        var editLink = '';
                        if ($('#CanUpdate').val() == 'true') {
                            editLink = '&nbsp <a href="#' + data + '" class="btnEdit" style="margin-right: 5px;"><i class="fa fa-edit"></i></a>|&nbsp';
                        }

                        var deleteLink = '';
                        if ($('#CanDelete').val() == 'true') {
                            deleteLink = '&nbsp <a href="#" class="btnDelete" style="margin-right: 5px;"><i class="fa fa-trash"></i></a>|&nbsp';
                        }

                        return '<input type="hidden" value="' + data + '">' +
                            editLink +
                            deleteLink;
                    }
                },
                { 'data': 'blno' },
                { 'data': 'shippername' },
                { 'data': 'consignee_name' },
                { 'data': 'notify_name' },
                { 'data': 'marks_and_nos' },
                { 'data': 'quantity' },
            ],
            "aoColumnDefs": [
                {
                    'bSortable': false,
                    'aTargets': [0],
                },
                {
                    "targets": 2,
                    "className": "text-center",
                }
            ],
        });

    })

}
$('body').on('click', '.btnsaves', function () {
    var res = validate();
    if (res == false)
        return false;
    var res = finalsave();
    if (res == false)
        return false;
    var projectRequirement = new FormData();

    projectRequirement.append("auto_id", $("#auto_id").val());
    projectRequirement.append("vessel_name", $("#vessel_name").val());
    projectRequirement.append("discharge_port", $("#discharge_port").val());
    projectRequirement.append("EAT_HH", $("#EAT_HH option:selected").text());
    projectRequirement.append("EAT_MM", $("#EAT_MM option:selected").text());
    projectRequirement.append("EAT_Date", $("#EAT_Date").val());
    projectRequirement.append("refNo", $("#refNo").val());
    projectRequirement.append("rcn", $("#rcn").val());
    projectRequirement.append("LoadPort", $("#LoadPort").val());
    projectRequirement.append("mastername", $("#mastername").val());
    projectRequirement.append("mastercontactnumber", $("#mastercontactnumber").val());
    projectRequirement.append("masteremail", $("#masteremail").val());
    projectRequirement.append("owners_name", $("#owners_name").val());
    projectRequirement.append("receiver_name", $("#receiver_name").val());
    projectRequirement.append("surveyor_name", $("#surveyor_name").val());
    projectRequirement.append("stevedore_name", $("#stevedore_name").val());
    projectRequirement.append("shipping_name", $("#shipping_name").val());
    projectRequirement.append("checker_name", $("#checker_name").val());
    projectRequirement.append("VoyageNo", $("#VoyageNo").val());
    projectRequirement.append("LastPortofCall", $("#LastPortofCall").val());
    projectRequirement.append("FC_Stevedore_Name", $("#FC_Stevedore_Name").val());
    projectRequirement.append("eta_notice", $("#eta_notice").val());
    projectRequirement.append("EAT_HH_eta_notice", $("#EAT_HH_eta_notice").val());
    projectRequirement.append("EAT_MM_eta_notice", $("#EAT_MM_eta_notice").val());
    projectRequirement.append("EAT_HH_commence_to_discharge_cargo", $("#EAT_HH_commence_to_discharge_cargo").val());
    projectRequirement.append("EAT_MM_commence_to_discharge_cargo", $("#EAT_MM_commence_to_discharge_cargo").val());
    projectRequirement.append("est_to_complete_loading_cargo", $("#est_to_complete_loading_cargo").val());
    projectRequirement.append("plan_of_sailing", $("#plan_of_sailing").val());
    projectRequirement.append("vessels_stay", $("#vessels_stay").val());
    projectRequirement.append("commence_to_discharge_cargo", $("#commence_to_discharge_cargo").val());
    projectRequirement.append("lat", $("#lat").val());
    projectRequirement.append("longt", $("#longt").val());

    projectRequirement.append("nor_notice", $("#nor_notice").val());
    projectRequirement.append("EAT_HH_nor_notice", $("#EAT_HH_nor_notice").val());
    projectRequirement.append("EAT_MM_nor_notice", $("#EAT_MM_nor_notice").val());


    $.ajax({
        url: '/InBound/UpdateAll',
        type: 'POST',
        data: projectRequirement,
        processData: false,  // tell jQuery not to process the data
        contentType: false,  // tell jQuery not to set contentType
        success: function (data) {
            swal(data.Title, data.Message, data.Type).then(function () {
                
            });
        },
        failure: function (errorMessage) {
            swal("Oops!", errorMessage, "failure");
        }
    });
});

function finalsave() {
    var isValid = true;
    if ($('#mastername').val().trim() == "") {
        $('#mastername').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#mastername').css('border-color', 'lightgrey');
    }
    if ($('#owners_name').val().trim() == "00000000-0000-0000-0000-000000000000") {
        $('#owners_name').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#owners_name').css('border-color', 'lightgrey');
    }
    if ($('#receiver_name').val().trim() == "00000000-0000-0000-0000-000000000000") {
        $('#receiver_name').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#receiver_name').css('border-color', 'lightgrey');
    }
    if ($('#surveyor_name').val().trim() == "00000000-0000-0000-0000-000000000000") {
        $('#surveyor_name').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#surveyor_name').css('border-color', 'lightgrey');
    }
    if ($('#stevedore_name').val().trim() == "00000000-0000-0000-0000-000000000000") {
        $('#stevedore_name').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#stevedore_name').css('border-color', 'lightgrey');
    }
    if ($('#shipping_name').val().trim() == "00000000-0000-0000-0000-000000000000") {
        $('#shipping_name').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#shipping_name').css('border-color', 'lightgrey');
    }
    if ($('#checker_name').val().trim() == "00000000-0000-0000-0000-000000000000") {
        $('#checker_name').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#checker_name').css('border-color', 'lightgrey');
    }

    //
    
   

    return isValid;
}
function validate() {

    var isValid = true;
    if ($('#VoyageNo').val().trim() == "") {
        $('#VoyageNo').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#VoyageNo').css('border-color', 'lightgrey');
    }
    if ($('#LastPortofCall').val().trim() == "") {
        $('#LastPortofCall').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#LastPortofCall').css('border-color', 'lightgrey');
    }
    if ($('#vessel_name').val().trim() == "00000000-0000-0000-0000-000000000000") {
        $('#vessel_name').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#vessel_name').css('border-color', 'lightgrey');
    }

    if ($('#discharge_port').val().trim() == "00000000-0000-0000-0000-000000000000") {
        $('#discharge_port').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#discharge_port').css('border-color', 'lightgrey');
    }
    if ($('#EAT_HH').val().trim() == "") {
        $('#EAT_HH').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#EAT_HH').css('border-color', 'lightgrey');
    }
    if ($('#EAT_MM').val().trim() == "") {
        $('#EAT_MM').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#EAT_MM').css('border-color', 'lightgrey');
    }
    if ($('#EAT_Date').val().trim() == "") {
        $('#EAT_Date').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#EAT_Date').css('border-color', 'lightgrey');
    }
    if ($('#refNo').val().trim() == "") {
        $('#refNo').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#refNo').css('border-color', 'lightgrey');
    }
    if ($('#rcn').val().trim() == "") {
        $('#rcn').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#rcn').css('border-color', 'lightgrey');
    }
    if ($('#LoadPort').val().trim() == "") {
        $('#LoadPort').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#LoadPort').css('border-color', 'lightgrey');
    }
    if ($('#eta_notice').val().trim() == "") {
        $('#eta_notice').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#eta_notice').css('border-color', 'lightgrey');
    }
    if ($('#EAT_HH_eta_notice').val().trim() == "") {
        $('#EAT_HH_eta_notice').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#EAT_HH_eta_notice').css('border-color', 'lightgrey');
    }
    if ($('#EAT_MM_eta_notice').val().trim() == "") {
        $('#EAT_MM_eta_notice').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#EAT_MM_eta_notice').css('border-color', 'lightgrey');
    }
    if ($('#nor_notice').val().trim() == "") {
        $('#nor_notice').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#nor_notice').css('border-color', 'lightgrey');
    }
    if ($('#commence_to_discharge_cargo').val().trim() == "") {
        $('#commence_to_discharge_cargo').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#commence_to_discharge_cargo').css('border-color', 'lightgrey');
    }
    if ($('#est_to_complete_loading_cargo').val().trim() == "") {
        $('#est_to_complete_loading_cargo').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#est_to_complete_loading_cargo').css('border-color', 'lightgrey');
    }
    if ($('#plan_of_sailing').val().trim() == "") {
        $('#plan_of_sailing').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#plan_of_sailing').css('border-color', 'lightgrey');
    }


    return isValid;
}
$('body').on('click', '.mybtn', function () {
    var res = savemanifest();
    if (res == false)
        return false;

    var isChecked = false;
    if ($('#is_cleanonBoard').is(':checked'))
        isChecked = true;

    //var isChecked = $("#is_cleanonBoard").prop("checked");

    //if (isChecked) {

    //    isChecked = true;
    //} else {
    //    isChecked = false;
    //}
    var projectRequirement = new FormData();

    //projectRequirement.append("id", $("#id").val());
    projectRequirement.append("auto_id", $("#auto_id").val());
    projectRequirement.append("blno", $("#blno").val());
    projectRequirement.append("shipper_name", $("#shipper_name").val());
    projectRequirement.append("consignee", $("#consignee").val());
    projectRequirement.append("notify", $("#notify").val());
    projectRequirement.append("marks_and_nos_name", $("#marks_and_nos_name").val());
    projectRequirement.append("quantity_and_kind_of_cargo_name", $("#Quantity_and_Kind_of_cargo").val());
    projectRequirement.append("quantity", $("#quantity").val());
    projectRequirement.append("cargotype_desc", $("#cargotype_desc").val());
    projectRequirement.append("is_active", $("#is_active").val());
    projectRequirement.append("created_by", $("#created_by").val());
    projectRequirement.append("updated_by", $("#updated_by").val());
    projectRequirement.append("is_cleanonBoard", isChecked);
    projectRequirement.append("cargo_type_name", $("#cargo_type_name").val());
    projectRequirement.append("qtt_name", $("#qtt_name").val());
    $.ajax({
        url: '/InBound/SaveManiFest',
        type: 'POST',
        data: projectRequirement,
        processData: false,  // tell jQuery not to process the data
        contentType: false,  // tell jQuery not to set contentType
        success: function (data) {
            swal(data.Title, data.Message, data.Type).then(function () {
                //$("#vessel_name").prop("disabled", true);
                //$("#discharge_port").prop("disabled", true);
                //$("#eat_display").prop("disabled", true);
                //$("#EAT_HH").prop("disabled", true);
                //$("#EAT_MM").prop("disabled", true);
                //$("#EAT_Date").prop("disabled", true);
                //$("#refNo").prop("disabled", true);
                //$("#rcn").prop("disabled", true);
                //$("#LoadPort").prop("disabled", true);

                $("#masterdetails_section").show();
                $("#masterdetails").show();
                $("#inward_section").show();
                $("#inward").show();
                $("#btnProcess").hide();

                $("#blno").val('');
                $("#shipper_name").val();
                $("#consignee").val('');
                $("#quantity").val('');
                $("#cargotype_desc").val('');
                $("#Quantity_and_Kind_of_cargo").val('');
                $("#manifest").show();

                loadData();
            });
        },
        failure: function (errorMessage) {
            swal("Oops!", errorMessage, "failure");
        }
    });

})
function savemanifest() {
    var isValid = true;
    if ($('#blno').val().trim() == "") {
        $('#blno').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#blno').css('border-color', 'lightgrey');
    }
    if ($('#shipper_name').val().trim() == "00000000-0000-0000-0000-000000000000") {
        $('#shipper_name').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#shipper_name').css('border-color', 'lightgrey');
    }
    if ($('#consignee').val().trim() == "") {
        $('#consignee').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#consignee').css('border-color', 'lightgrey');
    }
    if ($('#notify').val().trim() == "00000000-0000-0000-0000-000000000000") {
        $('#notify').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#notify').css('border-color', 'lightgrey');
    }
    if ($('#marks_and_nos_name').val().trim() == "00000000-0000-0000-0000-000000000000") {
        $('#marks_and_nos_name').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#marks_and_nos_name').css('border-color', 'lightgrey');
    }
    if ($('#quantity').val().trim() == "") {
        $('#quantity').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#quantity').css('border-color', 'lightgrey');
    }
    if ($('#Quantity_and_Kind_of_cargo').val().trim() == "00000000-0000-0000-0000-000000000000")
    {
        $('#Quantity_and_Kind_of_cargo').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#Quantity_and_Kind_of_cargo').css('border-color', 'lightgrey');
    }
    if ($('#cargo_type_name').val().trim() == "00000000-0000-0000-0000-000000000000") {
        $('#cargo_type_name').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#cargo_type_name').css('border-color', 'lightgrey');
    }
    if ($('#qtt_name').val().trim() == "00000000-0000-0000-0000-000000000000") {
        $('#qtt_name').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#qtt_name').css('border-color', 'lightgrey');
    }
    return isValid;
}
$('body').on('click', '.btnEdit', function () {
    $.get("/InBound/Edit_ManiFest", { id: $(this).closest('tr').find('td:eq(0) input').val() }, function (data) {
        $("#id_ManiFest").val(data.id);
        $("#blno").val(data.blno);
        $("#shipper_name").val(data.shipper_name);
        $("#consignee").val(data.consignee);
        $("#notify").val(data.notify);
        $("#marks_and_nos_name").val(data.marks_and_nos_name);
        $("#quantity").val(data.quantity);
        $("#cargotype_desc").val(data.cargotype_desc);
        $("#Quantity_and_Kind_of_cargo").val(data.quantity_and_kind_of_cargo);
        $("#cargotype_desc").focus();
        $("#cargo_type_name").val(data.cargo_type_name);
        $("#qtt_name").val(data.qtt_name);

        if (data.is_cleanonBoard === true) {
            $('#is_cleanonBoard').prop('checked', true);
        } else {
            $('#is_cleanonBoard').prop('checked', false);
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

    $("#blno").val('');
    $("#shipper_name").val('');
    $("#consignee").val('');
    $("#notify").val('');
    $("#marks_and_nos_name").val('');
    $("#quantity").val('');
    $("#cargotype_desc").val('');
    $("#Quantity_and_Kind_of_cargo").val('');

    $("#blno").focus();
})
$('body').on('click', '.mybtn_Edit', function () {
    var res = savemanifest();
    var isChecked = $("#is_cleanonBoard").prop("checked");
    if (isChecked) {
        isChecked = true;
    } else {
        isChecked = false;
    }

    if (res == false)
        return false;
    var projectRequirement = new FormData();

    projectRequirement.append("id", $("#id_ManiFest").val());
    projectRequirement.append("auto_id", $("#auto_id").val());
    projectRequirement.append("blno", $("#blno").val());
    projectRequirement.append("shipper_name", $("#shipper_name").val());
    projectRequirement.append("consignee", $("#consignee").val());
    projectRequirement.append("notify", $("#notify").val());
    projectRequirement.append("marks_and_nos_name", $("#marks_and_nos_name").val());
    projectRequirement.append("quantity_and_kind_of_cargo_name", $("#Quantity_and_Kind_of_cargo").val());
    projectRequirement.append("quantity", $("#quantity").val());
    projectRequirement.append("cargotype_desc", $("#cargotype_desc").val());
    projectRequirement.append("is_active", $("#is_active").val());
    projectRequirement.append("created_by", $("#created_by").val());
    projectRequirement.append("updated_by", $("#updated_by").val());
    projectRequirement.append("cargo_type_name", $("#cargo_type_name").val());
    projectRequirement.append("qtt_name", $("#qtt_name").val());
    projectRequirement.append("is_cleanonBoard", isChecked);

    $.ajax({
        url: '/InBound/UpdateManiFest',
        type: 'POST',
        data: projectRequirement,
        processData: false,  // tell jQuery not to process the data
        contentType: false,  // tell jQuery not to set contentType
        success: function (data) {
            swal(data.Title, data.Message, data.Type).then(function () {
                //$("#vessel_name").prop("disabled", true);
                //$("#discharge_port").prop("disabled", true);
                //$("#eat_display").prop("disabled", true);
                //$("#EAT_HH").prop("disabled", true);
                //$("#EAT_MM").prop("disabled", true);
                //$("#EAT_Date").prop("disabled", true);
                //$("#refNo").prop("disabled", true);
                //$("#rcn").prop("disabled", true);
                //$("#LoadPort").prop("disabled", true);

                $("#masterdetails_section").show();
                $("#masterdetails").show();
                $("#inward_section").show();
                $("#inward").show();
                $("#btnProcess").hide();

                $("#blno").val('');
                $("#shipper_name").val();
                $("#consignee").val('');
                $("#quantity").val('');
                $("#cargotype_desc").val('');
                $("#Quantity_and_Kind_of_cargo").val('');
                $("#manifest").show();

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
                $.get("/InBound/Delete", { id: $(this).closest('tr').find('td:eq(0) input').val() }, function (data) {
                    swal(data.Title, data.Message, data.Type).then(function () {
                        loadData();
                    });
                });
            }
        });
});
$('body').on('click', '.btnApprove', function () {
    GetApprovalData();
    $('#modal-default').modal('show');
   
    //swal({
    //    title: "Are you sure?",
    //    text: "Once Approved, you will not be able to change.",
    //    icon: "warning",
    //    buttons: true,
    //    dangerMode: true,
    //})
    //.then((willDelete) => {
    //        if (willDelete) {
    //            $.get("/InBound/Approved", {
    //                auto_id: $("#auto_id").val(),
    //                id: $("#id").val()
    //            }, function (data) {
    //                swal(data.Title, data.Message, data.Type).then(function () {
    //                    window.location.href = '/InBound/Index';
    //                });
    //            });
    //        }
    //    });
});
function GetApprovalData() {
    $.get("/InBound/GetApprovalData/" + $("#id").val(), function (data, status) {
        //$("#auto_id").val(data.auto_id);
        $("#vesselName").text(data.vessel);
        $("#mastername_cargo").text(data.mastername);

        $("#flags_cargo").text(data.flags);
        $("#LoadPort_cargo").text(data.LoadPort);
        $("#discharge_port_name_cargo").text(data.discharge_port_name);
        $("#rcn_cargo").text(data.rcn);
        $("#refNo_cargo").text(data.refNo);
        $("#TotalQuantity").text(data.TotalQuantity);
        LoadData_Cargro();
        //$("#discharge_port").val(data.discharge_port);
        //$("#EAT_HH").val(data.EAT_HH); // Assuming data.EAT_HH is the selected option's value
        //$("#EAT_MM").val(data.EAT_MM); // Assuming data.EAT_MM is the selected option's value
        //$("#EAT_Date").val(data.EAT_Date);
        //$("#refNo").val(data.refNo);
        //$("#rcn").val(data.rcn);
        //$("#LoadPort").val(data.LoadPort);
        //$("#mastername").val(data.mastername);
        //$("#mastercontactnumber").val(data.mastercontactnumber);
        //$("#masteremail").val(data.masteremail);
        //$("#owners_name").val(data.owners_name);
        //$("#receiver_name").val(data.receiver_name);
        //$("#surveyor_name").val(data.surveyor_name);
        //$("#stevedore_name").val(data.stevedore_name);
        //$("#shipping_name").val(data.shipping_name);
        //$("#checker_name").val(data.checker_name);
        //$("#VoyageNo").val(data.VoyageNo);
        //$("#LastPortofCall").val(data.LastPortofCall);
        //$("#FC_Stevedore_Name").val(data.FC_Stevedore_Name);

        //loadData();
    })
}

function LoadData_Cargro() {
    var request = {
        auto_id: $("#auto_id").val()
    }
    $.ajax({
        url: "/InBound/GetCargoMenifestById",
        data: JSON.stringify(request),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (data) {
            var op = '<table  style="border-collapse: collapse; text-align:center ;width: 96%;margin-right: 20px;margin-left: 20px; margin-top:8px;">' +
                '<tr>'+
                '<th style="border: 1px solid black; padding: 8px; text-align: left; background-color: #f2f2f2;">B/L NO.</th>' +
                '<th style="border: 1px solid black; padding: 8px; text-align: left; background-color: #f2f2f2;">SHIPPER (SH)</th>' +
                '<th style="border: 1px solid black; padding: 8px; text-align: left; background-color: #f2f2f2;">CONSIGNEE (CN)</th>' +
                '<th style="border: 1px solid black; padding: 8px; text-align: left; background-color: #f2f2f2;">NOTIFY (NY)</th>' +
                '<th style="border: 1px solid black; padding: 8px; text-align: left; background-color: #f2f2f2;">MARKS & NOS</th>' +
                '<th style="border: 1px solid black; padding: 8px; text-align: left; background-color: #f2f2f2;">QUANTITY AND UNIT</th>' +
                '<th style="border: 1px solid black; padding: 8px; text-align: left; background-color: #f2f2f2;">DESCRIPTION OF GOODS</th>' +
                '<th style="border: 1px solid black; padding: 8px; text-align: left; background-color: #f2f2f2;">GROSS WEIGHT</th>' +
                '</tr>'

            $.each(data, function (i, item) {
                //var filename = item.project_name;
                //alert("Table ID"+item.id);
                op += "<tr>";
                op += " <td style=\"border: 1px solid black; padding: 8px; text - align: left;\">";
                op += "" + item.blno + "";
                op += "</td>";

                //op += "<tr>";
                op += " <td style=\"border: 1px solid black; padding: 8px; text - align: left;\">";
                op += "" + item.shippername + "";
                op += "</td>";

                //op += "<tr>";
                op += " <td style=\"border: 1px solid black; padding: 8px; text - align: left;\">";
                op += "" + item.consignee + "";
                op += "</td>";

                //op += "<tr>";
                op += " <td style=\"border: 1px solid black; padding: 8px; text - align: left;\">";
                op += "" + item.notify_name + "";
                op += "</td>";

                //op += "<tr>";
                op += " <td style=\"border: 1px solid black; padding: 8px; text - align: left;\">";
                op += "" + item.marks_and_nos + "";
                op += "</td>";

                //op += "<tr>";
                op += " <td style=\"border: 1px solid black; padding: 8px; text - align: left;\">";
                op += "" + item.Quantity_and_Kind_of_for_cargo + "";
                op += "</td>";

                //op += "<tr>";
                op += " <td style=\"border: 1px solid black; padding: 8px; text - align: left;\">";
                op += "" + item.cargotype_desc + "";
                op += "</td>";

                //op += "<tr>";
                op += " <td style=\"border: 1px solid black; padding: 8px; text - align: left;\">";
                op += "" + item.quantity_each + "";
                op += "</td>";
                op += "</tr>";

            });
            op += '</table >';
            $("#tbldata").html('');
            $("#tbldata").append(op);
            


        }
    });
}
$('body').on('click', '#btnApproved', function () {
    swal({
        title: "Are you sure?",
        text: "Once Approved, you will not be able to change.",
        icon: "warning",
        buttons: true,
        dangerMode: true,
    })
    .then((willDelete) => {
            if (willDelete) {
                $.get("/InBound/Approved", {
                    auto_id: $("#auto_id").val(),
                    id: $("#id").val()
                }, function (data) {
                    swal(data.Title, data.Message, data.Type).then(function () {
                        window.location.href = '/InBound/Index';
                    });
                });
            }
        });
})
//