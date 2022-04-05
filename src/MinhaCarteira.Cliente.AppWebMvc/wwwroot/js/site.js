// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

let ehErro = $(".alert").hasClass("alert-danger");
let tempo = ehErro ? 7000 : 1500;

$(".alert").fadeTo(tempo, 150).slideUp(150, function () {
    $("alert").slideUp(150);
});

$(function () {
    $('[data-toggle="tooltip"]').tooltip()
})

if ('serviceWorker' in navigator) {
    navigator.serviceWorker
        .register(window.siteRoot + '/js/service-worker.js')
        .then(function () {
            //console.log('Service Worker Registered');
        });
}

function gravarEstadoMenu(valor) {
    $.ajax({
        url: window.siteRoot + 'usuario/gravarCondicaoSidebar',
        data: { "valor": valor },
        type: "POST",
        success: function (data) {
            console.log("OK");
            console.log(data);
        },
        error: function (response) {
            console.log(response.responseText);
        },
        failure: function (response) {
            console.log(response.responseText);
        }
    });
}

$(document).on('shown.lte.pushmenu', function (configMenu) {
    console.log("sidebar-open");
    gravarEstadoMenu("sidebar-open");
})

$(document).on('collapsed.lte.pushmenu', function (configMenu) {
    console.log("sidebar-collapse");
    gravarEstadoMenu("sidebar-collapse");
})