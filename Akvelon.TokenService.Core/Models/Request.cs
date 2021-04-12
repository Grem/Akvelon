using System;
using Akvelon.TokenService.Core.Interfaces;

namespace Akvelon.TokenService.Core.Models
{
    public class Request : IBaseEntity
    {
        public Guid Id { get; set; }
        public DateTime RequestDateTime { get; set; }
        public string Token { get; set; }
        public string Ip { get; set; }
        public string UserAgent { get; set; }
        
        public virtual Callback Callback { get; set; }
    }
}