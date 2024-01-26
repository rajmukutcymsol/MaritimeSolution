$(document).ready(function () {
    //LoadData();
})
$("#group_id").change(function () {
    LoadData();
});
function save() {

    if ($("#group_id").val() == "00000000-0000-0000-0000-000000000000" || $("#group_id").val() == "") {
        return;
    }
    if ($("#employee_id").val() == "--Select--"){
        return;
    }
    var projectRequirement = new FormData();
    projectRequirement.append("group_id", $("#group_id").val());
    projectRequirement.append("employee_id", $("#employee_id").val());
   
    $.ajax(
        {
            url: '/GroupEmailMapping/Save',
            type: 'POST',
            data: projectRequirement,
            processData: false,  // tell jQuery not to process the data
            contentType: false,  // tell jQuery not to set contentType
            success: function (data) {
                swal(data.Title, data.Message, data.Type).then(function () {
                    LoadData();
                });
            },
            failure: function (errorMessage) {
                swal("Oops!", errorMessage, "failure");
            }
        }
    );
}

$("#btnSave").click(function () {
    save();
})

function LoadData() {
    var request = {
        group_id: $("#group_id").val(),
        
    }
    $.ajax({
        url: "/GroupEmailMapping/GetAll",
        data: JSON.stringify(request),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('#tbl_GroupEmail').DataTable({
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
                            return '<input type="hidden" value=' + data + '><a href="#" class="btnDelete" style="margin-right: 5px;"><i class="fa fa-trash"></i></a>';
                            //return '<input type="hidden" value=' + data + '><a href="#" class="btnDelete" style="margin-right: 5px;"><i class="fa fa-trash"></i></a>';
                        },
                    },
                    { 'data': 'group_name' },
                    { 'data': 'employee_name' },
                    { 'data': 'email_id' },

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


        }
    });
    $('body').on('click', '.btnDelete', function () {
        //alert('del');
        swal({
            title: "Are you sure?",
            text: "Once deleted, you will not be able to recover this role",
            icon: "warning",
            buttons: true,
            dangerMode: true,
        })
            .then((willDelete) => {
                if (willDelete) {
                    $.get("/GroupEmailMapping/Delete", { id: $(this).closest('tr').find('td:eq(0) input').val() }, function (data) {
                        swal(data.Title, data.Message, data.Type).then(function () {
                            LoadData();
                        });
                    });
                }
            });
    });
}