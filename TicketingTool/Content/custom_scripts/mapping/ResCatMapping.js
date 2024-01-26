$(document).ready(function () {
    LoadData();
})

function save() {
    var projectRequirement = new FormData();
    projectRequirement.append("res_cat1", $("#res_cat1").val());
    projectRequirement.append("res_cat2", $("#res_cat2").val());
    projectRequirement.append("res_cat3", $("#res_cat3").val());
    $.ajax(
        {
            url: '/ResCatMapping/Save',
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
    $.ajax({
        url: "/ResCatMapping/GetAll",
        data: null,
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('#tbl_ResCat').DataTable({
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
                    { 'data': 'res_cat1_name' },
                    { 'data': 'res_cat2_name' },
                    { 'data': 'res_cat3_name' },
                   
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
                    alert($(this).closest('tr').find('td:eq(0) input').val() );
                    $.get("/ResCatMapping/Delete", { id: $(this).closest('tr').find('td:eq(0) input').val() }, function (data) {
                        swal(data.Title, data.Message, data.Type).then(function () {
                            LoadData();
                        });
                    });
                }
            });
    });
}