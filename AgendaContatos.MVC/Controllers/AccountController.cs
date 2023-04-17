using AgendaContatos.Data.Entities;
using AgendaContatos.Data.Repositories;
using AgendaContatos.MVC.Models;
using AgendaContatos.Messages;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Bogus;

namespace AgendaContatos.Controllers
{
    public class AccountController : Controller
    {
        //rota /Account/login
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost] //comando utilizado para receber dados da pagina
        public IActionResult Login(AccountLoginModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    UsuarioRepository usuarioRepository = new UsuarioRepository();
                    Usuario consulta = new Usuario();
                    consulta = usuarioRepository.GetByEmail(model.Email);
                    if (consulta != null)
                    {
                        Usuario usuario = new Usuario();
                        usuario = usuarioRepository.GetByLogin(model.Email, model.Senha);
                        if (usuario == null)
                        {
                            TempData["Mensagem"] = $"Senha ou e-mail invalido";
                        }
                        else
                        {
                            var authenticationModel = new AuthenticationModel(usuario);
                            var json = JsonConvert.SerializeObject(authenticationModel);
                            SaveCookei(json);
                            return RedirectToAction("Home", "Contact");
                        }
                    }
                    else
                    {
                        TempData["Mensagem"] = $"E-mail não cadastrado.";
                    }
                }
                catch (Exception e)
                {
                    TempData["Mensagem"] = $"Falha ao acessar a conta do usuario {model.Senha} {e.Message}";
                }
            }
            
            return View();
        }

        //rota /Account/register
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost] //comando utilizado para receber dados da pagina
        public IActionResult Register(AccountRegisterModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    UsuarioRepository usuarioRepository = new UsuarioRepository();
                    Usuario consulta = new Usuario();
                    consulta = usuarioRepository.GetByEmail(model.Email);
                    if (consulta == null)
                    {
                        Usuario usuario = new Usuario();
                        usuario.Nome = model.Nome;
                        usuario.Email = model.Email;
                        usuario.DataCadastro = DateTime.Now;

                        if (model.Senha1.Equals(model.Senha2) == true)
                        {
                            usuario.Senha = model.Senha1;
                            usuarioRepository.Create(usuario);
                            TempData["Mensagem"] = $"Parabéns {usuario.Nome}, sua conta foi cadastrada com sucesso!";
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
            return Login();
        }

        //rota /Account/passwordRecover
        public IActionResult PasswordRecover()
        {
            return View();
        }

        [HttpPost] //comando utilizado para receber dados da pagina
        public IActionResult PasswordRecover(AccountPasswordRecoverModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    UsuarioRepository usuarioRepository = new UsuarioRepository();
                    Usuario consulta = new Usuario();
                    consulta = usuarioRepository.GetByEmail(model.Email);
                    if (consulta != null)
                    {
                        if (!PasswordRecoverUser(consulta))
                            TempData["Mensagem"] = $"Falha ao enviar o e-mail. {model.Email}";
                        else
                            TempData["Mensagem"] = $"Ola {consulta.Nome}, você receberá um e-mail com a sua nova senha de acesso.";
                    }
                    else
                    {
                        TempData["Mensagem"] = $"E-mail não cadastrado.";
                    }
                }
                catch (Exception e)
                {
                    TempData["Mensagem"] = $"Falha ao verificar o e-mail. {model.Email} {e.Message}";
                }
            }

            return View();
        }
        private void SaveCookei(string json)
        {
            //criar criptografia
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, json) }, CookieAuthenticationDefaults.AuthenticationScheme);

            //gravar json
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);
        }

        //recuperar senha
        private static Boolean PasswordRecoverUser(Usuario usuario)
        {
            try
            {
                //gerar uma nova senha
                Faker fake = new Faker();
                var novaSenha = fake.Internet.Password(10);

                //enviar senha por email
                var mailTo = usuario.Email;
                var subject = "Recuperação de senha de acesso - Ageda de Contatos";
                var body = $@"
                            <div>
                                <p>Olá {usuario.Nome}, uma nova senha foi gerada com sucesso.</p>
                                <p>Utilize a senha <strong>{novaSenha}</strong> para acessar sua conta.</p>
                                <p>Atenciosamente</p>
                                <p>Quipe Agenda de Contatos</p>
                            </div>
                        ";

                EmailMessage emailMessage = new EmailMessage();
                emailMessage.SendMail(mailTo, subject, body);
                UsuarioRepository usuarioRepository = new UsuarioRepository();
                usuario.Senha = novaSenha;
                usuarioRepository.Update(usuario);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

            return false;
        }
    }
}
