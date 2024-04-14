using MySql.Data.MySqlClient;
using System;
using System.Configuration;
using System.Data;

namespace FI.AtividadeEntrevista.helpers
{
    public class VerificarCPF
    {
        public bool VerificaCPF(string CPF)
        {
            using (var conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["BancoDeDados"].ConnectionString))
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }

                var comando = new MySqlCommand("fi_sp_validar_cpf", conn);
                comando.CommandType = CommandType.StoredProcedure;

                var paramCPF = new MySqlParameter("cpf", MySqlDbType.VarChar);
                paramCPF.Value = CPF;
                comando.Parameters.Add(paramCPF);

                var paramValido = new MySqlParameter("valido", MySqlDbType.Bit);
                paramValido.Direction = ParameterDirection.Output;
                comando.Parameters.Add(paramValido);

                comando.ExecuteNonQuery();

                int valido = Convert.ToInt32(comando.Parameters["valido"].Value);

                conn.Close();

                return valido == 1; // Retorna verdadeiro se o CPF for válido (retorno 1)
            }
        }
    }

}
