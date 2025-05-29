using EwizProcure.GlobalUtilities;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace EwizProcure.DataAccess
{
    public class CommonDA
    {
        /// <summary>
        /// returns specified object by reading SqlDataReader instance
        /// </summary>
        /// <param name="readerInstance">SqlDataReader value</param>
        /// <returns>object</returns>
        public static object fillObject(SqlDataReader readerInstance, List<string> lstColNames, string objectName)
        {
            object objectInstance = Activator.CreateInstance(Type.GetType(objectName));
            try
            {
                Type t = Type.GetType(objectName);
                foreach (var item in lstColNames)
                {
                    PropertyInfo fi = t.GetProperty(item, BindingFlags.SetProperty | BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                    object value = null;
                    object safeValue = null;
                    if (fi != null)
                    {
                        try
                        {
                            Type t1 = Nullable.GetUnderlyingType(fi.PropertyType) ?? fi.PropertyType;
                            safeValue = (readerInstance[item] is DBNull) ? null : Convert.ChangeType(readerInstance[item], t1);
                            //value = Convert.ChangeType(readerInstance[item], t1);
                        }
                        catch (InvalidCastException ex)
                        {
                            Exceptions.WriteExceptionLog(ex);
                        }
                        fi.SetValue(objectInstance, safeValue, null);
                    }
                }
            }
            catch (Exception ex)
            {
                Exceptions.WriteExceptionLog(ex);
            }
            return objectInstance;
        }

        /// <summary>
        /// Author : Martand Singh
        /// Date : 20/oct/2017
        /// Scope : Returns specified object list by reading SqlDataReader instance
        /// </summary>
        /// <typeparam name="T">Generic type parameter(only class types)</typeparam>
        /// <param name="readerInstance">SqlDataReader instance</param>
        /// <param name="lstColNames">List of all the column names</param>
        /// <returns>Returns a List<T> - List of generic class type</returns>
        public static List<T> fillObjectList<T>(SqlDataReader readerInstance, List<string> lstColNames) where T : class
        {
            List<T> list = new List<T>();
            try
            {
                Type t = typeof(T);
                while (readerInstance.Read())
                {
                    object objectInstance = Activator.CreateInstance(t);
                    foreach (var item in lstColNames)
                    {
                        PropertyInfo fi = t.GetProperty(item, BindingFlags.SetProperty | BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                        object safeValue = null;
                        if (fi != null)
                        {
                            try
                            {
                                Type t1 = Nullable.GetUnderlyingType(fi.PropertyType) ?? fi.PropertyType;
                                safeValue = (readerInstance[item] is DBNull) ? null : Convert.ChangeType(readerInstance[item], t1);
                                //value = Convert.ChangeType(readerInstance[item], t1);
                            }
                            catch (InvalidCastException ex)
                            {
                            }
                            fi.SetValue(objectInstance, safeValue, null);
                        }
                    }
                    list.Add((T)objectInstance);
                }
            }
            catch (Exception ex)
            {
                Exceptions.WriteExceptionLog(ex);
            }
            return list;
        }

        /// <summary>
        /// Author : Martand Singh
        /// Date : 20/oct/2017
        /// Scope : Returns specified object list by reading SqlDataReader instance & Clear used list and clear the datatable schema
        /// </summary>
        /// <typeparam name="T">Generic type parameter(only class types)</typeparam>
        /// <param name="ReaderInstance">SqlDataReader instance</param>
        /// <param name="lstColNames">List of all the column names</param>
        /// <returns></returns>
        public static List<T> FillObjectListComplete<T>(SqlDataReader ReaderInstance, List<string> lstColNames)
            where T : class
        {
            List<T> listObject = new List<T>();
            try
            {
                DataTable dtSchemaTable = ReaderInstance.GetSchemaTable();
                foreach (DataRow drRow in dtSchemaTable.Rows)
                {
                    lstColNames.Add(Convert.ToString(drRow["columnname"]).ToLower(new System.Globalization.CultureInfo("en-US", false)));
                }
                listObject = CommonDA.fillObjectList<T>(ReaderInstance, lstColNames);
                dtSchemaTable.Dispose();
                lstColNames.Clear();
            }
            catch (Exception ex)
            {
                Exceptions.WriteExceptionLog(ex);
            }
            return listObject;
        }
        /*
         
        /// <summary>
        /// Author  : Anoop Gupta
        /// Date    : 26/02/2016
        /// Scope   : Gets Collection based on Type Passed
        /// </summary>
        /// <returns>T</returns> 
        public static List<T> getCollectionItem<T>(string spName, Dictionary<string, string> param)
        {
            List<T> objectInstance = null;
            string connectionString = string.Empty;
            try
            {
                connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

                using (SqlConnection ConnectionInstance = new SqlConnection(connectionString))
                {
                    using (SqlCommand CommandInstance = new SqlCommand(spName, ConnectionInstance))
                    {
                        CommandInstance.CommandTimeout = 500; // Convert.ToInt32(TimeOutSetting.SetTime);
                        CommandInstance.CommandType = CommandType.StoredProcedure;
                        if (param != null)
                        {
                            foreach (KeyValuePair<string, string> para in param)
                            {
                                CommandInstance.Parameters.AddWithValue(string.Format("@{0}", para.Key), para.Value);
                            }
                        }
                        ConnectionInstance.Open();
                        using (SqlDataReader ReaderInstance = CommandInstance.ExecuteReader())
                        {
                            if (ReaderInstance.HasRows)
                            {
                                DataTable dtSchemaTable = ReaderInstance.GetSchemaTable();
                                List<string> lstColNames = new List<string>();

                                foreach (DataRow drRow in dtSchemaTable.Rows)
                                {
                                    lstColNames.Add(Convert.ToString(drRow["columnname"]).ToLower(new System.Globalization.CultureInfo("en-US", false)));
                                }

                                objectInstance = new List<T>();
                                while (ReaderInstance.Read())
                                {
                                    objectInstance.Add((T)fillObject(ReaderInstance, lstColNames, typeof(T).ToString()));
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Exceptions.WriteExceptionLog(ex);
                throw ex;
                //Dictionary<string, string> ErrorCode = new Dictionary<string, string>();
                //foreach (KeyValuePair<string, string> para in ErrorCode)
                //{
                //    ErrorCode.Add(para.Key, Convert.ToString(ex.InnerException));
                //}
                //objectInstance = new List<T>();
                //objectInstance = (ex.InnerException.ToString()).ToList<>;
            }
            return objectInstance;
        }

        /// <summary>
        /// Author  : Sheena Trivedi
        /// Date    : 03/08/2016
        /// Scope   : Update Table data and Gets Collection based on Type Passed
        /// </summary>
        /// <returns>T</returns> 
        public static List<T> getCollectionItem<T>(string spName, List<SqlParameter> param)
        {
            List<T> objectInstance = null;
            string connectionString = string.Empty;
            try
            {
                connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

                using (SqlConnection ConnectionInstance = new SqlConnection(connectionString))
                {
                    using (SqlCommand CommandInstance = new SqlCommand(spName, ConnectionInstance))
                    {
                        CommandInstance.CommandTimeout = 500;
                        CommandInstance.CommandType = CommandType.StoredProcedure;
                        if (param != null)
                        {
                            foreach (SqlParameter para in param)
                            {
                                CommandInstance.Parameters.AddWithValue(string.Format("@{0}", para.ParameterName), para.Value);
                            }
                        }
                        ConnectionInstance.Open();
                        using (SqlDataReader ReaderInstance = CommandInstance.ExecuteReader())
                        {
                            if (ReaderInstance.HasRows)
                            {
                                DataTable dtSchemaTable = ReaderInstance.GetSchemaTable();
                                List<string> lstColNames = new List<string>();

                                foreach (DataRow drRow in dtSchemaTable.Rows)
                                {
                                    lstColNames.Add(Convert.ToString(drRow["columnname"]).ToLower(new System.Globalization.CultureInfo("en-US", false)));
                                }

                                objectInstance = new List<T>();
                                while (ReaderInstance.Read())
                                {
                                    objectInstance.Add((T)fillObject(ReaderInstance, lstColNames, typeof(T).ToString()));
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Exceptions.WriteExceptionLog(ex);
                throw ex;
            }
            return objectInstance;
        }


        /// <summary>
        /// Author  : Sheena Trivedi
        /// Date    : 29/09/2016
        /// Scope   : Gets Collection
        /// </summary>
        /// <returns>T</returns> 
        public static DataSet getDynamicCollectionItems(string spName, List<SqlParameter> param)
        {
            DataSet dataSet = new DataSet();
            string connectionString = string.Empty;
            try
            {
                connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

                using (SqlConnection ConnectionInstance = new SqlConnection(connectionString))
                {
                    using (SqlCommand CommandInstance = new SqlCommand(spName, ConnectionInstance))
                    {
                        CommandInstance.CommandTimeout = 500;
                        CommandInstance.CommandType = CommandType.StoredProcedure;
                        if (param != null)
                        {
                            foreach (SqlParameter para in param)
                            {
                                CommandInstance.Parameters.AddWithValue(string.Format("@{0}", para.ParameterName), para.Value);
                            }
                        }
                        ConnectionInstance.Open();
                        SqlDataAdapter da = new SqlDataAdapter(CommandInstance);
                        da.Fill(dataSet);
                        da.Dispose();
                    }
                }
            }
            catch (Exception ex)
            {
                Exceptions.WriteExceptionLog(ex);
                throw ex;
            }
            return dataSet;
        }

        ///Author : Sripal
        public static object getItem(string spName, Dictionary<string, string> param, string objectName)
        {
            object objectInstance = null;
            string connectionString = string.Empty;
            try
            {
                connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;


                using (SqlConnection ConnectionInstance = new SqlConnection(connectionString))
                {
                    using (SqlCommand CommandInstance = new SqlCommand(spName, ConnectionInstance))
                    {
                        CommandInstance.CommandTimeout = 500;
                        CommandInstance.CommandType = CommandType.StoredProcedure;
                        if (param != null)
                        {
                            foreach (KeyValuePair<string, string> para in param)
                            {
                                CommandInstance.Parameters.AddWithValue(string.Format("@{0}", para.Key), para.Value);
                            }
                        }

                        ConnectionInstance.Open();
                        using (SqlDataReader ReaderInstance = CommandInstance.ExecuteReader())
                        {
                            if (ReaderInstance.HasRows)
                            {
                                DataTable dtSchemaTable = ReaderInstance.GetSchemaTable();
                                List<string> lstColNames = new List<string>();

                                foreach (DataRow drRow in dtSchemaTable.Rows)
                                {
                                    lstColNames.Add(Convert.ToString(drRow["columnname"]).ToLower(new System.Globalization.CultureInfo("en-US", false)));
                                }

                                while (ReaderInstance.Read())
                                {
                                    objectInstance = fillObject1(ReaderInstance, lstColNames, objectName);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Exceptions.WriteExceptionLog(ex);
                //ErrorManager.AddExceptionToDB(string.Format("{0} --> {1}", MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name), ex.Message, ex.StackTrace);
            }
            return objectInstance;
        }



        public static object fillObject1(SqlDataReader readerInstance, List<string> lstColNames, string objectName)
        {
            object objectInstance = Activator.CreateInstance(Type.GetType(objectName));
            try
            {
                Type t = Type.GetType(objectName);
                foreach (var item in lstColNames)
                {
                    PropertyInfo fi = t.GetProperty(item, BindingFlags.SetProperty | BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                    object value = null;
                    if (fi != null)
                    {
                        try
                        {
                            value = Convert.ChangeType(readerInstance[item], fi.PropertyType);
                        }
                        catch (InvalidCastException ex)
                        {
                            Exceptions.WriteExceptionLog(ex);
                        }
                        fi.SetValue(objectInstance, value, null);
                    }
                }
            }
            catch (Exception ex)
            {
                Exceptions.WriteExceptionLog(ex);
                //ErrorManager.AddExceptionToDB(string.Format("{0} --> {1}", MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name), ex.Message, ex.StackTrace);
            }
            return objectInstance;
        }
        public static List<T> getCollectionItemDataTable<T>(string spName, Dictionary<string, object> param)
        {
            List<T> objectInstance = null;
            string connectionString = string.Empty;
            try
            {
                connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

                using (SqlConnection ConnectionInstance = new SqlConnection(connectionString))
                {
                    using (SqlCommand CommandInstance = new SqlCommand(spName, ConnectionInstance))
                    {
                        CommandInstance.CommandTimeout = 500; // Convert.ToInt32(TimeOutSetting.SetTime);
                        CommandInstance.CommandType = CommandType.StoredProcedure;
                        if (param != null)
                        {
                            foreach (KeyValuePair<string, object> para in param)
                            {
                                CommandInstance.Parameters.AddWithValue(string.Format("@{0}", para.Key), para.Value);
                            }
                        }
                        ConnectionInstance.Open();
                        using (SqlDataReader ReaderInstance = CommandInstance.ExecuteReader())
                        {
                            if (ReaderInstance.HasRows)
                            {
                                DataTable dtSchemaTable = ReaderInstance.GetSchemaTable();
                                List<string> lstColNames = new List<string>();

                                foreach (DataRow drRow in dtSchemaTable.Rows)
                                {
                                    lstColNames.Add(Convert.ToString(drRow["columnname"]).ToLower(new System.Globalization.CultureInfo("en-US", false)));
                                }

                                objectInstance = new List<T>();
                                while (ReaderInstance.Read())
                                {
                                    objectInstance.Add((T)fillObject(ReaderInstance, lstColNames, typeof(T).ToString()));
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Exceptions.WriteExceptionLog(ex);
                throw ex;
            }
            return objectInstance;
        }

        /// <summary>
        /// Author : Martand Singh
        /// Date : 8 Jan 2018
        /// Scope : Return number in words format
        /// </summary>
        /// <param name="inputNumber"></param>
        /// <returns></returns>
        public static string ConvertNumbertoWords(int inputNumber)
        {
            int inputNo = inputNumber;
            if (inputNo == 0)
                return "Zero";
            int[] numbers = new int[4];
            int first = 0;
            int u, h, t;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            if (inputNo < 0)
            {
                sb.Append("Minus ");
                inputNo = -inputNo;
            }
            string[] words0 = {"" ,"One ", "Two ", "Three ", "Four ",
            "Five " ,"Six ", "Seven ", "Eight ", "Nine "};
            string[] words1 = {"Ten ", "Eleven ", "Twelve ", "Thirteen ", "Fourteen ",
            "Fifteen ","Sixteen ","Seventeen ","Eighteen ", "Nineteen "};
            string[] words2 = {"Twenty ", "Thirty ", "Forty ", "Fifty ", "Sixty ",
            "Seventy ","Eighty ", "Ninety "};
            string[] words3 = { "Thousand ", "Lakh ", "Crore " };
            numbers[0] = inputNo % 1000; // units
            numbers[1] = inputNo / 1000;
            numbers[2] = inputNo / 100000;
            numbers[1] = numbers[1] - 100 * numbers[2]; // thousands
            numbers[3] = inputNo / 10000000; // crores
            numbers[2] = numbers[2] - 100 * numbers[3]; // lakhs
            for (int i = 3; i > 0; i--)
            {
                if (numbers[i] != 0)
                {
                    first = i;
                    break;
                }
            }
            for (int i = first; i >= 0; i--)
            {
                if (numbers[i] == 0) continue;
                u = numbers[i] % 10; // ones
                t = numbers[i] / 10;
                h = numbers[i] / 100; // hundreds
                t = t - 10 * h; // tens
                if (h > 0) sb.Append(words0[h] + "Hundred ");
                if (u > 0 || t > 0)
                {
                    if ((h > 0 || i == 0) && inputNumber > 100) sb.Append("and ");
                    if (t == 0)
                        sb.Append(words0[u]);
                    else if (t == 1)
                        sb.Append(words1[u]);
                    else
                        sb.Append(words2[t - 2] + words0[u]);
                }
                if (i != 0) sb.Append(words3[i - 1]);
            }
            return sb.ToString().TrimEnd();
        }

        public static object ExecuteNonQuery(string spName, SqlParameter[] sqlParam, string sqlOutParam)
        {
            SqlCommand sqlCmd;
            SqlConnection sqlConn;
            string connectionString = string.Empty;
            int m_setCommandTimeOut = 0;

            connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            sqlConn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(spName, sqlConn);
            cmd.CommandTimeout = m_setCommandTimeOut;
            cmd.CommandType = CommandType.StoredProcedure;
            object retval;
            try
            {
                foreach (SqlParameter param in sqlParam)
                {
                    cmd.Parameters.Add(param);
                }
                cmd.Parameters[sqlOutParam].Direction = ParameterDirection.Output;
                if (sqlConn.State != ConnectionState.Open)
                    sqlConn.Open();
                cmd.ExecuteNonQuery();

                // Get the values
                retval = (object)cmd.Parameters[sqlOutParam].Value;
            }

            catch (Exception ex)
            {
                Exceptions.WriteExceptionLog(ex);
                retval = 0;
            }
            finally
            {
                sqlConn.Close();
                cmd.Dispose();
            }
            return retval;
        }


        /// <summary>
        /// Author : Babitha Bangera
        /// Date : 10/05/2021
        /// Scope : process the CSV to different database
        /// </summary> 
        public static object ExecuteNonQueryNewDB(string spName, SqlParameter[] sqlParam, string sqlOutParam, string CoupaDB)
        {
            SqlCommand sqlCmd;
            SqlConnection sqlConn;
            string connectionString = string.Empty;
            int m_setCommandTimeOut = 0;

            connectionString = CoupaDB;// ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            Exceptions.WriteInfoLog(connectionString);
            sqlConn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(spName, sqlConn);
            cmd.CommandTimeout = m_setCommandTimeOut;
            cmd.CommandType = CommandType.StoredProcedure;
            object retval;
            try
            {
                foreach (SqlParameter param in sqlParam)
                {
                    cmd.Parameters.Add(param);
                }
                cmd.Parameters[sqlOutParam].Direction = ParameterDirection.Output;
                if (sqlConn.State != ConnectionState.Open)
                    sqlConn.Open();
                cmd.ExecuteNonQuery();
                Exceptions.WriteInfoLog("Executed Successfully - ExecuteNonQueryNewDB");
                // Get the values
                retval = (object)cmd.Parameters[sqlOutParam].Value;
            }

            catch (Exception ex)
            {
                Exceptions.WriteExceptionLog(ex);
                Exceptions.WriteInfoLog("Error in ExecuteNonQueryNewDB");
                Exceptions.WriteInfoLog(ex.ToString());
                retval = 0;
            }
            finally
            {
                sqlConn.Close();
                cmd.Dispose();
            }
            return retval;
        }


        /// <summary>
        /// Author : Babitha Bangera
        /// Date : 10/05/2021
        /// Scope : process the CSV to different database
        /// </summary> 
        public static void ExecuteNonQueryNewDB(string spName, SqlParameter[] sqlParam, string CoupaDB)
        {
            SqlCommand sqlCmd;
            SqlConnection sqlConn;
            string connectionString = string.Empty;
            int m_setCommandTimeOut = 0;

            connectionString = CoupaDB;// ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            sqlConn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(spName, sqlConn);
            cmd.CommandTimeout = m_setCommandTimeOut;
            cmd.CommandType = CommandType.StoredProcedure;
            object retval;
            try
            {
                foreach (SqlParameter param in sqlParam)
                {
                    cmd.Parameters.Add(param);
                }

                if (sqlConn.State != ConnectionState.Open)
                    sqlConn.Open();
                cmd.ExecuteNonQuery();
            }

            catch (Exception ex)
            {
                Exceptions.WriteExceptionLog(ex);
            }
            finally
            {
                sqlConn.Close();
                cmd.Dispose();
            }
        }

        /// <summary>
        /// Author : Babitha Bangera
        /// Date : 01/11/2018
        /// Scope : process the CSV and writes to OutParm incase of any error
        /// </summary>                
        public static object ExecuteNonQuery1(string spName, List<SqlParameter> param, string sqlOutParam)
        {
            DataSet dataSet = new DataSet();
            string connectionString = string.Empty;
            object retval;
            try
            {
                connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

                using (SqlConnection ConnectionInstance = new SqlConnection(connectionString))
                {
                    using (SqlCommand CommandInstance = new SqlCommand(spName, ConnectionInstance))
                    {
                        CommandInstance.CommandTimeout = 500;
                        CommandInstance.CommandType = CommandType.StoredProcedure;
                        if (param != null)
                        {
                            foreach (SqlParameter para in param)
                            {
                                CommandInstance.Parameters.AddWithValue(string.Format("@{0}", para.ParameterName), para.Value);
                            }
                            CommandInstance.Parameters[sqlOutParam].Direction = ParameterDirection.Output;
                        }
                        ConnectionInstance.Open();
                        SqlDataAdapter da = new SqlDataAdapter(CommandInstance);
                        da.Fill(dataSet);
                        retval = (object)CommandInstance.Parameters[sqlOutParam].Value;
                        da.Dispose();
                    }
                }
            }

            catch (Exception ex)
            {
                retval = 0;
                Exceptions.WriteCoupaCSVInfoLog(ex.ToString());
            }
            return retval;
        }

        public static object ExecuteScalar(string spName, SqlParameter[] sqlParam)
        {
            SqlCommand sqlCmd;
            SqlConnection sqlConn;
            string connectionString = string.Empty;
            int m_setCommandTimeOut = 0;
            connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            sqlConn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(spName, sqlConn);
            cmd.CommandTimeout = m_setCommandTimeOut;
            cmd.CommandType = CommandType.StoredProcedure;
            object retval;
            try
            {
                foreach (SqlParameter param in sqlParam)
                {
                    cmd.Parameters.Add(param);
                }

                if (sqlConn.State != ConnectionState.Open)
                    sqlConn.Open();
                retval = cmd.ExecuteScalar();
            }

            catch (Exception ex)
            {
                Exceptions.WriteExceptionLog(ex);
                retval = 0;
            }
            finally
            {
                sqlConn.Close();
                cmd.Dispose();
            }
            return retval;
        }



        public static DataTable ExecuteDataTableOutParm(string spName, SqlParameter[] sqlParam, string sqlOutParam)
        {
            SqlCommand sqlCmd;
            SqlConnection sqlConn;
            DataTable dt = new DataTable();
            string connectionString = string.Empty;
            int m_setCommandTimeOut = 0;

            connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            sqlConn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(spName, sqlConn);
            cmd.CommandTimeout = m_setCommandTimeOut;
            cmd.CommandType = CommandType.StoredProcedure;
            object retval;

            try
            {
                if (sqlParam != null)
                {
                    foreach (SqlParameter param in sqlParam)
                    {
                        cmd.Parameters.Add(param);
                    }
                }

                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }

                // Get the values
                retval = (object)cmd.Parameters[sqlOutParam].Value;
            }


            catch (Exception ex)
            {
                Exceptions.WriteExceptionLog(ex);
                retval = 0;
            }
            finally
            {
                sqlConn.Close();
                cmd.Dispose();
            }
            return dt;
        }

        public static DataTable ExecuteDataTable(string spName, SqlParameter[] sqlParam)
        {
            SqlCommand sqlCmd;
            SqlConnection sqlConn;
            string connectionString = string.Empty;
            int m_setCommandTimeOut = 0;
            DataTable dt = new DataTable();
            connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            sqlConn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(spName, sqlConn);
            cmd.CommandTimeout = m_setCommandTimeOut;
            cmd.CommandType = CommandType.StoredProcedure;
            object retval;
            try
            {
                if (sqlParam != null)
                {
                    foreach (SqlParameter param in sqlParam)
                    {
                        cmd.Parameters.Add(param);
                    }
                }

                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Exceptions.WriteExceptionLog(ex);
                retval = 0;
            }
            finally
            {
                sqlConn.Close();
                cmd.Dispose();
            }
            return dt;
        }
*/

    }
}
