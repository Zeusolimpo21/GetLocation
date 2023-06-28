using System.Drawing;

namespace GetLocation.Class
{
    public class Punto
    {

        private double lt, ln, r;

        public Punto(double lt, double ln, double r)
        { 
          this.lt = lt; 
          this.ln = ln; 
          this.r = r; 
        }
        public double glt() { return lt; }
        public double gln() { return ln; }
        public double gr() { return r; }

        public static implicit operator Point(Punto v)
        {
            throw new NotImplementedException();
        }
    }
}
