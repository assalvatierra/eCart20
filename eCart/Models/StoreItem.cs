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
    
    public partial class StoreItem
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public StoreItem()
        {
            this.CartItems = new HashSet<CartItem>();
        }
    
        public int Id { get; set; }
        public int ItemMasterId { get; set; }
        public int StoreDetailId { get; set; }
        public decimal UnitPrice { get; set; }
    
        public virtual ItemMaster ItemMaster { get; set; }
        public virtual StoreDetail StoreDetail { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CartItem> CartItems { get; set; }
    }
}
