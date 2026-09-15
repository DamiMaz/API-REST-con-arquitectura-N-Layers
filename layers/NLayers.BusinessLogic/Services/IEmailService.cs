namespace NLayers.BusinessLogic.Services;

public interface IEmailService
{
    Task SendAsync(string email, string message);
}
