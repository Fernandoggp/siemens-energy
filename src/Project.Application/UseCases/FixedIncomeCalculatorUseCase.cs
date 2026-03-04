using Deviot.Common;
using DocumentFormat.OpenXml.Drawing.Charts;
using Microsoft.Extensions.Logging;
using Project.Application.Base;
using Project.Application.Dtos;
using Project.Domain.Entities;
using Project.Domain.Exceptions;
using Project.Domain.Interfaces.Http;
using Project.Domain.Interfaces.UseCases;
using System.Text.Json;

namespace Project.Application.UseCases
{
    public class FixedIncomeCalculatorUseCase : UseCaseBase, IFixedIncomeCalculatorUseCase
    {
        public FixedIncomeCalculatorUseCase(INotifier notifier, ILogger<FixedIncomeCalculatorUseCase> logger) : base(notifier, logger)
        {
        }

        public async Task<string> ExecuteAsync(List<FixedIncomeCalculatorEntity> investments)
        {
            if (investments.Count != 2)
                throw new ObjectValidationException("É necessário exatamente dois investimentos para comparação.");

            var results = new List<dynamic>();

            foreach (var investment in investments)
            {
                float grossProfitability = CalculateGrossProfitability(investment);
                float taxRate = CalculateTaxRate(investment.Type, investment.Months);
                float netProfitability = grossProfitability * (1 - taxRate);

                results.Add(new
                {
                    InvestmentType = investment.Type,
                    Modality = investment.Modality,
                    TaxRate = taxRate * 100,
                    Profitability = netProfitability,
                    Months = investment.Months
                });
            }

            var inv1 = results[0];
            var inv2 = results[1];

            string comparison = $"Com as taxas atuais, o investimento em {inv1.InvestmentType} oferece um retorno anual de {inv1.Profitability:F2}% ao longo de {inv1.Months} meses, enquanto o investimento em {inv2.InvestmentType} proporciona um retorno anual de {inv2.Profitability:F2}% no mesmo período. Portanto, no cenário atual, {(inv1.Profitability > inv2.Profitability ? inv1.InvestmentType : inv2.InvestmentType)} é uma opção mais vantajosa que {(inv1.Profitability > inv2.Profitability ? inv2.InvestmentType : inv1.InvestmentType)}. No entanto, é importante lembrar que essas condições são sazonais e podem sofrer alterações.";

            return await Task.FromResult(comparison);
        }

        private float CalculateGrossProfitability(FixedIncomeCalculatorEntity investment)
        {
            float baseProfitability;

            if (!Enum.TryParse(investment.Modality, out Modality modalityEnum))
                throw new ArgumentOutOfRangeException(nameof(investment.Modality));

            switch (modalityEnum)
            {
                case Modality.PRE:
                    baseProfitability = investment.Profitability;
                    break;

                case Modality.CDI:
                    float cdiRate = 10.4f;
                    baseProfitability = (investment.Profitability / 100) * cdiRate;
                    break;

                case Modality.IPCA:
                    float ipcaRate = 4.5f;
                    baseProfitability = investment.Profitability + ipcaRate;
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(investment.Modality));
            }

            return baseProfitability;
        }

        private float CalculateTaxRate(string type, int months)
        {
            if (type == InvestmentType.LCI.ToString() ||
                type == InvestmentType.LCA.ToString() ||
                type == InvestmentType.CRI.ToString() ||
                type == InvestmentType.CRA.ToString() ||
                type == InvestmentType.DEBENTUREINCENTIVADA.ToString())
            {
                return 0;
            }

            int days = months * 30;

            if (days <= 180)
                return 0.225f;
            else if (days <= 360)
                return 0.20f;
            else if (days <= 720)
                return 0.175f;
            else
                return 0.15f;
        }
    }
}
