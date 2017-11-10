namespace WebApi.Core.Utility
{
    public class AppMessages
    {

        public const string Input_MinCharsAllowed = "Minimum 5 characters required.";
        public const string Input_MaxCharsAllowed = "Maximum {1} characters required.";

        public const string User_AuthenticationFailed = "User authentication failed!.Check you login credentials.";

        public const string Email_Failed_Message = "Failed to send your email.please check email id(s).";
        public const string Email_Succeed_Message = "Your email sent successfully.";

        public const string Send_Email_Cancel = "Send canceled.";

        public const string Send_Email_Failed = "Sending email failed.";
        public const string Send_Email_Succeed = "Sent your email successfully.";

        public const string Email_Registration_Subject = "National Criminal DB : Registration email.";
        public const string Email_PdfResult_Subject = "National Criminal DB : Search results  for your request for criminal details.";

        public const string Pdf_Queue_Processing_Failed = "Pdf processing in queue failed.";
    
        public const string Pdf_Queue_Processing_Succeed = "Pdf processing in queue succeed. now go for email queue processing";

        public const string Request_Queue_Processing_Failed = "Request in queue failed.";

        public const string Request_Queue_Processing_Succeed = "Request in queue succeed. now go for email queue processing";

        public const string Email_Queue_Processing_Failed = "Email in queue failed.";

        public const string Email_Queue_Processing_Succeed = "Email in queue succeed. Users should receive the email with attached pdf files.";

        public const string No_Record_Found = "No record found.";

        public const string Retrieved_Details_Successfully = "Retrieved the details successfully.";
        public const string Saved_Details_Successfully = "Saved the details successfully.";
        public const string Deleted_Details_Successfully = "Deleted the details successfully.";

        public const string Action_Failed = "Unknown error occured.";

        public const string ActionMessage_Succeed = "The record is retrieved successfully.";
        public const string ActionMessage_Failed = "The action is failed.";

        public const string INVALID_INPUT = "Invalid input";
        public const string SIGNUP_SUCCESS_REDIRECT_TIMEOUT_MESSAGE = "Your registration is done successfully. you will be redirect to login in a minute.";
        public const string SIGNUP_ERROR_DUPLICATE_KEY_MESSAGE = "Email id already exists";

    }
}
