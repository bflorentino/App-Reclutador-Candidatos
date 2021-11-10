using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Candidate_Recruiter.Models
{
    public interface IObservable
    {
        void Suscribir(IObserver candidato, Puesto puesto);
        void Desuscribir(IObserver candidato, Puesto puesto);
        void Notificar(Puesto puesto);
    }
}
