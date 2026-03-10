 
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace User_Registration
{

    public class UserRepository
    {

        private readonly string _connStr;
        public UserRepository()
        {
            _connStr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }
        public async Task<List<UserRecord>> GetByStatusAsync(string status)
        {
            var list = new List<UserRecord>();

            using (var conn = new SqlConnection(_connStr))
            using (var cmd = new SqlCommand(@"
        SELECT
            EmpID, EmpName, EmpDesign, EmpProjID, EmpProjName, EmpMailID, EmpPassword,
            EmpSKMailID, EmpSKName, EmpRemarks, EmpStatus, Xpe_Proj_ID, Xpe_Proj_StoreID,
            ProcessStep, ProcessOrder, Reporting, SubReporting, XpeUser, ITSRep, MenuGroup,
            Mobile, FS_Folder, FS_UserLevel, FS_DEPT
        FROM dbo.UserTable
        WHERE EmpStatus = @status
        ORDER BY EmpName;", conn))
            {
                cmd.Parameters.AddWithValue("@status", (object)status ?? DBNull.Value);

                await conn.OpenAsync();
                using (var rdr = await cmd.ExecuteReaderAsync(CommandBehavior.CloseConnection))
                {
                    while (await rdr.ReadAsync())
                    {
                        var r = new UserRecord();
                        r.EmpID = rdr["EmpID"] as string;
                        r.EmpName = rdr["EmpName"] as string;
                        r.EmpDesign = rdr["EmpDesign"] as string;
                        r.EmpProjID = rdr["EmpProjID"] == DBNull.Value ? (int?)null : Convert.ToInt32(rdr["EmpProjID"]);
                        r.EmpProjName = rdr["EmpProjName"] as string;
                        r.EmpMailID = rdr["EmpMailID"] as string;
                        r.EmpPassword = rdr["EmpPassword"] as string;
                        r.EmpSKMailID = rdr["EmpSKMailID"] as string;
                        r.EmpSKName = rdr["EmpSKName"] as string;
                        r.EmpRemarks = rdr["EmpRemarks"] as string;
                        r.EmpStatus = rdr["EmpStatus"] as string;
                        r.Xpe_Proj_ID = rdr["Xpe_Proj_ID"] as string;
                        r.Xpe_Proj_StoreID = rdr["Xpe_Proj_StoreID"] as string;
                        r.ProcessStep = rdr["ProcessStep"] as string;
                        r.ProcessOrder = rdr["ProcessOrder"] == DBNull.Value ? (int?)null : Convert.ToInt32(rdr["ProcessOrder"]);
                        r.Reporting = rdr["Reporting"] as string;
                        r.SubReporting = rdr["SubReporting"] as string;
                        r.XpeUser = rdr["XpeUser"] as string;
                        r.ITSRep = rdr["ITSRep"] as string;
                        r.MenuGroup = rdr["MenuGroup"] as string;
                        r.Mobile = rdr["Mobile"] as string;
                        r.FS_Folder = rdr["FS_Folder"] as string;
                        r.FS_UserLevel = rdr["FS_UserLevel"] as string;
                        r.FS_DEPT = rdr["FS_DEPT"] as string;

                        list.Add(r);
                    }
                }
            }
            return list;
        }
        // Fetch all rows
        public async Task<List<UserRecord>> GetAllAsync()
        {
            var list = new List<UserRecord>();

            using (var conn = new SqlConnection(_connStr))
            using (var cmd = new SqlCommand(@"
                SELECT
                    EmpID, EmpName, EmpDesign, EmpProjID, EmpProjName, EmpMailID, EmpPassword,
                    EmpSKMailID, EmpSKName, EmpRemarks, EmpStatus, Xpe_Proj_ID, Xpe_Proj_StoreID,
                    ProcessStep, ProcessOrder, Reporting, SubReporting, XpeUser, ITSRep, MenuGroup,
                    Mobile, FS_Folder, FS_UserLevel, FS_DEPT
                FROM dbo.UserTable
                ORDER BY EmpName", conn))
            {
                await conn.OpenAsync();
                using (var rdr = await cmd.ExecuteReaderAsync(CommandBehavior.CloseConnection))
                {
                    while (await rdr.ReadAsync())
                    {
                        var r = new UserRecord();
                        r.EmpID = rdr["EmpID"] as string;
                        r.EmpName = rdr["EmpName"] as string;
                        r.EmpDesign = rdr["EmpDesign"] as string;
                        r.EmpProjID = rdr["EmpProjID"] == DBNull.Value ? (int?)null : Convert.ToInt32(rdr["EmpProjID"]);
                        r.EmpProjName = rdr["EmpProjName"] as string;
                        r.EmpMailID = rdr["EmpMailID"] as string;
                        r.EmpPassword = rdr["EmpPassword"] as string;
                        r.EmpSKMailID = rdr["EmpSKMailID"] as string;
                        r.EmpSKName = rdr["EmpSKName"] as string;
                        r.EmpRemarks = rdr["EmpRemarks"] as string;
                        r.EmpStatus = rdr["EmpStatus"] as string;
                        r.Xpe_Proj_ID = rdr["Xpe_Proj_ID"] as string;
                        r.Xpe_Proj_StoreID = rdr["Xpe_Proj_StoreID"] as string;
                        r.ProcessStep = rdr["ProcessStep"] as string;
                        r.ProcessOrder = rdr["ProcessOrder"] == DBNull.Value ? (int?)null : Convert.ToInt32(rdr["ProcessOrder"]);
                        r.Reporting = rdr["Reporting"] as string;
                        r.SubReporting = rdr["SubReporting"] as string;
                        r.XpeUser = rdr["XpeUser"] as string;
                        r.ITSRep = rdr["ITSRep"] as string;
                        r.MenuGroup = rdr["MenuGroup"] as string;
                        r.Mobile = rdr["Mobile"] as string;
                        r.FS_Folder = rdr["FS_Folder"] as string;
                        r.FS_UserLevel = rdr["FS_UserLevel"] as string;
                        r.FS_DEPT = rdr["FS_DEPT"] as string;

                        list.Add(r);
                    }
                }
            }
            return list;
        }

        // Optional: Search by EmpName / EmpID / Mail (parameterized)
        public async Task<List<UserRecord>> SearchAsync(string searchTerm)
        {
            var list = new List<UserRecord>();

            using (var conn = new SqlConnection(_connStr))
            using (var cmd = new SqlCommand(@"
                SELECT
                    EmpID, EmpName, EmpDesign, EmpProjID, EmpProjName, EmpMailID, EmpPassword,
                    EmpSKMailID, EmpSKName, EmpRemarks, EmpStatus, Xpe_Proj_ID, Xpe_Proj_StoreID,
                    ProcessStep, ProcessOrder, Reporting, SubReporting, XpeUser, ITSRep, MenuGroup,
                    Mobile, FS_Folder, FS_UserLevel, FS_DEPT
                FROM dbo.UserTable
                WHERE
                    (@term IS NULL)
                    OR (EmpID LIKE '%' + @term + '%'
                    OR  EmpName LIKE '%' + @term + '%'
                    OR  EmpMailID LIKE '%' + @term + '%')
                ORDER BY EmpName", conn))
            {
                cmd.Parameters.AddWithValue("@term", (object)searchTerm ?? DBNull.Value);

                await conn.OpenAsync();
                using (var rdr = await cmd.ExecuteReaderAsync(CommandBehavior.CloseConnection))
                {
                    while (await rdr.ReadAsync())
                    {
                        var r = new UserRecord();
                        r.EmpID = rdr["EmpID"] as string;
                        r.EmpName = rdr["EmpName"] as string;
                        r.EmpDesign = rdr["EmpDesign"] as string;
                        r.EmpProjID = rdr["EmpProjID"] == DBNull.Value ? (int?)null : Convert.ToInt32(rdr["EmpProjID"]);
                        r.EmpProjName = rdr["EmpProjName"] as string;
                        r.EmpMailID = rdr["EmpMailID"] as string;
                        r.EmpPassword = rdr["EmpPassword"] as string;
                        r.EmpSKMailID = rdr["EmpSKMailID"] as string;
                        r.EmpSKName = rdr["EmpSKName"] as string;
                        r.EmpRemarks = rdr["EmpRemarks"] as string;
                        r.EmpStatus = rdr["EmpStatus"] as string;
                        r.Xpe_Proj_ID = rdr["Xpe_Proj_ID"] as string;
                        r.Xpe_Proj_StoreID = rdr["Xpe_Proj_StoreID"] as string;
                        r.ProcessStep = rdr["ProcessStep"] as string;
                        r.ProcessOrder = rdr["ProcessOrder"] == DBNull.Value ? (int?)null : Convert.ToInt32(rdr["ProcessOrder"]);
                        r.Reporting = rdr["Reporting"] as string;
                        r.SubReporting = rdr["SubReporting"] as string;
                        r.XpeUser = rdr["XpeUser"] as string;
                        r.ITSRep = rdr["ITSRep"] as string;
                        r.MenuGroup = rdr["MenuGroup"] as string;
                        r.Mobile = rdr["Mobile"] as string;
                        r.FS_Folder = rdr["FS_Folder"] as string;
                        r.FS_UserLevel = rdr["FS_UserLevel"] as string;
                        r.FS_DEPT = rdr["FS_DEPT"] as string;

                        list.Add(r);
                    }
                }
            }
            return list;
        }

        // (Optional) Insert/Update/Delete — only if you need them later
        public async Task<int> InsertAsync(UserRecord r)
        {
            using (var conn = new SqlConnection(_connStr))
            using (var cmd = new SqlCommand(@"
                INSERT INTO dbo.UserTable
                (EmpID, EmpName, EmpDesign, EmpProjID, EmpProjName, EmpMailID, EmpPassword,
                 EmpSKMailID, EmpSKName, EmpRemarks, EmpStatus, Xpe_Proj_ID, Xpe_Proj_StoreID,
                 ProcessStep, ProcessOrder, Reporting, SubReporting, XpeUser, ITSRep, MenuGroup,
                 Mobile, FS_Folder, FS_UserLevel, FS_DEPT)
                VALUES
                (@EmpID, @EmpName, @EmpDesign, @EmpProjID, @EmpProjName, @EmpMailID, @EmpPassword,
                 @EmpSKMailID, @EmpSKName, @EmpRemarks, @EmpStatus, @Xpe_Proj_ID, @Xpe_Proj_StoreID,
                 @ProcessStep, @ProcessOrder, @Reporting, @SubReporting, @XpeUser, @ITSRep, @MenuGroup,
                 @Mobile, @FS_Folder, @FS_UserLevel, @FS_DEPT)", conn))
            {
                AddParameters(cmd, r);
                await conn.OpenAsync();
                return await cmd.ExecuteNonQueryAsync();
            }
        }

        public async Task<int> UpdateByEmpIDAsync(UserRecord r)
        {
            using (var conn = new SqlConnection(_connStr))
            using (var cmd = new SqlCommand(@"
                UPDATE dbo.UserTable
                SET EmpName=@EmpName, EmpDesign=@EmpDesign, EmpProjID=@EmpProjID, EmpProjName=@EmpProjName,
                    EmpMailID=@EmpMailID, EmpPassword=@EmpPassword, EmpSKMailID=@EmpSKMailID, EmpSKName=@EmpSKName,
                    EmpRemarks=@EmpRemarks, EmpStatus=@EmpStatus, Xpe_Proj_ID=@Xpe_Proj_ID, Xpe_Proj_StoreID=@Xpe_Proj_StoreID,
                    ProcessStep=@ProcessStep, ProcessOrder=@ProcessOrder, Reporting=@Reporting, SubReporting=@SubReporting,
                    XpeUser=@XpeUser, ITSRep=@ITSRep, MenuGroup=@MenuGroup, Mobile=@Mobile, FS_Folder=@FS_Folder,
                    FS_UserLevel=@FS_UserLevel, FS_DEPT=@FS_DEPT
                WHERE EmpID=@EmpID", conn))
            {
                AddParameters(cmd, r);
                await conn.OpenAsync();
                return await cmd.ExecuteNonQueryAsync();
            }
        }

        public async Task<int> DeleteByEmpIDAsync(string empId)
        {
            using (var conn = new SqlConnection(_connStr))
            using (var cmd = new SqlCommand("DELETE FROM dbo.UserTable WHERE EmpID=@EmpID", conn))
            {
                cmd.Parameters.AddWithValue("@EmpID", (object)empId ?? DBNull.Value);
                await conn.OpenAsync();
                return await cmd.ExecuteNonQueryAsync();
            }
        }

        private static void AddParameters(SqlCommand cmd, UserRecord r)
        {
            cmd.Parameters.AddWithValue("@EmpID", (object)r.EmpID ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@EmpName", (object)r.EmpName ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@EmpDesign", (object)r.EmpDesign ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@EmpProjID", (object)r.EmpProjID ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@EmpProjName", (object)r.EmpProjName ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@EmpMailID", (object)r.EmpMailID ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@EmpPassword", (object)r.EmpPassword ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@EmpSKMailID", (object)r.EmpSKMailID ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@EmpSKName", (object)r.EmpSKName ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@EmpRemarks", (object)r.EmpRemarks ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@EmpStatus", (object)r.EmpStatus ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Xpe_Proj_ID", (object)r.Xpe_Proj_ID ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Xpe_Proj_StoreID", (object)r.Xpe_Proj_StoreID ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@ProcessStep", (object)r.ProcessStep ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@ProcessOrder", (object)r.ProcessOrder ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Reporting", (object)r.Reporting ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@SubReporting", (object)r.SubReporting ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@XpeUser", (object)r.XpeUser ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@ITSRep", (object)r.ITSRep ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@MenuGroup", (object)r.MenuGroup ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Mobile", (object)r.Mobile ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@FS_Folder", (object)r.FS_Folder ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@FS_UserLevel", (object)r.FS_UserLevel ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@FS_DEPT", (object)r.FS_DEPT ?? DBNull.Value);
        }


        public async Task<List<ProcessItem>> GetProcessesAsync()
        {
            var list = new List<ProcessItem>();

            using (var conn = new SqlConnection(_connStr))
            using (var cmd = new SqlCommand(@"
        SELECT Code, Name
        FROM dbo.Process_MasterTbl
          
        ORDER BY Name;", conn))
            {
                await conn.OpenAsync();
                using (var rdr = await cmd.ExecuteReaderAsync())
                {
                    while (await rdr.ReadAsync())
                    {
                        list.Add(new ProcessItem
                        {
                            // Code type: nvarchar/varchar ആയാൽ
                            Code = rdr.IsDBNull(0) ? "" : rdr.GetString(0),
                            Name = rdr.IsDBNull(1) ? "" : rdr.GetString(1)

                            // Code INT ആണെങ്കിൽ:
                            // Code = rdr.GetInt32(0).ToString()
                        });
                    }
                }
            }
            return list;
        }
        public async Task<List<ITSRep>> GetITSRepAsync()
        {
            var list = new List<ITSRep>();

            using (var conn = new SqlConnection(_connStr))
            using (var cmd = new SqlCommand(@"
        SELECT Code, Name
        FROM dbo.ITSRep_MasterTbl
     
        ORDER BY Name;", conn))
            {
                await conn.OpenAsync();
                using (var rdr = await cmd.ExecuteReaderAsync())
                {
                    while (await rdr.ReadAsync())
                    {
                        list.Add(new ITSRep
                        {
                            // Code type: nvarchar/varchar ആയാൽ
                            Code = rdr.IsDBNull(0) ? "" : rdr.GetString(0),
                            Name = rdr.IsDBNull(1) ? "" : rdr.GetString(1)

                            // Code INT ആണെങ്കിൽ:
                            // Code = rdr.GetInt32(0).ToString()
                        });
                    }
                }
            }
            return list;
        }
        public async Task<List<BranchItem>> GetBranchesAsync()
           {
            var list = new List<BranchItem>();

            using (var conn = new SqlConnection(_connStr))
            using (var cmd = new SqlCommand(@"
            SELECT BRANCH_ID, PRIMARY_NAME 
            FROM dbo.Branches_Tbl
            ORDER BY PRIMARY_NAME;", conn))
            {
                await conn.OpenAsync();
                using (var rdr = await cmd.ExecuteReaderAsync())
                {
                    while (await rdr.ReadAsync())
                    {
                        list.Add(new BranchItem
                        {
                            BRANCH_ID = (int)rdr.GetDecimal(0),
                            //BRANCH_ID = rdr.GetInt32(0),
                            PRIMARY_NAME = rdr.IsDBNull(1) ? "" : rdr.GetString(1)
                           
                        });
                    }
                }
            }
            return list;
        }
    }

}
 