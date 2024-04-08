using System;
using System.Collections.Generic;
using FI.AtividadeEntrevista.DAL;
using FI.AtividadeEntrevista.DML;
using FI.AtividadeEntrevista.helpers;

namespace FI.AtividadeEntrevista.BLL
{
    public class BoCliente
    {
        private readonly DaoCliente _daoCliente;
        private readonly VerificarCPF _verificarCpf;

        public BoCliente()
        {
            _daoCliente = new DaoCliente();
            _verificarCpf = new VerificarCPF();
        }      

     
        public long Incluir(Cliente cliente)
        {
            if (!ValidarCliente(cliente))
            {
                throw new Exception("Cliente inválido.");
            }

            // Remover pontos e traços do CPF
            cliente.CPF = cliente.CPF.Replace(".", "").Replace("-", "");

            if (_verificarCpf.VerificaCPF(cliente.CPF))
            {
                throw new Exception("CPF já cadastrado.");
            }

            return _daoCliente.Incluir(cliente);
        }

        public void Alterar(Cliente cliente)
        {
            if (!ValidarCliente(cliente))
            {
                throw new Exception("Cliente inválido.");
            }

            if (!_verificarCpf.VerificaCPF(cliente.CPF))
            {
                throw new Exception("CPF não cadastrado.");
            }

            _daoCliente.Alterar(cliente);
        }

        public List<Cliente> Listar()
        {
            return _daoCliente.Listar();
        }

        public Cliente Consultar(long id)
        {
            return _daoCliente.Consultar(id);
        }

        public void Excluir(long id)
        {
            _daoCliente.Excluir(id);
        }

        private bool ValidarCliente(Cliente cliente)
        {
            // Aqui você pode adicionar regras de validação do cliente, se necessário
            return !string.IsNullOrWhiteSpace(cliente.Nome) &&
                   !string.IsNullOrWhiteSpace(cliente.CPF);
        }

        public List<Cliente> Pesquisa(int iniciarEm, int quantidade, string campoOrdenacao, bool crescente, out int qtd)
        {
            // Mapeamento do campo de ordenação para o formato esperado pela função em PostgreSQL
            string campoOrdenacaoPostgres = campoOrdenacao.ToLower(); // Garante que seja minúsculo
            if (campoOrdenacaoPostgres != "email" && campoOrdenacaoPostgres != "cpf")
            {
                campoOrdenacaoPostgres = "nome"; // Por padrão, ordena por nome
            }

            return _daoCliente.Pesquisa(iniciarEm, quantidade, campoOrdenacaoPostgres, crescente, out qtd);
        }

    }
}
