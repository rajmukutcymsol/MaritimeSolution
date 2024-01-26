$(document).ready(function () {
    loadData();
})
function loadData() {
    $.get("/Vessel/GetAll", function (data) {
        $('#tbl_cliui').DataTable({
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
                { 'data': 'vesselCode' },
                { 'data': 'vesselName' },
                /*{ 'data': 'LastPortofcall' },*/
                { 'data': 'imoNumber' },
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

$('body').on('click', '#btnAdd', function () {
    $('#modal-default').modal('show');
})

$("#btnSave").click(function () {
    if ($("#btnSave").text().trim() == "Save")
        save();
    if ($("#btnSave").text().trim() == "Update")
        update();
})



function save() {

    var is_active = false;
    if ($('#is_active').is(':checked'))
        is_active = true;

    var request = {
        vesselName: $("#vesselName").val(),
        vesselCode: $("#vesselCode").val(),
        deadweight: $("#deadweight").val(),
        loa: $("#loa").val(),
        maxDraft: $("#maxDraft").val(),
        beam: $("#beam").val(),
        grt: $("#grt").val(),
        nrt: $("#nrt").val(),
        flags: $("#flags").val(),
        callSign: $("#callSign").val(),
        imoNumber: $("#imoNumber").val(),
        hatchHolds: $("#hatchHolds").val(),
        swl: $("#swl").val(),
        vesselOthers: $("#vesselOthers").val(),
        is_active: is_active,
        LastPortofcall: $("#LastPortofcall").val(),
        piclub: $("#piclub").val(),
        ClassificationSociety: $("#ClassificationSociety").val(),
        Depth: $("#Depth").val()
    };

    $.ajax({
        url: '/Vessel/Save',
        data: JSON.stringify(request),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (data) {
            swal(data.Title, data.Message, data.Type).then(function () {
                loadData();
                $("#modal-default").modal('hide');
            });
        },
        failure: function (errorMessage) {
            swal("Oops!", errorMessage, "failure");
        }
    });
}


function update() {
    //var res = validate();
    //if (res == false)
    //    return false;
    var is_active = false;
    if ($('#is_active').is(':checked'))
        is_active = true;

    var request = {
        vesselName: $("#vesselName").val(),
        vesselCode: $("#vesselCode").val(),
        deadweight: $("#deadweight").val(),
        loa: $("#loa").val(),
        maxDraft: $("#maxDraft").val(),
        beam: $("#beam").val(),
        grt: $("#grt").val(),
        nrt: $("#nrt").val(),
        flags: $("#flags").val(),
        callSign: $("#callSign").val(),
        imoNumber: $("#imoNumber").val(),
        hatchHolds: $("#hatchHolds").val(),
        swl: $("#swl").val(),
        vesselOthers: $("#vesselOthers").val(),
        is_active: is_active,
        LastPortofcall: $("#LastPortofcall").val(),
        piclub: $("#piclub").val(),
        ClassificationSociety: $("#ClassificationSociety").val(),
        Depth: $("#Depth").val(),
        id: $("#id").val()

    };

    $.ajax({
        url: '/Vessel/Update',
        data: JSON.stringify(request),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (data) {

            swal(data.Title, data.Message, data.Type).then(function () {
                loadData();
                $("#btnSave").text('Save');
                $("#modal-default").modal('hide');
            });
        },
        failure: function (errorMessage) {
            swal("Oops!", errorMessage, "failure");
        }
    });
}

$('body').on('click', '.btnEdit', function () {
    $("#id").val($(this).closest('tr').find('td:eq(0) input').val());
    $.get("/Vessel/GetById", { id: $(this).closest('tr').find('td:eq(0) input').val() }, function (data) {
        $("#vesselCode").val(data.vesselCode);
        $("#vesselName").val(data.vesselName);
        $("#deadweight").val(data.deadweight);
        $("#loa").val(data.loa);
        $("#maxDraft").val(data.maxDraft);
        $("#beam").val(data.beam);
        $("#grt").val(data.grt);
        $("#nrt").val(data.nrt);
        $("#flags").val(data.flags);
        $("#callSign").val(data.callSign);
        $("#imoNumber").val(data.imoNumber);
        $("#hatchHolds").val(data.hatchHolds);
        $("#swl").val(data.swl);
        $("#vesselOthers").val(data.vesselOthers);
        $("#piclub").val(data.piclub);
        $("#classificationSociety").val(data.classificationSociety);
        $("#depth").val(data.depth);
        if (data.is_active == true)
            $('#is_active').prop("checked", true);
        else
            $('#is_active').prop("checked", false);

        $("#btnSave").text('Update');
        $("#modal-default").modal('show');
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
                $.get("/Vessel/Delete", { id: $(this).closest('tr').find('td:eq(0) input').val() }, function (data) {
                    swal(data.Title, data.Message, data.Type).then(function () {
                        loadData();
                    });
                });
            }
        });
});




function validate() {
    var isValid = true;
    if ($('#region_name').val().trim() == "") {
        $('#region_name').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#region_name').css('border-color', 'lightgrey');
    }
    return isValid;
}
