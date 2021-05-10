// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// barra movil indice //
/*$(function () {
    $("#filtros a").on("click", function () {
        return $("#filtros a").removeClass("activo"),
            $(this).addClass("activo"),
            $(".registrados tbody tr").hide(),
            "pagados" == $(this).attr("id") ? $(".registrados tbody tr.pagado").show() : $(".registrados tbody tr.no_pagado").show(), !1
    }),
        $(".nombre-sitio").lettering(),
        $('body.conferencia .navegacion-principal a:contains("Conferencia")').addClass("activo"),
        $('body.calendario .navegacion-principal a:contains("Calendario")').addClass("activo"),
        $('body.invitados .navegacion-principal a:contains("Invitados")').addClass("activo");
    var n = $(window).height(),
        a = $(".barra").innerHeight(); $(window).scroll(function () {
            $(window).scrollTop() > n ? ($(".barra").addClass("fixed"), $("body").css({ "margin-top": a + "px" })) : ($(".barra").removeClass("fixed"), $("body").css({ "margin-top": "0px" }))
        }), $(".menu-movil").on("click", function () {
            $(".navegacion-principal").slideToggle()
        }); $(window).resize(function () {
            $(document).width() >= 768 ? $(".navegacion-principal").show() : $(".navegacion-principal").hide()
        }), $(".programa-evento .info-curso:first").show(), $(".menu-programa a:first").addClass("activo"), $(".menu-programa a").on("click", function () {
            $(".menu-programa a").removeClass("activo"), $(this).addClass("activo"), $(".ocultar").hide(); var n = $(this).attr("href"); return $(n).fadeIn(1e3), !1
        }), jQuery(".resumen-evento").length > 0 && $(".resumen-evento").waypoint(function () {
            $(".resumen-evento li:nth-child(1) p").animateNumber({ number: 6 }, 1200), $(".resumen-evento li:nth-child(2) p").animateNumber({ number: 15 }, 1200), $(".resumen-evento li:nth-child(3) p").animateNumber({ number: 3 }, 1500), $(".resumen-evento li:nth-child(4) p").animateNumber({ number: 9 }, 1500)
        }, { offset: "60%" }), $(".cuenta-regresiva").countdown("2017/12/10 09:00:00", function (n) {
            $("#dias").html(n.strftime("%D")), $("#horas").html(n.strftime("%H")), $("#minutos").html(n.strftime("%M")), $("#segundos").html(n.strftime("%S"))
        }), $(".invitado-info").colorbox({ inline: !0, width: "50%" }), $(".boton_newsletter").colorbox({ inline: !0, width: "50%" })
});*/

// seleccionar foto //
$(document).on("click", "#add-photo", function () {
    $("#file").click();
});

function mostrar() {
    var archivo = document.getElementById("file").files[0];
    var reader = new FileReader();
    if (file) {
        reader.readAsDataURL(archivo);
        reader.onloadend = function () {
            document.getElementById("img").src = reader.result;
        }
    }
}
