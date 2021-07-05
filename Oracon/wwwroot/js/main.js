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

function favorite(id) {
    var $favorite = $('a#favorite_' + id);
    var $idCurso = id;
    $.ajax({
        'url': '/Curso/Favoritos?idCurso=' + id,
        'type': 'get'
    })
    var options = `<i onclick="nofavorite(${$idCurso})" class="fa fa-heart" aria-hidden="true"></i>`
    $favorite.html(options);
};

function nofavorite(id) {
    var $favorite = $('a#favorite_' + id);
    var $idCurso = id;
    $.ajax({
        'url': '/Curso/Favoritos?idCurso=' + id,
        'type': 'get'
    })
    var options = `<i onclick="favorite(${$idCurso})" class="fa fa-heart-o" aria-hidden="true"></i>`
    $favorite.html(options);
};

function Comprar(id) {
    var $compra = $('a#compra_' + id);
    var $idCurso = id;

    Swal.fire({
        title: 'Agregar a la cesta?',
        text: "Este curso se agregar a tu cesta",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Si, agregar'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: "/Curso/Compras?idCurso=" + id,
                type: "get"
            });
            Swal.fire({
                title: 'Agregado!',
                text: 'Este curso esta en tu cesta',
                icon: 'success',
                confirmButtonText: 'ok'
            }).then((result) => {
                if (result.isConfirmed) {
                    location.reload();
                }
            })
        }
    });
};
