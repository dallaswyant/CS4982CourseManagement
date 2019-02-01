<%@ Page Title="" Language="C#" MasterPageFile="~/CourseManagement.Master" AutoEventWireup="true" CodeBehind="RubricPage.aspx.cs" Inherits="CourseManagement.Views.RubricPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:DropDownList ID="ddlCourse" runat="server" DataSourceID="odsCourses" DataTextField="Name" DataValueField="CRN" OnSelectedIndexChanged="ddlCourse_SelectedIndexChanged">
    </asp:DropDownList>
    <asp:ObjectDataSource ID="odsCourses" runat="server" SelectMethod="GetCourseBulletinByTeacherID" TypeName="CourseManagement.DAL.CourseDAL" OldValuesParameterFormatString="original_{0}">
        <SelectParameters>
            <asp:SessionParameter Name="teacherIDCheck" SessionField="UserID" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:GridView ID="gvwWeights" runat="server" AutoGenerateColumns="False" DataSourceID="odsRubricItems" OnRowUpdated="GridView1_RowUpdated" OnRowUpdating="gvwWeights_RowUpdating" OnRowDeleting="gvwWeights_RowDeleting" CssClass="table">
        <Columns>
            <asp:BoundField DataField="Index" HeaderText="Index" SortExpression="Index" ReadOnly="True" />
            <asp:BoundField DataField="CRN" HeaderText="CRN" SortExpression="CRN" ReadOnly="True" />
            <asp:BoundField DataField="AssignmentType" HeaderText="AssignmentType" SortExpression="AssignmentType" />
            <asp:BoundField DataField="AssignmentWeight" HeaderText="AssignmentWeight" SortExpression="AssignmentWeight" />
            <asp:CommandField ShowEditButton="True" />
            <asp:CommandField ShowDeleteButton="True" />
        </Columns>
    </asp:GridView>
    <asp:ObjectDataSource ID="odsRubricItems" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetCourseRubricByCRN" TypeName="CourseManagement.DAL.CourseRubricDAL" UpdateMethod="UpdateCourseRubric" DeleteMethod="DeleteCourseRubric" InsertMethod="InsertCourseRubric" ConflictDetection="CompareAllValues">
        <DeleteParameters>
            <asp:Parameter Name="CRN" Type="Int32" />
            <asp:Parameter Name="assignmentType" Type="String" />
            <asp:Parameter Name="assignmentWeight" Type="Int32" />
            <asp:Parameter Name="index" Type="Int32" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="assignmentType" Type="String" />
            <asp:Parameter Name="assignmentWeight" Type="Int32" />

        </InsertParameters>
        <SelectParameters>
            <asp:ControlParameter ControlID="ddlCourse" Name="CRNCheck" PropertyName="SelectedValue" Type="Int32" />
        </SelectParameters>
        <UpdateParameters>
            <asp:Parameter Name="CRN" Type="Int32" />
            <asp:Parameter Name="assignmentType" Type="String" />
            <asp:Parameter Name="assignmentWeight" Type="Int32" />
            
            <asp:Parameter Name="original_AssignmentType" Type="string"/>
            <asp:Parameter Name="original_AssignmentWeight" Type="Int32"/>
            <asp:Parameter Name="index" Type="Int32" />
            <asp:Parameter Name="original_Index" Type="Int32" />
            <asp:Parameter Name="original_Crn" Type="Int32" />
        </UpdateParameters>
    </asp:ObjectDataSource>
    <asp:DetailsView ID="DetailsView1" runat="server" Width="500px" AutoGenerateRows="False" DataSourceID="odsRubricItems" DefaultMode="Insert" CssClass="table">
        <Fields>
            <asp:BoundField HeaderText="AssignmentType" DataField="AssignmentType" SortExpression="AssignmentType"/>
            <asp:BoundField DataField="AssignmentWeight" HeaderText="AssignmentWeight" SortExpression="AssignmentWeight" />
            <asp:CommandField ShowCancelButton="False" ShowInsertButton="True" />
        </Fields>
    </asp:DetailsView>
</asp:Content>