
    function fnValidation()
    {              
        var deptIndex = $("#ddlDepartment").get(0).selectedIndex;
        if (deptIndex == 0)
            $("#spnDept").show();        
                
        var desigtIndex = $("#ddlDesignation").get(0).selectedIndex;
        if (desigtIndex == 0) 
            $("#spnDesig").show();        
        
        var reptHeadIndex = $("#ddlReportingHead").get(0).selectedIndex;
        if (reptHeadIndex == 0) 
            $("#spnReptHead").show();
              
    }
function fnReset() {
    $("#spnDept").hide();
    $("#spnDesig").hide();
    $("#spnReptHead").hide();
}

