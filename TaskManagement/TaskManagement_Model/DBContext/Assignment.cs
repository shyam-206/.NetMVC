//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TaskManagement_Model.DBContext
{
    using System;
    using System.Collections.Generic;
    
    public partial class Assignment
    {
        public int AssignmentID { get; set; }
        public Nullable<int> TaskID { get; set; }
        public Nullable<int> StudentID { get; set; }
        public Nullable<bool> Status { get; set; }
    
        public virtual Student Student { get; set; }
        public virtual Task Task { get; set; }

        
    }
}
