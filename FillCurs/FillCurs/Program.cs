using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using static System.Console;

class FillDB
{
    static string connectionString = "Data Source=M15;Initial Catalog=Curs;Integrated Security=True";
    static private SqlConnection sqlConn = new SqlConnection(connectionString);
    static private SqlDataAdapter data;
    static private SqlCommand Com;
    static Random r = new Random();
    static List<string> NameSex = new List<string>();
    static List<string> Address = new List<string>();
    static List<string> Medicine = new List<string>();
    static List<string> Administering = new List<string>();
    static List<string> SideEffects = new List<string>();
    static List<string> Action = new List<string>();
    static List<string> DoctorName = new List<string>();
    static List<string> Diagnosis = new List<string>();
    static List<string> IDMedicine = new List<string>();
    static List<string> IDPatients = new List<string>();
    static void Main()
    {
        WNameSex();
        WAddress();
        WMedicine();
        WAdministering();
        WSideEffects();
        WAction();
        WDoctorName();
        WDiagnosis();
        WIDMedicine();
        WIDPatients();
        sqlConn.Open();
        for (int i = 0; i < 1500; i++)
        {
            try
            {
                string str = GenRowPatients();
                Com = new SqlCommand(str, sqlConn);
                Com.ExecuteNonQuery();
            }
            catch { }
        }
        for (int i = 0; i < 500; i++)
        {
            try
            {
                string str = GenRowMedicine();
                Com = new SqlCommand(str, sqlConn);
                Com.ExecuteNonQuery();
            }
            catch { }
        }
        for (int i = 0; i < 500; i++)
        {
            try
            {
                string str = GenRowExamination();
                Com = new SqlCommand(str, sqlConn);
                Com.ExecuteNonQuery();
            }
            catch { }
        }
        sqlConn.Close();
    }
    static List<string> WSurname()
    {
        StreamReader read = new StreamReader("D:\\Уроки\\ОП\\Курсова\\Surname.txt");
        char[] separator = new char[] { ' ' };
        List<string> list = read.ReadToEnd().Split(separator, StringSplitOptions.RemoveEmptyEntries).ToList();//1 6 11  ..... count - 4 = i-3
        int count = list.Count-1;
        for (int i = count; i >= 0; i--)
        {

            if (i % 5 != 1)
            {
                list.RemoveAt(i);
            }
        }
        for(int i = 0; i<list.Count;i++)
        {
            list[i] = list[i].Substring(0, 1) + list[i].Substring(1, list[i].Length - 1).ToLower();
            list[i]=list[i].Trim();
        }
        return list;
    }
    static List<string> Wnamesex()
    {
        StreamReader read = new StreamReader("D:\\Уроки\\ОП\\Курсова\\Name.txt");
        char[] separator = new char[] { '\n' };
        List<string> list = read.ReadToEnd().Split(separator, StringSplitOptions.RemoveEmptyEntries).ToList();
        for(int i = 0; i<list.Count; i++)
        {
            list[i] = list[i].Trim();
            if (i >= 514)
            {
                list[i] += "|" + "жінка";
            }
            else
            {
                list[i] += "|" + "чоловік";
            }
        }
        return list;
    }
    static void WNameSex()
    {
        List<string> Surname = new List<string>();
        List<string> name = new List<string>();
        Surname = WSurname();
        name = Wnamesex();
        for (int i = 0; i < 3000; i++)
        {
            string x = Surname[r.Next(Surname.Count - 1)] + " " + name[r.Next(name.Count - 1)];
            NameSex.Add(x);
        }
    }
    static DateTime GenerateBirthday()
    {
        string s;
        string year, month, day;
        year = r.Next(1930, 2022).ToString();
        month = r.Next(1, 12).ToString();
        //if(month.Length == 1) { month = "0" + month; }
        day = r.Next(1, 18).ToString();
        //if(day.Length == 1) { day = "0" + day; }
        s = year + "." + month + "." + day;
        return DateTime.Parse(s);
    }
    static void WAddress()
    {
        StreamReader read = new StreamReader("D:\\Уроки\\ОП\\Курсова\\Address.txt");
        char[] separator = new char[] { '\n' };
        Address = read.ReadToEnd().Split(separator, StringSplitOptions.RemoveEmptyEntries).ToList();
        for(int i = 0; i < Address.Count; i++)
        {
            Address[i]= Address[i].Trim();
        }
    }
    static string GenRowPatients()
    {
        string str="insert into Patients (Name, Sex, Birthday, Address) values (";
        char[] separator = new char[] { '|' };
        string[] arr = NameSex[r.Next(NameSex.Count - 1)].Split(separator);
        str += "'" + arr[0] + "', ";
        str += "'" + arr[1] + "', ";
        str += "'" + GenerateBirthday().ToString().Substring(0,10) + "', ";
        str += "'" + Address[r.Next(Address.Count-1)] + "');";
        return str;
    }
    static void WMedicine()
    {
        StreamReader read = new StreamReader("D:\\Уроки\\ОП\\Курсова\\Medicine.txt");
        char[] separator = new char[] { '\n' };
        Medicine = read.ReadToEnd().Split(separator, StringSplitOptions.RemoveEmptyEntries).ToList();
        for( int i = 0; i < Medicine.Count; i++)
        {
            Medicine[i]= Medicine[i].Trim();
        }
    }
    static void WAdministering()
    {
        StreamReader read = new StreamReader("D:\\Уроки\\ОП\\Курсова\\Administering.txt");
        char[] separator = new char[] { '\n' };
        Administering = read.ReadToEnd().Split(separator, StringSplitOptions.RemoveEmptyEntries).ToList();
        for (int i = 0; i < Administering.Count; i++)
        {
            Administering[i] = Administering[i].Trim();
        }
    }
    static void WSideEffects()
    {
        StreamReader read = new StreamReader("D:\\Уроки\\ОП\\Курсова\\SideEffects.txt");
        char[] separator = new char[] { '\n' };
        SideEffects = read.ReadToEnd().Split(separator, StringSplitOptions.RemoveEmptyEntries).ToList();
        for (int i = 0; i < SideEffects.Count; i++)
        {
            SideEffects[i] = SideEffects[i].ToLower().Trim();
        }
    }
    static void WAction()
    {
        StreamReader read = new StreamReader("D:\\Уроки\\ОП\\Курсова\\Action.txt");
        char[] separator = new char[] { '\n' };
        Action = read.ReadToEnd().Split(separator, StringSplitOptions.RemoveEmptyEntries).ToList();
        for (int i = 0; i < Action.Count; i++)
        {
            Action[i] = Action[i].Trim();
        }
    }
    static string GenRowMedicine()
    {
        string str = "insert into Medicine (Name, Administering, Action, SideEffects) values (";
        str += "'" + Medicine[r.Next(Medicine.Count-1)] + "', ";
        str += "'" + Administering[r.Next(Administering.Count - 1)] + "', ";
        str += "'" + Action[r.Next(Action.Count - 1)] + "', ";
        string str1="";
        int count = r.Next(7);
        while (count != 0)
        {
            str1 += SideEffects[r.Next(SideEffects.Count - 1)]+", ";
            count--;
        }
        str1=str1.Substring(0, str1.Length - 2);
        str += "'" + str1 + "');";
        return str;
    }
    static void WDoctorName()
    {
       foreach(string s in NameSex)
        {
            char[] separator = new char[] { '|' };
            string[] arr = s.Split(separator);
            DoctorName.Add(arr[0]);
        }
    }
    static DateTime GenerateDate()
    {
        string s;
        string year, month, day;
        year = "2022";
        month = r.Next(1, 6).ToString();
        //if(month.Length == 1) { month = "0" + month; }
        day = r.Next(1, 28).ToString();
        //if(day.Length == 1) { day = "0" + day; }
        s = year + "." + month + "." + day;
        return DateTime.Parse(s);
    }
    static void WDiagnosis()
    {
        StreamReader read = new StreamReader("D:\\Уроки\\ОП\\Курсова\\Diagnosis.txt");
        char[] separator = new char[] { '\n' };
        Diagnosis = read.ReadToEnd().Split(separator, StringSplitOptions.RemoveEmptyEntries).ToList();
        for (int i = 0; i < Diagnosis.Count; i++)
        {
            Diagnosis[i] = Diagnosis[i].Trim();
        }
    }
    static void WIDMedicine()
    {
        sqlConn.Open();
        data=new SqlDataAdapter("select IDMedicine from Medicine", sqlConn);
        DataTable dt = new DataTable();
        data.Fill(dt);
        for(int i = 0; i < dt.Rows.Count; i++)
        {
            IDMedicine.Add(dt.Rows[i][0].ToString());
        }
        sqlConn.Close();
    }
    static void WIDPatients()
    {
        sqlConn.Open();
        data = new SqlDataAdapter("select IDPatient from Patients", sqlConn);
        DataTable dt = new DataTable();
        data.Fill(dt);
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            IDPatients.Add(dt.Rows[i][0].ToString());
        }
        sqlConn.Close();
    }
    static string GenRowExamination()
    {
        string str = "insert into Examination values (";
        str += "'" + IDPatients[r.Next(IDPatients.Count-1)] + "', ";
        str += "'" + DoctorName[r.Next(DoctorName.Count - 1)] + "', ";
        str += "'" + GenerateDate() + "', ";
        str += "'" + Address[r.Next(Address.Count - 1)] + "', ";
        str += "'" + SideEffects[r.Next(SideEffects.Count - 1)] + "', ";
        str += "'" + Diagnosis[r.Next(Diagnosis.Count - 1)] + "', ";
        str += "'" + IDMedicine[r.Next(IDMedicine.Count - 1)] + "');";
        return str;
    }
}