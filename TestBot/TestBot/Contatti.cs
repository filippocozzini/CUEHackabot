using System;
namespace TestBot
{
    public class Contatti
    {
        int IDAssociato;
        int TelefonoAbitazione;
        int TelefonoUfficio;
        int TelefonoCellulare;
        int TelefonoServizio;
        int TelefonoAltroNumero;
        String MailPrincipale;
        String MailSecondaria;
        String MailAltroIndirizzo;
        String PEC;
        String Tetra;
        int FaxPrincipale;
        int FaxSecondario;
        String Skype;
        String Twitter;


        public Contatti()
        {
        }

		public Contatti(int IDAssociato, int telefonoAbitazione, int telefonoUfficio, int telefonoCellulare, int telefonoServizio, int telefonoAltroNumero, String mailPrincipale, String mailSecondaria, String mailAltroIndirizzo, String PEC, String tetra, int faxPrincipale, int faxSecondario, String skype, String twitter)
		{
			this.IDAssociato = IDAssociato;
			TelefonoAbitazione = telefonoAbitazione;
			TelefonoUfficio = telefonoUfficio;
			TelefonoCellulare = telefonoCellulare;
			TelefonoServizio = telefonoServizio;
			TelefonoAltroNumero = telefonoAltroNumero;
			MailPrincipale = mailPrincipale;
			MailSecondaria = mailSecondaria;
			MailAltroIndirizzo = mailAltroIndirizzo;
			this.PEC = PEC;
			Tetra = tetra;
			FaxPrincipale = faxPrincipale;
			FaxSecondario = faxSecondario;
			Skype = skype;
			Twitter = twitter;
		}

		public int getIDAssociato()
		{
			return IDAssociato;
		}

		public void setIDAssociato(int IDAssociato)
		{
			this.IDAssociato = IDAssociato;
		}

		public int getTelefonoAbitazione()
		{
			return TelefonoAbitazione;
		}

		public void setTelefonoAbitazione(int telefonoAbitazione)
		{
			TelefonoAbitazione = telefonoAbitazione;
		}

		public int getTelefonoUfficio()
		{
			return TelefonoUfficio;
		}

		public void setTelefonoUfficio(int telefonoUfficio)
		{
			TelefonoUfficio = telefonoUfficio;
		}

		public int getTelefonoCellulare()
		{
			return TelefonoCellulare;
		}

		public void setTelefonoCellulare(int telefonoCellulare)
		{
			TelefonoCellulare = telefonoCellulare;
		}

		public int getTelefonoServizio()
		{
			return TelefonoServizio;
		}

		public void setTelefonoServizio(int telefonoServizio)
		{
			TelefonoServizio = telefonoServizio;
		}

		public int getTelefonoAltroNumero()
		{
			return TelefonoAltroNumero;
		}

		public void setTelefonoAltroNumero(int telefonoAltroNumero)
		{
			TelefonoAltroNumero = telefonoAltroNumero;
		}

		public String getMailPrincipale()
		{
			return MailPrincipale;
		}

		public void setMailPrincipale(String mailPrincipale)
		{
			MailPrincipale = mailPrincipale;
		}

		public String getMailSecondaria()
		{
			return MailSecondaria;
		}

		public void setMailSecondaria(String mailSecondaria)
		{
			MailSecondaria = mailSecondaria;
		}

		public String getMailAltroIndirizzo()
		{
			return MailAltroIndirizzo;
		}

		public void setMailAltroIndirizzo(String mailAltroIndirizzo)
		{
			MailAltroIndirizzo = mailAltroIndirizzo;
		}

		public String getPEC()
		{
			return PEC;
		}

		public void setPEC(String PEC)
		{
			this.PEC = PEC;
		}

		public String getTetra()
		{
			return Tetra;
		}

		public void setTetra(String tetra)
		{
			Tetra = tetra;
		}

		public int getFaxPrincipale()
		{
			return FaxPrincipale;
		}

		public void setFaxPrincipale(int faxPrincipale)
		{
			FaxPrincipale = faxPrincipale;
		}

		public int getFaxSecondario()
		{
			return FaxSecondario;
		}

		public void setFaxSecondario(int faxSecondario)
		{
			FaxSecondario = faxSecondario;
		}

		public String getSkype()
		{
			return Skype;
		}

		public void setSkype(String skype)
		{
			Skype = skype;
		}

		public String getTwitter()
		{
			return Twitter;
		}

		public void setTwitter(String twitter)
		{
			Twitter = twitter;
		}
    }
}
