using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication1.Models;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.Web.Http.Cors;

namespace WebApplication1.Controllers
{
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    public class EmployeeController : ApiController
    {
        [System.Web.Mvc.HttpGet]
        //[ActionName("GetEmployeeByID")]
        public List<EmployeeModel>  Get(int id)
        {
           
            EmployeeModel objuser = new EmployeeModel();
            DataSet ds = new DataSet();
            List<EmployeeModel> userlist = new List<EmployeeModel>();
            using (SqlConnection con = new SqlConnection("Data Source=(local);Initial Catalog=SDMSampleMO;User ID=webuser;Password=23hammer;Persist Security Info=True;Min Pool Size=100;"))
            {
                using (SqlCommand cmd = new SqlCommand("sp_readPeopleById", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@lngid", id);
                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                    
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        EmployeeModel uobj = new EmployeeModel();
                        uobj.lngidId = Convert.ToInt32(ds.Tables[0].Rows[i]["lngId"].ToString());
                        uobj.strFirstName = ds.Tables[0].Rows[i]["strFirstName"].ToString();
                        uobj.strLastName = ds.Tables[0].Rows[i]["strLastName"].ToString();
                        userlist.Add(uobj);
                    }
                    //objuser.usersinfo = userlist;
                }
                con.Close();
            }

            return (userlist);
            //return Json(JsonConvert.SerializeObject(userlist));
        }
        [System.Web.Mvc.HttpPost]
        public void Post(EmployeeModel employee) {
            using (SqlConnection con = new SqlConnection("Data Source=(local);Initial Catalog=SDMSampleMO;User ID=webuser;Password=23hammer;Persist Security Info=True;Min Pool Size=100;")) {
                using (SqlCommand cmd = new SqlCommand("sp_UpdatePeopleById", con)) {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@lngid", employee.lngidId);
                    cmd.Parameters.AddWithValue("@strFirstName", employee.strFirstName);
                    cmd.Parameters.AddWithValue("@strLastName", employee.strLastName);
                    con.Open();
                    int rowInserted = cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }
    }
}
