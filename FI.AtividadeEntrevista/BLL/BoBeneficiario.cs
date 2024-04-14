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
                return _daoBeneficiarios.Incluir(beneficiario);
            }
            else
            {
                throw new Exception("CPF não encontrado em nossa base de dados.");
            }
            
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

        public bool Consultar(string cpfCliente)
        {
            return _daoBeneficiarios.Consultar(cpfCliente);
        }

        public void Excluir(long id)
        {
            _daoBeneficiarios.Excluir(id);
        }

        private bool ValidarBeneficiario(Beneficiario beneficiario)
        {           
            return !string.IsNullOrWhiteSpace(beneficiario.Nome) &&
                   !string.IsNullOrWhiteSpace(beneficiario.CPF);
        }
   
    }
}
