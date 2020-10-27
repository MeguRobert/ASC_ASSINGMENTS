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
            //convertirea din baza 10 in baza 2
            //convertirea din baza 2 in baza 10

            //convertirea din baza 10 in baza 16
            //convertirea din baza 16 in baza 10

            //convertirea din baza 2 in baza 16
            //convertirea din baza 16 in baza 2

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
            bool ok = false;
            if (split.Length <= 2 && split.Length > 0)
            {

                if (split.Length == 1)
                {
                    nr = int.Parse(split[0]);
                    fractie = 0;
                }
                else
                {
                    nr = int.Parse(split[0]);
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
            while(cat > 0)
            {                
                cat = numar / bazaTinta;
                rest = numar % bazaTinta;
                stiva.Push(rest);
                numar = cat;
            }
            while (stiva.Count > 0)
            {
                result += stiva.Pop();
            }
            Console.WriteLine(result);

          //  Console.ReadKey();
        }
    }
}
