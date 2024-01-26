$(document).ready(function () {
    loadData();
})

$("#role").change(function () {
    loadData();
});


function loadData() {
    $(".minimal").attr('checked', false);
    $.get("/RoleMenuMapping/GetMappedMenu", { id: $("#role").val() }, function (data) {
        $.each(data, function (key, item) {
            if (item.is_active == true)
                $("#" + item.menuid).attr('checked', true);
            else
                $("#" + item.menuid).attr('checked', false);

            
        });

    });
};


function save() {
    var menuMappingList = [];
   

    $("#tblMenu tr").each(function () {
        var roleMenu = {};
        roleMenu.roleid = $("#role").val();
        roleMenu.menuid = $(this).find("input[type=hidden]").val();
        if ($(this).find("input[type=checkbox]").is(':checked'))
            roleMenu.is_active = true;
        else
            roleMenu.is_active = false;
        menuMappingList.push(roleMenu);
    })


    $.ajax({
        url: '/RoleMenuMapping/Save',
        data: JSON.stringify(menuMappingList),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (data) {
            swal(data.Title, data.Message, data.Type).then(function () {
                window.location.reload();
            });
        },
        failure: function (errorMessage) {
            swal("Oops!", errorMessage, "failure");
        }
    });
}

$("#btnSave").click(function () {
    save();
})