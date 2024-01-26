$(document).ready(function () {
    //LoadData();
})

$("#project").change(function () {
    LoadData();
});

function save() {
    var projectRequirement = new FormData();
    projectRequirement.append("project", $("#project").val());
    projectRequirement.append("region", $("#region").val());
    $.ajax(
        {
            url: '/ProjectRegionMapping/Save',
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
        project: $("#project").val()
    }
    $.ajax({
        url: "/ProjectRegionMapping/GetById",
        data: JSON.stringify(request),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('#tbl_ProjectRegion').DataTable({
                "processing": true, // for show progress bar
                "serverSide": false, // for process server side
                "filter": true, // this is for disable filter (search box)
                "orderMulti": false, // for disable multiple column at once
                "searching": true,
                "destroy": true,
                "data": data,
                "columns": [
                    { 'data': 'project_name' },
                    { 'data': 'region_name' },
                    {
                        "data": "id",
                        "orderable": false,
                        "render": function (data, type, row, meta) {
                            return '<input type="hidden" value=' + data + '><a href="#" class="btnDelete" style="margin-right: 5px;"><i class="fa fa-trash"></i></a>';
                        },
                    },
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
                    //var $item = $(this).closest("tr")   // Finds the closest row <tr> 
                    //    .find(".nr")     // Gets a descendent with class="nr"
                    //    .text();
                    $.get("/ProjectRegionMapping/Delete", { id: $(this).closest('tr').find('td:eq(2) input').val() }, function (data) {
                        swal(data.Title, data.Message, data.Type).then(function () {
                            //alert(data.Title);
                            LoadData();
                        });
                    });
                }
            });
    });
}