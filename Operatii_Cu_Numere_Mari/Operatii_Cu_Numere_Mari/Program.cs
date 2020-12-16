using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Operatii_cu_numere_mari
{
    class Program
    {
        static int[] v1, v2, v, rest;
        static char operation;
        static int x;
        static int nrZecimale = 0;
        static string bigNumber;
        static Stopwatch sw = new Stopwatch();
        private static bool isNegative = false;

        static void Main(string[] args)
        {
            Write();
            Console.Write("Primul numar:        ");
            bigNumber = ValidInputNumber();
            v1 = MakeVectorFrom(bigNumber);
            Console.Write("Operatia:            ");
            operation = char.Parse(Console.ReadLine());
            Switch_And_Do(operation);
            Console.WriteLine();
        }

        private static void Write()
        {
            Console.WriteLine("Acesta este un program care poate efectua operatii cu numere mari.");
            Console.WriteLine("Operatiile valide sunt:");
            Console.WriteLine("+   adaugare");
            Console.WriteLine("-   scadere");
            Console.WriteLine("*   inmultire");
            Console.WriteLine("/   impartie");
            Console.WriteLine("p   ridicare la putere");
            Console.WriteLine("r   radacina patrata");
            Console.WriteLine("f   factorial");
        }
        /// <summary>
        /// This method based on REGEX returns a valid decimal number
        /// </summary>
        /// <returns>
        /// RETURNS:
        /// <example>
        /// 123
        /// 1230
        /// 101010
        /// 109702654
        /// 0
        /// </example>
        /// NOT RETURNS:
        /// <example>
        /// 0123
        /// 0000 
        /// </example>
        /// </returns>
        private static string ValidInputNumber()
        {

            string onlyNumbers = "^([1-9]+(0*[1-9]*)*)?0?$";
            string line;
            bool writetext = false;
            Match result;
            do
            {
                if (writetext) Console.Write("Incearca din nou:    ");
                line = Console.ReadLine();
                result = Regex.Match(line, onlyNumbers);
                //Daca line (sirul de caractere sau numarul mare)
                //se potriveste cu modelul initializat in  variabila onlyNumbers
                writetext = true;
            } while (!result.Success);

            return result.Value;
        }

        private static int[] MakeVectorFrom(string bigNumber)
        {
            v = new int[bigNumber.Length];
            for (int i = 0; i < v.Length; i++)
            {
                v[i] = Convert.ToInt32(bigNumber[i] - '0');
            }
            return v;
        }

        private static int[] MakeVectorFrom(int num)
        {
            if (num == 0) return null;
            int lenght = 0;
            int aux = num;
            int[] vtemp;
            while (aux != 0)
            {
                lenght++;
                aux /= 10;
            }
            vtemp = new int[lenght];
            while (num != 0)
            {
                vtemp[--lenght] = num % 10;
                num /= 10;
            }

            return vtemp;
        }

        private static void Switch_And_Do(char operation)
        {
            if (operation == 'f')
            {
                Fact();
            }
            else if (operation == 'r')
            {
                SquareRoot();
            }
            else
            {
                Console.Write("Al doilea numar:     ");
                bigNumber = ValidInputNumber();
                v2 = MakeVectorFrom(bigNumber);
                switch (operation)
                {
                    case '+':
                        Addition();
                        break;
                    case '-':
                        Subtraction();
                        break;
                    case '*':
                        Multiplication();
                        break;
                    case '/':
                        Division();
                        break;
                    case 'p':
                        Power();
                        break;
                    default:

                        break;
                }
            }

        }

        /*************************************************************
         *                        OPERATIONS
         * ***********************************************************/
        private static void Addition()
        {
            Console.WriteLine("****** suma celor doua numere ******");
            View(Suma(v1, v2));
        }

        private static int[] Suma(int[] v1, int[] v2)
        {
            if (v1.Length < v2.Length)
            {
                return Suma(v2, v1);
            }
            int max, k;
            max = v1.Length;
            k = v2.Length;    //contorul pentru v2
            int[] v = new int[max + 1];
            int[] vtemp = new int[max];
            int trans = 0;
            for (int i = max - 1; i >= 0; i--)
            {
                k--;
                v[i + 1] = trans + v1[i];     //adaugam transportul acumulat din pozitia anterioara 
                                              // si elementele vectorului 1
                if (k >= 0)
                    v[i + 1] += v2[k];      //adaugam elementele vectorului 2 pana cand avem ce adauga :) (Exclude overflow)     

                trans = v[i + 1] / 10; //transportul va fi intotdeauna egal cu cifra zecilor
                v[i + 1] %= 10;        //eliminam valoarea transportului din pozitia curenta a vetorului
            }
            if (trans == 0)
            {
                for (int i = 1; i < v.Length; i++)
                {
                    vtemp[i - 1] = v[i];
                }
                v = vtemp;
                //daca transportul final este 0 trebuie eliminat 0-ul de la inceputul vectorului
                //si lungimea vectorului devine mai mic cu 1 
            }
            else
            {
                v[0] = trans;   //adaugam ultimul transport, daca nu este 0
            }
            return v;
        }

        private static void Subtraction()
        {
            Console.WriteLine("****** Diferenta celor doua numere ******");
            int[] dif = Diferenta(v1, v2);
            if (isNegative)
            {
                Console.Write("-");
            }
            View(dif);
        }

        private static int[] Diferenta(int[] v1, int[] v2)
        {

            if (!FirstIsGreater(v1, v2))
            {
                isNegative = true;
                return Diferenta(v2, v1);
            }

            int max, min;
            max = v1.Length;
            min = v2.Length;
            int k = min;    //contorul pentru v2
            int[] vtemp;
            for (int i = max - 1; i >= 0; i--)
            {
                k--;
                if (k >= 0)
                {
                    v1[i] = v1[i] - v2[k];
                }
                else
                {
                    v1[i] = v1[i];
                }
                if (v1[i] < 0)
                {
                    v1[i - 1]--;  //imprumut
                    v1[i] += 10;
                }
            }

            /******** eliminarea 0-urilor de la inceputul vectorului ********/
            while (v1.Length > 1 && v1[0] == 0)
            {
                vtemp = new int[--max];
                for (int i = 1; i < v1.Length; i++)
                {
                    vtemp[i - 1] = v1[i];
                }
                v1 = vtemp;
            }
            return v1;
        }

        private static void Multiplication()
        {
            Console.WriteLine("****** Produsul celor doua numere ******");
            View(InmultireVectori(v1, v2));
        }

        private static int[] InmultireVectori(int[] v1, int[] v2)
        {
            int j = 1, scalar = v2[v2.Length - 1];
            int[] vSum = InmultireCuScalar(v1, scalar);
            for (int i = v2.Length - 2; i >= 0; i--)
            {
                scalar = v2[i];
                vSum = Suma(vSum, AdaugaNumar(InmultireCuScalar(v1, scalar), 0, j));
                j++;
            }
            return vSum;
        }

        private static int[] InmultireCuScalar(int[] v1, int scalar)
        {
            int[] v;
            if (scalar != 0)
            {
                v = v1;
                for (int i = 1; i < scalar; i++)
                {
                    v = Suma(v, v1);
                }
            }
            else
            {
                v = new int[1];
                v[0] = 0;
            }
            return v;
        }

        private static void Division()
        {
            rest = null; v = null;
            v = Impartire(v1, v2);

            if (v == null && rest == null)
            {
                Console.WriteLine("Nu putem divide cu 0!");
                Console.WriteLine("Incearca cu un alt divizor");
                Console.WriteLine();
                Switch_And_Do(operation);
            }
            else
            {
                if (rest != null)
                {
                    Console.WriteLine("Cu ce aproximare sa fie calculata impartirea?");
                    Console.Write("Numarul maxim a zecimalelor:");
                    nrZecimale = int.Parse(ValidInputNumber());

                }

                Console.WriteLine("****** Catul impartiri ******");
                if (v != null)
                {
                    View(v);
                }
                if (v == null) Console.Write("0");
                if (nrZecimale != 0)
                {
                    Console.Write(".");
                    v = Decimal(rest, v2, nrZecimale);
                    View(v);
                }
            }
        }

        private static int[] Decimal(int[] rest, int[] divizor, int nrZecimale)
        {
            rest = AdaugaNumar(rest, 0, 1);
            int lenght = rest.Length;
            int[] v = null;

            int s;
            bool ok;
            int i = 0;
            while (i < nrZecimale)
            {
                ok = FirstIsGreater(rest, divizor);
                if (ok)
                {
                    s = 0;
                    while (FirstIsGreater(rest, divizor))
                    {
                        rest = Diferenta(rest, divizor);
                        s++;
                    }
                    if (v == null) v = MakeVectorFrom(s);
                    else v = AdaugaNumar(v, s, 1);
                    if (rest.Length == 1 && rest[0] == 0) { rest = null; break; }
                }
                else
                {
                    if (v != null) v = AdaugaNumar(v, 0, 1);
                }
                rest = AdaugaNumar(rest, 0, 1);
                i++;
            }

            return v;
        }

        private static int[] Impartire(int[] deimpartit, int[] divizor)
        {
            int[] v = null;

            if (divizor.Length == 1 && divizor[0] == 0) return null;
            else if (divizor.Length == 1 && divizor[0] == 1) return deimpartit;
            else if (deimpartit.Length == 1 && deimpartit[0] == 0) return deimpartit;
            else
            {
                int[] r = null;
                int i = 0;
                bool ok;
                do
                {
                    ok = false;
                    if (r == null) r = MakeVectorFrom(deimpartit[i]);
                    else r = AdaugaNumar(r, deimpartit[i], 1);
                    if (r != null) ok = FirstIsGreater(r, divizor);
                    if (ok)
                    {
                        int s = 0;
                        while (FirstIsGreater(r, divizor))
                        {
                            r = Diferenta(r, divizor);
                            s++;
                        }
                        if (v == null) v = MakeVectorFrom(s);
                        else v = AdaugaNumar(v, s, 1);
                        if (r.Length == 1 && r[0] == 0) r = null;
                    }
                    else
                    {
                        if (v != null) v = AdaugaNumar(v, 0, 1);
                    }
                    i++;
                } while (i < deimpartit.Length);

                rest = r;
                return v;
            }
        }

        private static void Power()
        {
            Console.WriteLine("****** Puterea celor doua numere ******");
            View(Putere(v1, v2));
        }

        private static int[] Putere(int[] v1, int[] v2)
        {
            int[] vPutere = PutereScalar(v1, v2[v2.Length - 1]);
            int scalar, j = 1;

            for (int i = v2.Length - 2; i >= 0; i--)
            {
                scalar = v2[i];
                vPutere = InmultireVectori(vPutere, PutereScalar(v1, scalar * (int)Math.Pow(10, j)));
                j++;
            }
            return vPutere;
        }

        private static int[] PutereScalar(int[] v1, int n)
        {
            int[] putere;
            if (n == 0)
            {
                putere = new int[1];
                putere[0] = 1;
            }
            else
            {
                putere = v1;
            }

            for (int i = 2; i <= n; i++)
            {
                putere = InmultireVectori(putere, v1);
            }
            return putere;
        }

        private static void SquareRoot()
        {
            rest = null;
            Console.WriteLine("****** Radacina patrata al numarului ******");
            RadacinaPatrata(v1);

        }

        private static void RadacinaPatrata(int[] num)
        {
            int x, i, n;
            int[] v1, v2, aux, product, parteaIntreaga;
            bool lengthIsEven = num.Length % 2 == 0;
            bool isFraction = false;
            product = null;
            if (lengthIsEven)
                x = 10 * num[0] + num[1];
            else x = num[0];

            n = (int)Math.Sqrt(x);
            parteaIntreaga = MakeVectorFrom(n);

            v1 = MakeVectorFrom(x);
            v2 = MakeVectorFrom(n * n);
            rest = Diferenta(v1, v2);

            //
            if (lengthIsEven) i = 2;
            else i = 1;


            if (i < num.Length && num[i] == 0 && num[i + 1] == 0)
            {
                while (i < num.Length)
                {
                    parteaIntreaga = AdaugaNumar(parteaIntreaga, 0, 1);
                    i += 2;
                }
            }


            while (i < num.Length)
            {
                if (rest[0] == 0)
                {
                    v1 = MakeVectorFrom(num[i]);
                }
                else
                {
                    v1 = AdaugaNumar(rest, num[i], 1);
                }

                v1 = AdaugaNumar(v1, num[i + 1], 1);
                aux = InmultireCuScalar(parteaIntreaga, 2);
                int testnum = 0;
                do
                {
                    v2 = product;
                    product = AdaugaNumar(aux, ++testnum, 1);
                    product = InmultireCuScalar(product, testnum);

                } while (FirstIsGreater(v1, product) && product != v1);
                rest = Diferenta(v1, v2);
                testnum--;
                parteaIntreaga = AdaugaNumar(parteaIntreaga, testnum, 1);
                i += 2;
            }
            #region Parte fractionala
            int[] numarReal = parteaIntreaga;
            int[] parteFract = null;
            product = null;
            if (rest[0] != 0)
            {
                isFraction = true;
                Console.WriteLine("Cu ce aproximare sa fie calculata impartirea?");
                Console.Write("Numarul maxim a zecimalelor:");
                nrZecimale = int.Parse(ValidInputNumber());
                i = 0;
                while (i < nrZecimale && rest[0] != 0)
                {
                    v1 = AdaugaNumar(rest, 0, 2);
                    aux = InmultireCuScalar(numarReal, 2);
                    int testnum = -1;
                    do
                    {
                        testnum++;
                        v2 = product;
                        product = AdaugaNumar(aux, testnum, 1);
                        product = InmultireCuScalar(product, testnum);
                    } while (FirstIsGreater(v1, product) && product != v1);
                    rest = Diferenta(v1, v2);
                    testnum--;
                    if (parteFract == null) parteFract = MakeVectorFrom(testnum);
                    else parteFract = AdaugaNumar(parteFract, testnum, 1);
                    numarReal = AdaugaNumar(numarReal, testnum, 1);
                    i++;
                }
            }
            #endregion
            View(parteaIntreaga);
            if (isFraction && nrZecimale != 0)
            {
                Console.Write(".");
                View(parteFract);
            }

        }

        private static void Fact()
        {
            Console.WriteLine("\n FACTORIAL");
            x = int.Parse(bigNumber);
            sw.Start();
            View(Factorial(x));
            sw.Stop();
            TimeSpan ts = sw.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
            Console.WriteLine();
            Console.WriteLine($"Runtime " + elapsedTime);
            Console.WriteLine();
        }

        private static int[] Factorial(int n)
        {
            int nrZerouriLaFinal = 0, index, unit;
            int[] fact = { 1 };
            int[] vidx;


            for (int i = 1; i <= n; i++)
            {
                //Console.WriteLine(i);
                index = i;
                unit = index % 10;

                while (index % 10 == 0)
                {
                    nrZerouriLaFinal++;
                    index /= 10;
                }
                vidx = MakeVectorFrom(index);
                fact = InmultireVectori(fact, vidx);
            }
            fact = AdaugaNumar(fact, 0, nrZerouriLaFinal);
            Console.Write($"{x}! = ");
            return fact;
        }

        /*************************************************************
         *                        LOGICALS & OTHERS
         * ***********************************************************/
        private static bool FirstIsGreater(int[] v1, int[] v2)
        {
            if (v1.Length < v2.Length) return false;
            else if (v1.Length == v2.Length)
            {
                for (int i = 0; i < v2.Length; i++)
                {
                    if (v1[i] < v2[i])
                    {
                        return false;
                    }
                    else if (v1[i] > v2[i])
                    {
                        return true;
                    }
                }
            }
            return true;
        }

        private static int[] AdaugaNumar(int[] v1, int numar, int decateori)
        {
            int[] vtemp = new int[v1.Length + decateori];
            for (int i = 0; i < vtemp.Length; i++)
            {
                if (i < v1.Length)
                {
                    vtemp[i] = v1[i];
                }
                else
                {
                    vtemp[i] = numar;
                }
            }
            return vtemp;
        }

        private static void View(int[] v)
        {
            for (int i = 0; i < v.Length; i++)
            {
                Console.Write($"{v[i]}");
            }
        }

    }
}