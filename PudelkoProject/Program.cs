using System;
using PudelkoProject.Enums;

namespace PudelkoProject
{
    class Program
    {
        public static void Main(string[] args)
        {
            Pudelko p = new Pudelko(5.5, 9.321, 0.1, UnitOfMeasure.centimeter);
            Console.WriteLine(p.ToString("cm"));
        }
    }
}