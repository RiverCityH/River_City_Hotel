using lib_aplicaciones.Implementaciones;
using lib_entidades_dominio;
using lib_utilidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace asp_hoteles.Pages.Emergentes
{
    public class PaisesModel : PageModel
    {
        private PaisesAplicacion? paisesAplicacion = null;
        public bool MostrarLista = true;

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

        public virtual void OnGet() { OnPostBtRefrescar(); }

        public void OnPostBtRefrescar()
        {
            try
            {
                Lista = paisesAplicacion!.Listar();
            }
            catch (Exception ex)
            {
                LogHelper.Log(ex);
            }
        }
    }
}