$(function () {
    $("#txtInstituicao").autocomplete({
        source: function (request, response) {
            $.ajax({
                url: window.siteRoot + 'contabancaria/obterInstituicoesFinanceira',
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
            $("#IdInstituicaoFinanceira").val(i.item.val);
        }
    });

    $("#txtInstituicao").on('change keyup copy paste cut', function () {
        if (!this.value) {
            $("#IdInstituicaoFinanceira").val('');
        }
    });
});
