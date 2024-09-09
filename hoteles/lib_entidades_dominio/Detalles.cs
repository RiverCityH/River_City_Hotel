using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lib_entidades_dominio
{
    public class Detalles
    {
        [Key] public virtual int Id { get; set; }
        public virtual int Factura { get; set; }
        public virtual int Producto { get; set; }
        public virtual decimal Valor { get; set; }
        public virtual decimal Cantidad { get; set; }
        public virtual decimal Total { get; set; }
        [ForeignKey("Factura")] public virtual Facturas? _Factura { get; set; }
        [ForeignKey("Producto")] public virtual Productos? _Producto { get; set; }

    }
}
