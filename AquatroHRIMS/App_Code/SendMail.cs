using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Net;
using System.Net.Mail;



    public class SendMail
    {
        public static string SendEmail(string i_sTo,string i_sFrom,string i_sSubject,string i_sEmailBody,bool i_bIsHtml)
        {
            string o_sStatus = "<br/>ok";
            try
            {
                string i_sSMTPHost = ConfigurationManager.AppSettings["SMTPMailServer"].ToString();
                MailMessage oMail =new MailMessage();
                oMail.To.Add(i_sTo);
                oMail.From = new MailAddress(i_sFrom);
                oMail.Subject = i_sSubject;
                if (i_bIsHtml)
                {
                    oMail.IsBodyHtml = true;
                }
                else 
                {
                    oMail.IsBodyHtml = false;
                }
                oMail.Body = i_sEmailBody;
                SmtpClient smtpmail = new SmtpClient();
                smtpmail.Host = i_sSMTPHost;
                try
                {
                    smtpmail.Send(oMail);
                }
                catch (Exception ex2)
                { 
                 o_sStatus= "error2:" + ex2.Message + "\nTrace:"+ ex2.StackTrace;
                   
                }       

            }
            catch (Exception ex1)
            {
                o_sStatus = "error1:" + ex1.Message + "\ntrace:" + ex1.Message;
            }
            return o_sStatus;
        }
        //public static void SendEmail()
        //{

        //    try
        //    {
        //        string strHtml = MailFormat();
        //        CreatePDFDocument(strHtml);
        //        MailMessage mail = new MailMessage();
        //        SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
        //        mail.From = new MailAddress("rohan.kathe1601@gmail.com");
        //        //mail.From = new MailAddress(Convert.ToString(ViewState["ClientEmail"]));
        //        mail.To.Add("rohan.kathe@a4technology.com");
        //        //mail.CC.Add("nilesh.singh@a4technology.com");
        //        mail.Subject = ViewState["ClientEmail"].ToString() + "has submitted feedback for " + lblEmployee.Text;
        //        mail.Body = "";

        //        System.Net.Mail.Attachment attachment;
        //        string strFileName = HttpContext.Current.Server.MapPath("pdf\\" + ViewState["EmployeeName"] + ".pdf");
        //        attachment = new System.Net.Mail.Attachment(strFileName);
        //        mail.Attachments.Add(attachment);


        //        SmtpServer.Port = 587;
        //        SmtpServer.Credentials = new System.Net.NetworkCredential("rohan.kathe1601@gmail.com", "nokialumia620");
        //        SmtpServer.EnableSsl = true;

        //        SmtpServer.Send(mail);
        //        ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Email sent.');", true);
        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //}
        //public static string SendEmailAttachment(string i_sTo, string i_sFrom, string i_sSubject, string i_sEmailBody, bool i_bIsHTML, string sPath)
        //{
        //    string o_sStatus = "<br/>ok";
        //    try
        //    {
        //        string i_sSMTPHost = ConfigurationManager.AppSettings["SMTPMailServer"].ToString();

        //        MailMessage oMail = new MailMessage();
        //        oMail.To.Add(i_sTo);
        //        oMail.From = new MailAddress(i_sFrom);
        //        oMail.Subject = i_sSubject;

        //        Attachment attach = new Attachment(sPath, "application/pdf");
        //        oMail.Attachments.Add(attach);

        //        if (i_bIsHTML)
        //        {
        //            oMail.IsBodyHtml = true;
        //        }
        //        else
        //        {
        //            oMail.IsBodyHtml = false;
        //        }
        //        oMail.Body = i_sEmailBody;
        //        //foreach (Attachment m_att in i_msg.Attachments)
        //        //{
        //        //    oMail.Attachments.Add(m_att);
        //        //}
        //        SmtpClient SmtpMail = new SmtpClient();

        //        SmtpMail.Credentials = new NetworkCredential("testinga4mail@gmail.com", "a4technology"); /* New added for testing */
        //        SmtpMail.Port = 25; /* New added for testing */
        //        SmtpMail.EnableSsl = true; /* New added for testing */
        //        SmtpMail.DeliveryMethod = SmtpDeliveryMethod.Network; /* New added for testing */
        //        SmtpMail.Host = i_sSMTPHost;

        //        try
        //        {
        //            SmtpMail.Send(oMail);
        //        }
        //        catch (Exception ex2)
        //        {
        //            // LogError("SendEmail1a", "", ex2.Message, ex2.StackTrace);
        //            o_sStatus = "error2:" + ex2.Message + "\ntrace:" + ex2.StackTrace;
        //            // LogErrors("cCustoMain.cs", ex2.StackTrace.ToString(), ex2.Message.ToString(), "SendEmail()");

        //        }
        //    }
        //    catch (Exception ex1)
        //    {
        //        //LogError("SendEmail1", "", ex1.Message, ex1.StackTrace);
        //        o_sStatus = "error1:" + ex1.Message + "\ntrace:" + ex1.StackTrace;
        //        // LogErrors("cCustoMain.cs", ex1.StackTrace.ToString(), ex1.Message.ToString(), "SendEmail()");
        //    }
        //    return o_sStatus;
        //}
        public static string SendEmailAttachment(string i_sTo, string i_sFrom, string i_sSubject, string i_sEmailBody, bool i_bIsHTML, string sPath)
        {
            string o_sStatus = "<br/>ok";
            try
            {
                string i_sSMTPHost = ConfigurationManager.AppSettings["HostMail"].ToString();

                string pass = ConfigurationManager.AppSettings["password"].ToString();

                MailMessage oMail = new MailMessage();
                oMail.To.Add(i_sTo);
                oMail.From = new MailAddress(i_sFrom);
                oMail.Subject = i_sSubject;

              //  Attachment attach = new Attachment(sPath, "application/pdf");
              //  oMail.Attachments.Add(attach);

                if (i_bIsHTML)
                {
                    oMail.IsBodyHtml = true;
                }
                else
                {
                    oMail.IsBodyHtml = false;
                }
                oMail.Body = i_sEmailBody;
                //foreach (Attachment m_att in i_msg.Attachments)
                //{
                //    oMail.Attachments.Add(m_att);
                //}
                SmtpClient SmtpMail = new SmtpClient("relay-hosting.secureserver.net", 25);

                NetworkCredential Credential = new NetworkCredential(); /* New added for testing */
                Credential.UserName = i_sFrom;
                Credential.Password = pass;
                SmtpMail.Port = 80; /* New added for testing */
                SmtpMail.EnableSsl = false; /* New added for testing */
                SmtpMail.UseDefaultCredentials = false;
                SmtpMail.UseDefaultCredentials = false;
                SmtpMail.Credentials = Credential;
                SmtpMail.Port = 80;/* New added for testing */
                SmtpMail.Host = i_sSMTPHost;

                try
                {
                    SmtpMail.Send(oMail);
                }
                catch (Exception ex2)
                {
                    // LogError("SendEmail1a", "", ex2.Message, ex2.StackTrace);
                    o_sStatus = "error2:" + ex2.Message + "\ntrace:" + ex2.StackTrace;
                    // LogErrors("cCustoMain.cs", ex2.StackTrace.ToString(), ex2.Message.ToString(), "SendEmail()");

                }
            }
            catch (Exception ex1)
            {
                //LogError("SendEmail1", "", ex1.Message, ex1.StackTrace);
                o_sStatus = "error1:" + ex1.Message + "\ntrace:" + ex1.StackTrace;
                // LogErrors("cCustoMain.cs", ex1.StackTrace.ToString(), ex1.Message.ToString(), "SendEmail()");
            }
            return o_sStatus;
        }

    }

