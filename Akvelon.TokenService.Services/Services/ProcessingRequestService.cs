using System;
using System.Threading.Tasks;
using Akvelon.TokenService.Core.DTO;
using Akvelon.TokenService.Core.Models;
using Akvelon.TokenService.DataLayer.Repository;
using Akvelon.TokenService.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Akvelon.TokenService.Services.Services
{
    public class ProcessingRequestService : IProcessingRequestService
    {
        private const string Pattern = "{ph}";
        private readonly TokenDbContext _context;
        private readonly IHttpService _httpService;

        public ProcessingRequestService(TokenDbContext context, IHttpService httpService)
        {
            _context = context;
            _httpService = httpService;
        }

        #region Public

        public async Task<ResultDto> ProcessingRequest(string ip, string userAgent, string token, string callback,
            string ph)
        {
            var result = new ResultDto();
            
            var clickId = Guid.NewGuid();
            var callbackUrl = ReplacePlaceHolder(callback, ph);
            
            var request = GetRequest(clickId, ip, userAgent, token, callbackUrl);

            await _context.Requests.AddAsync(request);
            await _context.SaveChangesAsync();
            
            if (!string.IsNullOrEmpty(callbackUrl))
                result = await ProcessingCallback(callbackUrl, clickId);

            return result;
        }

        #endregion
        
        #region Private

        private static Request GetRequest(Guid clickId, string ip, string userAgent, string token, string callbackUrl)
        {
            return new Request
            {
                Id = clickId,
                RequestDateTime = DateTime.UtcNow,
                Ip = ip,
                UserAgent = userAgent,
                Token = token,
                Callback = new Callback
                {
                    Id = clickId,
                    CallbackUrl = callbackUrl,
                },
            };
        }

        /// <summary>
        /// Обработка вызова Url
        /// </summary>
        /// <param name="url">URL-адрес</param>
        /// <param name="id">ID записи в БД</param>
        private async Task<ResultDto> ProcessingCallback(string url, Guid id)
        {
            var model = await _context.Callbacks.FirstOrDefaultAsync(_ => _.Id == id);

            if (model == null)
                throw new Exception($"Could not find an entry in the table with ID {id}");
            
            if (!Uri.IsWellFormedUriString(url, UriKind.Absolute))
                throw new Exception($"Wrong URL format!");
            
            var statusCode = await _httpService.Get(url);
            
            model.DateTime = DateTime.UtcNow;
            model.HttpResponseCode = statusCode;

            await _context.SaveChangesAsync();

            return ToDto(model);
        }

        /// <summary>
        /// Заменяет часть строки URL на указанный образец
        /// </summary>
        /// <param name="callback">URL адрес</param>
        /// <param name="ph">Строка для замены</param>
        private static string ReplacePlaceHolder(string callback, string ph)
        {
            if (string.IsNullOrEmpty(callback)) return string.Empty;
            
            var hasPlaceHolder = callback.IndexOf(Pattern, StringComparison.Ordinal) >= 0;
            if (hasPlaceHolder)
            {
                callback = callback.Replace(Pattern, ph);
            }

            return callback;
        }

        private static ResultDto ToDto(Callback model)
        {
            return new ResultDto
            {
                Id = model.Id,
                RequestDateTime = model.Request.RequestDateTime,
                Token = model.Request.Token,
                Ip = model.Request.Ip,
                UserAgent = model.Request.UserAgent,
                CallbackUrl = model.CallbackUrl,
                DateTime = model.DateTime,
                HttpResponseCode = model.HttpResponseCode,
            };
        }

        #endregion
    }
}