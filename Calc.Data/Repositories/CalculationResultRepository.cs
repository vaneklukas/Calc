using Calc.Data.Interfaces;
using Calc.Data.Models;
using Microsoft.Extensions.Logging;

namespace Calc.Data.Repositories;

public class CalculationResultRepository:GenericRepository<CalculationResult>, ICalculationResultRepository
{
    public CalculationResultRepository(ApplicationDbContext context, ILogger logger) : base(context, logger)
    {
    }

    public IEnumerable<CalculationResult> GetLastResults()
    {
        return _dbSet.OrderByDescending(x => x.CalculationResultId)
            .Take(10).ToList();
    }
}