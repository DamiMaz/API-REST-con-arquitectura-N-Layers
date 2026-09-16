namespace NLayers.BusinessLogic.Services;

public class EmailService : IEmailService
{
    public Task SendAsync(string email, string message)
    {
        // Implementación de envío de correo (ej. SMTP, Google, etc.)
        return Task.CompletedTask;
    }
}
