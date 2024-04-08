using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FI.AtividadeEntrevista.helpers
{
    public class VerificarCPF
    {

        public  bool VerificaCPF(string CPF)
        {
            using (var conn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["BancoDeDados"].ConnectionString))
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }

                var comando = new NpgsqlCommand("fi_sp_verificacliente", conn);
                comando.CommandType = CommandType.StoredProcedure;

                var paramCPF = new NpgsqlParameter("cpf", NpgsqlDbType.Text);
                paramCPF.Value = CPF;
                comando.Parameters.Add(paramCPF);

                var ds = new DataSet();
                var da = new NpgsqlDataAdapter(comando);
                da.Fill(ds);

                conn.Close(); // Fechar conexão após execução

                return ds.Tables[0].Rows.Count > 0;
            }
        }
    }
}
