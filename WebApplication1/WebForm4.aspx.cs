using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    /// <summary>
    /// ExecuteReader, ExecuteNonQuery, ExecuteScalar
    /// </summary>
    public partial class WebForm4 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            btnSearch_Click(null, null);
        }

        /// <summary>
        /// ExecuteReader
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["ADODBConnectionString"].ConnectionString))
            {
                cn.Open();
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cmd.CommandText = "select * from department where Name like @name";
                    cmd.Parameters.Add(new SqlParameter("@name", "%" + txtSearch.Text + "%"));
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        DataTable dt = new DataTable();
                        dt.Load(dr);
                        gvResult.DataSource = dt;
                        gvResult.DataBind();
                        dr.Close();
                    }
                }
            }
        }

        //ExecuteScalar
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string sql = @"INSERT INTO [dbo].[department]([Name],[Location],[ManagerID],[DepartmentID])
                             VALUES
                               (@Name
                               ,@Location
                               ,@ManagerID
                               ,@DepartmentID);SELECT CAST(scope_identity() AS int);";
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["ADODBConnectionString"].ConnectionString))
            {
                cn.Open();
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    cmd.Parameters.Add(new SqlParameter("@Name", txtName.Text));
                    cmd.Parameters.Add(new SqlParameter("@Location", txtAge.Text));
                    cmd.Parameters.Add(new SqlParameter("@ManagerID", txtMan.Text));
                    cmd.Parameters.Add(new SqlParameter("@DepartmentID", txtID.Text));

                    txtID.Text = cmd.ExecuteScalar().ToString();
                }

                //reset
                txtName.Text = "";
                txtAge.Text = "";
                txtMan.Text = "";
                txtID.Text = "";

                btnSearch_Click(null, null);
            }
        }

        /// <summary>
        /// ExecuteNonQuery
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            string sql = @"update [department] set [Name] = @Name, [Location] = @Location, [ManagerID] = @ManagerID
                             where DepartmentID = @DepartmentID";
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["ADODBConnectionString"].ConnectionString))
            {
                cn.Open();
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    cmd.Parameters.Add(new SqlParameter("@Name", txtE_Name.Text));
                    cmd.Parameters.Add(new SqlParameter("@Location", txtE_Age.Text));
                    cmd.Parameters.Add(new SqlParameter("@ManagerID", txtE_Man.Text));
                    cmd.Parameters.Add(new SqlParameter("@DepartmentID", txtE_ID.Text));

                    cmd.ExecuteNonQuery();
                }

                //reset
                txtE_Name.Text = "";
                txtE_Age.Text = "";
                txtE_Man.Text = "";
                txtE_ID.Text = "";

                btnSearch_Click(null, null);
            }
        }


        protected void btnDelete_Click(object sender, EventArgs e)
        {
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["ADODBConnectionString"].ConnectionString))
            {
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cmd.CommandText = "select * from department";

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        //自動產生Insert, Update, DeleteCommand
                        SqlCommandBuilder builder = new SqlCommandBuilder(da);
                        //builder.GetInsertCommand()
                        //builder.GetUpdateCommand()
                        //builder.GetDeleteCommand()

                        DataSet ds = new DataSet();
                        ds.Clear();
                        da.Fill(ds);
                        DataTable dt = ds.Tables[0];
                        DataRow dr = dt.Select(string.Format("DepartmentID = {0}", txtD_ID.Text)).First();
                        dr.Delete();
                        da.Update(dt);

                        btnSearch_Click(null, null);
                    }
                }
            }
        }
    }
}