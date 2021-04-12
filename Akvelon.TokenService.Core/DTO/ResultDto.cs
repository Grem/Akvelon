using System;

namespace Akvelon.TokenService.Core.DTO
{
    public class ResultDto
    {
        public Guid Id { get; set; }
        public DateTime RequestDateTime { get; set; }
        public string Token { get; set; }
        public string Ip { get; set; }
        public string UserAgent { get; set; }
        public string CallbackUrl { get; set; }
        public string HttpResponseCode { get; set; }
        public DateTime? DateTime { get; set; }
    }
}