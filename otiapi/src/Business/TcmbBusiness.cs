using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace OtiBusiness
{
    public static class TcmbBusiness
    {
        public static GetCurrencyResponse GetWebData(this GetCurrencyRequest request)
        {
            GetCurrencyResponse Response = new GetCurrencyResponse(){ CurrencyList = new List<Currency>() };
            try
            {
                string url = "https://www.tcmb.gov.tr/kurlar/today.xml";

                Response.CurrencyList = XDocument
                    .Load(url)
                    .Root
                    .Elements("Currency")
                    .Select(p => new Currency
                    {
                        CurrencyCode = p.Attribute("CurrencyCode").Value,
                        CrossOrder = p.Attribute("CrossOrder").Value,
                        Kod = p.Attribute("Kod").Value,
                        Unit = (string)p.Element("Unit"),
                        Isim = (string)p.Element("Isim"),
                        CurrencyName = (string)p.Element("CurrencyName"),
                        ForexBuying = (decimal?)(string.IsNullOrEmpty((string)p.Element("ForexBuying")) ? null : p.Element("ForexBuying")),
                        ForexSelling = (decimal?)(string.IsNullOrEmpty((string)p.Element("ForexSelling")) ? null : p.Element("ForexSelling")),
                        BanknoteBuying = (decimal?)(string.IsNullOrEmpty((string)p.Element("BanknoteBuying")) ? null : p.Element("BanknoteBuying")),
                        BanknoteSelling = (decimal?)(string.IsNullOrEmpty((string)p.Element("BanknoteSelling")) ? null : p.Element("BanknoteSelling")),
                        CrossRateUSD = (decimal?)(string.IsNullOrEmpty((string)p.Element("CrossRateUSD")) ? null : p.Element("CrossRateUSD")),
                        CrossRateOther = (decimal?)(string.IsNullOrEmpty((string)p.Element("CrossRateOther")) ? null : p.Element("CrossRateOther"))
                    })
                    .ToList();
            }
            catch (Exception ex)
            {
                Response.Success = false;
                Response.ErrorDescription = ex.Message;
            }
            return Response;
        }

        public static List<Currency> SortAndFilterData(this List<Currency> CurrencyList,GetCurrencyRequest request)
        {
            try
            {
                if (!string.IsNullOrEmpty(request.CurrencyType))
                {
                    CurrencyList = CurrencyList.FindAll(x => x.CurrencyCode == request.CurrencyType);
                }

                switch (request.SortCostType)
                {
                    case SortCostEnums.BanknoteBuying:
                        switch (request.OrderByType)
                        {
                            case OrderByEnums.DESCENDING:
                                CurrencyList = CurrencyList
                                                .Where(x => x.BanknoteBuying != null)
                                                .OrderByDescending(x => x.BanknoteBuying)
                                                .ToList();
                                break;
                            case OrderByEnums.ASCENDING:
                                CurrencyList = CurrencyList
                                                .Where(x => x.BanknoteBuying != null)
                                                .OrderBy(x => x.BanknoteBuying)
                                                .ToList();
                                break;
                            default:
                                break;
                        }
                        break;
                    case SortCostEnums.BanknoteSelling:
                        switch (request.OrderByType)
                        {
                            case OrderByEnums.DESCENDING:
                                CurrencyList = CurrencyList
                                                .Where(x => x.BanknoteSelling != null)
                                                .OrderByDescending(x => x.BanknoteSelling)
                                                .ToList();
                                break;
                            case OrderByEnums.ASCENDING:
                                CurrencyList = CurrencyList
                                                .Where(x => x.BanknoteSelling != null)
                                                .OrderBy(x => x.BanknoteSelling)
                                                .ToList();
                                break;
                            default:
                                break;
                        }
                        break;
                    case SortCostEnums.ForexBuying:
                        switch (request.OrderByType)
                        {
                            case OrderByEnums.DESCENDING:
                                CurrencyList = CurrencyList
                                                .Where(x => x.ForexBuying != null)
                                                .OrderByDescending(x => x.ForexBuying)
                                                .ToList();
                                break;
                            case OrderByEnums.ASCENDING:
                                CurrencyList = CurrencyList
                                                .Where(x => x.ForexBuying != null)
                                                .OrderBy(x => x.ForexBuying)
                                                .ToList();
                                break;
                            default:
                                break;
                        }
                        break;
                    case SortCostEnums.ForexSelling:
                        switch (request.OrderByType)
                        {
                            case OrderByEnums.DESCENDING:
                                CurrencyList = CurrencyList
                                                .Where(x => x.ForexSelling != null)
                                                .OrderByDescending(x => x.ForexSelling)
                                                .ToList();
                                break;
                            case OrderByEnums.ASCENDING:
                                CurrencyList = CurrencyList
                                                .Where(x => x.ForexSelling != null)
                                                .OrderBy(x => x.ForexSelling)
                                                .ToList();
                                break;
                            default:
                                break;
                        }
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            { 
                throw ex;
            }
            return CurrencyList;
        }
    }
}
