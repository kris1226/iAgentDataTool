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
    
    public partial class ExtractionMap
    {
        public int ExtractionMapId { get; set; }
        public int PayerResponseId { get; set; }
        public string LocationType { get; set; }
        public string Location { get; set; }
        public string DataItem { get; set; }
        public string PageType { get; set; }
        public string NormalizeFunction { get; set; }
        public string Param1 { get; set; }
        public string Param2 { get; set; }
        public string Param3 { get; set; }
        public string AttributeName { get; set; }
    
        public virtual PayerResponseMap PayerResponseMap { get; set; }
    }
}