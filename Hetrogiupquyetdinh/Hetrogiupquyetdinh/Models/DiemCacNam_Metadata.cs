using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hetrogiupquyetdinh.Models
{
    
    [MetadataTypeAttribute(typeof(DiemCacNam_Metadata))]
    public partial class DiemCacNam
    {

    }
    public class DiemCacNam_Metadata
    {
        [Display(Name = "Mã viện")]
        public int IDVien { get; set; }
        [Display(Name = "Năm 2014")]
        public double Nam2014 { get; set; }
        [Display(Name = "Năm 2015")]
        public double Nam2015 { get; set; }
        [Display(Name = "Năm 2016")]
        public double Nam2016 { get; set; }
        [Display(Name = "Năm 2017")]
        public double Nam2017 { get; set; }
    }
}