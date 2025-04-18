$(document).ready(function () {
    debugger;
    console.log('test');
    // Custom rule: Phone must start with 01 and be 10 to 12 digits
    $.validator.addMethod("phonecustom", function (value, element) {
        return this.optional(element) || /^01\d{8,10}$/.test(value);
    }, "Phone number must start with '01' and be 10 to 12 digits long.");
    // Enable client-side validation when the page is loaded
    $('#contactForm').validate({
        rules: {
            Name: {
                required: true,
                maxlength: 100
            },
            Phone: {
                required: true,
                phonecustom: true
            }
        },
        messages: {
            Name: {
                required: "Name is required.",
                phonecustom: "01."
            }
        },
        submitHandler: function (form) {
            // Submit the form if it's valid
            form.submit();
        }
    });
});