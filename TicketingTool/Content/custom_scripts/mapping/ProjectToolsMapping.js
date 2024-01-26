$(document).ready(function () {
    //loadData();
})

$("#projectid").change(function () {
    LoadData();
});

function save() {
   // var id = $("#projectid").val();
    //alert(employee_id);
    //alert(id);
    var projectRequirement = new FormData();
    projectRequirement.append("projectid", $("#projectid").val());
    projectRequirement.append("toolid", $("#toolid").val());
    //console.log(projectRequirement);
    $.ajax(
        {
            url: '/ProjectToolsMapping/Save',
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

function LoadData() {
    var request = {
        projectid: $("#projectid").val()
    }
    //alert(request.projectid);
    $.ajax({
        url: "/ProjectToolsMapping/GetById",
        data: JSON.stringify(request),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (data) {
            var op = '<table class="table table-hover text-nowrap">' +
                '<thead>' +
                '<tr>' +
                /*'<th>id</th>' +*/
                '<th>Project Name</th>' +
                '<th>Tool Name</th>' +
                '<th>Delete</th>' +
                '</tr> ' +
                '</thead> ' +
                '<tbody>';

            $.each(data, function (i, item) {
                //var filename = item.project_name;
                //alert("Table ID" + item.tool_name);
                op += "<tr>";
                op += " <td style=\"display: none;\" class=\"nr\">";
                op += "" + item.id + "";
                op += "</td>";

                op += "<td>";
                op += "<p>" + item.project_name + "</p>";
                op += "</td>";
                op += "<td>";
                op += "<p>" + item.tool_name + "</p>";
                op += "</td>";

                op += " <td>";
                op += "<input type=\"hidden\" value='" + item.id + "'/> <a href=\"/IssueRequest/Edit/'" + item.id + "'\" style=\"margin-right:5px;\"><a href=\"#\" class=\"btnDelete\" style=\"margin-right: 5px;\"><i class=\"fa fa-trash\"></i></a>";
                op += "</td>";
                op += "</tr>";

            });
            op += '</tbody>' +
                '</table>';
            $("#tbldata").html('');
            $("#tbldata").append(op);


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
}