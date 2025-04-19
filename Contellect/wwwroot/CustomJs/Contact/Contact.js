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

$(document).ready(function () {
    $('#SearchWord').on('input', function () {
        let searchWord = $(this).val();
        let pageNumber = 1; // default to first page
        let pageSize = 10;  // set page size

        $.ajax({
            url: '/Contact/ContactGetAllDataBySearch',
            type: 'GET',
            data: { pageNumber: pageNumber, pageSize: pageSize, searchWord: searchWord },
            success: function (data) {
                updateTable(data);
            },
            error: function () {
                alert('Error retrieving contacts.');
            }
        });
    });

    function updateTable(data) {
        let tbody = $('table tbody');
        tbody.empty();

        if (data.length === 0) {
            tbody.append('<tr><td colspan="5">No contacts found.</td></tr>');
            return;
        }

        $.each(data, function (i, contact) {
            let row = '<tr>' +
                '<td>' + contact.name + '</td>' +
                '<td>' + contact.phone + '</td>' +
                '<td>' + contact.address + '</td>' +
                '<td><a href="/Contact/EditContact/' + contact.id + '" class="btn btn-warning btn-sm">Edit</a></td>' +
                '<td><a href="/Contact/DeleteContact/' + contact.id + '" class="btn btn-danger btn-sm" id="delete-btn">Delete</a></td>' +
                '</tr>';
            tbody.append(row);
        });
    }
});



$(document).ready(function () {
    $("#delete-btn").click(function (e) {
        e.preventDefault(); // prevent default link navigation

        var link = $(this).attr("href");

        Swal.fire({
            title: 'Are you sure?',
            text: "You won't be able to revert this!",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#d33',
            cancelButtonColor: '#3085d6',
            confirmButtonText: 'Yes, delete it!'
        }).then((result) => {
            if (result.isConfirmed) {
                window.location.href = link;
            }
        });
    });
});


