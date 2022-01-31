pageSetUp();
runAllForms();

// Validation
$(function () {
    // Validation
    var $validator = $("#addpayment").validate({
        ignore: [],
        // Rules for form validation
        rules: {
            clientid: {
                required: true,
            },
            currency: {
                required: true
            },
            amount: {
                required: true,
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




    $("#addpayment").on('submit', function (e) {
        var form = $(this);
        if (form.valid()) {

            $.ajax({
                type: "POST",
                url: form.action,
                data: form.serialize(), // serializes the form's elements.
                success: function (data) {

                    console.log(data); // show response



                    if (data.errorCode === 0) {
                        $('#dialog_success').dialog('open');

                    }


                    if (data.errorCode === '-1000') {
                        $('#general-error').html(data.errorDescription).show();
                    }

                }
            });
        }

        e.preventDefault();

        return false;

    });

    $("#dialog_success").dialog({
        autoOpen: false,
        modal: true,
        width: 400,
        close: function () {
            $("#addpayment").trigger("reset");
            $("#clientid").select2("val", "");
            $("#currency").select2("val", "");
        },
    });

    
    $("#clientid").select2({
        minimumInputLength: 2,
        tags: [],
        ajax: {
            url: '/VirtualTerminals/SearchUsers/',
            dataType: 'json',
            type: "POST",
            quietMillis: 50,
            data: function (term) {
                return {
                    SearchText: term
                };
            },
            processResults: function (data) {

                if (data.errorCode === 0){
                    return {
                        results: $.map(data.clients, function (item) {
                            return {
                                text: item.firstName + ' ' + item.lastName ,
                                id: item.clientId
                            }
                        })
                    }
                }
            }
        }
    });


});