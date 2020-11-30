using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ServiceReservasi
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        string pemesanan(string IDPemesanan, string NamaCustomer, string NoTelpon, int JumlahPemesanan, string IDLokasi);//method (proses input data)

        [OperationContract]
        string editPemesanan(string IDPemesanan, string NamaCustomer, string No_telpon);

        [OperationContract]
        string deletePemesanan(string IDPemesanan);

        [OperationContract]
        List<CekLokasi> ReviewLokasi();//menampilkan data yg ada di database (select all) // menampilkan isi dr yg ada contract

        [OperationContract]
        List<DetailLokasi> DetailLokasi();//menampilkan detail lokasi

        [OperationContract]
        List<Pemesanan> Pemesanan();

    }

    [DataContract]
    public class CekLokasi //daftar lokasi nongkrong
    {
        [DataMember]
        public string IDLokasi { get; set; }//variabel dr public class
        [DataMember]
        public string NamaLokasi { get; set; }
        [DataMember]
        public string DeskripsiSingkat { get; set; }

    }

    [DataContract]
    public class DetailLokasi //menampilkan detail lokasi
    {
        [DataMember]
        public string IDLokasi { get; set; }//variabel dr public class
        [DataMember]
        public string NamaLokasi { get; set; }
        [DataMember]
        public string DeskripsiFull { get; set; }
        [DataMember]
        public int Kuota { get; set; }

    }

    [DataContract]
    public class Pemesanan //menampilkan detail lokasi
    {
        [DataMember]
        public string IDPemesanan { get; set; }//variabel dr public class
        [DataMember]
        public string NamaCustomer { get; set; }//method
        [DataMember]
        public string NoTelpon { get; set; }
        [DataMember]
        public int JumlahPemesanan { get; set; }
        [DataMember]
        public string Lokasi { get; set; }

    }
}
