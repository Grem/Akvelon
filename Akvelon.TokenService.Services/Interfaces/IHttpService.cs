using System.Threading.Tasks;

namespace Akvelon.TokenService.Services.Interfaces
{
    public interface IHttpService
    {
        /// <summary>
        /// Отправить GET-запрос по HTTP
        /// </summary>
        /// <param name="url">Адрес запроса</param>
        Task<string> Get(string url);
    }
}