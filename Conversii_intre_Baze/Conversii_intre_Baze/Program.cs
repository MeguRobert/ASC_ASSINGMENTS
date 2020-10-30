using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace Conversii_intre_Baze
{
    class Program
    {
        static void Main(string[] args)
        {
            int baza=0, bazaTinta=0, nr, fractie;
            string line="", result = "";
            GetData(ref baza, ref bazaTinta, ref  line);
 
            string[] split = line.Split('.');
            fractie = 0;
            while(!(split.Length <= 2 && split.Length > 0))
            {
                Console.WriteLine("\n Nu era numar corect!  Va rog introduceti datele din nou! \n");
                GetData(ref baza, ref bazaTinta, ref line);
                split = line.Split('.');
            }
           
            nr = int.Parse(split[0]);
            if (split.Length == 2) fractie = int.Parse(split[1]);

            if (baza!= 10)
            nr=ConvertToBase10(nr,ref baza,bazaTinta,fractie, ref result, split);
            result += nr;
            if (bazaTinta != 10)
                ConvertFromBase10(nr, baza, bazaTinta, fractie, ref result, split);
            Console.WriteLine("\n REZULTAT: "+result);
        }

        private static int ConvertToBase10(int nr,ref int baza, int bazaTinta, int fractie, ref string result, string[] split)
        {
            int i=0, num = 0; ;
       
            while (nr>0)
            {
                num += nr % 10 * (int)Math.Pow(baza, i);
                i++;
                nr /= 10;
            }
            return num;
        }

        private static string ConvertFromBase10(int nr,int baza, int bazaTinta, int fractie, ref string result, string[] split)
        {
            result = "";
            Stack<double> stiva = new Stack<double>();
            int cat, rest, numar;

            string[] hex = { "A", "B", "C", "D", "E", "F" };
            cat = nr;
            numar = nr;
            //convertirea unui numar intreg din baza 10 in baza inferior

            if (cat == 0) result += cat;
            while (cat > 0)
            {
                //TODO transcriere in metoda recursiva
                cat = numar / bazaTinta;
                rest = numar % bazaTinta;
                stiva.Push(rest);
                numar = cat;
            }

            while (stiva.Count > 0)
            {
                double x = stiva.Pop();
                if (x >= 10)
                    result += hex[(int)x - 10];
                else
                    result += x;
            }

            if (fractie != 0)
            {
                double fr = fractie;
                numar = split[1].Length;

                while (numar > 0)
                {
                    fr /= 10;
                    numar--;
                }
                Console.WriteLine($"fractie= {fr}");
                result += ".";
                while (fr > 0)
                {
                    fr = fr * bazaTinta;
                    cat = (int)fr;
                    fr -= cat;
                    result += cat;
                }
               
            }
            return result;
        }

        private static void GetData(ref int baza,ref int bazaTinta,ref string line)
        {
            
            do
            {
                Console.WriteLine("Introduceti baza din care doriti sa convertiti numarul");
                baza = int.Parse(Console.ReadLine());
            } while (baza < 2 || baza > 16);
            
            
            
            do
            {
                Console.WriteLine("Introduceti bazaTinta in care doriti sa convertiti numarul");
                bazaTinta = int.Parse(Console.ReadLine());
            } while (bazaTinta < 2 || bazaTinta > 16);
            

            //conditii
            //while(!numarCorect)
            Console.WriteLine("Introduceti numarul care trebuie convertit");
            line = Console.ReadLine();
            if (line[0] == '0' && line[1] == '0') line = line.TrimStart('0');
            
        }

    }
}
