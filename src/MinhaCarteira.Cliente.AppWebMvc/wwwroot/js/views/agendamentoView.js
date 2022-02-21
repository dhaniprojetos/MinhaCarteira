function definirTipoParcelas(el) {
    var value = el.id;

    switch (value) {
        case 'btnTipoParcela1':
            $('#numeroParcelas').removeClass('d-none');
            $('#tipoRecorrencia').addClass('d-none');
            break;
        case 'btnTipoParcela2':
            $('#numeroParcelas').addClass('d-none');
            $('#tipoRecorrencia').removeClass('d-none');
            // expected output: "Mangoes and papayas are $2.79 a pound."
            break;
        default:
            $('#numeroParcelas').addClass('d-none');
            $('#tipoRecorrencia').addClass('d-none');
    }

}