using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Candidate_Recruiter.Models
{
    public class CandidatosCrud
    {
        // Clase creada para el manejo de la lista de los candidatos registrados en el programa.

        private static readonly List<IObserver> candidatos = new List<IObserver>();

        public static List<IObserver> Candidatos { get { return candidatos; } }

        public static void AgregarCandidato(IObserver candidato)
        {
            candidatos.Add(candidato);
        }
    }
}
