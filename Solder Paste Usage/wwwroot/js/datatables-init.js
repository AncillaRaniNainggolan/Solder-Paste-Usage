$(document).ready(function () {

    $(".datatable").DataTable({

        responsive: true,

        pageLength: 25,

        lengthMenu: [
            [10, 25, 50, 100],
            [10, 25, 50, 100]
        ],

        ordering: true,

        searching: true,

        paging: true,

        info: true,

        autoWidth: false,

        language: {

            search: "Search:",

            lengthMenu: "Show _MENU_ entries",

            info: "Showing _START_ to _END_ of _TOTAL_ entries",

            paginate: {

                previous: "Previous",

                next: "Next"

            },

            zeroRecords: "No matching records found"

        }

    });

});