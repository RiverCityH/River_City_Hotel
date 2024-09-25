using asp_hoteles.Nucleo;
using lib_aplicaciones.Implementaciones;
using lib_entidades_dominio;
using lib_utilidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace asp_hoteles.Pages
{
    public class InicioCliente : PageModel
    {
        private PersonasAplicacion? personasAplicacion = null;
        private EmpleadosAplicacion? empleadosAplicacion = null;
        public bool EstaLogueado = false;

        public InicioCliente(PersonasAplicacion p_personasAplicacion, EmpleadosAplicacion p_empleadosAplicacion)
        {
            try
            {
                this.personasAplicacion = this.personasAplicacion == null ?
                    p_personasAplicacion : this.personasAplicacion;
                this.empleadosAplicacion = this.empleadosAplicacion == null ?
                    p_empleadosAplicacion : this.empleadosAplicacion;
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
                    ViewData["Rol"] = HttpContext!.Session.GetObject<string>("Rol");
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

                var empleado = new Empleados()
                {
                    Id = 0,
                    Persona = persona.Id,
                };
                var cargo = 2;

                var empleados = empleadosAplicacion!.Buscar(empleado, "PERSONA");
                if (empleados.Count > 0)
                {
                    empleado = empleados[0];
                    cargo = empleado._Cargo!.Accion;
                }
                persona = personas[0];

                OnPostBtLimpiar();
                ViewData["Logueado"] = true;
                ViewData["Rol"] = cargo;
                HttpContext.Session.SetObject("Usuario", Email!);
                HttpContext.Session.SetObject("Rol", cargo);
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