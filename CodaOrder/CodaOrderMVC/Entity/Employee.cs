//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplication3.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class Employee : BaseEntity
    {
        public Employee()
        {
            this.IconIndex = -1;
        }
    
        public long OID { get; set; }
        public Nullable<int> CID { get; set; }
        public Nullable<long> CodaUserID { get; set; }
        public string Comments { get; set; }
        public Nullable<System.DateTime> DelDate { get; set; }
        public Nullable<long> DutyTypeID { get; set; }
        public string EMail { get; set; }
        public string FullName { get; set; }
        public System.Guid GUID { get; set; }
        public int IconIndex { get; set; }
        public Nullable<long> IndividualID { get; set; }
        public bool IsDeleted { get; set; }
        public string MedicalPaper { get; set; }
        public Nullable<System.DateTime> MedPaperDateBegin { get; set; }
        public Nullable<System.DateTime> MedPaperDateEnd { get; set; }
        public string Name { get; set; }
        public Nullable<long> OwnerID { get; set; }
        public Nullable<long> ParentID { get; set; }
        public string Phone { get; set; }
        public string Position { get; set; }
        public Nullable<long> SellerID { get; set; }
        public Nullable<long> SID { get; set; }
        public byte[] TST { get; set; }
    }
}
