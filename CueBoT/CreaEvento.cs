using System;
using System.Collections.Generic;

namespace CueBoT
{
    public class CreaEvento
    {
        public string Nome { get; set; }
        public string Descrizione { get; set; }
        public float Latitudine { get; set; }
        public float Longitudine { get; set; }
        public DateTime DataOraInizio { get; set; }
        public DateTime? DataOraFine { get; set; }
        public List<string> Responsabili { get; set; } //id telegram dei responsabili
        public bool Privato { get; set; }
    }


}