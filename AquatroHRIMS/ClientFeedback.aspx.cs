using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Net;
using System.Net.Mail;

namespace AquatroHRIMS
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        cBaseHelper cs = new cBaseHelper();
        SqlParameterCollection param = new SqlCommand().Parameters;
        DataSet ds;
        string strQuerystring = "";
        string res = "";
        int ProjectID = 0;
        int EmployeeID = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString["Q"] != null || Request.QueryString["Q"] != "")
                {
                    //strQuerystring = Convert.ToString(Request.QueryString["Q"]);

                    //string decryptStr = EncryptDecrypt.Decrypt(strQuerystring);
                    //if (decryptStr != "0" && decryptStr != "")
                    //{
                    //    string[] splitString = decryptStr.Split(',');
                    //    EmployeeID = Convert.ToInt32(splitString[0]);
                    //    ProjectID = Convert.ToInt32(splitString[1]);
                    //}
                    //else
                    //{
                    //    Response.Redirect("Error.html");
                    //}
                    EmployeeID = 153;
                    ProjectID = 2;
                }
                else
                {

                    Response.Redirect("Error.html");
                }
                BindSkills(ProjectID);
                if (!IsPostBack)
                {
                    PreviousHistory(ProjectID, EmployeeID);
                    BindClient(ProjectID, EmployeeID);
                }
            }
            catch (Exception)
            {

                Response.Redirect("Error.html");
            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string result = CheckIfRecordExist(ProjectID, EmployeeID);
        }
        public void BindSkills(int ProjectID)
        {

            try
            {
                SqlParameterCollection param = new SqlCommand().Parameters;
                param.AddWithValue("@objProjectDetail", ProjectID);
                DataTable dt = cs.GenericStatementReturnDataTable("uspSkills", param);
                if (dt.Rows.Count > 0)
                {


                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Panel pnl = new Panel();
                        pnlSkills.Attributes.Add("class", "padding-top:20px;");
                        pnlSkills.Attributes.Add("style", "col-lg-12");
                        Label lbl = new Label();

                        LiteralControl div = new LiteralControl(@"<div class='col-lg-2'>" + dt.Rows[i]["sSkillName"].ToString().Trim() + "</div>");

                        //lbl.Text = dt.Rows[i]["sSkillName"].ToString().Trim();
                        //  lbl.Attributes.Add("class", "col-lg-4");
                        TextBox txt = new TextBox();
                        txt.ID = "txt" + dt.Rows[i]["iID"].ToString().Trim();
                        txt.Attributes.Add("class", "col-lg-4");
                        txt.Attributes.Add("skill", dt.Rows[i]["sSkillName"].ToString().Trim());
                        LinkButton btn = new LinkButton();
                        btn.ID = "btn" + dt.Rows[i]["iID"].ToString().Trim();
                        btn.Text = "Clear";
                        btn.Attributes.Add("style", "padding-left:10px;");
                        btn.Attributes.Add("type", "button");
                        pnlSkills.Controls.Add(div);
                        pnlSkills.Controls.Add(txt);
                        pnl.Controls.Add(btn);

                        pnlSkills.Controls.Add(pnl);
                        pnlSkills.Controls.Add(new LiteralControl("<br>"));

                    }
                }

            }
            catch (Exception ex)
            {

            }
        }
        public void SaveSkillComment(int objFeedback)
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("sName");
                dt.Columns.Add("objClientFeedback");
                dt.Columns.Add("objSkill");
                dt.Columns.Add("dtCreatedOn");
                dt.Columns.Add("IsActive");
                dt.Columns.Add("sDescriptionComment");

                string[] SkillComment = hdnSkillComment.Value.ToString().Split(new[] { ", " }, StringSplitOptions.None);

                for (int i = 0; i < SkillComment.Length - 1; i++)
                {
                    string sName = "";
                    int objClientFeedback = objFeedback;
                    var results = SkillComment[i].ToString().Split(new[] { ":", "~" }, StringSplitOptions.None);
                    int objSkill = Convert.ToInt16(results[0]);
                    DateTime date = DateTime.Now;
                    string IsActive = "1";
                    string sDescriptionComment = results[1].Trim();
                    if (!string.IsNullOrEmpty(sDescriptionComment))
                        dt.Rows.Add(sName, objClientFeedback, objSkill, date, IsActive, sDescriptionComment);
                }

                param.AddWithValue("@tblSkill", dt);
                int result = cs.GenericStatementBulkInsert("uspInsertSkillComment", dt);
                GenerateHTML();
            }
            catch (Exception ex)
            {

            }
        }
        public void PreviousHistory(int ProjectID, int EmployeeID)
        {
            param.AddWithValue("@ProjectId", ProjectID);
            param.AddWithValue("@EmployeeId", EmployeeID);
            DataTable dt = cs.GenericStatementReturnDataTable("uspFeedbackhistory", param);

            if (dt.Rows.Count > 0)
            {


                var result = from row in dt.AsEnumerable()
                             group row by row.Field<int>("id") into grp
                             select new
                             {
                                 id = grp.Key
                                 // MemberCount = grp.Count()
                             };
                divPreviousHistory.InnerHtml += @"<div class='panel-group' id='accordion'>";
                foreach (var t in result)
                {
                    DataView dv = dt.DefaultView;
                    dv.RowFilter = "id=" + t.id;
                    headName.InnerText = "Previous review of " + dv[0]["Employee Name"].ToString();
                    divPreviousHistory.InnerHtml += @"<div class='panel panel-default'>
		                        <div class='panel-heading'>
			                        <h4 class='panel-title'>
                                        <a class='accordion-toggle' data-toggle='collapse' data-parent='#accordion' href='#div" + dv[0]["id"].ToString() + @"'>" + dv[0]["datecreated"].ToString() + @"</a>
			                        </h4>
		                        </div>
		                        <div id='div" + dv[0]["id"].ToString() + @"' class='panel-collapse collapse'>
			                        <div class='panel-body'>
                                        <label>Technical Skill Level</label>:<label class='lblCSS'>" + dv[0]["TechSkill"].ToString() + "</label><br /><label>Skill Improve</label>:<label></label><br />";
                    for (int j = 0; j < dv.Count; j++)
                    {
                        divPreviousHistory.InnerHtml += @"<span>" + dv[j]["sSkillName"].ToString() + "</span>:<span class='lblCSS'>" + dv[j]["sDescriptionComment"].ToString() + "</span><br />";
                    }
                    divPreviousHistory.InnerHtml += @"<label>Meet Goals</label>:<label class='lblCSS'>" + dv[0]["MeetGoals"].ToString() + @"</label><br />
                                        <label>Work Critism</label>:<label class='lblCSS'>" + dv[0]["Attention"].ToString() + @"</label><br />
                                        <label>Overall Performance</label>:<label class='lblCSS'>" + dv[0]["EmployeePerformance"].ToString() + @"</label><br />
                                        <label>Improve Performance Comment</label>:<label class='lblCSS'>" + dv[0]["Improvements"].ToString() + @"</label><br />
                                         <label>Additional Comment</label>:<label class='lblCSS'>" + dv[0]["AdditionalComment"].ToString() + @"</label>
			                        </div>
		                        </div>
                            </div>";
                }
                divPreviousHistory.InnerHtml += "</div>";
            }
            else
            {
                divPreviousHistory.InnerText = "No previous history.";
                divPreviousHistory.Style.Add("color", "red");
            }
        }
        public void GenerateHTML()
        {
            string strBody = "";
            StreamReader sr = new StreamReader(HttpContext.Current.Server.MapPath(HttpContext.Current.Request.ApplicationPath + "/App_Data/PDFFormat.html"));
            strBody = sr.ReadToEnd();
            sr.Close();
            string Logo = @"<p style='text-align:center'><img src='" + Server.MapPath(Request.ApplicationPath) + "\\Content/Images/logo.jpg' /></p>";
            strBody = strBody.Replace("#Logo#", Logo);
            strBody = strBody.Replace("#DateTime#", DateTime.Now.ToString());
            strBody = strBody.Replace("#ClientName#", lblClientNAme.InnerText);
            strBody = strBody.Replace("#ProjectName#", lblProjectName.InnerText);
            strBody = strBody.Replace("#EmployeeName#", lblEmployeeName.InnerText);
            strBody = strBody.Replace("#ReviewerName#", lblReviewerName.InnerText);
            strBody = strBody.Replace("#ProjectStatus#", rbtlStatus.SelectedItem.Text);

            strBody = strBody.Replace("#Outstanding#", rbtlTechSkill.SelectedItem.Text);
            strBody = strBody.Replace("#Goal#", rbtnGoals.SelectedItem.Text);
            strBody = strBody.Replace("#Meets#", rbtnWork.SelectedItem.Text);
            strBody = strBody.Replace("#RatePerformance#", rbtnPerformance.SelectedItem.Text);
            strBody = strBody.Replace("#ImprovementPer#", txtAreaPerformance.Value);
            strBody = strBody.Replace("#Additional#", txtAreaAdditionalComment.Value);
            //  string[] SkillComment = hdnSkillComment.Value.ToString().Split(new[] { ", " }, StringSplitOptions.None);

            //string MailTo = txtEmail.Text;
            string[] SkillComment = hdnSkillComment.Value.ToString().Split(new[] { ", " }, StringSplitOptions.None);
            string text = "";
            for (int i = 0; i < SkillComment.Length - 1; i++)
            {
                var result = SkillComment[i].ToString().Split(new[] { ":", "~" }, StringSplitOptions.None);
                string Skill = result[2].ToString();
                string sDescriptionComment = result[1].ToString();
                if (!string.IsNullOrEmpty(sDescriptionComment))
                    text += @"<p ><span style='font-weight:bold' >" + Skill + ": </span>" + sDescriptionComment + "</p>";

            }
            strBody = strBody.Replace("#SkillImprove#", text);
            ClientScript.RegisterStartupScript(GetType(), "Message", "SucessMsg()", true);
            CreatePDFDocument(strBody);
        }
        public void CreatePDFDocument(string strHtml)
        {
            string filename = lblEmployeeName.InnerText + "Feedback_" + DateTime.Now.ToString() + ".pdf";
            filename = filename.Replace(":", "_").Replace("/", "");
            string strFileName = HttpContext.Current.Server.MapPath("PDF\\" + filename);
            // step 1: creation of a document-object 
            Document document = new Document();
            // step 2: 

            // we create a writer that listens to the document 
            Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 100f, 0f);
            PdfWriter.GetInstance(document, new FileStream(strFileName, FileMode.Create));
            StringReader se = new StringReader(strHtml);
            HTMLWorker obj = new HTMLWorker(document);
            document.Open();
            obj.Parse(se);
            document.Close();


            //MailBody For HR mail:-
            string strBody = "";
            StreamReader sr = new StreamReader(HttpContext.Current.Server.MapPath(HttpContext.Current.Request.ApplicationPath + "/App_Data/MailFormat.html"));

            strBody = sr.ReadToEnd();
            sr.Close();
            strBody = strBody.Replace("#ClientName#", lblReviewerName.InnerText);
            strBody = strBody.Replace("#EmployeeName#", lblEmployeeName.InnerText);
            strBody = strBody.Replace("#ProjectName#", lblProjectName.InnerText);
            string from = ConfigurationManager.AppSettings["FromHR"].ToString();
            string MailTo = ConfigurationManager.AppSettings["FromHR"].ToString();
            string msg = SendEmailAttachment(MailTo, from, "Client Feedback", strBody, true, strFileName);
            ClientScript.RegisterStartupScript(GetType(), "Message", "SucessMsg()", true);

        }
        public override void VerifyRenderingInServerForm(Control control)
        {

        }
        public void BindClient(int ProjectID, int EmployeeID)
        {
            SqlParameterCollection param = new SqlCommand().Parameters;
            param.AddWithValue("@ProjectID", ProjectID);
            param.AddWithValue("@EmployeeID", EmployeeID);
            DataTable dt = cs.GenericStatementReturnDataTable("uspBindProjectClient", param);
            if (dt.Rows.Count > 0)
            {
                lblProjectName.InnerText = dt.Rows[0]["ProjectName"].ToString();
                lblReviewerName.InnerText = dt.Rows[0]["ReviewerName"].ToString();
                lblClientNAme.InnerText = dt.Rows[0]["ClientName"].ToString();
                lblEmployeeName.InnerText = dt.Rows[0]["EmployeeName"].ToString();
            }
        }
        public string CheckIfRecordExist(int ProjectID, int EmployeeID)
        {
            SqlParameterCollection param = new SqlCommand().Parameters;
            param.AddWithValue("@ProjectID", ProjectID);
            param.AddWithValue("@EmployeeID", EmployeeID);
            DataTable dt = cs.GenericStatementReturnDataTable("uspCheckExisting", param);
            string result = "";
            if (dt.Rows.Count > 0)
            {
                InsertClientFeedBack(dt.Rows[0]["Linkid"].ToString());
                //  result = dt.Rows[0]["Result"].ToString();
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "CheckSubmission()", true);
            }
            return result;
        }
        public static string SendEmailAttachment(string i_sTo, string i_sFrom, string i_sSubject, string i_sEmailBody, bool i_bIsHTML, string sPath)
        {
            string o_sStatus = "<br/>ok";
            try
            {
                string i_sSMTPHost = ConfigurationManager.AppSettings["HostMail"].ToString();

                string pass = ConfigurationManager.AppSettings["HRpassword"].ToString();

                MailMessage oMail = new MailMessage();
                oMail.To.Add(i_sTo);
                oMail.From = new MailAddress(i_sFrom);
                oMail.Subject = i_sSubject;

                Attachment attach = new Attachment(sPath, "application/pdf");
                oMail.Attachments.Add(attach);

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
                SmtpMail.Port = 25; /* New added for testing */
                SmtpMail.EnableSsl = false; /* New added for testing */
                SmtpMail.UseDefaultCredentials = false;
                SmtpMail.UseDefaultCredentials = false;
                SmtpMail.Credentials = Credential;
                //SmtpMail.Port = 80;/* New added for testing */
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
        public void InsertClientFeedBack(string linkid)
        {
            try
            {
                //if (res != "0")
                //{
                string ClientID = "1";
                string EmployeeId = EmployeeID.ToString();
                string ProjectId = ProjectID.ToString();
                string ProjectStatus = rbtlStatus.SelectedItem.Text.ToString();
                string ProjectSkill = rbtlTechSkill.SelectedItem.Text.ToString();
                string ProjectGoal = rbtnGoals.SelectedItem.Text.ToString();
                string ProjectWork = rbtnWork.SelectedItem.Text.ToString();
                string ProjectPerformance = rbtnPerformance.SelectedItem.Text.ToString();
                string ProjectImproveComment = txtAreaPerformance.Value.ToString();
                string ProjectAdditionalComment = txtAreaAdditionalComment.Value.ToString();

                param.AddWithValue("@objClientID", ClientID);
                param.AddWithValue("@objEmployeeDetail", EmployeeId);
                param.AddWithValue("@objProjectDetail", ProjectId);
                param.AddWithValue("@sProjectStatus", ProjectStatus);
                param.AddWithValue("@sTechnicalSkill ", ProjectSkill);
                param.AddWithValue("@sMeetGoals", ProjectGoal);
                param.AddWithValue("@sAttention", ProjectWork);
                param.AddWithValue("@sPerformance", ProjectPerformance);
                param.AddWithValue("@sImproveComment", ProjectImproveComment);
                param.AddWithValue("@sAdditionalComment", ProjectAdditionalComment);
                param.AddWithValue("@bIsActive", true);
                param.AddWithValue("@linkid", linkid);

                int result = cs.GenericStatementReturnRowsAffected("uspClientFeedback", param);
                SaveSkillComment(result);
                // }
                // else {
                //  ClientScript.RegisterStartupScript(GetType(), "Message", "CheckSubmission()", true);
                // }
            }
            catch (Exception ex)
            {

            }

        }
    }
}