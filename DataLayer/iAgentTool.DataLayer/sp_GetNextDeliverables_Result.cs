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
    
    public partial class sp_GetNextDeliverables_Result
    {
        public System.Guid RequestTransactionId { get; set; }
        public string rawAgentResponse { get; set; }
        public string responsePayload { get; set; }
        public string HowToDeliver { get; set; }
        public int CurrentState { get; set; }
    }
}