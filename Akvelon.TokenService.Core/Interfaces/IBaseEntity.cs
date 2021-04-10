using System;

namespace Akvelon.TokenService.Core.Interfaces
{
    /// <summary>
    /// Base Entity
    /// </summary>
    public interface IBaseEntity
    {
        /// <summary>
        /// Id
        /// </summary>
        Guid Id { get; set; }
    }
}