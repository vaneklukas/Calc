using Calc.Business.Interfaces;
using Calc.Business.ViewModels;
using Calc.Data.Interfaces;
using Calc.Data.Models;
using Microsoft.Extensions.Logging;

namespace Calc.Business.Managers;

public class CalculationManager:ICalculationManager
{
    private IUnitOfWork _repo;
    private readonly ILogger<CalculationManager> _logger;


    public CalculationManager(IUnitOfWork repo, ILogger<CalculationManager> logger)
    {
        _repo = repo;
        _logger = logger;
    }

    public string GetHello()
    {
        return "hello";
    }
    public async Task<double> GetCalcResult(CalculationInputViewModel calculationInputViewModel)
    {
        try
        {
            
            if (calculationInputViewModel.SecondInputNumber==0 &&calculationInputViewModel.MathOperator=='/')
            {
                return 0;
            }

            double calcResult = 0;
            switch (calculationInputViewModel.MathOperator)
            {
                case '+' :
                    calcResult = calculationInputViewModel.FirstInputNumber + 
                                 calculationInputViewModel.SecondInputNumber;
                    break;
                case '-' :
                    calcResult = calculationInputViewModel.FirstInputNumber - 
                                 calculationInputViewModel.SecondInputNumber;
                    break;
                case '/' :
                    calcResult = calculationInputViewModel.FirstInputNumber / 
                                 calculationInputViewModel.SecondInputNumber;
                    break;
                case '*' :
                    calcResult = calculationInputViewModel.FirstInputNumber * 
                                 calculationInputViewModel.SecondInputNumber;
                    break;
                default:
                    calcResult = 0;
                    break;
            }

            if (calculationInputViewModel.ResultIntegers)
            {
                calcResult = Math.Round(calcResult, 0);
            }
            
            CalculationResult calculationResult=new CalculationResult
            {
                Result = calcResult
            };
            if ( await _repo.CalculationResultRepository.Add(calculationResult))
            {
                 await _repo.SaveDataAsync();
            }
            
            return calcResult;
        }
        catch (Exception e)
        {
            _logger.LogDebug("{Error}",e.ToString());
            return 0;
        }
    }

    public IEnumerable<LastCalculationResultViewModel> GetLastCalcRecords()
    {
        IEnumerable<LastCalculationResultViewModel> viewModel = _repo.CalculationResultRepository
            .GetLastResults()
            .Select(x => new LastCalculationResultViewModel
            {
                CalculationResultId = x.CalculationResultId,
                Result = x.Result
            }).ToList();

        return viewModel;
    }
}