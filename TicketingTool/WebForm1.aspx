<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="TicketingTool.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
       <script>
           // Attach click event handler to the "Add" button
           $('#addRowBtn').on('click', function () {
               addRow.call(this); // Call the addRow function in the context of the button
           });

           // Your addRow function (as defined earlier)
           function addRow() {
               var isValid = true;
               var numHutch = @Model.capacities; // Replace this with the actual value of HOLD columns

               // Check if all input fields in the current row have values
               $(this).closest('tr').find('input').each(function () {
                   if ($(this).val() === '') {
                       isValid = false;
                       alert('Please fill in all fields before adding a new row.');
                       return false; // Exit the loop early
                   }
               });

               if (isValid) {
                   var newRow = '<tr>';

                   // Add destination input field
                   newRow += '<td><input type="text" class="form-control" placeholder="Enter destination"></td>';

                   // Add HOLD input fields based on the value of numHutch
                   for (var i = 0; i < numHutch; i++) {
                       newRow += '<td>';
                       newRow += 'Enter Weight: <input type="text" class="form-control destination"><br>';
                       newRow += 'Select Marks & Nos: <select class="form-control input-sm" id="marks_and_nos_name' + (i + 1) + '" name="marks_and_nos_name' + (i + 1) + '"></select><br>';
                       newRow += 'Select Type of Cargo: <select class="form-control input-sm" id="cargo_type_name' + (i + 1) + '" name="cargo_type_name' + (i + 1) + '"><option value="b79d934e-fc2d-ee11-b020-782b4610c006">Fertilizer</option><!-- Add your options here --></select><br>';
                       newRow += 'Select Receiver: <select class="form-control input-sm" id="receiver_name' + (i + 1) + '" name="ReceiverId' + (i + 1) + '"><option value="00000000-0000-0000-0000-000000000000">--Select--</option><!-- Add your options here --></select>';
                       newRow += '</td>';

                       // Make an AJAX call to populate the "Select Marks & Nos" dropdown
                       $.ajax({
                           url: '/api/marksandnos', // Replace with your server-side endpoint URL
                           method: 'GET',
                           dataType: 'json',
                           success: function (data) {
                               // Populate the dropdown with the received data
                               var marksAndNosDropdown = $('#marks_and_nos_name' + (i + 1));
                               marksAndNosDropdown.empty(); // Clear existing options
                               $.each(data, function (index, item) {
                                   marksAndNosDropdown.append($('<option>', {
                                       value: item.id,
                                       text: item.name
                                   }));
                               });
                           },
                           error: function () {
                               console.error('Failed to load Marks & Nos data.');
                           }
                       });
                   }

                   // Add total input field
                   newRow += '<td><input type="text" class="form-control destination"></td>';

                   // Add buttons
                   newRow += '<td>' +
                       '<button class="btn btn-success addRow">' +
                       '<i class="fa fa-plus"></i>' +
                       '</button>' +
                       '<button class="btn btn-danger deleteRow" id="deleteRowBtn">' +
                       '<i class="fa fa-trash"></i>' +
                       '</button>' +
                       '</td>' +
                       '</tr>';

                   $(this).closest('tr').after(newRow);
               }
           }
       </script>

            
    </form>
</body>
</html>
