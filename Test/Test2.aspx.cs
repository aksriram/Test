using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace Test
{
    public partial class Test2 : System.Web.UI.Page
    {
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["constr"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GridLoad(0, 2);
                LoadGridProd();
            }
        }

        private void GridLoad(int pageIndex,int pageSize)
        {
            DataSet dt = new DataSet();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string commandText = @"with sh(productId,productCode,productName,departmentDesc,uodDescription,productPrice,rowId)
                                        as
                                        (
                                        select productId,productCode,productName,departmentDesc,uodDescription,productPrice,row_number() over (order by [productId]) rowId from tblProductDtl pd inner join 
                                        tblUnitofMeasure uod on pd.Uom = uod.uodId inner join
                                        department dp on dp.depId = pd.departmentId 
                                        )
                                        select * from sh where rowId between (@pageIndex*@pageSize)+1 and (@pageIndex+1)*@pageSize;
                                       select count(*) from  [dbo].[tblProductDtl]";
                SqlDataAdapter sqlDa = new SqlDataAdapter(commandText, con);
                SqlParameter p = new SqlParameter("@pageIndex",pageIndex);
                SqlParameter p1 = new SqlParameter("@pageSize", pageSize);
                sqlDa.SelectCommand.Parameters.AddRange(new SqlParameter[] { p, p1 });
                sqlDa.Fill(dt);
            }
            if (dt.Tables[1].Rows.Count > 0)
            {
                gvProduct.VirtualItemCount = Convert.ToInt32(dt.Tables[1].Rows[0][0]);
                gvProduct.DataSource = dt.Tables[0];
                gvProduct.DataBind();
            }
        }

        private void LoadGridProd()
        {
            DataSet dt = new DataSet();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string commandText = @"select productId,productCode,productName,departmentDesc,uodDescription,productPrice,row_number() over (order by [productId]) rowId from tblProductDtl pd inner join 
                                        tblUnitofMeasure uod on pd.Uom = uod.uodId inner join
                                        department dp on dp.depId = pd.departmentId 
                                       ";
                                       
                SqlDataAdapter sqlDa = new SqlDataAdapter(commandText, con);
                sqlDa.Fill(dt);
            }
            if (dt.Tables[0].Rows.Count > 0)
            {
                gvPrd.DataSource = dt.Tables[0];
                gvPrd.DataBind();
            }
        }

        protected void gvProduct_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvProduct.PageIndex = e.NewPageIndex;
            GridLoad(e.NewPageIndex,2);
        }

        protected void gvProduct_Sorting(object sender, GridViewSortEventArgs e)
        {
            SortDirection direction = dir ==SortDirection.Ascending? SortDirection.Descending : SortDirection.Ascending;
            gvProduct.Sort(e.SortExpression, direction);
        }

        protected SortDirection dir
        {
            get
            {
                if (ViewState["dirState"] == null)
                {
                    ViewState["dirState"] = SortDirection.Ascending;
                }
                return (SortDirection)ViewState["dirState"];
            }
            set
            {
                ViewState["dirState"] = value;
            }
        }
    }
}