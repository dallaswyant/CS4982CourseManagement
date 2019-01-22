<%@ Page Title="" Language="C#" MasterPageFile="~/CourseManagement.Master" AutoEventWireup="true" CodeBehind="RubricPage.aspx.cs" Inherits="CourseManagement.Views.RubricPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:DropDownList ID="ddlCourse" runat="server" DataSourceID="odsCourses" DataTextField="Name" DataValueField="CRN">
    </asp:DropDownList>
    <asp:ObjectDataSource ID="odsCourses" runat="server" SelectMethod="GetCourseBulletinByTeacherID" TypeName="CourseManagement.DAL.CourseDAL" OldValuesParameterFormatString="original_{0}">
        <SelectParameters>
            <asp:SessionParameter Name="teacherIDCheck" SessionField="UserID" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="odsWeights">
        <Columns>
            <asp:BoundField HeaderText="Assignment Type" DataField="Key" SortExpression="Key"/>
            <asp:BoundField HeaderText="Weight" DataField="Value" ReadOnly="True" SortExpression="Value"/>
        </Columns>
    </asp:GridView>
    <asp:ObjectDataSource ID="odsWeights" runat="server" SelectMethod="GetCourseRubricByCRN" TypeName="CourseManagement.DAL.CourseRubricDAL" UpdateMethod="UpdateCourseRubric">
        <SelectParameters>
            <asp:ControlParameter ControlID="ddlCourse" Name="CRNCheck" PropertyName="SelectedValue" Type="Int32" />
        </SelectParameters>
        <UpdateParameters>
            <asp:Parameter Name="CRN" Type="Int32" />
            <asp:Parameter Name="rubricToUpdate" Type="Object" />
        </UpdateParameters>
    </asp:ObjectDataSource>
    <asp:DetailsView ID="DetailsView1" runat="server" Height="50px" Width="333px">
        <Fields>
            <asp:BoundField HeaderText="Assignment Type"/>
            <asp:BoundField HeaderText="Weight"/>
            <asp:CommandField ShowInsertButton="True"/>
        </Fields>
    </asp:DetailsView>
</asp:Content>