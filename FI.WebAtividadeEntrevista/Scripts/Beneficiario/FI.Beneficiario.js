$(document).ready(function () {
    $('#formCadastroBeneficiario').submit(function (e) {
        e.preventDefault();
        var cpf = $('#CPF_beneficiario').val();
        if (!validarCPF(cpf)) {
            ModalDialog("CPF Inválido", "O CPF inserido é inválido. Por favor, insira um CPF válido.");
            return;
        }

        // Envio do formulário via AJAX
        $.ajax({
            url: urlPost,
            method: "POST",
            data: {
                "Nome": $(this).find("#Nome_beneficiario").val(),
                "CEP": $(this).find("#CEP_beneficiario").val(),
                "Email": $(this).find("#Email_beneficiario").val(),
                "Sobrenome": $(this).find("#Sobrenome_beneficiario").val(),
                "Nacionalidade": $(this).find("#Nacionalidade_beneficiario").val(),
                "Estado": $(this).find("#Estado_beneficiario").val(),
                "Cidade": $(this).find("#Cidade_beneficiario").val(),
                "Logradouro": $(this).find("#Logradouro_beneficiario").val(),
                "Telefone": formatarTelefone($(this).find("#Telefone_beneficiario").val()),
                "CPF": cpf
            },
            error: function (r) {
                if (r.status == 400)
                    ModalDialog("Ocorreu um erro", r.responseJSON);
                else if (r.status == 500)
                    ModalDialog("Ocorreu um erro", "Ocorreu um erro interno no servidor.");
            },
            success: function (r) {
                ModalDialog("Sucesso!", r);
                $("#formCadastroBeneficiario")[0].reset();
            }
        });
    });

    // Função para formatar telefone
    $('#Telefone_beneficiario').on('input', function () {
        var telefone = $(this).val();
        telefone = telefone.replace(/\D/g, '');
        telefone = telefone.replace(/^(\d{2})(\d)/g, '($1) $2');
        telefone = telefone.replace(/(\d)(\d{4})$/, '$1-$2');
        $(this).val(telefone);
    });

    // Função para formatar CPF
    $('#CPF_beneficiario').keyup(function () {
        var cpf = $(this).val();
        cpf = cpf.replace(/\D/g, '');
        cpf = cpf.replace(/(\d{3})(\d)/, '$1.$2');
        cpf = cpf.replace(/(\d{3})(\d)/, '$1.$2');
        cpf = cpf.replace(/(\d{3})(\d{1,2})$/, '$1-$2');
        $(this).val(cpf);
    });

    // Função para formatar CEP
    $('#CEP_beneficiario').keyup(function () {
        var cep = $(this).val();
        cep = cep.replace(/\D/g, '');
        cep = cep.replace(/(\d{5})(\d)/, '$1-$2');
        $(this).val(cep);
    });
});

// Função para validar CPF
function validarCPF(cpf) {
    cpf = cpf.replace(/\D/g, '');
    if (cpf.length !== 11 || !cpf.match(/^\d{11}$/)) {
        return false; // CPF deve ter exatamente 11 dígitos
    }
    var soma = 0;
    for (var i = 0; i < 9; i++) {
        soma += parseInt(cpf.charAt(i)) * (10 - i);
    }
    var resto = 11 - (soma % 11);
    var digitoVerificador1 = (resto === 10 || resto === 11) ? 0 : resto;
    soma = 0;
    for (var i = 0; i < 10; i++) {
        soma += parseInt(cpf.charAt(i)) * (11 - i);
    }
    resto = 11 - (soma % 11);
    var digitoVerificador2 = (resto === 10 || resto === 11) ? 0 : resto;
    if (parseInt(cpf.charAt(9)) !== digitoVerificador1 || parseInt(cpf.charAt(10)) !== digitoVerificador2) {
        return false; // Dígitos verificadores incorretos
    }
    return true; // CPF válido
}

// Função para formatar telefone
function formatarTelefone(telefone) {
    telefone = telefone.replace(/\D/g, '');
    telefone = telefone.replace(/^(\d{2})(\d)/g, '($1) $2');
    telefone = telefone.replace(/(\d)(\d{4})$/, '$1-$2');
    return telefone;
}

// Função para exibir modal
function ModalDialog(titulo, texto) {
    var random = Math.random().toString().replace('.', '');
    var texto = '<div id="' + random + '" class="modal fade">                                                               ' +
        '        <div class="modal-dialog">                                                                                 ' +
        '            <div class="modal-content">                                                                            ' +
        '                <div class="modal-header">                                                                         ' +
        '                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>         ' +
        '                    <h4 class="modal-title">' + titulo + '</h4>                                                    ' +
        '                </div>                                                                                             ' +
        '                <div class="modal-body">                                                                           ' +
        '                    <p>' + texto + '</p>                                                                           ' +
        '                </div>                                                                                             ' +
        '                <div class="modal-footer">                                                                         ' +
        '                    <button type="button" class="btn btn-default" data-dismiss="modal">Fechar</button>             ' +
        '                </div>                                                                                             ' +
        '            </div><!-- /.modal-content -->                                                                         ' +
        '  </div><!-- /.modal-dialog -->                                                                                    ' +
        '</div> <!-- /.modal -->                                                                                        ';

    $('body').append(texto);
    $('#' + random).modal('show');
}
