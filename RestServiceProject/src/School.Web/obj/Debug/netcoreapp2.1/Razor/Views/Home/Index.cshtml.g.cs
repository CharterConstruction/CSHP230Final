#pragma checksum "C:\Users\philg\Documents\GitHub\CSHP230Final\WebSiteProject\src\School.Web\Views\Home\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "2f185643ade3071e738577354378eb8a60de72cf"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Index), @"mvc.1.0.view", @"/Views/Home/Index.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Home/Index.cshtml", typeof(AspNetCore.Views_Home_Index))]
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
#line 1 "C:\Users\philg\Documents\GitHub\CSHP230Final\WebSiteProject\src\School.Web\Views\_ViewImports.cshtml"
using School.Web;

#line default
#line hidden
#line 2 "C:\Users\philg\Documents\GitHub\CSHP230Final\WebSiteProject\src\School.Web\Views\_ViewImports.cshtml"
using School.Web.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"2f185643ade3071e738577354378eb8a60de72cf", @"/Views/Home/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"b6876639910d42a518af3386f22bc9cfa4edc225", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 1 "C:\Users\philg\Documents\GitHub\CSHP230Final\WebSiteProject\src\School.Web\Views\Home\Index.cshtml"
  
    ViewData["Title"] = "Home Page";

#line default
#line hidden
            BeginContext(45, 61, true);
            WriteLiteral("\r\n<div class=\"row\">\r\n    <div class=\"col-md-2\">\r\n        <h2>");
            EndContext();
            BeginContext(107, 45, false);
#line 7 "C:\Users\philg\Documents\GitHub\CSHP230Final\WebSiteProject\src\School.Web\Views\Home\Index.cshtml"
       Write(Html.ActionLink("Classes", "Classes", "Home"));

#line default
#line hidden
            EndContext();
            BeginContext(152, 161, true);
            WriteLiteral("</h2>\r\n        <ul>\r\n            <li>View all available classes and enroll.</li>            \r\n        </ul>\r\n    </div>\r\n    <div class=\"col-md-3\">\r\n        <h2>");
            EndContext();
            BeginContext(314, 52, false);
#line 13 "C:\Users\philg\Documents\GitHub\CSHP230Final\WebSiteProject\src\School.Web\Views\Home\Index.cshtml"
       Write(Html.ActionLink("My Classes", "UserClasses", "Home"));

#line default
#line hidden
            EndContext();
            BeginContext(366, 123, true);
            WriteLiteral("</h2>\r\n        <ul>\r\n            <li>View the classes that you are enrolled in.</li>\r\n        </ul>\r\n    </div>\r\n\r\n</div>\r\n");
            EndContext();
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591