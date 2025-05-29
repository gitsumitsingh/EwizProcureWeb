using EwizProcure.Interfaces;
using EwizProcure.Models;
using System.Data.SqlClient;
using EwizProcure.Interfaces;
using EwizProcure.GlobalUtilities;

namespace EwizProcure.DataAccess
{
    public class LoginDA : ILoginDA
    {
        // GLobal Readonly connection string
        private readonly string __ConnectionString = "";
        public LoginDA(IConfiguration config)
        {
            __ConnectionString = config.GetSection("ConnectionStrings:DefaultConnection").Value;
        }
        //private readonly string __ConnectionString = Convert.ToString(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);


        /// <summary>
        /// Author : Sumit Singh
        /// Date : 29/05/2025
        /// Scope : Get Userlist
        /// </summary>
        /// <param name="EmailId"></param>
        /// <returns></returns>

        public List<LoginDetailViewModels> GetUserList(string EmailId)
        {
            List<LoginDetailViewModels> LoginDetails = new List<LoginDetailViewModels>();
            try
            {
                using (SqlConnection ConObject = new SqlConnection(__ConnectionString))
                {
                    using (SqlCommand CmdObject = new SqlCommand("USP_GetUserList2", ConObject))
                    {
                        CmdObject.CommandType = System.Data.CommandType.StoredProcedure;
                        CmdObject.CommandTimeout = 500;
                        CmdObject.Parameters.AddWithValue("@EmailId", null);
                        //If connection is close then open.
                        if (ConObject.State == System.Data.ConnectionState.Closed)
                        {
                            ConObject.Open();
                        }
                        SqlDataReader oReader = CmdObject.ExecuteReader();
                        if (oReader != null)
                        {
                            if (oReader.HasRows)
                            {
                                List<LoginDetailViewModels> Result = CommonDA.FillObjectListComplete<LoginDetailViewModels>(oReader, new List<string>());
                                if (Result != null && Result.Count > 0) //Null Check and Count Check
                                {
                                    LoginDetails = Result;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Exceptions.WriteExceptionLog(ex);
            }
            return LoginDetails;
        }

    }
}
