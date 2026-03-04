using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Dtos
{
    public class FixedIncomeCalculatorDto
    {
        public List<Asset> asset { get; set; }
    }
    public class Asset
    {
        public InvestmentType type { get; set; }
        public Modality modality { get; set; }
        public float profitability { get; set; }
        public int months { get; set; }
    }
    public enum InvestmentType
    {
        TESOURODIRETO,
        CDB,
        CRI,
        CRA,
        LCI,
        LCA,
        DEBENTURE,
        DEBENTUREINCENTIVADA
    }
    public enum Modality
    {
        PRE,
        CDI,
        IPCA
    }
}
