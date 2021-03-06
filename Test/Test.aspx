﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Test.aspx.cs" Inherits="Test.Test" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Test</title>
    <style type="text/css">
        .controlStyle{
            padding:1px;
            margin:1px;

        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:HiddenField runat="server" ID="hdId" />
     <div>
        <div style="margin-left:40px">
            <div class="controlStyle">
            <asp:Label runat="server">Product Name is changed twice</asp:Label>
            <asp:TextBox runat="server" ID="txtProductName"></asp:TextBox>
                </div>
            <br />
            <br />
            <div class="controlStyle">
             <asp:Label runat="server">Department Name</asp:Label>
           <asp:DropDownList ID="ddlDepartment" runat="server">
               <asp:ListItem Value="0">Please Select</asp:ListItem>
           </asp:DropDownList>
                </div>
            <br />
            <br />
            <div class="controlStyle">
              <asp:Label runat="server">UOD</asp:Label>
           <asp:DropDownList ID="ddlUod" runat="server">
               <asp:ListItem Value="0">Please Select</asp:ListItem>
           </asp:DropDownList>
                </div>
            <br />
            <br />
            <div class="controlStyle">
             <asp:Label runat="server">Product Price$</asp:Label>
            <asp:TextBox runat="server" ID="txtProductPrice"></asp:TextBox>
                </div>
            <br />
            <br />
            <asp:Button ID="btnSubmit" runat="server" Text="Save" OnClientClick="return vaidate();" OnClick="btnSubmit_Click" />
        </div>

        <div id="maxpriceproduct" style="font-size:20px">
            <table>
                <tr>
                    <td>Product Name</td>
                    <td><asp:Label ID="lblPrdName" runat="server"></asp:Label></td>
                </tr>
            </table>
        </div>
        <div>
            <asp:GridView ID="gvproduct" runat="server" AutoGenerateColumns="false" AutoGenerateSelectButton="true" AutoGenerateDeleteButton="true"
                 OnRowDeleted="gvproduct_RowDeleted"  OnRowCommand="gvproduct_RowCommand" OnRowDeleting="gvproduct_RowDeleting">
                <Columns>
                    <asp:BoundField DataField="productId" HeaderText="Id" />
                      <asp:BoundField DataField="productCode" HeaderText="COoe" />  
                      <asp:BoundField DataField="productName" HeaderText="Product" />
                      <asp:BoundField DataField="departmentDesc" HeaderText="Department" />
                     <asp:BoundField DataField="uodDescription" HeaderText="UOD" />
                     <asp:BoundField DataField="productPrice" HeaderText="Price" />
                </Columns>
            </asp:GridView>
        </div>
     </div>
    </form>
    <script type="text/javascript">
        function vaidate() {
            try{
                var prd = document.getElementById('txtProductName').value;
                var dpt = document.getElementById('ddlDepartment');
                var dptSel ="0";
                var uodSel="0";
                //if (dpt != null) {
                //     dpt.options[dpt.selectedIndex].value;
                //}
                var uod = document.getElementById('ddlUod');
                //if (uod != null) {
                //    uod.options[uod.selectedIndex].value;
                //}
                var price = document.getElementById('txtProductPrice').value;
                console.log(typeof price);
                dptSel = dpt.value;
                uodSel = uod.value;
            
                if (prd == "" || dptSel == "0" || uodSel == "0" || price == "") {
                    alert('Select all details');
                    return false;
                }
                else if (!/^[a-zA-Z]*$/g.test(prd)) {
                    alert('Enter valid Product name');
                    return false;

                }
                else if (!/^[0-9]*$/g.test(price)) {
                    alert('Enter valid price');
                    return false;
                }
                else {
                    return true;
                }
                return false;
            }
            catch (ex) {
                console.log(ex);
                return false;
            }
        }
    </script>
</body>

</html>
