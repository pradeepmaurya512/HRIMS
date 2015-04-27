
function fnValidate()
{   
    var flag = false;
    var regex = /^[a-zA-Z ]*$/;

    //============================= Project Name ===============================//

    if ($("#txtProjectName").val() == "") {
        $("#spnPrjName").show();
        $("#spnPrjName1").hide();
        flag = true;
    }
    if($("#txtProjectName").val() != ""){     
        if (!(regex.test($("#txtProjectName").val()))) {           
            $("#spnPrjName1").show()
            $("#spnPrjName").hide();
            flag = true;
    }
}    
    // =========================== End Project Name ===============================//
    //============================ Internal Project Head ==========================//

    var indInterPrjHead = $("#ddlInternalPrjHead").get(0).selectedIndex;
    if (indInterPrjHead == 0) {
        $("#spnInterPrjHead").show();
        flag = true;
    }
    
    //============================ End Internal Project Head =======================//
    //============================ client Name =====================================//

    var intClientId = $("#ddlClientID").get(0).selectedIndex;
    if (intClientId == 0) {
        $("#spnClientId").show();
        flag = true;
    }
    
    //============================ End Client Name ==================================//
    //============================ External Prooject Head ===========================//
    if ($("#txtExtProjectHead").val() == "") {
        $("#spnExtPrjHead").show();
        $("#spnExtPrjHead1").hide();
        $("#spnExtPrjHead2").hide();
        flag = true;        
    }
    else {
        $("#spnExtPrjHead").hide();
    }

    //========================= End External Project Head ==================//
    //========================= External Project Head Email ID =============//

    if ($("#txtExtProjectHeadEmailID").val() == "") {
        $("#spnExtPrjHeadEmail1").show();
        $("#spnExtPrjHeadEmail").hide();       
        flag = true;
    }
    else {
        $("#spnExtPrjHeadEmail1").hide();
    }

    //========================= End External Project Head Email ID =============//    
    //======================= ListBox Validation ====================================//

    var lstFalg = false;
    var lstbox = document.getElementById("lstTeamMemDetails");
    var optsLength = lstbox.options.length;
    for (var i = 0; i < optsLength; i++) {
        if (lstbox.options[i].selected) {
            lstFalg = true;
            break;
        }
    }
    if (lstFalg == false) {
        $("#spnTeamMemDetails").show();
        flag = true;
    }
    else
        $("#spnTeamMemDetails").hide();

    //======================== End ListBox Validation ================================//
    //======================= Date Validation ======================================//
    var startDate1 = $("#txtProjectStartDate").val();
    var endDate1 = $("#txtProjectEndDate").val();

    if (startDate1 == "") {
        $("#spnStartDate").show();
        flag = true;
    }
    else {
        $("#spnStartDate").hide();
    }

    if (endDate1 == "") {
        $("#spnEndDate").show();
        flag = true;
    }
    else {
        $("#spnEndDate").hide();
    }
    
    if (startDate1 != "" && endDate1 != "") {
        var startDate = new Date($("#txtProjectStartDate").val().toString('yyyy-MM-dd'));
        var endDate = new Date($("#txtProjectEndDate").val().toString('yyyy-MM-dd'));

        if (startDate >= endDate) {
            $("#spnDateValid1").show();
            flag = true;
        }
        else {
            $("#spnDateValid1").hide();
        }
    }

    //====================== End Date Validation ====================================//
    //====================== Project Status Validation ====================================//


    var indPrjStatus = $("#ddlProjectStatus").get(0).selectedIndex;
    if (indPrjStatus == 0) {
        $("#spnPrjStatus").show();
        flag = true;       
    }

    //====================== End Project Status Validation ====================================//
    //====================== Project Completion Validation ====================================//

      
    var indPrjCompletion = $("#ddlCompletion").get(0).selectedIndex;
    if (indPrjCompletion == 0) {
        $("#spnPrjCompletion").show();

        flag = true;
    }

    //====================== End Project Completion Validation ====================================//
      
    if (flag == true)
        return false;
    else
        return true;    
}

function fnReset()
{
    $("#spnExtPrjHead").hide();
    $("#spnClientId").hide();
    $("#spnInterPrjHead").hide();
    $("#spnPrjStatus").hide();
    $("#spnPrjCompletion").hide();
    $("#spnTeamMemDetails").hide();
    $("#spnPrjName1").hide();
    $("#spnExtPrjHeadEmail1").hide();
    $("#spnPrjName").hide();
    $("#spnStartDate").hide();
    $("#spnEndDate").hide();
}