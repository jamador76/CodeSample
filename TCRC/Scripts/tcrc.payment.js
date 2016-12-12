$.fn.exists = function () {
    return this.length !== 0;
};

(function ($) {
    $(document).ready(function () {
        //todo: add mask for money, credit card expiration date, 
        $('input[name=PaymentMethod]:radio').change(function () {
            var paymentMethodVal = $(this).val();

            if (paymentMethodVal === 'credit') {
                //credit
                $('#payment-credit').show();
                $('#payment-check').hide();
            } else if (paymentMethodVal === 'check') {
                //check
                $('#payment-credit').hide();
                $('#payment-check').show();
            }
        });
    });
}(jQuery));