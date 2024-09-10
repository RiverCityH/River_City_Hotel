using lib_aplicaciones.Implementaciones;
using lib_entidades_dominio;
using lib_utilidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace asp_hoteles.Pages.Emergentes
{
    public class PersonasPPModel : PageModel
    {
        private PersonasAplicacion? personasAplicacion = null;
        public HttpContext? ContextHttp { get; set; }
        public ViewDataDictionary? DataView { get; set; }

        public PersonasPPModel(PersonasAplicacion p_PersonasAplicacion)
        {
            try
            {
                this.personasAplicacion = this.personasAplicacion == null ?
                    p_PersonasAplicacion : this.personasAplicacion;
                this.personasAplicacion.Configurar(Startup.Configuration!["ConectionString"]!);
            }
            catch (Exception ex)
            {
                LogHelper.Log(ex, ViewData!);
            }
        }

        [BindProperty] public Personas? Actual { get; set; }
        [BindProperty] public List<Personas>? Lista { get; set; }

        public virtual void OnGet() { OnPostBtRefrescar(); }

        public void OnPostBtRefrescar()
        {
            try
            {
                Lista = personasAplicacion!.Listar();
            }
            catch (Exception ex)
            {
                LogHelper.Log(ex, ViewData!);
            }
        }
    }
}