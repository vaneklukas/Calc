using Calc.Data.Interfaces;
using Microsoft.Extensions.Logging;

namespace Calc.Data.Repositories;

public class UnitOfWork:IUnitOfWork, IDisposable
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger _logger;
    
    public ICalculationResultRepository CalculationResultRepository { get; }

    public UnitOfWork(ApplicationDbContext context, ILoggerFactory logger)
    {
        _context = context;
        _logger = logger.CreateLogger("logs");
        CalculationResultRepository = new CalculationResultRepository(_context,_logger);
    }
    public async Task SaveDataAsync()
    {
        await _context.SaveChangesAsync();
    }
    public void Dispose()
    {
        _context.Dispose();
    }
}