using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hetrogiupquyetdinh.Models
{
   
    [MetadataTypeAttribute(typeof(Vien_SoThich_Metadata))]
    public partial class Vien_SoThich
    {

    }
    public class Vien_SoThich_Metadata
    {
        [Display(Name = "Mã viện")]
        public int IdVien { get; set; }
        [Display(Name = "Mã sở thích")]

        public int IdSoThich { get; set; }
        [Display(Name = "Tên trường")]
        public int Diem { get; set; }
    }
}