<%@ Control Language="C#" AutoEventWireup="true" CodeFile="HelloWorldFetchingData.ascx.cs" Inherits="RockWeb.Plugins.com_seansdigs.Tutorials.HelloWorldFetchingData" %>

<asp:UpdatePanel ID="upnlContent" runat="server">
    <ContentTemplate>

<ContentTemplate>

    <Rock:Grid ID="gPeople" runat="server" AllowSorting="true" OnRowSelected="gPeople_RowSelected" DataKeyNames="Id">
        <Columns>
            <asp:BoundField DataField="FirstName" HeaderText="First Name" />
            <asp:BoundField DataField="LastName" HeaderText="Last Name" />
        </Columns>
    </Rock:Grid>

</ContentTemplate>

    </ContentTemplate>
</asp:UpdatePanel>