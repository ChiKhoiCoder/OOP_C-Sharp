using System;
using System.Runtime.InteropServices.Marshalling;
using System.Transactions;
using System.Xml.Linq;
using ZstdSharp.Unsafe;


namespace programing
{
    public abstract class Nhanvien
    {
        private string idMa;
        private string name;
        private DateTime ngaySinh;

        public Nhanvien(string idMa, string name, DateTime ngaySinh)
        {
            this.idMa = idMa;
            this.name = name;
            this.ngaySinh = ngaySinh;
        }



        public string getidMa
        {
            get { return idMa; }
            set { idMa = value; }
        }
        public string getName
        {
            get { return name; }
            set { name = value; }
        }
        public DateTime getngaySinh
        {
            get { return ngaySinh; }
            set { ngaySinh = value; }
        }

        public abstract double TinhLuong();

        public virtual void displayInfo()
        {
            Console.WriteLine("Ma nhan vien: " + idMa);
            Console.WriteLine("Ten sinh vien: " + name);
            Console.WriteLine("Ngay sinh: " + ngaySinh);
        }

    }
    class nhanvienHC : Nhanvien
    {
        private int songaycong;
        private double luongCB;

        public nhanvienHC(string idMa, string name, DateTime ngaySinh, int songaycong, double luongCB) : base(idMa, name, ngaySinh)
        {
            this.songaycong = songaycong;
            this.luongCB = luongCB;
        }


        public int getsoNC
        {
            get { return songaycong; }
            set { songaycong = value; }
        }

        public double getluongCB
        {
            get { return luongCB; }
            set { luongCB = value; }
        }

        public override double TinhLuong()
        {
            return songaycong * luongCB;
        }

        public override void displayInfo()
        {
            base.displayInfo();
            Console.WriteLine("So ngay cong: " + songaycong);
            Console.WriteLine("Luong co ban la: " + luongCB);
            Console.WriteLine("Luong la: " + TinhLuong());
        }

    }
    class nhanvienSX : Nhanvien
    {
        private int soSP;
        private double DonGia;

        public nhanvienSX(string idMa, string name, DateTime ngaySinh, int soSP, double DonGia) : base(idMa, name, ngaySinh)
        {
            this.soSP = soSP;
            this.DonGia = DonGia;
        }


        public int getSoSp
        {
            get { return soSP; }
            set { soSP = value; }
        }

        public double getDonGia
        {
            get { return DonGia; }
            set { DonGia = value; }
        }

        public override double TinhLuong()
        {
            return soSP * DonGia;
        }
        public override void displayInfo()
        {
            base.displayInfo();
            Console.WriteLine("So san pham: " + soSP);
            Console.WriteLine("Don Gia: " + DonGia);
            Console.WriteLine("Luong la: " + TinhLuong());
        }

    }
    class Program
    {
        static List<Nhanvien> danhSach = new List<Nhanvien>();

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            while (true)
            {
                Console.WriteLine("\n===== MENU =====");
                Console.WriteLine("1. Thêm nhân viên hành chính");
                Console.WriteLine("2. Thêm nhân viên sản xuất");
                Console.WriteLine("3. Hiển thị tất cả nhân viên");
                Console.WriteLine("4. Tổng lương công ty");
                Console.WriteLine("5. Nhân viên có lương cao nhất");
                Console.WriteLine("6. Sắp xếp theo lương giảm dần");
                Console.WriteLine("0. Thoát");
                Console.Write("Chọn: ");
                string chon = Console.ReadLine();

                switch (chon)
                {
                    case "1":
                        ThemnhanvienHC();
                        break;
                    case "2":
                        ThemNVSanXuat();
                        break;
                    case "3":
                        HienThi();
                        break;
                    case "4":
                        TongLuong();
                        break;
                    case "5":
                        NVCaoNhat();
                        break;
                    case "6":
                        SapXep();
                        break;
                    case "0": return;
                    default:
                        Console.WriteLine("Chọn sai!");
                        break;
                }
            }
        }


        static void ThemnhanvienHC()
        {
            try
            {
                Console.Write("Mã nhân viên: ");
                string id = Console.ReadLine();

                Console.Write("Họ tên: ");
                string name = Console.ReadLine();

                Console.Write("Ngày sinh (dd/MM/yyyy): ");
                DateTime ngaySinh = DateTime.Parse(Console.ReadLine());

                Console.Write("Số ngày công: ");
                int soNgayCong = int.Parse(Console.ReadLine());

                Console.Write("Lương cơ bản: ");
                double luongCB = double.Parse(Console.ReadLine());

                var nvHC = new nhanvienHC(id, name, ngaySinh, soNgayCong, luongCB);
                danhSach.Add(nvHC);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi nhập: " + ex.Message);

            }
        
        }
        static void ThemNVSanXuat()
        {
            try
            {
                Console.Write("Mã: ");
                string id = Console.ReadLine();

                Console.Write("Họ tên: ");
                string name = Console.ReadLine();

                Console.Write("Ngày sinh (dd/MM/yyyy): ");
                DateTime ngaySinh = DateTime.Parse(Console.ReadLine());

                Console.Write("Số sản phẩm: ");
                int soSP = int.Parse(Console.ReadLine());

                Console.Write("Đơn giá: ");
                double donGia = double.Parse(Console.ReadLine());

                var nv = new nhanvienSX(id, name, ngaySinh, soSP, donGia);
                danhSach.Add(nv);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi nhập: " + ex.Message);
            }
        }
            static void HienThi()
            {
                foreach (var nv in danhSach)
                {
                    nv.displayInfo();
                    Console.WriteLine("-------------------------");
                }
            }

            static void TongLuong()
            {
                double tong = danhSach.Sum(nv => nv.TinhLuong());
                Console.WriteLine($"Tổng lương: {tong:0.00}");
            }

            static void NVCaoNhat()
            {
                if (danhSach.Count == 0)
                {
                    Console.WriteLine("Chưa có nhân viên.");
                    return;
                }

                var maxLuong = danhSach.Max(nv => nv.TinhLuong());
                Console.WriteLine("Nhân viên có lương cao nhất:");
                foreach (var nv in danhSach)
                {
                    if (nv.TinhLuong() == maxLuong)
                        nv.displayInfo();
                }
            }

            static void SapXep()
            {
                danhSach.Sort((a, b) => b.TinhLuong().CompareTo(a.TinhLuong()));
                Console.WriteLine("Đã sắp xếp theo lương giảm dần.");
            }
        }
    }


