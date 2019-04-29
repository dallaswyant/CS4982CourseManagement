<%@ Page Title="" Language="C#" MasterPageFile="~/CourseManagement.Master" AutoEventWireup="true" CodeBehind="DegreeProgramPage.aspx.cs" Inherits="CourseManagement.Views.Student.DegreeProgramPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    Degree Program:<br />
    <asp:DropDownList ID="DropDownList1" runat="server" DataTextField="DegreeProgramName" DataValueField="DegreeProgramName" AutoPostBack="True" DataSourceID="odsTranscript" ToolTip="Your degree program.">
    </asp:DropDownList>
    <br />
    <br />
    
    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AutoGenerateColumns="False" DataSourceID="odsTranscript" PageSize="15">
        <Columns>
            <asp:BoundField DataField="CRN" HeaderText="CRN" SortExpression="CRN" />
            <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
            <asp:BoundField DataField="CourseStatus" HeaderText="CourseStatus" ReadOnly="True" SortExpression="CourseStatus" />
            <asp:CheckBoxField DataField="IsRequired" HeaderText="IsRequired" ReadOnly="True" SortExpression="IsRequired" />
        </Columns>
    </asp:GridView>
    <asp:ObjectDataSource ID="odsTranscript" runat="server" SelectMethod="GetDegreeReport" TypeName="CourseManagement.DAL.TranscriptDAL">
        <SelectParameters>
            <asp:SessionParameter Name="studentIDCheck" SessionField="UserID" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <br />
</asp:Content>
