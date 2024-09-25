using asp_hoteles.Nucleo;
using asp_hoteles.Pages.Emergentes;
using lib_aplicaciones.Implementaciones;
using lib_entidades_dominio;
using lib_utilidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace asp_hoteles.Pages
{
    public class RegistrarseModel : PageModel
    {
        private PersonasAplicacion? PersonasAplicacion = null;
        public bool MostrarTipos = false,
            MostrarCiudades = false;
        public CiudadesPPModel? ciudadesPP = null;
        public TiposPPModel? tiposPP = null;

        public RegistrarseModel(
            PersonasAplicacion p_PersonasAplicacion,
            CiudadesPPModel p_ciudadesPP,
            TiposPPModel p_tiposPP)
        {
            try
            {
                this.PersonasAplicacion = this.PersonasAplicacion == null ?
                    p_PersonasAplicacion : this.PersonasAplicacion;
                this.ciudadesPP = this.ciudadesPP == null ?
                    p_ciudadesPP : this.ciudadesPP;
                this.tiposPP = this.tiposPP == null ?
                    p_tiposPP : this.tiposPP;
                this.PersonasAplicacion.Configurar(Startup.Configuration!["ConectionString"]!);
                Actual = new Personas() 
                { 
                    FechaNacimiento = DateTime.Now,
                    Activo = true
                };
            }
            catch (Exception ex)
            {
                LogHelper.Log(ex, ViewData!);
            }
        }

        [BindProperty] public Personas? Actual { get; set; }

        public bool ChequearUsuario()
        {
            try
            {
                var usario = HttpContext!.Session.GetObject<string>("Usuario");
                if (usario == null)
                {
                    HttpContext.Response.Redirect("/");
                    return false;
                }
                ViewData!["Logueado"] = true;
                ViewData["Rol"] = HttpContext!.Session.GetObject<string>("Rol");
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.Log(ex, ViewData!);
                return false;
            }
        }

        public virtual void OnGet() { }

        public virtual void OnPostBtGuardar()
        {
            try
            {
                if (!EncryptHelper.EstaEncriptado(Actual!.Contraseña!))
                    Actual!.Contraseña = EncryptHelper.Encriptar(Actual!.Contraseña!);

                Actual!.Token = EncryptHelper.Encriptar(Actual!.Email + DateTime.Now.ToString());
                if (Actual!.Id == 0)
                    Actual = PersonasAplicacion!.Guardar(Actual!);

                EmailHelper.SendEmail("rivercityhotel85@gmail.com", "riverhotel.1234", Actual!.Email, null, "Verificacion de cuenta",
                    Actual!.Token!);
            }
            catch (Exception ex)
            {
                LogHelper.Log(ex, ViewData!);
            }
        }

        public void OnPostBtCiudades()
        {
            try
            {
                MostrarCiudades = true;
                ciudadesPP!.ContextHttp = this.HttpContext;
                ciudadesPP!.DataView = this.ViewData;
                ciudadesPP!.OnPostBtRefrescar();
            }
            catch (Exception ex)
            {
                LogHelper.Log(ex, ViewData!);
            }
        }

        public void OnPostBtSelCiudad(string data)
        {
            try
            {
                if (ciudadesPP == null)
                    return;
                ciudadesPP!.ContextHttp = this.HttpContext;
                ciudadesPP!.DataView = this.ViewData;
                ciudadesPP!.OnPostBtRefrescar();

                var seleccionado = ciudadesPP!.Lista!.
                    FirstOrDefault(x => x.Id.ToString() == EsconderID.Desencriptar(data));
                if (seleccionado == null || Actual == null)
                    return;
                ModelState.Clear();
                Actual!.Ciudad = seleccionado.Id;
                Actual!._Ciudad = seleccionado;
            }
            catch (Exception ex)
            {
                LogHelper.Log(ex, ViewData!);
            }
        }

        public void OnPostBtTipos(string data)
        {
            try
            {
                MostrarTipos = true;
                tiposPP!.ContextHttp = this.HttpContext;
                tiposPP!.DataView = this.ViewData;
                tiposPP!.DataView["Tabla"] = data;
                tiposPP!.OnPostBtRefrescar();
            }
            catch (Exception ex)
            {
                LogHelper.Log(ex, ViewData!);
            }
        }

        public void OnPostBtSelTipo(string data)
        {
            try
            {
                var split = data.Split("||");

                if (tiposPP == null)
                    return;
                tiposPP!.ContextHttp = this.HttpContext;
                tiposPP!.DataView = this.ViewData;
                tiposPP!.DataView["Tabla"] = split[1].Trim();
                tiposPP!.OnPostBtRefrescar();

                var seleccionado = tiposPP!.Lista!.
                    FirstOrDefault(x => x.Id.ToString() == EsconderID.Desencriptar(split[0].Trim()));
                if (seleccionado == null || Actual == null)
                    return;
                ModelState.Clear();

                switch (split[1].Trim())
                {
                    case "TipoDocumentos":
                        Actual!.TipoDocumento = seleccionado.Id;
                        Actual!._TipoDocumento = seleccionado;
                        break;
                    case "Generos":
                        Actual!.Genero = seleccionado.Id;
                        Actual!._Genero = seleccionado;
                        break;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log(ex, ViewData!);
            }
        }
    }
}