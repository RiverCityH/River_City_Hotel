using asp_hoteles.Nucleo;
using lib_aplicaciones.Implementaciones;
using lib_entidades_dominio;
using lib_utilidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace asp_hoteles.Pages
{
    public class IndexModel : PageModel
    {
        private PersonasAplicacion? personasAplicacion = null;
        public bool EstaLogueado = false;

        public IndexModel(PersonasAplicacion p_personasAplicacion)
        {
            try
            {
                this.personasAplicacion = this.personasAplicacion == null ?
                    p_personasAplicacion : this.personasAplicacion;
                this.personasAplicacion.Configurar(Startup.Configuration!["ConectionString"]!);
            }
            catch (Exception ex)
            {
                LogHelper.Log(ex, ViewData!);
            }
        }

        public void OnGet()
        {
            try
            {
                var user = HttpContext.Session.GetObject<string>("Usuario");
                if (user != null)
                {
                    EstaLogueado = true;
                    ViewData["Logueado"] = true;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log(ex, ViewData!);
            }
        }

        [BindProperty] public string? Email { get; set; }
        [BindProperty] public string? Contraseña { get; set; }

        public void OnPostBtLimpiar()
        {
            try
            {
                Email = string.Empty;
                Contraseña = string.Empty;
            }
            catch (Exception ex)
            {
                LogHelper.Log(ex, ViewData!);
            }
        }

        public void OnPostBtEntrar()
        {
            try
            {
                if (string.IsNullOrEmpty(Email) &&
                    string.IsNullOrEmpty(Contraseña))
                {
                    OnPostBtLimpiar();
                    return;
                }

                var persona = new Personas() 
                {
                    Id = 0,
                    Email = this.Email,
                    Contraseña = EncryptHelper.Encriptar(this.Contraseña!),
                };
                var personas = personasAplicacion!.Buscar(persona, "LOGIN");
                if (personas.Count <= 0)
                {
                    OnPostBtLimpiar();
                    return;
                }

                OnPostBtLimpiar();
                ViewData["Logueado"] = true;
                HttpContext.Session.SetObject("Usuario", Email!);
                EstaLogueado = true;
            }
            catch (Exception ex)
            {
                LogHelper.Log(ex, ViewData!);
            }
        }

        public void OnPostBtCerrar()
        {
            try
            {
                HttpContext.Session.Clear();
                HttpContext.Response.Redirect("/");
                EstaLogueado = false;
            }
            catch (Exception ex)
            {
                LogHelper.Log(ex, ViewData!);
            }
        }
    }
}