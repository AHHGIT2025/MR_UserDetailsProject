using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User_Registration
{ 
    public class UserRecord
    {
        public string EmpID { get; set; }
        public string EmpName { get; set; }
        public string EmpDesign { get; set; }
        public int? EmpProjID { get; set; }
        public string EmpProjName { get; set; }
        public string EmpMailID { get; set; }
        public string EmpPassword { get; set; }// consider hashing
        public string EmpSKMailID { get; set; }
        public string EmpSKName { get; set; }
        public string EmpRemarks { get; set; }
        public string EmpStatus { get; set; }
        public string Xpe_Proj_ID { get; set; }
        public string Xpe_Proj_StoreID { get; set; }
        public string ProcessStep { get; set; }
        public int? ProcessOrder { get; set; }
        public string Reporting { get; set; }
        public string SubReporting { get; set; }
        public string XpeUser { get; set; }

        public string ITSRep { get; set; }
        public string MenuGroup { get; set; }
        public string Mobile { get; set; }
        public string FS_Folder { get; set; }

        public string FS_UserLevel { get; set; }
        public string FS_DEPT { get; set; }
        

    }

    public class BranchItem
    {
        public int BRANCH_ID { get; set; }
        public string PRIMARY_NAME { get; set; }
        public string USER_CODE { get; set; } // if you need to show later
    }

    public class ProcessItem
    {
        public string Code { get; set; }   // varchar/nvarchar ആയാൽ string. INT ആണെങ്കിൽ int.
        public string Name { get; set; }
    }
    public class ITSRep  
    {
        public string Code { get; set; }   
        public string Name { get; set; }
    }

    

}
