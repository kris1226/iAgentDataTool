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
    
    public partial class dsa_criteriaDetails
    {
        public Nullable<System.DateTime> dateAdded { get; set; }
        public Nullable<System.DateTime> dateChanged { get; set; }
        public string lastUserID { get; set; }
        public string deviceID { get; set; }
        public System.Guid criteriaSetKey { get; set; }
        public System.Guid criteriaDetailKey { get; set; }
        public Nullable<System.Guid> fieldKey { get; set; }
        public short fieldPosition { get; set; }
        public string @operator { get; set; }
        public string compareValue { get; set; }
        public string lineBooleanOperator { get; set; }
        public int irecordKey { get; set; }
    }
}
