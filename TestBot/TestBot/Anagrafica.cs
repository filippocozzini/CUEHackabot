using System;
namespace TestBot
{
    public class Anagrafica
    {
        int IDAssociato;
        int Matricola;
        String Nome;
        String Cognome;
        DateTime DataNascita;
        String Sesso;
        int CodiceComuneNascita;
        String ComuneNascita;
        String ProvinciaNascita;
        String NazioneNascita;
        String CodiceFiscale;
        String IndirizzoResidenza;
        String CivicoResidenza;
        String CodiceComuneResidenza;
        String ProvinciaResidenza;
        String NazioneResidenza;
        int CapResidenza;
        String LocalitaResidenza;
        int CodiceTitoloStudio;
        String CodiceProfessione;
        String CartaIdentita;
        DateTime ScadenzaCartaIdentita;
        String Passaporto;
        DateTime ScadenzaPassaporto;
        String CodiceGruppoSanguigno;
        Boolean Donatore;
        String Allergie;
        String Note;
        String VisitaMedica;
        Boolean Idoneo;
        DateTime ScadenzaIdoneita;
        Boolean Operativo;
        Boolean Stato;

        public Anagrafica()
        {
        }

        public Anagrafica(int IDAssociato, int Matricola, String Nome, String Cognome, DateTime DataNascita, String Sesso, int CodiceComuneNascita,
                          String ComuneNascita, String ProvinciaNascita, String NazioneNascita, String CodiceFiscale, String IndirizzoResidenza,
                          String CivicoResidenza, String CodiceComuneResidenza, String ProvinciaResidenza, String NazioneResidenza, int CapResidenza,
                          String LocalitaResidenza, int CodiceTitoloStudio, String CodiceProfessione, String CartaIdentita, DateTime ScadenzaCartaIdentita,
                          String Passaporto, DateTime ScadenzaPassaporto, String CodiceGruppoSanguigno, Boolean Donatore, String Allergie, String Note,
                          String VisitaMedica, Boolean Idoneo, DateTime ScadenzaIdoneita, Boolean Operativo, Boolean Stato){

            this.IDAssociato = IDAssociato;
            this.Matricola = Matricola;
            this.Nome = Nome;
            this.Cognome = Cognome;
            this.DataNascita = DataNascita;
            this.Sesso = Sesso;
            this.CodiceComuneNascita = CodiceComuneNascita;
            this.ComuneNascita = ComuneNascita;
            this.ProvinciaNascita = ProvinciaNascita;
            this.NazioneNascita = NazioneNascita;
            this.CodiceFiscale = CodiceFiscale;
            this.IndirizzoResidenza = IndirizzoResidenza;
            this.CivicoResidenza = CivicoResidenza;
            this.CodiceComuneResidenza = CodiceComuneResidenza;
            this.ProvinciaResidenza = ProvinciaResidenza;
            this.NazioneResidenza = NazioneResidenza;
            this.CapResidenza = CapResidenza;
            this.LocalitaResidenza = LocalitaResidenza;
            this.CodiceTitoloStudio = CodiceTitoloStudio;
            this.CodiceProfessione = CodiceProfessione;
            this.CartaIdentita = CartaIdentita;
            this.ScadenzaCartaIdentita = ScadenzaCartaIdentita;
            this.Passaporto = Passaporto;
            this.ScadenzaPassaporto = ScadenzaPassaporto;
            this.CodiceGruppoSanguigno = CodiceGruppoSanguigno;
            this.Donatore = Donatore;
            this.Allergie = Allergie;
            this.Note = Note;
            this.VisitaMedica = VisitaMedica;
            this.Idoneo = Idoneo;
            this.ScadenzaIdoneita = ScadenzaIdoneita;
            this.Operativo = Operativo;
            this.Stato = Stato;
        }

		public int getIDAssociato()
		{
			return IDAssociato;
		}

		public void setIDAssociato(int IDAssociato)
		{
			this.IDAssociato = IDAssociato;
		}

		public int getMatricola()
		{
			return Matricola;
		}

		public void setMatricola(int matricola)
		{
			Matricola = matricola;
		}

		public String getNome()
		{
			return Nome;
		}

		public void setNome(String nome)
		{
			Nome = nome;
		}

		public String getCognome()
		{
			return Cognome;
		}

		public void setCognome(String cognome)
		{
			Cognome = cognome;
		}

		public DateTime getDataNascita()
		{
			return DataNascita;
		}

		public void setDataNascita(DateTime dataNascita)
		{
			DataNascita = dataNascita;
		}

		public String getSesso()
		{
			return Sesso;
		}

		public void setSesso(String sesso)
		{
			Sesso = sesso;
		}

		public int getCodiceComuneNascita()
		{
			return CodiceComuneNascita;
		}

		public void setCodiceComuneNascita(int codiceComuneNascita)
		{
			CodiceComuneNascita = codiceComuneNascita;
		}

		public String getComuneNascita()
		{
			return ComuneNascita;
		}

		public void setComuneNascita(String comuneNascita)
		{
			ComuneNascita = comuneNascita;
		}

		public String getProvinciaNascita()
		{
			return ProvinciaNascita;
		}

		public void setProvinciaNascita(String provinciaNascita)
		{
			ProvinciaNascita = provinciaNascita;
		}

		public String getNazioneNascita()
		{
			return NazioneNascita;
		}

		public void setNazioneNascita(String nazioneNascita)
		{
			NazioneNascita = nazioneNascita;
		}

		public String getCodiceFiscale()
		{
			return CodiceFiscale;
		}

		public void setCodiceFiscale(String codiceFiscale)
		{
			CodiceFiscale = codiceFiscale;
		}

		public String getIndirizzoResidenza()
		{
			return IndirizzoResidenza;
		}

		public void setIndirizzoResidenza(String indirizzoResidenza)
		{
			IndirizzoResidenza = indirizzoResidenza;
		}

		public String getCivicoResidenza()
		{
			return CivicoResidenza;
		}

		public void setCivicoResidenza(String civicoResidenza)
		{
			CivicoResidenza = civicoResidenza;
		}

		public String getCodiceComuneResidenza()
		{
			return CodiceComuneResidenza;
		}

		public void setCodiceComuneResidenza(String codiceComuneResidenza)
		{
			CodiceComuneResidenza = codiceComuneResidenza;
		}

		public String getProvinciaResidenza()
		{
			return ProvinciaResidenza;
		}

		public void setProvinciaResidenza(String provinciaResidenza)
		{
			ProvinciaResidenza = provinciaResidenza;
		}

		public String getNazioneResidenza()
		{
			return NazioneResidenza;
		}

		public void setNazioneResidenza(String nazioneResidenza)
		{
			NazioneResidenza = nazioneResidenza;
		}

		public int getCapResidenza()
		{
			return CapResidenza;
		}

		public void setCapResidenza(int capResidenza)
		{
			CapResidenza = capResidenza;
		}

		public String getLocalitaResidenza()
		{
			return LocalitaResidenza;
		}

		public void setLocalitaResidenza(String localitaResidenza)
		{
			LocalitaResidenza = localitaResidenza;
		}

		public int getCodiceTitoloStudio()
		{
			return CodiceTitoloStudio;
		}

		public void setCodiceTitoloStudio(int codiceTitoloStudio)
		{
			CodiceTitoloStudio = codiceTitoloStudio;
		}

		public String getCodiceProfessione()
		{
			return CodiceProfessione;
		}

		public void setCodiceProfessione(String codiceProfessione)
		{
			CodiceProfessione = codiceProfessione;
		}

		public String getCartaIdentita()
		{
			return CartaIdentita;
		}

		public void setCartaIdentita(String cartaIdentita)
		{
			CartaIdentita = cartaIdentita;
		}

		public DateTime getScadenzaCartaIdentita()
		{
			return ScadenzaCartaIdentita;
		}

		public void setScadenzaCartaIdentita(DateTime scadenzaCartaIdentita)
		{
			ScadenzaCartaIdentita = scadenzaCartaIdentita;
		}

		public String getPassaporto()
		{
			return Passaporto;
		}

		public void setPassaporto(String passaporto)
		{
			Passaporto = passaporto;
		}

		public DateTime getScadenzaPassaporto()
		{
			return ScadenzaPassaporto;
		}

		public void setScadenzaPassaporto(DateTime scadenzaPassaporto)
		{
			ScadenzaPassaporto = scadenzaPassaporto;
		}

		public String getCodiceGruppoSanguigno()
		{
			return CodiceGruppoSanguigno;
		}

		public void setCodiceGruppoSanguigno(String codiceGruppoSanguigno)
		{
			CodiceGruppoSanguigno = codiceGruppoSanguigno;
		}

		public Boolean getDonatore()
		{
			return Donatore;
		}

		public void setDonatore(Boolean donatore)
		{
			Donatore = donatore;
		}

		public String getAllergie()
		{
			return Allergie;
		}

		public void setAllergie(String allergie)
		{
			Allergie = allergie;
		}

		public String getNote()
		{
			return Note;
		}

		public void setNote(String note)
		{
			Note = note;
		}

		public String getVisitaMedica()
		{
			return VisitaMedica;
		}

		public void setVisitaMedica(String visitaMedica)
		{
			VisitaMedica = visitaMedica;
		}

		public Boolean getIdoneo()
		{
			return Idoneo;
		}

		public void setIdoneo(Boolean idoneo)
		{
			Idoneo = idoneo;
		}

		public DateTime getScadenzaIdoneita()
		{
			return ScadenzaIdoneita;
		}

		public void setScadenzaIdoneita(DateTime scadenzaIdoneita)
		{
			ScadenzaIdoneita = scadenzaIdoneita;
		}

		public Boolean getOperativo()
		{
			return Operativo;
		}

		public void setOperativo(Boolean operativo)
		{
			Operativo = operativo;
		}

		public Boolean getStato()
		{
			return Stato;
		}

		public void setStato(Boolean stato)
		{
			Stato = stato;
		}
    }
}
