using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Test
{
    public partial class Test : System.Web.UI.Page
    {
        static string connectionStr = string.Empty;
        protected Test()
        {
            connectionStr = System.Configuration.ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataTable dt = new DataTable();
                using (SqlConnection con = new SqlConnection(connectionStr))
                {
                    string commandText = "Select depId,departmentDesc from department";
                    SqlDataAdapter sqlDa = new SqlDataAdapter(commandText, con);
                    sqlDa.Fill(dt);
                }
                if (dt.Rows.Count > 0)
                {
                    ddlDepartment.DataSource = dt;
                    ddlDepartment.DataValueField = "depId";
                    ddlDepartment.DataTextField = "departmentDesc";
                    ddlDepartment.Items.Insert(0, new ListItem("Please Select", "0"));
                    ddlDepartment.DataBind();
                }
                else
                {
                    ddlDepartment.DataSource = null;
                    ddlDepartment.DataBind();
                }

                //To Load UOD
                DataTable dt1 = new DataTable();
                using (SqlConnection con = new SqlConnection(connectionStr))
                {
                    string commandText = "select [uodId],[uodDescription] from [dbo].[tblUnitofMeasure]";
                    SqlDataAdapter sqlDa = new SqlDataAdapter(commandText, con);
                    sqlDa.Fill(dt1);
                }
                if (dt1.Rows.Count > 0)
                {
                    ddlUod.DataSource = dt1;
                    ddlUod.DataValueField = "uodId";
                    ddlUod.DataTextField = "uodDescription";
                    ddlUod.Items.Insert(0, new ListItem("Please Select", "0"));
                    ddlUod.DataBind();
                }
                else
                {
                    ddlUod.DataSource = null;
                    ddlUod.DataBind();
                }

                LoadGrid();
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (hdId.Value == "")
            {
                string code = txtProductName.Text.Substring(0, 1);
                string random = new Random(10).Next().ToString();
                code = code + random + DateTime.Now.Day + DateTime.Now.Month;
                using (SqlConnection con = new SqlConnection(connectionStr))
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    con.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "spInsertProduct";
                    SqlParameter p = new SqlParameter("@Name", txtProductName.Text.Trim());
                    SqlParameter p1 = new SqlParameter("@code", code);
                    SqlParameter p2 = new SqlParameter("@depId", ddlDepartment.SelectedValue);
                    SqlParameter p3 = new SqlParameter("@UomId", ddlUod.SelectedValue);
                    SqlParameter p4 = new SqlParameter("@price", txtProductPrice.Text.Trim());
                    cmd.Parameters.AddRange(new SqlParameter[] { p, p1, p2, p3, p4 });
                    int output = cmd.ExecuteNonQuery();
                    if (output != 0)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "Script", "alert('Inserted Successfullt');", true);
                        LoadGrid();
                        txtProductName.Text = "";
                        txtProductPrice.Text = "";
                        ddlDepartment.ClearSelection();
                        ddlUod.ClearSelection();
                    }
                }
            }
            else
            {
                using (SqlConnection con = new SqlConnection(connectionStr))
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    con.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "spUpdateProduct";
                    SqlParameter p1 = new SqlParameter("@id", Convert.ToInt32(hdId.Value));
                    SqlParameter p = new SqlParameter("@Name", txtProductName.Text.Trim());
                    SqlParameter p2 = new SqlParameter("@depId", ddlDepartment.SelectedValue);
                    SqlParameter p3 = new SqlParameter("@UomId", ddlUod.SelectedValue);
                    SqlParameter p4 = new SqlParameter("@price", txtProductPrice.Text.Trim());
                    cmd.Parameters.AddRange(new SqlParameter[] { p,p1, p2, p3, p4 });
                    int output = cmd.ExecuteNonQuery();
                    if (output != 0)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "Script", "alert('Updated Successfullt');", true);
                        LoadGrid();
                        txtProductName.Text = "";
                        txtProductPrice.Text = "";
                        ddlDepartment.ClearSelection();
                        ddlUod.ClearSelection();
                    }
                }
            }
        }

        private void LoadGrid()
        {
            //To Load Grid View
            DataTable dtGrid = new DataTable();
            using (SqlConnection con = new SqlConnection(connectionStr))
            {
                string commandText = @"select productId,productCode,productName,departmentDesc,uodDescription,productPrice from tblProductDtl pd inner join 
                                        tblUnitofMeasure uod on pd.Uom = uod.uodId inner join
                                        department dp on dp.depId = pd.departmentId";
                SqlDataAdapter sqlDa = new SqlDataAdapter(commandText, con);
                sqlDa.Fill(dtGrid);
            }
            if (dtGrid.Rows.Count > 0)
            {
                gvproduct.DataSource = dtGrid;
                gvproduct.DataBind();
            }
            else
            {
                gvproduct.DataSource = null;
                gvproduct.DataBind();
            }
        }

        protected void gvproduct_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            var c = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = gvproduct.Rows[c];
            int id = Convert.ToInt32(row.Cells[1].Text.Trim() == "" ? "0" : row.Cells[1].Text.Trim());
           
            if (e.CommandName == "Select")
            {
                txtProductName.Text = row.Cells[3].Text.Trim();
                ddlDepartment.Items.FindByText(row.Cells[4].Text.Trim()).Selected = true;
                ddlUod.Items.FindByText(row.Cells[5].Text.Trim()).Selected = true;
                txtProductPrice.Text = row.Cells[6].Text.Trim();
            }
            else if (e.CommandName == "Delete")
            {
                using (SqlConnection con= new SqlConnection(connectionStr))
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "delete from tblProductDtl where [productId]=@id";
                    cmd.Parameters.AddWithValue("@id",id);
                    cmd.Connection.Open();
                    int output= cmd.ExecuteNonQuery();
                    if (output != 0)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "Script", "alert('Deleted Successfullt');", true);
                        LoadGrid();
                    }
                }
            }
        }

        protected void gvproduct_RowDeleted(object sender, GridViewDeletedEventArgs e)
        {

        }

        protected void gvproduct_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }
    }
}