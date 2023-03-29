using PudelkoProject.Enums;

namespace PudelkoProject
{
    public static class KompresujMethod
    {
        public static Pudelko Kompresuj(this Pudelko p)
        {
            if ((p.A == p.B) & (p.A == p.C))
                return new Pudelko(p.A, p.B, p.C);

            double edge = Math.Round(Math.Pow(p.Objetosc, (double)1 / 3), 3);

            return new Pudelko(edge, edge, edge, UnitOfMeasure.meter);
        }
    }
}

