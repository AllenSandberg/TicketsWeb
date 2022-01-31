pageSetUp();
runAllForms();

// Validation
$(function () {


    // Validation
    $("#addCommission").validate({

        // Rules for form validation
        rules: {
            Amount: {
                required: true
            }
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


    $("#addCommission").on('submit', function (e) {

        var form = $("#addCommission");

        if ($(this).valid()) {

            $.ajax({
                type: "POST",
                url: form[0].action,
                data: form.serialize(), // serializes the form's elements.

                success: function (data) {

                    console.log(data); // show response


                    if (data.errorCode === 0) {

                        $('#add_commission_success').dialog('open');

                    } else {
                        $('#general-error').html(data.errorDescription).show();
                    }
                }
            });
        }

        e.preventDefault();

        return false;

    });


    $("#add_commission_success").dialog({
        autoOpen: false,
        modal: true,
        width: 400,
        close: function () {

        },
    });

});