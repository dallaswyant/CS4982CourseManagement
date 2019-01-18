﻿<%@ Page Title="" Language="C#" MasterPageFile="~/CourseManagement.Master" AutoEventWireup="true" CodeBehind="RubricPage.aspx.cs" Inherits="CourseManagement.Views.RubricPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:GridView ID="GridView1" runat="server">
        <Columns>
            <asp:BoundField HeaderText="Assignment Type" />
            <asp:BoundField HeaderText="Assignment Weight" />
            <asp:CommandField ShowEditButton="True" />
            <asp:CommandField ShowDeleteButton="True" />
        </Columns>
    </asp:GridView>
    <asp:DetailsView ID="DetailsView1" runat="server" Height="50px" Width="333px">
        <Fields>
            <asp:BoundField HeaderText="Assignment Type" />
            <asp:BoundField HeaderText="Weight" />
            <asp:CommandField ShowInsertButton="True" />
        </Fields>
    </asp:DetailsView>
</asp:Content>
