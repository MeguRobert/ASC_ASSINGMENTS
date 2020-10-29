using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conversie_din_baza2_in_baza_putere__4_8_16_
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, string> base16 = new Dictionary<string, string>() ;
            base16.Add("0000", "0");
            base16.Add("0001", "1");
            base16.Add("0010", "2");
            base16.Add("0011", "3");

            base16.Add("0100", "4");
            base16.Add("0101", "5");
            base16.Add("0110", "6");
            base16.Add("0111", "7");
            
            base16.Add("1000", "0");
            base16.Add("1001", "1");
            base16.Add("1010", "2");
            base16.Add("1011", "3");
                        
            base16.Add("1100", "4");
            base16.Add("1101", "5");
            base16.Add("1110", "6");
            base16.Add("1111", "7");


            Dictionary<string, string> base8 = new Dictionary<string, string>();
            base8.Add("000", "0");
            base8.Add("001", "1");
            base8.Add("010", "2");
            base8.Add("011", "3");
                
            base8.Add("100", "4");
            base8.Add("101", "5");
            base8.Add("110", "6");
            base8.Add("111", "7");


            Dictionary<string, string> base4 = new Dictionary<string, string>();
            base4.Add("00", "0");
            base4.Add("01", "1");
            base4.Add("10", "2");
            base4.Add("11", "3");



        }
    }
}
