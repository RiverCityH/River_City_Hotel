using asp_hoteles.Nucleo;
using lib_aplicaciones.Implementaciones;
using lib_entidades_dominio;
using lib_repositorios.Implementaciones;
using lib_utilidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace asp_hoteles.Pages
{
    public class IndexModel : PageModel
    {
        private PersonasAplicacion? personasAplicacion = null;
        public bool IsLogged = false;

        public IndexModel(PersonasAplicacion p_personasAplicacion)
        {
            try
            {
                this.personasAplicacion = this.personasAplicacion == null ?
                    p_personasAplicacion : this.personasAplicacion;
            }
            catch (Exception ex)
            {
                LogHelper.Log(ex);
            }
        }

        public void OnGet()
        {
            try
            {
                var user = HttpContext.Session.GetObject<string>("User");
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

        [BindProperty] public string? Email { get; set; }
        [BindProperty] public string? Contraseña { get; set; }

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

                var persona = new Personas() 
                {
                    Id = 0,
                    Email = this.Email,
                    Contraseña = this.Contraseña,
                };
                var personas = personasAplicacion!.Buscar(persona, "LOGIN");

                OnPostBtClean();
                ViewData["Logged"] = true;
                HttpContext.Session.SetObject("User", Email!);
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