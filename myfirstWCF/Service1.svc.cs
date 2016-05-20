using myfirstWCF.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace myfirstWCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.

   [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Single,
       InstanceContextMode = InstanceContextMode.PerCall)]
    public class Service1 : IService1
    {
        string connectionString = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

        public TvShows DisplayTvShows()
        {
            TvShows AllShows = new TvShows();

            //Define some variable to store data temporarily
            var tvId = new List<int>();
            var TvShows = new List<string>();
            var Ratings = new List<int>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    string Query = "SELECT * FROM JustGetCodes.dbo.My_TvShows";
                    SqlCommand cmd = new SqlCommand(Query, con);
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandTimeout = 0;

                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            //Make Sure that the columns heading are same as in the SQL Table

                            tvId.Add(Convert.ToInt32(sdr["TVID"]));
                            TvShows.Add(sdr["TV_Show_Name"].ToString());
                            Ratings.Add(Convert.ToInt32((sdr["Show_Ratings"])));
                        }
                    }

                    //Attach the Lists to the Model

                    AllShows.TVID = tvId.ToArray();
                    AllShows.TvShowsName = TvShows.ToArray();
                    AllShows.Ratings = Ratings.ToArray();
                }

                catch (Exception ex)
                {
                    throw ex;
                   
                }

                //Close the database connection
                con.Close();
            }

            //Return the list of Tv Shows
            return AllShows;
        }
    }
}
