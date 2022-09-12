using Calc.Data.Models;

namespace Calc.Data.Interfaces;

public interface ICalculationResultRepository:IGenericRepository<CalculationResult>
{
    IEnumerable<CalculationResult> GetLastResults();
}