using System.Collections.Generic;

namespace CueBoT
{
    public class CreaPunto
    {
        public string Nome { get; set; }
        public float Latitudine { get; set; }
        public float Longitudine { get; set; }
        public List<string> Volontari { get; set; }
        public bool Aperto { get; set; } //Se il punto di controllo è aperto true altrimento false
    }
}