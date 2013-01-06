using System;
using System.Collections;
using System.Text;

namespace HarmonicOscillation
{
    public class Calculation
    {
        /**
         * Internen Felder
         */
        static private double _phaseangle;
        static private double _angularfrequency;
        static private double _amplitude;

        /**
         * Phrasenwinkel (ϕ) in Bogenmaß (rad)
         */
        public double PhaseAngle
        {
            get { return _phaseangle; }
            set { _phaseangle = value; }
        }

        /**
         * Kreisfrequenz (ω)
         */
        public double AngularFrequency
        {
            get { return _angularfrequency; }
            set { _angularfrequency = value; }
        }

        /**
         * Amplitude / Scheitelwert (y_max)
         */
        public double Amplitude
        {
            get { return _amplitude; }
            set { _amplitude = value; }
        }

        /**
         * Berechnen von Ort
         */
        public double calculateWay(double x)
        {
            // Werte
            return _amplitude * Math.Sin(_angularfrequency * x + _phaseangle);
        }

        /**
         * Berechnen von Geschwindigkeit
         */
        public double calculateSpeed(double x)
        {
            // Werte
            return _amplitude * Math.Cos(_angularfrequency * x + _phaseangle);
        }

        /**
         * Berechnen von Geschwindigkeit
         */
        public double calculateAcceleration(double x)
        {
            // Werte
            return -_amplitude * (_angularfrequency * _angularfrequency) * Math.Sin(_angularfrequency * x + _phaseangle);
        }
    }
}
