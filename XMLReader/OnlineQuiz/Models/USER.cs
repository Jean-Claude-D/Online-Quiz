//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OnlineQuiz.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    public partial class USER
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public USER()
        {
            this.QUESTIONs = new HashSet<QUESTION>();
            this.USER_ANSWER = new HashSet<USER_ANSWER>();
        }

        [DisplayName("Username")]
        public string UNAME { get; set; }
        [DisplayName("Password")]
        public string PASSW_HASH { get; set; }
        public string SALT { get; set; }
        [DisplayName("First Name")]
        public string FNAME { get; set; }
        [DisplayName("Last Name")]
        public string LNAME { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<QUESTION> QUESTIONs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<USER_ANSWER> USER_ANSWER { get; set; }
    }
}
