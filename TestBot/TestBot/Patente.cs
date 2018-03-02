using System;
namespace TestBot
{
    public class Patente
    {
        int IDAssociato;
        Boolean PatenteA;
        Boolean PatenteB;
        Boolean PatenteC;
        Boolean PatenteD;
        Boolean PatenteE;
        String NumeroPatente;
        DateTime DataRilascioPatente;
        DateTime DataScadenzaPatente;
        String Motorizzazione;
        Boolean PatenteCRI1;
        Boolean PatenteCRI2;
        Boolean PatenteCRI3;
        Boolean PatenteCRI4;
        Boolean PatenteCRI5;
        Boolean PatenteCRI5B;
        Boolean PatenteCRI6;

        public Patente()
        {
        }

		public Patente(int IDAssociato, Boolean patenteA, Boolean patenteB, Boolean patenteC, Boolean patenteD, Boolean patenteE, String numeroPatente, Date dataRilascioPatente, Date dataScadenzaPatente, String motorizzazione, Boolean patenteCRI1, Boolean patenteCRI2, Boolean patenteCRI3, Boolean patenteCRI4, Boolean patenteCRI5, Boolean patenteCRI5B, Boolean patenteCRI6)
		{
			this.IDAssociato = IDAssociato;
			PatenteA = patenteA;
			PatenteB = patenteB;
			PatenteC = patenteC;
			PatenteD = patenteD;
			PatenteE = patenteE;
			NumeroPatente = numeroPatente;
			DataRilascioPatente = dataRilascioPatente;
			DataScadenzaPatente = dataScadenzaPatente;
			Motorizzazione = motorizzazione;
			PatenteCRI1 = patenteCRI1;
			PatenteCRI2 = patenteCRI2;
			PatenteCRI3 = patenteCRI3;
			PatenteCRI4 = patenteCRI4;
			PatenteCRI5 = patenteCRI5;
			PatenteCRI5B = patenteCRI5B;
			PatenteCRI6 = patenteCRI6;
		}

		public int getIDAssociato()
		{
			return IDAssociato;
		}

		public void setIDAssociato(int IDAssociato)
		{
			this.IDAssociato = IDAssociato;
		}

		public Boolean getPatenteA()
		{
			return PatenteA;
		}

		public void setPatenteA(Boolean patenteA)
		{
			PatenteA = patenteA;
		}

		public Boolean getPatenteB()
		{
			return PatenteB;
		}

		public void setPatenteB(Boolean patenteB)
		{
			PatenteB = patenteB;
		}

		public Boolean getPatenteC()
		{
			return PatenteC;
		}

		public void setPatenteC(Boolean patenteC)
		{
			PatenteC = patenteC;
		}

		public Boolean getPatenteD()
		{
			return PatenteD;
		}

		public void setPatenteD(Boolean patenteD)
		{
			PatenteD = patenteD;
		}

		public Boolean getPatenteE()
		{
			return PatenteE;
		}

		public void setPatenteE(Boolean patenteE)
		{
			PatenteE = patenteE;
		}

		public String getNumeroPatente()
		{
			return NumeroPatente;
		}

		public void setNumeroPatente(String numeroPatente)
		{
			NumeroPatente = numeroPatente;
		}

		public DateTime getDataRilascioPatente()
		{
			return DataRilascioPatente;
		}

		public void setDataRilascioPatente(DateTime dataRilascioPatente)
		{
			DataRilascioPatente = dataRilascioPatente;
		}

		public DateTime getDataScadenzaPatente()
		{
			return DataScadenzaPatente;
		}

		public void setDataScadenzaPatente(DateTime dataScadenzaPatente)
		{
			DataScadenzaPatente = dataScadenzaPatente;
		}

		public String getMotorizzazione()
		{
			return Motorizzazione;
		}

		public void setMotorizzazione(String motorizzazione)
		{
			Motorizzazione = motorizzazione;
		}

		public Boolean getPatenteCRI1()
		{
			return PatenteCRI1;
		}

		public void setPatenteCRI1(Boolean patenteCRI1)
		{
			PatenteCRI1 = patenteCRI1;
		}

		public Boolean getPatenteCRI2()
		{
			return PatenteCRI2;
		}

		public void setPatenteCRI2(Boolean patenteCRI2)
		{
			PatenteCRI2 = patenteCRI2;
		}

		public Boolean getPatenteCRI3()
		{
			return PatenteCRI3;
		}

		public void setPatenteCRI3(Boolean patenteCRI3)
		{
			PatenteCRI3 = patenteCRI3;
		}

		public Boolean getPatenteCRI4()
		{
			return PatenteCRI4;
		}

		public void setPatenteCRI4(Boolean patenteCRI4)
		{
			PatenteCRI4 = patenteCRI4;
		}

		public Boolean getPatenteCRI5()
		{
			return PatenteCRI5;
		}

		public void setPatenteCRI5(Boolean patenteCRI5)
		{
			PatenteCRI5 = patenteCRI5;
		}

		public Boolean getPatenteCRI5B()
		{
			return PatenteCRI5B;
		}

		public void setPatenteCRI5B(Boolean patenteCRI5B)
		{
			PatenteCRI5B = patenteCRI5B;
		}

		public Boolean getPatenteCRI6()
		{
			return PatenteCRI6;
		}

		public void setPatenteCRI6(Boolean patenteCRI6)
		{
			PatenteCRI6 = patenteCRI6;
		}
    }
}
