using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Conversii_intre_Baze
{
    class Program
    {
        static void Main(string[] args)
        {
            int baza , bazaTinta, nr, fractie;
            string line;
            
            Console.WriteLine("Introduceti baza din care doriti sa convertiti numarul");
            baza = int.Parse(Console.ReadLine());
            Console.WriteLine("Introduceti bazaTinta in care doriti sa convertiti numarul");
            bazaTinta = int.Parse(Console.ReadLine());

            //conditii
            //while(!numarCorect)
            Console.WriteLine("Introduceti numarul care trebuie convertit");
            line = Console.ReadLine();

            string[] split = line.Split('.');
            nr = int.Parse(split[0]);
            fractie = 0;
            bool ok = false;
            if (split.Length <= 2 && split.Length > 0)
            {

                if (split.Length == 2)
                {
                    fractie = int.Parse(split[1]);
                }
                ok = true;
            }
            else
            {
                // Nu e nr corect
            }

            Stack<double> stiva = new Stack<double>();
            int cat, rest, numar;
            string result="";
            string[] hex = { "A", "B", "C", "D", "E", "F" };
            cat = nr;
            numar = nr;

            //convertirea unui numar intreg din baza 10 in baza inferior

            if (cat == 0)result += cat;
            while(cat > 0)
            {     
                //TODO transcriere in metoda recursiva
                cat = numar / bazaTinta;
                rest = numar % bazaTinta;
                stiva.Push(rest);
                numar = cat;
            }

            while (stiva.Count > 0)
            {
                result += stiva.Pop();
            }

            if (fractie != 0)
            {
                double fr = fractie;
                numar = split[1].Length;

                while (numar>0)
                {
                    fr /= baza;
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

           
            Console.WriteLine(result);

          //  Console.ReadKey();
        }
    }
}
