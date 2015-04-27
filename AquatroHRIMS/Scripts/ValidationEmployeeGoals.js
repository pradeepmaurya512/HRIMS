function fnValidation()
{
    var flag = false;

    var width = $("#divCal").width();
    var completion = parseInt(width) / parseInt(2);
    completion = Math.round(completion);
    $("#hdnCompletion").val(completion);

    var intTitle = $("#ddlTitle").get(0).selectedIndex;
  
    if (intTitle == 0) {
        $("#spnTitle").show();
        flag = true;
    }

   var indStatus = $("#ddlStatus").get(0).selectedIndex;
    if (indStatus == 0) {
        $("#spnStatus").show();
        flag = true;
    }

    if (flag)
        return false;
    else
        return true;    
}
function fnReset()
{     
    $("#spnTitle").hide();
    $("#spnEmployee").hide();
    $("#ddlEmployee").val(0);
    $("#ddlTitle").val(0);
    $("#ddlStatus").val(0);
    $("#txtComplation").val("");
    $("#txtMgrComment").val("");
    $("#txtGoalDesc").val("");
  
    return false;
}
function fnValidationPopUP()
{
    var GoalTitle = $("#txtGoalTitle").val().trim();

    if (GoalTitle == "")        
        $("#spnGoalTitle").show();    
    else
        $("#spnGoalTitle").hide();

    return false;
}
function fnResetValues()
{
    $("#spnGoalTitle").hide();
    $("#spnGoalTitle1").hide();
}