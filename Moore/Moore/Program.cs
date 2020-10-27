using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moore
{
    class Program
    {
        static void Main(string[] args)
        {
            string input;
            int n;
            while (true)
            {
                Console.WriteLine("     Introduceti un numar natural n, pentru a afla timpul necesar,\n  ca tranzistoarele sa devine de n-ori mai rapide decat in momentul de fata:");
                input = Console.ReadLine();
                //folosim TryParse pentru a consulta daca input-ul este un numar natural, sau nu.
                if (int.TryParse(input, out n) && n > 0 )
                {
                    double ani;
                    ani = 1.5 * Math.Log(n, 2);
                    Console.WriteLine("Tranzistoarele vor fi mai rapide de " + n + " ori in " + ani + " ani");
                    break;
                }
                else
                {
                    Console.WriteLine("Introduceti un numar natural!\n");
                    //se va repeta pana cand utilizatorul introduce un numar natural.
                }
            }

        }
    }
}
