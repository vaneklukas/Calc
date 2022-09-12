using System.Diagnostics;
using System.Globalization;
using System.Net;
using Calc.Business.Interfaces;
using Calc.Business.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Calc.Models;
using static System.Char;
using static System.Double;

namespace Calc.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private ICalculationManager _calculationManager;

    public HomeController(ILogger<HomeController> logger, ICalculationManager calculationManager)
    {
        _logger = logger;
        _calculationManager = calculationManager;
    }

    public IActionResult Index()
    {
        return View();
    }
    [HttpPost]
    public async Task<JsonResult> GetCalcResult(string numberOne, string numberTwo, 
        string mathOperator, bool resultIntegers)
    {
        CalculationInputViewModel viewModel = new CalculationInputViewModel();
        try
        {
            viewModel.FirstInputNumber = Parse(numberOne,CultureInfo.InvariantCulture);
            viewModel.SecondInputNumber= Parse(numberTwo,CultureInfo.InvariantCulture);
            viewModel.MathOperator = char.Parse(mathOperator);
            viewModel.ResultIntegers = resultIntegers;
            return Json( _calculationManager.GetCalcResult(viewModel));
        }
        catch (Exception e)
        {
            _logger.LogError("{Error}",e.ToString());
            return new JsonResult("Chyba aplikace"){
                StatusCode = (int)HttpStatusCode.InternalServerError};
        }
    }

    [HttpGet]
    public IActionResult GetCalcResult()
    {
        return Json( _calculationManager.GetLastCalcRecords());
    }
}