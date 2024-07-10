namespace MyShopPay.DataAccessLayer.Models;

internal sealed class User : BaseEntity
{
    public string Username { get; private set; } = default!;
    public string SecuredPassword { get; private set; } = default!;

    public User(string username, string securedPassword) : base()
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(username, nameof(username));
        ArgumentException.ThrowIfNullOrWhiteSpace(securedPassword, nameof(securedPassword));

        Username = username;
        SecuredPassword = securedPassword;
    }
}
