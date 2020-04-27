//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace eCart.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class CartDelivery
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CartDelivery()
        {
            this.CartDeliveryActivities = new HashSet<CartActivity>();
        }
    
        public int Id { get; set; }
        public int CartDetailId { get; set; }
        public System.DateTime dtDelivery { get; set; }
        public string Address { get; set; }
        public string Remarks { get; set; }
        public int RiderDetailId { get; set; }
    
        public virtual CartDetail CartDetail { get; set; }
        public virtual RiderDetail RiderDetail { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CartActivity> CartDeliveryActivities { get; set; }
    }
}
