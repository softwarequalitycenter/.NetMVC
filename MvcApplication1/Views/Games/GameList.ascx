<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<MvcApplication1.Models.GamesModel>" %>
<%@ Import Namespace="MvcApplication1.GamesServiceReference" %>

<%: ViewData["Message"] %>
<table>
    <tr>
        <td colspan="3">
            <h2>
                Games we have</h2>
        </td>
    </tr>
    <tr>
        <td>
            Game Id
        </td>
        <td>
            Title
        </td>
        <td>
            Status
        </td>
    </tr>
    <% ArrayList games = Model.GetGamesWeHave();
       foreach (XboxGame game in games)
       {
    %>
    <tr>
        <td>
            <%= Html.Encode(game.Id)%>
        </td>
        <td>
            <%= Html.Encode(game.Title)%>
        </td>
        <td>
            <%= Html.Encode(game.Status)%>
        </td>
    </tr>
    <% } %>
</table>
<table>
    <tr>
        <td colspan="6">
            <h2>
                Games we want</h2>
        </td>
    </tr>
    <tr>
        <td style="height: 25px">
            Game Id
        </td>
        <td style="height: 25px">
            Title
        </td>
        <td style="height: 25px">
            Status
        </td>
        <td style="height: 25px">
            Votes
        </td>
        <td style="height: 25px">
            &nbsp;
        </td>
        <td style="height: 25px">
            &nbsp;
        </td>
        <td style="height: 25px">
            &nbsp;
        </td>
    </tr>
    <% games = Model.GetGamesWeWant();
       foreach (XboxGame game in games)
       {
    %>
    <tr>
        <td>
            <%= Html.Encode(game.Id)%>
        </td>
        <td>
            <%= Html.Encode(game.Title)%>
        </td>
        <td>
            <%= Html.Encode(game.Status)%>
        </td>
        <td>
            <%= Html.Encode(game.Votes)%>
        </td>
        <td>
            <%= Html.ActionLink("Vote this Game", "VoteGame", new { id = game.Id })%>
        </td>

    </tr>
    <% } %>
    <tr>
        <td colspan="6">
            <%= Html.ActionLink("Add a new Title", "AddGame") %>
        </td>
    </tr>
</table>
