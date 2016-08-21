using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Snake.Web.TagHelpers
{
    [HtmlTargetElement("script")]
    public class ScriptTagHelper : BaseInlinecontentTagHelper
    {
        public ScriptTagHelper(IHostingEnvironment hostEnvironment, IHttpContextAccessor httpContextAccessor)
            : base(hostEnvironment, httpContextAccessor)
        {
        }

        [HtmlAttributeName("src")]
        public string Src { get; set; }

        [HtmlAttributeName("type")]
        public string Type { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (DevelopmentMode())
            {
                output.Attributes.Add("src", Src.Replace(".min", ""));
                output.Attributes.Add("type", Type);
                return;
            }

            // Load the file
            var file = File.ReadAllText(HostEnvironment.WebRootPath + "\\" + Src);
            output.TagMode = TagMode.StartTagAndEndTag;
            output.Content.SetHtmlContent(file);
            output.Attributes.Clear();
        }
    }
}
