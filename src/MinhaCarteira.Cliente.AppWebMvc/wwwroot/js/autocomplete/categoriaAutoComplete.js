$(function () {
    $("#edtCategoriaNome").autocomplete({
        source: function (request, response) {
            $.ajax({
                url: window.siteRoot + 'categoria/ObterCategorias',
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
            $("#IdCategoriaPai").val(i.item.val);
        }
    });

    $("#edtCategoriaNome").on('change keyup copy paste cut', function () {
        if (!this.value) {
            $("#IdCategoriaPai").val('');
        }
    });
});
