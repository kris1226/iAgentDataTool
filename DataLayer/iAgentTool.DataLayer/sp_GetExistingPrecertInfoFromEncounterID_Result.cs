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
    
    public partial class sp_GetExistingPrecertInfoFromEncounterID_Result
    {
        public System.Guid TransactionId { get; set; }
        public string ClientRequestId { get; set; }
        public string MemberId { get; set; }
        public string PatientLastname { get; set; }
        public string PatientFirstname { get; set; }
        public string PatientMiddlename { get; set; }
        public Nullable<System.DateTime> PatientDOB { get; set; }
        public Nullable<int> ServiceType { get; set; }
        public string PatientState { get; set; }
        public Nullable<int> SubscriberRelationship { get; set; }
        public string ClientIdentifier { get; set; }
        public string TradingPartnerId { get; set; }
        public string FacilityId { get; set; }
        public string AuthorizationNumber { get; set; }
        public string PayerIdentifier { get; set; }
        public string PlanId { get; set; }
        public string CompanyId { get; set; }
        public string InsuranceCarrierName { get; set; }
        public string CPTcodes { get; set; }
        public string HospitalCode { get; set; }
        public string AlternatePayer { get; set; }
        public Nullable<System.DateTime> DateOfService { get; set; }
        public string InquiryType { get; set; }
        public string ServiceLine { get; set; }
        public Nullable<int> ConsumingApplication { get; set; }
        public string InformationProviderKey { get; set; }
        public string EncounterIdentifier { get; set; }
        public string PatientKey { get; set; }
        public Nullable<int> UseKnowledgebase { get; set; }
        public string PatientSSN { get; set; }
        public string GroupNumber { get; set; }
        public string CaseNumber { get; set; }
        public string OrderingPhysicianNPI { get; set; }
        public Nullable<System.Guid> ClientRequest_Id { get; set; }
        public Nullable<System.DateTime> CreateDateTime { get; set; }
        public string Primary_IPRKey { get; set; }
        public Nullable<int> ScriptFinished { get; set; }
        public System.DateTime RequestTime { get; set; }
    }
}
