<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<MvcApplication1.GamesServiceReference.XboxVotingServiceSoapClient>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    AddGame
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%: ViewData["Message"] %>
    <h2>AddGame</h2>
    <% using (Html.BeginForm())
    {
    %>
    <%: Html.ValidationSummary(true, "The game could not be added. Please Enter a unique title.") %>
    <fieldset>
        <legend>Fields</legend>
        <p>
            <label for="Title">Title:</label>
            <%= Html.TextBox("Title") %>
            <%= Html.ValidationMessage("Title", "*") %>
        </p>
        <p>
            <input type="submit" value="Create" />
        </p>
    </fieldset>
    <% } %>
    <div>
        <%= Html.ActionLink("Back to List", "Index") %>
    </div>
</asp:Content>
