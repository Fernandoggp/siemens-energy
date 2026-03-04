using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Project.Domain.Models
{
    public class SectorDbModel
    {
        public string Name { get; set; }
        public string Perennial { get; set; }

        public SectorDbModel(string name, string perennial)
        {
            Name = name;
            Perennial = perennial;
        }
    }

}
