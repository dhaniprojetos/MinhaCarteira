$(".pagination a").click(function () {
    if (event.preventDefault) {
        event.preventDefault();
    } else {
        event.returnValue = false;
    }
    $("#page").val($(this).html());
    $("#formBusca").submit();
});