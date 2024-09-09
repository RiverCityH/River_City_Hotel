using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace lib_entidades_dominio
{
    public class Ciudades
    {
        [Key] public virtual int Id { get; set; }
        public virtual string? Nombre { get; set; }
        public virtual int Departamento { get; set; }

        [ForeignKey("Departamento")] public virtual Departamentos? _Departamentos { get; set; }
        [NotMapped] public virtual ICollection<Proveedores>? Proveedores { get; set; }

    }
}