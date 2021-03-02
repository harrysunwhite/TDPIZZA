$(document).ready(function () {

    load();

    $('#btQLTD').click(function () {
     
        //$('#ctNhom').fadeOut();
        $('.content').fadeOut();
       
        $('#ctThucDon').fadeIn();
       



    });

    $('#btQLN').click(function () {
        //$('#ctThucDon').fadeOut()
        $('.content').fadeOut();
       
        $('#ctNhom').fadeIn()


    });

    $('#btLC').click(function () {
        //$('#ctThucDon').fadeOut()
        $('.content').fadeOut();

        $('#ctListCart').fadeIn()


    });
});

function load() {

    //$('#ctListCart').load('@Url.Action("GetListCart", "admin")');
    $('.content').fadeOut();

    $('#ctThucDon').fadeIn();
}

