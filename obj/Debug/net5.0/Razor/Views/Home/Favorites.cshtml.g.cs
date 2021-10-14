#pragma checksum "C:\Users\Min dator\RiderProjects\ChalmersBookExchange\Views\Home\Favorites.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "b1378637fd529aa50e2483a15b25c2cf93ccf649"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Favorites), @"mvc.1.0.razor-page", @"/Views/Home/Favorites.cshtml")]
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
#line 1 "C:\Users\Min dator\RiderProjects\ChalmersBookExchange\Views\_ViewImports.cshtml"
using ChalmersBookExchange;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Min dator\RiderProjects\ChalmersBookExchange\Views\_ViewImports.cshtml"
using ChalmersBookExchange.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\Min dator\RiderProjects\ChalmersBookExchange\Views\Home\Favorites.cshtml"
using ChalmersBookExchange.Views.Home;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\Min dator\RiderProjects\ChalmersBookExchange\Views\Home\Favorites.cshtml"
using ChalmersBookExchange.Controllers;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Users\Min dator\RiderProjects\ChalmersBookExchange\Views\Home\Favorites.cshtml"
using ChalmersBookExchange.Domain;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "C:\Users\Min dator\RiderProjects\ChalmersBookExchange\Views\Home\Favorites.cshtml"
using System.Security.Claims;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "C:\Users\Min dator\RiderProjects\ChalmersBookExchange\Views\Home\Favorites.cshtml"
using Microsoft.AspNetCore.Identity;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"b1378637fd529aa50e2483a15b25c2cf93ccf649", @"/Views/Home/Favorites.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"f313ac166375ec28aa2c9424361e70aaaa007ccc", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Favorites : global::Microsoft.AspNetCore.Mvc.RazorPages.Page
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "RemoveFavoriteFromFavorite", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("name", "_ValidationScriptsPartial", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
            WriteLiteral("\r\n");
            WriteLiteral(@"
<link rel=""stylesheet"" href=""https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css"">
<style>


#removeFavoriteButton{
text-align: center !important;
background-color: #d95430;
border-radius: 100px;
-webkit-box-shadow: 0 3px 0 0 #b13f21;
-moz-box-shadow: 0 3px 0 0 #b13f21;
box-shadow: 0 3px 0 0 #b13f21;
position: relative;
-webkit-transition: .1s background-color linear;
-moz-transition: .1s background-color linear;
-o-transition: .1s background-color linear;
transition: .1s background-color linear;
padding: 10px 10px;
color: #fff;
border-color: #d34b27;
font-size: 10px;
text-transform: uppercase;
letter-spacing: 1px;
font-weight: 600;
font-style: normal;
width: 150px;
}

</style>
<h1>");
#nullable restore
#line 42 "C:\Users\Min dator\RiderProjects\ChalmersBookExchange\Views\Home\Favorites.cshtml"
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
            <th>Poster</th>
        </tr>
        </thead>
        <tbody>
         

");
#nullable restore
#line 59 "C:\Users\Min dator\RiderProjects\ChalmersBookExchange\Views\Home\Favorites.cshtml"
         foreach (var post in _postController.GetFavorites((@User.Identity.Name)))
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <tr>\r\n                <td>");
#nullable restore
#line 62 "C:\Users\Min dator\RiderProjects\ChalmersBookExchange\Views\Home\Favorites.cshtml"
               Write(post.BookName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 63 "C:\Users\Min dator\RiderProjects\ChalmersBookExchange\Views\Home\Favorites.cshtml"
               Write(post.CourseCode);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 64 "C:\Users\Min dator\RiderProjects\ChalmersBookExchange\Views\Home\Favorites.cshtml"
               Write(post.Price);

#line default
#line hidden
#nullable disable
            WriteLiteral(":-</td>\r\n                <td>");
#nullable restore
#line 65 "C:\Users\Min dator\RiderProjects\ChalmersBookExchange\Views\Home\Favorites.cshtml"
               Write(post.Location);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 66 "C:\Users\Min dator\RiderProjects\ChalmersBookExchange\Views\Home\Favorites.cshtml"
               Write(post.Shippable);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 67 "C:\Users\Min dator\RiderProjects\ChalmersBookExchange\Views\Home\Favorites.cshtml"
               Write(post.Meetup);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 68 "C:\Users\Min dator\RiderProjects\ChalmersBookExchange\Views\Home\Favorites.cshtml"
               Write(_userController.RetrieveName(post.Poster));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td>");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "b1378637fd529aa50e2483a15b25c2cf93ccf6498454", async() => {
                WriteLiteral(" <button class=\"btn\" id=\"removeFavoriteButton\"> Remove Favorite</button>");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 69 "C:\Users\Min dator\RiderProjects\ChalmersBookExchange\Views\Home\Favorites.cshtml"
                                                                  WriteLiteral(post.ID);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            BeginWriteTagHelperAttribute();
#nullable restore
#line 69 "C:\Users\Min dator\RiderProjects\ChalmersBookExchange\Views\Home\Favorites.cshtml"
                                                                                             WriteLiteral(ViewBag.ThisUser);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["email"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-email", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["email"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("</td>\r\n\r\n            </tr>\r\n");
#nullable restore
#line 72 "C:\Users\Min dator\RiderProjects\ChalmersBookExchange\Views\Home\Favorites.cshtml"
             
        }  

#line default
#line hidden
#nullable disable
            WriteLiteral("      \r\n        </tbody>\r\n    </table>\r\n\r\n</div>\r\n\r\n\r\n");
            DefineSection("Scripts", async() => {
                WriteLiteral("\r\n    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("partial", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "b1378637fd529aa50e2483a15b25c2cf93ccf64911891", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Name = (string)__tagHelperAttribute_1.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n     \r\n");
            }
            );
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public UserManager<IdentityUser> UserManager { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public IPostController _postController { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public IUserController _userController { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<ChalmersBookExchange.Domain.Post> Html { get; private set; }
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<ChalmersBookExchange.Domain.Post> ViewData => (global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<ChalmersBookExchange.Domain.Post>)PageContext?.ViewData;
        public ChalmersBookExchange.Domain.Post Model => ViewData.Model;
    }
}
#pragma warning restore 1591
