// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(".alert").fadeTo(7000, 150).slideUp(150, function () {
    $("alert").slideUp(150);
});