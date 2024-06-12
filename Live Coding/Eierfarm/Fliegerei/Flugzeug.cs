using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fliegerei
{
    public class Flugzeug : Fluggeraet
    {
        public event System.EventHandler TouchDown;
        public event System.EventHandler LiftOff;
        public event System.EventHandler MotorAn;
        public event System.EventHandler MotorAus;

        public int AnzahlMotoren
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        public int Motorleistung
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        public int Tankinhalt
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        public int Verbrauch
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }
    }
}