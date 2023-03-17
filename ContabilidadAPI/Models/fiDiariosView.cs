using System;
using System.Collections.Generic;

namespace ContabilidadAPI.Models
{
    public partial class fiDiariosView
    {
        public fiDiarios Encabezado { get; set; }
        public List<FiDiariosDetalle> Detalle { get; set; }
    }
}
