using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static ST10116374_PROG6212_POE.Pages.Modules.IndexModel;
using System.Data.SqlClient;

namespace ST10116374_PROG6212_POE.Pages.Modules
{
    public class EditModel : PageModel
    {
        //calls the list from index
        public ModuleInfo moduleinfo = new ModuleInfo();
        public string errorMessage = "";          //calls error message when user enter wrong into the app
        public string succesMesage = "";          //calls succes message when user enter the right inputs into the app
        public int credits;                       //int are needed for rieving data in the form of ints
        public int hours_per_week;
        public int weeks;
        public int current_hours;

        public void OnGet()
        {
            String id = Request.Query["id"];//id is used to know which column needs to be edited

            try
            {
                string connectionString = "Data Source=(localdb)\\MSSqlLocalDb;Initial Catalog=modulesdb;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM modules WHERE id=@id";//id is used to know which column needs to be edited
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);//brings up the database data with id data
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
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

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex);
            }
        }

        public void Onpost()
        {
            //when the database gets updataed this is what will be updated
            moduleinfo.id = Request.Form["id"];
            moduleinfo.mdcode = Request.Form["code"];
            moduleinfo.mdname = Request.Form["name"];
            moduleinfo.credits = credits;
            moduleinfo.hourspweek = hours_per_week;
            moduleinfo.weeks = weeks;
            moduleinfo.startdate = Request.Form["start_date"];
            moduleinfo.CurrentHours = current_hours;

            if (moduleinfo.mdcode.Length == 0 || moduleinfo.mdname.Length == 0 ||
                moduleinfo.startdate.Length == 0 || moduleinfo.credits <= 0 || moduleinfo.hourspweek <= 0 || moduleinfo.weeks <= 0 || moduleinfo.CurrentHours <= 0)
            {
                errorMessage = "All the fields are required";
                return;
            }

            try
            {
                string connectionString = "Data Source=(localdb)\\MSSqlLocalDb;Initial Catalog=modulesdb;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "UPDATE modules" +
                                 "SET  code=@code, name=@name, credits=@credits, weeks=@weeks, hours_per_week=@hours_per_week, start_date=@start_date, current_hours=@current_hours" +
                                 "WHERE id=@id"; //this qeuery uses id to find whuch column to update the new data into
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@code", moduleinfo.mdcode);
                        command.Parameters.AddWithValue("@name", moduleinfo.mdname);
                        command.Parameters.AddWithValue("@credits", moduleinfo.credits);
                        command.Parameters.AddWithValue("@weeks", moduleinfo.weeks);
                        command.Parameters.AddWithValue("@hours_per_week", moduleinfo.hourspweek);
                        command.Parameters.AddWithValue("@start_date", moduleinfo.startdate);
                        command.Parameters.AddWithValue("@current_hours", moduleinfo.CurrentHours);
                        command.ExecuteNonQuery();
                    }
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex);
            }

            Response.Redirect("/Modules/Index");
        }
    }
}
