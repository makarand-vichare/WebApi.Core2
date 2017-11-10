namespace WebApi.Core.Utility
{
    public class AppProperties
    {
        //public static SmtpSection SmtpMailSettings
        //{
        //    get
        //    {
        //        if (AppMethods.GetCache<SmtpSection>(AppConstants.SmtpMailSettings) == null)
        //        {
        //            AppMethods.AddCache(AppConstants.SmtpMailSettings, ConfigurationManager.GetSection("system.net/mailSettings/smtp") as SmtpSection);
        //        }

        //        return AppMethods.GetCache<SmtpSection>(AppConstants.SmtpMailSettings) as SmtpSection;
        //    }
        //}

        public static string BasePhysicalPath
        {
            get
            {
                return AppMethods.GetCache<string>(AppConstants.BasePhysicalPath).ToString();
            }
            set
            {
                AppMethods.AddCache(AppConstants.BasePhysicalPath, value);
            }
        }

    }
}
