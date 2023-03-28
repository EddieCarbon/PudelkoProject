using System.Collections;
using System.Globalization;
using PudelkoProject.Enums;

namespace PudelkoProject
{
    public sealed class Pudelko
    {
        private readonly double[] dimensions;

        #region Constructors
        public Pudelko(double? a, double? b, double? c) : this(a, b, c, UnitOfMeasure.meter) { }
        public Pudelko(double? a, double? b, UnitOfMeasure unit) : this(a, b, null, unit) { }
        public Pudelko(double? a, double? b) : this(a, b, 0.1, UnitOfMeasure.meter) { }
        public Pudelko(double? a, UnitOfMeasure unit) : this(a, null, null, unit) { }
        public Pudelko(double? a) : this(a, 0.1, 0.1, UnitOfMeasure.meter) { }
        public Pudelko() : this(0.1, 0.1, 0.1, UnitOfMeasure.meter) { }

        public Pudelko(double? a, double? b, double? c, UnitOfMeasure unit)
        {
            int factor = 1000; // Default factor for meter
            if (unit == UnitOfMeasure.centimeter) factor = 10;
            else if (unit == UnitOfMeasure.milimeter) factor = 1;

            double[] dimensions = new double[3];
            if (a is not null) dimensions[0] = Math.Floor((double)a * factor);
            else dimensions[0] = 100;
            if (b is not null) dimensions[1] = Math.Floor((double)b * factor);
            else dimensions[1] = 100;
            if (c is not null) dimensions[2] = Math.Floor((double)c * factor);
            else dimensions[2] = 100;

            for (int i = 0; i < dimensions.Length; i++)
                if (dimensions[i] > 10000 | dimensions[i] < 1)
                    throw new ArgumentOutOfRangeException("Wrong dimension!");

            this.dimensions = dimensions;
        } // Main constructor
        
        public




    }


        #endregion
    }
}

