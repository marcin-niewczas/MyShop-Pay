using System.ComponentModel.DataAnnotations;

namespace MyShopPay.Dtos;

internal sealed record PaymentCard
{
    [Required(ErrorMessage = $"The Credit Card Number is required")]
    [RegularExpression("^[0-9]{4} [0-9]{4} [0-9]{4} [0-9]{4}$", ErrorMessage = $"The Credit Card Number is required")]
    public string CreditCardNumber { get; set; } = default!;
    [Required(ErrorMessage = $"The {nameof(Expires)} is required")]
    [RegularExpression("^[0-9]{2}/[0-9]{2}$", ErrorMessage = $"The {nameof(Expires)} is required")]
    public string Expires { get; set; } = default!;
    [Required(ErrorMessage = $"The {nameof(CVC)} is required")]
    [RegularExpression("^[0-9]{3}$", ErrorMessage = $"The {nameof(CVC)} is required")]
    public string CVC { get; set; } = default!;
}
