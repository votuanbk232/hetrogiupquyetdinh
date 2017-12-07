using Hetrogiupquyetdinh.Models;
using Hetrogiupquyetdinh.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hetrogiupquyetdinh.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //Hiển thị Sở thích và điểm số của thí sinh
            List<SoThich> sothichs = new List<SoThich>();
            List<Truong> truongs = new List<Truong>();
            List<Khoi> khois = new List<Khoi>();
            using (HeTroGiupQuyetDinh1Entities db=new HeTroGiupQuyetDinh1Entities())
            {
                sothichs = db.SoThiches.OrderBy(x => x.Ten).ToList();
                truongs = db.Truongs.OrderBy(x => x.ID).ToList();
                khois = db.Khois.OrderBy(x => x.ID).ToList();
                ViewBag.SoThich = new SelectList (sothichs,"Id","Ten");
                ViewBag.Truong = new SelectList (truongs,"ID","Ten");
                ViewBag.Khoi = new SelectList (khois,"ID","Ten");

                return View();
            }
            
        }

        public ActionResult Predict(int Id,string Diem,string truongID,string khoiID)
        {
            

            //if (Id.ToString()==null || Id.ToString()==null)
            //{
            //    ModelState.AddModelError("SoThichValidate","Vui lòng nhập sở thích!");
            //    return View("Index");
            //}
            //if (String.IsNullOrEmpty(Diem))
            //{
            //    ModelState.AddModelError("DiemValidate", "Vui lòng nhập điểm!");
            //    return View("Index");
            //}

            //lấy đc Id sở thích và điểm
            //double test = Convert.ToDouble("-3.5");
            //từ Id lấy sở thích
            using (HeTroGiupQuyetDinh1Entities db=new HeTroGiupQuyetDinh1Entities())
            {
                SoThich st = db.SoThiches.SingleOrDefault(x => x.Id == Id);
                //string t = GetTruongIDByTen(Truong);
                //List<Vien> viens = db.Viens.Where(x => x.TruongID == t.ID).ToList();
                //lấy số dòng của ma trận chính là số lượng viện trong bảng DiemCacNam
                int dong;
                //số cột chính là số thuộc tính: sở thích và chênh lệch 4 năm từ 2014->2017
                //int cot = 6; //ví dụ 5 cột thuộc tính và 1 cột phương án
                int cot = 6; //thử nghiệm
                //List<DiemCacNam> diemcacnams = db.DiemCacNams.Where(x=>x.Vien.TruongID.ToString()==truongID)
                //    .ToList();


                //List<DiemCacNam> diemcacnams = (from a in db.DiemCacNams
                //                                join b in db.Viens on a.IDVien equals b.Id
                //                                where a.Vien.TruongID.Equals(truongID) 
                //                                where b.Khoi.ID.Equals(khoiID)
                //                                select a).ToList();

                string diem= "SELECT  * FROM dbo.DiemCacNam AS d,dbo.Vien AS v WHERE d.IDVien = v.Id AND v.TruongID = "+truongID+" AND v.KhoiID = "+khoiID;
                List<DiemCacNam> diemcacnams = db.DiemCacNams.SqlQuery(diem).ToList();

                dong = diemcacnams.Count();

                //ma trận quyết định
                //string[,] matrix = new string[dong+1, cot];

                //thêm 2 dòng: giá trị cao nhất và trọng số
                string[,] matrix = new string[dong + 3, cot];

                //fix hàng đầu tiên của ma trận:
                matrix[0, 0] = "Viện";
                matrix[0, 1] = st.Ten;
                matrix[0, 2] = "Chênh lệch năm 2014";
                matrix[0, 3] = "Chênh lệch năm 2015";
                matrix[0, 4] = "Chênh lệch năm 2016";
                matrix[0, 5] = "Chênh lệch năm 2017";

                matrix[dong + 1, 0] = "Giá trị cao nhất";
                matrix[dong + 2, 0] = "Trọng số";

                //cột viện: cột =0
                for (int i= 0;i < dong; i++){
                    //lấy tên viện dựa vào ID của Viện
                    matrix[i+1,0] = GetTenVienById(diemcacnams[i].IDVien);

                    //cột 2
                    //từ sở thích và viện lấy ra đc điểm của sở thích đó theo viện( từ bảng viện_sở thích)
                    //Vien_SoThich v = (from a in db.Vien_SoThich
                    //                  join b in db.Viens on a.IdVien equals b.Id
                    //                  join c in db.SoThiches on a.IdSoThich equals c.Id
                    //                  where a.IdSoThich.Equals(st.Id) && a.IdVien.Equals(diemcacnams[i].IDVien)
                    //                  select a
                    //                  ).FirstOrDefault();
                    string sql = "Select * from dbo.Vien_SoThich where IdVien=" + diemcacnams[i].IDVien + " and IdSoThich=" + st.Id;
                    Vien_SoThich v = db.Vien_SoThich.SqlQuery(sql).SingleOrDefault();
                    if (v == null)
                    {
                        matrix[i + 1, 1] = "5";

                    }
                    else
                    {
                        matrix[i + 1, 1] = v.Diem.ToString();

                    }

                    //cột 3,4,5,6: chênh lệch năm 2014,15,16,17
                    //từ ID Viện lấy ra đc điểm năm 2014... của Viện đó==> độ chênh lệch

                    //mục tiêu là chênh lệch phải ít nhất lớn hơn điểm các năm
                    //matrix[i + 1, 2] = (Convert.ToDouble(Diem)-GetDiemByIdVien(diemcacnams[i].IDVien, 2014)).ToString();
                    //matrix[i + 1, 3] = (Convert.ToDouble(Diem)-GetDiemByIdVien(diemcacnams[i].IDVien, 2015)).ToString();
                    //matrix[i + 1, 4] = (Convert.ToDouble(Diem)-GetDiemByIdVien(diemcacnams[i].IDVien, 2016)).ToString();
                    //matrix[i + 1, 5] = (Convert.ToDouble(Diem)-GetDiemByIdVien(diemcacnams[i].IDVien, 2017)).ToString();

                    matrix[i + 1, 2] = ChuanHoaChenhLechDiem(Convert.ToDouble(Diem), GetDiemByIdVien(diemcacnams[i].IDVien, 2014)).ToString("0.####");
                    matrix[i + 1, 3] = ChuanHoaChenhLechDiem(Convert.ToDouble(Diem), GetDiemByIdVien(diemcacnams[i].IDVien, 2015)).ToString("0.####");
                    matrix[i + 1, 4] = ChuanHoaChenhLechDiem(Convert.ToDouble(Diem), GetDiemByIdVien(diemcacnams[i].IDVien, 2016)).ToString("0.####");
                    matrix[i + 1, 5] = ChuanHoaChenhLechDiem(Convert.ToDouble(Diem), GetDiemByIdVien(diemcacnams[i].IDVien, 2017)).ToString("0.####");




                }

                //thêm hàng giá trị cao nhất: Giá trị max mỗi cột
                matrix[dong + 1, 1] = GetMaxAColumn(matrix, 1);
                matrix[dong + 1, 2] = GetMaxAColumn(matrix, 2);
                matrix[dong + 1, 3] = GetMaxAColumn(matrix, 3);
                matrix[dong + 1, 4] = GetMaxAColumn(matrix, 4);
                matrix[dong + 1, 5] = GetMaxAColumn(matrix, 5);


                //thêm hàng trọng số
                matrix[dong + 2, 1] = 0.2.ToString(); //sở thích
                matrix[dong + 2, 2] = 0.15.ToString(); //năm 2014
                matrix[dong + 2, 3] = 0.15.ToString(); //năm 2015
                matrix[dong + 2, 4] = 0.2.ToString(); //năm 2016
                matrix[dong + 2, 5] = 0.3.ToString(); //năm 2017

                BangQuyetDinh bangquyetdinh = new BangQuyetDinh();
                bangquyetdinh.Matrix = matrix;

                bangquyetdinh.ChuanHoaMatrix = ChuanHoa(matrix);
                bangquyetdinh.ChuanHoa_TrongSoMatrix = ChuanHoa_TrongSo(ChuanHoa(matrix));
                
                int dongchuanhoa = bangquyetdinh.ChuanHoa_TrongSoMatrix.GetLength(0); //số hàng: với 2 ngành là 5
                int cotchuanhoa = bangquyetdinh.ChuanHoa_TrongSoMatrix.GetLength(1); //số cột là 6: có 2 ngành
                //từ bảng chuanhoa(matrix), thêm 2 dòng cho 2 phương án lí tưởng
                //getleng là 5, => hàng thứ 4

                //chú ý: mức độ quan trọng âm=> sẽ cộng 2 lần cột đó, khi tính S*

                //lí tưởng tốt
                bangquyetdinh.ChuanHoa_TrongSoMatrix[matrix.GetLength(0)-2, 1] = GetMaxAColumn(bangquyetdinh.ChuanHoa_TrongSoMatrix, 1);
                bangquyetdinh.ChuanHoa_TrongSoMatrix[matrix.GetLength(0)-2, 2] = GetMaxAColumn(bangquyetdinh.ChuanHoa_TrongSoMatrix, 2);
                bangquyetdinh.ChuanHoa_TrongSoMatrix[matrix.GetLength(0)-2, 3] = GetMaxAColumn(bangquyetdinh.ChuanHoa_TrongSoMatrix, 3);
                bangquyetdinh.ChuanHoa_TrongSoMatrix[matrix.GetLength(0)-2, 4] = GetMaxAColumn(bangquyetdinh.ChuanHoa_TrongSoMatrix, 4);
                bangquyetdinh.ChuanHoa_TrongSoMatrix[matrix.GetLength(0)-2, 5] = GetMaxAColumn(bangquyetdinh.ChuanHoa_TrongSoMatrix, 5);
                //lí tưởng xấu
                bangquyetdinh.ChuanHoa_TrongSoMatrix[matrix.GetLength(0) - 1, 1] = GetMinAColumn(bangquyetdinh.ChuanHoa_TrongSoMatrix, 1);
                bangquyetdinh.ChuanHoa_TrongSoMatrix[matrix.GetLength(0) - 1, 2] = GetMinAColumn(bangquyetdinh.ChuanHoa_TrongSoMatrix, 2);
                bangquyetdinh.ChuanHoa_TrongSoMatrix[matrix.GetLength(0) - 1, 3] = GetMinAColumn(bangquyetdinh.ChuanHoa_TrongSoMatrix, 3);
                bangquyetdinh.ChuanHoa_TrongSoMatrix[matrix.GetLength(0) - 1, 4] = GetMinAColumn(bangquyetdinh.ChuanHoa_TrongSoMatrix, 4);
                bangquyetdinh.ChuanHoa_TrongSoMatrix[matrix.GetLength(0) - 1, 5] = GetMinAColumn(bangquyetdinh.ChuanHoa_TrongSoMatrix, 5);

                //đổi tên hàng lí tưởng xấu và tốt:
                bangquyetdinh.ChuanHoa_TrongSoMatrix[matrix.GetLength(0) - 2, 0] = "Lí tưởng tốt(A*)";
                bangquyetdinh.ChuanHoa_TrongSoMatrix[matrix.GetLength(0) - 1, 0] = "Lí tưởng xấu(A-)";

                //khoảng cách với lí tưởng tốt và xấu: thêm 2 cột lí tưởng và 1 cột độ tương tự=>9
                string[,] matranlituong = new string[bangquyetdinh.ChuanHoa_TrongSoMatrix.GetLength(0),9];
                //tên cột
                matranlituong[0, 6] = "Khảng cách tới A*(S*)";
                matranlituong[0, 7] = "Khoảng cách tới A-(S-)";
                matranlituong[0, 8] = "Phương án hợp lí C*";
                for (int i = 0; i < bangquyetdinh.ChuanHoa_TrongSoMatrix.GetLength(0); i++)
                {
                    for(int j = 0; j < 6; j++)
                    {
                        matranlituong[i, j] = bangquyetdinh.ChuanHoa_TrongSoMatrix[i, j];
                    }
                }
                for (int i = 0; i < diemcacnams.Count(); i++)
                {
                    //so với tốt
                    //bangquyetdinh.ChuanHoa_TrongSoMatrix[i+1, 6] = GetKhoangCachToGiaiPhapLiTuong(bangquyetdinh.ChuanHoa_TrongSoMatrix,i+1, 2);
                    matranlituong[i + 1, 6] = GetKhoangCachToGiaiPhapLiTuong(bangquyetdinh.ChuanHoa_TrongSoMatrix, i + 1, 2);
                    //so với xấu
                    matranlituong[i + 1, 7] = GetKhoangCachToGiaiPhapLiTuong(bangquyetdinh.ChuanHoa_TrongSoMatrix, i + 1, 1);
                    //độ tương tự
                    matranlituong[i + 1, 8] = GetDoTuongTu(matranlituong, i + 1).ToString("0.####");
                }
                bangquyetdinh.Matranlituong = matranlituong;

                ///từ đây ta lấy đc độ tương tự tới phương án hợp lí
                //lấy giá trị max của cột C*
                //từ giá trị max => Tên viện
                double Cmax=0;
                string predictVien="";
                string dongMax = "";

                for (int i = 0; i < diemcacnams.Count;i++)
                {
                    if (Convert.ToDouble(matranlituong[i+1, 8]) > Cmax)
                    {
                        Cmax = Convert.ToDouble(matranlituong[i+1, 8]);
                        predictVien = matranlituong[i + 1, 0];
                        dongMax = (i+1).ToString();
                    }

                }
                TempData["Yeucau"] = "Yêu cầu trợ giúp: Trường "+ GetTruongByID(Convert.ToInt16(truongID))+", Khối: "+db.Khois.SingleOrDefault(x=>x.ID.ToString()==khoiID).Ten+", Sở thích: " + st.Ten + " ," + Diem + " điểm";
                TempData["Predict"] = "Chọn ngành: " + predictVien;

                ViewBag.CMax = Cmax;
                ViewBag.DongMax = dongMax;
                
                ViewBag.Dong = dong+3; //số dòng=số lượng viện + thêm 3
                ViewBag.Cot = cot;
                return PartialView(bangquyetdinh);
            }
        }

        //từ bảng các năm lấy đc id viện,=> tên viện
        public string GetTenVienById(int id)
        {
            using(HeTroGiupQuyetDinh1Entities db=new HeTroGiupQuyetDinh1Entities())
            {
                Vien v = db.Viens.SingleOrDefault(x => x.Id == id);
                return v.Ten;
            }
        }
        //từ ID Viện(ngành) lấy ra đc điểm các năm
        public double GetDiemByIdVien(int id, int nam)
        {
            using(HeTroGiupQuyetDinh1Entities db=new HeTroGiupQuyetDinh1Entities())
            {
                DiemCacNam diemcacnam = db.DiemCacNams.FirstOrDefault(x => x.IDVien == id);
                if (nam == 2014)
                {
                    return diemcacnam.Nam2014;
                }
                if (nam == 2015)
                {
                    return diemcacnam.Nam2015;
                }
                if (nam == 2016)
                {
                    return diemcacnam.Nam2016;
                }
                if (nam == 2017)
                {
                    return diemcacnam.Nam2017;
                }
                return 0;
            }
        }
        //quy độ chênh lệch điểm về chuẩn, càng gần điểm năm cũ và lớn hơn càng tốt
        //coi độ chênh lệch là dương
        public double ChuanHoaChenhLechDiem(double diemdauvao, double diemcu)
        {
            double result = 0;
            //quy về chuẩn từ 1->10, chênh lệch từ 1-> 30
            double chenhlech = Math.Abs(diemdauvao - diemcu);
            //nếu chênh lệch là 0=> trả về 10
            if (chenhlech == 0)
            {
                return 10; //max là 10
            }
            if (diemdauvao > diemcu)
            {
                result = 10 - (chenhlech / 15) * 10; //chạy từ 0 tới 10
            }
            if (diemdauvao < diemcu)
            {
                result =(chenhlech / 15) * -10; // chạy từ -10 tới 0

            }
            return result;
        }
        //lấy giá trị max mỗi cột từ matran
        public string GetMaxAColumn(string[,] a, int cot)
        {
            string max = a[1, cot]; //giả sử max ở hàng đầu( ko kể hàng header)
            //int x = a.GetLength(1); //bằng số cột
            //int y = a.GetLength(0); //bằng số hàng
            //var width = input2DArray.GetLength(0);
            //var height = input2DArray.GetLength(1);
            for (int i = 1; i<a.GetLength(0)-2; i++)
            {
                //nếu tồn tại giá trị lớn hơn thì trả về số đó
                //a.Length=30 tức 6(cột)*5(hàng)
                if (Convert.ToDouble(a[i, cot]) > Convert.ToDouble(max))
                {
                    max = Convert.ToDouble(a[i, cot]).ToString("0.####");
                }
            }
            return max;
        }
        //lấy giá trị min mỗi cột 
        public string GetMinAColumn(string[,] a, int cot)
        {
            string min = a[1, cot]; //giả sử max ở hàng đầu( ko kể hàng header)
            //int x = a.GetLength(1); //bằng số cột
            //int y = a.GetLength(0); //bằng số hàng
            //var width = input2DArray.GetLength(0);
            //var height = input2DArray.GetLength(1);
            for (int i = 1; i < a.GetLength(0) - 2; i++)
            {
                //nếu tồn tại giá trị lớn hơn thì trả về số đó
                //a.Length=30 tức 6(cột)*5(hàng)
                if (Convert.ToDouble(a[i, cot]) <Convert.ToDouble(min))
                {
                    min = Convert.ToDouble(a[i, cot]).ToString("0.####");
                }
            }
            return min;
        }
        //chuẩn hóa: x/căn tổng x bình
        //public double ChuanHoa(string[,] a,int hang, int cot)
        //{
        //    //dòng từ 1 tới leng(0)-2
        //    //cột từ 1 tới 5

        //    double tongbinhphuong = 0;
        //    //hàng 1 tới hàng leng-2
        //    for (int i = 1; i < a.GetLength(0) - 2; i++)
        //    {
        //        //tính tổng bình phương cột: cot
        //        tongbinhphuong += Convert.ToDouble(a[i, cot]);
        //    }
        //    return Convert.ToDouble(a[hang, cot]) / Math.Sqrt(tongbinhphuong);

        //}
        public string[,] ChuanHoa(string[,] a)
        {
            string[,] b=new string[a.GetLength(0),a.GetLength(1)];
            //dòng từ 1 tới leng(0)-2
            //cột từ 1 tới 5

            double tongbinhphuong = 0;
            //hàng 0:
            for(int i = 0; i < 6;i++)
            {
                b[0,i] = a[0,i];

            }
            //cột 0:
            for(int i = 0; i < a.GetLength(0);i++)
            {
                b[i, 0] = a[i, 0];

            }
            //cột từ 1 tới 5
            for (int j = 1; j <= 5; j++)
            {
                //hàng từ 1 tới hàng leng-2
                //tính tổng bình phương từng cột
                for (int i = 1; i < a.GetLength(0) - 2; i++)
                    {
                        //tính tổng bình phương cột: cot
                        tongbinhphuong += Convert.ToDouble(a[i, j])* Convert.ToDouble(a[i, j]);
                    }
                //chuẩn hóa số
                for (int i = 1; i < a.GetLength(0)-2; i++)
                {
                    //tính tổng bình phương cột: cot
                    b[i,j]=(Convert.ToDouble(a[i, j]) / Math.Sqrt(tongbinhphuong)).ToString("0.####");
                }
                for (int i = a.GetLength(0) - 2; i < a.GetLength(0); i++)
                {
                    //tính tổng bình phương cột: cot
                    b[i, j] = a[i, j];
                }
                tongbinhphuong = 0;

            }
            return b;

        }
        //ma trận sau khi tính giá trị theo trọng số
        public string[,] ChuanHoa_TrongSo(string[,] a)
        {
            string[,] b = new string[a.GetLength(0), a.GetLength(1)];
            //dòng từ 1 tới leng(0)-2
            //cột từ 1 tới 5

            //double tongbinhphuong = 0;
            //hàng 0:
            for (int i = 0; i < 6; i++)
            {
                b[0, i] = a[0, i];

            }
            //cột 0:
            for (int i = 0; i < a.GetLength(0); i++)
            {
                b[i, 0] = a[i, 0];

            }
            //cột từ 1 tới 5
            for (int j = 1; j <= 5; j++)
            {
                //hàng từ 1 tới hàng leng-2
                //tính tổng bình phương từng cột
                //for (int i = 1; i < a.GetLength(0) - 2; i++)
                //{
                //    //tính tổng bình phương cột: cot
                //    tongbinhphuong += Convert.ToDouble(a[i, j]) * Convert.ToDouble(a[i, j]);
                //}
                //chuẩn hóa số
                int dongcuoiIndex=a.GetLength(0) - 1;
                for (int i = 1; i < a.GetLength(0)-2; i++)
                {
                    //tính tổng bình phương cột: cot
                    b[i, j] = (Convert.ToDouble(a[i, j])*Convert.ToDouble(a[dongcuoiIndex,j ])).ToString("0.####");
                }

            }
            return b;

        }

        //chú ý: mức độ quan trọng âm=> cần trừ khi tính S.

        //khoảng cách mỗi phương án tới giải pháp lí tưởng
        public string GetKhoangCachToGiaiPhapLiTuong(string[,] a,int hang, int lituong)
        {
            double x=0;
            //leng-1: xấu, leng-2: tốt
            //từ cột 1 tới 5
            for(int i = 1; i < 6; i++)
            {
                //hang giải pháp=1=> so với xấu
                //hàng giải pháp =2=> so với tốt
                double hangsosanh = Convert.ToDouble(a[hang, i]);
                double hanggiaiphap = Convert.ToDouble(a[a.GetLength(0) - lituong, i]);
                x += (hangsosanh -hanggiaiphap)* (hangsosanh - hanggiaiphap);
            }
            
            return Math.Sqrt(x).ToString("0.####");

        }
        //độ tương tự
        public double GetDoTuongTu(string[,] a,int hang)
        {
            double tong = Convert.ToDouble(a[hang, a.GetLongLength(1) - 2]) + Convert.ToDouble(a[hang, a.GetLongLength(1) - 3]);
            return Convert.ToDouble(a[hang, a.GetLongLength(1) - 2]) / tong;
        }

        //lấy id trường từ tên trường
        public string GetTruongByID(int truongid)
        {
            using(HeTroGiupQuyetDinh1Entities db=new HeTroGiupQuyetDinh1Entities())
            {
                return db.Truongs.SingleOrDefault(x=>x.ID== truongid).Ten.ToString();
            }
        }
        //lấy điểm trường dựa vào id
        public string GetDiemTruongByID(int truongid)
        {
            using(HeTroGiupQuyetDinh1Entities db=new HeTroGiupQuyetDinh1Entities())
            {
                return db.Truongs.SingleOrDefault(x=>x.ID==truongid).DiemChuan.ToString();
            }
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}