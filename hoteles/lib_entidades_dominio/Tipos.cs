using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lib_entidades_dominio
{
    internal class Tipos
    {
        [Key] public virtual int Id { get; set; }
        public virtual string? Nombre { get; set; }
        public virtual int Tabla { get; set; }
        public virtual int Accion { get; set; }

        //[NotMapped] public virtual ICollection<Facturas>? Facturas { get; set; }
    }
}

