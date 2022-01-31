pageSetUp();
runAllForms();

// Validation
$(function () {

    $("#systemappealtemplateadd").validate({

        // Rules for form validation
        rules: {

            TemplateName: {
                required: true,
            },
            TemplateDescription: {
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

    $("#systemappealtemplateadd").on('submit', function (e) {
        var form = $(this);
        if (form.valid()) {

            var formData = new FormData(this);

            form.find('button[type=submit]').prop('disabled', true).html('<i class="fa fa-refresh fa-spin"></i> מעלים תבנית...');

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
                        $('#systemusercreate_success').dialog('open');
                        setTimeout(function () {
                            window.location.replace($('#backButton').attr('href'));
                        }, 2000);
                    } else {
                        $('#general-error').html(data.errorDescription).show();
                        form.find('button[type=submit]').prop('disabled', false).html('הוסף');
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


    $("#systemusercreate_success").dialog({
        autoOpen: false,
        modal: true,
        width: 400,
        close: function () {
            $("#systemusercreate").trigger("reset");
        },
    });




});