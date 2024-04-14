$(document).ready(function () {
    var urlPostCliente = 'http://localhost:13007/Cliente/Incluir';
    var urlPostBeneficiario = 'http://localhost:13007/Beneficiario/Incluir';
    var beneficiarioTemp = {}; // Variável temporária para armazenar os dados do beneficiário
    var urlVerificarBeneficiario = 'http://localhost:13007/Beneficiario/VerificarBeneficiario';
    var cpfClienteGlobal; 

    // Armazenar uma referência ao botão de beneficiários
    var btnBeneficiarios = $('#btnAbrirModalBeneficiarios');

    // Desabilitar o botão de beneficiários inicialmente
    btnBeneficiarios.prop('disabled', true);

    // Função para verificar se todos os campos do cliente estão preenchidos
    function verificarCamposClientePreenchidos() {
        var nomeCliente = $('#Nome').val();
        cpfClienteGlobal = $('#CPF').val();
        var cepCliente = $('#CEP').val();
        var emailCliente = $('#Email').val();
        var sobrenomeCliente = $('#Sobrenome').val();
        var nacionalidadeCliente = $('#Nacionalidade').val();
        var estadoCliente = $('#Estado').val();
        var cidadeCliente = $('#Cidade').val();
        var logradouroCliente = $('#Logradouro').val();
        var telefoneCliente = $('#Telefone').val();

        // Verificar se todos os campos obrigatórios do cliente estão preenchidos
        if (nomeCliente && cpfClienteGlobal && cepCliente && emailCliente && sobrenomeCliente && nacionalidadeCliente && estadoCliente && cidadeCliente && logradouroCliente && telefoneCliente) {
            return true;
        } else {
            return false;
        }
    }

    // Função para habilitar ou desabilitar o botão de beneficiários com base nos campos do cliente
    function atualizarEstadoBtnBeneficiarios() {
        if (verificarCamposClientePreenchidos()) {
            btnBeneficiarios.prop('disabled', false);
        } else {
            btnBeneficiarios.prop('disabled', true);
        }
    }

    // Adicionar eventos de alteração aos campos do cliente para atualizar o estado do botão de beneficiários
    $('#Nome, #CPF, #CEP, #Email, #Sobrenome, #Nacionalidade, #Estado, #Cidade, #Logradouro, #Telefone').on('input', function () {
        atualizarEstadoBtnBeneficiarios();
    });



    $('#formCadastro').submit(function (e) {
        e.preventDefault();
        var cpf = $('#CPF').val();
        if (!validarCPF(cpf)) {
            exibirModal("CPF Inválido", "O CPF inserido é inválido. Por favor, insira um CPF válido.");
            return;
        }
        $.ajax({
            url: urlPostCliente,
            method: "POST",
            data: {
                "NOME": $(this).find("#Nome").val(),
                "CEP": $(this).find("#CEP").val(),
                "Email": $(this).find("#Email").val(),
                "Sobrenome": $(this).find("#Sobrenome").val(),
                "Nacionalidade": $(this).find("#Nacionalidade").val(),
                "Estado": $(this).find("#Estado").val(),
                "Cidade": $(this).find("#Cidade").val(),
                "Logradouro": $(this).find("#Logradouro").val(),
                "Telefone": formatarTelefone($(this).find("#Telefone").val()),
                "CPF": cpf
            },
            error: function (r) {
                if (r.status == 400)
                    exibirModal("Ocorreu um erro", r.responseJSON);
                else if (r.status == 500)
                    exibirModal("Ocorreu um erro", "Ocorreu um erro interno no servidor.");
            },
            success: function (r) {
                exibirModal("Sucesso!", "Cliente cadastrado com sucesso.");

                // Verificar se há dados de beneficiário temporário e incluir o beneficiário
                if (Object.keys(beneficiarioTemp).length !== 0) {
                    incluirBeneficiario(); // Chamada do método incluirBeneficiario()
                }
            }
        });
    });

    $('#btnSalvarBeneficiario').click(function () {
        var cpfBeneficiario = $('#cpfBeneficiario').val();
        var cpfClienteLimpo = cpfClienteGlobal.replace(/\D/g, '');
        if (!validarCPF(cpfClienteLimpo)) {
            exibirModal("CPF do Beneficiário Inválido", "O CPF inserido para o beneficiário é inválido. Por favor, insira um CPF válido.");
            return;
        }

        if (cpfBeneficiario === cpfClienteLimpo) {
            exibirModal("CPF do Cliente", "O CPF do beneficiário não pode ser igual ao CPF do cliente.");
            return;
        }
        // Verificar se já existe um beneficiário para o CPF do cliente
        $.ajax({
            url: 'http://localhost:13007/Beneficiario/VerificarBeneficiario',
            method: 'POST',
            contentType: 'application/json',
            data: JSON.stringify({ cpfCliente: cpfClienteLimpo }), // Passa o CPF no corpo da solicitação
            success: function (response) {
                console.log(response);
                if (response === true) {
                    exibirModal("Beneficiário Existente", "Já existe um beneficiário cadastrado para este CPF de cliente. Você pode alterar os dados do beneficiário.");
                } else {
                    // Armazenar temporariamente os dados do beneficiário
                    beneficiarioTemp = {
                        "Nome": $('#nomeBeneficiario').val(),
                        "CPF": cpfBeneficiario,
                        "IdCliente": $('#idCliente').val()
                    };                   

                    // Fechar a modal de inclusão do beneficiário
                    $('#modalBeneficiarios').modal('hide');
                }
            },
            error: function (xhr, status, error) {
                console.error(error);
            }
        });
    });

    // Função para incluir o beneficiário
    function incluirBeneficiario() {
        $.ajax({
            url: urlPostBeneficiario,
            method: "POST",
            data: beneficiarioTemp,
            error: function (r) {
                if (r.status == 400)
                    exibirModal("Ocorreu um erro", r.responseJSON);
                else if (r.status == 500)
                    exibirModal("Ocorreu um erro", "Ocorreu um erro interno no servidor.");
            },
            success: function (r) {
                exibirModal("Sucesso!", "Beneficiário cadastrado com sucesso.");
                beneficiarioTemp = {}; // Limpar os dados do beneficiário temporário após incluí-lo com sucesso
            }
        });
    }

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
        telefone = telefone.replace(/\D/g, ''); // Remove caracteres não numéricos
        telefone = telefone.replace(/^(\d{2})(\d)/g, '($1) $2'); // Adiciona parênteses em volta do DDD
        telefone = telefone.replace(/(\d)(\d{4})$/, '$1-$2'); // Adiciona hífen entre os últimos dígitos
        return telefone;
    }

    function formatarTelefone(telefone) {
        telefone = telefone.replace(/\D/g, ''); // Remove caracteres não numéricos
        telefone = telefone.replace(/^(\d{2})(\d)/g, '($1) $2'); // Adiciona parênteses em volta do DDD
        telefone = telefone.replace(/(\d)(\d{4})$/, '$1-$2'); // Adiciona hífen entre os últimos dígitos
        return telefone;
    }

    $('#Telefone').on('input', function () {
        var telefone = $(this).val();
        telefone = telefone.replace(/\D/g, '');
        telefone = telefone.replace(/^(\d{2})(\d)/g, '($1) $2');
        telefone = telefone.replace(/(\d)(\d{4})$/, '$1-$2');
        $(this).val(telefone);
    });

    $('#CPF').keyup(function () {
        var cpf = $(this).val();
        cpf = cpf.replace(/\D/g, '');
        cpf = cpf.replace(/(\d{3})(\d)/, '$1.$2');
        cpf = cpf.replace(/(\d{3})(\d)/, '$1.$2');
        cpf = cpf.replace(/(\d{3})(\d{1,2})$/, '$1-$2');
        $(this).val(cpf);
    });

    $('#cpfBeneficiario').keyup(function () {
        var cpf = $(this).val();
        cpf = cpf.replace(/\D/g, '');
        cpf = cpf.replace(/(\d{3})(\d)/, '$1.$2');
        cpf = cpf.replace(/(\d{3})(\d)/, '$1.$2');
        cpf = cpf.replace(/(\d{3})(\d{1,2})$/, '$1-$2');
        $(this).val(cpf);
    });

    $('#CEP').keyup(function () {
        var cep = $(this).val();
        cep = cep.replace(/\D/g, '');
        cep = cep.replace(/(\d{5})(\d)/, '$1-$2');
        $(this).val(cep);
    });

    function incluirBeneficiario() {
        $.ajax({
            url: urlPostBeneficiario,
            method: "POST",
            data: beneficiarioTemp,
            error: function (r) {
                if (r.status == 400)
                    exibirModal("Ocorreu um erro", r.responseJSON);
                else if (r.status == 500)
                    exibirModal("Ocorreu um erro", "Ocorreu um erro interno no servidor.");
            },
            success: function (r) {
                exibirModal("Sucesso!", "Beneficiário cadastrado com sucesso.");
                beneficiarioTemp = {}; // Limpar os dados do beneficiário temporário após incluí-lo com sucesso
            }
        });
    }

    function exibirModal(titulo, texto) {
        var random = Math.random().toString().replace('.', '');
        var modalHtml = '<div id="' + random + '" class="modal fade">                                                               ' +
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

        $('body').append(modalHtml);
        $('#' + random).modal('show');
    }
});
