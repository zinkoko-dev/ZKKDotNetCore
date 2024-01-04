using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZKKDotNetCore.ATMWebApp.EFDbContext;
using ZKKDotNetCore.ATMWebApp.Models;

namespace ZKKDotNetCore.ATMWebApp.Controllers;

public class AtmActionController : Controller
{
    private readonly AppDbContext _appDbContext;
    private const string SessionKeyUserId = "_AuthenticatedUser";
    private const string SessionKeyUserFirstName = "_AuthenticatedUserName";
    private int? userId;
    public AtmActionController(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public bool CheckAuthUserId()
    {
        userId = HttpContext.Session.GetInt32(SessionKeyUserId);
        return userId != null;
    }

    public IActionResult Register()
    {
        if (CheckAuthUserId())
        {
            return Redirect("/atmaction/home");
        }
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> SaveRegister(CardHolderDataModel reqModel)
    {
        var model = new CardHolderDataModel()
        {
            FirstName = reqModel.FirstName,
            LastName = reqModel.LastName,
            CardNumber = reqModel.CardNumber,
            Pin = reqModel.Pin,
            Balance = 0
        };

        await _appDbContext.CardHolders.AddAsync(model);
        var result = await _appDbContext.SaveChangesAsync();

        TempData["Message"] = result > 0 ?"Register Successful !!": "Register Fail !!";
        TempData["IsSuccess"] = result > 0 ? true : false;
        return View("Register");
    }

    // GET
    [ActionName("Index")]
    public IActionResult AtmActionIndex()
    {
        if (CheckAuthUserId())
        {
            return Redirect("/atmaction/home");
        }
        return View("AtmActionIndex");
    }

    [ActionName("Home")]
    public IActionResult AtmActionHome()
    {
        if (CheckAuthUserId())
        {
            return View("AtmActionHome");
        }
        TempData["Message"] = "Please Login !!";
        return Redirect("/");
    }

    [HttpPost]
    public IActionResult Login(CardHolderResponseModel reqModel)
    {
        CardHolderDataModel cardHolderDataModel = _appDbContext.GetCardHolderByCardNumAndPin(reqModel.CardNumber, reqModel.Pin);
        if (cardHolderDataModel is null)
        {
            TempData["Message"] = "Login Fail !! Please Enter Again";
            return Redirect("/");
        }
        
        HttpContext.Session.SetInt32(SessionKeyUserId, cardHolderDataModel.Id);
        HttpContext.Session.SetString(SessionKeyUserFirstName, cardHolderDataModel.FirstName);
        return Redirect("/atmaction/home");
    }

    [HttpPost]
    public async Task<IActionResult> Deposit(CardHolderResponseModel reqModel)
    {
        if (reqModel.DepositAmt <= 0)
        {
            MessageModel model = new MessageModel(false, "Deposit amount should be greater than zero.");
            return Json(model);
        }

        if (!CheckAuthUserId())
        {
            MessageModel model = new MessageModel(false, "User not found.");
            return Json(model);
        }

        var cardHolder = await _appDbContext.CardHolders.FirstOrDefaultAsync(x => x.Id == userId);
        if (cardHolder is null)
        {
            MessageModel model = new MessageModel(false, "Card holder not found.");
            return Json(model);
        }

        cardHolder.Balance += reqModel.DepositAmt;
        var saveResult = await _appDbContext.SaveChangesAsync();

        if (saveResult > 0)
        {
            MessageModel model = new MessageModel(true, "Deposit successful.");
            return Json(model);
        }
        else
        {
            MessageModel model = new MessageModel(false, "Failed to save deposit.");
            return Json(model);
        }
    }
    
    [HttpPost]
    public async Task<IActionResult> Withdraw(CardHolderResponseModel reqModel)
    {
        if (reqModel.WithdrawAmt <= 0)
        {
            MessageModel model = new MessageModel(false, "Withdraw amount should be greater than zero.");
            return Json(model);
        }
        
        if (!CheckAuthUserId())
        {
            MessageModel model = new MessageModel(false, "User not found.");
            return Json(model);
        }

        var cardHolder = await _appDbContext.CardHolders.FirstOrDefaultAsync(x => x.Id == userId);
        if (cardHolder is null)
        {
            MessageModel model = new MessageModel(false, "Card holder not found.");
            return Json(model);
        }

        if (reqModel.WithdrawAmt > cardHolder.Balance)
        {
            MessageModel model = new MessageModel(false, "Not enough balance.");
            return Json(model);
        }

        cardHolder.Balance -= reqModel.WithdrawAmt;
        var saveResult = await _appDbContext.SaveChangesAsync();

        if (saveResult > 0)
        {
            MessageModel model = new MessageModel(true, "Withdraw successful.");
            return Json(model);
        }
        else
        {
            MessageModel model = new MessageModel(false, "Failed to save withdraw.");
            return Json(model);
        }
    }

    public async Task<IActionResult> ShowBalance()
    {
        MessageModel model = new MessageModel();
        if (!CheckAuthUserId())
        {
            model = new MessageModel(false, "User not found.");
            return Json(model);
        }

        var cardHolder = await _appDbContext.CardHolders.FirstOrDefaultAsync(x => x.Id == userId);
        if (cardHolder is null)
        {
            model = new MessageModel(false, "Card holder not found.");
            return Json(model);
        }

        decimal balance = cardHolder.Balance;
        model = new MessageModel(true, balance.ToString());
        return Json(model);
    }
    
    public IActionResult Exit()
    {
        HttpContext.Session.Clear();
        return Redirect("/");
    }
}