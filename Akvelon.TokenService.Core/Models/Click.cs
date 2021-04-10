using System;
using Akvelon.TokenService.Core.Interfaces;

namespace Akvelon.TokenService.Core.Models
{
    public class Click : IBaseEntity
    {
        public Guid Id { get; set; }
        public DateTime ClickTime { get; set; }
        public string Token { get; set; }
        public string Ip { get; set; }
        public string UserAgent { get; set; }
    }
}