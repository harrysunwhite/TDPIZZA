$(document).ready(function () {

    load();

    $('#btQLTD').click(function () {
     
        $('#ctNhom').fadeOut();
       
        $('#ctThucDon').fadeIn();
       



    });

    $('#btQLN').click(function () {
        $('#ctThucDon').fadeOut()
       
        $('#ctNhom').fadeIn()


    });
});
function load() {
    $('#ctNhom').fadeOut();

    $('#ctThucDon').fadeIn();
}