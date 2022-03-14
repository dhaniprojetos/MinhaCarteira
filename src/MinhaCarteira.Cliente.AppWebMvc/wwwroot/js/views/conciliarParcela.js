$("#add").click(adicionar);
$("#remove").click(remover);
$("#origem").dblclick(adicionar);
$("#destino").dblclick(remover);
$("#addTodos").click(adicionarTodos);
$("#removerTodos").click(removerTodos);
$("#buscar").click(carregarMovimentos);

function obterValorPagoParcela() {
    var opcao = $("#valorPago").text();
    var txtValor = opcao
        .replace(/^\D+/g, '')
        .replace('.', '')
        .replace(',', '.');

    return parseFloat(txtValor);
}

function calcularSomaSelecionados() {
    $("#destino option").length > 0
        ? $("#salvar").removeAttr('disabled')
        : $("#salvar").attr('disabled', 'disabled');

    if ($("#destino option").length === 0)
        return;

    var valor = 0.0;
    $("#destino option").each(function () {
        var opcao = $(this).text().split('|');
        var txtValor = opcao[1]
            .replace(/^\D+/g, '')
            .replace('.', '')
            .replace(',', '.');

        valor += parseFloat(txtValor);
    });

    let realLocal = Intl.NumberFormat("pt-BR", { style: "currency", currency: "BRL" });
    $("#total").html(realLocal.format(valor));

    var valorParcela = obterValorPagoParcela();
    valor > valorParcela * 0.98 && valor < valorParcela * 1.02
        ? $("#salvar").removeAttr('disabled')
        : $("#salvar").attr('disabled', 'disabled');
}

function adicionar(e) {
    const selectedOpts = $("#origem option:selected");
    if (selectedOpts.length === 0) {
        alert("Por gentileza, selecione algum registro.");
        e.preventDefault();
        return;
    }

    $("#destino").append($(selectedOpts).clone());
    $(selectedOpts).remove();
    calcularSomaSelecionados();
    e.preventDefault();
}

function remover(e) {
    const selectedOpts = $("#destino option:selected");
    if (selectedOpts.length === 0) {
        alert("Por gentileza, selecione algum registro.");
        e.preventDefault();
        return;
    }

    $("#origem").append($(selectedOpts).clone());
    $(selectedOpts).remove();
    calcularSomaSelecionados();
    e.preventDefault();
}

function adicionarTodos(e) {
    const selectedOpts = $("#origem option");
    if (selectedOpts.length === 0) {
        alert("Por gentileza, selecione algum registro.");
        e.preventDefault();
        return;
    }

    $("#destino").append($(selectedOpts).clone());
    $(selectedOpts).remove();
    calcularSomaSelecionados();
    e.preventDefault();
};

function removerTodos(e) {
    const selectedOpts = $("#destino option");
    if (selectedOpts.length === 0) {
        alert("Por gentileza, selecione algum registro.");
        e.preventDefault();
        return;
    }

    $("#origem").append($(selectedOpts).clone());
    $(selectedOpts).remove();
    calcularSomaSelecionados();
    e.preventDefault();
};

function carregarMovimentos(e) {
    var datastring = $("#formConciliar").serialize();
    $("#origem").empty();

    $.ajax({
        url: window.siteRoot + 'agendamento/obterMovimentos',
        type: "POST",
        data: datastring,
        dataType: 'json',
        contentType: 'application/x-www-form-urlencoded; charset=utf-8',
        success: function (data) {
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
            let item = "<option value='0'>Nenhum registro localizado</option>";
            $("#origem").append(item);
            //alert(response.responseText);
        },
        failure: function (response) {
            alert(response.responseText);
        }
    });
}

$("#formConciliar").submit(function (e) {
    e.preventDefault(); // avoid to execute the actual submit of the form.

    var form = $(this);
    var idParcela = $("#Id").val();
    var idMovimentos = "";

    $("#destino option").each(function () {
        idMovimentos += $(this).val() + ",";
        // Add $(this).val() to your list
    });

    console.log(idParcela);
    console.log(idMovimentos);

    if (isEmpty(idParcela) || isEmpty(idMovimentos))
        return false;

    $.ajax({
        url: window.siteRoot + 'agendamento/ConciliarParcela',
        data: { "id": idParcela, "idMovimentos": idMovimentos },
        type: "POST",
        success: function (data) {
            window.location.href = data.redirectToUrl;
        }
    });

});

function isEmpty(val) {
    return val === undefined || val == null || val.length <= 0 || val === NaN || val === "";
}