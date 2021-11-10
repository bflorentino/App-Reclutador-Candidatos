using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Candidate_Recruiter.Models;

namespace Candidate_Recruiter.Controllers
{
    public class ReclutadoraController : Controller
    {
        private Reclutadora reclutadora = Reclutadora.GetReclutadora();
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CandidatosForm()
        {
            return View();
        }

        public IActionResult ProcessCandidatosForm()
        {
            CandidatosCrud.AgregarCandidato(new Candidato()
            {
                Cedula = Request.Form["cedula"],
                Nombre = Request.Form["nombre"],
                Correo = Request.Form["correo"],
                AspiracionSalarialMinima = Double.Parse(Request.Form["salario"])
            });

            return RedirectToAction("MessageAfterProcessing");
        }

        public IActionResult MessageAfterProcessing()
        {
            return View();
        }

        public IActionResult PuestosForm()
        {
            return View();
        }

        public IActionResult ProcessPuestosForm()
        {
            PuestosCrud.AgregarPuesto(new Puesto()
            {
                Codigo = Request.Form["codigo"],
                Nombre = Request.Form["puesto"],
                Salario = Double.Parse(Request.Form["salario"]),
                Status = Request.Form["status"]
            });

            return RedirectToAction("MessageAfterProcessing");
        }

        public IActionResult ViewCandidatos()
        {
            var candidatos = from candidato in CandidatosCrud.Candidatos
                             select candidato;

            ViewBag.mensaje = CandidatosCrud.Candidatos.Count > 0 ? "" : "No hay ningun candidato registrado";

            return View(candidatos);
        }

        public IActionResult ViewPuestos()
        {
            var puestos = from puesto in PuestosCrud.Puestos
                          select puesto;

            ViewBag.mensaje = PuestosCrud.Puestos.Count > 0 ? "" : "No hay ningun puesto registrado";

            return View(puestos);
        }

        public IActionResult EditPuesto(int posicion)
        {
            Puesto puesto = PuestosCrud.Puestos[posicion];
            ViewBag.posicion = posicion;
            return View(puesto);
        }

        public IActionResult ProcessEditPuesto()
        {
            int posicion = int.Parse(Request.Form["posicion"]);

            PuestosCrud.EditarPuesto(posicion, Double.Parse(Request.Form["salario"]), Request.Form["Status"]);

            // Desencadenamiento del patron observer
            reclutadora.VerificarStatusDePuesto(PuestosCrud.Puestos[posicion]);

            return RedirectToAction("ViewPuestos");
        }

        public IActionResult SuscripcionPuesto(int posicion)
        {
            var puestos = from puesto in reclutadora.GetPuestosNoSuscritos(CandidatosCrud.Candidatos[posicion].Cedula)
                          select puesto;

            ViewBag.posicion = posicion;
            ViewBag.mensaje = reclutadora.GetPuestosNoSuscritos(CandidatosCrud.Candidatos[posicion].Cedula).Count > 0 ? "" : "Al parecer el candidato esta suscrito a todos los puestos";

            return View(puestos);
        }

        public IActionResult ProcessSuscripcion(string codigo, int posicion)
        {
            Puesto puesto = PuestosCrud.BuscarPuestoPorCodigo(codigo);

            // Si el candidato se suscribe a un determinado puesto y el mismo esta vacante al momento de suscribirse
            // tambien se le mandara el correo
            reclutadora.Suscribir(CandidatosCrud.Candidatos[posicion], puesto);
            reclutadora.VerificarStatusDePuesto(puesto);
            
            return RedirectToAction("ViewCandidatos");
        }

        public IActionResult DesuscripcionPuesto(int posicion)
        {
            var puestos = from puesto in reclutadora.GetPuestosSuscritos(CandidatosCrud.Candidatos[posicion].Cedula)
                          select puesto;

            ViewBag.posicion = posicion;
            ViewBag.mensaje = reclutadora.GetPuestosSuscritos(CandidatosCrud.Candidatos[posicion].Cedula).Count > 0 ? "" : "Este candidato no esta suscrito a ningun puesto";

            return View(puestos);
        }

        public IActionResult ProcessDesuscripcion(string codigo, int posicion)
        {
            Puesto puesto = PuestosCrud.BuscarPuestoPorCodigo(codigo);
            reclutadora.Desuscribir(CandidatosCrud.Candidatos[posicion], puesto);
            return RedirectToAction("ViewCandidatos");
        }
    }
}