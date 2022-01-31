pageSetUp();
runAllForms();

// Validation
$(function () {

    

    $("#merchantprofile_document").validate({

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

    $("#merchantprofile_logo").validate({

        // Rules for form validation
        rules: {
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

   
    // Validation
    $("#merchantprofile").validate({

        // Rules for form validation
        rules: {
            MerchantName: {
                required: true
            },
            Email: {
                required: true,
                email: true
            },
            FirstName: {
                required: true
            },
            LastName: {
                required: true
            },
            Phone: {
                required: true
            },
            MerchantCompany: {
                required: true
            },
            countryIso: {
                required: true
            },
            MerchantRegistrationNumber: {
                required: true
            },
            Address: {
                required: true
            },
            City: {
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


    $("#merchantprofile").on('submit', function (e) {
        var form = $(this);
        if (form.valid()) {

            

            $.ajax({
                type: "POST",
                url: form.action,
                data: form.serialize(), // serializes the form's elements.

                success: function (data) {

                    console.log(data); // show response



                    if (data.errorCode == 0) {

                        $('#merchantprofile_success').dialog('open');

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


    
    $("#merchantprofile_document, #merchantprofile_logo").on('submit', function (e) {
        var form = $(this);
        if (form.valid()) {

            var formData = new FormData(this);

            $.ajax({
                type: "POST",
                url: form.action,
                data: formData, // serializes the form's elements.
                cache: false,
                contentType: false,
                processData: false,
                success: function (data) {

                    console.log(data); // show response
                    
                    if (data.errorCode == 0) {
                        window.location.reload();
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

    $("#merchantprofile_success").dialog({
        autoOpen: false,
        modal: true,
        width: 400,
        close: function () {
            
        },
    });




});