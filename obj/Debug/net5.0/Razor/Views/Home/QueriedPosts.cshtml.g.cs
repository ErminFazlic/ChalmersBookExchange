#pragma checksum "C:\Users\Dator\RiderProjects\ChalmersBookExchange\ChalmersBookExchange\Views\Home\QueriedPosts.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "8942279f99333d4046fa121e14ee5ac92a52ffb3"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_QueriedPosts), @"mvc.1.0.razor-page", @"/Views/Home/QueriedPosts.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\Dator\RiderProjects\ChalmersBookExchange\ChalmersBookExchange\Views\_ViewImports.cshtml"
using ChalmersBookExchange;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Dator\RiderProjects\ChalmersBookExchange\ChalmersBookExchange\Views\_ViewImports.cshtml"
using ChalmersBookExchange.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Dator\RiderProjects\ChalmersBookExchange\ChalmersBookExchange\Views\Home\QueriedPosts.cshtml"
using ChalmersBookExchange.Views.Home;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\Dator\RiderProjects\ChalmersBookExchange\ChalmersBookExchange\Views\Home\QueriedPosts.cshtml"
using ChalmersBookExchange.Controllers;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\Dator\RiderProjects\ChalmersBookExchange\ChalmersBookExchange\Views\Home\QueriedPosts.cshtml"
using ChalmersBookExchange.Domain;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"8942279f99333d4046fa121e14ee5ac92a52ffb3", @"/Views/Home/QueriedPosts.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"f313ac166375ec28aa2c9424361e70aaaa007ccc", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_QueriedPosts : global::Microsoft.AspNetCore.Mvc.RazorPages.Page
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("name", "_ValidationScriptsPartial", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
            WriteLiteral("\r\n<h1>");
#nullable restore
#line 8 "C:\Users\Dator\RiderProjects\ChalmersBookExchange\ChalmersBookExchange\Views\Home\QueriedPosts.cshtml"
Write(ViewBag.Title);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</h1>
<div>
    <table class=""table table-striped"">
        <thead>
            <tr>
                <th>Book Name</th>
                <th>Course Code</th>
                <th>Price</th>
                <th>Location</th>
                <th>Shippable</th>
                <th>Meetup</th>
            </tr>
        </thead>
        <tbody>
        
");
#nullable restore
#line 23 "C:\Users\Dator\RiderProjects\ChalmersBookExchange\ChalmersBookExchange\Views\Home\QueriedPosts.cshtml"
         foreach (var post in ViewBag.Message)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <tr>\r\n                <td>");
#nullable restore
#line 26 "C:\Users\Dator\RiderProjects\ChalmersBookExchange\ChalmersBookExchange\Views\Home\QueriedPosts.cshtml"
               Write(post.BookName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 27 "C:\Users\Dator\RiderProjects\ChalmersBookExchange\ChalmersBookExchange\Views\Home\QueriedPosts.cshtml"
               Write(post.CourseCode);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 28 "C:\Users\Dator\RiderProjects\ChalmersBookExchange\ChalmersBookExchange\Views\Home\QueriedPosts.cshtml"
               Write(post.Price);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 29 "C:\Users\Dator\RiderProjects\ChalmersBookExchange\ChalmersBookExchange\Views\Home\QueriedPosts.cshtml"
               Write(post.Location);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 30 "C:\Users\Dator\RiderProjects\ChalmersBookExchange\ChalmersBookExchange\Views\Home\QueriedPosts.cshtml"
               Write(post.Shippable);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 31 "C:\Users\Dator\RiderProjects\ChalmersBookExchange\ChalmersBookExchange\Views\Home\QueriedPosts.cshtml"
               Write(post.Meetup);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n            </tr>\r\n");
#nullable restore
#line 33 "C:\Users\Dator\RiderProjects\ChalmersBookExchange\ChalmersBookExchange\Views\Home\QueriedPosts.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("        </tbody>\r\n</table>\r\n\r\n</div>\r\n\r\n\r\n");
            DefineSection("Scripts", async() => {
                WriteLiteral("\r\n    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("partial", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "8942279f99333d4046fa121e14ee5ac92a52ffb37160", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Name = (string)__tagHelperAttribute_0.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n");
            }
            );
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public IPostController _postController { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Views_Home_QueriedPosts> Html { get; private set; }
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<Views_Home_QueriedPosts> ViewData => (global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<Views_Home_QueriedPosts>)PageContext?.ViewData;
        public Views_Home_QueriedPosts Model => ViewData.Model;
    }
}
#pragma warning restore 1591
