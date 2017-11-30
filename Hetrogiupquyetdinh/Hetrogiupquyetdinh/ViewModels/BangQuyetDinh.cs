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

        //thêm hai hàng cho chuẩn hóa theo trọng số: phương án lí tưởng tốt và xấu
        public string[,] ChuanHoa_TrongSoMatrix { get; set; }

        //giá trị max của mỗi cột
        public string[,] Matranlituong { get; set; }

    }
}