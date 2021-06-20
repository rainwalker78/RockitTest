<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CustomPeopleList.ascx.cs" Inherits="RockWeb.Blocks.DeveloperTest.CustomPeopleList" %>

<asp:UpdatePanel ID="upList" runat="server">
    <ContentTemplate>
        <asp:Panel ID="pnlContent" runat="server">
            <style scoped="scoped">
                .table > tbody > tr.is-unknown-gender > td {
                    background-color: #fcf8e3;
                }
                .table > tbody > tr.is-underage > td {
                    background-color: #f2dede;
                }
            </style>
            
            <div class="panel panel-block">
                <div class="panel-heading clearfix">
                    <h1 class="panel-title pull-left">
                        <i class="fa fa-users"></i>
                        <asp:Literal ID="lHeading" runat="server" Text="People" />
                    </h1>

                    <div class="panel-labels">
                        <asp:HyperLink ID="hlSyncSource" runat="server"><Rock:HighlightLabel ID="hlSyncStatus" runat="server" LabelType="Info" Visible="false" Text="<i class='fa fa-exchange'></i>" /></asp:HyperLink> &nbsp;
                    </div>
                </div>

                <div class="panel-body">
                    <div class="grid grid-panel">
                        <Rock:GridFilter ID="rFilter" runat="server">
                            <Rock:RockTextBox ID="tbFirstName" runat="server" Label="First Name" />
                            <Rock:RockTextBox ID="tbLastName" runat="server" Label="Last Name" />
                            <Rock:RockTextBox ID="tbFunnyNickName" runat="server" Label="Funny Nick Name" />
                            <Rock:RockCheckBoxList ID="cblGenderFilter" runat="server" RepeatDirection="Horizontal" Label="Gender">
                                <asp:ListItem Text="Male" Value="Male" />
                                <asp:ListItem Text="Female" Value="Female" />
                                <asp:ListItem Text="Unknown" Value="Unknown" />
                            </Rock:RockCheckBoxList>
                        </Rock:GridFilter>
                        <Rock:Grid ID="gPeople" runat="server" DisplayType="Full" AllowSorting="true"
                            DataKeyNames="Id, FirstName, NickName, FunnyNickName, LastName"
                            OnRowDataBound="gPeople_RowDataBound" >
                            <Columns>
                                <Rock:RockBoundField DataField="FirstName" HeaderText="First Name" SortExpression="FirstName" />
                                <Rock:RockBoundField DataField="NickName" HeaderText="Nick Name" SortExpression="NickName" />
                                <Rock:RockBoundField DataField="FunnyNickName" HeaderText="Funny Nick Name" SortExpression="FunnyNickName" />
                                <Rock:RockBoundField DataField="LastName" HeaderText="Last Name" SortExpression="LastName" />
                            </Columns>
                        </Rock:Grid>
                    </div>
                </div>
            </div>

        </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>
