using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hetrogiupquyetdinh.Models
{
   
    [MetadataTypeAttribute(typeof(Vien_Metadata))]
    public partial class Vien
    {

    }
    public class Vien_Metadata
    {
        [Display(Name = "Mã viện")]

        public int Id { get; set; }
        [Display(Name = "Tên viện")]
        public string Ten { get; set; }
        [Display(Name = "Tên trường")]

        public Nullable<int> TruongID { get; set; }
    }
}