using System;
using Akvelon.TokenService.Core.Interfaces;

namespace Akvelon.TokenService.Core.Models
{
    public class Callback : IBaseEntity
    {
        public Guid Id { get; set; }
        public string CallbackUrl { get; set; }
        public string HttpResponseCode { get; set; }
        public DateTime? DateTime { get; set; }
        
        public virtual Request Request { get; set; }
    }
}