﻿using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using WebApplication2.Models;
using WebApplication2.ServiceDB;

namespace WebApplication2
{
    public class CustomOAuthProvider : OAuthAuthorizationServerProvider
    {
        private SHOPEntities db = new SHOPEntities();
        private Service service = new Service();
        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            string clientId = string.Empty;
            string clientSecret = string.Empty;
            string symmetricKeyAsBase64 = string.Empty;

            if (!context.TryGetBasicCredentials(out clientId, out clientSecret))
            {
                context.TryGetFormCredentials(out clientId, out clientSecret);
            }

            if (context.ClientId == null)
            {
                context.SetError("invalid_clientId", "client_Id is not set");
                return Task.FromResult<object>(null);
            }

            var audience = AudiencesStore.FindAudience(context.ClientId);

            if (audience == null)
            {
                context.SetError("invalid_clientId", string.Format("Invalid client_id '{0}'", context.ClientId));
                return Task.FromResult<object>(null);
            }

            context.Validated();
            return Task.FromResult<object>(null);
        }

        public override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {

            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

            //Dummy check here, you need to do your DB checks against memebrship system http://bit.ly/SPAAuthCode

            if (service.checkAcc(context.UserName, context.Password) == false)
            {
                context.SetError("invalid_grant", "The user name or password is incorrect");
                //return;
                return Task.FromResult<object>(null);
            }

            var identity = new ClaimsIdentity("JWT");

            
            identity.AddClaim(new Claim(ClaimTypes.Role, "Manager"));
            //identity.AddClaim(new Claim(ClaimTypes.Role, "User"));

            var props = new AuthenticationProperties(new Dictionary<string, string>
                {
                    {
                         "audience", (context.ClientId == null) ? string.Empty : context.ClientId
                    }
                });

            var ticket = new AuthenticationTicket(identity, props);
            context.Validated(ticket);
            return Task.FromResult<object>(null);
        }
    }
}