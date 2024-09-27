using ChinookDal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChinookDataAccess.Model
{
    public class Genre
    {
        public string Name { get; set; }
        public List<Artist> Artists { get; set; }
    }
}
