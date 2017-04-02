function checkAll(objRef) {
    var GridView = objRef.parentNode.parentNode.parentNode.parentNode;
    var inputList = GridView.getElementsByTagName("input");
    for (var i = 0; i < inputList.length; i++) {
        //Get the Cell To find out ColumnIndex
        var row = inputList[i].parentNode.parentNode;
        if (inputList[i].type == "checkbox" && objRef
                                        != inputList[i]) {
            if (objRef.checked) {
                //If the header checkbox is checked
                //check all checkboxes
                //and highlight all rows
                // row.style.backgroundColor = "#5CADFF";
                inputList[i].checked = true;
            }
            else {
                //If the header checkbox is checked
                //uncheck all checkboxes
                //and change rowcolor back to original

                //  if (row.rowIndex % 2 == 0) {
                //Alternating Row Color
                //row.style.backgroundColor = "#AED6FF";
                // }
                // else {
                // row.style.backgroundColor = "white";
                // }
                inputList[i].checked = false;
            }
        }
    }
}

function MouseEvents(objRef, evt) {
    var checkbox = objRef.getElementsByTagName("input")[0];
    if (evt.type == "mouseover") {
        objRef.style.backgroundColor = "#C0C8D1";
    }
    else {
        if (checkbox.checked) {
            objRef.style.backgroundColor = "#C0C8D1";
        }
        else if (evt.type == "mouseout") {
            //if (objRef.rowIndex % 2 == 0) {
            //    //Alternating Row Color
            //    objRef.style.backgroundColor = "#C2D69B";
            //}
            //else {
            //    objRef.style.backgroundColor = "white";
            //}
            objRef.style.backgroundColor = "white";
        }
    }
}

function Check_Click(objRef) {
    //Get the Row based on checkbox
    var row = objRef.parentNode.parentNode.parentNode;
    if (objRef.checked) {

        //If checked change color to Aqua
        //row.style.backgroundColor = "#C0C8D1";
    }
    else {
        //If not checked change back to original color
        //if (row.rowIndex % 2 == 0) {
        //    //Alternating Row Color
        //    row.style.backgroundColor = "#C2D69B";
        //}
        //else {
        //    row.style.backgroundColor = "white";
        //}
        // row.style.backgroundColor = "white";
    }

    //Get the reference of GridView
    var GridView = row.parentNode;

    //Get all input elements in Gridview
    var inputList = GridView.getElementsByTagName("input");

    for (var i = 0; i < inputList.length; i++) {
        //The First element is the Header Checkbox
        var headerCheckBox = inputList[0];

        //Based on all or none checkboxes
        //are checked check/uncheck Header Checkbox
        var checked = true;
        if (inputList[i].type == "checkbox" && inputList[i] != headerCheckBox) {
            if (!inputList[i].checked) {
                checked = false;
                break;
            }
        }
    }
    headerCheckBox.checked = checked;
}

function EmptyExcelFile(msg) {
    var v = document.getElementById("ErrorFileMsg");
    if (msg == "") {
        v.innerHTML = "";
    }
    else {
        v.innerHTML = msg;
    }
}