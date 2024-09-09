using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace lib_entidades_dominio
{
    public class Personas
    {        
        [Key] public virtual int Id { get; set; }
        public virtual int TipoDocumento { get; set; }
        public virtual string? Documento { get; set; }
        public virtual string? Nombre { get; set; }
        public virtual DateTime FechaNacimiento { get; set; }
        public virtual string? Celular { get; set; }
        public virtual int Genero { get; set; }
        public virtual string? Direccion { get; set; }
        public virtual string? Email { get; set; }
        public virtual string? Contraseña { get; set; }
        public virtual bool Confirmar { get; set; }
        public virtual bool Restablecer { get; set; }
        public virtual string? Token { get; set; }
        public virtual int Ciudad { get; set; }
        public virtual bool Activo { get; set; }

        [ForeignKey("Ciudad")] public virtual Ciudades? _Ciudad { get; set; }
        [ForeignKey("TipoDocumento")] public virtual Tipos? _TipoDocumento { get; set; }
        [ForeignKey("Genero")] public virtual Tipos? _Genero { get; set; }

        [NotMapped] public virtual ICollection<Empleados>? Empleados { get; set; }
    }
}