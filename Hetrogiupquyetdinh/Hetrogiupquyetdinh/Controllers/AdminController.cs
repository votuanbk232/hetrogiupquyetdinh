using Hetrogiupquyetdinh.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hetrogiupquyetdinh.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult QuanLiNganh()
        {
            using(HeTroGiupQuyetDinh1Entities db=new HeTroGiupQuyetDinh1Entities())
            {
                List<Vien> viens = db.Viens.ToList();
                return View(viens);
            }
        }
        
            public ActionResult QuanLiTruong()
        {
            using (HeTroGiupQuyetDinh1Entities db = new HeTroGiupQuyetDinh1Entities())
            {
                List<Truong> truongs = db.Truongs.ToList();
                return View(truongs);
            }
        }
        
             public ActionResult QuanLiSoThich()
        {
            using (HeTroGiupQuyetDinh1Entities db = new HeTroGiupQuyetDinh1Entities())
            {
                List<SoThich> sothichs = db.SoThiches.ToList();
                return View(sothichs);
            }
        }
        
            public ActionResult QuanLiNganh_SoThich()
        {
            using (HeTroGiupQuyetDinh1Entities db = new HeTroGiupQuyetDinh1Entities())
            {
                List<Vien_SoThich> vien_sothichs = db.Vien_SoThich.ToList();
                return View(vien_sothichs);
            }
        }
        
                 public ActionResult QuanLiDiemCacNam()
        {
            using (HeTroGiupQuyetDinh1Entities db = new HeTroGiupQuyetDinh1Entities())
            {
                List<DiemCacNam> diemcacnams = db.DiemCacNams.ToList();
                return View(diemcacnams);
            }
        }
        public ActionResult Index()
        {
            return View();
        }
    }
}