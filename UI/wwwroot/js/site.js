// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {

    //Arama çubuğu yapma kodları User Index sayfasındaki tablonun üzerine eklenen input(arama çubuğu) ile çalışır.
    $("#searchBar").keyup(function () {
        let value = $(this).val().toLowerCase();
        $("#userTable tr").filter(function () {
            $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
        });
    });

   







});
