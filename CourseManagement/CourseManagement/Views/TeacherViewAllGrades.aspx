<%@ Page Title="" Language="C#" MasterPageFile="~/CourseManagement.Master" AutoEventWireup="true" CodeBehind="TeacherViewAllGrades.aspx.cs" Inherits="CourseManagement.Views.TeacherViewAllGrades" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:GridView ID="gvwStudents" runat="server" OnSelectedIndexChanging="gvwGrade_SelectedIndexChanging" DataSourceID="ObjectDataSource1">
        <Columns>
            <asp:BoundField HeaderText="Student ID"/>
            <asp:BoundField HeaderText="First Name"/>
            <asp:BoundField HeaderText="Last Type"/>
            <asp:BoundField HeaderText="Overall Grade"/>
            <asp:BoundField HeaderText="Classification"/>
            <asp:BoundField HeaderText="Major"/>
            <asp:TemplateField ShowHeader="False">
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Select" Text="Select"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <br/>

    <asp:GridView ID="gvwGrade" runat="server" OnSelectedIndexChanging="gvwGrade_SelectedIndexChanging">
        <Columns>
            <asp:BoundField HeaderText="Student ID"/>
            <asp:BoundField HeaderText="Assignment Name"/>
            <asp:BoundField HeaderText="Assignment Type"/>
            <asp:BoundField HeaderText="Weight"/>
            <asp:BoundField HeaderText="Grade"/>
            <asp:TemplateField ShowHeader="False">
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Select" Text="Select"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>