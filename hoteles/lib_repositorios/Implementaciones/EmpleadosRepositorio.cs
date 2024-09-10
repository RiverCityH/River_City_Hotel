using lib_entidades_dominio;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;

namespace lib_repositorios.Implementaciones
{
    public class EmpleadosRepositorio
    {
        private Conexion? conexion;

        public EmpleadosRepositorio(Conexion conexion)
        {
            this.conexion = conexion;
        }

        public void Configurar(string string_conexion)
        {
            this.conexion!.StringConnection = string_conexion;
        }

        public List<Empleados> Listar()
        {
            return conexion!.ObtenerSet<Empleados>()
                .Include(x => x._Persona)
                .Include(x => x._Cargo)
                .Include(x => x._ARL)
                .Include(x => x._Pension)
                .Include(x => x._EPS)
                .Include(x => x._TipoSangre)
                .Include(x => x._EstadoCivil)
                .ToList();
        }

        public List<Empleados> Buscar(Expression<Func<Empleados, bool>> condiciones)
        {
            return conexion!.ObtenerSet<Empleados>()
                .Include(x => x._Persona)
                .Include(x => x._Cargo)
                .Include(x => x._ARL)
                .Include(x => x._Pension)
                .Include(x => x._EPS)
                .Include(x => x._TipoSangre)
                .Include(x => x._EstadoCivil)
                .ToList();
        }

        public Empleados Guardar(Empleados entidad)
        {
            conexion!.Guardar(entidad);
            conexion!.GuardarCambios();
            return entidad;
        }

        public Empleados Modificar(Empleados entidad)
        {
            conexion!.Modificar(entidad);
            conexion!.GuardarCambios();
            return entidad;
        }

        public Empleados Borrar(Empleados entidad)
        {
            conexion!.Borrar(entidad);
            conexion!.GuardarCambios();
            return entidad;
        }

        public bool Existe(Expression<Func<Empleados, bool>> condiciones)
        {
            return conexion!.Existe(condiciones);
        }
    }
}
