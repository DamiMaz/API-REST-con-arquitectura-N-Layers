namespace NLayers.BusinessLogic.Services;

public class EmailService : IEmailService
{
    public Task SendAsync(string email, string message)
    {
        // ImplementaciÃ³n de envÃ­o de correo (ej. SMTP, Google, etc.)
        return Task.CompletedTask;
    }
}
