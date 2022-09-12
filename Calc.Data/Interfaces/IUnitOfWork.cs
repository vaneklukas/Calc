namespace Calc.Data.Interfaces;

public interface IUnitOfWork
{
    Task SaveDataAsync();
    void Dispose();

    public ICalculationResultRepository CalculationResultRepository { get;}
   
}
