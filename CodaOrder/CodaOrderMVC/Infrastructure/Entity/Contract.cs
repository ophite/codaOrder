//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace iOrder.Infrastructure.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class Contract : BaseEntity
    {
        public Contract()
        {
            this.IconIndex = -1;
        }
    
        public long OID { get; set; }
        public Nullable<int> CID { get; set; }
        public string Comments { get; set; }
        public Nullable<long> ConditionID { get; set; }
        public Nullable<long> ContractKindID { get; set; }
        public string ContractNO { get; set; }
        public Nullable<long> ContractTypeID { get; set; }
        public Nullable<decimal> DebtLimit { get; set; }
        public Nullable<long> DelayLimit { get; set; }
        public Nullable<System.DateTime> DelDate { get; set; }
        public Nullable<System.DateTime> ExpireDate { get; set; }
        public string ExtraContractNO { get; set; }
        public Nullable<System.DateTime> ExtraIssueDate { get; set; }
        public Nullable<long> FilialID { get; set; }
        public long FirmID { get; set; }
        public string FullName { get; set; }
        public Nullable<decimal> GrowthValue { get; set; }
        public System.Guid GUID { get; set; }
        public int IconIndex { get; set; }
        public Nullable<bool> IsArchived { get; set; }
        public Nullable<bool> IsBlocked { get; set; }
        public bool IsDeleted { get; set; }
        public Nullable<bool> IsFixed { get; set; }
        public Nullable<System.DateTime> IssueDate { get; set; }
        public Nullable<decimal> ManualDiscount { get; set; }
        public string Name { get; set; }
        public Nullable<long> OwnerID { get; set; }
        public Nullable<long> ParentID { get; set; }
        public Nullable<long> SID { get; set; }
        public long SubjectID { get; set; }
        public byte[] TST { get; set; }
    }
}