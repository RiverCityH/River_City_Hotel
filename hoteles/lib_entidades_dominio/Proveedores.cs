using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace lib_entidades_dominio
{
    public class Proveedores
    {
        [Key] public virtual int Id { get; set; }
        public virtual int TipoDocumento { get; set; }
        public virtual string? Documento { get; set; }
        public virtual string? Nombre { get; set; }
        public virtual string? Celular { get; set; }
        public virtual string? Email { get; set; }
        public virtual string? Dirección { get; set; }
        public virtual int Ciudad { get; set; }

        [ForeignKey("Ciudad")] public virtual Ciudades? _Ciudad { get; set; }
        [ForeignKey("TipoDocumento")] public virtual Tipos? _TipoDocumento { get; set; }
        [NotMapped] public virtual ICollection<Productos>? Productos { get; set; }
    }
}
