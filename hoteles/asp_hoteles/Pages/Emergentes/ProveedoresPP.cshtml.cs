using lib_aplicaciones.Implementaciones;
using lib_entidades_dominio;
using lib_utilidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace asp_hoteles.Pages.Emergentes
{
    public class ProveedoresPPModel : PageModel
    {
        private ProveedoresAplicacion? ProveedoresAplicacion = null;
        public HttpContext? ContextHttp { get; set; }
        public ViewDataDictionary? DataView { get; set; }

        public ProveedoresPPModel(ProveedoresAplicacion p_ProveedoresAplicacion)
        {
            try
            {
                this.ProveedoresAplicacion = this.ProveedoresAplicacion == null ?
                    p_ProveedoresAplicacion : this.ProveedoresAplicacion;
                this.ProveedoresAplicacion.Configurar(Startup.Configuration!["ConectionString"]!);
            }
            catch (Exception ex)
            {
                LogHelper.Log(ex, ViewData!);
            }
        }

        [BindProperty] public Proveedores? Actual { get; set; }
        [BindProperty] public List<Proveedores>? Lista { get; set; }

        public virtual void OnGet() { OnPostBtRefrescar(); }

        public void OnPostBtRefrescar()
        {
            try
            {
                Lista = ProveedoresAplicacion!.Listar();
            }
            catch (Exception ex)
            {
                LogHelper.Log(ex, ViewData!);
            }
        }
    }
}