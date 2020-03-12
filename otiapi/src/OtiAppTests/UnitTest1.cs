using System;
using Xunit;
using OtiApp.Controllers;
using Microsoft.Extensions.Logging;

namespace OtiAppTests
{
    public class UnitTest1
    {
        private readonly ILogger<TcmbController> _logger;

        [Fact]
        public void Test1()
        {
            GetCurrencyRequest req = new GetCurrencyRequest()
            {
                CurrencyType = null,
                OrderByType = OrderByEnums.ASCENDING,
                SortCostType = SortCostEnums.ForexBuying
            };
            TcmbController cont = new TcmbController(_logger);
            Assert.True(cont.GetCurrency(req).Success);
        }

        [Fact]
        public void Test2()
        {
            GetCurrencyRequest req = new GetCurrencyRequest()
            {
                CurrencyType = null,
                OrderByType = OrderByEnums.ASCENDING,
                SortCostType = SortCostEnums.ForexBuying
            }; 
            TcmbController cont = new TcmbController(_logger);
            var result = cont.GetCurrencyCsv(req);
            Assert.NotNull(result);
            Assert.NotNull(result.FileDownloadName);
        }
    }
}
