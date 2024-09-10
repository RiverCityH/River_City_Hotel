using lib_aplicaciones.Implementaciones;
using lib_entidades_dominio;
using lib_utilidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace asp_hoteles.Pages.Emergentes
{
    public class CiudadesPPModel : PageModel
    {
        private CiudadesAplicacion? CiudadesAplicacion = null;
        public HttpContext? ContextHttp { get; set; }
        public ViewDataDictionary? DataView { get; set; }

        public CiudadesPPModel(CiudadesAplicacion p_CiudadesAplicacion)
        {
            try
            {
                this.CiudadesAplicacion = this.CiudadesAplicacion == null ?
                    p_CiudadesAplicacion : this.CiudadesAplicacion;
                this.CiudadesAplicacion.Configurar(Startup.Configuration!["ConectionString"]!);
            }
            catch (Exception ex)
            {
                LogHelper.Log(ex, ViewData!);
            }
        }

        [BindProperty] public Ciudades? Actual { get; set; }
        [BindProperty] public List<Ciudades>? Lista { get; set; }

        public virtual void OnGet() { OnPostBtRefrescar(); }

        public void OnPostBtRefrescar()
        {
            try
            {
                Lista = CiudadesAplicacion!.Listar();
            }
            catch (Exception ex)
            {
                LogHelper.Log(ex, ViewData!);
            }
        }
    }
}