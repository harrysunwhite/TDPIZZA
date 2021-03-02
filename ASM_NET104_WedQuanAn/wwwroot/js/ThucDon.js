function readURLc(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $('#imgTD').attr('src', e.target.result);
        }

        reader.readAsDataURL(input.files[0]);
    }
}

$("#imgfile").change(function () {
    readURLc(this);

});

function readURL(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $('#imgTDu').attr('src', e.target.result);
        }

        reader.readAsDataURL(input.files[0]);
    }
}

$("#imgfileu").change(function () {
    readURL(this);

});


function _getByTDId(id) {

    $.ajax({
        url: '/api/thucdon/' + id,

        type: "GET",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            $('#formEdit').attr('Action', 'Edit/' + id);
            $('#MaTD').val(id);
            $('#TenTD').val(result.TenTd);
            $('#MoTaTD').val(result.MoTa);
            $('#imgTDu').attr('src', '/img/' + result.Hinh);
            $('#HinhTD').val(result.Hinh);;
            $('#NhomTD').prop('selectedIndex', result.Nhom - 1);

            $('#GiaTD').val(result.Price)


            $('#myModal2').modal('show');
            $('#btnUpdateTD').show();
            $('#btnAddTD').hide();
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
    return false;
}

function _deleteTD(id) {
    var cf = confirm('Bạn muốn xoá thực đơn này?');
    if (cf) {
        $.ajax({
            url: '/api/ThucDon/' + id,
            type: "DELETE",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: true,
            statusCode: {
                200: function () {
                    location.reload();
                }
            }



        });
    }
}
