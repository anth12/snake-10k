using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Snake.Web.TagHelpers
{
    [HtmlTargetElement("link")]
    public class LinkTagHelper : BaseInlinecontentTagHelper
    {

        public LinkTagHelper(IHostingEnvironment hostEnvironment, IHttpContextAccessor httpContextAccessor)
            : base(hostEnvironment, httpContextAccessor)
        {
        }

        [HtmlAttributeName("href")]
        public string Href { get; set; }


        [HtmlAttributeName("rel")]
        public string Rel { get; set; }

        [HtmlAttributeName("type")]
        public string Type { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (DevelopmentMode() || Rel != "stylesheet")
            {
                output.Attributes.Add("href", Href.Replace(".min", ""));
                output.Attributes.Add("rel", Rel);
                output.Attributes.Add("type", Type);
                return;
            }

            // Load the file
            var file = File.ReadAllText(HostEnvironment.WebRootPath + "\\" + Href);
            output.TagName = "style";
            output.TagMode = TagMode.StartTagAndEndTag;
            output.Content.SetHtmlContent(file);
            output.Attributes.Clear();
        }

    }
}
