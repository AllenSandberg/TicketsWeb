pageSetUp();
runAllForms();

// Validation
$(function () {
    
    // Validation
    $("#systemstatuscreate").validate({

        // Rules for form validation
        rules: {
            status: {
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
    
    $("#systemstatuscreate").on('submit', function (e) {
        var form = $(this);
        if (form.valid()) {
            $.ajax({
                type: "POST",
                url: form.action,
                data: form.serialize(), // serializes the form's elements.

                success: function (data) {
                    console.log(data); // show response

                    if (data.errorCode === 0) {

                        $('#systemstatuscreate_success').dialog('open');
                        setTimeout(function () {
                            window.location.replace($('#backButton').attr('href'));
                        }, 2000);
                        
                    } else {
                        $('#general-error').html(data.errorDescription).show();
                    }
                }
            });
        }

        e.preventDefault();

        return false;

    });


    $("#systemstatuscreate_success").dialog({
        autoOpen: false,
        modal: true,
        width: 400,
        close: function () {
            $("#systemusercreate").trigger("reset");
        },
    });




});