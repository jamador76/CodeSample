$.fn.exists = function () {
    return this.length !== 0;
};

(function ($) {
    //todo: these hardcoded values were ported over.  refactor to get from ajax request.
    var numLocations = 1;
    var baseRegistrationAssessmentFee = 275;
    var newRegistrationLateFeePerDay = 5;
    var maxLateFee = 500;
    var today = new Date();

    function calculateFees() {
        var regAssessmentFees = baseRegistrationAssessmentFee * numLocations;
        var daysLate = 0;

        if (isDate($('#LegalBusinessStartDate').val())) {
            var busStartDate = new Date($('#LegalBusinessStartDate').val());
            var dueDate = new Date();
            var ONEDAY = 1000 * 60 * 60 * 24;
            dueDate.setTime(busStartDate.getTime() - 10 * ONEDAY);

            if (today > dueDate) {
                daysLate = Math.floor((Math.abs(today.getTime() - dueDate.getTime())) / ONEDAY);
            }
        }
        var lateFeeTotal = daysLate * newRegistrationLateFeePerDay;
        lateFeeTotal = lateFeeTotal > maxLateFee ? maxLateFee : lateFeeTotal;

        var totalAmountDue = lateFeeTotal + regAssessmentFees;

        $('#SubtotalAmount').val(regAssessmentFees);
        $('#LateFeeAmount').val(lateFeeTotal);
        $('#TotalAmount').val(totalAmountDue);
    }

    $(document).ready(function () {
        $('.money').autoNumeric('init', { vMax: '999999999.99' });

        var dbas = new $.xfields('.FormElementGroupDBAs', '#btnAddDBA', '#btnRemoveDBA', '0', true, false);
        var businessAddresses = new $.xfields('.FormElementGroupBusinessAddress', '#btnAddBusinessAddress', '#btnRemoveBusinessAddress', '0', false, true);

        $('.numbersOnly').keyup(function () {
            if ($(this).val() != $(this).val().replace(/[^0-9]/g, '')) {
                $(this).val($(this).val().replace(/[^0-9]/g, ''));
            }
        });

        $('.datepicker').each(function () {
            $(this).removeClass('hasDatepicker').datepicker({
                showOn: 'focus',
                showButtonPanel: true,
                changeMonth: true,
                changeYear: true,
            });
        });

        $('#LegalBusinessStartDate').on('change', function () {
            if ($(this).val()) {
                $(this).removeClass('field-validation-error');
                $(this).addClass('field-validation-valid');
            }
        });

        $('#btnTcrcSearch').on('click', function () {
            var tcrcID = $('#TCRCNumber').val();

            var request = $.ajax({
                url: 'GetTcrcMember',
                type: 'GET',
                data: { 'tcrcID': tcrcID },
                success: function (data) {
                    $('#tcrcSearchResults').html(data);
                }//,
                //error:
            });
        });

        $('#btnSotSearch').on('click', function () {
            var sotID = $('#SOTNumber').val();

            var request = $.ajax({
                url: 'GetSellerOfTravel',
                type: 'GET',
                data: { 'sotID': sotID },
                success: function (data) {
                    $('#sotSearchResults').html(data);
                }//,
                //error:
            });
        });

        $('#LegalBusinessStartDate').on('change', function () {
            calculateFees();
        });

        $('#btnAddBusinessAddress').on('click', function () {
            numLocations += 1;
            calculateFees();
        });

        $('#btnRemoveBusinessAddress').on('click', function () {
            numLocations -= 1;
            calculateFees();
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