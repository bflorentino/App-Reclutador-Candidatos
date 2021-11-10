using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Candidate_Recruiter.Models
{
    public class PuestosCrud
    {
        // Esta clase se crea con las intenciones de manejar las listas de puestos para hacer operaciones como
        // agregar nuevos puestos al programa, hacer busquedas de puestos por codigo y editar puestos existentes.

        // Propiedad estatica para tener acceso a la lista en diversas partes del programa
        private static readonly List<Puesto> puestos = new List<Puesto>();

        public static List<Puesto> Puestos { get { return puestos; } }

        public static void AgregarPuesto(Puesto puesto)
        {
            puestos.Add(puesto);
        }

        public static Puesto BuscarPuestoPorCodigo(string codigo)
        {
            // Retorna un puesto en particular segun el codigo recibido

            foreach(var puesto in puestos)
            {
                if(puesto.Codigo == codigo)
                {
                    return puesto;
                }
            }
            return null;
        }

        public static void EditarPuesto(int posicion, double salario, string status)
        {
            // Este metodo actualiza un determinado puesto con los valores nuevos recibidos.

            puestos[posicion].Salario = salario;
            puestos[posicion].Status = status;
        }
    }
}
