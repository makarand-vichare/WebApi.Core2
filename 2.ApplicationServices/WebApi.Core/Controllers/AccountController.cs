﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApi.Core.IDomainServices.IdentityStores;
using WebApi.Core.Models;
using WebApi.Core.ViewModels.Identity.WebApi;

namespace WebApi.Core.Controllers
{
    /// <summary>
    /// /reference http://bitoftech.net/2014/06/01/token-based-authentication-asp-net-web-api-2-owin-asp-net-identity/
    /// </summary>

    [Authorize]
    [Route("api/Account")]
    public class AccountController : BaseController
    {
        private UserManager<IdentityUserViewModel> userManager;
        private SignInManager<IdentityUserViewModel> signInManager;

        //private AuthenticationManager AuthenticationManager => ;

        //private UserManager<IdentityUserViewModel> UserManager
        //{
        //    get
        //    {
        //        return _userManager ?? AuthenticationHttpContextExtensions.GetUserManager<UserManager<IdentityUserViewModel>>();
        //    }
        //     set
        //    {
        //        _userManager = value;
        //    }
        //}

        private readonly IClientService clientService;

        public AccountController(UserManager<IdentityUserViewModel> _userManager,
            SignInManager<IdentityUserViewModel> _signInManager,
            IClientService _clientService)
        {
            this.userManager = _userManager;
            this.signInManager = _signInManager;
            this.clientService = _clientService;
        }

        // POST api/Account/Register
        [AllowAnonymous]
        [Route("Register")]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new IdentityUserViewModel() {
                            UserName = model.Email,
                            Email = model.Email,
                            DateOfBirth = model.DateOfBirth,
                            Gender = model.Gender,
                            CityId = model.CityId,
                            CountryId = model.CountryId,
                            AboutInfo = model.AboutInfo
            };

            IdentityResult result = await userManager.CreateAsync(user, model.Password);

            IActionResult errorResult = GetErrorResult(result);

            if (errorResult != null)
            {
                return errorResult;
            }

            return Ok();
        }

        // GET api/Account/ExternalLogin
        //[OverrideAuthentication]
        //[HostAuthentication(DefaultAuthenticationTypes.ExternalCookie)]
        //[AllowAnonymous]
        //[Route("ExternalLogin", Name = "ExternalLogin")]
        //public async Task<IActionResult> GetExternalLogin(string provider, string error = null)
        //{
        //    string redirectUri = string.Empty;

        //    if (error != null)
        //    {
        //        return BadRequest(Uri.EscapeDataString(error));
        //    }

        //    if (!User.Identity.IsAuthenticated)
        //    {
        //        return new ChallengeResult(provider);
        //    }

        //    var redirectUriValidationResult = ValidateClientAndRedirectUri(Request, ref redirectUri);

        //    if (!string.IsNullOrWhiteSpace(redirectUriValidationResult))
        //    {
        //        return BadRequest(redirectUriValidationResult);
        //    }

        //    ExternalLoginData externalLogin = ExternalLoginData.FromIdentity(User.Identity as ClaimsIdentity);

        //    if (externalLogin == null)
        //    {
        //        return BadRequest();
        //    }

        //    if (externalLogin.LoginProvider != provider)
        //    {
        //        await AuthenticationHttpContextExtensions.SignOutAsync(HttpContext, IdentityConstants.ExternalScheme);
        //        return new ChallengeResult(provider);
        //    }

        //    var user = await userManager.FindByLoginAsync(externalLogin.LoginProvider, externalLogin.ProviderKey);

        //    bool hasRegistered = user != null;

        //    var emailExternalLogin = await GetEmailExternalLogin(externalLogin.LoginProvider, externalLogin.ExternalAccessToken);
        //    if (emailExternalLogin != null)
        //    {
        //        externalLogin.Email = emailExternalLogin.email;
        //    }

        //    redirectUri = string.Format("{0}?external_access_token={1}&provider={2}&haslocalaccount={3}&external_user_name={4}&email={5}",
        //                                    redirectUri,
        //                                    externalLogin.ExternalAccessToken,
        //                                    externalLogin.LoginProvider,
        //                                    hasRegistered.ToString(),
        //                                    externalLogin.UserName,
        //                                    externalLogin.Email);

        //    return Redirect(redirectUri);

        //}

        // POST api/Account/RegisterExternal
        //[AllowAnonymous]
        //[Route("RegisterExternal")]
        //public async Task<IActionResult> RegisterExternal(RegisterExternalBindingModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var verifiedAccessToken = await VerifyExternalAccessToken(model.Provider, model.ExternalAccessToken);
        //    if (verifiedAccessToken == null)
        //    {
        //        return BadRequest("Invalid Provider or External Access Token");
        //    }

        //    var user = await userManager.FindByLoginAsync(model.Provider, verifiedAccessToken.user_id);

        //    bool hasRegistered = user != null;

        //    if (hasRegistered)
        //    {
        //        return BadRequest("External user is already registered");
        //    }

        //    user = new IdentityUserViewModel() { UserName = model.UserName, DateOfBirth = DateTime.Now , Email = model.Email};

        //    IdentityResult result = await userManager.CreateAsync(user);
        //    if (!result.Succeeded)
        //    {
        //        return GetErrorResult(result);
        //    }

        //    result = await userManager.AddLoginAsync(user, new UserLoginInfo(model.Provider, verifiedAccessToken.user_id, model.UserName));
        //    if (!result.Succeeded)
        //    {
        //        return GetErrorResult(result);
        //    }

        //    //generate access token response
        //    var accessTokenResponse = "";//GenerateLocalAccessTokenResponse(model.UserName);

        //    return Ok(accessTokenResponse);
        //}

        //[AllowAnonymous]
        //[HttpGet]
        //[Route("ObtainLocalAccessToken")]
        //public async Task<IActionResult> ObtainLocalAccessToken(string provider, string externalAccessToken)
        //{

        //    if (string.IsNullOrWhiteSpace(provider) || string.IsNullOrWhiteSpace(externalAccessToken))
        //    {
        //        return BadRequest("Provider or external access token is not sent");
        //    }

        //    var verifiedAccessToken = await VerifyExternalAccessToken(provider, externalAccessToken);
        //    if (verifiedAccessToken == null)
        //    {
        //        return BadRequest("Invalid Provider or External Access Token");
        //    }

        //    var user = await userManager.FindByLoginAsync(provider, verifiedAccessToken.user_id);

        //    bool hasRegistered = user != null;

        //    if (!hasRegistered)
        //    {
        //        return BadRequest("External user is not registered");
        //    }

        //    //generate access token response
        //    var accessTokenResponse = "";// GenerateLocalAccessTokenResponse(user.UserName);

        //    return Ok(accessTokenResponse);

        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                userManager.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Helpers

        private IActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return BadRequest();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }

        private string ValidateClientAndRedirectUri(HttpRequestMessage request, ref string redirectUriOutput)
        {

            Uri redirectUri;

            var redirectUriString = GetQueryString(request, "redirect_uri") + "#/redirector";

            if (string.IsNullOrWhiteSpace(redirectUriString))
            {
                return "redirect_uri is required";
            }

            bool validUri = Uri.TryCreate(redirectUriString, UriKind.Absolute, out redirectUri);

            if (!validUri)
            {
                return "redirect_uri is invalid";
            }

            var clientId = GetQueryString(request, "client_id");

            if (string.IsNullOrWhiteSpace(clientId))
            {
                return "client_Id is required";
            }

            var client = clientService.FindClient(clientId);

            if (client == null)
            {
                return string.Format("Client_id '{0}' is not registered in the system.", clientId);
            }

            if (client.AllowedOrigin != "*" && !string.Equals(client.AllowedOrigin, redirectUri.GetLeftPart(UriPartial.Authority), StringComparison.OrdinalIgnoreCase))
            {
                return string.Format("The given URL is not allowed by Client_id '{0}' configuration.", clientId);
            }

            redirectUriOutput = redirectUri.AbsoluteUri;

            return string.Empty;

        }

        private string GetQueryString(HttpRequestMessage request, string key)
        {
            //var queryStrings = request.GetQueryNameValuePairs();

            //if (queryStrings == null) return null;

            //var match = queryStrings.FirstOrDefault(keyValue => string.Compare(keyValue.Key, key, true) == 0);

            //if (string.IsNullOrEmpty(match.Value)) return null;

            //return match.Value;

            return string.Empty;
        }

        //private async Task<ParsedExternalAccessToken> VerifyExternalAccessToken(string provider, string accessToken)
        //{
        //    ParsedExternalAccessToken parsedToken = null;

        //    var verifyTokenEndPoint = "";

        //    if (provider == "Facebook")
        //    {
        //        //You can get it from here: https://developers.facebook.com/tools/accesstoken/
        //        //More about debug_tokn here: http://stackoverflow.com/questions/16641083/how-does-one-get-the-app-access-token-for-debug-token-inspection-on-facebook
        //        var appToken = "289207288125802|aJI-5DCcnJyrxM6ne4d2gn2ppDc";
        //        verifyTokenEndPoint = string.Format("https://graph.facebook.com/debug_token?input_token={0}&access_token={1}", accessToken, appToken);
        //    }
        //    else if (provider == "Google")
        //    {
        //        verifyTokenEndPoint = string.Format("https://www.googleapis.com/oauth2/v1/tokeninfo?access_token={0}", accessToken);
        //    }
        //    else
        //    {
        //        return null;
        //    }

        //    var client = new HttpClient();
        //    var uri = new Uri(verifyTokenEndPoint);
        //    var response = await client.GetAsync(uri);

        //    if (response.IsSuccessStatusCode)
        //    {
        //        var content = await response.Content.ReadAsStringAsync();

        //        dynamic jObj = (JObject)Newtonsoft.Json.JsonConvert.DeserializeObject(content);

        //        parsedToken = new ParsedExternalAccessToken();

        //        if (provider == "Facebook")
        //        {
        //            parsedToken.user_id = jObj["data"]["user_id"];
        //            parsedToken.app_id = jObj["data"]["app_id"];

        //            if (!string.Equals(Startup.facebookAuthOptions.AppId, parsedToken.app_id, StringComparison.OrdinalIgnoreCase))
        //            {
        //                return null;
        //            }
        //        }
        //        else if (provider == "Google")
        //        {
        //            parsedToken.user_id = jObj["user_id"];
        //            parsedToken.app_id = jObj["audience"];

        //            if (!string.Equals(Startup.googleAuthOptions.ClientId, parsedToken.app_id, StringComparison.OrdinalIgnoreCase))
        //            {
        //                return null;
        //            }

        //        }

        //    }

        //    return parsedToken;
        //}

        //private async Task<ExternalLoginEmailInfo> GetEmailExternalLogin(string provider, string accessToken)
        //{
        //    ExternalLoginEmailInfo externalLoginEmail = null;

        //    var getEmailEndPoint = "";

        //    if (provider == "Facebook")
        //    {
        //        var appToken = "589067647936345|_f8Z3IuWCFHjeypMb2cDr5tCkk0";
        //        getEmailEndPoint = string.Format("https://graph.facebook.com/v2.5/me?access_token={0}", accessToken);
        //    }
        //    else if (provider == "Google")
        //    {
        //        getEmailEndPoint = string.Format("https://www.googleapis.com/oauth2/v1/me?access_token={0}", accessToken);
        //    }
        //    else
        //    {
        //        return null;
        //    }

        //    var client = new HttpClient();
        //    var uri = new Uri(getEmailEndPoint);
        //    var response = await client.GetAsync(uri);

        //    if (response.IsSuccessStatusCode)
        //    {
        //        var content = await response.Content.ReadAsStringAsync();

        //        dynamic jObj = (JObject)Newtonsoft.Json.JsonConvert.DeserializeObject(content);

        //        externalLoginEmail = new ExternalLoginEmailInfo();

        //        if (provider == "Facebook")
        //        {
        //           if (jObj.Property("email") != null)
        //            {
        //                externalLoginEmail.email = jObj.Property("email").Value;
        //            }

        //            if (jObj.Property("app_id") != null)
        //            {
        //                externalLoginEmail.app_id = jObj.Property("app_id").Value;
        //            }

        //            if (!string.Equals(Startup.facebookAuthOptions.AppId, externalLoginEmail.app_id, StringComparison.OrdinalIgnoreCase))
        //            {
        //                return null;
        //            }
        //        }
        //        else if (provider == "Google")
        //        {
        //            externalLoginEmail.email = jObj["user_id"];
        //            externalLoginEmail.app_id = jObj["audience"];

        //            if (!string.Equals(Startup.googleAuthOptions.ClientId, externalLoginEmail.app_id, StringComparison.OrdinalIgnoreCase))
        //            {
        //                return null;
        //            }

        //        }

        //    }

        //    return externalLoginEmail;
        //}

        //private JObject GenerateLocalAccessTokenResponse(string userName)
        //{
        //    var tokenExpiration = TimeSpan.FromDays(1);

        //    ClaimsIdentity identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);

        //    identity.AddClaim(new Claim(ClaimTypes.Name, userName));
        //    identity.AddClaim(new Claim("role", "user"));

        //    var props = new Microsoft.AspNetCore.Authentication.AuthenticationProperties()
        //    {
        //        IssuedUtc = DateTime.UtcNow,
        //        ExpiresUtc = DateTime.UtcNow.Add(tokenExpiration),
        //    };

        //    var ticket = new AuthenticationTicket(identity, props);

        //    var accessToken = Startup.OAuthBearerOptions.AccessTokenFormat.Protect(ticket);

        //    JObject tokenResponse = new JObject(
        //                                new JProperty("userName", userName),
        //                                new JProperty("access_token", accessToken),
        //                                new JProperty("token_type", "bearer"),
        //                                new JProperty("expires_in", tokenExpiration.TotalSeconds.ToString()),
        //                                new JProperty(".issued", ticket.Properties.IssuedUtc.ToString()),
        //                                new JProperty(".expires", ticket.Properties.ExpiresUtc.ToString())
        //);

        //    return tokenResponse;
        //}

        private class ExternalLoginData
        {
            public string LoginProvider { get; set; }
            public string ProviderKey { get; set; }
            public string UserName { get; set; }
            public string ExternalAccessToken { get; set; }
            public string Email { get; set; }

            public IList<Claim> GetClaims()
            {
                IList<Claim> claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.NameIdentifier, ProviderKey, null, LoginProvider));

                if (UserName != null)
                {
                    claims.Add(new Claim(ClaimTypes.Name, UserName, null, LoginProvider));
                }

                return claims;
            }

            public static ExternalLoginData FromIdentity(ClaimsIdentity identity)
            {
                if (identity == null)
                {
                    return null;
                }

                Claim providerKeyClaim = identity.FindFirst(ClaimTypes.NameIdentifier);

                if (providerKeyClaim == null || String.IsNullOrEmpty(providerKeyClaim.Issuer) || String.IsNullOrEmpty(providerKeyClaim.Value))
                {
                    return null;
                }

                if (providerKeyClaim.Issuer == ClaimsIdentity.DefaultIssuer)
                {
                    return null;
                }

                return new ExternalLoginData
                {
                    LoginProvider = providerKeyClaim.Issuer,
                    ProviderKey = providerKeyClaim.Value,
                    UserName = identity.FindFirst(o => o.ValueType == ClaimTypes.Name).Value,
                    ExternalAccessToken = identity.FindFirst(o => o.Type == "ExternalAccessToken").Value
                };
            }
        }

        #endregion
    }

}
