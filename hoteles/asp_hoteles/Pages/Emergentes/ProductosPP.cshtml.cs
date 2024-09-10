using lib_aplicaciones.Implementaciones;
using lib_entidades_dominio;
using lib_utilidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace asp_hoteles.Pages.Emergentes
{
    public class ProductosPPModel : PageModel
    {
        private ProductosAplicacion? productosAplicacion = null;
        public HttpContext? ContextHttp { get; set; }
        public ViewDataDictionary? DataView { get; set; }

        public ProductosPPModel(ProductosAplicacion p_ProductosAplicacion)
        {
            try
            {
                this.productosAplicacion = this.productosAplicacion == null ?
                    p_ProductosAplicacion : this.productosAplicacion;
                this.productosAplicacion.Configurar(Startup.Configuration!["ConectionString"]!);
            }
            catch (Exception ex)
            {
                LogHelper.Log(ex, ViewData!);
            }
        }

        [BindProperty] public Productos? Actual { get; set; }
        [BindProperty] public List<Productos>? Lista { get; set; }

        public virtual void OnGet() { OnPostBtRefrescar(); }

        public void OnPostBtRefrescar()
        {
            try
            {
                Lista = productosAplicacion!.Listar();
            }
            catch (Exception ex)
            {
                LogHelper.Log(ex, ViewData!);
            }
        }
    }
}