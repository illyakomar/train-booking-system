jQuery.validator.addMethod("dates",
    function (value, element, param) {
        return (new Date(value) < new Date($('#DestinationDate').val()));
    });

jQuery.validator.unobtrusive.adapters.addBool("dates");
