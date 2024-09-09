using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace lib_entidades_dominio
{
    public class Empleados
    {
        [Key] public virtual int Id { get; set; }
        public virtual int Persona { get; set; }
        public virtual int Cargo { get; set; }
        public virtual int ARL { get; set; }
        public virtual int Pension { get; set; }
        public virtual int EPS { get; set; }
        public virtual int TipoSangre { get; set; }
        public virtual int EstadoCivil { get; set; }

        [ForeignKey("Persona")] public virtual Personas? _Persona { get; set; }
        [ForeignKey("Cargo")] public virtual Tipos? _Cargo { get; set; }
        [ForeignKey("ARL")] public virtual Tipos? _ARL { get; set; }
        [ForeignKey("Pension")] public virtual Tipos? _Pension { get; set; }
        [ForeignKey("EPS")] public virtual Tipos? _EPS { get; set; }
        [ForeignKey("TipoSangre")] public virtual Tipos? _TipoSangre { get; set; }
        [ForeignKey("EstadoCivil")] public virtual Tipos? _EstadoCivil { get; set; }
    }
}
