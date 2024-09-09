using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lib_entidades_dominio
{
    public class Facturas
    {
        [Key] public virtual int Id { get; set; }
        public virtual string? Numero { get; set; }
        public virtual int Persona { get; set; }
        public virtual DateTime Fecha { get; set; }
        public virtual decimal Total { get; set; }
        public virtual int MetodoPago { get; set; }
        public virtual int Tipo { get; set; }
        public virtual bool Activo { get; set; }
        [ForeignKey("Persona")] public virtual Personas? _Persona { get; set; }
        [ForeignKey("MetodoPago")] public virtual Tipos? _MetodoPago { get; set; }
        [ForeignKey("Tipo")] public virtual Tipos? _Tipo { get; set; }
        [NotMapped] public virtual ICollection<Detalles>? Detalles { get; set; }
    }
}
