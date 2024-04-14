
using WebAtividadeEntrevista.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FI.AtividadeEntrevista.BLL;
using FI.AtividadeEntrevista.DML;


namespace WebAtividadeEntrevista.Controllers
{
    public class ClienteController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Incluir()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Incluir(ClienteModel model)
        {
            BoCliente bo = new BoCliente();

            if (!this.ModelState.IsValid)
            {
                List<string> erros = (from item in ModelState.Values
                                      from error in item.Errors
                                      select error.ErrorMessage).ToList();

                Response.StatusCode = 400;
                return Json(string.Join(Environment.NewLine, erros));
            }
            else
            {
                // Incluindo o cliente
                long clienteId = bo.Incluir(new Cliente()
                {
                    CEP = model.CEP,
                    Cidade = model.Cidade,
                    Email = model.Email,
                    Estado = model.Estado,
                    Logradouro = model.Logradouro,
                    Nacionalidade = model.Nacionalidade,
                    Nome = model.Nome,
                    Sobrenome = model.Sobrenome,
                    Telefone = model.Telefone,
                    CPF = model.CPF // Incluindo CPF
                });

                if (clienteId > 0)
                {
                    // Incluir o cliente foi bem-sucedido
                    return Json("Cliente cadastrado com sucesso!");
                }
                else
                {
                    // Erro ao incluir o cliente
                    Response.StatusCode = 500;
                    return Json("Erro ao cadastrar o cliente");
                }
            }
        }


        [HttpPost]
        public JsonResult Alterar(ClienteModel model)
        {
            BoCliente bo = new BoCliente();

            if (!this.ModelState.IsValid)
            {
                List<string> erros = (from item in ModelState.Values
                                      from error in item.Errors
                                      select error.ErrorMessage).ToList();

                Response.StatusCode = 400;
                return Json(string.Join(Environment.NewLine, erros));
            }
            else
            {
                bo.Alterar(new Cliente()
                {
                    Id = model.Id,
                    CEP = model.CEP,
                    Cidade = model.Cidade,
                    Email = model.Email,
                    Estado = model.Estado,
                    Logradouro = model.Logradouro,
                    Nacionalidade = model.Nacionalidade,
                    Nome = model.Nome,
                    Sobrenome = model.Sobrenome,
                    Telefone = model.Telefone,
                    CPF = model.CPF // Incluindo CPF
                });

                return Json("Cadastro alterado com sucesso");
            }
        }

        [HttpGet]
        public ActionResult Alterar(long id)
        {
            BoCliente bo = new BoCliente();
            Cliente cliente = bo.Consultar(id);
            ClienteModel model = null;

            if (cliente != null)
            {
                model = new ClienteModel()
                {
                    Id = cliente.Id,
                    CEP = cliente.CEP,
                    Cidade = cliente.Cidade,
                    Email = cliente.Email,
                    Estado = cliente.Estado,
                    Logradouro = cliente.Logradouro,
                    Nacionalidade = cliente.Nacionalidade,
                    Nome = cliente.Nome,
                    Sobrenome = cliente.Sobrenome,
                    Telefone = cliente.Telefone,
                    CPF = cliente.CPF // Incluindo CPF
                };
            }

            return View(model);
        }

        [HttpPost]
        public JsonResult ClienteList(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                int qtd = 0;
                string campo = string.Empty;
                bool crescente = true; // Definindo a direção padrão como ascendente

                string[] arraySorting = jtSorting?.Split(' ') ?? new string[] { };

                if (arraySorting.Length > 0)
                    campo = arraySorting[0];
                campo = campo.ToLower();

                if (arraySorting.Length > 1)
                    crescente = arraySorting[1]?.ToLower() == "asc"; // Convertendo para minúsculas e verificando se é ascendente

                List<Cliente> clientes = new BoCliente().Pesquisa(jtStartIndex, jtPageSize, campo, crescente, out qtd);

                // Convertendo clientes para ClienteModel para incluir CPF na resposta
                var clientesModel = clientes.Select(c => new ClienteModel
                {
                    Id = c.Id,
                    CEP = c.CEP,
                    Cidade = c.Cidade,
                    Email = c.Email,
                    Estado = c.Estado,
                    Logradouro = c.Logradouro,
                    Nacionalidade = c.Nacionalidade,
                    Nome = c.Nome,
                    Sobrenome = c.Sobrenome,
                    Telefone = c.Telefone,
                    CPF = c.CPF // Incluindo CPF na resposta
                }).ToList();

                // Return result to jTable
                return Json(new { Result = "OK", Records = clientesModel, TotalRecordCount = qtd });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

    }
}
