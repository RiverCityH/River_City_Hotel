﻿using asp_hoteles.Nucleo;
using asp_hoteles.Pages.Emergentes;
using lib_aplicaciones.Implementaciones;
using lib_entidades_dominio;
using lib_utilidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace asp_hoteles.Pages.Ventanas
{
    public class PersonasModel : PageModel
    {
        private PersonasAplicacion? PersonasAplicacion = null;
        public bool MostrarLista = true, 
            MostrarBorrar = false,
            MostrarPaises = false;
        public CiudadesPPModel? ciudadesPP = null;

        public PersonasModel(
            PersonasAplicacion p_PersonasAplicacion,
            CiudadesPPModel p_ciudadesPP)
        {
            try
            {
                this.PersonasAplicacion = this.PersonasAplicacion == null ?
                    p_PersonasAplicacion : this.PersonasAplicacion;
                this.ciudadesPP = this.ciudadesPP == null ?
                    p_ciudadesPP : this.ciudadesPP;
                this.PersonasAplicacion.Configurar(Startup.Configuration!["ConectionString"]!);
            }
            catch (Exception ex)
            {
                LogHelper.Log(ex);
            }
        }

        [BindProperty] public Personas? Actual { get; set; }
        [BindProperty] public List<Personas>? Lista { get; set; }

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
                Lista = PersonasAplicacion!.Listar();
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
                Actual = new Personas();
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
                    Actual = PersonasAplicacion!.Guardar(Actual!);
                else
                    Actual = PersonasAplicacion!.Modificar(Actual!);
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
                Actual = PersonasAplicacion!.Borrar(Actual!);
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

        public void OnPostBtCiudad()
        {
            try
            {
                MostrarLista = false;
                MostrarPaises = true;
                if (!ChequearUsuario())
                    return;
                ciudadesPP!.ContextHttp = this.HttpContext;
                ciudadesPP!.DataView = this.ViewData;
                ciudadesPP!.OnPostBtRefrescar();
            }
            catch (Exception ex)
            {
                LogHelper.Log(ex);
            }
        }

        public void OnPostBtSelCiudad(string data)
        {
            try
            {
                MostrarLista = false;
                if (!ChequearUsuario())
                    return;
                if (ciudadesPP == null)
                    return;
                ciudadesPP!.ContextHttp = this.HttpContext;
                ciudadesPP!.DataView = this.ViewData;
                ciudadesPP!.OnPostBtRefrescar();

                var seleccionado = ciudadesPP.Lista!.
                    FirstOrDefault(x => x.Id.ToString() == EsconderID.Desencriptar(data));
                if (seleccionado == null || Actual == null)
                    return;
                ModelState.Clear();
                Actual!.Ciudad = seleccionado.Id;
                Actual!._Ciudad = seleccionado;
            }
            catch (Exception ex)
            {
                LogHelper.Log(ex);
            }
        }
    }
}