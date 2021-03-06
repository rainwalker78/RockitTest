<%@ Control Language="C#" AutoEventWireup="true" CodeFile="EventItemOccurrencesSearchLava.ascx.cs" Inherits="RockWeb.Blocks.Event.EventItemOccurrencesSearchLava" %>

<asp:UpdatePanel ID="upnlContent" runat="server">
    <ContentTemplate>
        <div class="event-search-filters">
            <div class="row">
                <div class="col-md-4" id="divCampus" runat="server">
                    <Rock:CampusPicker ID="cpCampusPicker" runat="server" Label="&nbsp;" AutoPostBack="true" OnSelectedIndexChanged="btnSearch_Click" />
                </div>
                <div class="col-md-4" id="divDateRange" runat="server">
                    <Rock:DateRangePicker ID="pDateRange" runat="server" Label="&nbsp;" />
                    <asp:LinkButton ID="btnSearch" runat="server" Style="display: none" OnClick="btnSearch_Click" />
                </div>
                <div class="col-md-4">
                    <Rock:RockCheckBoxList ID="cblAudience" runat="server" Label="Audiences" DataTextField="Value" DataValueField="Id" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="btnSearch_Click" />
                </div>
            </div>
        </div> 

        <asp:Literal ID="lResults" runat="server" />

        <script type="text/javascript">
            Sys.Application.add_load(function () {
                $('#<%=pDateRange.ClientID%> .js-date-picker').on('changeDate', function () {
                    window.location = $('#<%=btnSearch.ClientID%>').prop('href');
                });
            });
        </script>

    </ContentTemplate>
</asp:UpdatePanel>
