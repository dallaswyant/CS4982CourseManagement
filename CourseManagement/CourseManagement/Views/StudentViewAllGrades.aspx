﻿<%@ Page Title="" Language="C#" MasterPageFile="~/CourseManagement.Master" AutoEventWireup="true" CodeBehind="StudentViewAllGrades.aspx.cs" Inherits="CourseManagement.Views.StudentViewAllGrades" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style3 {
            width: 819px;
        }
        .auto-style5 {
            height: 26px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="auto-style1">
        <tr>
            <td class="auto-style3" rowspan="2">
                <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" DataSourceID="ObjectDataSource1">
                </asp:GridView>
                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetCoursesByStudentID" TypeName="CourseManagement.DAL.CourseDAL">
                    <SelectParameters>
                        <asp:SessionParameter Name="studentUIDCheck" SessionField="UserID" Type="String" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </td>
            <td>
                <asp:Label ID="Label2" runat="server" Text="Prerequisites:"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:ListView ID="lvwPrerequisites" runat="server">
                </asp:ListView>
            </td>
        </tr>
        <tr>
            <td class="auto-style5" colspan="2">
                <asp:Label ID="Label1" runat="server" Text="Description:"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="auto-style5" colspan="2">
                <asp:Label ID="lblDescription" runat="server"></asp:Label>
            </td>
        </tr>
    </table>
    <br />
    <asp:GridView ID="GridView1" runat="server" OnSelectedIndexChanging="GridView1_SelectedIndexChanging">
        <Columns>
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