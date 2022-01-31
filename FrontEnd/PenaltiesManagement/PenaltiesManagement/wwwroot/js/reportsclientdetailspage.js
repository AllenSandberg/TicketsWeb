pageSetUp();
runAllForms();

// Validation
$(function () {

    

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

   
    // Validation
    $("#clientdetails").validate({

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
            Phone: {
                required: true
            },
            City: {
                required: true
            },
            Zip: {
                required: true
            },
            countryIso: {
                required: true
            },
            RegistrationDate: {
                required: true,
                date: true
            },
            Status: {
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


    $("#clientdetails").on('submit', function (e) {

        var form = $("#clientdetails");

        if ($(this).valid()) {

            $.ajax({
                type: "POST",
                url: form[0].action,
                data: form.serialize(), // serializes the form's elements.

                success: function (data) {

                    console.log(data); // show response


                    if (data.errorCode === 0) {

                        $('#clientdetails_success').dialog('open');

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


    
    $("#clientdetails_document").on('submit', function (e) {
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
                    
                    if (data.errorCode === 0) {
                        window.location.reload();
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

    $('#registration_date').datepicker({
        dateFormat: 'yy-mm-dd',
        prevText: '<i class="fa fa-chevron-left"></i>',
        nextText: '<i class="fa fa-chevron-right"></i>',
        onSelect: function (selectedDate) {
            
        }
    });

    $("#clientdetails_success").dialog({
        autoOpen: false,
        modal: true,
        width: 400,
        close: function () {
            
        },
    });




});