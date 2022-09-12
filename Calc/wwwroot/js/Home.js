
function addValue(intValue){
    let textBox=document.getElementById("calculatorDisplay").value==="0";
    if (textBox)
    document.getElementById("calculatorDisplay").value=intValue;
    else
    document.getElementById("calculatorDisplay").value+=intValue;
}

function addOperator(value){
    let numberOneEmpty=document.getElementById("numberOne").value.length===0;
    if (numberOneEmpty){
    
    document.getElementById("numberOne").value=document.getElementById("calculatorDisplay").value;
    document.getElementById("calculatorDisplay").value="0";
    document.getElementById("operator").value=value;
    }
    else
    alert("Nelze zadat více než 2 čísla");
}

function clearData(){

    document.getElementById("numberOne").value=null;
    document.getElementById("numberTwo").value=null;
    document.getElementById("operator").value=null;
    document.getElementById("calculatorDisplay").value="0";
}

$(function () {
    $("#btnSubmit").click(function () {
    
        let numberOneEmpty=document.getElementById("numberOne").value.length===0;
        if (numberOneEmpty)
            alert("Musíš zadat nějaké číslo");
        else
        {
            document.getElementById("numberTwo").value=document.getElementById("calculatorDisplay").value;
    
            $.ajax({
                type: "POST",
                url: "/Home/GetCalcResult",
                data: { "numberOne": $("#numberOne").val(),
                    "numberTwo": $("#numberTwo").val(),
                    "mathOperator": $("#operator").val(),
                    "resultIntegers":$("#resultIntegers").is(':checked')
                },
                
                success: function (response) {
                    document.getElementById("calculatorDisplay").value=response;
                    $('#lastCalcResult').DataTable().ajax.reload();
                },
                failure: function (response) {
                    alert(response.responseText);
                },
                error: function (response) {
                    alert(response.responseText);
                }
            });}
    });
});

function GetLastResults(){
    $('#lastCalcResult').DataTable( {
        ajax: {
            url: '/Home/GetCalcResult',
            dataSrc:""
        },
        columns: [
            {data:'CalculationResultId'},
            {data:'Result'}],
        searching: false,
        paging: false,
        info: false
    } );
    }
    $(document).ready(function(){
    GetLastResults();
});

