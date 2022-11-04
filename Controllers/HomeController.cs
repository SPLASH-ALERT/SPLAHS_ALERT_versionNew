using Microsoft.AspNetCore.Mvc;
using splash_alert.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;


using Microsoft.AspNetCore.Authorization;
using splash_alert.Models.ViewModels;

namespace splash_alert.Controllers
{
    //[Authorize]
    public class HomeController : Controller
    {
        

        public readonly splash_dataContext dbcontext;

        public HomeController(splash_dataContext _context)
        {
            dbcontext = _context;
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
        public IActionResult registro_encargado()
        {
            List<Usuario> lista = dbcontext.Usuarios.ToList();
          
            return View(lista);
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

        [Authorize]
        [HttpGet]
        public IActionResult registro_empleado()
        {

            NewUsuario nUsuario = new NewUsuario()
            {
                n_usuario = new Usuario()
            };

            return View(nUsuario);
        }
        [Authorize]
        [HttpPost]
        public IActionResult registro_empleado(NewUsuario nUsuario)
        {
           
            if (nUsuario.n_usuario.IdUsuario == 0)
            {
                dbcontext.Usuarios.Add(nUsuario.n_usuario);
            }

            dbcontext.SaveChanges();
            return RedirectToAction("registro_encargado", "Home");

        }


        [HttpGet]
        public IActionResult ninio(string causa, DateTime fecha)
        {
            Models.ViewModels.IngresaCausacs model = new Models.ViewModels.IngresaCausacs();
            model.Causab = causa;
            model.FechaCausab = fecha;
            return View();
        }
        [HttpPost]
        public IActionResult ninio(Models.ViewModels.IngresaCausacs model)
        {
            return View();
        }



        [HttpGet]
        public IActionResult adulto(string causa, DateTime fecha)
        {
            Models.ViewModels.IngresaCausacs model = new Models.ViewModels.IngresaCausacs();
            model.Causab = causa;
            model.FechaCausab = fecha;
            return View();
        }

        [HttpPost]
        public IActionResult adulto(Models.ViewModels.IngresaCausacs model)
        {

            return View();
        }



        [HttpGet]
        public IActionResult animal(string causa, DateTime fecha)
        {
            Models.ViewModels.IngresaCausacs model = new Models.ViewModels.IngresaCausacs();
            model.Causab = causa;
            model.FechaCausab = fecha;
            return View();
        }


        [HttpPost]
        public IActionResult animal(Models.ViewModels.IngresaCausacs model)
        {
            return View();
        }


        [HttpGet]
        public IActionResult objeto(string causa, DateTime fecha)
        {
            Models.ViewModels.IngresaCausacs model = new Models.ViewModels.IngresaCausacs();
            model.Causab = causa;
            model.FechaCausab = fecha;

            return View();
        }

        [HttpPost]
        public IActionResult objeto(Models.Causa model)
        {

           dbcontext.Causas.Add(model);

            

            return View();
        }

    }
}

/*user.Contrasena = model.Password;
                    dbcontext.Entry(user).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    dbcontext.SaveChanges();
                    */