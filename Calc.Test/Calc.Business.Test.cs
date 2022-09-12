using System.Threading.Tasks;
using Calc.Business.Managers;
using Calc.Business.ViewModels;
using Calc.Data.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace Calc.Test;

public class Tests
{
    private readonly CalculationManager _manager;

    public Tests()
    {
        var repositoryMock = new Mock<ICalculationResultRepository>();
        var unitOfworkMock = new Mock<IUnitOfWork>();
        unitOfworkMock.Setup(uow => uow.CalculationResultRepository).Returns(repositoryMock.Object);
        _manager = new CalculationManager(unitOfworkMock.Object, Mock.Of<ILogger<CalculationManager>>());
    }

    
    [Test]
    public async Task CalcSum()
    {
        CalculationInputViewModel model = new CalculationInputViewModel
        {
            FirstInputNumber = 1.0,
            SecondInputNumber = 1.0,
            MathOperator = '+',
            ResultIntegers = false
        };
        
        Assert.AreEqual(2.0, await _manager.GetCalcResult(model) );
        
    }
    [Test]
    public async Task CalcSub()
    {
        CalculationInputViewModel model = new CalculationInputViewModel
        {
            FirstInputNumber = 1.0,
            SecondInputNumber = 1.0,
            MathOperator = '-',
            ResultIntegers = false
        };
        
        Assert.AreEqual(0.0, await _manager.GetCalcResult(model) );
        
    }
    [Test]
    public async Task CalcMul()
    {
        CalculationInputViewModel model = new CalculationInputViewModel
        {
            FirstInputNumber = 17.0,
            SecondInputNumber = 10.0,
            MathOperator = '*',
            ResultIntegers = false
        };
        
        Assert.AreEqual(170.0, await _manager.GetCalcResult(model) );
        
    }
    [Test]
    public async Task CalcDiv()
    {
        CalculationInputViewModel model = new CalculationInputViewModel
        {
            FirstInputNumber = 13.2,
            SecondInputNumber = 1.2,
            MathOperator = '/',
            ResultIntegers = false
        };
        
        Assert.AreEqual(11, await _manager.GetCalcResult(model) );
    }
    [Test]
    public async Task CalcDivByZero()
    {
        CalculationInputViewModel model = new CalculationInputViewModel
        {
            FirstInputNumber = 1.0,
            SecondInputNumber = 0,
            MathOperator = '/',
            ResultIntegers = false
        };
        
        Assert.AreEqual(0.0, await _manager.GetCalcResult(model) );
        
    }
    [Test]
    public async Task CalcSumInt()
    {
        CalculationInputViewModel model = new CalculationInputViewModel
        {
            FirstInputNumber = 1.5,
            SecondInputNumber = 1.2,
            MathOperator = '+',
            ResultIntegers = true
        };
        
        Assert.AreEqual(3.0, await _manager.GetCalcResult(model) );
        
    }
    [Test]
    public async Task CalcSubInt()
    {
        CalculationInputViewModel model = new CalculationInputViewModel
        {
            FirstInputNumber = 1.2,
            SecondInputNumber = 1.0,
            MathOperator = '-',
            ResultIntegers = true
        };
        
        Assert.AreEqual(0.0, await _manager.GetCalcResult(model) );
        
    }
    [Test]
    public async Task CalcMulInt()
    {
        CalculationInputViewModel model = new CalculationInputViewModel
        {
            FirstInputNumber = 17.5,
            SecondInputNumber = 9.0,
            MathOperator = '*',
            ResultIntegers = true
        };
        
        Assert.AreEqual(158.0, await _manager.GetCalcResult(model) );
        
    }
    [Test]
    public async Task CalcDivInt()
    {
        CalculationInputViewModel model = new CalculationInputViewModel
        {
            FirstInputNumber = 138,
            SecondInputNumber = 7.3,
            MathOperator = '/',
            ResultIntegers = true
        };
        
        Assert.AreEqual(19, await _manager.GetCalcResult(model) );
    }
}