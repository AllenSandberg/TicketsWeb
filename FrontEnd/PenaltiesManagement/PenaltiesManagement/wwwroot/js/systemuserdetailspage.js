pageSetUp();
runAllForms();

// Validation
$(function () {
    
    // Validation
    $("#systemuserupdate").validate({

        // Rules for form validation
        rules: {
            Email: {
                required: true,
                email: true,
            },
            FirstName: {
                required: true
            },
            LastName: {
                required: true
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

    // Validation
    $("#changepassword").validate({

        // Rules for form validation
        rules: {
            Password: {
                required: true,
                minlength: 3,
                maxlength: 20
            },
            ConfirmPassword: {
                required: true,
                minlength: 3,
                maxlength: 20,
                equalTo: '#password'
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

    
    $("#systemuserupdate, #changepassword").on('submit', function (e) {
        var form = $(this);
        if (form.valid()) {
            
            $.ajax({
                type: "POST",
                url: form.action,
                data: form.serialize(), // serializes the form's elements.

                success: function (data) {

                    console.log(data); // show response



                    if (data.errorCode == 0) {

                        $('#systemuserupdate_success').dialog('open');

                    }


                    if (data.errorCode == '-1000') {
                        $('#general-error').html(data.errorDescription).show();
                    }

                }
            });
        }

        e.preventDefault();

        return false;

    });


    $("#systemuserupdate_success").dialog({
        autoOpen: false,
        modal: true,
        width: 400,
        close: function () {
            
        },
    });




});