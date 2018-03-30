/*
 * Licensed under the Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0)
 * See https://github.com/KastraCMS/kastra-core for more information concerning
 * the license and the contributors participating to this project.
 */

using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Kastra.Core.Helpers;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Routing;

namespace Kastra.Core.TagHelpers
{
    [HtmlTargetElement("a", Attributes = ModuleIdAttributeName)]
    [HtmlTargetElement("a", Attributes = ModuleActionAttributeName)]
    public class ActionLinkHelper : AnchorTagHelper
    {
        #region Constants

        private const string ModuleActionAttributeName = "module-action";
        private const string ModuleIdAttributeName = "module-id";
        private const string ModuleFragmentName = "module-fragment";
        private const string ModuleControlAttributeName = "module-control";
        private const string PageActionAttributeName = "page-action";
        private const string AdminModuleAttributeName = "admin-module";

        private const string PageActionKeyName = "pa";
        private const string ModuleActionKeyName = "ma";
        private const string ModuleIdKeyName = "mid";
        private const string ModuleControlKeyName = "mc";

        #endregion

        #region Properties

        [HtmlAttributeName("href")]
        public string Url { get; set; }

        [HtmlAttributeName(PageActionAttributeName)]
        public string PageAction { get; set; }

        [HtmlAttributeName(ModuleActionAttributeName)]
        public string ModuleAction { get; set; }

        [HtmlAttributeName(ModuleIdAttributeName)]
        public string ModuleId { get; set; }

        [HtmlAttributeName(ModuleControlAttributeName)]
        public string ModuleControl { get; set; }

        [HtmlAttributeName(AdminModuleAttributeName)]
        public bool AdminModule { get; set; }

        #endregion

        public ActionLinkHelper(IHtmlGenerator generator) : base(generator)
        {
        }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            string url = String.Empty;
            TagBuilder tagBuilder = null;
            CustomUrlHelper urlHelper = new CustomUrlHelper(ViewContext);

            UrlActionContext urlContext = new UrlActionContext();
            urlContext.Action = Action;
            urlContext.Controller = Controller;
            urlContext.Host = Host;
            urlContext.Protocol = Protocol;
            urlContext.Values = GetValuesDictionary(RouteValues);
            urlContext.Fragment = Fragment;
            
            if(AdminModule)
            {
                url = urlHelper.AdminAction(urlContext);
            }
            else
            {
                url = urlHelper.Action(urlContext);
            }

            tagBuilder = new TagBuilder("a");
            tagBuilder.MergeAttribute("href", url);

            output.Attributes.RemoveAll("href");
            output.MergeAttributes(tagBuilder);
        }

        private static void RemoveLink(TagHelperOutput output, string content)
        {
            output.PreContent.SetContent(String.Empty);
            output.TagName = String.Empty;
            output.Content.SetContent(content);
            output.PostContent.SetContent(String.Empty);
        }

        private RouteValueDictionary GetValuesDictionary(object values)
        {
            IDictionary<string, string> valuesDictionary = values as IDictionary<string, string>;
            RouteValueDictionary routeValuesDictionary = new RouteValueDictionary();
            
            foreach(var valueDictionary in valuesDictionary)
            {
                routeValuesDictionary[valueDictionary.Key] = valueDictionary.Value;
            }

            if(String.IsNullOrEmpty(ModuleId))
            {
                return routeValuesDictionary;
            }

            routeValuesDictionary[PageActionKeyName] = PageAction;

            if(String.IsNullOrEmpty(PageAction) && !AdminModule)
            {
                routeValuesDictionary[ModuleIdKeyName] = null;
                routeValuesDictionary[ModuleControlKeyName] = null;
                routeValuesDictionary[ModuleActionKeyName] = null;

                return routeValuesDictionary;
            }

            routeValuesDictionary[ModuleIdKeyName] = ModuleId;

            if(String.IsNullOrEmpty(ModuleId))
            {
                routeValuesDictionary[ModuleControlKeyName] = null;
                routeValuesDictionary[ModuleActionKeyName] = null;

                return routeValuesDictionary;
            }

            routeValuesDictionary[ModuleControlKeyName] = ModuleControl;

            if(String.IsNullOrEmpty(ModuleControl))
            {
                routeValuesDictionary[ModuleActionKeyName] = null;

                return routeValuesDictionary;
            }

            routeValuesDictionary[ModuleActionKeyName] = ModuleAction;
              
            return routeValuesDictionary;
        }

        private async Task<HttpStatusCode> GetStatusCode()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage msg = new HttpResponseMessage();
                    await client.GetAsync(Url);
                    return msg.StatusCode;
                }
            }
            catch
            {
                return HttpStatusCode.InternalServerError;
            }
        }
    }
}