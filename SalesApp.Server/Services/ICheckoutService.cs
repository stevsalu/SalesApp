using SalesApp.Server.DTOs;

namespace SalesApp.Server.Services;
public interface ICheckoutService {
    Task<CheckoutResult> ProcessAsync(CheckoutRequest dto);

}

