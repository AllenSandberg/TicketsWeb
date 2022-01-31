pageSetUp();
runAllForms();

// Validation
$(function () {
    // Validation
    var $validator = $("#clientadd").validate({

        // Rules for form validation
        rules: {
            email: {
                required: true,
                email: true
            },
            FirstName: {
                required: true
            },
            LastName: {
                required: true
            },
            countryIso: {
                required: true
            },
            city: {
                required: true
            },
            zip: {
                required: true
            },
            address: {
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


    $('#clientadd-wizard').bootstrapWizard({
        'tabClass': 'form-wizard',
        'onNext': function (tab, navigation, index) {
            var $valid = $("#clientadd").valid();
            if (!$valid) {
                $validator.focusInvalid();
                return false;
            } else {
                $('#clientadd-wizard').find('.form-wizard').children('li').eq(index - 1).addClass('complete');
                $('#clientadd-wizard').find('.form-wizard').children('li').eq(index - 1).find('.step').html('<i class="fa fa-check"></i>');
            }
        },
        'onTabClick': function () {
            return false;
        },
        'onTabShow': function (tab, navigation, index) {

            $('#general-error').html('').hide();

            if (index >= 2) {
                $("#clientadd-wizard .next").hide();
                $("#clientadd-wizard .finish").removeClass('hidden').show();
            }

            if (index >= 3) {
                $("#clientadd-wizard .previous").hide();
                $("#clientadd-wizard .next").hide();
                $("#clientadd-wizard .finish").hide();
                $("#clientadd-wizard .newclient").removeClass('hidden').show();
                $("#clientadd-wizard .finishFile").removeClass('hidden').show();
            }


            if (index >= 4) {
                $("#clientadd-wizard .previous").hide();
                $("#clientadd-wizard .next").hide();
                $("#clientadd-wizard .finish").hide();
                $("#clientadd-wizard .newclient").hide();
            }

            if (index < 2) {
                $("#clientadd-wizard .previous").show();
                $("#clientadd-wizard .next").show();
                $("#clientadd-wizard .finish").hide();
                $("#clientadd-wizard .finishFile").hide();
            }

        }
    });

    $("#clientadd-wizard .finish").on('click', function () {
        $("#clientadd").submit();
    });

    $("#clientadd-wizard .finishFile").on('click', function () {
        $("#clientadd").submit();
    });


    $("#clientadd").on('submit', function (e) {
        var form = $(this);
        if (form.valid()) {

            var formClientId = $('#clientadd-clientid').val();

            var formData = new FormData(this);

            var url_action = '/virtualterminals/clientadd/';
            if (formClientId) {
                url_action = '/virtualterminals/uploadFile/?clientid=' + formClientId;
            }

            $.ajax({
                type: "POST",
                url: url_action,
                data: formData, // serializes the form's elements.
                cache: false,
                contentType: false,
                processData: false,
                success: function (data) {

                    console.log(data); // show response

                    


                    if (data.errorCode === 0 && formClientId === '') {
                        $('#clientadd-wizard').find('.form-wizard').children('li').eq(2).addClass('complete');
                        $('#clientadd-wizard').bootstrapWizard('show', 3);
                        $('#clientadd-clientid').val(data.clientId);
                    }

                    if (data.errorCode === 0 && formClientId > 0) {
                        $('#clientadd-wizard').find('.form-wizard').children('li').eq(3).addClass('complete');
                        $('#clientadd-wizard').bootstrapWizard('show', 4);
                        $("#clientadd-wizard .finishFile").hide();
                    }

                    if (data.errorCode === 200) {
                        $('#clientadd-wizard').bootstrapWizard('show', 0);
                        $('#email').closest('.form-group').removeClass('has-success').addClass('has-error');
                        $('#general-error').html(data.errorDescription).show();
                    }

                    if (data.errorCode === '-1000') {
                        $('#general-error').html(data.errorDescription).show();
                    }

                },
                done: function (data) {
                    console.log(data);
                }
            });
        }

        e.preventDefault();

        return false;

    });

});