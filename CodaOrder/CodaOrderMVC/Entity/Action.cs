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
    
    public partial class Action : BaseEntity
    {
        public Action()
        {
            this.IconIndex = -1;
        }
    
        public long OID { get; set; }
        public Nullable<int> CID { get; set; }
        public string ActionMethod { get; set; }
        public string ActionName { get; set; }
        public long ActionTypeID { get; set; }
        public string Caption { get; set; }
        public string Comments { get; set; }
        public Nullable<System.DateTime> DelDate { get; set; }
        public string DotNetMethod { get; set; }
        public string FullName { get; set; }
        public System.Guid GUID { get; set; }
        public int IconIndex { get; set; }
        public bool IsAuto { get; set; }
        public bool IsCardItem { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsDocumentChanged { get; set; }
        public bool IsGroup { get; set; }
        public bool IsHidden { get; set; }
        public bool IsListItem { get; set; }
        public bool IsManual { get; set; }
        public bool IsReset { get; set; }
        public bool IsRowItem { get; set; }
        public bool IsToolbarItem { get; set; }
        public string JournalClasses { get; set; }
        public string Name { get; set; }
        public Nullable<long> OwnerID { get; set; }
        public Nullable<long> ParentID { get; set; }
        public string PrepareAction { get; set; }
        public string Shortcut { get; set; }
        public Nullable<long> SID { get; set; }
        public Nullable<int> SortNO { get; set; }
        public string SQLMethod { get; set; }
        public Nullable<long> TargetStatusID { get; set; }
        public string ToolTip { get; set; }
        public byte[] TST { get; set; }
    }
}
