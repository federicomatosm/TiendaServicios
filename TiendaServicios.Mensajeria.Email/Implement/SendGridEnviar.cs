using System;
using TiendaServicios.Mensajeria.Email.Interface;
using TiendaServicios.Mensajeria.Email.Modelo;

using SendGrid;
using SendGrid.Helpers.Mail;

namespace TiendaServicios.Mensajeria.Email.Implement
{
	public class SendGridEnviar : ISendGridEnviar
	{
		public SendGridEnviar()
		{
		}

        public async Task<(bool resultado, string errorMessage)> EnviarEmail(SendGridData data)
        {
            try
            {
                var sendGridCliente = new SendGridClient(data.SendGridAPIKey);
            var destinatario = new EmailAddress(data.EmailDestinatario, data.NombreDestinatario);
            var sender = new EmailAddress("jerfymatos@gmail.com", "Federico Matos");
            var contenido = data.Contenido;

            var objMensaje = MailHelper.CreateSingleEmail(sender, destinatario, data.Titulo, contenido, contenido);
            await sendGridCliente.SendEmailAsync(objMensaje);

                return (true, null);

           

            }catch(Exception ex)
            {
                return (false, ex.Message);
            }
           
        }
    }
}

