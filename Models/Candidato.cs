using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;

namespace Candidate_Recruiter.Models
{
    public class Candidato: IObserver
    {
        public string Cedula { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public double AspiracionSalarialMinima { get; set;}

        public bool EnviarCorreo(Puesto puestoInteresado)
        {
            // Metodo para enviar correo al usuario sobre el puesto laboral interesado
            // siempre y cuando el salario del mismo sea el deseado por el usuario

            if (puestoInteresado.Salario >= this.AspiracionSalarialMinima)
            {
                var credentials = new NetworkCredential("reclutadorbryan@gmail.com", "recluta@12345");

                var mail = new MailMessage("202010674@itla.edu.do",
                                            this.Correo,
                                            "Puesto de trabajo vacante",
                                             $"El puesto de trabajo {puestoInteresado.Nombre} al que esta suscrito, está vacante");

                mail.IsBodyHtml = true;

                SmtpClient smtp = new SmtpClient("smtp.gmail.com");
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Port = 587;
                smtp.Credentials = credentials;
                smtp.Send(mail);

                smtp.Dispose();
            }
            return true;
        }
    }
}