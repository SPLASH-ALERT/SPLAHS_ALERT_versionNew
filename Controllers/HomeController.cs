using Microsoft.AspNetCore.Mvc;
using splash_alert.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using splash_alert.Servicio;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using splash_alert.Models.ViewModels;
using splash_alert.Models.Servicios;

namespace splash_alert.Controllers
{
    //[Authorize]
    public class HomeController : Controller
    {


        public readonly splash_dataContext dbcontext;
        public readonly IServicioApi _servicio;


        public HomeController(splash_dataContext _context, IServicioApi servicioApi)
        {
            dbcontext = _context;
            _servicio = servicioApi;
        }

        public IActionResult resumenCausa()
        {
            DateTime FechaInicial = DateTime.Now;
            FechaInicial = FechaInicial.AddDays(-15);

            List<VCausa> Lista = (from tbCausa in dbcontext.Causas
                                  where tbCausa.FechaCausa.Value.Date >= FechaInicial.Date
                                  group tbCausa by tbCausa.FechaCausa.Value.Date into grupo
                                  select new VCausa
                                  {
                                      fecha = grupo.Key.ToString("dd/MM/yyyy"),
                                      cantidad = grupo.Count(),

                                  }).ToList();

            return StatusCode(StatusCodes.Status200OK, Lista);
        }
        [Authorize]
        public IActionResult resumenCausa2()
        {


            List<VCausaCantidad> Lista = (from tbCausaCnt in dbcontext.Causas

                                          group tbCausaCnt by tbCausaCnt.Causa1 into grupo
                                          orderby grupo.Count() descending
                                          select new VCausaCantidad
                                          {
                                              causa = grupo.Key,
                                              cantidad = grupo.Count(),

                                          }).Take(10).ToList();

            return StatusCode(StatusCodes.Status200OK, Lista);
        }


        [Authorize]
        public IActionResult Index()
        {

            return View();
        }
        [Authorize]
        public IActionResult causas()
        {

            return View();
        }

        [Authorize]
        public async Task<IActionResult> registro_encargado1()
        {
            //List<UsuarioS> lista =  await _servicio.ListaAsistente();

            return View();
        }

        [Authorize]
        public async Task<IActionResult> registro_encargado(UsuarioS objeto)
        {
            bool respuesta;

            respuesta = await _servicio.ListaAsistente(objeto);

            if(respuesta)
            return View(); 
            else
                return NoContent();


        }

        [Authorize]
        public IActionResult reportes()
        {
            return View();
        }
        [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult registro_empleado1(UsuarioS objeto)
        {

            return View();

        }

        public IActionResult registro_admin()
        {

            return View();

        }


        [Authorize]
        [HttpPost]
        public async Task<IActionResult> registro_empleado(UsuarioS objeto)
        {
            bool respuesta;


            respuesta = await _servicio.GuardarAsistente(objeto);


            if (respuesta)


                return RedirectToAction("registro_encargado", "Home");
            else
                return NoContent();

        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> registro_administrador(UsuarioS objeto)
        {
            bool respuesta;


            respuesta = await _servicio.GuardarAdmin(objeto);


            if (respuesta)


                return RedirectToAction("registro_encargado", "Home");
            else
                return NoContent();



        }

        // LLAMADO DE LA CONFIGURACIÓN DE APIS PARA EJECUTAR LOS CONTROLADORES PARA INGRESAR LAS CAUSAS

        [HttpPost]
        public async Task<IActionResult> ninio(CausaS objeto)
        {

            bool respuesta;


            respuesta = await _servicio.GuardarCausaNino(objeto);


            if (respuesta)


                return RedirectToAction("confirmacion", "Home");
            else
                return NoContent();
        }


        [HttpPost]
        public async Task<IActionResult> adulto(CausaS objeto)
        {

            bool respuesta;


            respuesta = await _servicio.GuardarCausaAdulto(objeto);


            if (respuesta)


                return RedirectToAction("confirmacion", "Home");
            else
                return NoContent();
        }



        [HttpPost]
        public async Task<IActionResult> animal(CausaS objeto)
        {

            bool respuesta;


            respuesta = await _servicio.GuardarCausaAnimal(objeto);


            if (respuesta)


                return RedirectToAction("confirmacion", "Home");
            else
                return NoContent();

        }


        [HttpPost]
        public async Task<IActionResult> objeto(CausaS objeto)
        {
            bool respuesta;


            respuesta = await _servicio.GuardarCausaObjeto(objeto);


            if (respuesta)


                return RedirectToAction("confirmacion", "Home");
            else
                return NoContent();

        }


        public IActionResult confirmacion()
        {
            return View();
        }

    }
}

