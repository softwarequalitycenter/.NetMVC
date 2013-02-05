<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<MvcApplication1.Models.GamesModel>" %>

<%@ Import Namespace="MvcApplication1.GamesServiceReference" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Index
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%: ViewData["Message"] %>
    <div id="ajaxDiv">
        <% if (User.IsInRole("Administrators"))
           {
        %>
        <%  Html.RenderPartial("AdminGameList"); %>
        <% }
           else
           {
        %>
        <%  Html.RenderPartial("GameList"); %>
        <%} %>
    </div>
</asp:Content>
