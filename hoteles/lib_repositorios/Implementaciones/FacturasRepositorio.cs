using lib_entidades_dominio;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace lib_repositorios.Implementaciones
{
    public class FacturasRepositorio
    {
        private Conexion? conexion;

        public FacturasRepositorio(Conexion conexion)
        {
            this.conexion = conexion;
        }

        public void Configurar(string string_conexion)
        {
            this.conexion!.StringConnection = string_conexion;
        }

        public List<Facturas> Listar()
        {
            return Buscar(x => x.Id > 0);
        }

        public List<Facturas> Buscar(Expression<Func<Facturas, bool>> condiciones)
        {
            var facturas = conexion!.ObtenerSet<Facturas>()
                .Where(condiciones)
                .Include(x => x._Persona)
                .Include(x => x._MetodoPago)
                .Include(x => x._Tipo)
                .ToList();
            var ids_facturas = facturas.Select(x => x.Id);

            var detalles = conexion.ObtenerSet<Detalles>()
                .Where(x => ids_facturas.Any(y => y == x.Factura))
                .Include(x => x._Producto)
                .ToList();
            detalles.ForEach(delegate (Detalles detalle)
            {
                detalle._Factura = null;
            });

            foreach (var factura in facturas)
            {
                factura.Detalles = detalles
                    .Where(x => x.Factura == factura.Id)
                    .ToList();
            }
            return facturas;
        }

        public Facturas Guardar(Facturas entidad)
        {
            conexion!.Guardar(entidad);
            conexion!.GuardarCambios();
            return entidad;
        }

        public Facturas Modificar(Facturas entidad)
        {
            conexion!.Modificar(entidad);
            conexion!.GuardarCambios();
            return entidad;
        }

        public Facturas Borrar(Facturas entidad)
        {
            conexion!.Borrar(entidad);
            conexion!.GuardarCambios();
            return entidad;
        }

        public bool Existe(Expression<Func<Facturas, bool>> condiciones)
        {
            return conexion!.Existe(condiciones);
        }
    }
}
