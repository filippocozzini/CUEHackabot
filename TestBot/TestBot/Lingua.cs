using System;
namespace TestBot
{
    public class Lingua
    {
        int IDAssociato;
        String Lingua1;
        String LivelloLingua1;
        String Lingua2;
        String LivelloLingua2;
        String Lingua3;
        String LivelloLingua3;
        String Lingua4;
        String LivelloLingua4;
        String Lingua5;
        String LivelloLingua5;

        public Lingua()
        {
        }

		public Lingua(int IDAssociato, String lingua1, String livelloLingua1, String lingua2, String livelloLingua2, String lingua3, String livelloLingua3, String lingua4, String livelloLingua4, String lingua5, String livelloLingua5)
		{
			this.IDAssociato = IDAssociato;
			Lingua1 = lingua1;
			LivelloLingua1 = livelloLingua1;
			Lingua2 = lingua2;
			LivelloLingua2 = livelloLingua2;
			Lingua3 = lingua3;
			LivelloLingua3 = livelloLingua3;
			Lingua4 = lingua4;
			LivelloLingua4 = livelloLingua4;
			Lingua5 = lingua5;
			LivelloLingua5 = livelloLingua5;
		}

		public int getIDAssociato()
		{
			return IDAssociato;
		}

		public void setIDAssociato(int IDAssociato)
		{
			this.IDAssociato = IDAssociato;
		}

		public String getLingua1()
		{
			return Lingua1;
		}

		public void setLingua1(String lingua1)
		{
			Lingua1 = lingua1;
		}

		public String getLivelloLingua1()
		{
			return LivelloLingua1;
		}

		public void setLivelloLingua1(String livelloLingua1)
		{
			LivelloLingua1 = livelloLingua1;
		}

		public String getLingua2()
		{
			return Lingua2;
		}

		public void setLingua2(String lingua2)
		{
			Lingua2 = lingua2;
		}

		public String getLivelloLingua2()
		{
			return LivelloLingua2;
		}

		public void setLivelloLingua2(String livelloLingua2)
		{
			LivelloLingua2 = livelloLingua2;
		}

		public String getLingua3()
		{
			return Lingua3;
		}

		public void setLingua3(String lingua3)
		{
			Lingua3 = lingua3;
		}

		public String getLivelloLingua3()
		{
			return LivelloLingua3;
		}

		public void setLivelloLingua3(String livelloLingua3)
		{
			LivelloLingua3 = livelloLingua3;
		}

		public String getLingua4()
		{
			return Lingua4;
		}

		public void setLingua4(String lingua4)
		{
			Lingua4 = lingua4;
		}

		public String getLivelloLingua4()
		{
			return LivelloLingua4;
		}

		public void setLivelloLingua4(String livelloLingua4)
		{
			LivelloLingua4 = livelloLingua4;
		}

		public String getLingua5()
		{
			return Lingua5;
		}

		public void setLingua5(String lingua5)
		{
			Lingua5 = lingua5;
		}

		public String getLivelloLingua5()
		{
			return LivelloLingua5;
		}

		public void setLivelloLingua5(String livelloLingua5)
		{
			LivelloLingua5 = livelloLingua5;
		}
    }
}
