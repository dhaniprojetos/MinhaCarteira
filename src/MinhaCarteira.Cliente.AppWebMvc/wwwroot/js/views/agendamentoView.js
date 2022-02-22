function definirTipoParcelas(el) {
    switch (el) {
        case 'tipoParcelaParcelada':
            $('#numeroParcelas').removeClass('d-none');
            $('#tipoRecorrencia').addClass('d-none');
            break;
        case 'tipoParcelaRecorrente':
            $('#numeroParcelas').addClass('d-none');
            $('#tipoRecorrencia').removeClass('d-none');
            // expected output: "Mangoes and papayas are $2.79 a pound."
            break;
        default:
            $('#numeroParcelas').addClass('d-none');
            $('#tipoRecorrencia').addClass('d-none');
    }

}