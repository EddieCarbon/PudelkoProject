using System;
using PudelkoProject.Enums;

namespace PudelkoProject
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            //Create list of Pudelko using different contructors, parsing and conversion
            Console.WriteLine("\nUnsorted list:");
            
            List<Pudelko> boxList = new()
            {
                Pudelko.Parse("200 cm"),
                Pudelko.Parse("2000 mm × 9321 mm × 100 cm"),
                Pudelko.Parse("200 cm × 932,1 cm"),
                new Pudelko(2222,2222,2313, UnitOfMeasure.milimeter),
                new Pudelko(123,434,334, UnitOfMeasure.centimeter),
                new Pudelko(2,4,8, UnitOfMeasure.meter),
                new Pudelko(456,346, UnitOfMeasure.centimeter),
                new Pudelko(2332, UnitOfMeasure.milimeter),
                new Pudelko(6, UnitOfMeasure.meter),
                new Pudelko(0.1, 0.1, 0.1, UnitOfMeasure.meter),
                new Pudelko(10, 10, 10, UnitOfMeasure.centimeter),
                new Pudelko(0.1, 0.1, 0.1),
                new Pudelko(0.1, 0.1),
                new Pudelko(0.1)
            };

            // List before sort          
            foreach (Pudelko p in boxList)
                Console.WriteLine(p.ToString());

            //Sort the list using delegate "Comparison"
            boxList.Sort(Comparison);

            // List after sort
            Console.WriteLine("\nSorted list: \n");
            foreach (Pudelko p in boxList)
                Console.WriteLine(p.ToString());
        }

        private static int Comparison(Pudelko p1, Pudelko p2)
        {
            if (p2 is null) //Null is assumed to be smallest
                return 1;
            else if (p1.Objetosc.CompareTo(p2.Objetosc) != 0)
                return p1.Objetosc.CompareTo(p2.Objetosc);
            else if (p1.Pole.CompareTo(p2.Pole) != 0)
                return p1.Pole.CompareTo(p2.Pole);
            else
                return (p1.A + p1.B + p1.C).CompareTo(p2.A + p2.B + p2.C);
        }
    }
}