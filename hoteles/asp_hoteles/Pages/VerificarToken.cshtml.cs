using lib_aplicaciones.Implementaciones;
using lib_entidades_dominio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace asp_hoteles.Pages
{
    public class VerificarTokenModel : PageModel
    {
        private PersonasAplicacion _personasAplicacion;

        public VerificarTokenModel(PersonasAplicacion personasAplicacion)
        {
            _personasAplicacion = personasAplicacion;
        }

        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public string Token { get; set; }

        public string Mensaje { get; private set; }

        public void OnGet() { }

        public void OnPost()
        {
            try
            {
                var persona = _personasAplicacion.VerificarToken(Email, Token);
                Mensaje = "¡Cuenta confirmada con éxito!";
            }
            catch (Exception ex)
            {
                Mensaje = ex.Message; 
            }
        }
    }
}