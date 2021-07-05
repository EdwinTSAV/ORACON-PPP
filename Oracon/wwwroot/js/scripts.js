/*!
    * Start Bootstrap - SB Admin v7.0.1 (https://startbootstrap.com/template/sb-admin)
    * Copyright 2013-2021 Start Bootstrap
    * Licensed under MIT (https://github.com/StartBootstrap/startbootstrap-sb-admin/blob/master/LICENSE)
    */
    // 
// Scripts
// 

window.addEventListener('DOMContentLoaded', event => {

    // Toggle the side navigation
    const sidebarToggle = document.body.querySelector('#sidebarToggle');
    if (sidebarToggle) {
        // Uncomment Below to persist sidebar toggle between refreshes
        // if (localStorage.getItem('sb|sidebar-toggle') === 'true') {
        //     document.body.classList.toggle('sb-sidenav-toggled');
        // }
        sidebarToggle.addEventListener('click', event => {
            event.preventDefault();
            document.body.classList.toggle('sb-sidenav-toggled');
            localStorage.setItem('sb|sidebar-toggle', document.body.classList.contains('sb-sidenav-toggled'));
        });
    }

});

//Eliminar objetos (usuarios y cursos)

function Eliminar(id, objeto) {
    Swal.fire({
        title: '¿Estas seguro?',
        text: "Eliminaras un " + objeto,
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Si, ¡Eliminar!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: "/admin/eliminar" + objeto + "?id" + objeto + "=" + id,
                type: "get"
            });
            Swal.fire({
                title: '¡Eliminado!',
                text: 'Objeto eliminado correctamente',
                icon: 'success',
                confirmButtonText: 'ok'
            }).then((result) => {
                if (result.isConfirmed) {
                    location.reload();
                }
            })
        }
    });
}

//Eliminar favoritos

function EliminarF(id) {
    Swal.fire({
        title: '¿Estas seguro?',
        text: "Quitaras este curso de favoritos",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Si, ¡Eliminar!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: "/Curso/Favoritos?idCurso=" + id,
                type: "get"
            });
            Swal.fire({
                title: '¡Eliminado!',
                text: 'Este curso ya no estara en favoritos',
                icon: 'success',
                confirmButtonText: 'ok'
            }).then((result) => {
                if (result.isConfirmed) {
                    location.reload();
                }
            })
        }
    });
}

//Eliminar cesta

function Comprar(id) {
    Swal.fire({
        title: 'Eliminar de la cesta?',
        text: "Quitar este curso",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Si, quitar'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: "/Curso/Compras?idCurso=" + id,
                type: "get"
            });
            Swal.fire({
                title: '¡Eliminado!',
                text: 'Este curso ya no esta en tu cesta',
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