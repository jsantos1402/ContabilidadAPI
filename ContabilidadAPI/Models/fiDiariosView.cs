using System;
using System.Collections.Generic;

namespace ContabilidadAPI.Models
{
    public partial class fiDiariosView
    {
        public fiDiarios Encabezado { get; set; }
        public List<fiDiariosDetalle> Detalle { get; set; }
    }
}
