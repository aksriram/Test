<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Test2.aspx.cs" Inherits="Test.Test2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Grid Views</title>
    <style type="text/css">
        table{
            border:thin;
            border-radius:5px;
            border-collapse:collapse;
            border-color:rgba(226, 62, 62, 0.68);
        }
        .thead{
            min-height:20px;
            background-color:#d88239;
            opacity:50;
            margin:20px;
        }
        table tr{
            min-height:15px;
            padding :10px;
            border-radius:5px;
        }
        table td th{
            padding:10px ;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
            <asp:GridView ID="gvProduct" runat="server" AllowPaging="true" AutoGenerateColumns="false" AllowCustomPaging="true"  PageIndex="0" PageSize="2" OnPageIndexChanging="gvProduct_PageIndexChanging"
              HeaderStyle-CssClass="thead" PagerSettings-FirstPageText="Start">
                <Columns>
                    <asp:BoundField DataField="productId" HeaderText="Id" />
                      <asp:BoundField DataField="productCode"  HeaderText="Code" />
                      <asp:BoundField DataField="productName" HeaderText="Name" />  
                    <asp:BoundField DataField="departmentDesc"    HeaderText="Department" />
                      <asp:BoundField DataField="uodDescription"  HeaderText="UOD" />
                      <asp:BoundField DataField="productPrice" HeaderText="Price" />
                      <asp:BoundField DataField="rowId"  HeaderText="Row Id" />
                </Columns>
            </asp:GridView>

        <br />
        <asp:GridView ID="gvPrd" runat="server" AllowPaging="true" AutoGenerateColumns="false" AllowCustomPaging="true"  PageIndex="0" PageSize="2" OnPageIndexChanging="gvProduct_PageIndexChanging"
              HeaderStyle-CssClass="thead" PagerSettings-FirstPageText="Start">
                <Columns>
                      <asp:BoundField DataField="productId" HeaderText="Id" />
                      <asp:BoundField DataField="productCode" HeaderText="Code" />
                      <asp:BoundField DataField="productName" HeaderText="Name" />  
                      <asp:BoundField DataField="departmentDesc" HeaderText="Department" />
                      <asp:BoundField DataField="uodDescription" HeaderText="UOD" />
                      <asp:BoundField DataField="productPrice" HeaderText="Price" />
                      <asp:BoundField DataField="rowId" HeaderText="Row Id" />
                </Columns>
            </asp:GridView>
    </div>
    </form>
</body>
</html>
