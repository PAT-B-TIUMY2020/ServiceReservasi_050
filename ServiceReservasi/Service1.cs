using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ServiceReservasi
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class Service1 : IService1
    {
        string constring = "Data Source=DESKTOP-5C1L09A;Initial Catalog=WCFReservasi;Persist Security Info=True;User ID=sa;Password=12345";
        SqlConnection connection;
        SqlCommand com;//untuk mengkoneksikan database ke visual studio

        public string deletePemesanan(string IDPemesanan)
        {
            string a = "gagal";
            try
            {
                string sql = "delete from dbo.Pemesanan where ID_reservasi ='"+ IDPemesanan +"'"; 
                connection = new SqlConnection(constring); //fungsi koneksi ke database
                com = new SqlCommand(sql, connection);
                connection.Open();
                com.ExecuteNonQuery();
                connection.Close();

                a = "sukses";
            }
            catch (Exception es)
            {
                Console.WriteLine(es);
            }
            return a;
        }

        public List<DetailLokasi> DetailLokasi()
        {
            List<DetailLokasi> LokasiFull = new List<DetailLokasi>(); //proses untuk mendeclare nama list yg telah dibuat dgn nama baru
            try
            {
                string sql = "select ID_lokasi, Nama_lokasi, Deskripsi_full, Kuota from dbo.Lokasi";//declare query
                connection = new SqlConnection(constring);//fungsi konek ke database
                com = new SqlCommand(sql, connection);//proses execute query
                connection.Open();//membuka koneksi
                SqlDataReader reader = com.ExecuteReader();//menampilkan data query
                while (reader.Read())
                {
                    //nama class

                    DetailLokasi data = new DetailLokasi();//deklarasi data, mengambil 1 per 1 dr database
                    //bentuk array
                    data.IDLokasi = reader.GetString(0);//0 itu index, ada dikolom keerapa di string sql diatas
                    data.NamaLokasi = reader.GetString(1);
                    data.DeskripsiFull = reader.GetString(2);
                    data.Kuota = reader.GetInt32(3);
                    LokasiFull.Add(data);
                }
                connection.Close();//utk menutup akses ke database


            } catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return LokasiFull;
        }

        public string editPemesanan(string IDPemesanan, string NamaCustomer, string No_telpon)
        {
            string a = "gagal";
            try
            {
                string sql = "update dbo.Pemesanan set Nama_customer = '" + NamaCustomer + "', No_telpon = '" + No_telpon + "'" +
                             " where ID_reservasi = '" + IDPemesanan + "' ";
                connection = new SqlConnection(constring); //fungsi koneksi ke database
                com = new SqlCommand(sql, connection);
                connection.Open();
                com.ExecuteNonQuery();
                connection.Close();

                a = "sukses";
            }
            catch (Exception es)
            {
                Console.WriteLine(es);
            }
            return a;
        }

        public string pemesanan(string IDPemesanan, string NamaCustomer, string NoTelpon , int JumlahPemesanan, string IDLokasi)
        {
            string a = "gagal";
            try
            {
                //petik 1 utk menyatakan varchar, petik 2 menyatakan integer
                string sql ="insert into dbo.Pemesanan values ('"+ IDPemesanan + "', '" + NamaCustomer + "','" + NoTelpon + "'," + JumlahPemesanan + ",'"+ IDLokasi+ "')";
                connection = new SqlConnection(constring);//fungsi koneksi ke database
                com = new SqlCommand(sql, connection);
                connection.Open();
                com.ExecuteNonQuery();
                connection.Close();

                string sql2 = "update dbo.Lokasi set Kuota = Kuota - " + JumlahPemesanan + " where ID_Lokasi = '" + IDLokasi + "'";
                connection = new SqlConnection(constring);//fungsi konek ke database
                com = new SqlCommand(sql2, connection);
                connection.Open();
                com.ExecuteNonQuery();
                connection.Close();

                a = "sukses";
            }
            catch(Exception es)
            {
                Console.WriteLine(es);
            }
            return a;

        }

        public List<Pemesanan> Pemesanan()
        {
            List<Pemesanan> pemesanan = new List<Pemesanan>();//proses utk mendeclare nama list yg telah dibuat 
            try
            {
                string sql = "select ID_reservasi, Nama_customer, No_telpon, Jumlah_pesanan, Nama_lokasi from dbo.Pemesanan p join dbo.Lokasi l on p.ID_lokasi = l.ID_lokasi";
                connection = new SqlConnection(constring);//fungsi konek ke database
                com = new SqlCommand(sql, connection);
                connection.Open();//proses execude query
                SqlDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    //nama class
                    Pemesanan data = new Pemesanan();
                    //bentuk array
                    data.IDPemesanan = reader.GetString(0);
                    data.NamaCustomer = reader.GetString(1);
                    data.NoTelpon = reader.GetString(2);
                    data.JumlahPemesanan = reader.GetInt32(3); 
                    data.Lokasi = reader.GetString(4);
                    pemesanan.Add(data);//mengumpulkan data yang awalnya dari array
                }
                connection.Close();//untuk menutup akses ke db
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
            return pemesanan;

        }

        public List<CekLokasi> ReviewLokasi()
        {
            throw new NotImplementedException();
        }

        public string Login(string username, string password)
        {
            string kategori = "";

            string sql = "select Kategori from Login where Username='" + username + "' and Password='" + password + "'";
            connection = new SqlConnection(constring);
            com = new SqlCommand(sql, connection);
            connection.Open();
            SqlDataReader reader = com.ExecuteReader();
            while (reader.Read())
            {
                kategori = reader.GetString(0);
            }
            return kategori;
        }

        public string Register(string username, string password, string kategori)
        {
            try
            {
                string sql = "insert into Login values('" + username + " ', '" + password + "' , '" + kategori + " ')";
                connection = new SqlConnection(constring);
                com = new SqlCommand(sql, connection);
                connection.Open();
                com.ExecuteNonQuery();
                connection.Close();

                return "sukses";
            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }

        public string UpdateRegister(string username, string password, string kategori, int id)
        {
            try
            {
                string sql2 = "update Login SET Username '" + username + "', Password='"+ password+"', Kategori= '"+ kategori+"" +
                    "where ID_login = " + id +"";
                connection = new SqlConnection(constring);
                com = new SqlCommand(sql2, connection);
                connection.Open();
                com.ExecuteNonQuery();
                connection.Close();

                return "sukses";

            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }

        public string DeleteRegister(string username)
        {
            try
            {
                int id = 0;
                string sql = "select ID_login from dbo.Login where Username ='"+ username+"'";
                connection = new SqlConnection(constring);
                com = new SqlCommand(sql, connection);
                connection.Open();
                SqlDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    id = reader.GetInt32(0);
                }
                connection.Close();
                string sql2 = "delete from Login where ID_login" + id + "";
                connection = new SqlConnection(constring);
                com = new SqlCommand(sql2, connection);
                connection.Open();
                com.ExecuteNonQuery();
                connection.Close();

                return "sukses";
            }
            catch(Exception e)
            {
                return e.ToString();
            }
        }

        public List<DataRegister> DataRegist()
        {
            List<DataRegister> list = new List<DataRegister>();
            try
            {
                string sql = "select ID_login, Username, Password, Kategori from Login";
                connection = new SqlConnection(constring);
                com = new SqlCommand(sql, connection);
                connection.Open();
                SqlDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    DataRegister data = new DataRegister();
                    data.id = reader.GetInt32(0);
                    data.username = reader.GetString(1);
                    data.password = reader.GetString(2);
                    data.kategori = reader.GetString(3);
                    list.Add(data);
                }
                connection.Close();
            }
            catch(Exception e)
            {
                e.ToString();
            }
            return list;
        }

    }
}
