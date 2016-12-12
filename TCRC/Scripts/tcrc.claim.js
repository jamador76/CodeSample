$.fn.exists = function () {
    return this.length !== 0;
};

(function ($) {
    function onUpload(e) {
        //send hidden field for folder
        e.data = { uploadKey: $('#UploadKey').val() };
    }

    function onUploadRemove(e) {
        //send hidden field for folder
        e.data = { uploadKey: $('#UploadKey').val() };
    }

    function onUploadSuccess(e) {
        //set hidden field for folder with guid if e.operation is upload and if it's not already set
        if (e.operation == 'upload' && $('#UploadKey').val() == '') {
            $('#UploadKey').val(e.response.data);
        }
    }

    $(document).ready(function () {
        $('.money').autoNumeric('init', { vMax: '999999999.99'});

        var passengers = new $.xfields('.FormElementGroup', '#btnAddPassenger', '#btnRemovePassenger', '0', true, false);
        var payments = new $.xfields('.FormElementGroupPayment', '#btnAddPayment', '#btnRemovePayment', '0', true, false);
        var documents = new $.xfields('.FormElementGroupDocuments', '#btnAddDocument', '#btnRemoveDocument', '0', true, false);

        $('.datepicker').each(function () {
            $(this).removeClass('hasDatepicker').datepicker({
                showOn: 'focus',
                showButtonPanel: true,
                changeMonth: true,
                changeYear: true
            });
        });

        $('#btnSellerSearch').on('click', function () {
            var tcrcNum = $('#TCRCNumber').val();
            var sotNum = $('#SOTNumber').val();
            var agencyName = $('#AgencyName').val();
            var city = $('#AgencyCity').val();
            var scheduledReturnDateStr = $('#ScheduledReturnDate').val();

            //logic ported over from current system
            var earliestClaimDate = new Date();
            $('.payment-date').each(function () {
                if ($(this).val()) {
                    var currentDate = new Date($(this).val());
                    earliestClaimDate = currentDate < earliestClaimDate ? currentDate : earliestClaimDate;
                }
            });

            var request = $.ajax({
                url: 'GetBusinessAddressesForClaim',
                type: 'GET',
                data: { 'tcrcNum': tcrcNum, 'sotNum': sotNum, 'agencyName': agencyName, 'city': city, 'claimDateStr': earliestClaimDate.toDateString(), 'scheduledReturnDateStr': scheduledReturnDateStr },
                success: function (data) {
                    $('#tblSOT').html(data);
                    $('#claim-sot').DataTable({
                        'ordering': false
                    });
                }//,
                //error:
            });
        });

        $('#HasPurchasedInsurance').on('click', function () {
            if ($(this).is(':checked')) {
                $('#InsuranceDetails').show();
            } else {
                $('#InsuranceDetails').hide();
            }
        });

        $("#files").kendoUpload({
            async: {
                saveUrl: "/FileClaim/Upload",
                removeUrl: "/FileClaim/Remove",
                autoUpload: true,
                batch: true
            },
            upload: onUpload,
            remove: onUploadRemove,
            success: onUploadSuccess
        });

        $('#HasOtherReimbursement').on('click', function () {
            if ($(this).is(':checked')) {
                $('#ReimbursementDetails').show();
            } else {
                $('#ReimbursementDetails').hide();
            }
        });

        $('#HasAcceptedTerms').on('click', function () {
            if ($(this).is(':checked')) {
                $('#btnSubmit').removeAttr('disabled');
            } else {
                $('#btnSubmit').attr('disabled', 'disabled');
            }
        });
    });
}(jQuery));