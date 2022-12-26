// Funções gerais

// Busca endereço pelo CEP
jQuery(function ($) {
    $("input[name='zip']").change(function () {
        var zipCode = $(this).val();
        if (zipCode.length <= 0) return;
        $.get("https://viacep.com.br/ws/" + zipCode + "/json/"),
            function (result) {
                if (result.erro == true) {
                    return;
                } else {

                    console.log(result);
                    $("input[id='address']").val(result.logradouro);
                    $("input[id='district']").val(result.bairro);

                }
            }
    });
});

// Selecione linha de tabela
jQuery(function ($) {
    $('#amxTable').on('click', 'tbody tr', function (event) {
        $(this).addClass('highlight').siblings().removeClass('highlight');
    });
});
