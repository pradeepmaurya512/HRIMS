<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ClientFeedback.aspx.cs" Inherits="AquatroHRIMS.WebForm1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Client Feedback</title>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta name="description" content="" />
    <meta name="author" content="" />
        <!-- Bootstrap Core CSS -->
     <script src="Scripts/jquery-1.11.1.js"></script>   
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <script src="Scripts/bootstrap.min.js"></script>
    
    <!-- Custom CSS -->
    <link href="Content/modern-business.css" rel="stylesheet" />

    <!-- Custom Fonts -->
    <link href="font-awesome-4.1.0/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link href="css/styleclientfeedback.css" rel="stylesheet" type="text/css" />
    <style>
        .feedBackTitle{
    background-color:#2F96DB;
    color:white;
}
.formBorder{
border:2px solid black;
}
.lblCSS {
font-family:Calibri;
color:#2F96DB;
font-size:15px;
}
.btnModel {
}
    </style>
    <script>
        function SetValue() {                     
            var result = "";           
            $('#pnlSkills').find("input").each(function () {               
                if ($(this).val().trim() != '') {                   
                    var id = $(this).attr("id").replace("txt", "");                    
                    result += id + " : " + $(this).val() + " ~ " + $(this).attr('skill') + ", ";                    
                }
            });
            $("#hdnSkillComment").val(result);

            //Validation Of Input:-
            var v = '';
            var Counter = 0;
            v = $("#<%=rbtlTechSkill.ClientID %> input:checked").length;
            if (v == 0) {
                $("#<%=lblErrorskill.ClientID %>").css('display', '');
                Counter = Counter + 1;
            }
            v = $("#<%=rbtnGoals.ClientID %> input:checked").length;
            if (v == 0) {
                $("#<%=lblEmployeeGoal.ClientID %>").css('display', '');
                Counter = Counter + 1;
            }
            v = $("#<%=rbtnWork.ClientID %> input:checked").length;
            if (v == 0) {
                $("#<%=lblEmployeeAttention.ClientID %>").css('display', '');
                Counter = Counter + 1;
            }
            v = $("#<%=rbtnPerformance.ClientID %> input:checked").length;
            if (v == 0) {
                $("#<%=lblEmployeeperformance.ClientID %>").css('display', '');
                Counter = Counter + 1;
            }
            v = $("#<%=txtAreaPerformance.ClientID %>").val().trim();
            if (v == "") {
                $("#<%=lblComment.ClientID %>").css('display', '');
                Counter = Counter + 1;
            }
            if (Counter > 0) {
                return false;
            }
            return true;

        }
        $(document).ready(function () {
            $("a").on("click", function () {
                var id = $(this).attr("id").replace("btn", "txt");
                $("#" + id).val("");
                return false;
            });
            $("#<%=rbtlTechSkill.ClientID %>").change(function () {
                $("#<%=lblErrorskill.ClientID %>").css('display', 'none');
            });
            $("#<%=rbtnGoals.ClientID %>").change(function () {
                $("#<%=lblEmployeeGoal.ClientID %>").css('display', 'none');
            });
            $("#<%=rbtnWork.ClientID %>").change(function () {
                $("#<%=lblEmployeeAttention.ClientID %>").css('display', 'none');
            });
            $("#<%=rbtnPerformance.ClientID %>").change(function () {
                $("#<%=lblEmployeeperformance.ClientID %>").css('display', 'none');
            });
        })

        function RemoveError() {
            $("#<%=lblComment.ClientID %>").css('display', 'none');
        }

        function SucessMsg() {
            alert('Thank you for submitting your feedback.');
            ClearData();
        }
        function CheckSubmission() {
            alert('You have already submitted your feedback.');
            $('form').clearForm();
            return false;
        }
        function ClearData() {
            $('form').clearForm();
        }
        function Reset() {
            $('form').clearForm();
            return false;
        }
        $.fn.clearForm = function () {
            return this.each(function () {
                var type = this.type, tag = this.tagName.toLowerCase();
                if (tag == 'form')
                    return $(':input', this).clearForm();
                if (type == 'text' || type == 'password' || tag == 'textarea')
                    this.value = '';
                else if (type == 'checkbox' || type == 'radio') {
                    this.checked = false;
                    $("#rbtlStatus_0").prop("checked", true);
                }
                else if (tag == 'select')
                    this.selectedIndex = -1;


            });
        };
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="row">
          <div id="MainFormContent" class="container">
         
                <div class="col-lg-12">
                    <div class="logo">
                        <a href="Index.aspx">
                            <img src="Content/Images/A4logo.png" width="433" height="63" alt="" /></a>
                    </div>
                </div>
         
                <div class="col-lg-12 text-center feedBackTitle">
                    <h4>Client Feedback</h4>
                </div>
                <div id="ClientDetail" class="formBorder col-lg-12" style="padding-top:20px;">
                    <div class="col-lg-4">
                        <div class="controls">
                            <label>Client Name:</label>
                            <label id="lblClientNAme" class="lblCSS" runat="server">Client Name</label><br />
                            <label>Employee Name:</label>
                            <label id="lblEmployeeName" class="lblCSS" runat="server">Employee Name</label><br />
                            <label>Reviewer Name:</label>
                            <label id="lblReviewerName" class="lblCSS" runat="server">Reviewer Name</label>
                        </div>
                    </div>
                    <div class="col-lg-4">
                        <div class="controls">
                            <label>Project Name:</label>
                            <label id="lblProjectName" class="lblCSS" runat="server">Project Name</label><br />
                            <label>Project Status:</label>
                            <div style="margin-top: -27px;padding-left:109px;">
                                <asp:RadioButtonList ID="rbtlStatus" runat="server">
                                    <asp:ListItem Selected="True" Text="On Going"></asp:ListItem>
                                    <asp:ListItem Text="Completed"></asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
   
                        </div>
                    </div>
                    <div class="col-lg-4"  style="padding-top:20px;">
                     
                <!-- Button trigger modal -->

            <asp:Button ID="btnShowModal" runat="server"  Text="Previous History" CssClass="btn btn-primary btn-info btnModel" data-target="#pnlModal" data-toggle="modal" OnClientClick="javascript:return false;"/>
            <asp:Panel ID="pnlModal" runat="server" role="dialog" CssClass="modal fade">
            <asp:Panel ID="pnlInner" runat="server" CssClass="modal-dialog" >
                <asp:Panel ID="pnlContent" CssClass="modal-content" runat="server">
                    <asp:Panel runat="server" class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">
                            <span aria-hidden="true">&times;</span><span class="sr-only">Close</span>
                        </button>
                        <h4 class="modal-title" id="headName" runat="server">Previous History</h4>
                    </asp:Panel>
                    <asp:panel runat="server" class="modal-body">                	 
	                         <div id="divPreviousHistory" runat="server">

	                         </div>
                     </asp:panel>
                    <asp:panel runat="server" class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    </asp:panel>
                </asp:Panel>
            </asp:Panel>
        </asp:Panel>

                    </div>
                </div>
                <div class="col-lg-12" style="padding-top:20px; padding-bottom:10px;">
                        <label  runat="server">Please provide your valuable feedback and help us to monitor our employee’s performance.</label>
                    </div>
                <div class="col-lg-12">
                      <div class="form-group">
                           <label runat="server">1) Please rate our employee’s technical skill level.</label><span style="color:red"> *</span>
                                <label id="lblErrorskill" runat="server" style="color:red;display:none;">Please select an option</label>
                       <div class="radio col-lg-12" style="margin-left: 16px">
                              <asp:RadioButtonList  ID="rbtlTechSkill"  runat="server">
                                <asp:ListItem Text="Outstanding"></asp:ListItem>
                                <asp:ListItem Text="Exceeds Expectation"></asp:ListItem>
                                <asp:ListItem Text="Meets Expectation"></asp:ListItem>
                                <asp:ListItem Text="Poor"></asp:ListItem>
                            </asp:RadioButtonList>
                      
                        </div>
                       
                      </div>
                    </div>
                <div class="col-lg-12">
                      <div class="form-group">
                        <label runat="server">2) Please select a relevant skill from below list and provide your comment that you think our employee needs to improve.</label>
                     <div class="col-lg-12">
                       <asp:Panel id="pnlSkills"  runat="server">

                       </asp:Panel>
                      </div> 
                        <asp:HiddenField ID="hdnSkillComment" runat="server" />
                      </div>
                    </div>
                <div class="col-lg-12">
                        <label runat="server">3) How well does our employee work to meet the goals you set for him/her?</label><span style="color:red"> *</span>
                     <label id="lblEmployeeGoal" runat="server" style="color:red;display:none;">Please select an option</label>
                        <div class="radio col-lg-12" style="margin-left: 16px">
                            <asp:RadioButtonList ID="rbtnGoals" runat="server">
                                <asp:ListItem Text="Outstanding"></asp:ListItem>
                                <asp:ListItem Text="Exceeds Expectation"></asp:ListItem>
                                <asp:ListItem Text="Meets Expectation"></asp:ListItem>
                                <asp:ListItem Text="Poor"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                        <div class="form-group">
                        <label runat="server">4) How much attention to detail does our employee have and how well does our employee handle criticism of his/her work?</label><span style="color:red"> *</span>
                                <label id="lblEmployeeAttention" runat="server" style="color:red;display:none;">Please select an option</label>
                        <div class="radio col-lg-12" style="margin-left: 16px">
                            <asp:RadioButtonList ID="rbtnWork" runat="server">
                                <asp:ListItem Text="Outstanding"></asp:ListItem>
                                <asp:ListItem Text="Exceeds Expectation"></asp:ListItem>
                                <asp:ListItem Text="Meets Expectation"></asp:ListItem>
                                <asp:ListItem Text="Poor"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                            </div>
                    </div>
               <div class="col-lg-12">
                            <div class="form-group">
                        <label runat="server">5) How do you rate our employee’s overall performance?</label><span style="color:red"> *</span>
                       <label id="lblEmployeeperformance" runat="server" style="color:red;display:none;">Please select an option</label>
                        <div class="radio col-lg-12" style="margin-left: 16px">
                            <asp:RadioButtonList ID="rbtnPerformance" runat="server">
                                <asp:ListItem Text="Outstanding"></asp:ListItem>
                                <asp:ListItem Text="Exceeds Expectation"></asp:ListItem>
                                <asp:ListItem Text="Meets Expectation"></asp:ListItem>
                                <asp:ListItem Text="Poor"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                                </div>
                        <div class="form-group">
                       <label  runat="server">6) What does our employee need to do to improve his/her performance?</label><span style="color:red;"> *</span><label id="lblComment" runat="server" style="color:red;display:none;">Please enter comment.</label>
                        <div class="col-lg-12">
                            <textarea  class="form-control" runat="server" onchange="RemoveError();" rows="5" id="txtAreaPerformance"></textarea>
                        </div>
                         </div>
                         <div class="form-group">
                          <label runat="server">7) Please provide additional comment about this employee(Optional)</label>
                        <div class="col-lg-12 ">
                            <textarea class="form-control" runat="server" rows="5" id="txtAreaAdditionalComment"></textarea>
                        </div>
                             </div>

                        <div class="col-lg-12">
                            <br />
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClientClick="return SetValue();" OnClick="btnSubmit_Click"/>
                           <%-- <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClientClick="return SetValue();" OnClick="btnSubmit_Click"  />--%>
                            <asp:Button ID="btnReset" CssClass="col-lg-pull-1" runat="server" OnClientClick="return Reset();" Text="Reset" />
                        </div>
                        <div class="col-lg-12 text-center">
                            <br />
                            <label runat="server">"Good businesses listen to feedback. Great ones act on it"</label>
                        </div>
                    </div>
               <div class="col-lg-12 text-center">
                    <hr />
                    Copyright © 2015 <a target="_blank" href="http://www.a4technology.com">A<sup>4</sup> Technology Solutions</a>
                </div>
              <div class="modal fade">
                  <div class="modal-dialog">
                    <div class="modal-content">
                      <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                        <h4 class="modal-title">Modal title</h4>
                      </div>
                      <div class="modal-body">
                        <p>One fine body…</p>
                      </div>
                      <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                        <button type="button" class="btn btn-primary">Save changes</button>
                      </div>
                    </div>>
                  </div>
               </div>
            
        </div>
        </div>
    </form>
</body>
