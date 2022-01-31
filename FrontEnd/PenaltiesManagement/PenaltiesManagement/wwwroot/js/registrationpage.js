pageSetUp();
runAllForms();

// Model i agree button
$("#i-agree").click(function () {
    $this = $("#terms");
    if ($this.checked) {
        $('#myModal').modal('toggle');
    } else {
        $this.prop('checked', true);
        $('#myModal').modal('toggle');
    }
});

// Validation
$(function () {
    // Validation
    var $validator = $("#signup").validate({

        // Rules for form validation
        rules: {
            MerchantName: {
                required: true
            },
            Email: {
                required: true,
                email: true
            },
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
            FirstName: {
                required: true
            },
            LastName: {
                required: true
            },
            MerchantCompany: {
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

    $('#signup-wizard').bootstrapWizard({
        'tabClass': 'form-wizard',
        'onNext': function (tab, navigation, index) {
            var $valid = $("#signup").valid();
            if (!$valid) {
                $validator.focusInvalid();
                return false;
            } else {
                $('#signup-wizard').find('.form-wizard').children('li').eq(index - 1).addClass('complete');
                $('#signup-wizard').find('.form-wizard').children('li').eq(index - 1).find('.step').html('<i class="fa fa-check"></i>');
            }
        },
        'onTabClick': function () {
            return false;
        },
        'onTabShow': function (tab, navigation, index) {

            $('#general-error').html('').hide();

            if (index >= 3) {
                $("#signup-wizard .next").hide();
                $("#signup-wizard .finish").removeClass('hidden').show();
            } else {
                $("#signup-wizard .next").show();
                $("#signup-wizard .finish").hide();
            }

            if (index >= 4) {
                $("#signup-wizard .previous").hide();
                $("#signup-wizard .next").hide();
                $("#signup-wizard .finish").hide();
            }
        }
    });

    $("#signup-wizard .finish").on('click', function () {
        $("#signup").submit();
    });

    $("#signup").on('submit', function (e) {
        var form = $(this);
        if (form.valid()) {

            $.ajax({
                type: "POST",
                url: form.action,
                data: form.serialize(), // serializes the form's elements.
                success: function (data) {

                    console.log(data); // show response

                    

                    if (data.errorCode === 0) {
                        $('#signup-wizard').find('.form-wizard').children('li').eq(3).addClass('complete');
                        $('#signup-wizard').bootstrapWizard('show', 4);
                    }

                    if (data.errorCode === 20) {
                        $('#signup-wizard').bootstrapWizard('show', 0);
                        $('#email').closest('.form-group').removeClass('has-success').addClass('has-error');
                        $('#general-error').html(data.errorDescription).show();
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

});