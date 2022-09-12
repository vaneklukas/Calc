namespace Calc.Business.ViewModels;

public class CalculationInputViewModel
{
    public double FirstInputNumber { get; set; }
    public double SecondInputNumber { get; set; }
    public char MathOperator { get; set; }
    public bool ResultIntegers { get; set; }
}