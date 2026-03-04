using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Project.Domain.Models
{
    public class SectorModel
    {
        public string Name { get; set; }
        public string Perennial { get; set; }
        public double Percentage { get; set; }

        public SectorModel() { }

        public SectorModel(string name, string perennial, double percentage)
        {
            Name = name;
            Perennial = perennial;
            Percentage = percentage;    
        }
    }
    
}
