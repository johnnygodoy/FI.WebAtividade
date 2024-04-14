using FI.AtividadeEntrevista.DAL;
using FI.AtividadeEntrevista.DML;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System;
using System.Linq;

internal class DaoCliente : AcessoDados
{
    internal long Incluir(Cliente cliente)
    {
        using (var conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["BancoDeDados"].ConnectionString))
        {
            conn.Open();

            var parametros = new List<MySqlParameter>
        {
            new MySqlParameter("p_NOME", MySqlDbType.VarChar) { Value = cliente.Nome },
            new MySqlParameter("p_SOBRENOME", MySqlDbType.VarChar) { Value = cliente.Sobrenome },
            new MySqlParameter("p_NACIONALIDADE", MySqlDbType.VarChar) { Value = cliente.Nacionalidade },
            new MySqlParameter("p_CEP", MySqlDbType.VarChar) { Value = cliente.CEP },
            new MySqlParameter("p_ESTADO", MySqlDbType.VarChar) { Value = cliente.Estado },
            new MySqlParameter("p_CIDADE", MySqlDbType.VarChar) { Value = cliente.Cidade },
            new MySqlParameter("p_LOGRADOURO", MySqlDbType.VarChar) { Value = cliente.Logradouro },
            new MySqlParameter("p_EMAIL", MySqlDbType.VarChar) { Value = cliente.Email },
            new MySqlParameter("p_TELEFONE", MySqlDbType.VarChar) { Value = cliente.Telefone },
            new MySqlParameter("p_CPF", MySqlDbType.VarChar) { Value = cliente.CPF }
        };

            var cmd = CriarComando(conn, "FI_SP_IncClienteV2", parametros, CommandType.StoredProcedure);
            var resultado = cmd.ExecuteScalar();
            conn.Close(); // Fechar conexão após execução
            return (resultado != null) ? Convert.ToInt64(resultado) : 0;
        }
    }


    internal Cliente Consultar(long Id)
    {
        using (var conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["BancoDeDados"].ConnectionString))
        {
            conn.Open();

            List<MySqlParameter> parametros = new List<MySqlParameter>
            {
                new MySqlParameter("@p_id", MySqlDbType.Int64) { Value = Id }
            };

            var ds = Consultar(conn, "fi_sp_conscliente", parametros);
            conn.Close(); // Fechar conexão após execução

            // Verifica se há dados retornados e retorna o primeiro cliente encontrado, ou null se nenhum cliente for encontrado
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

    internal List<Cliente> Pesquisa(int iniciarEm, int quantidade, string campoOrdenacao, bool crescente, out int qtd)
    {
        using (var conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["BancoDeDados"].ConnectionString))
        {
            conn.Open();

            var parametros = new List<MySqlParameter>
        {
            new MySqlParameter("p_iniciarem", MySqlDbType.Int32) { Value = iniciarEm },
            new MySqlParameter("p_quantidade", MySqlDbType.Int32) { Value = quantidade },
            new MySqlParameter("p_campoordenacao", MySqlDbType.VarChar) { Value = campoOrdenacao },
            new MySqlParameter("p_crescente", MySqlDbType.Bit) { Value = crescente ? 1 : 0 }
        };

            // Adicione um parâmetro de saída para qtd
            var parametroQtd = new MySqlParameter("qtd", MySqlDbType.Int32);
            parametroQtd.Direction = ParameterDirection.Output;
            parametros.Add(parametroQtd);

            var ds = Consultar(conn, "fi_sp_pesqcliente", parametros);

            List<Cliente> cli = Converter(ds);

            conn.Close(); // Fechar conexão após execução
            int iQtd = 0;

            if (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
                int.TryParse(ds.Tables[1].Rows[0][0].ToString(), out iQtd);

            qtd = iQtd;

            return cli;
        }
    }
   


    internal void Alterar(Cliente cliente)
    {
        using (var conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["BancoDeDados"].ConnectionString))
        {
            conn.Open();

            var parametros = new List<MySqlParameter>
            {
                new MySqlParameter("p_nome", MySqlDbType.VarChar) { Value = cliente.Nome },
                new MySqlParameter("p_sobrenome", MySqlDbType.VarChar) { Value = cliente.Sobrenome },
                new MySqlParameter("p_nacionalidade", MySqlDbType.VarChar) { Value = cliente.Nacionalidade },
                new MySqlParameter("p_cep", MySqlDbType.VarChar) { Value = cliente.CEP },
                new MySqlParameter("p_estado", MySqlDbType.VarChar) { Value = cliente.Estado },
                new MySqlParameter("p_cidade", MySqlDbType.VarChar) { Value = cliente.Cidade },
                new MySqlParameter("p_logradouro", MySqlDbType.VarChar) { Value = cliente.Logradouro },
                new MySqlParameter("p_email", MySqlDbType.VarChar) { Value = cliente.Email },
                new MySqlParameter("p_telefone", MySqlDbType.VarChar) { Value = cliente.Telefone },
                new MySqlParameter("p_cpf", MySqlDbType.VarChar) { Value = cliente.CPF },
                new MySqlParameter("p_id", MySqlDbType.Int64) { Value = cliente.Id }
            };

            Executar(conn, "fi_sp_altcliente", parametros);
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

            Executar(conn, "fi_sp_delcliente", parametros);
            conn.Close(); // Fechar conexão após execução
        }
    }

    private List<Cliente> Converter(DataSet ds)
    {
        var lista = new List<Cliente>();
        if (ds.Tables.Count > 0)
        {
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                var cli = new Cliente
                {
                    // Mapeamento dos dados para o objeto Cliente...
                };
                lista.Add(cli);
            }
        }
        return lista;
    }

    internal List<Cliente> Listar()
    {
        List<Cliente> clientes = new List<Cliente>();

        return clientes;
    }
}
