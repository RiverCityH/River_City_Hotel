using asp_hoteles.Nucleo;
using lib_aplicaciones.Implementaciones;
using lib_entidades_dominio;
using lib_utilidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace asp_hoteles.Pages.Ventanas
{
    public class PaisesModel : PageModel
    {
        private PaisesAplicacion? paisesAplicacion = null;
        public bool MostrarLista = true, MostrarBorrar = false;
        public IFormFile? FormFile { get; set; }

        public PaisesModel(PaisesAplicacion p_paisesAplicacion)
        {
            try
            {
                this.paisesAplicacion = this.paisesAplicacion == null ?
                    p_paisesAplicacion : this.paisesAplicacion;
                this.paisesAplicacion.Configurar(Startup.Configuration!["ConectionString"]!);
            }
            catch (Exception ex)
            {
                LogHelper.Log(ex);
            }
        }

        [BindProperty] public Paises? Actual { get; set; }
        [BindProperty] public List<Paises>? Lista { get; set; }

        public bool ChequearUsuario()
        {
            try
            {
                var user = HttpContext!.Session.GetObject<string>("Usuario");
                if (user == null)
                {
                    HttpContext.Response.Redirect("/");
                    return false;
                }
                ViewData!["Logueado"] = true;
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.Log(ex);
                return false;
            }
        }

        public virtual void OnGet() { OnPostBtRefrescar(); }

        public void OnPostBtRefrescar()
        {
            try
            {
                if (!ChequearUsuario())
                    return;
                Lista = paisesAplicacion!.Listar();
            }
            catch (Exception ex)
            {
                LogHelper.Log(ex);
            }
        }
    }
}