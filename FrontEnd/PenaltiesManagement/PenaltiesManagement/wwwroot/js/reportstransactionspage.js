pageSetUp();
runAllForms();

$(document).ready(function () {

    
    $('#TransactionDateFrom').datepicker({
        dateFormat: 'yy-mm-dd',
        prevText: '<i class="fa fa-chevron-left"></i>',
        nextText: '<i class="fa fa-chevron-right"></i>',
        onSelect: function (selectedDate) {
            $('#TransactionDateTo').datepicker('option', 'minDate', selectedDate);
        }
    });

    $('#TransactionDateTo').datepicker({
        dateFormat: 'yy-mm-dd',
        prevText: '<i class="fa fa-chevron-left"></i>',
        nextText: '<i class="fa fa-chevron-right"></i>',
        onSelect: function (selectedDate) {
            $('#TransactionDateFrom').datepicker('option', 'maxDate', selectedDate);
        }
    });

    function statusToggle() {
        let status = parseInt($('#transaction_status').val());
        if (status === 1) {
            $('.field_BankReferenceNumber, .field_SettledAmount').show();
        } else {
            $('.field_BankReferenceNumber, .field_SettledAmount').hide();
        }
    }

    $('#transaction_status').on('change', function () {
        statusToggle(status);
    });

    statusToggle(status);

    $("#clientdetails_document").validate({

        // Rules for form validation
        rules: {

            DocumentType: {
                required: true,
            },
            File: {
                required: true,
            },

        },


        submitHandler: function (form) {
            //form.submit();
            return false;
        },

        // Do not change code below
        highlight: function (element) {
            $(element).closest('.form-group').removeClass('has-success').addClass('has-error');
        },
        unhighlight: function (element) {
            $(element).closest('.form-group').removeClass('has-error').addClass('has-success');
        },
        errorElement: 'span',
        errorClass: 'help-block',
        errorPlacement: function (error, element) {
            if (element.parent('.input-group').length) {
                error.insertAfter(element.parent());
            } else {
                error.insertAfter(element);
            }
        }
    });


    $("#transactionDetails").validate({
        ignore: [],
        // Rules for form validation
        rules: {
            Status: {
                required: true,
            },
            BankReferenceNumber: {
                required: function (element) {
                    return (parseInt($("#transaction_status").val()) === 1);
                },
            },
            SettledAmount: {
                required: function (element) {
                    return (parseInt($("#transaction_status").val()) === 1);
                },
                number: true,
            },

        },


        submitHandler: function (form) {
            //form.submit();
            return false;
        },

        // Do not change code below
        highlight: function (element) {
            $(element).closest('.form-group').removeClass('has-success').addClass('has-error');
        },
        unhighlight: function (element) {
            $(element).closest('.form-group').removeClass('has-error').addClass('has-success');
        },
        errorElement: 'span',
        errorClass: 'help-block',
        errorPlacement: function (error, element) {
            if (element.parent('.input-group').length) {
                error.insertAfter(element.parent());
            } else {
                error.insertAfter(element);
            }
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

    $("#clientdetails_document").on('submit', function (e) {
        var form = $(this);
        if (form.valid()) {

            var formData = new FormData(this);

            form.find('button[type=submit]').prop('disabled', true).html('<i class="fa fa-refresh fa-spin"></i> Uploading document...');

            $.ajax({
                type: "POST",
                url: form[0].action,
                data: formData, // serializes the form's elements.
                cache: false,
                contentType: false,
                processData: false,
                success: function (data) {

                    console.log(data); // show response

                    if (data.errorCode === 0) {
                        window.location.reload();
                    } else {
                        $('#general-error').html(data.errorDescription).show();
                        form.find('button[type=submit]').prop('disabled', false).html('Upload document');
                    }

                },
                error: function (jqXHR, textStatus, errorThrown) {
                    console.log(textStatus, errorThrown);
                    form.find('button[type=submit]').prop('disabled', false).html('Upload document');
                    $('#general-error').html('General error. Please, try again later.').show();
                }
            });
        }
        e.preventDefault();
        return false;
    });



    $("#transactionDetails").on('submit', function (e) {
        var form = $(this);
        if (form.valid()) {

            $.ajax({
                type: "POST",
                url: form[0].action,
                data: form.serialize(), // serializes the form's elements.
                success: function (data) {
                    console.log(data); // show response

                    if (data.errorCode === 0) {
                        $('#transactionDetails_success').dialog('open');

                    }


                    if (data.errorCode !== 0) {
                        $('#general-error').html(data.errorDescription).show();
                    }

                },
            });
        }

        e.preventDefault();

        return false;

    });

    $("#transactionDetails_success").dialog({
        autoOpen: false,
        modal: true,
        width: 400,
        close: function () {

        },
    });


});