using System.Data;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace FI.AtividadeEntrevista.DAL
{
    internal class AcessoDados
    {
        private string StringDeConexao
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

        protected MySqlCommand CriarComando(MySqlConnection conn, string comandoSql, List<MySqlParameter> parametros, CommandType tipoComando)
        {
            var comando = new MySqlCommand(comandoSql, conn);
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

        internal void Executar(MySqlConnection conn, string NomeProcedure, List<MySqlParameter> parametros)
        {
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }

            using (MySqlCommand comando = CriarComando(conn, NomeProcedure, parametros, CommandType.StoredProcedure))
            {
                comando.ExecuteNonQuery();
            }

            conn.Close();
        }

        internal DataSet Consultar(MySqlConnection conn, string NomeProcedure, List<MySqlParameter> parametros)
        {
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }

            using (MySqlCommand comando = CriarComando(conn, NomeProcedure, parametros, CommandType.StoredProcedure))
            {
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(comando))
                {
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);
                    return ds;
                }
            }
        }
    }
}
