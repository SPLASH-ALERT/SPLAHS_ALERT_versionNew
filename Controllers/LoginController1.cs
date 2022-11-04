using Microsoft.AspNetCore.Mvc;
using splash_alert.Models;
using splash_alert.Models.ViewModels;
using System.Data.Entity;


using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Net.Mail;

namespace splash_alert.Controllers
{
    public class LoginController1 : Controller
    {
        string urlDom = "https://localhost:7061/";
        public readonly splash_dataContext dbcontext;

        public LoginController1(splash_dataContext _context)
        {
            dbcontext = _context;
        }



        [HttpPost] // obtiene información del servidor
        public async Task<IActionResult> Login(string correo, string contrasena)
        {

            var user = dbcontext.Usuarios.Where(x => x.Correo == correo && x.Contrasena == contrasena).FirstOrDefault(); // se compara el elemento ingresado con el elemento en la base de datos



            if (user != null)
            {

                string rol = user.IdRol.ToString();
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.NombreUsuario),
                    new Claim("Correo", user.Correo)


                };
                claims.Add(new Claim(ClaimTypes.Role, rol));

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));



                return RedirectToAction("Index", "Home");
            }
            else {


                return RedirectToAction("Index", "LoginController1"); ;
            }


        }

        public async Task<IActionResult> cerrar()
        {

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "LoginController1");
        }
        [HttpGet]
        public IActionResult StartRecovery()
        {
            Models.ViewModels.VRecovery model = new Models.ViewModels.VRecovery();
            return View(model);
        }
        [HttpGet]
        public IActionResult Recovery(string token)
        {
            Models.ViewModels.VRecoverypass model = new Models.ViewModels.VRecoverypass();
            model.token = token;

            return View();
        }
        [HttpPost]
        public IActionResult Recovery(Models.ViewModels.VRecoverypass model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model); 
                }
                

                var user =dbcontext.Usuarios.Where(d =>d.Tokken ==model.token).FirstOrDefault();
                if (user != null)
                {
                    user.Contrasena = model.Password;
                    dbcontext.Entry(user).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    dbcontext.SaveChanges();
                    
                }   



                }  catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return RedirectToAction("Index", "LoginController1");
        }

        [HttpPost]
        public IActionResult StartRecovery(Models.ViewModels.VRecovery model, DateTime fecha)
        {
            if (ModelState.IsValid)
            {
                string token = GetSha256(Guid.NewGuid().ToString());


                var user = dbcontext.Usuarios.Where(x => x.Correo == model.Email).FirstOrDefault();
                if (user != null)
                {
                    user.Tokken = token;
                    user.FechaCreacion = fecha;
                    dbcontext.Entry(user).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    dbcontext.SaveChanges();
                    
                }

                SendEmail(user.Correo, token);

            }

           




            return View();
        }

        #region  HELPERS
        private string GetSha256(string str) {

            SHA256 sha256 = SHA256Managed.Create();
            ASCIIEncoding enconding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = sha256.ComputeHash(enconding.GetBytes(str));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString();
        }
        #endregion


        #region  HELPERS
        private void SendEmail(string EmailDestino, string token)
        {
         
           
                string EmailOrigen = "22999oscar@gmail.com";
                 string Contrasena = "ymtzerqvkahszdrv";
                    string url=urlDom + "LoginController1/Recovery/?token="+token;
            MailMessage oMail = new MailMessage(EmailOrigen, EmailDestino, "recuperación de contraseña",
                "<p> correo para recuperación de contraseña</P><br>"+
                "<a href ='" + url +"'</a>" + " >Click para recuperar");

            oMail.IsBodyHtml = true;
            SmtpClient oSmtp = new SmtpClient("smtp.gmail.com");
            oSmtp.EnableSsl = true;
            oSmtp.UseDefaultCredentials = false;
            oSmtp.Port = 587;
            oSmtp.Credentials = new System.Net.NetworkCredential(EmailOrigen, Contrasena);
            oSmtp.Send(oMail);
            oSmtp.Dispose();



        }
        #endregion

        public IActionResult Index()
        {
            return View();
        }



    
    }

}
