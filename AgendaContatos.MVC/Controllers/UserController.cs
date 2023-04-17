using Microsoft.AspNetCore.Mvc;
using AgendaContatos.MVC.Models;
using Microsoft.AspNetCore.Authorization;
using AgendaContatos.Data.Repositories;
using AgendaContatos.Data.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Newtonsoft.Json;
using System.Security.Claims;

namespace AgendaContatos.MVC.Controllers
{
    public class UserController : Controller
    {
        [Authorize]
        public IActionResult Edition()
        {
            try
            {
                var json = User.Identity.Name;
                var authenticationModel = JsonConvert.DeserializeObject<AuthenticationModel>(json);
                UsuarioRepository usuarioRepository = new UsuarioRepository();
                Usuario usuario = new Usuario();
                usuario = usuarioRepository.GetById(authenticationModel.IdUsuario);
                ContactUserModel contactUserModel = new ContactUserModel();
                contactUserModel.IdUsuario = usuario.IdUsuario;
                contactUserModel.Nome = usuario.Nome;
                contactUserModel.Email = usuario.Email;
                contactUserModel.Senha1 = null;
                contactUserModel.Senha2 = null;
                return View(contactUserModel);
            }
            catch (Exception)
            {
                TempData["Mensagem"] = $"Erro ao recuperar os dados do usuário.";
            }

            return View();
        }

        [HttpPost] //comando utilizado para receber dados da pagina
        public IActionResult Edition(ContactUserModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    UsuarioRepository usuarioRepository = new UsuarioRepository();
                    Usuario consulta = new Usuario();
                    consulta = usuarioRepository.GetByEmail(model.Email);
                    if ((consulta == null) || (GetIdUsuarioCookei() == consulta.IdUsuario))
                    {
                        if (model.Senha1.Equals(model.Senha2) == true)
                        {
                            Usuario usuario = new Usuario();
                            usuario = usuarioRepository.GetById(model.IdUsuario);
                            usuario.Nome = model.Nome;
                            usuario.Email = model.Email;
                            usuario.Senha = model.Senha1;
                            usuarioRepository.Update(usuario);
                            TempData["Mensagem"] = $"Dados atualizados com sucesso!";

                            //apaga o cookei
                            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

                            //escreve o cookei
                            var authenticationModel = new AuthenticationModel(usuario);
                            var json = JsonConvert.SerializeObject(authenticationModel);

                            //criar criptografia
                            ClaimsIdentity claimsIdentity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, json) }, CookieAuthenticationDefaults.AuthenticationScheme);

                            //gravar json
                            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);
                        }
                        else
                        {
                            TempData["Mensagem"] = $"Os campos senhas são diferentes!";
                        }
                    }
                    else
                    {
                        TempData["Mensagem"] = $"Email {model.Email} ja cadastrado!";
                    }
                }
                catch (Exception e)
                {
                    TempData["Mensagem"] = $"Falha no cadastro {e.Message}.";
                }
            }

            return View();
        }

        private int GetIdUsuarioCookei()
        {
            var json = User.Identity.Name;
            var authenticationModel = JsonConvert.DeserializeObject<AuthenticationModel>(json);
            return authenticationModel.IdUsuario;
        }
    }
}
