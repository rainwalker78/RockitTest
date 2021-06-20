<%@ Control Language="C#" AutoEventWireup="true" CodeFile="GetGroups.ascx.cs" Inherits="RockWeb.Plugins.com_seansdigs.DeveloperTest.GetGroups" %>

<asp:UpdatePanel ID="upnlContent" runat="server">
    <ContentTemplate>

<ContentTemplate>

    <Rock:Grid ID="gGroups" runat="server" AllowSorting="true" OnRowSelected="gGroups_RowSelected" DataKeyNames="Id">
        <Columns>
            <asp:BoundField DataField="Name" HeaderText="Name" />
            <asp:BoundField DataField="Description" HeaderText="Description" />
            <asp:BoundField DataField="GroupCapacity" HeaderText="Capacity" />
            <asp:BoundField DataField="CreatedDateTime" HeaderText="Created On" />
            <asp:BoundField DataField="ModifiedDateTime" HeaderText="Modified On" />
        </Columns>
    </Rock:Grid>

</ContentTemplate>

    </ContentTemplate>
</asp:UpdatePanel>