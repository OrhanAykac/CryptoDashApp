using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Business.Abstract;

namespace WebUI.Controllers;

[Authorize]
public class HomeController : Controller
{
    private readonly ICryptoService _cryptoService;

    public HomeController(ICryptoService cryptoService)
    {
        _cryptoService = cryptoService;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> GetCryptoChartDatasByDateRange(DateTime startDate, DateTime endDate)
    {
        var result = await _cryptoService.GetCryptoAssetHistoryAsync(startDate, endDate);
        return Json(result);
    }
}
