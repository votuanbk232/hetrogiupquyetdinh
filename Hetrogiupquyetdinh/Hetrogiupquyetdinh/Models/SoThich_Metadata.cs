using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hetrogiupquyetdinh.Models
{
   
    [MetadataTypeAttribute(typeof(SoThich_Metadata))]
    public partial class SoThich
    {

    }
    public class SoThich_Metadata
    {
        [Display(Name = "Mã sở thích")]
        public int Id { get; set; }
        [Display(Name = "Tên sở thích")]

        public string Ten { get; set; }
    }
}