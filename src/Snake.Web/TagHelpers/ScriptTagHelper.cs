using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Snake.Web.TagHelpers
{
    [HtmlTargetElement("script")]
    public class ScriptTagHelper : TagHelper
    {
        private readonly IHostingEnvironment _hostEnvironment;

        public ScriptTagHelper(IHostingEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
        }

        [HtmlAttributeName("src")]
        public string Src { get; set; }

        
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            // Load the file
            var file = File.ReadAllText(_hostEnvironment.WebRootPath + "\\" + Src);
            output.TagMode = TagMode.StartTagAndEndTag;
            output.Content.SetHtmlContent(file);
            output.Attributes.Clear();
        }
    }
}
