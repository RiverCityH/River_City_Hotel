using asp_hoteles.Nucleo;
using asp_hoteles.Pages.Emergentes;
using lib_aplicaciones.Implementaciones;
using lib_entidades_dominio;
using lib_utilidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace asp_hoteles.Pages.Ventanas
{
    public class FacturaModel : PageModel
    {
        private FacturasAplicacion? facturasAplicacion = null;
        public bool MostrarLista = true, 
            MostrarBorrar = false,
            MostrarTipos = false,
            MostrarPersonas = false;
        public PersonasPPModel? personasPP = null;
        public TiposPPModel? tiposPP = null; 

        public FacturaModel(
            FacturasAplicacion p_FacturasAplicacion,
            PersonasPPModel p_personasPP,
            TiposPPModel p_tiposPP)
        {
            try
            {
                this.facturasAplicacion = this.facturasAplicacion == null ?
                    p_FacturasAplicacion : this.facturasAplicacion;
                this.personasPP = this.personasPP == null ?
                    p_personasPP : this.personasPP;
                this.tiposPP = this.tiposPP == null ?
                    p_tiposPP : this.tiposPP;
                this.facturasAplicacion.Configurar(Startup.Configuration!["ConectionString"]!);
            }
            catch (Exception ex)
            {
                LogHelper.Log(ex, ViewData!);
            }
        }

        [BindProperty] public Facturas? Actual { get; set; }
        [BindProperty] public List<Facturas>? Lista { get; set; }

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
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.Log(ex, ViewData!);
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
                Lista = facturasAplicacion!.Listar();
            }
            catch (Exception ex)
            {
                LogHelper.Log(ex, ViewData!);
            }
        }

        public virtual void OnPostBtNuevo()
        {
            try
            {
                if (!ChequearUsuario())
                    return;
                MostrarLista = false;
                Actual = new Facturas()
                {
                    Fecha = DateTime.Now,
                    Activo = true,
                };
            }
            catch (Exception ex)
            {
                LogHelper.Log(ex, ViewData!);
            }
        }

        public virtual void OnPostBtModificar(string data)
        {
            try
            {
                if (!ChequearUsuario())
                    return;
                MostrarLista = false;
                OnPostBtRefrescar();
                Actual = Lista!
                    .FirstOrDefault(x => x.Id.ToString() == EsconderID.Desencriptar(data));
            }
            catch (Exception ex)
            {
                LogHelper.Log(ex, ViewData!);
            }
        }

        public virtual void OnPostBtGuardar()
        {
            try
            {
                MostrarLista = false;
                if (Actual!.Id == 0)
                    Actual = facturasAplicacion!.Guardar(Actual!);
                else
                    Actual = facturasAplicacion!.Modificar(Actual!);
                MostrarLista = true;
                OnPostBtRefrescar();
            }
            catch (Exception ex)
            {
                LogHelper.Log(ex, ViewData!);
            }
        }

        public virtual void OnPostBtBorrarVal(string data)
        {
            try
            {
                if (!ChequearUsuario())
                    return;
                MostrarBorrar = true;
                ViewData!["MostrarLista"] = MostrarLista;
                OnPostBtRefrescar();
                Actual = Lista!
                    .FirstOrDefault(x => x.Id.ToString() == EsconderID.Desencriptar(data));
            }
            catch (Exception ex)
            {
                LogHelper.Log(ex, ViewData!);
            }
        }

        public virtual void OnPostBtBorrar()
        {
            try
            {
                Actual = facturasAplicacion!.Borrar(Actual!);
                OnPostBtRefrescar();
            }
            catch (Exception ex)
            {
                LogHelper.Log(ex, ViewData!);
            }
        }

        public void OnPostBtCancelar()
        {
            try
            {
                MostrarLista = true;
                MostrarBorrar = false;
                OnPostBtRefrescar();
            }
            catch (Exception ex)
            {
                LogHelper.Log(ex, ViewData!);
            }
        }

        public void OnPostBtPersonas()
        {
            try
            {
                MostrarLista = false;
                MostrarPersonas = true;
                if (!ChequearUsuario())
                    return;
                personasPP!.ContextHttp = this.HttpContext;
                personasPP!.DataView = this.ViewData;
                personasPP!.OnPostBtRefrescar();
            }
            catch (Exception ex)
            {
                LogHelper.Log(ex, ViewData!);
            }
        }

        public void OnPostBtSelPersona(string data)
        {
            try
            {
                MostrarLista = false;
                if (!ChequearUsuario())
                    return;
                if (personasPP == null)
                    return;
                personasPP!.ContextHttp = this.HttpContext;
                personasPP!.DataView = this.ViewData;
                personasPP!.OnPostBtRefrescar();

                var seleccionado = personasPP!.Lista!.
                    FirstOrDefault(x => x.Id.ToString() == EsconderID.Desencriptar(data));
                if (seleccionado == null || Actual == null)
                    return;
                ModelState.Clear();
                Actual!.Persona = seleccionado.Id;
                Actual!._Persona = seleccionado;
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
                MostrarLista = false;
                MostrarTipos = true;
                if (!ChequearUsuario())
                    return;
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

                MostrarLista = false;
                if (!ChequearUsuario())
                    return;
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
                    case "MetodoPago":
                        Actual!.MetodoPago = seleccionado.Id;
                        Actual!._MetodoPago = seleccionado;
                        break;
                    case "TiposFacturas":
                        Actual!.Tipo = seleccionado.Id;
                        Actual!._Tipo = seleccionado;
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