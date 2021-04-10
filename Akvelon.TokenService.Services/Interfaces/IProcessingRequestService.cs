using System.Threading.Tasks;

namespace Akvelon.TokenService.Services.Interfaces
{
    public interface IProcessingRequestService
    {
        /// <summary>
        /// Обработка запроса
        /// </summary>
        /// <param name="token">Токен</param>
        /// <param name="callback">URL</param>
        /// <param name="ph">Строка для замены в URL</param>
        /// <returns>URL - если callback не пуст и удалось корректно обработать строку, иначе - пустая строка</returns>
        Task<string> ProcessingRequest(string token, string callback, string ph);
    }
}