using System;
namespace TestBot
{
    public class Professione
    {
        int IDAssociato;
        int TipoLavoro;
        String CodiceBenefici;
        String DatoreLavoro;
        String Responsabile;
        String SedeLegale;
        String SedeLavoro;
        String IndirizzoLavoro;
        String LavoroSedentario;
        int CodiceMansione;
        String DescrizioneMansione;
        String Esperienze;
        String NumeroIscrizioneAlbo;
        DateTime DataIscrizioneAlbo;

        public Professione()
        {
        }

		public int getIDAssociato()
		{
			return IDAssociato;
		}

		public void setIDAssociato(int IDAssociato)
		{
			this.IDAssociato = IDAssociato;
		}

		public int getTipoLavoro()
		{
			return TipoLavoro;
		}

		public void setTipoLavoro(int tipoLavoro)
		{
			TipoLavoro = tipoLavoro;
		}

		public String getCodiceBenefici()
		{
			return CodiceBenefici;
		}

		public void setCodiceBenefici(String codiceBenefici)
		{
			CodiceBenefici = codiceBenefici;
		}

		public String getDatoreLavoro()
		{
			return DatoreLavoro;
		}

		public void setDatoreLavoro(String datoreLavoro)
		{
			DatoreLavoro = datoreLavoro;
		}

		public String getResponsabile()
		{
			return Responsabile;
		}

		public void setResponsabile(String responsabile)
		{
			Responsabile = responsabile;
		}

		public String getSedeLegale()
		{
			return SedeLegale;
		}

		public void setSedeLegale(String sedeLegale)
		{
			SedeLegale = sedeLegale;
		}

		public String getSedeLavoro()
		{
			return SedeLavoro;
		}

		public void setSedeLavoro(String sedeLavoro)
		{
			SedeLavoro = sedeLavoro;
		}

		public String getIndirizzoLavoro()
		{
			return IndirizzoLavoro;
		}

		public void setIndirizzoLavoro(String indirizzoLavoro)
		{
			IndirizzoLavoro = indirizzoLavoro;
		}

		public String getLavoroSedentario()
		{
			return LavoroSedentario;
		}

		public void setLavoroSedentario(String lavoroSedentario)
		{
			LavoroSedentario = lavoroSedentario;
		}

		public int getCodiceMansione()
		{
			return CodiceMansione;
		}

		public void setCodiceMansione(int codiceMansione)
		{
			CodiceMansione = codiceMansione;
		}

		public String getDescrizioneMansione()
		{
			return DescrizioneMansione;
		}

		public void setDescrizioneMansione(String descrizioneMansione)
		{
			DescrizioneMansione = descrizioneMansione;
		}

		public String getEsperienze()
		{
			return Esperienze;
		}

		public void setEsperienze(String esperienze)
		{
			Esperienze = esperienze;
		}

		public String getNumeroIscrizioneAlbo()
		{
			return NumeroIscrizioneAlbo;
		}

		public void setNumeroIscrizioneAlbo(String numeroIscrizioneAlbo)
		{
			NumeroIscrizioneAlbo = numeroIscrizioneAlbo;
		}

		public DateTime getDataIscrizioneAlbo()
		{
			return DataIscrizioneAlbo;
		}

		public void setDataIscrizioneAlbo(DateTime dataIscrizioneAlbo)
		{
			DataIscrizioneAlbo = dataIscrizioneAlbo;
		}
    }
}
