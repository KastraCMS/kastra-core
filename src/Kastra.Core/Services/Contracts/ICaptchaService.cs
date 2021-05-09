/*
 * Licensed under the Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0)
 * See https://github.com/KastraCMS/kastra-core for more information concerning
 * the license and the contributors participating to this project.
 */

using System.Threading.Tasks;

namespace Kastra.Core.Services.Contracts
{
    public interface ICaptchaService
    {
        /// <summary>
        /// Verify the token.
        /// </summary>
        /// <param name="secret">Secret key</param>
        /// <param name="token">Token received</param>
        /// <param name="remoteIp">Remote Ip</param>
        /// <returns>True if the token is correct</returns>
        Task<bool> Verify(string secret, string token, string remoteIp);
    }
}
