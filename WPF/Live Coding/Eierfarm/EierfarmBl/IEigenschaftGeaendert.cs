using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EierfarmBl
{
    public interface IEigenschaftGeaendert
    {
        event EventHandler<GefluegelEventArgs> EigenschaftGeaendert;
    }
}
