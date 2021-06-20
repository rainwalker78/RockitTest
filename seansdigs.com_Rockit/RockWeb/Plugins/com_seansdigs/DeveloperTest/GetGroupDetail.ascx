<%@ Control Language="C#" AutoEventWireup="true" CodeFile="GetGroupDetail.ascx.cs" Inherits="RockWeb.Plugins.com_seansdigs.DeveloperTest.GetGroupDetail" %>

<asp:UpdatePanel ID="upnlContent" runat="server">
    <ContentTemplate>
 

        <asp:Panel ID="pnlView" runat="server" CssClass="panel panel-block">
        
            <div class="panel-heading">
                <h1 class="panel-title">
                    <i class="fa fa-star"></i> 
                    Group Details (Developer Test)
                </h1>

                <div class="panel-labels">
                    <Rock:HighlightLabel ID="hlblTest" runat="server" LabelType="Info" Text="Label" />
                </div>
            </div>
            <Rock:PanelDrawer ID="pdAuditDetails" runat="server"></Rock:PanelDrawer>
            <div class="panel-body">

                <div class="alert alert-info">
                     <p><h4><asp:Label id="lblName" runat="server"/></h4></p>                    
                              
                    <p>Group Capacity: <asp:Label id="lblCapacity"  runat="server"/></p>

                    <p><asp:Label id="lblDescription"  runat="server"/></p>
    
                    <p><Strong>Created on</Strong> <asp:Label id="lblCreatedOn"  runat="server"/> | <Strong>Modified On</Strong>  <asp:Label id="lblModifiedOn"  runat="server"/></p>
   

                </div>
            </div>
        
        </asp:Panel>

    </ContentTemplate>
</asp:UpdatePanel>