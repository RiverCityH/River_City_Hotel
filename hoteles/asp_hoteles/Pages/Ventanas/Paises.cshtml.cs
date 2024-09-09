using asp_hoteles.Nucleo;
using lib_aplicaciones.Implementaciones;
using lib_entidades_dominio;
using lib_utilidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace asp_hoteles.Pages.Ventanas
{
    public class PaisesModel : PageModel
    {
        private PaisesAplicacion? paisesAplicacion = null;
        public bool MostrarLista = true, MostrarBorrar = false;

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
                Lista = paisesAplicacion!.Listar();
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
                Actual = new Paises();
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
                    Actual = paisesAplicacion!.Guardar(Actual!);
                else
                    Actual = paisesAplicacion!.Modificar(Actual!);
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
                Actual = paisesAplicacion!.Borrar(Actual!);
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
    }
}