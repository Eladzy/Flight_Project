using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using FlightManagerProject;

namespace FlightManagerWebService
{
    public class JwtTokenHandler:DelegatingHandler
    {
        /// <summary>
        /// validates token is not expired
        /// </summary>
        /// <param name="notBefore"></param>
        /// <param name="expires"></param>
        /// <param name="securityToken"></param>
        /// <param name="validationParameters"></param>
        /// <returns></returns>
        public bool LifetimeValidator(DateTime? notBefore,DateTime? expires,SecurityToken securityToken,TokenValidationParameters validationParameters)
        {
            if (expires != null)
            {
                if (DateTime.UtcNow < expires) return true;
            }
            return false;
        }

        /// <summary>
        /// verifies that the request includes Jwt token and outs a trimmed jwt token
        /// </summary>
        /// <param name="request"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        private static bool TryGetToken(HttpRequestMessage request,out string token)
        {
            token = null;
            IEnumerable<string> authzHeaders;
            if (!request.Headers.TryGetValues("Authorization", out authzHeaders) || authzHeaders.Count() > 1)//if cannot retrieve auth values or values length<1 returns false
            {
                return false;
            }

            string bearerToken = authzHeaders.ElementAt(0);
            token = bearerToken.StartsWith("Bearer ") ? bearerToken.Substring(7) : bearerToken;
            return true;

        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            HttpStatusCode statusCode;
            string token;
            if(!TryGetToken(request,out token))
            {
                statusCode = HttpStatusCode.Unauthorized;
               return  base.SendAsync(request, cancellationToken);//unauthorized token would yield cancellation token
            }
            try
            {//decodes the request message
                const string secretKey = "99C4A955E7274BE9B4D78B0025E04E88D55C6783EF3446C88ACAF9EBC22D9758";
                var securityKey = new SymmetricSecurityKey(System.Text.Encoding.Default.GetBytes(secretKey));


                var handler = new JwtSecurityTokenHandler();

                TokenValidationParameters validationParameters = new TokenValidationParameters
                {
                    ValidAudience = "https://localhost:44375/",
                    ValidIssuer = "https://localhost:44375/",
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    LifetimeValidator = LifetimeValidator,
                    ClockSkew = TimeSpan.FromMinutes(5),
                    IssuerSigningKey = securityKey
                };

                //extract and assign the user of the jwt gets the claims settings
                Thread.CurrentPrincipal = handler.ValidateToken(token, validationParameters, out _);
                HttpContext.Current.User = handler.ValidateToken(token, validationParameters, out _);

                return base.SendAsync(request, cancellationToken);
            }
            catch (SecurityTokenValidationException e)
            {
                ErrorLogger.Logger(e);
                statusCode = HttpStatusCode.Unauthorized;
            }
            catch (Exception e)
            {
                ErrorLogger.Logger(e);
                statusCode = HttpStatusCode.InternalServerError;
            }
            return Task<HttpResponseMessage>.Factory.StartNew(() => new HttpResponseMessage(statusCode) { }, cancellationToken);
        }
    }

    
}