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
    
    public partial class dsa_currentActiveAgents
    {
        public string activeUserID { get; set; }
        public System.Guid activeSessionToken { get; set; }
        public long dateAdded { get; set; }
        public string activeDeviceID { get; set; }
        public int recordKey { get; set; }
        public System.Guid activeClientLocKey { get; set; }
        public System.Guid activeClientKey { get; set; }
        public long lastInteraction { get; set; }
        public string assignedAgentID { get; set; }
    }
}