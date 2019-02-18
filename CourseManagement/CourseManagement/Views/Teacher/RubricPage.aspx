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
    <asp:GridView ID="gvwWeights" runat="server" AutoGenerateColumns="False" DataSourceID="odsRubricItems" OnRowDeleting="gvwWeights_RowDeleting" CssClass="table" OnRowDeleted="gvwWeights_RowDeleted" OnRowUpdated="gvwWeights_RowUpdated">
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
    <asp:Label runat="server" ID="lblWarning"></asp:Label>
    <asp:DetailsView ID="dvwAddGradeItem" runat="server" Width="500px" AutoGenerateRows="False" DataSourceID="odsRubricItems" DefaultMode="Insert" CssClass="table" OnItemInserted="dvwAddGradeItem_ItemInserted">
        <Fields>
            <asp:TemplateField HeaderText="AssignmentType" SortExpression="AssignmentType">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("AssignmentType") %>'></asp:TextBox>
                </EditItemTemplate>
                <InsertItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("AssignmentType") %>'></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvAssignmentType" runat="server" ControlToValidate="TextBox1" CssClass="error" ErrorMessage="*"></asp:RequiredFieldValidator>

                </InsertItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("AssignmentType") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="AssignmentWeight" SortExpression="AssignmentWeight">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("AssignmentWeight") %>'></asp:TextBox>
                </EditItemTemplate>
                <InsertItemTemplate>
                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("AssignmentWeight") %>'></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvAssignmentType" runat="server" ControlToValidate="TextBox2" CssClass="error" ErrorMessage="*"></asp:RequiredFieldValidator>
                </InsertItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("AssignmentWeight") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:CommandField ShowCancelButton="False" ShowInsertButton="True" />
        </Fields>
    </asp:DetailsView>
</asp:Content>