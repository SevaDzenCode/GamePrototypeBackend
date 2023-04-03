namespace GamePrototypeBackend.BL.Services.Interfaces
{
    public interface IMessageSender
    {
        Task SendEmailAsync(string email, string message);
    }
}
