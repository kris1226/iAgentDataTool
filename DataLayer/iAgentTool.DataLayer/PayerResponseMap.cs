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
    
    public partial class PayerResponseMap
    {
        public PayerResponseMap()
        {
            this.ExtractionMaps = new HashSet<ExtractionMap>();
        }
    
        public int PayerResponseId { get; set; }
        public string IPRKey { get; set; }
        public string LookupText { get; set; }
        public string ResponseType { get; set; }
        public string HtmlResponseType { get; set; }
    
        public virtual ICollection<ExtractionMap> ExtractionMaps { get; set; }
    }
}
