using Microsoft.AspNetCore.Mvc;
using WebApp2byJessica.Services;

namespace WebApp2byJessica.Services
{
    public interface IGreetingService
    {
        string GetGreeting();
    }
}
