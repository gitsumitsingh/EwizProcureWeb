using EwizProcureWeb.Interfaces;
using Newtonsoft.Json;

namespace EwizProcureWeb.GlobalUtilities
{
    public class GlobalFunctions
    {
        #region Declarations
#if DEBUG
        private static string PhysicalApplicationPath = "";
#else
    private static string PhysicalApplicationPath =  HttpContext.Current.Server.MapPath(HttpContext.Current.Request.ApplicationPath);
#endif
        private static IDictionary<string, IGlobalSettings> _roleGlobalSettings = new Dictionary<string, IGlobalSettings>(2);
        public static IDictionary<string, IGlobalSettings> GlobalSettings
        {
            get
            {
                return _roleGlobalSettings;
            }
        }
        #endregion

        public static string DefaultKey
        {
            get
            {
                return "DefaultSetting";
            }
        }

        public static string ChinaKey
        {
            get
            {
                return "ChinaSetting";
            }
        }

        /// <summary>
        /// Author  : Anoop Gupta
        /// Date    : 07-01-16
        /// Scope   :Physical path of application
        /// </summary>
        /// <returns>Physical path of application</returns>
        public static string GetPhysicalFolderPath()
        {
            return PhysicalApplicationPath + "\\";
        }

        ///// <summary>
        ///// Author  : Anoop Gupta
        ///// Date    : 07-01-16
        ///// Scope   : Returns a string representing application's virtual path
        ///// </summary>
        ///// <returns>Virtual url of the link</returns>
        ///// <remarks></remarks>
        //public static string GetVirtualPath()
        //{
        //    try
        //    {
        //        if (!string.IsNullOrEmpty(Convert.ToString(ConfigurationManager.AppSettings["host"])))
        //            return Convert.ToString(ConfigurationManager.AppSettings["host"]);
        //        else
        //            return HttpContext.Current.Request.Url.Scheme + Uri.SchemeDelimiter + HttpContext.Current.Request.Url.Authority; // + "/";
        //    }
        //    catch (Exception ex)
        //    {
        //        Exceptions.WriteExceptionLog(ex);
        //        throw ex;
        //    }
        //}

        /// <summary>
        /// Author  : Anoop Gupta
        /// Date    : 14-01-16
        /// Scope   : Serialize object to JSON
        /// </summary>
        /// <returns>Serialized string</returns>
        public static string GetSerializedData(object obj)
        {
            try
            {
                //JavaScriptSerializer serializer = new JavaScriptSerializer();
                //return serializer.Serialize(obj);

                //object o = JsonConvert.DeserializeObject(obj);
                return JsonConvert.SerializeObject(obj, Formatting.Indented);
            }
            catch (Exception ex)
            {
                //throw ex;
                Exceptions.WriteExceptionLog(ex);
            }
            return "";
        }

    }
}
