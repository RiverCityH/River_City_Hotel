using asp_hoteles.Nucleo;
using lib_utilidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace asp_hoteles.Views.Shared
{
    public class IndexModel : PageModel
    {
        public bool IsLogged = false;
        [BindProperty] public string? Email { get; set; }
        [BindProperty] public string? Contraseña { get; set; }

        public void OnGet()
        {
            try
            {
                var user = HttpContext.Session.GetObject<string>("Usuario");
                if (user != null)
                {
                    IsLogged = true;
                    ViewData["Logged"] = true;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log(ex);
            }
        }

        public void OnPostBtClean()
        {
            try
            {
                Email = string.Empty;
                Contraseña = string.Empty;
            }
            catch (Exception ex)
            {
                LogHelper.Log(ex);
            }
        }

        public void OnPostBtEnter()
        {
            try
            {
                if (string.IsNullOrEmpty(Email) &&
                    string.IsNullOrEmpty(Contraseña))
                {
                    OnPostBtClean();
                    return;
                }

                if (DatosGenerales.usuario_datos != Email + "." + Contraseña)
                {
                    OnPostBtClean();
                    return;
                }
                OnPostBtClean();
                ViewData["Logged"] = true;
                HttpContext.Session.SetObject("Usuario", Email!);
                IsLogged = true;
            }
            catch (Exception ex)
            {
                LogHelper.Log(ex);
            }
        }

        public void OnPostBtClose()
        {
            try
            {
                HttpContext.Session.Clear();
                HttpContext.Response.Redirect("/");
                IsLogged = false;
            }
            catch (Exception ex)
            {
                LogHelper.Log(ex);
            }
        }
    }
}