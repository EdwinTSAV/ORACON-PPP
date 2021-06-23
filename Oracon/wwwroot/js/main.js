/** BARRA MOVIL ALL CONTROLLERS */

$(function () {
    var a = $(".barra").innerHeight();
    $(window).scroll(function () {
        $(window).scrollTop() > 0 ? ($(".barra").addClass("fixed"),
            $("body").css({ "margin-top": a + "px" })) : ($(".barra").removeClass("fixed"),
                $("body").css({ "margin-top": "0px" }))
    });
    $(".menu-movil").on("click", function () {
        $(".navegacion-principal").slideToggle()
    });
    $(window).resize(function () {
        $(document).width() >= 768 ?
            $(".navegacion-principal").show() :
            $(".navegacion-principal").hide()
    })
});
