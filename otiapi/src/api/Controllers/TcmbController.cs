using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net.Mime;
using OtiBusiness;
using System.Net.Http;
using System.IO;
using System.Net;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;
using CsvHelper;
using System.Text;

namespace OtiApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TcmbController : ControllerBase
    {
        private readonly ILogger<TcmbController> _logger;

        public TcmbController(ILogger<TcmbController> logger)
        {
            _logger = logger;
        }

        [HttpPost("GetCurrency.{format}"), FormatFilter]
        public GetCurrencyResponse GetCurrency(GetCurrencyRequest request)
        {
            GetCurrencyResponse Response = new GetCurrencyResponse() { CurrencyList = new List<Currency>() };
            var res = request.GetWebData();
            if (!res.Success)
            {
                Response.Success = res.Success;
                Response.ErrorCode = res.ErrorCode;
                Response.ErrorDescription = res.ErrorDescription;
            }
            var sortedData = res.CurrencyList.SortAndFilterData(request);
            Response.CurrencyList = sortedData;

            return Response;
        }
        [HttpPost("GetCurrencyCsv.{format}"), FormatFilter]
        public FileResult GetCurrencyCsv(GetCurrencyRequest request) {
            var Response = new GetCurrencyResponse() { CurrencyList = new List<Currency>() };
            var res = request.GetWebData();
            if (!res.Success)
            {
                Response.Success = res.Success;
                Response.ErrorCode = res.ErrorCode;
                Response.ErrorDescription = res.ErrorDescription;
            }

            var sortedData = res.CurrencyList.SortAndFilterData(request);

            var sb = new StringBuilder();
            sb.AppendLine("crossOrder"+";"+"kod"+";"+"currencyCode" +";"+"unit"+";"+"isim"+";"+"currencyName"+";"
            +"forexBuying"+";"+"forexSelling"+";"+"banknoteBuying"+";"+"banknoteSelling"+";"+"crossRateUSD"+";"+"crossRateOther");
            foreach (var data in sortedData)
            {
                sb.AppendLine(data.CrossOrder+";"+data.Kod+";"+data.CurrencyCode+";"+data.Unit+";"+data.Isim+";"+data.CurrencyName+","
                +data.ForexBuying+";"+data.ForexSelling+";"+data.BanknoteBuying+";"+data.BanknoteSelling+";"+data.CrossRateUSD+";"+data.CrossRateOther);
            }
            return File(new UTF8Encoding().GetBytes(sb.ToString()), "text/csv", "export.csv");
        }
    }
}