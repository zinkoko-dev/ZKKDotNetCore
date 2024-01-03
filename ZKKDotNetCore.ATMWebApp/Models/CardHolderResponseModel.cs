namespace ZKKDotNetCore.ATMWebApp.Models;

public class CardHolderResponseModel
{
    public string CardNumber { get; set; }
    public int Pin { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public Decimal Balance { get; set; }
    public Decimal DepositAmt { get; set; }
    public Decimal WithdrawAmt { get; set; }
}

public class MessageModel
{
    public MessageModel() { }
    public MessageModel(bool isSuccess, string message)
    {
        IsSuccess = isSuccess;
        Message = message;
    }
    public bool IsSuccess { get; set; }
    public string Message { get; set; }
}