public class webResponseBase
{
    public webResponseBase()
    {
        Success = true;
    }

    public bool Success { get; set; }
    public string ErrorCode { get; set; }
    public string ErrorDescription { get; set; }
}