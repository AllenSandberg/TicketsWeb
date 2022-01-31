runAllForms();

$(function () {
    // Validation

    $('#btn-submit').prop('disabled', false);

    $("#login-form").validate({
        submitHandler: function (form) {
            //form.submit();
            return false;
        },
        // Rules for form validation
        rules: {
            email: {
                required: true,
                email: true
            },
            password: {
                required: true,
                minlength: 3,
                maxlength: 20
            }
        },

        // Messages for form validation
        messages: {
            email: {
                required: 'נא לא להזין את כתובת אמייל',
                email: 'נא להשתמש באמייל תקין'
            },
            password: {
                required: 'נא להזין את סיסמתכם.',
            }
        },

        // Do not change code below
        highlight: function (element) {
            $(element).closest('.form-group').removeClass('has-success').addClass('has-error');
        },
        unhighlight: function (element) {
            $(element).closest('.form-group').removeClass('has-error').addClass('has-success');
            $(element).closest('.form-group').find('.help-block').hide();
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

    $("#login-form").on('submit', function (e) {
        var form = $(this);
        if (form.valid()) {
            $('#btn-submit').prop('disabled', true).html('<i class="fa fa-refresh fa-spin"></i> נכנס...');
            $.ajax({
                type: "POST",
                url: form.action,
                data: form.serialize(), // serializes the form's elements.
                success: function (data) {

                    console.log(data); // show response

                    if (data.errorCode === 0) {
                        window.location = "/";
                    } else {
                        $('#password').closest('.form-group').removeClass('has-success').addClass('has-error');
                        $('#general-error').html(data.errorDescription).show();
                        $('#btn-submit').prop('disabled', false).html('כניסה');

                    }

                },
                error: function (jqXHR, textStatus, errorThrown) {
                    console.log(textStatus, errorThrown);
                    $('#btn-submit').prop('disabled', false).html('כנוסה');
                    $('#general-error').html('שגיא. נא תנסו יותר מאוחר.').show();
                }

            });
        }

        e.preventDefault();

        return false;

    });



});