using Calc.Business.ViewModels;

namespace Calc.Business.Interfaces;

public interface ICalculationManager
{
    Task<double> GetCalcResult(CalculationInputViewModel calculationInputViewModel);
    IEnumerable<LastCalculationResultViewModel> GetLastCalcRecords();
    
}