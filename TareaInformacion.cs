using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generador
{
    public class TareaInformacion
    {
        public string Nombre { get; set; }
        public string Id { get; set; }
        public string TipoEntrega { get; set; }
        public string Nivel { get; set; }
        public List<string> Imagenes { get; set; }
    }

}
