//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace iAgentTool.DataLayer
{
    using System;
    using System.Collections.Generic;
    
    public partial class dsa_scriptMaster
    {
        public Nullable<System.DateTime> dateAdded { get; set; }
        public Nullable<System.DateTime> dateChanged { get; set; }
        public string lastUserID { get; set; }
        public string deviceID { get; set; }
        public System.Guid scriptKey { get; set; }
        public short noRetries { get; set; }
        public int delayBefore { get; set; }
        public int delayAfter { get; set; }
        public int timeout { get; set; }
        public string scriptDesc { get; set; }
        public string scriptCode { get; set; }
        public Nullable<System.Guid> websiteKey { get; set; }
        public Nullable<bool> iterative { get; set; }
        public string setAgentAs { get; set; }
        public int irecordKey { get; set; }
        public Nullable<int> noIterations { get; set; }
        public Nullable<int> tableRow { get; set; }
        public Nullable<int> tableColumn { get; set; }
        public string Category { get; set; }
        public Nullable<int> Priority { get; set; }
    }
}
