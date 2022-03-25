$(".pagination a").click(function () {
    if (event.preventDefault) {
        event.preventDefault();
    } else {
        event.returnValue = false;
    }

    var url = new URL($(this)[0].href);
    var page = url.searchParams.get("page");

    $("#page").val(page);
    $("#formBusca").submit();
});