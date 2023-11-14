using Data.Entity;

namespace Bussines.Services
{
    public interface IEmailService
    {
        bool Email(EmailEntity email);
    }
}
