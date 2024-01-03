using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZKKDotNetCore.ATMWebApp.Models;

[Table("Tbl_CardHolder")]
public class CardHolderDataModel
{
    [Key]
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string CardNumber { get; set; }
    public int Pin { get; set; }
    public Decimal Balance { get; set; }
}