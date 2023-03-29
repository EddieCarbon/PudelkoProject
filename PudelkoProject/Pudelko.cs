using System.Collections;
using System.ComponentModel;
using System.Globalization;
using PudelkoProject.Enums;

namespace PudelkoProject
{
    public sealed class Pudelko : IFormattable, IEquatable<Pudelko>
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
        public double Pole
        {
            get => Math.Round(
                2 * (dimensions[0] / 1000) * (dimensions[1] / 1000) +
                2 * (dimensions[0] / 1000) * (dimensions[2] / 1000) +
                2 * (dimensions[1] / 1000) * (dimensions[2] / 1000), 6);
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

        #region Implementation IEquatable

        public bool Equals(Pudelko other)
        {
            if (other == null) return false;
            if (ReferenceEquals(this, other)) return true;

            double[] thisBox = { this.A, this.B, this.C };
            double[] otherBox = { other.A, other.B, other.C };
            Array.Sort(thisBox);
            Array.Sort(otherBox);
            return thisBox[0] == otherBox[0] & thisBox[1] == otherBox[1] & thisBox[2] == otherBox[2];
        }

        public override bool Equals(object obj)
        {
            if (obj is Pudelko) return Equals((Pudelko)obj);
            return false;
        }
        public override int GetHashCode() => this.dimensions.GetHashCode();

        public static bool Equals(Pudelko p1, Pudelko p2)
        {
            if ((p1 is null) && (p2 is null)) return false;
            if ((p1 is null)) return false;

            return p1.Equals(p2);
        }
        public static bool operator ==(Pudelko p1, Pudelko p2) => Equals(p1, p2);
        public static bool operator !=(Pudelko p1, Pudelko p2) => !(p1 == p2);
        #endregion
        
        #region Implementation of operators
        public static Pudelko operator +(Pudelko p1, Pudelko p2)
        {
            double outputA, outputB, outputC;
            double[] dimensionsP1 = { p1.A, p1.B, p1.C };
            double[] dimensionsP2 = { p2.A, p2.B, p2.C };
            
            Array.Sort(dimensionsP1);
            Array.Sort(dimensionsP2);

            if (dimensionsP1[2] > dimensionsP2[2])
                outputA = dimensionsP1[2];
            else
                outputA = dimensionsP2[2];
            
            if (dimensionsP1[1] > dimensionsP1[2])
                outputB = dimensionsP1[1];
            else
                outputB = dimensionsP2[1];

            outputC = dimensionsP1[0] + dimensionsP2[0];

            return new Pudelko(outputA, outputB, outputC);
        }
        #endregion

        #region Conversion

        public static explicit operator double[](Pudelko p1)
        {
            return new[] { p1.A, p1.B, p1.C };
        }

        public static implicit operator Pudelko(ValueTuple<int, int, int> input)
        {
            return new Pudelko(input.Item1, input.Item2, input.Item3, UnitOfMeasure.milimeter);
        }
        #endregion
        
        
    }
}

