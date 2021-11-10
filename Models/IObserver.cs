using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Candidate_Recruiter.Models
{
   public interface IObserver
    {
        public string Cedula { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public double AspiracionSalarialMinima { get; set; }
        bool EnviarCorreo(Puesto puesto);
    }
}
