using FI.AtividadeEntrevista.DAL;
using FI.AtividadeEntrevista.DML;
using Npgsql;
using NpgsqlTypes;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System;
using System.Linq;
using System.Threading.Tasks;
using FI.AtividadeEntrevista.BLL;

namespace FI.AtividadeEntrevista.DAL.Clientes
{
    internal class DaoBeneficiarios: AcessoDados
    {
        internal long Incluir(Beneficiario beneficiario)
        {
            using (var conn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["BancoDeDados"].ConnectionString))
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }

                var parametros = new List<NpgsqlParameter>
            {
                new NpgsqlParameter("_nome", NpgsqlDbType.Text) { Value = beneficiario.Nome },              
                new NpgsqlParameter("_cpf", NpgsqlDbType.Char) { Value = beneficiario.CPF }
            };

                var cmd = CriarComando(conn, "fi_sp_incluir_beneficiario", parametros, CommandType.StoredProcedure);
                var resultado = cmd.ExecuteScalar();
                conn.Close(); // Fechar conexão após execução
                return (resultado != null) ? Convert.ToInt64(resultado) : 0;
            }
        }

        internal Beneficiario Consultar(long Id)
        {
            using (var conn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["BancoDeDados"].ConnectionString))
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }

                List<NpgsqlParameter> parametros = new List<NpgsqlParameter>
        {
            new NpgsqlParameter("@_id", NpgsqlDbType.Bigint) { Value = Id }
        };

                var ds = Consultar(conn, "fi_sp_consultar_beneficiario", parametros);
                conn.Close(); // Fechar conexão após execução

                // Verifica se há dados retornados e retorna o primeiro beneficiario encontrado, ou null se nenhum beneficiario for encontrado
                if (ds.Tables[0].Rows.Count > 0)
                {
                    return Converter(ds).FirstOrDefault();
                }
                else
                {
                    return null;
                }
            }
        }
      
        internal void Alterar(Beneficiario beneficiario)
        {
            using (var conn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["BancoDeDados"].ConnectionString))
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }

                var parametros = new List<NpgsqlParameter>
            {
                new NpgsqlParameter("_nome", NpgsqlDbType.Text) { Value = beneficiario.Nome },               
                new NpgsqlParameter("_cpf", NpgsqlDbType.Char) { Value = beneficiario.CPF },
                new NpgsqlParameter("_id", NpgsqlDbType.Bigint) { Value = beneficiario.Id }
            };

                Executar(conn, "fi_sp_alterar_beneficiario", parametros);
                conn.Close(); // Fechar conexão após execução
            }
        }

        internal void Excluir(long Id)
        {
            using (var conn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["BancoDeDados"].ConnectionString))
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }

                var parametros = new List<NpgsqlParameter>
            {
                new NpgsqlParameter("_id", NpgsqlDbType.Bigint) { Value = Id }
            };

                Executar(conn, "fi_sp_delcliente", parametros);
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
                    var cli = new Beneficiario
                    {
                        // Mapeamento dos dados para o objeto Cliente...
                    };
                    lista.Add(cli);
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
