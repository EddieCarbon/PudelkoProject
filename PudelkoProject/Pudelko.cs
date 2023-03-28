using System.Collections;
using System.Globalization;
using PudelkoProject.Enums;

namespace PudelkoProject
{
    public sealed class Pudelko : IFormattable
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
        #endregion
        
        #region External properties
        
        public double A { get => Math.Round(dimensions[0] / 1000, 3); }
        public double B { get => Math.Round(dimensions[1] / 1000, 3); }
        public double C { get => Math.Round(dimensions[2] / 1000, 3); }
        public double Objetosc
        {
            get => Math.Round((dimensions[0] / 1000) * (dimensions[1] / 1000) * (dimensions[2] / 1000), 9);
        }
        
        #endregion

        #region Implementation IFormatable

        public override string ToString()
        {
            return this.ToString("M", CultureInfo.CurrentCulture);
        }
        public string ToString(string format)
        {
            return this.ToString(format, CultureInfo.CurrentCulture);
        }
        public string ToString(string format, IFormatProvider? formatProvider)
        {
            if (String.IsNullOrEmpty(format)) format = "M";
            formatProvider ??= CultureInfo.CurrentCulture;

            int offset;
            switch (format.ToUpper())
            {
                case "MM":
                    offset = 1000;
                    return $"{(A * offset).ToString("F0", formatProvider)} mm × " +
                           $"{(B * offset).ToString("F0", formatProvider)} mm × " +
                           $"{(C * offset).ToString("F0", formatProvider)} mm";
                case "CM":
                    offset = 100;
                    return $"{(A * offset).ToString("F1", formatProvider)} cm × " +
                           $"{(B * offset).ToString("F1", formatProvider)} cm × " +
                           $"{(C * offset).ToString("F1", formatProvider)} cm";
                case "M":
                    return $"{A.ToString("F3", formatProvider)} m × " +
                           $"{B.ToString("F3", formatProvider)} m × " +
                           $"{C.ToString("F3", formatProvider)} m";
                default:
                    throw new FormatException(String.Format($"The {format} format string is not supported."));
            }
        }
        #endregion
    }
}

