using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthorizeAttribute : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        string verification = context.HttpContext.Items["verify_tokent"].ToString();
        if (verification == "failed")
        {
            // not logged in
            context.Result = new JsonResult(new { status="-1", message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
        }
    }
}