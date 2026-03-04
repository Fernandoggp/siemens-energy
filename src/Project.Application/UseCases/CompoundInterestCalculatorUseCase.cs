using Deviot.Common;
using Microsoft.Extensions.Logging;
using Project.Application.Base;
using Project.Domain.Interfaces.UseCases;

namespace Project.Application.UseCases
{
    public class CompoundInterestCalculatorUseCase: UseCaseBase, ICompoundInterestCalculatorUseCase
    {
        public CompoundInterestCalculatorUseCase(INotifier notifier, ILogger<CompoundInterestCalculatorUseCase> logger) : base(notifier, logger)
        {
        }

        public Task<dynamic> ExecuteAsync(float inicialValue, float monthlyValue, float profitability, bool profitabilityIsAnnual, int time, bool timeIsAnnual, float desiredValue)
        {
            var yearlyDetails = new List<dynamic>();
            float monthlyProfitability;

            if (profitabilityIsAnnual)
            {
                monthlyProfitability = profitability / 100 / 12;
            }
            else
            {
                monthlyProfitability = profitability / 100;
            }

            float totalInvested = inicialValue;
            float totalInterest = 0;
            float total = inicialValue;

            int totalMonths = timeIsAnnual ? time * 12 : time;

            for (int month = 1; month <= totalMonths; month++)
            {
                float interestThisMonth = total * monthlyProfitability;

                total += interestThisMonth; 
                total += monthlyValue;      

                totalInvested += monthlyValue; 
                totalInterest += interestThisMonth; 

                if (month % 12 == 0 || month == totalMonths)
                {
                    bool goalAchieved = desiredValue <= total;

                    yearlyDetails.Add(new
                    {
                        Year = (int)Math.Ceiling(month / 12.0),
                        Invested = Math.Round(totalInvested, 2),
                        Interest = Math.Round(totalInterest, 2),
                        Total = Math.Round(total, 2),
                        GoalAchieved = goalAchieved
                    });
                }
            }

            return Task.FromResult<dynamic>(new
            {
                TotalInvested = Math.Round(totalInvested, 2),
                TotalInterest = Math.Round(totalInterest, 2),
                Total = Math.Round(total, 2),
                YearlyDetails = yearlyDetails
            });
        }

    }
}
