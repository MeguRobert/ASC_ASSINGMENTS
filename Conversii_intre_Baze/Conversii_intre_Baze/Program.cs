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

            int baza , bazaTinta, nr;

            Console.WriteLine("Introduceti baza din care doriti sa convertiti numarul");
            baza = int.Parse(Console.ReadLine());
            Console.WriteLine("Introduceti bazaTinta in care doriti sa convertiti numarul");
            bazaTinta = int.Parse(Console.ReadLine());
            Console.WriteLine("Introduceti numarul care trebuie convertit");
            nr = int.Parse(Console.ReadLine());


            Stack<int> stiva = new Stack<int>();
            int cat, rest, numar;
            string result="";
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

            Console.ReadKey();
        }
    }
}
