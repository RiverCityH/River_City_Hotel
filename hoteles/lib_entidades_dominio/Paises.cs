using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace lib_entidades_dominio
{
    public class Paises
    {
        [Key] public virtual int Id { get; set; }
        public virtual string? Nombre { get; set; }

        [NotMapped] public virtual ICollection<Departamentos>? Departamentos { get; set; }
    }
}