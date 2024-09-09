using System.ComponentModel.DataAnnotations;

namespace lib_entidades_dominio
{
    public class Tipos
    {
        [Key] public virtual int Id { get; set; }
        public virtual string? Nombre { get; set; }
        public virtual int Tabla { get; set; }
        public virtual int Accion { get; set; }

        //[NotMapped] public virtual ICollection<Facturas>? Facturas { get; set; }
    }
}

