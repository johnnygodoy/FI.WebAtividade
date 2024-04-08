using FI.AtividadeEntrevista.BLL;
using FI.AtividadeEntrevista.DML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAtividadeEntrevista.Models;

namespace WebAtividadeEntrevista.Controllers
{
    public class BeneficiarioController : Controller
    {
        // GET: Beneficiario
        public ActionResult Index()
        {
            return View();
        }

        // GET: Beneficiario/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Beneficiario/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Beneficiario/Create
        [HttpPost]
        public JsonResult Incluir(BeneficiarioModel model)
        {
            BoBeneficiario bo = new BoBeneficiario();

            if (!ModelState.IsValid)
            {
                List<string> erros = ModelState.Values.SelectMany(item => item.Errors.Select(error => error.ErrorMessage)).ToList();
                Response.StatusCode = 400;
                return Json(string.Join(Environment.NewLine, erros));
            }
            else
            {
                model.Id = bo.Incluir(new Beneficiario
                {
                    Nome = model.Nome,
                    CPF = model.CPF
                });

                return Json("Cadastro efetuado com sucesso");
            }
        }

        // GET: Beneficiario/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Beneficiario/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Beneficiario/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Beneficiario/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Beneficiario/FormsBeneficiario
        public ActionResult FormsBeneficiario()
        {
            return View();
        }
    }
}
