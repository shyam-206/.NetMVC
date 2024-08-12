//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ShyamDhokiya_557_Model.DbContext
{
    using System;
    using System.Collections.Generic;
    
    public partial class Bike
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Bike()
        {
            this.Favourites = new HashSet<Favourites>();
        }
    
        public int BikeId { get; set; }
        public int SellerId { get; set; }
        public string Brand { get; set; }
        public string Models { get; set; }
        public int Years { get; set; }
        public int kilometresDriven { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public Nullable<bool> IsDelete { get; set; }
    
        public virtual Seller Seller { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Favourites> Favourites { get; set; }
    }
}
