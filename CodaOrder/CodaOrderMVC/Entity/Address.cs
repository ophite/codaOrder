//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace iOrder.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class Address : BaseEntity
    {
        public Address()
        {
            this.IconIndex = -1;
        }
    
        public long OID { get; set; }
        public Nullable<int> CID { get; set; }
        public string CODE { get; set; }
        public string Comments { get; set; }
        public string ExtraName { get; set; }
        public string FlatNO { get; set; }
        public string FullName { get; set; }
        public System.Guid GUID { get; set; }
        public string HouseNO { get; set; }
        public int IconIndex { get; set; }
        public Nullable<long> Location { get; set; }
        public string LOCCODE { get; set; }
        public string Name { get; set; }
        public Nullable<long> ParentID { get; set; }
        public string PostalCode { get; set; }
        public Nullable<long> SettlementID { get; set; }
        public Nullable<long> StreetID { get; set; }
        public byte[] TST { get; set; }
    }
}
