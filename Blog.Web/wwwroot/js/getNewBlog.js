$(document).ready(function () {

    var resHtml = "";
    var pageSize = 5;
    var count = parseInt($("input[name=count]").val());
    $("#moreblogClicker").on("click", function (e) {
        // AJAX isteği
        $.ajax({
            url: "/Home/GetNewBlogs/",
            type: 'GET',
            data: { count: count },
            success: function (response) {
                
                if (response.message) {
                    $("#error").toggle();
                    $("#error").html("" + response.message);
                } else {
                    $("#success").toggle();
                    resHtml += response;
                    count += pageSize;
                    $("#datacontainer").html(resHtml);
                }
               
            },
            error: function () {
                $("#error").toggle();
                $("#error").html("Sunucuda sorun var. ");
                
            }
        });

       

       



    });

});