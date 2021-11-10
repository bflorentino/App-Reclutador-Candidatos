using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Candidate_Recruiter.Models
{
    public class Reclutadora : IObservable
    {
        // IMPLEMENTACIÓN DE PATRON DE DISEÑO SINGLETON
        private static Reclutadora reclutadora = null;
        
        private List<Tuple<IObserver, Puesto>> candidatos_Puestos = new List<Tuple<IObserver, Puesto>>();

        private Reclutadora(){}

        public static Reclutadora GetReclutadora()
        {
            if(reclutadora == null)
            {
                reclutadora = new Reclutadora();
            }
            return reclutadora;
        }

        public void VerificarStatusDePuesto(Puesto puesto)
        {
            // En caso de que el puesto se haya actuailzado a vacante. Si el puesto esta vacante, este metodo
            // llama al metodo notificar para notificar a los suscritores del cambio de puesto a vacante
            if(puesto.Status == "Vacante")
            {
                this.Notificar(puesto);
            }
        }

        public void Suscribir(IObserver candidato, Puesto puesto)
        {
            // Metodo que suscribe a un puesto a un candidato en especifico

            candidatos_Puestos.Add(Tuple.Create(candidato, puesto));
        }

        public void Desuscribir(IObserver candidato, Puesto puesto)
        {
            // Metodoq que desuscribe un candidato de un puesto en especifico

            foreach (var tupla in candidatos_Puestos)
            {
                if(tupla.Item1.Cedula == candidato.Cedula && tupla.Item2.Codigo == puesto.Codigo)
                {
                    candidatos_Puestos.Remove(tupla);
                    break;
                }
            }
        }

        public void Notificar(Puesto puesto)
        {
            // Metodo que notifica los cambios a los observadores interesados en algun puesto especifico.

            foreach(var tupla in candidatos_Puestos)
            {
                if(tupla.Item2 == puesto)
                {
                    tupla.Item1.EnviarCorreo(puesto);
                }
            }
        }

        public List<Puesto> GetPuestosSuscritos(string cedula)
        {
            // Este metodo fue creado por conveniencia, con las intenciones de poder obtener los puestos
            // a los que esta suscrito un candidato para mostrarlos en una vista.

            List<Puesto> puestosSuscritos = new List<Puesto>();
                
            foreach(var candidato in candidatos_Puestos)
            {
                if(candidato.Item1.Cedula == cedula)
                {
                    puestosSuscritos.Add(candidato.Item2);
                }
            }

            return puestosSuscritos;
        }

        public List<Puesto> GetPuestosNoSuscritos(string cedula)
        {
            // Este metodo fue creado por conveniencia, con las intenciones de poder obtener los puestos
            // a los que no esta suscrito un candidato para mostrar estos puestos en una vista.
            if(candidatos_Puestos.Count > 0)
            {
                 List<Puesto> puestosNoSuscritos = new List<Puesto>();

                foreach(var puesto in PuestosCrud.Puestos)
                {
                    bool puestoEncontrado = false;

                    foreach (var candidato in candidatos_Puestos)
                    {
                        if (candidato.Item1.Cedula == cedula && candidato.Item2.Codigo == puesto.Codigo)
                        {
                            puestoEncontrado = true;
                            break;
                        }
                    }
                    if (!puestoEncontrado)
                    {
                        puestosNoSuscritos.Add(puesto);
                    }
                }
                return puestosNoSuscritos;
            }
            else
            {
                return PuestosCrud.Puestos;
            }
        }
    }
}