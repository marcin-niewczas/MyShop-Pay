using System.Net;

namespace MyShopPay.Exceptions;

internal sealed class BadRequestException : CustomException
{
    public override HttpStatusCode HttpStatusCode => HttpStatusCode.BadRequest;
}
