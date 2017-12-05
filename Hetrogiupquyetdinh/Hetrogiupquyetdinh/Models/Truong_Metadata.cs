using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hetrogiupquyetdinh.Models
{
    
    [MetadataTypeAttribute(typeof(Truong_Metadata))]
    public partial class Truong
    {

    }
    public class Truong_Metadata
    {
        [Display(Name = "Mã trường")]

        public int ID { get; set; }
        [Display(Name = "Tên trường")]

        public string Ten { get; set; }
    }
}