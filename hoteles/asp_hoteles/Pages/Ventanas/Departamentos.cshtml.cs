using asp_hoteles.Nucleo;
using asp_hoteles.Pages.Emergentes;
using lib_aplicaciones.Implementaciones;
using lib_entidades_dominio;
using lib_utilidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace asp_hoteles.Pages.Ventanas
{
    public class DepartamentosModel : PageModel
    {
        private DepartamentosAplicacion? departamentosAplicacion = null;
        public bool MostrarLista = true, 
            MostrarBorrar = false,
            MostrarPaises = false;
        public PaisesPPModel? paisesPP = null;

        public DepartamentosModel(
            DepartamentosAplicacion p_departamentosAplicacion,
            PaisesPPModel p_paisesPP)
        {
            try
            {
                this.departamentosAplicacion = this.departamentosAplicacion == null ?
                    p_departamentosAplicacion : this.departamentosAplicacion;
                this.paisesPP = this.paisesPP == null ?
                    p_paisesPP : this.paisesPP;
                this.departamentosAplicacion.Configurar(Startup.Configuration!["ConectionString"]!);
            }
            catch (Exception ex)
            {
                LogHelper.Log(ex);
            }
        }

        [BindProperty] public Departamentos? Actual { get; set; }
        [BindProperty] public List<Departamentos>? Lista { get; set; }

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
                Lista = departamentosAplicacion!.Listar();
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
                Actual = new Departamentos();
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
                    Actual = departamentosAplicacion!.Guardar(Actual!);
                else
                    Actual = departamentosAplicacion!.Modificar(Actual!);
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
                Actual = departamentosAplicacion!.Borrar(Actual!);
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

        public void OnPostBtPaises()
        {
            try
            {
                MostrarLista = false;
                MostrarPaises = true;
                if (!ChequearUsuario())
                    return;
                paisesPP!.ContextHttp = this.HttpContext;
                paisesPP!.DataView = this.ViewData;
                paisesPP!.OnPostBtRefrescar();
            }
            catch (Exception ex)
            {
                LogHelper.Log(ex);
            }
        }

        public void OnPostBtSeleccionarPais(string data)
        {
            try
            {
                MostrarLista = false;
                if (!ChequearUsuario())
                    return;
                if (paisesPP == null)
                    return;
                paisesPP!.ContextHttp = this.HttpContext;
                paisesPP!.DataView = this.ViewData;
                paisesPP!.OnPostBtRefrescar();

                var seleccionado = paisesPP.Lista!.
                    FirstOrDefault(x => x.Id.ToString() == EsconderID.Desencriptar(data));
                if (seleccionado == null || Actual == null)
                    return;
                ModelState.Clear();
                Actual!.Pais = seleccionado.Id;
                Actual!._Pais = seleccionado;
            }
            catch (Exception ex)
            {
                LogHelper.Log(ex);
            }
        }
    }
}