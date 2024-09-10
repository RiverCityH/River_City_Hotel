using lib_aplicaciones.Implementaciones;
using lib_entidades_dominio;
using lib_utilidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace asp_hoteles.Pages.Emergentes
{
    public class DepartamentosPPModel : PageModel
    {
        private DepartamentosAplicacion? DepartamentosAplicacion = null;
        public HttpContext? ContextHttp { get; set; }
        public ViewDataDictionary? DataView { get; set; }

        public DepartamentosPPModel(DepartamentosAplicacion p_DepartamentosAplicacion)
        {
            try
            {
                this.DepartamentosAplicacion = this.DepartamentosAplicacion == null ?
                    p_DepartamentosAplicacion : this.DepartamentosAplicacion;
                this.DepartamentosAplicacion.Configurar(Startup.Configuration!["ConectionString"]!);
            }
            catch (Exception ex)
            {
                LogHelper.Log(ex, ViewData!);
            }
        }

        [BindProperty] public Departamentos? Actual { get; set; }
        [BindProperty] public List<Departamentos>? Lista { get; set; }

        public virtual void OnGet() { OnPostBtRefrescar(); }

        public void OnPostBtRefrescar()
        {
            try
            {
                Lista = DepartamentosAplicacion!.Listar();
            }
            catch (Exception ex)
            {
                LogHelper.Log(ex, ViewData!);
            }
        }
    }
}