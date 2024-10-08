﻿using asp_hoteles.Nucleo;
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
        private DetallesAplicacion? detallesAplicacion = null;
        public bool MostrarLista = true, 
            MostrarBorrar = false,
            MostrarTipos = false,
            MostrarProductos = false;
        public ProductosPPModel? productosPP = null;

        public FacturaModel(
            FacturasAplicacion p_facturasAplicacion,
            DetallesAplicacion p_detallesAplicacion,
            ProductosPPModel p_productosPP)
        {
            try
            {
                this.facturasAplicacion = this.facturasAplicacion == null ?
                    p_facturasAplicacion : this.facturasAplicacion;
                this.detallesAplicacion = this.detallesAplicacion == null ?
                    p_detallesAplicacion : this.detallesAplicacion;
                this.productosPP = this.productosPP == null ?
                    p_productosPP : this.productosPP;
                this.detallesAplicacion.Configurar(Startup.Configuration!["ConectionString"]!);
            }
            catch (Exception ex)
            {
                LogHelper.Log(ex, ViewData!);
            }
        }

        [BindProperty] public int id_factura { get; set; }
        [BindProperty] public Facturas? Factura { get; set; }
        [BindProperty] public Detalles? Actual { get; set; }
        [BindProperty] public List<Detalles>? Lista { get; set; }

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

        public virtual void OnGet(string data) 
        {
            try
            {
                id_factura = Convert.ToInt32(EncryptHelper.Desencriptar(data));
                OnPostBtRefrescar();
            }
            catch (Exception ex)
            {
                LogHelper.Log(ex, ViewData!);
            } 
        }

        public void OnPostBtRefrescar()
        {
            try
            {
                if (!ChequearUsuario())
                    return;
                var detalle = new Detalles()
                {
                    Factura = id_factura,
                };
                Lista = detallesAplicacion!.Buscar(detalle, "Factura");
                CargarFactura();
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
                CargarFactura();
                Actual = new Detalles();
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
                CargarFactura();
                Actual!.Factura = Factura!.Id;
                Actual!.Total = Actual!.Valor * Actual!.Cantidad;

                if (Actual!.Id == 0)
                    Actual = detallesAplicacion!.Guardar(Actual!);
                else
                    Actual = detallesAplicacion!.Modificar(Actual!);

                decimal total = 0.0m;
                var detalles = Factura!.Detalles!.ToList();
                if (detalles.Any(x => x.Id == Actual!.Id))
                    detalles.Remove(detalles.FirstOrDefault(x => x.Id == Actual!.Id)!);
                detalles.Add(Actual!);

                foreach (var detalle in detalles)
                    total += detalle.Total;
                Factura.Total = total;
                Factura = facturasAplicacion!.Modificar(Factura!);
                Factura = null;

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
                Actual = detallesAplicacion!.Borrar(Actual!);
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

        public void OnPostBtProductos()
        {
            try
            {
                MostrarLista = false;
                MostrarProductos = true;
                if (!ChequearUsuario())
                    return;
                productosPP!.ContextHttp = this.HttpContext;
                productosPP!.DataView = this.ViewData;
                productosPP!.OnPostBtRefrescar();
            }
            catch (Exception ex)
            {
                LogHelper.Log(ex, ViewData!);
            }
        }

        public void OnPostBtSelProducto(string data)
        {
            try
            {
                MostrarLista = false;
                if (!ChequearUsuario())
                    return;
                if (productosPP == null)
                    return;
                productosPP!.ContextHttp = this.HttpContext;
                productosPP!.DataView = this.ViewData;
                productosPP!.OnPostBtRefrescar();

                var seleccionado = productosPP!.Lista!.
                    FirstOrDefault(x => x.Id.ToString() == EsconderID.Desencriptar(data));
                if (seleccionado == null || Actual == null)
                    return;
                ModelState.Clear();
                Actual!.Producto = seleccionado.Id;
                Actual!._Producto = seleccionado;
                Actual!.Valor = seleccionado.Valor;
                CargarFactura();
            }
            catch (Exception ex)
            {
                LogHelper.Log(ex, ViewData!);
            }
        }

        private void CargarFactura()
        {
            try
            {
                if (Factura != null && Factura.Id != 0)
                    return;
                var factura = new Facturas()
                {
                    Id = id_factura,
                };
                var facturas = facturasAplicacion!.Buscar(factura, "ID");
                if (facturas.Count < 0)
                    return;
                Factura = facturas.FirstOrDefault();
            }
            catch (Exception ex)
            {
                LogHelper.Log(ex, ViewData!);
            }
        }
    }
}