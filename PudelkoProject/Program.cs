using System;
using PudelkoProject.Enums;

namespace PudelkoProject
{
    class Program
    {
        public static void Main(string[] args)
        {
            Pudelko p1 = new Pudelko(5.5, 9.321, 1, UnitOfMeasure.centimeter);
            Pudelko p2 = new Pudelko(6, 7, 8, UnitOfMeasure.centimeter);
            Pudelko p3 = new Pudelko(3, 5, 9, UnitOfMeasure.centimeter);
            Console.WriteLine((p1 + p2) + p3);
        }
    }
}