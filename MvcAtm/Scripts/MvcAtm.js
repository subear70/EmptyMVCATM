$(document).ready(function () {
    $('.clearCardNumberButton').click(function () {
        $('.cardNumber').val('');
    });
    $('.clearPinButton').click(function () {
        $('#Pin').val('');
    });
    $('.cardNumberInput').inputmask("9999-9999-9999-9999");
    $('#Pin').inputmask("9999");
});