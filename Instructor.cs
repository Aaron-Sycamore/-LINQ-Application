using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_8
{
    class Instructor
    {
        public string Name;
        public string OfficeNum;
        public string Email;


        public Instructor(string Nam, string Off, string Ema)
        {
            Name = Nam;
            OfficeNum = Off;
            Email = Ema;

        }
        public void printer()
        {
            Console.WriteLine(Name);
            Console.WriteLine(OfficeNum);
            Console.WriteLine(Email);

        }
    }
}
