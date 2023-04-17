using System.Net;
using System.Net.Mail;

namespace AgendaContatos.Messages
{
    public class EmailMessage
    {
        #region Atributos
        private string _conta = "cotinaoresponda@outlook.com";
        private string _senha = "@Admin123456";
        private string _smtp = "smtp-mail.outlook.com";
        private int _porta = 587;
        #endregion

        //metodos para realizar o envio
        public void SendMail(string emailTo, string subject, string body)
        {
            //criar email
            var mailMessage = new MailMessage(_conta, emailTo);
            mailMessage.Subject = subject;
            mailMessage.Body = body;
            mailMessage.IsBodyHtml = true;

            //enviar email
            var smtpClient = new SmtpClient(_smtp, _porta);
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = new NetworkCredential(_conta, _senha);
            smtpClient.Send(mailMessage);
        }
    }
}