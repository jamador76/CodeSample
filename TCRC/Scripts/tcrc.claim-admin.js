$.fn.exists = function () {
    return this.length !== 0;
};

(function ($) {
    function toggleClaimStatus(claimStatusTypeId) {
        if (claimStatusTypeId === "3") {
            $('#claim-approved-paid').show();
        }
        else {
            $('#claim-approved-paid').hide();
        }

        if (claimStatusTypeId === "11") {
            $('#claim-check-bounced').show();
        }
        else {
            $('#claim-check-bounced').hide();
        }

        if (claimStatusTypeId === "10") {
            $('#claim-check-cleared').show();
        }
        else {
            $('#claim-check-cleared').hide();
        }

        if (claimStatusTypeId === "9") {
            $('#claim-check-received').show();
        }
        else {
            $('#claim-check-received').hide();
        }

        if (claimStatusTypeId === "8") {
            $('#claim-fee-refunded').show();
        }
        else {
            $('#claim-fee-refunded').hide();
        }
    }

    $(document).ready(function () {
        $('#table-claim-history').DataTable({
            'ordering': false
        });

        $('#ClaimStatusTypeId').change(function () {
            var claimStatusTypeId = $('#ClaimStatusTypeId').val();
            var claimId = $('#ClaimID').val();
            var claimant = $('#FirstName').val() + " " + $('#LastName').val();
            var address = $('#Address').val();
            var city = $('#City').val();
            var state = $('#State').val();
            var zip = $('#ZipCode').val();
            toggleClaimStatus(claimStatusTypeId);

            if (claimStatusTypeId != '') {
                var request = $.ajax({
                    url: 'GetClaimStatusType',
                    type: 'GET',
                    data: { 'claimId': claimId, 'claimStatusTypeId': claimStatusTypeId, 'claimant': claimant, 'address': address, 'city': city, 'state': state, 'zip': zip },
                    success: function (data) {
                        $('#EmailText').val(data.EmailTemplate);
                    }//,
                    //error: function () {
                    //}
                });
            }
        });
    });
}(jQuery));