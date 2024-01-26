$(document).ready(function () {
    //loadData();
})

$("#employee_id").change(function () {
    LoadData();
});

function save() {
    //var employee_id = $("#employee_id").val();
    //var id = $("#id").val();
    //alert(employee_id);
    var projectRequirement = new FormData();
    projectRequirement.append("id", $("#id").val());
    projectRequirement.append("employee_id", $("#employee_id").val());
    $.ajax(
        {
            url: '/UserProjectsMapping/Save',
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
        employee_id: $("#employee_id").val()
    }
    //alert(request.employee_id);
    $.ajax({
        url: "/UserProjectsMapping/GetUserProjectMappingByEmployeeId",
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
                '<th>Delete</th>' +
                '</tr> ' +
                '</thead> ' +
                '<tbody>';

            $.each(data, function (i, item) {
                //var filename = item.project_name;
                //alert("Table ID"+item.id);
                op += "<tr>";
                op += " <td style=\"display: none;\" class=\"nr\">";
                op += "" + item.projectid + "";
                op += "</td>";

                op += "<td>";
                op += "<p>" + item.project_name+"</p>";
                //op += "<a href=\"/uploads/mop_sop_attachment/" + item.project_name + "\" target=\"_blank\" style=\"margin-right:5px;\"><i class=fa fa-edit\">" + item.project_name + "</i></a>";
                op += "</td>";

                op += " <td>";
                op += "<input type=\"hidden\" value='" + item.projectid + "'/> <a href=\"/IssueRequest/Edit/'" + item.projectid + "'\" style=\"margin-right:5px;\"><a href=\"#\" class=\"btnDelete\" style=\"margin-right: 5px;\"><i class=\"fa fa-trash\"></i></a>";
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
                        .text().toUpperCase();
                    //alert($item);
                    $.get("/UserProjectsMapping/Delete", { id: $item }, function (data) {
                        swal(data.Title, data.Message, data.Type).then(function () {
                            //alert(data.Title);
                            LoadData();
                        });
                    });
                }
            });
    });
}