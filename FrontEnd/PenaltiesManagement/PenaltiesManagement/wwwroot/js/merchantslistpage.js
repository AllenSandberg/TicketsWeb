$(document).ready(function () {
    $('#registration_from').datepicker({
        dateFormat: 'yy-mm-dd',
        prevText: '<i class="fa fa-chevron-left"></i>',
        nextText: '<i class="fa fa-chevron-right"></i>',
        onSelect: function (selectedDate) {
            $('#registration_to').datepicker('option', 'minDate', selectedDate);
        }
    });

    $('#registration_to').datepicker({
        dateFormat: 'yy-mm-dd',
        prevText: '<i class="fa fa-chevron-left"></i>',
        nextText: '<i class="fa fa-chevron-right"></i>',
        onSelect: function (selectedDate) {
            $('#registration_from').datepicker('option', 'maxDate', selectedDate);
        }
    });
    $("#btnExcel").on('click', function (e) {
        $("#hdBtnType").val("1");
    }
    );
    $("#btnFilter").on('click', function (e) {
        $("#hdBtnType").val("0");
    }
    );


});