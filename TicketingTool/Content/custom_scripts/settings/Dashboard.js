$(document).ready(function () {
        // Fetch the partial view and insert it into the #partialViewContainer
        $("#partialViewContainer").load("/Dashboard/PartialViewAction", function (responseText, textStatus, xhr) {
            // Handle any errors, if necessary
            if (textStatus === "error") {
                console.error(xhr.status + ": " + xhr.statusText);
            }
        });

    $("#Project").hide();
    $("#Domain").hide();
});
$("#QuickFilters").change(function () {
    if ($("#QuickFilters").val() == "1") {
        $("#Project").show();
        $("#Domain").hide();
        BindProject();
    }
    else if ($("#QuickFilters").val() == "2") {
       
        $("#Project").hide();
        $("#Domain").show();
        BindDomain();
    }
    else {
        // Fetch the partial view and insert it into the #partialViewContainer
        $("#partialViewContainer").load("/Dashboard/PartialViewAction", function (responseText, textStatus, xhr) {
            // Handle any errors, if necessary
            if (textStatus === "error") {
                console.error(xhr.status + ": " + xhr.statusText);
            }
        });
        $("#Project").hide();
        $("#Domain").hide();
    }
})
function BindProject() {
    var sessionValue = $('#sessionValue').val();
    var sessionUserName = $('#sessionUserName').val();

    var request = {
        employee_id: sessionUserName,
        user_role: sessionValue
    }
    $.ajax({
            url: "/Dashboard/Projectsforusers",
            data: JSON.stringify(request),
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (data) {
                var op = "";
                op += "<option value='0'>" + '--Select Project Name--';
                //op += "<option value='A'>" + 'All';
                $.each(data, function (i, item) {
                    op += "<option value='" + item.id + "'>" + item.project_name;
                });

                $("#project_name").html('');
                $("#project_name").append(op);
                //BindVendor();

            }
        });
    
}

$("#project_name").change(function () {
    var selectedValue = $(this).val();
    if (selectedValue == 'A') {
        $("#partialViewContainer").load("/Dashboard/PartialViewAction", function (responseText, textStatus, xhr) {
            // Handle any errors, if necessary
            if (textStatus === "error") {
                console.error(xhr.status + ": " + xhr.statusText);
            }
        });
    }
    else {
        // Make an AJAX request to set the session variable
        $.ajax({
            url: '/Dashboard/SetProjectName', // Replace with the actual URL of your server-side code
            data: { projectName: selectedValue }, // Send the selected value as a parameter
            type: 'POST', // Use 'POST' method to pass data to the server
            success: function () {
                $("#partialViewContainer").load("/Dashboard/PartialViewAction", function (responseText, textStatus, xhr) {
                    // Handle any errors, if necessary
                    if (textStatus === "error") {
                        console.error(xhr.status + ": " + xhr.statusText);
                    }
                });
            },
            error: function (xhr, status, error) {
                // Handle any errors, if necessary
                console.error(error);
            }
        });
    }
});
function BindDomain() {
   
    $.ajax({
        url: "/Dashboard/DomainNameforusers",
        data: null,
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (data) {
            var op = "";
            op += "<option value='0'>" + '--Select Domain Name--';
            //op += "<option value='A'>" + 'All';
            $.each(data, function (i, item) {
                op += "<option value='" + item.id + "'>" + item.domain_name;
            });

            $("#domain_name").html('');
            $("#domain_name").append(op);
            //BindVendor();

        }
    });

}
$("#domain_name").change(function () {
    var selectedValue = $(this).val();
    if (selectedValue == 'A') {
        $("#partialViewContainer").load("/Dashboard/PartialViewAction", function (responseText, textStatus, xhr) {
            // Handle any errors, if necessary
            if (textStatus === "error") {
                console.error(xhr.status + ": " + xhr.statusText);
            }
        });
    }
    else {
        // Make an AJAX request to set the session variable
        $.ajax({
            url: '/Dashboard/SetDomainName', // Replace with the actual URL of your server-side code
            data: { domain_name: selectedValue }, // Send the selected value as a parameter
            type: 'POST', // Use 'POST' method to pass data to the server
            success: function () {
                $("#partialViewContainer").load("/Dashboard/PartialViewAction", function (responseText, textStatus, xhr) {
                    // Handle any errors, if necessary
                    if (textStatus === "error") {
                        console.error(xhr.status + ": " + xhr.statusText);
                    }
                });
            },
            error: function (xhr, status, error) {
                // Handle any errors, if necessary
                console.error(error);
            }
        });
    }
});
