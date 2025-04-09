$(document).ready(function () {

    
    $("#iletisimform").on("submit", function (e) {

        e.preventDefault();

        var name = $("input[name=name]").val();
        var email = $("input[name=email]").val();
        var tel = $("input[name=tel]").val();
        var subject = $("input[name=subject]").val();
        var body = $("textarea[name=body]").val();

        if (!name || !email || !body) {
            $("#error").toggle();
            $("#error").html("Tüm alanları doldurun");
            return;  // Verileri göndermeyecek
        }

       
        // AJAX isteği
       
        $.ajax({
            url: "/Home/Iletisim",
            type: 'POST',
            data: {name: name, email: email, body:body,subject:subject,tel:tel },
            success: function (response) {
                if (response.success) {
                    $("#success").toggle();
                    $("#success").html(response.message);
                } else {
                    $("#error").toggle();
                    $("#error").html(response.message);
                }
            },
            error: function () {
                $("#error").toggle();
                $("#error").html("Bir hata oluştu");
            }
        });
    });

});