using FI.AtividadeEntrevista.DAL;
using FI.AtividadeEntrevista.DML;
using Npgsql;
using NpgsqlTypes;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System;
using System.Linq;

internal class DaoCliente : AcessoDados
{
    internal long Incluir(Cliente cliente)
    {
        using (var conn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["BancoDeDados"].ConnectionString))
        {
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }

            var parametros = new List<NpgsqlParameter>
            {
                new NpgsqlParameter("_nome", NpgsqlDbType.Text) { Value = cliente.Nome },
                new NpgsqlParameter("_sobrenome", NpgsqlDbType.Text) { Value = cliente.Sobrenome },
                new NpgsqlParameter("_nacionalidade", NpgsqlDbType.Text) { Value = cliente.Nacionalidade },
                new NpgsqlParameter("_cep", NpgsqlDbType.Text) { Value = cliente.CEP },
                new NpgsqlParameter("_estado", NpgsqlDbType.Text) { Value = cliente.Estado },
                new NpgsqlParameter("_cidade", NpgsqlDbType.Text) { Value = cliente.Cidade },
                new NpgsqlParameter("_logradouro", NpgsqlDbType.Text) { Value = cliente.Logradouro },
                new NpgsqlParameter("_email", NpgsqlDbType.Text) { Value = cliente.Email },
                new NpgsqlParameter("_telefone", NpgsqlDbType.Text) { Value = cliente.Telefone },
                new NpgsqlParameter("_cpf", NpgsqlDbType.Char) { Value = cliente.CPF }
            };

            var cmd = CriarComando(conn, "fi_sp_incliente_v2", parametros, CommandType.StoredProcedure);
            var resultado = cmd.ExecuteScalar();
            conn.Close(); // Fechar conexão após execução
            return (resultado != null) ? Convert.ToInt64(resultado) : 0;
        }
    }

    internal Cliente Consultar(long Id)
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
        using (var conn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["BancoDeDados"].ConnectionString))
        {
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }

            var parametros = new List<NpgsqlParameter>
            {
                new NpgsqlParameter("_iniciarem", NpgsqlDbType.Integer) { Value = iniciarEm },
                new NpgsqlParameter("_quantidade", NpgsqlDbType.Integer) { Value = quantidade },
                new NpgsqlParameter("_campoordenacao", NpgsqlDbType.Text) { Value = campoOrdenacao },
                new NpgsqlParameter("_crescente", NpgsqlDbType.Bit) { Value = crescente ? true : false }
            };

            var ds = Consultar(conn, "fi_sp_pesqcliente", parametros);
            qtd = (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0) ? Convert.ToInt32(ds.Tables[1].Rows[0][0]) : 0;
            conn.Close(); // Fechar conexão após execução
            return Converter(ds);
        }
    }

    internal void Alterar(Cliente cliente)
    {
        using (var conn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["BancoDeDados"].ConnectionString))
        {
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }

            var parametros = new List<NpgsqlParameter>
            {
                new NpgsqlParameter("_nome", NpgsqlDbType.Text) { Value = cliente.Nome },
                new NpgsqlParameter("_sobrenome", NpgsqlDbType.Text) { Value = cliente.Sobrenome },
                new NpgsqlParameter("_nacionalidade", NpgsqlDbType.Text) { Value = cliente.Nacionalidade },
                new NpgsqlParameter("_cep", NpgsqlDbType.Text) { Value = cliente.CEP },
                new NpgsqlParameter("_estado", NpgsqlDbType.Text) { Value = cliente.Estado },
                new NpgsqlParameter("_cidade", NpgsqlDbType.Text) { Value = cliente.Cidade },
                new NpgsqlParameter("_logradouro", NpgsqlDbType.Text) { Value = cliente.Logradouro },
                new NpgsqlParameter("_email", NpgsqlDbType.Text) { Value = cliente.Email },
                new NpgsqlParameter("_telefone", NpgsqlDbType.Text) { Value = cliente.Telefone },
                new NpgsqlParameter("_cpf", NpgsqlDbType.Char) { Value = cliente.CPF },
                new NpgsqlParameter("_id", NpgsqlDbType.Bigint) { Value = cliente.Id }
            };

            Executar(conn, "fi_sp_altcliente", parametros);
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
