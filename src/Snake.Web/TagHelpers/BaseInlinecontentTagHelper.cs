using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Snake.Web.TagHelpers
{
    public class BaseInlinecontentTagHelper : TagHelper
    {
        protected readonly IHostingEnvironment HostEnvironment;
        protected readonly IHttpContextAccessor HttpContextAccessor;

        protected BaseInlinecontentTagHelper(IHostingEnvironment hostEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            HostEnvironment = hostEnvironment;
            HttpContextAccessor = httpContextAccessor;
        }

        protected bool DevelopmentMode()
        {
            return HttpContextAccessor.HttpContext.Request.Query.ContainsKey("mode")
                && HttpContextAccessor.HttpContext.Request.Query["mode"] == "dev";
        }
    }
}
