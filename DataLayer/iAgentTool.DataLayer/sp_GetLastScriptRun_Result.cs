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
    
    public partial class sp_GetLastScriptRun_Result
    {
        public int Id { get; set; }
        public System.Guid TransactionId { get; set; }
        public System.DateTimeOffset RunDate { get; set; }
        public string AgentIdentifier { get; set; }
        public string Notes { get; set; }
        public Nullable<int> PortalUserId { get; set; }
        public Nullable<int> PortalId { get; set; }
    }
}
