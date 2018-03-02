using System;
namespace TestBot
{
    public class Associazione
    {
        int IDAssociato;
        String CodiceAssociazione;
        int CodiceTerritorio;
        String DescrizioneTerritorio;
        int IDInternoTerritorio;
        int CodiceSede;
        String CodiceRuolo;
        String DescrizioneRuolo;
        int IDInternoRuolo;
        DateTime DataAdesione;
        DateTime UltimaModifica;

        public Associazione() { }

        public Associazione(int IDAssociato, String CodiceAssociazione, int CodiceTerritorio, String DescrizioneTerritorio, 
                            int IDInternoTerritorio, int CodiceSede, String CodiceRuolo, String DescrizioneRuolo, 
                            int IDInternoRuolo, DateTime DataAdesione, DateTime UltimaModifica){
            this.IDAssociato = IDAssociato;
            this.CodiceAssociazione = CodiceAssociazione;
            this.CodiceTerritorio = CodiceTerritorio;
            this.DescrizioneTerritorio = DescrizioneTerritorio;
            this.IDInternoTerritorio = IDInternoTerritorio;
            this.CodiceSede = CodiceSede;
            this.CodiceRuolo = CodiceRuolo;
            this.DescrizioneRuolo = DescrizioneRuolo;
            this.IDInternoRuolo = IDInternoRuolo;
            this.DataAdesione = DataAdesione;
            this.UltimaModifica = UltimaModifica;
            //Valutare se importare la data di "DataAdesione" e "UltimaModifica" in Stringa e convirtire qui in DateTime oppure fuori dalla classe
        }


        public int getIDAssociato() {
            return IDAssociato;
        }

        public void setIDAssociato(int v) {
            IDAssociato = v;
        }


        public String getCodiceAssociazione() {
            return CodiceAssociazione;
        }

        public void setCodiceAssociazione(String v){
            CodiceAssociazione = v;
        }


        public int getCodiceTerritorio() {
            return CodiceTerritorio;
        }

        public void setCodiceTerritorio(int v) {
            CodiceTerritorio = v;
        }


        public String getDescrizioneTerritorio() {
            return DescrizioneTerritorio;
        }

        public void setDescrizioneTerritorio(String v){
            DescrizioneTerritorio = v;
        }


        public int getIDInternoTerritorio() {
            return IDInternoTerritorio;
        }

        public void setIDInternoTerritorio(int v){
            IDInternoTerritorio = v;
        }


        public int getCodiceSede() {
            return CodiceSede;
        }

        public void setCodiceSede(int v){
            CodiceSede = v;
        }


        public String getCodiceRuolo() {
            return CodiceRuolo;
        }
        public void setCodiceRuolo(String v){
            CodiceRuolo = v;
        }


        public String getDescrizioneRuolo() {
            return DescrizioneRuolo;
        }

        public void setDescrizioneRuolo(String v){
            DescrizioneRuolo = v;
        }


        public int getIDInternoRuolo() {
            return IDInternoRuolo;
        }

        public void setIDInternoRuolo(int v){
            IDInternoRuolo = v;
        }


        public DateTime getDataAdesione() {
            return DataAdesione;
        }

        public void setDataAdesione(DateTime v){
            DataAdesione = v;
        }


        public DateTime getUltimaModifica() {
            return UltimaModifica;
        }

        public void setUltimaModifica(DateTime v){
            UltimaModifica = v;
        }
    }
}
