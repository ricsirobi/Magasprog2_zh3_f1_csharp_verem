using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DinamikusVerem
{
    class Program
    {
        /*A számítástechnikában a verem egy LIFO adatszerkezet, amelyben általában véges számú azonos típusú (méretű) adatot
        lehet tárolni. A LIFO azt jelenti, hogy az utoljára a verembe helyezett elemhez férünk hozzá először.
        Készítse el a Verem generikus osztályt, amely típusparaméterben kapja meg a tárolni kívánt elemek adattípusát. Az
        osztály adattároló mezője egy generikus típusú tömb legyen, amely méretét a konstruktorban kell megadni, amennyiben
        a konstruktor nem kapja meg a méretet, akkor a méte alapértelmezés szerint 10 legyen. Az osztály az alábbiakat
        implementálja:

        írta: tc92hl Ács Róbert

        --
        5-ös érdemjegyért:
         Az verem tud intelligens módon is működni, ehhez készítsen egy IntelligenseTesz() metódust, amely
        következtében a verem sosem telik be, ha betelne, akkor dinamikusan növeljük a tároló tömb méretét (újra
        deklarálás, elemek másolása).
         A verem osztály tartalmaz olyan eseményt, amelyre feliratkozva értesítést lehet kapni, ha egy nem intelligens
        verem 80%-ban betelt.*/
        class Verem<Object>
        {
            int tipus;
            private uint meret; //Meret: metódus vagy virtuális mező, amely visszaadja a veremben tárolt elemek számát.
            public Object[] vermem;
            private Exception kiakad = new Exception("Nem tudom megfordítani mivel 0 a méret");
            private uint szamlalo;
            public uint Meret
            {
                get
                {
                    return meret;
                }
            }
            public bool Ures
            {
                get { if (meret == 0) return true; else return false; }
                //Ures: metódus vagy virtuális mező, amely visszaadja, hogy a verem üres-e vagy sem 
                //(akkor üres, ha 0 elem van benne)
            }
            public Verem(uint meret = 10)
            {
                this.szamlalo = 0;
                vermem = new Object[meret];
                this.meret = meret;
            }
            private void Bovit()
            {
                Object[] temp = this.vermem;
                vermem = new Object[this.vermem.Length + 1];
                for (int i = 0; i < temp.Length; i++)
                {
                    vermem[i] = temp[i];
                }
                this.meret = (uint)temp.Length + 1;
            }
            public void Rarak(Object o)
            {
                // Rarak(elem): adott elem elhelyezése a verem tetején. 
                // Ha egy teli verembe akarunk új elemet rakni, akkor hibát kell adni.
                if (Meret == 0)
                {
                    throw kiakad;
                }
                else if (szamlalo == meret)
                {
                    Bovit();
                    //throw new IndexOutOfRangeException("Nincs tobb hely a verembe");
                }
                //else
                //{
                //    vermem[szamlalo] = o;
                //    szamlalo++;
                //}
                vermem[szamlalo] = o;
                szamlalo++;
            }
            public void Levesz()
            {
                //if (vermem[0].Equals(o)) throw new Exception("Üres a verem, nem tudok kivenni belőle elemet.");
                if (szamlalo == 0)
                {
                    throw kiakad;
                }
                else
                {
                    vermem[szamlalo - 1] = default;
                    szamlalo--;
                }
                //Levesz(): a verem tetején lévő elem kiolvasása és az elem törlése a verem tetejéről.
                //Ha egy üres verem tetejéről veszünk le egy elemet, akkor hibát kell adni.
            }
            public Object Kiolvas()
            {
                if (Meret == 0) throw kiakad;
                return vermem[szamlalo - 1];
                //Kiolvas(): metódus, amely kiolvassa a verem tetején lévő értéket, de nem törli azt a veremből. 
                //Ha egy üres erem tetejéről olvasunk ki egy elemet, akkor hibát kell adni.
            }
            public void Megfordit()
            {
                if (Meret == 0) throw kiakad;
                vermem.Reverse();
                //Megfordit: a verem tartalmának teljes megfordítását kezdeményező metódus. 
                //Ekkor a veremben az elemek megfordulnak.
            }
            public override string ToString()
            {
                String returnolni = "";
                for (int i = 0; i < szamlalo; i++)
                {
                    returnolni += vermem[i].ToString() + " ";
                }
                return returnolni;
            }
        }
        static void Main(string[] args)
        {
            Verem<int> vermem = new Verem<int>(10);
            for (int i = 0; i < 10; i++)
            {
                vermem.Rarak(i);
            }
            vermem.Rarak(10);
            vermem.Rarak(11);
            vermem.Rarak(12);
            vermem.Rarak(13);
            vermem.Levesz();
            vermem.Levesz();
            Console.WriteLine("Utolso elem: " + vermem.Kiolvas());
            Console.WriteLine(vermem.ToString());
            Console.ReadKey();
        }
    }
}
