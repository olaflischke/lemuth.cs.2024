using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reederei
{
    public class Mitarbeiter : Person
    {
        public int Personalnummer
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        public System.Collections.Generic.List<Qualifikation> Qualifikationen
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