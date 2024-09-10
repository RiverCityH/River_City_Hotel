using asp_hoteles.Nucleo;
using asp_hoteles.Pages.Emergentes;
using lib_aplicaciones.Implementaciones;
using lib_entidades_dominio;
using lib_utilidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace asp_hoteles.Pages.Ventanas
{
    public class CiudadesModel : PageModel
    {
        private CiudadesAplicacion? CiudadesAplicacion = null;
        public bool MostrarLista = true, 
            MostrarBorrar = false,
            MostrarPaises = false;
        public DepartamentosPPModel? departamentosPP = null;

        public CiudadesModel(
            CiudadesAplicacion p_CiudadesAplicacion,
            DepartamentosPPModel p_departamentosPP)
        {
            try
            {
                this.CiudadesAplicacion = this.CiudadesAplicacion == null ?
                    p_CiudadesAplicacion : this.CiudadesAplicacion;
                this.departamentosPP = this.departamentosPP == null ?
                    p_departamentosPP : this.departamentosPP;
                this.CiudadesAplicacion.Configurar(Startup.Configuration!["ConectionString"]!);
            }
            catch (Exception ex)
            {
                LogHelper.Log(ex);
            }
        }

        [BindProperty] public Ciudades? Actual { get; set; }
        [BindProperty] public List<Ciudades>? Lista { get; set; }

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
                LogHelper.Log(ex);
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
                Lista = CiudadesAplicacion!.Listar();
            }
            catch (Exception ex)
            {
                LogHelper.Log(ex);
            }
        }

        public virtual void OnPostBtNuevo()
        {
            try
            {
                if (!ChequearUsuario())
                    return;
                MostrarLista = false;
                Actual = new Ciudades();
            }
            catch (Exception ex)
            {
                LogHelper.Log(ex);
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
                LogHelper.Log(ex);
            }
        }

        public virtual void OnPostBtGuardar()
        {
            try
            {
                MostrarLista = false;
                if (Actual!.Id == 0)
                    Actual = CiudadesAplicacion!.Guardar(Actual!);
                else
                    Actual = CiudadesAplicacion!.Modificar(Actual!);
                MostrarLista = true;
                OnPostBtRefrescar();
            }
            catch (Exception ex)
            {
                LogHelper.Log(ex);
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
                LogHelper.Log(ex);
            }
        }

        public virtual void OnPostBtBorrar()
        {
            try
            {
                Actual = CiudadesAplicacion!.Borrar(Actual!);
                OnPostBtRefrescar();
            }
            catch (Exception ex)
            {
                LogHelper.Log(ex);
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
                LogHelper.Log(ex);
            }
        }

        public void OnPostBtDepartamentos()
        {
            try
            {
                MostrarLista = false;
                MostrarPaises = true;
                if (!ChequearUsuario())
                    return;
                departamentosPP!.ContextHttp = this.HttpContext;
                departamentosPP!.DataView = this.ViewData;
                departamentosPP!.OnPostBtRefrescar();
            }
            catch (Exception ex)
            {
                LogHelper.Log(ex);
            }
        }

        public void OnPostBtSelDepartamento(string data)
        {
            try
            {
                MostrarLista = false;
                if (!ChequearUsuario())
                    return;
                if (departamentosPP == null)
                    return;
                departamentosPP!.ContextHttp = this.HttpContext;
                departamentosPP!.DataView = this.ViewData;
                departamentosPP!.OnPostBtRefrescar();

                var seleccionado = departamentosPP.Lista!.
                    FirstOrDefault(x => x.Id.ToString() == EsconderID.Desencriptar(data));
                if (seleccionado == null || Actual == null)
                    return;
                ModelState.Clear();
                Actual!.Departamento = seleccionado.Id;
                Actual!._Departamento = seleccionado;
            }
            catch (Exception ex)
            {
                LogHelper.Log(ex);
            }
        }
    }
}