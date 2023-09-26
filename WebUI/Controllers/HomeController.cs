using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RiseX.Business.Abstract;
using System.Data.SqlTypes;

namespace RiseX.WebUI.Controllers;

[Authorize]
public class HomeController : Controller
{
    private readonly ICryptoService _cryptoService;

    public HomeController(ICryptoService cryptoService)
    {
        _cryptoService = cryptoService;
    }

    [HttpGet]
    public async Task<IActionResult> Index(DateTime startDate,DateTime endDate)
    {
        var result = await _cryptoService.GetCryptoAssetHistoryAsync(startDate, endDate);
        return View(result);
    }
}
