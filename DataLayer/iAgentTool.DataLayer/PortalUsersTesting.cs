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
    
    public partial class PortalUsersTesting
    {
        public int Id { get; set; }
        public Nullable<int> Portal_Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public Nullable<bool> IsExpired { get; set; }
        public Nullable<System.DateTime> DateExpired { get; set; }
        public Nullable<bool> IsEnabled { get; set; }
    }
}
