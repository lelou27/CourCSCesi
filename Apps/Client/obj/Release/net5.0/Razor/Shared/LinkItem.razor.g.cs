#pragma checksum "C:\Users\leandreg\source\repos\myhn\Apps\Client\Shared\LinkItem.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "125e834d3b71323095a47731ad64b068b7fde824"
// <auto-generated/>
#pragma warning disable 1591
namespace Client.Shared
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
#nullable restore
#line 1 "C:\Users\leandreg\source\repos\myhn\Apps\Client\_Imports.razor"
using System.Net.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\leandreg\source\repos\myhn\Apps\Client\_Imports.razor"
using System.Net.Http.Json;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\leandreg\source\repos\myhn\Apps\Client\_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\leandreg\source\repos\myhn\Apps\Client\_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Users\leandreg\source\repos\myhn\Apps\Client\_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "C:\Users\leandreg\source\repos\myhn\Apps\Client\_Imports.razor"
using Microsoft.AspNetCore.Components.Web.Virtualization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "C:\Users\leandreg\source\repos\myhn\Apps\Client\_Imports.razor"
using Microsoft.AspNetCore.Components.WebAssembly.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "C:\Users\leandreg\source\repos\myhn\Apps\Client\_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "C:\Users\leandreg\source\repos\myhn\Apps\Client\_Imports.razor"
using Client;

#line default
#line hidden
#nullable disable
#nullable restore
#line 10 "C:\Users\leandreg\source\repos\myhn\Apps\Client\_Imports.razor"
using Client.Shared;

#line default
#line hidden
#nullable disable
    public partial class LinkItem : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
            __builder.OpenElement(0, "article");
            __builder.OpenElement(1, "h3");
            __builder.AddContent(2, 
#nullable restore
#line 2 "C:\Users\leandreg\source\repos\myhn\Apps\Client\Shared\LinkItem.razor"
         Item.Url

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.AddMarkupContent(3, "\r\n    ");
            __builder.OpenElement(4, "p");
            __builder.AddMarkupContent(5, "\r\n        👍 ");
            __builder.AddContent(6, 
#nullable restore
#line 4 "C:\Users\leandreg\source\repos\myhn\Apps\Client\Shared\LinkItem.razor"
            Item.UpVotesCount

#line default
#line hidden
#nullable disable
            );
            __builder.AddMarkupContent(7, " / 👎 ");
            __builder.AddContent(8, 
#nullable restore
#line 4 "C:\Users\leandreg\source\repos\myhn\Apps\Client\Shared\LinkItem.razor"
                                    Item.DownVotesCount

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.CloseElement();
        }
        #pragma warning restore 1998
#nullable restore
#line 8 "C:\Users\leandreg\source\repos\myhn\Apps\Client\Shared\LinkItem.razor"
       
    [Parameter]
    public LinkDto Item { get; set; }

#line default
#line hidden
#nullable disable
    }
}
#pragma warning restore 1591
