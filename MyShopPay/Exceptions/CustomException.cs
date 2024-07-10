using System.Net;

namespace MyShopPay.Exceptions;

internal abstract class CustomException : Exception
{
    public abstract HttpStatusCode HttpStatusCode { get; }
}
