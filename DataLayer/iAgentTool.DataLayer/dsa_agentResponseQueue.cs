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
    
    public partial class dsa_agentResponseQueue
    {
        public System.Guid requestTransactionID { get; set; }
        public System.Guid transactionID { get; set; }
        public long agentResponseTimeStamp { get; set; }
        public Nullable<long> agentRequestTimeStamp { get; set; }
        public Nullable<System.Guid> agentSessionToken { get; set; }
        public Nullable<System.Guid> appSessionToken { get; set; }
        public Nullable<System.Guid> appKey { get; set; }
        public Nullable<System.Guid> clientKey { get; set; }
        public Nullable<System.Guid> clientLocKey { get; set; }
        public string rawAgentResponse { get; set; }
        public int currentState { get; set; }
        public string agentUserID { get; set; }
        public Nullable<System.TimeSpan> expectedCompletionTime { get; set; }
        public Nullable<int> expectCompletionConfidence { get; set; }
        public string responsePayloadType { get; set; }
        public string responsePayload { get; set; }
        public string howToDeliver { get; set; }
        public Nullable<bool> noPrompt { get; set; }
        public Nullable<System.DateTime> AddDate { get; set; }
        public string ErrorCode { get; set; }
        public string ErrorDescription { get; set; }
        public string Source { get; set; }
        public Nullable<bool> IsCleared { get; set; }
        public string Notes { get; set; }
        public Nullable<bool> IsPickedUpForDelivery { get; set; }
        public string ScriptRunIds { get; set; }
    }
}
