using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Buffers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using SportsStore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Infrastructure
{
    public class PageLinkTagHelper : TagHelper
    {
        private readonly IUrlHelperFactory urlHelperFactory;

        public PageLinkTagHelper(IUrlHelperFactory urlHelperFactory)
        {
            this.urlHelperFactory = urlHelperFactory;
        }

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }

        public PagingInfo PageModel { get; set; }

        public string PageAction { get; set; }

        [HtmlAttributeName(DictionaryAttributePrefix = "page-url-")]
        public Dictionary<string, object> PageUrlValues { get; set; } = new Dictionary<string, object>();

        public bool PageClassesEnabled { get; set; }

        public string PageClass { get; set; }

        public string PageClassNormal { get; set; }

        public string PageClassSelected { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            IUrlHelper urlHelper = this.urlHelperFactory.GetUrlHelper(this.ViewContext);
            TagBuilder divTagBuilder = new TagBuilder("div");
            for (int i = 1; i < this.PageModel.TotalPages + 1; i++)
            {
                TagBuilder tb = new TagBuilder("a");
                PageUrlValues["productPage"] = i;
                tb.Attributes["href"] = urlHelper.Action(this.PageAction, this.PageUrlValues);
                if (this.PageClassesEnabled)
                {
                    tb.AddCssClass(this.PageClass);
                    tb.AddCssClass(i == this.PageModel.CurrentPage ? this.PageClassSelected : this.PageClassNormal);
                }

                tb.InnerHtml.Append(i.ToString());
                divTagBuilder.InnerHtml.AppendHtml(tb);
            }

            output.Content.AppendHtml(divTagBuilder.InnerHtml);
        }
    }
}
