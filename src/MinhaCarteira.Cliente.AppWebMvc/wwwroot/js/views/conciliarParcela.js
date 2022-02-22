﻿$("#add").click(adicionar);
$("#remove").click(remover);
$("#origem").dblclick(adicionar);
$("#destino").dblclick(remover);
$("#addTodos").click(adicionarTodos);
$("#removerTodos").click(removerTodos);
$("#buscar").click(carregarMovimentos);

function adicionar(e) {
    const selectedOpts = $("#origem option:selected");
    if (selectedOpts.length === 0) {
        alert("Nothing to move.");
        e.preventDefault();
    }

    $("#destino").append($(selectedOpts).clone());
    $(selectedOpts).remove();
    e.preventDefault();
}

function remover(e) {
    const selectedOpts = $("#destino option:selected");
    if (selectedOpts.length === 0) {
        alert("Nothing to move.");
        e.preventDefault();
    }

    $("#origem").append($(selectedOpts).clone());
    $(selectedOpts).remove();
    e.preventDefault();
}

function adicionarTodos(e) {
    const selectedOpts = $("#origem option");
    if (selectedOpts.length === 0) {
        alert("Nothing to move.");
        e.preventDefault();
    }

    $("#destino").append($(selectedOpts).clone());
    $(selectedOpts).remove();
    e.preventDefault();
};

function removerTodos(e) {
    const selectedOpts = $("#destino option");
    if (selectedOpts.length === 0) {
        alert("Nothing to move.");
        e.preventDefault();
    }

    $("#origem").append($(selectedOpts).clone());
    $(selectedOpts).remove();
    e.preventDefault();
};

function carregarMovimentos(e) {
    $.ajax({
        url: window.siteRoot + 'agendamento/obterMovimentos',
        data: { "prefix": "teste" },
        type: "POST",
        success: function (data) {
            $("#origem").empty();

            const zeroPad = (num, places) => String(num).padStart(places, "\u00A0");
            let realLocal = Intl.NumberFormat("pt-BR", { style: "currency", currency: "BRL" });
            $.each(data, function (i) {
                let descricao = ""
                    + zeroPad(data[i].id, 3) + "|"
                    + zeroPad(realLocal.format(data[i].valor), 13) + "| "
                    + data[i].descricao;

                let item = `<option value="${data[i].id}">${descricao}</option>`;
                $("#origem").append(item);
            });
        },
        error: function (response) {
            alert(response.responseText);
        },
        failure: function (response) {
            alert(response.responseText);
        }
    });
}