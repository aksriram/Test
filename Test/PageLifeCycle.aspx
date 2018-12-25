<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PageLifeCycle.aspx.cs" Inherits="Test.PageLifeCycle" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
       <asp:Label ID="sd" runat="server">Id</asp:Label>
       <asp:Button ID="btnSave" Text="Save" runat="server" OnClick="btnSave_Click" />
    </div>
    </form>
</body>
</html>
