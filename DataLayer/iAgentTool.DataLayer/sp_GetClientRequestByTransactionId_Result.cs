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
    
    public partial class sp_GetClientRequestByTransactionId_Result
    {
        public System.Guid Id { get; set; }
        public string ClientRequestId { get; set; }
        public string Username { get; set; }
        public System.DateTime RequestTime { get; set; }
        public string ClientId { get; set; }
        public string TradingPartnerId { get; set; }
        public string FacilityId { get; set; }
        public Nullable<System.Guid> TransactionId { get; set; }
        public string RequestData { get; set; }
        public Nullable<int> RequestType { get; set; }
        public string IPRKey { get; set; }
        public string ResponseXML { get; set; }
        public string Primary_IPRKey { get; set; }
        public Nullable<System.DateTime> NextRun { get; set; }
    }
}