using System.Data;
using System.Collections.Generic;
using Npgsql;
using System.Configuration;

namespace FI.AtividadeEntrevista.DAL
{
    internal class AcessoDados
    {
        private string stringDeConexao
        {
            get
            {
                ConnectionStringSettings conn = ConfigurationManager.ConnectionStrings["BancoDeDados"];
                if (conn != null)
                    return conn.ConnectionString;
                else
                    return string.Empty;
            }
        }

        protected NpgsqlCommand CriarComando(NpgsqlConnection conn, string comandoSql, List<NpgsqlParameter> parametros, CommandType tipoComando)
        {
            var comando = new NpgsqlCommand(comandoSql, conn);
            comando.CommandType = tipoComando;

            if (parametros != null)
            {
                foreach (var parametro in parametros)
                {
                    comando.Parameters.Add(parametro);
                }
            }

            return comando;
        }

        internal void Executar(NpgsqlConnection conn, string NomeProcedure, List<NpgsqlParameter> parametros)
        {
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }

            using (NpgsqlCommand comando = CriarComando(conn, NomeProcedure, parametros, CommandType.StoredProcedure))
            {
                comando.ExecuteNonQuery();
            }

            conn.Close();
        }

        internal DataSet Consultar(NpgsqlConnection conn, string NomeProcedure, List<NpgsqlParameter> parametros)
        {
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }

            using (NpgsqlCommand comando = CriarComando(conn, NomeProcedure, parametros, CommandType.StoredProcedure))
            {
                using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(comando))
                {
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);
                    return ds;
                }
            }

            conn.Close();
        }
    }
}
