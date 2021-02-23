

$(document).ready(
    function () {
    _getAll();
});

function _getAll() {
    $.ajax({
        url: "api/Nhom",
        type: "GET",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            var html = '';
            $.each(result, function (key, item) {
                html += '<tr>';
                html += '<td>' + item.MaNhom + '</td>';
                html += '<td>' + item.TenNhom + '</td>';
                html += '<td><a class="btn btn-secondary btn-lg active" onclick="return _getById(' + item.MaNhom + ')">Edit</a> | <a  class="btn btn-secondary btn-lg active"  onclick="return _delete(' + item.MaNhom + ')">Delete</a></td>';
                html += '</tr>';
            });
            $('#list tbody').html(html);
        },
        error: function (errormessage) {
           
            alert(errormessage.responseText);
        }
    });

    $('#btnUpdate').hide();
    $('#btnAdd').show();
    return false;
}
function _getById(id) {
    $.ajax({
        url: '/api/nhom/' + id,
        // data: JSON.stringify(dto),
        type: "GET",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            $('#idNhom').val(result.MaNhom);
            $('#NameNhom').val(result.TenNhom);
           

            $('#myModal').modal('show');
            $('#btnUpdate').show();
            $('#btnAdd').hide();
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
    return false;
}
function _add() {
    var obj = {
        MaNhom: 1,
        TenNhom: $('#NameNhom').val(),
        
    }
    $.ajax({
        url: '/api/Nhom',
        data: JSON.stringify(obj),
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        statusCode: {
            200: function () {
                _getAll();
                $('#myModal').modal('hide');
            }
        },
       
        
      

    });
}
function _edit() {
    var obj = {
        MaNhom: $('#idNhom').val(),
        TenNhom: $('#NameNhom').val(),
       
    }

    $.ajax({
        url: '/api/Nhom',
        data: JSON.stringify(obj),
        type: "PUT",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        statusCode: {
            200: function () {
                _getAll();
                $('#myModal').modal('hide');
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        }        
    });
}
function _delete(id) {
    var cf = confirm('Are you sure want to permanently delete this row?');
    if (cf) {
        $.ajax({
            url: '/api/Nhom/' + id,
            type: "DELETE",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: true,
            statusCode: {
                200: function () {
                    _getAll();
                }
            }
           
            
            
        });
    }
}