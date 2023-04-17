using Microsoft.AspNetCore.Mvc;
using AgendaContatos.MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using AgendaContatos.Data.Entities;
using AgendaContatos.Data.Repositories;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace AgendaContatos.MVC.Controllers
{
    [Authorize]
    public class ContactController : Controller
    {
        //rota /Contact/Register
        public IActionResult Home()
        {
            return View();
        }

        //rota /Contact/Register
        public IActionResult Register()
        {
            ContactRegisterModel model = new ContactRegisterModel();
            model.IdContato = 0;
            return View(model);
        }

        [HttpPost] //comando utilizado para receber dados da pagina
        public IActionResult Register(ContactRegisterModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Contato contato = new Contato();
                    contato.IdUsuario = GetIdUsuarioCookei();
                    contato.Nome = model.Nome;
                    contato.Email = model.Email;
                    contato.Telefone1 = model.Telefone1;
                    contato.Telefone2 = model.Telefone2;
                    contato.Observacao = model.Observacao;
                    contato.DataCadastro = DateTime.Now;
                    ContatoRepository contatoRepository = new ContatoRepository();
                    contatoRepository.Create(contato);
                    TempData["Mensagem"] = $"Contato salvo com sucesso em sua lista de contatos!";
                    ModelState.Clear();
                }
                catch (Exception)
                {
                    TempData["Mensagem"] = $"Falha ao atualizar os dados do contato!";
                }
            }

            return View();
        }

        //rota /Contact/Search
        public IActionResult Search()
        {
            try
            {
                ContatoRepository contatoRepository = new ContatoRepository();

                //declarando e inicializando uma lista
                var lista = new List<ContactSearchModel>();

                //acessando a camada business para consultar os clientes
                foreach (Contato c in contatoRepository.GetByUsuario(GetIdUsuarioCookei()))
                {
                    var model = new ContactSearchModel();
                    model.IdContato = c.IdContato;
                    model.Nome = c.Nome;
                    model.Telefone1 = c.Telefone1;
                    model.Telefone2 = c.Telefone2;
                    model.Email = c.Email;

                    lista.Add(model); //adicionar na lista
                }

                return View(lista);
            }
            catch (Exception)
            {
                throw;
            }

            return View();
        }

        //rota /Contact/Edition
        public IActionResult Edition(int id)
        {
            try
            {
                ContactRegisterModel model = new ContactRegisterModel();
                Contato contato = new Contato();
                ContatoRepository contatoRepository = new ContatoRepository();
                contato = contatoRepository.GetByIdContato(id, GetIdUsuarioCookei());
                model.IdContato = contato.IdContato;
                model.Nome = contato.Nome;
                model.Email = contato.Email;
                model.Telefone1 = contato.Telefone1;
                model.Telefone2 = contato.Telefone2;
                model.Observacao = contato.Observacao;
                model.DataCadastro = contato.DataCadastro;
                return View(model);
            }
            catch (Exception)
            {
                ViewBag.Mensagem = "Fala ao selecionar os dados do contato ";
            }

            return View();
        }

        [HttpPost] //comando utilizado para receber dados da pagina
        public IActionResult Edition(ContactRegisterModel model)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    Contato consulta = new Contato();
                    ContatoRepository contatoRepository = new ContatoRepository();
                    consulta = contatoRepository.GetByIdContato((int)model.IdContato, GetIdUsuarioCookei());
                    Contato contato = new Contato();
                    contato.IdContato = (int)model.IdContato;
                    contato.IdUsuario = GetIdUsuarioCookei();
                    contato.Nome = model.Nome;
                    contato.Email = model.Email;
                    contato.Telefone1 = model.Telefone1;
                    contato.Telefone2 = model.Telefone2;
                    contato.Observacao = model.Observacao;
                    contato.DataCadastro = consulta.DataCadastro;
                    contatoRepository.Update(contato);
                    TempData["Mensagem"] = $"Contato alterado com sucesso em sua lista de contatos!";
                }
                catch (Exception)
                {
                    TempData["Mensagem"] = $"Falha ao atualizar os dados do contato!";
                }
            }

            return View();
        }

        public IActionResult Delete(int id)
        {
            try
            {
                ContactRegisterModel model = new ContactRegisterModel();
                Contato contato = new Contato();
                ContatoRepository contatoRepository = new ContatoRepository();
                contato = contatoRepository.GetByIdContato(id, GetIdUsuarioCookei());

                model.IdContato = contato.IdContato;
                model.Nome = contato.Nome;
                model.Email = contato.Email;
                model.Telefone1 = contato.Telefone1;
                model.Telefone2 = contato.Telefone2;
                model.Observacao = contato.Observacao;
                return View(model);
            }
            catch (Exception)
            {
                ViewBag.Mensagem = "Fala ao selecionar os dados do contato ";
            }

            return View();
        }

        [HttpPost] //comando utilizado para receber dados da pagina
        public IActionResult Delete(ContactRegisterModel model)
        {
            try
            {
                Contato contato = new Contato();
                contato.IdContato = (int)model.IdContato;
                contato.IdUsuario = GetIdUsuarioCookei();
                ContatoRepository contatoRepository = new ContatoRepository();
                contatoRepository.Delete(contato);
                TempData["Mensagem"] = $"Contado excluido com sucesso!";
                return RedirectToAction("Search", "Contact");
            }
            catch (Exception)
            {
                TempData["Mensagem"] = $"Falha ao remover os dados do contato!";
            }

            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }

        private int GetIdUsuarioCookei()
        {
            var json = User.Identity.Name;
            var authenticationModel = JsonConvert.DeserializeObject<AuthenticationModel>(json);
            return authenticationModel.IdUsuario;
        }
    }
}
