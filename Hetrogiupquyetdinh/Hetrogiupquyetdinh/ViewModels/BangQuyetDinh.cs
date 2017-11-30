using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hetrogiupquyetdinh.ViewModels
{
    public class BangQuyetDinh
    {
        public string[,] Matrix { get; set; }
        public string[,] ChuanHoaMatrix { get; set; }
        public string[,] ChuanHoa_TrongSoMatrix { get; set; }
    }
}