using asp_hoteles.Nucleo;
using asp_hoteles.Pages.Emergentes;
using lib_aplicaciones.Implementaciones;
using lib_entidades_dominio;
using lib_utilidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace asp_hoteles.Pages.Ventanas
{
    public class EmpleadosModel : PageModel
    {
        private EmpleadosAplicacion? empleadosAplicacion = null;
        public bool MostrarLista = true, 
            MostrarBorrar = false,
            MostrarTipos = false,
            MostrarPersonas = false;
        public PersonasPPModel? personasPP = null;
        public TiposPPModel? tiposPP = null; 

        public EmpleadosModel(
            EmpleadosAplicacion p_EmpleadosAplicacion,
            PersonasPPModel p_personasPP,
            TiposPPModel p_tiposPP)
        {
            try
            {
                this.empleadosAplicacion = this.empleadosAplicacion == null ?
                    p_EmpleadosAplicacion : this.empleadosAplicacion;
                this.personasPP = this.personasPP == null ?
                    p_personasPP : this.personasPP;
                this.tiposPP = this.tiposPP == null ?
                    p_tiposPP : this.tiposPP;
                this.empleadosAplicacion.Configurar(Startup.Configuration!["ConectionString"]!);
            }
            catch (Exception ex)
            {
                LogHelper.Log(ex, ViewData!);
            }
        }

        [BindProperty] public Empleados? Actual { get; set; }
        [BindProperty] public List<Empleados>? Lista { get; set; }

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

        public virtual void OnGet() { OnPostBtRefrescar(); }

        public void OnPostBtRefrescar()
        {
            try
            {
                if (!ChequearUsuario())
                    return;
                Lista = empleadosAplicacion!.Listar();
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
                Actual = new Empleados();
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
                    Actual = empleadosAplicacion!.Guardar(Actual!);
                else
                    Actual = empleadosAplicacion!.Modificar(Actual!);
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
                Actual = empleadosAplicacion!.Borrar(Actual!);
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
                    case "Cargo":
                        Actual!.Cargo = seleccionado.Id;
                        Actual!._Cargo = seleccionado;
                        break;
                    case "ARL":
                        Actual!.ARL = seleccionado.Id;
                        Actual!._ARL = seleccionado;
                        break;
                    case "Pension":
                        Actual!.Pension = seleccionado.Id;
                        Actual!._Pension = seleccionado;
                        break;
                    case "EPS":
                        Actual!.EPS = seleccionado.Id;
                        Actual!._EPS = seleccionado;
                        break;
                    case "TipoSangre":
                        Actual!.TipoSangre = seleccionado.Id;
                        Actual!._TipoSangre = seleccionado;
                        break;
                    case "EstadoCivil":
                        Actual!.EstadoCivil = seleccionado.Id;
                        Actual!._EstadoCivil = seleccionado;
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