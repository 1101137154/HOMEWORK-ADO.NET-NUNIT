using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication1.Tests
{
    /*ＮUNIT RESULT
    WebApplication1.Tests.Class1 (TestFixtureSetUp):
    SetUp : System.Data.SqlClient.SqlException : 建立連接至 SQL Server 時，發生網路相關或執行個體特定的錯誤。找不到或無法存取伺服器。確認執行個名稱是否正確，以及 SQL Server 是否設定為允許遠端連線。 (provider: Named Pipes Provider, error: 40 - 無法開啟至 SQL Server 的連接)
    ----> System.ComponentModel.Win32Exception : 找不到網路路徑。
    */
    [TestFixture]
    public class Class1
    {
        string ID = "";

        [TestFixtureSetUp]
        public void Initial()
        {
            string sql = @"INSERT INTO [dbo].[Department]([Name],[Location])
                             VALUES
                               (@Name
                               ,@Location);SELECT CAST(scope_identity() AS int);";
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["ADODBConnectionString"].ConnectionString))
            {
                cn.Open();
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    cmd.Parameters.Add(new SqlParameter("@Name", "MIS"));
                    cmd.Parameters.Add(new SqlParameter("@Location", "Taipei"));

                    ID = cmd.ExecuteScalar().ToString();

                    Console.WriteLine(ID);
                }
            }
        }

        [Test]
        public void TestAdd()
        {
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["ADODBConnectionString"].ConnectionString))
            {
                cn.Open();
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cmd.CommandText = "select Max(DepartmentID) from Department";
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while ((dr.Read()))
                        {
                            Assert.AreEqual(ID, dr.GetSqlInt32(0).ToString());
                            break;
                        }
                        dr.Close();
                    }
                }
            }
        }
    }
}
