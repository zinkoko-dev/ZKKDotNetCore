using Microsoft.EntityFrameworkCore;
using ZKKDotNetCore.ATMWebApp.Models;

namespace ZKKDotNetCore.ATMWebApp.EFDbContext;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    
    public DbSet<CardHolderDataModel> CardHolders { get; set; }

    public CardHolderDataModel GetCardHolderByCardNumAndPin(string cardNum, int pin)
    {
        return CardHolders.FirstOrDefault(user => 
            user.CardNumber == cardNum && user.Pin == pin
        );
    }
}