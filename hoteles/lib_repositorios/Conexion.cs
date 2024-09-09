using lib_entidades_dominio;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace lib_repositorios
{
    public partial class Conexion : DbContext
    {
        public string? StringConnection { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(this.StringConnection!, p => { });
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }

        protected DbSet<Paises>? Paises { get; set; }
        protected DbSet<Departamentos>? Departamentos { get; set; }
        protected DbSet<Ciudades>? Ciudades { get; set; }
        protected DbSet<Tipos>? Tipos { get; set; }
        protected DbSet<Personas>? Personas { get; set; }
        protected DbSet<Empleados>? Empleados { get; set; }

        public virtual DbSet<T> ObtenerSet<T>() where T : class
        {
            return this.Set<T>();
        }

        public virtual List<T> Listar<T>() where T : class
        {
            return this.Set<T>().ToList();
        }

        public virtual List<T> Buscar<T>(Expression<Func<T, bool>> condiciones) where T : class
        {
            return this.Set<T>().Where(condiciones).ToList();
        }

        public virtual bool Existe<T>(Expression<Func<T, bool>> condiciones) where T : class
        {
            return this.Set<T>().Any(condiciones);
        }

        public virtual void Guardar<T>(T entidad) where T : class
        {
            this.Set<T>().Add(entidad);
        }

        public virtual void Modificar<T>(T entidad) where T : class
        {
            var entry = this.Entry(entidad);
            entry.State = EntityState.Modified;
        }

        public virtual void Borrar<T>(T entidad) where T : class
        {
            this.Set<T>().Remove(entidad);
        }

        public virtual void GuardarCambios()
        {
            this.SaveChanges();
        }
    }
}
