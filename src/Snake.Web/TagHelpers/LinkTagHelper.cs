using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Snake.Web.TagHelpers
{
    [HtmlTargetElement("link")]
    public class LinkTagHelper : TagHelper
    {
        private readonly IHostingEnvironment _hostEnvironment;

        public LinkTagHelper(IHostingEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
        }

        [HtmlAttributeName("href")]
        public string Href { get; set; }


        [HtmlAttributeName("rel")]
        public string Rel { get; set; }
        
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (Rel != "stylesheet")
                return;

            // Load the file
            var file = File.ReadAllText(_hostEnvironment.WebRootPath + "\\" + Href);
            output.TagName = "style";
            output.TagMode = TagMode.StartTagAndEndTag;
            output.Content.SetHtmlContent(file);
            output.Attributes.Clear();
        }
    }
}
