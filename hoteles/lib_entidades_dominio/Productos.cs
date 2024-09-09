using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlTypes;

namespace lib_entidades_dominio
{
    public class Productos
    {
        [Key] public virtual int Id { get; set; }
        public virtual string? Codigo { get; set; }
        public virtual string? Nombre { get; set; }
        public virtual decimal Valor { get; set; }
        public virtual decimal Costo { get; set; }
        public virtual decimal Cantidad { get; set; }
        public virtual DateTime? FechaIngreso { get; set; }
        public virtual DateTime? FechaVencimiento { get; set; }
        public virtual string? Lote { get; set; }
        public virtual int Categoria { get; set; }
        public virtual int Proveedor { get; set; }


        [ForeignKey("Categoria")] public virtual Tipos? _Categoria { get; set; }
        [ForeignKey("Proveedor")] public virtual Proveedores? _Proveedor { get; set; }
    }
}
