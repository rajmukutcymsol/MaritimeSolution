$(document).ready(function () {
    //LoadData();
})
$("#projectid").change(function () {
    BindTools();
});
$("#toolid").change(function () {
    LoadData();
});

function BindTools() {
    var request = {
        projectid: $("#projectid").val(),
        toolid: $("#toolid").val()
    }
  
    $.ajax({
        url: "/ToolUseCasesMapping/GeToolstByProjectId",
        data: JSON.stringify(request),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (data) {
            var op = "";
            op += "<option value='0'>" + '--select--';
            $.each(data, function (i, item) {
                op += "<option value='" + item.toolid + "'>" + item.tool_name;
            });
            $("#toolid").html('');
            $("#toolid").append(op);
            //LoadData();
        }
    });
}
function save() {
    var id = $("#usecaseid").val();
    var projectRequirement = new FormData();
    projectRequirement.append("toolid", $("#toolid").val());
    projectRequirement.append("usecaseid", $("#usecaseid").val());
    projectRequirement.append("projectid", $("#projectid").val());
    projectRequirement.append("venderid", $("#venderid").val());
    projectRequirement.append("technologyid", $("#technologyid").val());
    projectRequirement.append("nodetypeid", $("#nodetypeid").val());
    //console.log(projectRequirement);
    $.ajax(
        {
            url: '/ToolUseCasesMapping/Save',
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
        //      {
        //          url: '/UserProjectsMapping/Save',
        //          data: '{"employee_id":"' + employee_id +'","id":"'+id+'"}',
        //          type: "POST",
        //          contentType: "application/json;charset=utf-8",
        //          dataType: "json",
        //      success: function (data) {
        //          swal(data.Title, data.Message, data.Type).then(function () {
        //              window.location.reload();
        //          });
        //      },
        //      failure: function (errorMessage) {
        //          swal("Oops!", errorMessage, "failure");
        //      }
        //}
    );
}

$("#btnSave").click(function () {
    save();
})
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
                var $item = $(this).closest("tr")   // Finds the closest row <tr> 
                    .find(".nr")     // Gets a descendent with class="nr"
                    .text();
                $.get("/ProjectToolsMapping/Delete", { id: $item }, function (data) {
                    swal(data.Title, data.Message, data.Type).then(function () {
                        //alert(data.Title);
                        LoadData();
                    });
                });
            }
        });
});
function LoadData() {
    var request = {
        toolid: $("#toolid").val(),
        projectid: $("#projectid").val()
    }
    //alert(request.tool_id);
    $.ajax({
        url: "/ToolUseCasesMapping/GetByProjectNameWithToolId",
        data: JSON.stringify(request),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('#tbl_UseCase').DataTable({
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
                        },
                    },
                    { 'data': 'project_name' },
                    { 'data': 'tool_name' },
                    { 'data': 'use_case_name' },
                    { 'data': 'vendor_name' },
                    { 'data': 'technology_name' },
                    { 'data': 'node_type_name' },
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
                    $.get("/ToolUseCasesMapping/Delete", { id: $(this).closest('tr').find('td:eq(0) input').val() }, function (data) {
                        swal(data.Title, data.Message, data.Type).then(function () {
                            //alert(data.Title);
                            LoadData();
                        });
                    });
                }
            });
    });
}