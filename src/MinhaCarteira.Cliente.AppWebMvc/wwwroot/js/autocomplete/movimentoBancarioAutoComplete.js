$(function () {
    /*AUTO COMPLETE PARA O CAMPO CONTA BANCÁRIA*/
    $("#txtContaBancaria").autocomplete({
        source: function (request, response) {
            $.ajax({
                url: window.siteRoot + 'movimentoBancario/obterContaBancaria',
                data: { "prefix": request.term },
                type: "POST",
                success: function (data) {
                    response($.map(data, function (item) { return item; }));
                },
                error: function (response) {
                    alert(response.responseText);
                },
                failure: function (response) {
                    alert(response.responseText);
                }
            });
        },
        delay: 200,
        minLength: 2,
        select: function (e, i) {
            $("#ContaBancariaId").val(i.item.val);
        }
    });

    $("#txtContaBancaria").on('change keyup copy paste cut', function () {
        if (!this.value) {
            $("#ContaBancariaId").val('');
        }
    });


    /*AUTO COMPLETE PARA O CAMPO CATEGORIA*/
    $("#txtCategoria").autocomplete({
        source: function (request, response) {
            $.ajax({
                url: window.siteRoot + 'movimentoBancario/obterCategoria',
                data: { "prefix": request.term },
                type: "POST",
                success: function (data) {
                    response($.map(data, function (item) { return item; }));
                },
                error: function (response) {
                    alert(response.responseText);
                },
                failure: function (response) {
                    alert(response.responseText);
                }
            });
        },
        delay: 200,
        minLength: 2,
        select: function (e, i) {
            $("#CategoriaId").val(i.item.val);
        }
    });

    $("#txtCategoria").on('change keyup copy paste cut', function () {
        if (!this.value) {
            $("#CategoriaId").val('');
        }
    });


    /*AUTO COMPLETE PARA O CAMPO PESSOA*/
    $("#txtPessoa").autocomplete({
        source: function (request, response) {
            $.ajax({
                url: window.siteRoot + 'movimentoBancario/obterPessoa',
                data: { "prefix": request.term },
                type: "POST",
                success: function (data) {
                    response($.map(data, function (item) { return item; }));
                },
                error: function (response) {
                    alert(response.responseText);
                },
                failure: function (response) {
                    alert(response.responseText);
                }
            });
        },
        delay: 200,
        minLength: 2,
        select: function (e, i) {
            $("#PessoaId").val(i.item.val);
        }
    });

    $("#txtPessoa").on('change keyup copy paste cut', function () {
        if (!this.value) {
            $("#PessoaId").val('');
        }
    });


    /*AUTO COMPLETE PARA O CAMPO CENTRO DE CLASSIFICAÇÃO*/
    $("#txtCentroClassificacao").autocomplete({
        source: function (request, response) {
            $.ajax({
                url: window.siteRoot + 'movimentoBancario/obterCentroClassificacao',
                data: { "prefix": request.term },
                type: "POST",
                success: function (data) {
                    response($.map(data, function (item) { return item; }));
                },
                error: function (response) {
                    alert(response.responseText);
                },
                failure: function (response) {
                    alert(response.responseText);
                }
            });
        },
        delay: 200,
        minLength: 2,
        select: function (e, i) {
            $("#CentroClassificacaoId").val(i.item.val);
        }
    });

    $("#txtCentroClassificacao").on('change keyup copy paste cut', function () {
        if (!this.value) {
            $("#CentroClassificacaoId").val('');
        }
    });

});
