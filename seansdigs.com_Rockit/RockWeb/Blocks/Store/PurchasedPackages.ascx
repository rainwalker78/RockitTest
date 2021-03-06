<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PurchasedPackages.ascx.cs" Inherits="RockWeb.Blocks.Store.PurchasedPackages" %>

<asp:UpdatePanel ID="upnlContent" runat="server">
    <ContentTemplate>

        <asp:Panel ID="pnlView" runat="server" CssClass="panel panel-block">

            <div class="panel-heading">
                <h1 class="panel-title">
                    <i class="fa fa-gift"></i>
                    Purchased Packages
                </h1>

                <div class="panel-labels">
                    <asp:LinkButton ID="btnRevokeKey" runat="server" CssClass="btn btn-xs btn-default" Text="Revoke Store Key" OnClick="btnRevokeKey_Click" />
                </div>
            </div>
            <div class="panel-body">

                <asp:Panel ID="pnlPackages" runat="server">
                    <asp:Repeater ID="rptPurchasedProducts" runat="server" OnItemDataBound="rptPurchasedProducts_ItemDataBound" OnItemCommand="rptPurchasedProducts_ItemCommand">
                        <ItemTemplate>
                            <div class="purchasedpackage row mb-4">
                                <div class="col-md-3">
                                    <img class="img-responsive mx-auto" src="<%# Eval( "PackageIconBinaryFile.ImageUrl" ) %>&h=140&w=280&zoom=2&mode=crop" width="280" height="140">
                                </div>
                                <div class="col-md-7 mb-3">
                                    <h1><%# Eval( "Name" ) %></h1>

                                    <div class="clearfix mb-2">
                                        <div class="pull-left"><strong>Purchased</strong><br><%# string.Format("{0:M/d/yyyy}", Eval("PurchasedDate"))%></div>
                                        <div class="pull-right"><strong>Purchased by</strong><br><%# Eval( "Purchaser" ) %></div>
                                    </div>
                                    <div>
                                        <%# Eval( "Description" ) %>
                                    </div>
                                    <div class="margin-t-md">
                                        <asp:LinkButton ID="lbPackageDetails" runat="server" CssClass="btn btn-default btn-sm mb-2" CommandName="PackageDetails" CommandArgument='<%#Eval("Id") %>'>Package Details</asp:LinkButton>
                                    </div>
                                </div>
                                <div class="col-md-2 purchasedpackage-install">
                                    <asp:LinkButton ID="lbInstall" runat="server" CssClass="btn btn-primary mb-3" CommandName="Install" CommandArgument='<%#Eval("Id") %>'>Install</asp:LinkButton>

                                    <asp:Literal ID="lVersionNotes" runat="server"></asp:Literal>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>

                    <asp:Literal ID="lMessages" runat="server" />
                </asp:Panel>

                <asp:Panel ID="pnlError" runat="server" Visible="false">
                    <div class="alert alert-warning">
                        <h4>Store Currently Not Available</h4>
                        <p>We're sorry, the Rock Store is currently not available. Check back soon!</p>
                        <small><em><asp:Literal ID="lErrorMessage" runat="server" /></em></small>
                    </div>
                </asp:Panel>
            </div>

        </asp:Panel>

    </ContentTemplate>
</asp:UpdatePanel>
