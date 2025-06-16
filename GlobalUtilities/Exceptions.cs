namespace EwizProcureWeb.GlobalUtilities
{

    public static class Exceptions
    {
        #region WRITEEXCEPTIONLOG

        /// <summary>
        /// Method to write application wide exception log. 
        /// </summary>
        /// <param name="ex">Object of Class Exception</param>
        public static void WriteExceptionLog(Exception ex)
        {
            System.Threading.ThreadAbortException exception = ex as System.Threading.ThreadAbortException;
            if (exception == null)
            {
                string ExceptionLogFolderPath = GlobalFunctions.GetPhysicalFolderPath() + "ExceptionLogs";
                try
                {
                    if (!Directory.Exists(ExceptionLogFolderPath))
                        Directory.CreateDirectory(ExceptionLogFolderPath);

                    if (Directory.Exists(ExceptionLogFolderPath))
                    {
                        //Create month wise exception log file.
                        string date = string.Format("{0:dd}", DateTime.Now);
                        string month = string.Format("{0:MMM}", DateTime.Now);
                        string year = string.Format("{0:yyyy}", DateTime.Now);

                        string folderName = month + year; //Feb2013
                        string monthFolder = ExceptionLogFolderPath + "\\" + folderName;
                        if (!Directory.Exists(monthFolder))
                            Directory.CreateDirectory(monthFolder);

                        string ExceptionLogFileName = monthFolder +
                            "\\ExceptionLog_" + date + month + ".txt"; //ExceptionLog_04Feb.txt

                        using (System.IO.StreamWriter strmWriter = new System.IO.StreamWriter(ExceptionLogFileName, true))
                        {
                            strmWriter.WriteLine("On " + DateTime.Now.ToString() +
                                ", following error occured in the application:");
                            strmWriter.WriteLine("Message: ex.Message - display");
                            strmWriter.WriteLine("Message: " + ex.Message);
                            //strmWriter.WriteLine("Inner Exception: " + ex.InnerException.Message);
                            //strmWriter.WriteLine("Inner Exception(2): " + ex.InnerException.InnerException.Message);
                            strmWriter.WriteLine("Message: ex.Source - display");
                            strmWriter.WriteLine("Source: " + ex.Source);
                            strmWriter.WriteLine("Message: ex.StackTrace - display");
                            strmWriter.WriteLine("Stack Trace: " + ex.StackTrace);
                            strmWriter.WriteLine("Message: ex.HelpLink - display");
                            strmWriter.WriteLine("HelpLink: " + ex.HelpLink);
                            strmWriter.WriteLine("-------------------------------------------------------------------------------");
                        }
                    }
                    else
                        throw new DirectoryNotFoundException("Exception log folder not found in the specified path");
                }
                catch
                {

                }
            }
        }

        #endregion WRITEEXCEPTIONLOG

        #region WRITEINFOLOG
        /// <summary>
        /// Method to write application wide Information log. 
        /// </summary>
        /// <param name="ex">string message parameter</param>
        public static void WriteInfoLog(string message)
        {
            string ExceptionLogFolderPath = GlobalFunctions.GetPhysicalFolderPath() + "ExceptionLogs";
            try
            {
                if (!Directory.Exists(ExceptionLogFolderPath))
                    Directory.CreateDirectory(ExceptionLogFolderPath);

                if (Directory.Exists(ExceptionLogFolderPath))
                {
                    //Create month wise exception log file.
                    string date = string.Format("{0:dd}", DateTime.Now);
                    string month = string.Format("{0:MMM}", DateTime.Now);
                    string year = string.Format("{0:yyyy}", DateTime.Now);

                    string folderName = month + year; //Feb2013
                    string monthFolder = ExceptionLogFolderPath + "\\" + folderName;
                    if (!Directory.Exists(monthFolder))
                        Directory.CreateDirectory(monthFolder);

                    string ExceptionLogFileName = monthFolder +
                        "\\InfoLog_" + date + month + ".txt"; //ExceptionLog_04Feb.txt

                    using (System.IO.StreamWriter strmWriter = new System.IO.StreamWriter(ExceptionLogFileName, true))
                    {
                        strmWriter.WriteLine("On " + DateTime.Now.ToString() + ",");
                        strmWriter.WriteLine("Message: " + message);
                        strmWriter.WriteLine("-------------------------------------------------------------------------------");
                    }
                }
                else
                    throw new DirectoryNotFoundException("Exception log folder not found in the specified path");
            }
            catch
            {

            }
        }

        #endregion

        #region WritePunchout
        /// <summary>
        /// Method to write application wide Information log of Punchout InfoLog. 
        /// </summary>
        /// <param name="ex">string message parameter</param>
        public static void WritePunchoutInfoLog(string message)
        {
            string ExceptionLogFolderPath = GlobalFunctions.GetPhysicalFolderPath() + "Punchout";
            try
            {
                if (!Directory.Exists(ExceptionLogFolderPath))
                    Directory.CreateDirectory(ExceptionLogFolderPath);

                if (Directory.Exists(ExceptionLogFolderPath))
                {
                    //Create month wise exception log file.
                    string date = string.Format("{0:dd}", DateTime.Now);
                    string month = string.Format("{0:MMM}", DateTime.Now);
                    string year = string.Format("{0:yyyy}", DateTime.Now);

                    string folderName = month + year; //Feb2013
                    string monthFolder = ExceptionLogFolderPath + "\\" + folderName;
                    if (!Directory.Exists(monthFolder))
                        Directory.CreateDirectory(monthFolder);

                    string ExceptionLogFileName = monthFolder +
                        "\\PunchoutInfo_" + date + month + ".txt"; //ExceptionLog_04Feb.txt

                    using (System.IO.StreamWriter strmWriter = new System.IO.StreamWriter(ExceptionLogFileName, true))
                    {
                        strmWriter.WriteLine("On " + DateTime.Now.ToString() + ",");
                        strmWriter.WriteLine("Message: ");
                        strmWriter.WriteLine(message);
                        strmWriter.WriteLine("-------------------------------------------------------------------------------");
                    }
                }
                else
                    throw new DirectoryNotFoundException("Punchout log folder not found in the specified path");
            }
            catch
            {

            }
        }

        /// <summary>
        /// Method to write application wide Information log of Request Punchout. 
        /// </summary>
        /// <param name="ex">string message parameter</param>
        public static void WritePunchoutRequestLog(string message)
        {
            string ExceptionLogFolderPath = GlobalFunctions.GetPhysicalFolderPath() + "Punchout";
            try
            {
                if (!Directory.Exists(ExceptionLogFolderPath))
                    Directory.CreateDirectory(ExceptionLogFolderPath);

                if (Directory.Exists(ExceptionLogFolderPath))
                {
                    //Create month wise exception log file.
                    string date = string.Format("{0:dd}", DateTime.Now);
                    string month = string.Format("{0:MMM}", DateTime.Now);
                    string year = string.Format("{0:yyyy}", DateTime.Now);

                    string folderName = month + year; //Feb2013
                    string monthFolder = ExceptionLogFolderPath + "\\" + folderName;
                    if (!Directory.Exists(monthFolder))
                        Directory.CreateDirectory(monthFolder);

                    string ExceptionLogFileName = monthFolder +
                        "\\PunchoutRequest_" + date + month + ".txt"; //ExceptionLog_04Feb.txt

                    using (System.IO.StreamWriter strmWriter = new System.IO.StreamWriter(ExceptionLogFileName, true))
                    {
                        strmWriter.WriteLine("On " + DateTime.Now.ToString() + ",");
                        strmWriter.WriteLine("Message: ");
                        strmWriter.WriteLine(message);
                        strmWriter.WriteLine("-------------------------------------------------------------------------------");
                    }
                }
                else
                    throw new DirectoryNotFoundException("Punchout log folder not found in the specified path");
            }
            catch
            {

            }
        }

        /// <summary>
        /// Method to write application wide Information log of Response Punchout. 
        /// </summary>
        /// <param name="ex">string message parameter</param>
        public static void WritePunchoutResponseLog(string message)
        {
            string ExceptionLogFolderPath = GlobalFunctions.GetPhysicalFolderPath() + "Punchout";
            try
            {
                if (!Directory.Exists(ExceptionLogFolderPath))
                    Directory.CreateDirectory(ExceptionLogFolderPath);

                if (Directory.Exists(ExceptionLogFolderPath))
                {
                    //Create month wise exception log file.
                    string date = string.Format("{0:dd}", DateTime.Now);
                    string month = string.Format("{0:MMM}", DateTime.Now);
                    string year = string.Format("{0:yyyy}", DateTime.Now);

                    string folderName = month + year; //Feb2013
                    string monthFolder = ExceptionLogFolderPath + "\\" + folderName;
                    if (!Directory.Exists(monthFolder))
                        Directory.CreateDirectory(monthFolder);

                    string ExceptionLogFileName = monthFolder +
                        "\\PunchoutResponse_" + date + month + ".txt"; //ExceptionLog_04Feb.txt

                    using (System.IO.StreamWriter strmWriter = new System.IO.StreamWriter(ExceptionLogFileName, true))
                    {
                        strmWriter.WriteLine("On " + DateTime.Now.ToString() + ",");
                        strmWriter.WriteLine("Message: ");
                        strmWriter.WriteLine(message);
                        strmWriter.WriteLine("-------------------------------------------------------------------------------");
                    }
                }
                else
                    throw new DirectoryNotFoundException("Punchout log folder not found in the specified path");
            }
            catch
            {

            }
        }

        #endregion

        public static void WriteCoupaCSVInfoLog(string message)
        {
            string ExceptionLogFolderPath = GlobalFunctions.GetPhysicalFolderPath() + "Coupa";
            try
            {
                if (!Directory.Exists(ExceptionLogFolderPath))
                    Directory.CreateDirectory(ExceptionLogFolderPath);

                if (Directory.Exists(ExceptionLogFolderPath))
                {
                    //Create month wise exception log file.
                    string date = string.Format("{0:dd}", DateTime.Now);
                    string month = string.Format("{0:MMM}", DateTime.Now);
                    string year = string.Format("{0:yyyy}", DateTime.Now);

                    string folderName = month + year; //Feb2013
                    string monthFolder = ExceptionLogFolderPath + "\\" + folderName;
                    if (!Directory.Exists(monthFolder))
                        Directory.CreateDirectory(monthFolder);

                    string ExceptionLogFileName = monthFolder +
                        "\\CSVInfo_" + date + month + ".txt"; //ExceptionLog_04Feb.txt

                    using (System.IO.StreamWriter strmWriter = new System.IO.StreamWriter(ExceptionLogFileName, true))
                    {
                        strmWriter.WriteLine("On " + DateTime.Now.ToString() + ",");
                        strmWriter.WriteLine("Message: ");
                        strmWriter.WriteLine(message);
                        strmWriter.WriteLine("-------------------------------------------------------------------------------");
                    }
                }
                else
                    throw new DirectoryNotFoundException("Punchout log folder not found in the specified path");
            }
            catch
            {

            }
        }

        public static void WriteCapsaInfoLog(string message)
        {
            string ExceptionLogFolderPath = GlobalFunctions.GetPhysicalFolderPath() + "Capsa";
            try
            {
                if (!Directory.Exists(ExceptionLogFolderPath))
                    Directory.CreateDirectory(ExceptionLogFolderPath);

                if (Directory.Exists(ExceptionLogFolderPath))
                {
                    //Create month wise exception log file.
                    string date = string.Format("{0:dd}", DateTime.Now);
                    string month = string.Format("{0:MMM}", DateTime.Now);
                    string year = string.Format("{0:yyyy}", DateTime.Now);

                    string folderName = month + year; //Feb2013
                    string monthFolder = ExceptionLogFolderPath + "\\" + folderName;
                    if (!Directory.Exists(monthFolder))
                        Directory.CreateDirectory(monthFolder);

                    string ExceptionLogFileName = monthFolder +
                        "\\CSVInfo_" + date + month + ".txt"; //ExceptionLog_04Feb.txt

                    using (System.IO.StreamWriter strmWriter = new System.IO.StreamWriter(ExceptionLogFileName, true))
                    {
                        strmWriter.WriteLine("On " + DateTime.Now.ToString() + ",");
                        strmWriter.WriteLine("Message: ");
                        strmWriter.WriteLine(message);
                        strmWriter.WriteLine("-------------------------------------------------------------------------------");
                    }
                }
                else
                    throw new DirectoryNotFoundException("Punchout log folder not found in the specified path");
            }
            catch
            {

            }
        }

        public static void WriteIndexGlobalInfoLog(string message)
        {
            string ExceptionLogFolderPath = GlobalFunctions.GetPhysicalFolderPath() + "ElasticSearch";
            try
            {
                if (!Directory.Exists(ExceptionLogFolderPath))
                    Directory.CreateDirectory(ExceptionLogFolderPath);

                if (Directory.Exists(ExceptionLogFolderPath))
                {
                    //Create month wise exception log file.
                    string date = string.Format("{0:dd}", DateTime.Now);
                    string month = string.Format("{0:MMM}", DateTime.Now);
                    string year = string.Format("{0:yyyy}", DateTime.Now);

                    string folderName = month + year; //Feb2013
                    string monthFolder = ExceptionLogFolderPath + "\\" + folderName;
                    if (!Directory.Exists(monthFolder))
                        Directory.CreateDirectory(monthFolder);

                    string ExceptionLogFileName = monthFolder +
                        "\\GlobalIndex_" + date + month + ".txt"; //ExceptionLog_04Feb.txt

                    using (System.IO.StreamWriter strmWriter = new System.IO.StreamWriter(ExceptionLogFileName, true))
                    {
                        strmWriter.WriteLine("On " + DateTime.Now.ToString() + ",");
                        strmWriter.WriteLine("Message: ");
                        strmWriter.WriteLine(message);
                        strmWriter.WriteLine("-------------------------------------------------------------------------------");
                    }
                }
                else
                    throw new DirectoryNotFoundException("Punchout log folder not found in the specified path");
            }
            catch
            {

            }
        }


        #region WriteBespoke
        /// <summary>
        /// Method to write application wide Information log of Bespoke InfoLog. 
        ///https://www.unimart.co/BespokePoint/Oct2024/BespokePointInfo_16Oct.txt
        /// </summary>
        /// <param name="ex">string message parameter</param>
        public static void WriteBespokeInfoLog(string message)
        {
            string AppFolder = "\\";
            //string AppFolder = HttpContext.Current.Server.MapPath(HttpContext.Current.Request.ApplicationPath) + "\\";
            //string ExceptionLogFolderPath = GlobalFunctions.GetPhysicalFolderPath() + "BespokePoint";
            string ExceptionLogFolderPath = AppFolder + "BespokePoint";
            try
            {
                if (!Directory.Exists(ExceptionLogFolderPath))
                    Directory.CreateDirectory(ExceptionLogFolderPath);

                if (Directory.Exists(ExceptionLogFolderPath))
                {
                    //Create month wise exception log file.
                    string date = string.Format("{0:dd}", DateTime.Now);
                    string month = string.Format("{0:MMM}", DateTime.Now);
                    string year = string.Format("{0:yyyy}", DateTime.Now);

                    string folderName = month + year; //Oct2024
                    string monthFolder = ExceptionLogFolderPath + "\\" + folderName;
                    if (!Directory.Exists(monthFolder))
                        Directory.CreateDirectory(monthFolder);

                    string ExceptionLogFileName = monthFolder +
                        "\\BespokePointInfo_" + date + month + ".txt"; //BespokePointInfo_16Oct.txt

                    using (StreamWriter strmWriter = new StreamWriter(ExceptionLogFileName, true))
                    {
                        strmWriter.WriteLine("On " + DateTime.Now.ToString() + ",");
                        strmWriter.WriteLine("Message: ");
                        strmWriter.WriteLine(message);
                        strmWriter.WriteLine("-------------------------------------------------------------------------------");
                    }
                }
                else
                    throw new DirectoryNotFoundException("BespokePoint log folder not found in the specified path");
            }
            catch
            {

            }
        }

        #endregion


    }
}
