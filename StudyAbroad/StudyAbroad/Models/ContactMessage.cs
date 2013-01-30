using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Resources;
using System.ComponentModel;
using StudyAbroad.Common;

namespace StudyAbroad.Models
{
    [Table("ContactMessages")]
    public class ContactMessage
    {
        [Key]
        public int MessageID { get; set; }
        public DateTime AddedDate { get; set; }
        public string AddedByIP { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationStrings), ErrorMessageResourceName = "Required_Validation")]
        [LocalizedDisplayName("Label_Contact_Name")]
        [StringLength(30, ErrorMessageResourceType = typeof(ValidationStrings), ErrorMessageResourceName = "Length_Validation")]
        public string AddedBy { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationStrings), ErrorMessageResourceName = "Required_Validation")]
        [LocalizedDisplayName("Label_Contact_Email")]
        [RegularExpression("^[a-z0-9_\\+-]+(\\.[a-z0-9_\\+-]+)*@[a-z0-9-]+(\\.[a-z0-9-]+)*\\.([a-z]{2,4})$",
            ErrorMessageResourceType = typeof(ValidationStrings), ErrorMessageResourceName = "Email_Validation")]
        public string AddedByEmail { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationStrings), ErrorMessageResourceName = "Required_Validation")]
        [LocalizedDisplayName("Label_Contact_Message")]
        [StringLength(1024, ErrorMessageResourceType = typeof(ValidationStrings), ErrorMessageResourceName = "Length_Validation")]
        public string Body { get; set; }
    }
}