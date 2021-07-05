// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

/** BARRA MOVIL HOME INDEX */
$(function () {
    var n = $(window).height(),
        a = $(".barra").innerHeight();
    $(window).scroll(function () {
        $(window).scrollTop() > n ? ($(".barra").addClass("fixed"),
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

// SELECCION DE FOTO //
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

/** CARROSEL CON GLIDER **/

window.addEventListener('load', function () {
    new Glider(document.querySelector('.glider'), {
        slidesToShow: 2,
        slidesToScroll: 2,
        draggable: true,
        arrows: {
            prev: '.glider-prev',
            next: '.glider-next'
        },
        responsive: [
            {
                // screens greater than >= 700px
                breakpoint: 600,
                settings: {
                    slidesToShow: 3,
                    slidesToScroll: 3,
                    itemWidth: 150,
                    duration: 0.25
                }
            }, {
                // screens greater than >= 100px
                breakpoint: 900,
                settings: {
                    slidesToShow: 4,
                    slidesToScroll: 4,
                    itemWidth: 150,
                    duration: 0.25
                }
            }, {
                // screens greater than >= 100px
                breakpoint: 1200,
                settings: {
                    slidesToShow: 5,
                    slidesToScroll: 5,
                    itemWidth: 150,
                    duration: 0.25
                }
            }
        ]
    });
});