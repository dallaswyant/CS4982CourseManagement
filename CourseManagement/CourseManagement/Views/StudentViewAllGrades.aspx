<%@ Page Title="" Language="C#" MasterPageFile="~/CourseManagement.Master" AutoEventWireup="true" CodeBehind="StudentViewAllGrades.aspx.cs" Inherits="CourseManagement.Views.StudentViewAllGrades" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:GridView ID="GridView1" runat="server" OnSelectedIndexChanging="GridView1_SelectedIndexChanging">
        <Columns>
            <asp:BoundField HeaderText="Assignment Name" />
            <asp:BoundField HeaderText="Assignment Type" />
            <asp:BoundField HeaderText="Weight" />
            <asp:BoundField HeaderText="Grade" />
            <asp:TemplateField ShowHeader="False">
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Select" Text="Select"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>
