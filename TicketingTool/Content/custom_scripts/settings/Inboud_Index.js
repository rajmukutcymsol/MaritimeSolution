$(document).ready(function () {
    $("#masterdetails_section").hide();
    $("#masterdetails").hide();
    $("#inward_section").hide();
    $("#inward").hide();
    $("#manifest").hide();
    $('#btnSave').hide();
    $('#etanotice').hide();
    $('#etanotice_section').hide();

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
})

$('body').on('click', '.btnProcess', function () {
    var res = validate();
    if (res == false)
        return false;
    var projectRequirement = new FormData();
    $('#btnSave').show();

    projectRequirement.append("auto_id", $("#auto_id").val());
    projectRequirement.append("vessel_name", $("#vessel_name").val());
    projectRequirement.append("discharge_port", $("#discharge_port").val());
    projectRequirement.append("EAT_HH", $("#EAT_HH option:selected").text());
    projectRequirement.append("EAT_MM", $("#EAT_MM option:selected").text());
    projectRequirement.append("EAT_Date", $("#EAT_Date").val());
    projectRequirement.append("refNo", $("#refNo").val());
    projectRequirement.append("rcn", $("#rcn").val());
    projectRequirement.append("LoadPort", $("#LoadPort").val());
  
    $.ajax({
        url: '/InBound/Save',
        type: 'POST',
        data: projectRequirement,
        processData: false,  // tell jQuery not to process the data
        contentType: false,  // tell jQuery not to set contentType
        success: function (data) {
            swal(data.Title, data.Message, data.Type).then(function () {

                $("#vessel_name").prop("disabled", true);
                $("#discharge_port").prop("disabled", true);
                $("#eat_display").prop("disabled", true);
                $("#EAT_HH").prop("disabled", true);
                $("#EAT_MM").prop("disabled", true);
                $("#EAT_Date").prop("disabled", true);
                $("#refNo").prop("disabled", true);
                $("#rcn").prop("disabled", true);
                $("#LoadPort").prop("disabled", true);

                $("#masterdetails_section").show();
                $("#masterdetails").show();
                $("#inward_section").show();
                $("#inward").show();

                $("#etanotice_section").show();
                $("#etanotice").show();

                $("#btnProcess").hide();
            });
        },
        failure: function (errorMessage) {
            swal("Oops!", errorMessage, "failure");
        }
    });
});
function validate() {
    var isValid = true;
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
    //if ($('#refNo').val().trim() == "") {
    //    $('#refNo').css('border-color', 'Red');
    //    isValid = false;
    //}
    //else {
    //    $('#refNo').css('border-color', 'lightgrey');
    //}
    //if ($('#rcn').val().trim() == "") {
    //    $('#rcn').css('border-color', 'Red');
    //    isValid = false;
    //}
    //else {
    //    $('#rcn').css('border-color', 'lightgrey');
    //}
    if ($('#LoadPort').val().trim() == "") {
        $('#LoadPort').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#LoadPort').css('border-color', 'lightgrey');
    }
    
    return isValid;
}
$('body').on('click', '.btnsave', function () {
    var res = finalsave();
    if (res == false)
        return false;

    var projectRequirement = new FormData();

    projectRequirement.append("mastername", $("#mastername").val());
    projectRequirement.append("mastercontactnumber", $("#mastercontactnumber").val());
    projectRequirement.append("masteremail", $("#masteremail").val());
    projectRequirement.append("owners_name", $("#owners_name").val());
    projectRequirement.append("receiver_name", $("#receiver_name").val());
    projectRequirement.append("surveyor_name", $("#surveyor_name").val());
    projectRequirement.append("stevedore_name", $("#stevedore_name").val());
    projectRequirement.append("shipping_name", $("#shipping_name").val());
    projectRequirement.append("checker_name", $("#checker_name").val());
    projectRequirement.append("auto_id", $("#auto_id").val());
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
        url: '/InBound/Update',
        type: 'POST',
        data: projectRequirement,
        processData: false,  // tell jQuery not to process the data
        contentType: false,  // tell jQuery not to set contentType
        success: function (data) {
            swal(data.Title, data.Message, data.Type).then(function () {
                $("#vessel_name").prop("disabled", true);
                $("#discharge_port").prop("disabled", true);
                $("#eat_display").prop("disabled", true);
                $("#EAT_HH").prop("disabled", true);
                $("#EAT_MM").prop("disabled", true);
                $("#EAT_Date").prop("disabled", true);
                $("#refNo").prop("disabled", true);
                $("#rcn").prop("disabled", true);
                $("#LoadPort").prop("disabled", true);

                $("#masterdetails_section").show();
                $("#masterdetails").show();
                $("#inward_section").show();
                $("#inward").show();
                $("#btnProcess").hide();
            });
        },
        failure: function (errorMessage) {
            swal("Oops!", errorMessage, "failure");
        }
    });

})

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
    var isChecked = $("#is_cleanonBoard").prop("checked");

    if (isChecked) {

        isChecked = true;
    } else {
        isChecked = false;
    }
    var projectRequirement = new FormData();

    //projectRequirement.append("id", $("#id").val());
    projectRequirement.append("auto_id", $("#auto_id").val());
    projectRequirement.append("blno", $("#blno").val());
    projectRequirement.append("shipper_name", $("#shipper_name").val());
    projectRequirement.append("consignee", $("#consignee").val());
    projectRequirement.append("notify", $("#notify").val());
    projectRequirement.append("marks_and_nos_name", $("#marks_and_nos_name").val());
    projectRequirement.append("quantity_and_kind_of_cargo_name", $("#quantity_and_kind_of_cargo_name").val());
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
                $("#vessel_name").prop("disabled", true);
                $("#discharge_port").prop("disabled", true);
                $("#eat_display").prop("disabled", true);
                $("#EAT_HH").prop("disabled", true);
                $("#EAT_MM").prop("disabled", true);
                $("#EAT_Date").prop("disabled", true);
                $("#refNo").prop("disabled", true);
                $("#rcn").prop("disabled", true);
                $("#LoadPort").prop("disabled", true);

                $("#masterdetails_section").show();
                $("#masterdetails").show();
                $("#inward_section").show();
                $("#inward").show();

                $("#etanotice_section").show();
                $("#etanotice").show();

                $("#btnProcess").hide();
             
                $("#blno").val('');
                $("#shipper_name").val();
                $("#consignee").val('');
                $("#quantity").val('');
                $("#cargotype_desc").val('');
                $("#manifest").show();
                $('#etanotice').show();
                $('#etanotice_section').show();
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
    if ($('#Quantity_and_Kind_of_cargo_name').val()== "00000000-0000-0000-0000-000000000000") {
        $('#Quantity_and_Kind_of_cargo_name').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#Quantity_and_Kind_of_cargo_name').css('border-color', 'lightgrey');
    }
    if ($('#cargo_type_name').val()== "00000000-0000-0000-0000-000000000000") {
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
function loadData() {
    //$.get("/ChangeRequest/Delete", { id: $(this).closest('tr').find('td:eq(0) input').val() }, function (data) {
    //swal(data.Title, data.Message, data.Type).then(function () {
        //LoadData();
    //});
//});
    $.get("/InBound/GetManiFestByAutoId", { auto_id: $("#auto_id").val() }, function (data) {
        $('#tbl_InBoundManiFest').DataTable({
            "processing": true, // for show progress bar
            "serverSide": false, // for process server side
            "filter": true, // this is for disable filter (search box)
            "orderMulti": false, // for disable multiple column at once
            "searching": true,
            "destroy": true,
            "data": data,
            "columns": [
                //{
                //    "data": "id",
                //    "orderable": false,
                //    "render": function (data, type, row, meta) {
                //        return '<input type="hidden" value=' + data + '>';
                //    },
                //},
                { 'data': 'blno' },
                { 'data': 'shippername' },
                { 'data': 'consignee_name' },
                { 'data': 'notify_name' },
                { 'data': 'marks_and_nos' },
                { 'data': 'quantity' },

                //{
                //    data: "is_active",
                //    "searchable": false,
                //    "orderable": false,
                //    "render": function (data, type, row) {
                //        if (data == false)
                //            return '<span class="label label-danger">In-Active</span>';
                //        else
                //            return '<span class="label label-success">Active</span>';
                //    }

                //}


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
