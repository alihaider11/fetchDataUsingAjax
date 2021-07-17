$(document).ready(() => {

    $("#btnfillTable").on("click", function () {
        $.ajax({
            url: "/home/fetchTableData",
            success: function (data) {
                console.log(data);
                for (var i = 0; i < data.length; i++) {
                    $('tbody').append(`<tr> <td>${data[i].addressId}</td> <td>${data[i].address}</td> <td>${data[i].city}</td> <td>${data[i].provinceId}</td>  <td>${data[i].postalCode}</td>   </tr>`);
                }
            },
            error: function (er) {
                console.log(er);
            }
        });

    });

});