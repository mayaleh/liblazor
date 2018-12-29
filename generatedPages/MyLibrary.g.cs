#pragma checksum "C:\Users\Salim Mayaleh\source\repos\PersonalLibrary\PersonalLibrary.Client\Pages\MyLibrary.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "d7f22ed1238d3c09769a9056175f7f3214d80af8"
// <auto-generated/>
#pragma warning disable 1591
namespace PersonalLibrary.Client.Pages
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Blazor;
    using Microsoft.AspNetCore.Blazor.Components;
    using System.Net.Http;
    using Microsoft.AspNetCore.Blazor.Layouts;
    using Microsoft.AspNetCore.Blazor.Routing;
    using Microsoft.JSInterop;
    using PersonalLibrary.Client;
    using PersonalLibrary.Client.Shared;
    using PersonalLibrary.Shared;
    [Microsoft.AspNetCore.Blazor.Layouts.LayoutAttribute(typeof(MainLayout))]

    [Microsoft.AspNetCore.Blazor.Components.RouteAttribute("/my-library")]
    public class MyLibrary : Microsoft.AspNetCore.Blazor.Components.BlazorComponent
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Blazor.RenderTree.RenderTreeBuilder builder)
        {
            base.BuildRenderTree(builder);
            builder.AddMarkupContent(0, "<h1>My Library List</h1>\n\n");
            builder.OpenElement(1, "p");
            builder.AddContent(2, "\n    ");
            builder.OpenComponent<Microsoft.AspNetCore.Blazor.Routing.NavLink>(3);
            builder.AddAttribute(4, "href", "/form-book");
            builder.AddAttribute(5, "Match", Microsoft.AspNetCore.Blazor.Components.RuntimeHelpers.TypeCheck<Microsoft.AspNetCore.Blazor.Routing.NavLinkMatch>(NavLinkMatch.All));
            builder.AddAttribute(6, "ChildContent", (Microsoft.AspNetCore.Blazor.RenderFragment)((builder2) => {
                builder2.AddContent(7, "\n        ");
                builder2.AddMarkupContent(8, "<button class=\"btn btn-primary\">\n            <span class=\"oi oi-plus\"></span>\n            Add Book\n        </button>\n    ");
            }
            ));
            builder.CloseComponent();
            builder.AddContent(9, "\n");
            builder.CloseElement();
            builder.AddContent(10, "\n\n");
            builder.OpenElement(11, "nav");
            builder.AddAttribute(12, "class", "navbar navbar-expand-lg navbar-light bg-light");
            builder.AddContent(13, "\n    ");
            builder.OpenElement(14, "div");
            builder.AddAttribute(15, "class", "collapse navbar-collapse");
            builder.AddAttribute(16, "id", "navbarSupportedContent");
            builder.AddContent(17, "\n        ");
            builder.OpenElement(18, "ul");
            builder.AddAttribute(19, "class", "navbar-nav mr-auto");
            builder.AddContent(20, "\n\n            ");
            builder.OpenElement(21, "li");
            builder.AddAttribute(22, "class", "nav-item dropdown");
            builder.AddContent(23, "\n                ");
            builder.AddMarkupContent(24, "<a class=\"nav-link dropdown-toggle\" id=\"navbarDropdown\" role=\"button\" data-toggle=\"dropdown\" aria-haspopup=\"true\" aria-expanded=\"false\">\n                    Filtr\n                </a>\n                ");
            builder.OpenElement(25, "div");
            builder.AddAttribute(26, "class", "dropdown-menu");
            builder.AddAttribute(27, "aria-labelledby", "navbarDropdown");
            builder.AddContent(28, "\n                    ");
            builder.OpenElement(29, "a");
            builder.AddAttribute(30, "class", "dropdown-item");
            builder.AddAttribute(31, "onclick", Microsoft.AspNetCore.Blazor.Components.BindMethods.GetEventHandlerValue<Microsoft.AspNetCore.Blazor.UIMouseEventArgs>(OnInitAsync));
            builder.AddContent(32, "List knih");
            builder.CloseElement();
            builder.AddMarkupContent(33, "\n                    <div class=\"dropdown-divider\"></div>\n                    ");
            builder.OpenElement(34, "a");
            builder.AddAttribute(35, "class", "dropdown-item");
            builder.AddAttribute(36, "onclick", Microsoft.AspNetCore.Blazor.Components.BindMethods.GetEventHandlerValue<Microsoft.AspNetCore.Blazor.UIMouseEventArgs>(GetBooksByAuthor));
            builder.AddContent(37, "Podle autora");
            builder.CloseElement();
            builder.AddContent(38, "\n                ");
            builder.CloseElement();
            builder.AddContent(39, "\n            ");
            builder.CloseElement();
            builder.AddContent(40, "\n        ");
            builder.CloseElement();
            builder.AddContent(41, "\n    ");
            builder.CloseElement();
            builder.AddContent(42, "\n");
            builder.CloseElement();
            builder.AddContent(43, "\n");
#line 35 "C:\Users\Salim Mayaleh\source\repos\PersonalLibrary\PersonalLibrary.Client\Pages\MyLibrary.cshtml"
 if (!books.Any() && !authorsBook.Any())
{

#line default
#line hidden
            builder.AddContent(44, "    ");
            builder.AddMarkupContent(45, "<div class=\"text-center\">\n        <div class=\"spinner-border\" role=\"status\">\n            <span class=\"sr-only\">Loading...</span>\n        </div>\n    </div>\n");
#line 42 "C:\Users\Salim Mayaleh\source\repos\PersonalLibrary\PersonalLibrary.Client\Pages\MyLibrary.cshtml"
}
else
{
    

#line default
#line hidden
#line 45 "C:\Users\Salim Mayaleh\source\repos\PersonalLibrary\PersonalLibrary.Client\Pages\MyLibrary.cshtml"
     switch (FiltrType)
    {
        case "listBooks":

#line default
#line hidden
            builder.AddContent(46, "            ");
            builder.OpenElement(47, "div");
            builder.AddAttribute(48, "class", "row");
            builder.AddContent(49, "\n                ");
            builder.OpenElement(50, "div");
            builder.AddAttribute(51, "class", "col-3");
            builder.AddContent(52, "\n                    ");
            builder.OpenElement(53, "div");
            builder.AddAttribute(54, "class", "form-group");
            builder.AddContent(55, "\n                        ");
            builder.OpenElement(56, "input");
            builder.AddAttribute(57, "onchange", Microsoft.AspNetCore.Blazor.Components.BindMethods.GetEventHandlerValue<Microsoft.AspNetCore.Blazor.UIChangeEventArgs>(SearchFor));
            builder.AddAttribute(58, "type", "text");
            builder.AddAttribute(59, "class", "form-control");
            builder.AddAttribute(60, "id", "search");
            builder.AddAttribute(61, "aria-describedby", "searchHelp");
            builder.AddAttribute(62, "placeholder", "Search...");
            builder.AddAttribute(63, "value", Microsoft.AspNetCore.Blazor.Components.BindMethods.GetValue(searchFor));
            builder.AddAttribute(64, "onchange", Microsoft.AspNetCore.Blazor.Components.BindMethods.SetValueHandler(__value => searchFor = __value, searchFor));
            builder.CloseElement();
            builder.AddContent(65, "\n                        ");
            builder.AddMarkupContent(66, "<small id=\"searchHelp\" class=\"form-text text-muted\">Search for...</small>\n\n                    ");
            builder.CloseElement();
            builder.AddContent(67, "\n                ");
            builder.CloseElement();
            builder.AddContent(68, "\n                ");
            builder.OpenElement(69, "div");
            builder.AddAttribute(70, "class", "col");
            builder.AddContent(71, "\n                    ");
            builder.OpenElement(72, "button");
            builder.AddAttribute(73, "onclick", Microsoft.AspNetCore.Blazor.Components.BindMethods.GetEventHandlerValue<Microsoft.AspNetCore.Blazor.UIMouseEventArgs>(SearchFor));
            builder.AddAttribute(74, "class", "btn btn-primary");
            builder.AddMarkupContent(75, "\n                        <span class=\"oi oi-magnifying-glass\"></span>\n                        Search\n                    ");
            builder.CloseElement();
            builder.AddContent(76, "\n                ");
            builder.CloseElement();
            builder.AddContent(77, "\n            ");
            builder.CloseElement();
            builder.AddContent(78, "\n");
#line 64 "C:\Users\Salim Mayaleh\source\repos\PersonalLibrary\PersonalLibrary.Client\Pages\MyLibrary.cshtml"
             if (isSearch)
            {

#line default
#line hidden
            builder.AddContent(79, "                ");
            builder.AddMarkupContent(80, "<h2>Search result:</h2>\n");
#line 67 "C:\Users\Salim Mayaleh\source\repos\PersonalLibrary\PersonalLibrary.Client\Pages\MyLibrary.cshtml"
                 if (!searchResult.Any())
                {

#line default
#line hidden
            builder.AddContent(81, "                    ");
            builder.AddMarkupContent(82, "<p>Nothing found...</p>\n");
#line 70 "C:\Users\Salim Mayaleh\source\repos\PersonalLibrary\PersonalLibrary.Client\Pages\MyLibrary.cshtml"
                }
                else
                {

#line default
#line hidden
            builder.AddContent(83, "                    ");
            builder.OpenElement(84, "ul");
            builder.AddContent(85, "\n\n");
#line 75 "C:\Users\Salim Mayaleh\source\repos\PersonalLibrary\PersonalLibrary.Client\Pages\MyLibrary.cshtml"
                         foreach (var found in searchResult)
                        {

#line default
#line hidden
            builder.AddContent(86, "                            ");
            builder.OpenElement(87, "li");
            builder.AddContent(88, "Book name: ");
            builder.OpenElement(89, "b");
            builder.AddContent(90, found.Name);
            builder.CloseElement();
            builder.AddContent(91, " - Author: ");
            builder.OpenElement(92, "b");
            builder.AddContent(93, found.Author.Name);
            builder.CloseElement();
            builder.AddContent(94, " - Place: ");
            builder.OpenElement(95, "b");
            builder.AddContent(96, found.Place);
            builder.CloseElement();
            builder.CloseElement();
            builder.AddContent(97, "\n");
#line 78 "C:\Users\Salim Mayaleh\source\repos\PersonalLibrary\PersonalLibrary.Client\Pages\MyLibrary.cshtml"
                        }

#line default
#line hidden
            builder.AddContent(98, "                    ");
            builder.CloseElement();
            builder.AddContent(99, "\n");
#line 80 "C:\Users\Salim Mayaleh\source\repos\PersonalLibrary\PersonalLibrary.Client\Pages\MyLibrary.cshtml"
                }

#line default
#line hidden
#line 80 "C:\Users\Salim Mayaleh\source\repos\PersonalLibrary\PersonalLibrary.Client\Pages\MyLibrary.cshtml"
                 
            }

#line default
#line hidden
#line 83 "C:\Users\Salim Mayaleh\source\repos\PersonalLibrary\PersonalLibrary.Client\Pages\MyLibrary.cshtml"
             if (editBookId != null)
            {

#line default
#line hidden
            builder.AddContent(100, "                    ");
            builder.OpenElement(101, "div");
            builder.AddAttribute(102, "class", "padding-md border border-info");
            builder.AddContent(103, "\n                        ");
            builder.OpenElement(104, "div");
            builder.AddAttribute(105, "class", "row");
            builder.AddContent(106, "\n                            ");
            builder.OpenElement(107, "div");
            builder.AddAttribute(108, "class", "col-12 text-right");
            builder.AddContent(109, "\n                                ");
            builder.OpenElement(110, "div");
            builder.AddAttribute(111, "class", "text-right");
            builder.AddContent(112, "\n                                    ");
            builder.OpenElement(113, "button");
            builder.AddAttribute(114, "onclick", Microsoft.AspNetCore.Blazor.Components.BindMethods.GetEventHandlerValue<Microsoft.AspNetCore.Blazor.UIMouseEventArgs>(()=> editBookId = null ));
            builder.AddAttribute(115, "class", "btn btn-danger");
            builder.AddMarkupContent(116, "\n                                        <span class=\"oi oi-x\"></span>\n                                    ");
            builder.CloseElement();
            builder.AddContent(117, "\n                                ");
            builder.CloseElement();
            builder.AddContent(118, "\n                            ");
            builder.CloseElement();
            builder.AddContent(119, "\n                        ");
            builder.CloseElement();
            builder.AddContent(120, "\n                        ");
            builder.OpenElement(121, "div");
            builder.AddAttribute(122, "class", "padding-y-sm");
            builder.AddContent(123, "\n                            ");
            builder.OpenComponent<PersonalLibrary.Client.Pages.FormBook>(124);
            builder.AddAttribute(125, "BookId", Microsoft.AspNetCore.Blazor.Components.RuntimeHelpers.TypeCheck<System.Int32>(Convert.ToInt32(editBookId)));
            builder.AddAttribute(126, "IsEdit", Microsoft.AspNetCore.Blazor.Components.RuntimeHelpers.TypeCheck<System.Boolean>(true));
            builder.CloseComponent();
            builder.AddContent(127, "\n                        ");
            builder.CloseElement();
            builder.AddContent(128, "\n                    ");
            builder.CloseElement();
            builder.AddContent(129, "\n");
            builder.AddMarkupContent(130, "                <br>\n");
#line 101 "C:\Users\Salim Mayaleh\source\repos\PersonalLibrary\PersonalLibrary.Client\Pages\MyLibrary.cshtml"

            }

#line default
#line hidden
            builder.AddContent(131, "            ");
            builder.OpenElement(132, "table");
            builder.AddAttribute(133, "class", "table");
            builder.AddContent(134, "\n                ");
            builder.AddMarkupContent(135, @"<thead>
                    <tr>
                        <th>Book Name</th>
                        <th>Author</th>
                        <th>Place</th>
                        <th>Action</th>
                    </tr>
                </thead>
                ");
            builder.OpenElement(136, "tbody");
            builder.AddContent(137, "\n");
#line 114 "C:\Users\Salim Mayaleh\source\repos\PersonalLibrary\PersonalLibrary.Client\Pages\MyLibrary.cshtml"
                     foreach (var book in books)
                    {

#line default
#line hidden
            builder.AddContent(138, "                        ");
            builder.OpenElement(139, "tr");
            builder.AddContent(140, "\n                            ");
            builder.OpenElement(141, "td");
            builder.AddContent(142, "\n                                ");
            builder.OpenElement(143, "a");
            builder.AddAttribute(144, "href", "#");
            builder.AddMarkupContent(145, "\n                                    <span class=\"oi oi-link-intact\"></span>\n                                    ");
            builder.AddContent(146, book.Name);
            builder.AddContent(147, "\n                                ");
            builder.CloseElement();
            builder.AddContent(148, "\n                            ");
            builder.CloseElement();
            builder.AddContent(149, "\n                            ");
            builder.OpenElement(150, "td");
            builder.AddContent(151, "\n");
#line 124 "C:\Users\Salim Mayaleh\source\repos\PersonalLibrary\PersonalLibrary.Client\Pages\MyLibrary.cshtml"
                                 if (book.Authorid != null)
                                {

#line default
#line hidden
            builder.AddContent(152, "                                    ");
            builder.OpenComponent<Microsoft.AspNetCore.Blazor.Routing.NavLink>(153);
            builder.AddAttribute(154, "href", "/author/detail");
            builder.AddAttribute(155, "Match", Microsoft.AspNetCore.Blazor.Components.RuntimeHelpers.TypeCheck<Microsoft.AspNetCore.Blazor.Routing.NavLinkMatch>(NavLinkMatch.All));
            builder.AddAttribute(156, "ChildContent", (Microsoft.AspNetCore.Blazor.RenderFragment)((builder2) => {
                builder2.AddContent(157, "\n                                        ");
                builder2.AddContent(158, book.Author.Name);
                builder2.AddContent(159, "\n                                    ");
            }
            ));
            builder.CloseComponent();
            builder.AddContent(160, "\n");
#line 129 "C:\Users\Salim Mayaleh\source\repos\PersonalLibrary\PersonalLibrary.Client\Pages\MyLibrary.cshtml"
                                }

#line default
#line hidden
            builder.AddContent(161, "                            ");
            builder.CloseElement();
            builder.AddContent(162, "\n                            ");
            builder.OpenElement(163, "td");
            builder.AddContent(164, book.Place);
            builder.CloseElement();
            builder.AddContent(165, "\n                            ");
            builder.OpenElement(166, "td");
            builder.AddContent(167, "\n                                ");
            builder.OpenElement(168, "a");
            builder.AddAttribute(169, "class", "btn btn-success");
            builder.AddAttribute(170, "onclick", Microsoft.AspNetCore.Blazor.Components.BindMethods.GetEventHandlerValue<Microsoft.AspNetCore.Blazor.UIMouseEventArgs>( () => editBookId = book.Bookid));
            builder.AddMarkupContent(171, "\n                                    <span class=\"oi oi-pencil\"></span>\n                                    Edit\n                                ");
            builder.CloseElement();
            builder.AddContent(172, "\n                                |\n                                ");
            builder.OpenElement(173, "a");
            builder.AddAttribute(174, "class", "btn btn-danger");
            builder.AddAttribute(175, "href", "/delete-book/" + (book.Bookid));
            builder.AddMarkupContent(176, "\n                                    <span class=\"oi oi-trash\"></span>\n                                    Delete\n                                ");
            builder.CloseElement();
            builder.AddContent(177, "\n                            ");
            builder.CloseElement();
            builder.AddContent(178, "\n\n                        ");
            builder.CloseElement();
            builder.AddContent(179, "\n");
#line 145 "C:\Users\Salim Mayaleh\source\repos\PersonalLibrary\PersonalLibrary.Client\Pages\MyLibrary.cshtml"
                    }

#line default
#line hidden
            builder.AddContent(180, "                ");
            builder.CloseElement();
            builder.AddContent(181, "\n            ");
            builder.CloseElement();
            builder.AddContent(182, "\n");
#line 148 "C:\Users\Salim Mayaleh\source\repos\PersonalLibrary\PersonalLibrary.Client\Pages\MyLibrary.cshtml"
            break;
        case "getByAuthors":
            

#line default
#line hidden
#line 150 "C:\Users\Salim Mayaleh\source\repos\PersonalLibrary\PersonalLibrary.Client\Pages\MyLibrary.cshtml"
             foreach (var author in authorsBook)
            {

#line default
#line hidden
            builder.AddContent(183, "                ");
            builder.OpenElement(184, "h3");
            builder.AddContent(185, author.Name);
            builder.AddContent(186, ":");
            builder.CloseElement();
            builder.AddContent(187, "\n                ");
            builder.OpenElement(188, "ul");
            builder.AddContent(189, "\n");
#line 154 "C:\Users\Salim Mayaleh\source\repos\PersonalLibrary\PersonalLibrary.Client\Pages\MyLibrary.cshtml"
                     foreach (var book in author.Book)
                    {

#line default
#line hidden
            builder.AddContent(190, "                        ");
            builder.OpenElement(191, "li");
            builder.AddContent(192, book.Name);
            builder.CloseElement();
            builder.AddContent(193, "\n");
#line 157 "C:\Users\Salim Mayaleh\source\repos\PersonalLibrary\PersonalLibrary.Client\Pages\MyLibrary.cshtml"
                    }

#line default
#line hidden
            builder.AddContent(194, "                ");
            builder.CloseElement();
            builder.AddContent(195, "\n");
#line 159 "C:\Users\Salim Mayaleh\source\repos\PersonalLibrary\PersonalLibrary.Client\Pages\MyLibrary.cshtml"
            }

#line default
#line hidden
#line 159 "C:\Users\Salim Mayaleh\source\repos\PersonalLibrary\PersonalLibrary.Client\Pages\MyLibrary.cshtml"
             
            break;
        default:

#line default
#line hidden
            builder.AddContent(196, "            ");
            builder.AddMarkupContent(197, "<h2><em>Filtr doesnt exist</em></h2>\n");
#line 163 "C:\Users\Salim Mayaleh\source\repos\PersonalLibrary\PersonalLibrary.Client\Pages\MyLibrary.cshtml"
            break;
    }

#line default
#line hidden
#line 164 "C:\Users\Salim Mayaleh\source\repos\PersonalLibrary\PersonalLibrary.Client\Pages\MyLibrary.cshtml"
     
}

#line default
#line hidden
        }
        #pragma warning restore 1998
#line 167 "C:\Users\Salim Mayaleh\source\repos\PersonalLibrary\PersonalLibrary.Client\Pages\MyLibrary.cshtml"
            
    /* ukazka, jak nastavit componente parametr: */
    /*
    [Parameter]
    private string Title { get; set; }
    */

    //Book[] books;
    //Author[] authorsBook;

    List<Book> books = new List<Book>();
    List<Author> authorsBook = new List<Author>();

    string searchFor;
    List<Book> searchResult = new List<Book>();
    string FiltrType = "listBooks";

    bool isSearch = false;

    int? editBookId = null;

    /*
     * Volani api server
     */
    protected override async Task OnInitAsync()
    {
        FiltrType = "listBooks";
        books = await Http.GetJsonAsync<List<Book>>("api/mylibrary/getBooks");
        //books = await Http.GetJsonAsync<Book[]>("api/mylibrary/getBooks");

        //result = await Http.GetStringAsync("api/mylibrary/getBooks");

    }

    protected async Task GetBooksByAuthor()
    {
        //books = await Http.GetJsonAsync<Book[]>("sample-data/books.json");
        FiltrType = "getByAuthors";
        //authorsBook = await Http.GetJsonAsync<Author[]>("api/mylibrary/getBooksByAuthor");
        authorsBook = await Http.GetJsonAsync<List<Author>>("api/mylibrary/getBooksByAuthor");
    }


    private void SearchFor()
    {
        if (!string.IsNullOrEmpty(searchFor))
        {
            isSearch = true;
            /*
        searchResult = books.FindAll(
            x =>
                (x.Name.Contains(searchFor))
                //|| (x.Place.Contains(searchFor))
                //|| (x.About.Contains(searchFor))
                //|| (x.Author.Name.Contains(searchFor))
            );
            */
            searchResult = books.FindAll(
                delegate (Book bk)
                {
                    return (bk.Name.Contains(searchFor) 
                        //|| bk.Place.Contains(searchFor) 
                        //|| bk.About.Contains(searchFor)
                        //|| bk.Author.Name.Contains(searchFor)
                        );
                }
            );
        }
        else
        {

            isSearch = false;
        }
    }


#line default
#line hidden
        [global::Microsoft.AspNetCore.Blazor.Components.InjectAttribute] private HttpClient Http { get; set; }
    }
}
#pragma warning restore 1591
