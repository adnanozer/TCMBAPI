public class GetCurrencyRequest
{
    public OrderByEnums? OrderByType { get; set; }
    public SortCostEnums? SortCostType { get; set; }
    public string CurrencyType { get; set; }
}