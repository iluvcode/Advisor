<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ContractorList.aspx.cs" Inherits="Advisor.ContractorList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        
        <asp:Repeater runat="server" ID="ContractorListRepeater" OnItemDataBound="ContractorListRepeater_OnItemDataBound" OnItemCommand="ContractorListRepeater_OnItemCommand">
            <ItemTemplate>
                <div>
                    <asp:Label runat="server" ID="CompanyNameLabel"></asp:Label>
                    <asp:Label runat="server" ID="AddressLineLabel"></asp:Label>
                    <asp:Label runat="server" ID="CityLabel"></asp:Label>
                    <asp:Label runat="server" ID="ZipCodeLabel"></asp:Label>
                </div>
            </ItemTemplate>
        </asp:Repeater>

    </div>
</asp:Content>
