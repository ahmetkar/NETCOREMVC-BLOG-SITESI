$(document).ready(function () {

    
    $("#commentForm").on("submit", function (e) {

        e.preventDefault();

        var name = $("input[name=name]").val();
        var email = $("input[name=email]").val();
        var id = $("input[name=id]").val();
        var text = $("textarea[name=text]").val();

        if (!name || !email || !text) {
            $("#error").toggle();
            $("#error").html("Tüm alanları doldurun");
            return;  // Verileri göndermeyecek
        }

       
        // AJAX isteği
       
        $.ajax({
            url: "/Home/AddComment",
            type: 'POST',
            data: { id: id, name: name, email: email, text:text },
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