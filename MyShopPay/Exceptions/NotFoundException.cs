using System.Net;

namespace MyShopPay.Exceptions;

internal sealed class NotFoundException : CustomException
{
    public override HttpStatusCode HttpStatusCode => HttpStatusCode.NotFound;

}