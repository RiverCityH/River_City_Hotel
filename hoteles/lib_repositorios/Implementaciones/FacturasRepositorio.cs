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
            return conexion!.ObtenerSet<Facturas>()
                .Include(x => x._Persona)
                .Include(x => x._MetodoPago)
                .Include(x => x._Tipo)
                .ToList();
        }

        public List<Facturas> Buscar(Expression<Func<Facturas, bool>> condiciones)
        {
            return conexion!.ObtenerSet<Facturas>()
                .Where(condiciones)
                .Include(x => x._Persona)
                .Include(x => x._MetodoPago)
                .Include(x => x._Tipo)
                .ToList();
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
