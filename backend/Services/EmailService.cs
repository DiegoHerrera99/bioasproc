using System;
using bioinsumos_asproc_backend.Models;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;

namespace bioinsumos_asproc_backend.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public bool SendEmail(SendEmailDto request)
        {
            try 
            {
                // Create a new MimeMessage for composing the email
                var email = new MimeMessage();

                // Set the sender's email address
                email.From.Add(MailboxAddress.Parse(_config.GetSection("EmailUserName").Value));

                // Set the recipient's email address
                email.To.Add(MailboxAddress.Parse(_config.GetSection("EmailRecipient").Value));

                // Set the email subject
                TimeZoneInfo zonaGuatemala = TimeZoneInfo.CreateCustomTimeZone("Guatemala Standard Time", new TimeSpan(-6, 0, 0), "Guatemala Standard Time", "Guatemala Standard Time");
                DateTime horaGuatemala = TimeZoneInfo.ConvertTime(DateTime.Now, zonaGuatemala);
                string fechaGuatemala = horaGuatemala.ToString("dd/MM/yyyy hh:mm tt");
                email.Subject = $@"APP BioAsproc - 🆕 🆕 🆕 Nueva consulta 🤔🙋 - {fechaGuatemala}";

                // Set the email body as HTML
                string body = $@"        
                    <html>
                        <body>
                            <h1>¡Nueva Consulta!</h1>
                            <br>
                            <p><strong>Pregunta:</strong></p>
                            <p>{request.Question}</p>
                            <br>
                            <hr>
                            <h2>Información de contacto:</h2>
                            <p><strong>Nombre:</strong> {request.Name}</p>
                            <p><strong>Teléfono:</strong> {request.Phone}</p>
                            <p><strong>Correo electrónico:</strong> {request.Email}</p>
                            <br>
                            <p>Saludos,</p>
                            <p><em>El equipo de BioAsproc</em></p>
                        </body>
                    </html>
                ";

                email.Body = new TextPart(TextFormat.Html) { Text = body };

                // Create an instance of SmtpClient for sending the email
                using var smtp = new SmtpClient();

                // Connect to the SMTP server with the specified host and port using StartTLS for security
                smtp.Connect(_config.GetSection("EmailHost").Value, 587, SecureSocketOptions.StartTls);

                // Authenticate with the SMTP server using the provided username and password
                smtp.Authenticate(_config.GetSection("EmailUserName").Value, _config.GetSection("EmailPassword").Value);

                // Send the composed email
                smtp.Send(email);

                // Disconnect from the SMTP server after sending the email
                smtp.Disconnect(true);

                // Return a success message
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}