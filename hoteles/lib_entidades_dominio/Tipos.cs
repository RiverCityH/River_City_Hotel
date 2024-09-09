﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace lib_entidades_dominio
{
    public class Tipos
    {
        [Key] public virtual int Id { get; set; }
        public virtual string? Nombre { get; set; }
        public virtual string? Tabla { get; set; }
        public virtual int Accion { get; set; }

    }
}