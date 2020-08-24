#pragma checksum "C:\Users\philg\Documents\GitHub\CSHP230Final\WebSiteProject\src\School.Web\Views\Home\Classes.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "edd722da7c228e7dd136c33c507256c41c9d0846"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Classes), @"mvc.1.0.view", @"/Views/Home/Classes.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Home/Classes.cshtml", typeof(AspNetCore.Views_Home_Classes))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"edd722da7c228e7dd136c33c507256c41c9d0846", @"/Views/Home/Classes.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"b6876639910d42a518af3386f22bc9cfa4edc225", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Classes : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<School.Web.Models.Class>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Enroll", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
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
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(38, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 3 "C:\Users\philg\Documents\GitHub\CSHP230Final\WebSiteProject\src\School.Web\Views\Home\Classes.cshtml"
  
    ViewBag.Title = Model;

#line default
#line hidden
            BeginContext(75, 38, true);
            WriteLiteral("\r\n<h2>Available Classes</h2>\r\n<br />\r\n");
            EndContext();
#line 9 "C:\Users\philg\Documents\GitHub\CSHP230Final\WebSiteProject\src\School.Web\Views\Home\Classes.cshtml"
 if (@TempData["Message"] != null)
{

#line default
#line hidden
            BeginContext(152, 38, true);
            WriteLiteral("    <div class=\"alert-info\">\r\n        ");
            EndContext();
            BeginContext(191, 19, false);
#line 12 "C:\Users\philg\Documents\GitHub\CSHP230Final\WebSiteProject\src\School.Web\Views\Home\Classes.cshtml"
   Write(TempData["Message"]);

#line default
#line hidden
            EndContext();
            BeginContext(210, 15, true);
            WriteLiteral(";\r\n    </div>\r\n");
            EndContext();
#line 14 "C:\Users\philg\Documents\GitHub\CSHP230Final\WebSiteProject\src\School.Web\Views\Home\Classes.cshtml"
}

#line default
#line hidden
            BeginContext(228, 7, true);
            WriteLiteral("<div>\r\n");
            EndContext();
#line 16 "C:\Users\philg\Documents\GitHub\CSHP230Final\WebSiteProject\src\School.Web\Views\Home\Classes.cshtml"
     foreach (var availableClass in Model)
    {



#line default
#line hidden
            BeginContext(290, 52, true);
            WriteLiteral("        <div>\r\n            <h4>\r\n                <b>");
            EndContext();
            BeginContext(343, 24, false);
#line 22 "C:\Users\philg\Documents\GitHub\CSHP230Final\WebSiteProject\src\School.Web\Views\Home\Classes.cshtml"
              Write(availableClass.ClassName);

#line default
#line hidden
            EndContext();
            BeginContext(367, 37, true);
            WriteLiteral("</b>\r\n            </h4>\r\n            ");
            EndContext();
            BeginContext(405, 31, false);
#line 24 "C:\Users\philg\Documents\GitHub\CSHP230Final\WebSiteProject\src\School.Web\Views\Home\Classes.cshtml"
       Write(availableClass.ClassDescription);

#line default
#line hidden
            EndContext();
            BeginContext(436, 33, true);
            WriteLiteral("\r\n            <br/>\r\n            ");
            EndContext();
            BeginContext(469, 132, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "c122bda5e17a479a8df842351197f874", async() => {
                BeginContext(537, 60, false);
#line 26 "C:\Users\philg\Documents\GitHub\CSHP230Final\WebSiteProject\src\School.Web\Views\Home\Classes.cshtml"
                                                                          Write(String.Format("Enroll for {0:c}", availableClass.ClassPrice));

#line default
#line hidden
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-classId", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#line 26 "C:\Users\philg\Documents\GitHub\CSHP230Final\WebSiteProject\src\School.Web\Views\Home\Classes.cshtml"
                                          WriteLiteral(availableClass.ClassId);

#line default
#line hidden
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["classId"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-classId", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["classId"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(601, 39, true);
            WriteLiteral("\r\n            <br/>\r\n\r\n        </div>\r\n");
            EndContext();
#line 30 "C:\Users\philg\Documents\GitHub\CSHP230Final\WebSiteProject\src\School.Web\Views\Home\Classes.cshtml"


    }

#line default
#line hidden
            BeginContext(651, 16, true);
            WriteLiteral("\r\n</div>\r\n\r\n\r\n\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<School.Web.Models.Class>> Html { get; private set; }
    }
}
#pragma warning restore 1591
