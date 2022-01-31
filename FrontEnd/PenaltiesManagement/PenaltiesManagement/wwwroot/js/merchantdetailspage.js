pageSetUp();
runAllForms();

// Validation
$(function () {


    var hash = $.trim(window.location.hash);
    if (hash) {
        $('#main_tabs a[href$="' + hash + '"]').trigger('click');
    }


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

    $("#bankDetails").validate({

        // Rules for form validation
        rules: {

            BankName: {
                required: true,
            },
            BeneficiaryName: {
                required: true,
            },
            Swift: {
                required: true,
            },
            Iban: {
                required: true,
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


    $("#merchantprofile,#bankDetails").on('submit', function (e) {
        var form = $(this);
        if (form.valid()) {

            $.ajax({
                type: "POST",
                url: form[0].action,
                data: form.serialize(), // serializes the form's elements.

                success: function (data) {

                    console.log(data); // show response
                    //$('#MerchantStatus').css('center-block padding-5 label label-success');
                    if (data.status) {
                        $('#MerchantStatus').removeClass('label-success');
                        $('#MerchantStatus').removeClass('label-danger');
                        $('#MerchantStatus').removeClass('label-warning');

                        switch (data.status) {
                            case 0:
                                $('#MerchantStatus').text("Pending");
                                $('#MerchantStatus').addClass('label-warning');
                                break;
                            case 1:
                                $('#MerchantStatus').text("Approved");
                                $('#MerchantStatus').addClass('label-success');
                                break;
                            case 2:
                                $('#MerchantStatus').text("Closed");
                                $('#MerchantStatus').addClass('label-danger');
                                break;
                            case 3:
                                $('#MerchantStatus').addClass('label-warning');
                                $('#MerchantStatus').text("Pending Documents");
                                break;
                        };

                    } else {
                        $('#bank-alert').remove();
                    }

                    //$("#MerchantStatus").text();
                    
                    //$(#MerchantStatus).text = "kaka";

                    if (data.errorCode === 0) {

                        $('#merchantprofile_success').dialog('open');

                    }


                    if (data.errorCode !== 0) {
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
        
        var form_id = form.attr('id');

        form.find('.btn-upload').prop('disabled', true).html('<i class="fa fa-refresh fa-spin"></i> Uploading...');

        if (form.valid()) {

            var formData = new FormData(this);

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
                        var mwrchant_reload_url = document.URL;
                        window.location = mwrchant_reload_url.substring(0, mwrchant_reload_url.lastIndexOf("#"));
                        if (form_id === "merchantprofile_document"){
                            window.location.hash = "#hr3";
                        } else {
                            window.location.hash = "#hr4";
                        }
                        location.reload();
                    }

                    if (data.errorCode !== 0) {
                        $('#general-error').html(data.errorDescription).show();
                        form.find('.btn-upload').prop('disabled', true).html('Upload document');
                    }

                },
                error: function (jqXHR, textStatus, errorThrown) {
                    console.log(textStatus, errorThrown);
                    form.find('.btn-upload').prop('disabled', true).html('Upload document');
                    $('#general-error').html('General error. Please, try again later.').show();
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