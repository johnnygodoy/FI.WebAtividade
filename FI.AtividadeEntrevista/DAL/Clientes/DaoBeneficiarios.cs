using FI.AtividadeEntrevista.DML;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;

namespace FI.AtividadeEntrevista.DAL.Clientes
{
    internal class DaoBeneficiarios : AcessoDados
    {
        internal long Incluir(Beneficiario beneficiario)
        {
            using (var conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["BancoDeDados"].ConnectionString))
            {
                conn.Open();

                var parametros = new List<MySqlParameter>
        {
            new MySqlParameter("p_cpf", MySqlDbType.VarChar) { Value = beneficiario.CPF },
            new MySqlParameter("p_nome", MySqlDbType.VarChar) { Value = beneficiario.Nome },
            new MySqlParameter("p_id_cliente", MySqlDbType.VarChar) { Value = beneficiario.IdCliente }
        };

                var cmd = CriarComando(conn, "fi_sp_incluir_beneficiario", parametros, CommandType.StoredProcedure);
                var resultado = cmd.ExecuteScalar();
                conn.Close(); // Fechar conexão após execução
                return (resultado != null) ? Convert.ToInt64(resultado) : 0;
            }
        }


        internal bool Consultar(string cpfCliente)
        {
            using (var conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["BancoDeDados"].ConnectionString))
            {
                conn.Open();

                // Parâmetro de saída
                MySqlParameter pExisteBeneficiario = new MySqlParameter("@p_existe_beneficiario", MySqlDbType.Bit);
                pExisteBeneficiario.Direction = ParameterDirection.Output;

                List<MySqlParameter> parametros = new List<MySqlParameter>
        {
            new MySqlParameter("@p_cpf", MySqlDbType.VarChar, 14) { Value = cpfCliente },
            pExisteBeneficiario // Adicionando o parâmetro de saída à lista de parâmetros
        };

                var ds = Consultar(conn, "fi_sp_consultar_beneficiario", parametros);
                conn.Close(); // Fechar conexão após execução

                // Retorna o valor booleano retornado pelo parâmetro de saída da stored procedure
                return Convert.ToBoolean(pExisteBeneficiario.Value);
            }
        }



        internal void Alterar(Beneficiario beneficiario)
        {
            using (var conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["BancoDeDados"].ConnectionString))
            {
                conn.Open();

                var parametros = new List<MySqlParameter>
                {
                    new MySqlParameter("_id", MySqlDbType.Int64) { Value = beneficiario.Id },
                    new MySqlParameter("_cpf", MySqlDbType.VarChar) { Value = beneficiario.CPF },
                    new MySqlParameter("_nome", MySqlDbType.VarChar) { Value = beneficiario.Nome }
                };

                Executar(conn, "fi_sp_alterar_beneficiario", parametros);
                conn.Close(); // Fechar conexão após execução
            }
        }

        internal void Excluir(long Id)
        {
            using (var conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["BancoDeDados"].ConnectionString))
            {
                conn.Open();

                var parametros = new List<MySqlParameter>
                {
                    new MySqlParameter("_id", MySqlDbType.Int64) { Value = Id }
                };

                Executar(conn, "fi_sp_excluir_beneficiario", parametros);
                conn.Close(); // Fechar conexão após execução
            }
        }

        private List<Beneficiario> Converter(DataSet ds)
        {
            var lista = new List<Beneficiario>();
            if (ds.Tables.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    var beneficiario = new Beneficiario
                    {
                        // Mapeamento dos dados para o objeto Beneficiario...
                    };
                    lista.Add(beneficiario);
                }
            }
            return lista;
        }

        internal List<Beneficiario> Listar()
        {
            List<Beneficiario> beneficiarios = new List<Beneficiario>();

            return beneficiarios;
        }
    }
}
