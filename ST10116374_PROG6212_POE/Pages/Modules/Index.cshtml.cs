using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using System.Data.SqlClient;
using ST10116374_PROG6212_POE_ClassLibrary;
using System.Reflection.Emit;
using System.Xml.Linq;
using XAct;

namespace ST10116374_PROG6212_POE.Pages.Modules
{
    public class IndexModel : PageModel
    {
        //These are list for connect sending and receiving data
        public List<SetDateInfo> listSetDay = new List<SetDateInfo>();
        public List<ModuleInfo> listModules = new List<ModuleInfo>();
        public void OnGet()
        {
            try
            {
                //This is the sql connection string you will need to change this to connect to the database there is one in login.cshtml.cs, Register.cshtml.cs, Setday.cshtml.cs, edit.cshtml.cs, create.cshtml.cs, delete.cshtml, deletesetday.cshtml
                string connectionString = "Data Source=(localdb)\\MSSqlLocalDb;Initial Catalog=modulesdb;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM modules"; //this is the sql query to make the data from the database table modules appear in the web app
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read()) //This reads from the database and outputs it to the web app
                            {
                                ModuleInfo moduleinfo = new ModuleInfo();
                                moduleinfo.id = "" + reader.GetInt32(0);
                                moduleinfo.mdcode = reader.GetString(1);
                                moduleinfo.mdname = reader.GetString(2);
                                moduleinfo.credits = reader.GetInt32(3);
                                moduleinfo.hourspweek = reader.GetInt32(4);
                                moduleinfo.weeks = reader.GetInt32(5);
                                moduleinfo.startdate = reader.GetString(6);
                                moduleinfo.CurrentHours = reader.GetInt32(7);
                                moduleinfo.Selfstudyhrs = reader.GetInt32(8);
                                moduleinfo.Selfstudyleft = reader.GetInt32(9);

                                listModules.Add(moduleinfo);//adds the data to the list
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString);
            }
            try
            {
                string connectionStrings = "Data Source=(localdb)\\MSSqlLocalDb;Initial Catalog=modulesdb;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionStrings))
                {
                    connection.Open();
                    string sql = "SELECT * FROM setaday;"; //select statment to get the data from the databse
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())//This reads from the database and outputs it to the web app
                            {
                                SetDateInfo listsetday = new SetDateInfo();
                                listsetday.id = "" + reader.GetInt32(0);
                                listsetday.sday = reader.GetString(1);
                                listsetday.mdscode = reader.GetString(2);
                                listsetday.studyhrs = reader.GetInt32(3);
                                listsetday.datecreatedon = reader.GetDateTime(4).ToString();

                                listSetDay.Add(listsetday);//adds the data to the list
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString);
            }
        }
        //these two methods hold the needed requiremnets for the database and lists
        public class ModuleInfo
        {  
            public string id;
            public string mdcode;
            public string mdname;
            public int credits;
            public int hourspweek;
            public int weeks;
            public string startdate;
            public int CurrentHours;
            public int Selfstudyhrs;
            public int Selfstudyleft;
        }

        public class SetDateInfo
        {
            public string id;
            public string sday;
            public string mdscode;
            public int studyhrs;
            public string datecreatedon;
        }
    }
}
