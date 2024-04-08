using FI.AtividadeEntrevista.DAL.Clientes;
using FI.AtividadeEntrevista.DML;
using FI.AtividadeEntrevista.helpers;
using System;
using System.Collections.Generic;

namespace FI.AtividadeEntrevista.BLL
{
    public class BoBeneficiario
    {
        private readonly DaoBeneficiarios _daoBeneficiarios;
        private readonly VerificarCPF _verificarCpf;

        public BoBeneficiario()
        {
            _daoBeneficiarios = new DaoBeneficiarios();
            _verificarCpf = new VerificarCPF();
        }

        public long Incluir(Beneficiario beneficiario)
        {
            if (!ValidarBeneficiario(beneficiario))
            {
                throw new Exception("Beneficiário inválido.");
            }

            // Remover pontos e traços do CPF
            beneficiario.CPF = beneficiario.CPF.Replace(".", "").Replace("-", "");

            if (_verificarCpf.VerificaCPF(beneficiario.CPF))
            {
                throw new Exception("CPF já cadastrado.");
            }

            return _daoBeneficiarios.Incluir(beneficiario);
        }

        public void Alterar(Beneficiario beneficiario)
        {
            if (!ValidarBeneficiario(beneficiario))
            {
                throw new Exception("Beneficiário inválido.");
            }

            if (!_verificarCpf.VerificaCPF(beneficiario.CPF))
            {
                throw new Exception("CPF não cadastrado.");
            }

            _daoBeneficiarios.Alterar(beneficiario);
        }

        public List<Beneficiario> Listar()
        {
            return _daoBeneficiarios.Listar();
        }

        public Beneficiario Consultar(long id)
        {
            return _daoBeneficiarios.Consultar(id);
        }

        public void Excluir(long id)
        {
            _daoBeneficiarios.Excluir(id);
        }

        private bool ValidarBeneficiario(Beneficiario beneficiario)
        {
            // Aqui você pode adicionar regras de validação do beneficiário, se necessário
            return !string.IsNullOrWhiteSpace(beneficiario.Nome) &&
                   !string.IsNullOrWhiteSpace(beneficiario.CPF);
        }

        // public List<Beneficiario> Pesquisa(string cpf)
        // {
        //     if (!string.IsNullOrEmpty(cpf))
        //     {
        //         // Se houver um CPF fornecido, realiza a pesquisa no DAO
        //         return _daoBeneficiarios.Pesquisa(cpf);
        //     }
        //     else
        //     {
        //         // Caso contrário, retorna uma lista vazia
        //         return new List<Beneficiario>();
        //     }
        // }
    }
}
