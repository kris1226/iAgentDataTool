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
    
    public partial class ResponseDeliveryLog
    {
        public int Id { get; set; }
        public string DeliveredPrecerts { get; set; }
        public string RawPayload { get; set; }
        public System.Guid TransactionId { get; set; }
        public Nullable<System.DateTime> CreateDateTime { get; set; }
    }
}
