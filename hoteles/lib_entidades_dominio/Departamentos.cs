using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace lib_entidades_dominio
{
    public class Departamentos
    {
        [Key] public virtual int Id { get; set; }
        public virtual string? Nombre { get; set; }
        public virtual int Pais { get; set; }

        [ForeignKey("Pais")] public virtual Paises? _Pais { get; set; }
        [NotMapped] public virtual ICollection<Ciudades>? Ciudades { get; set; }
    }
}