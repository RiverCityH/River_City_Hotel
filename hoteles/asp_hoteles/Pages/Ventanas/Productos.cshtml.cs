using asp_hoteles.Nucleo;
using asp_hoteles.Pages.Emergentes;
using lib_aplicaciones.Implementaciones;
using lib_entidades_dominio;
using lib_utilidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace asp_hoteles.Pages.Ventanas
{
    public class ProductosModel : PageModel
    {
        private ProductosAplicacion? productosAplicacion = null;
        public bool MostrarLista = true, 
            MostrarBorrar = false,
            MostrarTipos = false,
            MostrarProveedores = false;
        public TiposPPModel? tiposPP = null;
        public ProveedoresPPModel? proveedoresPP = null;

        public ProductosModel(
            ProductosAplicacion p_ProductosAplicacion,
            TiposPPModel p_tiposPP,
            ProveedoresPPModel p_proveedoresPP)
        {
            try
            {
                this.productosAplicacion = this.productosAplicacion == null ?
                    p_ProductosAplicacion : this.productosAplicacion;
                this.tiposPP = this.tiposPP == null ?
                    p_tiposPP : this.tiposPP;
                this.proveedoresPP = this.proveedoresPP == null ?
                    p_proveedoresPP : this.proveedoresPP;
                this.productosAplicacion.Configurar(Startup.Configuration!["ConectionString"]!);
            }
            catch (Exception ex)
            {
                LogHelper.Log(ex, ViewData!);
            }
        }

        [BindProperty] public Productos? Actual { get; set; }
        [BindProperty] public List<Productos>? Lista { get; set; }

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
                Lista = productosAplicacion!.Listar();
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
                Actual = new Productos();
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
                    Actual = productosAplicacion!.Guardar(Actual!);
                else
                    Actual = productosAplicacion!.Modificar(Actual!);
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
                Actual = productosAplicacion!.Borrar(Actual!);
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

        public void OnPostBtProveedores()
        {
            try
            {
                MostrarLista = false;
                MostrarProveedores = true;
                if (!ChequearUsuario())
                    return;
                proveedoresPP!.ContextHttp = this.HttpContext;
                proveedoresPP!.DataView = this.ViewData;
                proveedoresPP!.OnPostBtRefrescar();
            }
            catch (Exception ex)
            {
                LogHelper.Log(ex, ViewData!);
            }
        }

        public void OnPostBtSelProveedor(string data)
        {
            try
            {
                MostrarLista = false;
                if (!ChequearUsuario())
                    return;
                if (proveedoresPP == null)
                    return;
                proveedoresPP!.ContextHttp = this.HttpContext;
                proveedoresPP!.DataView = this.ViewData;
                proveedoresPP!.OnPostBtRefrescar();

                var seleccionado = proveedoresPP!.Lista!.
                    FirstOrDefault(x => x.Id.ToString() == EsconderID.Desencriptar(data));
                if (seleccionado == null || Actual == null)
                    return;
                ModelState.Clear();
                Actual!.Proveedor = seleccionado.Id;
                Actual!._Proveedor = seleccionado;
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
                    case "Categoría":
                        Actual!.Categoria = seleccionado.Id;
                        Actual!._Categoria = seleccionado;
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