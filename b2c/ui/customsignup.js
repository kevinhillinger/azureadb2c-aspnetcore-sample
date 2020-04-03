
function customizeSignUpUI() {
    //Hide the provide the following information label
    var introdivElements = document.getElementsByClassName("intro");
    if(typeof(introdivElements) != 'undefined' && introdivElements != null){
        for (var i=0; i<introdivElements.length; i++) {
            introdivElements.item(i).style.display = "none";
        }
    }

    //Hide help links
    var helpLinkElements = document.getElementsByClassName("helpLink");
    if(typeof(helpLinkElements) != 'undefined' && helpLinkElements != null){
        for (var i=0; i<helpLinkElements.length; i++) {
            helpLinkElements.item(i).style.display = "none";
        }
    }

    var emailElem = document.getElementById("email");
    if(typeof(emailElem) != 'undefined' && emailElem != null){
        var labelForEmailControl = findLabelForControl(emailElem);
        if(typeof(labelForEmailControl) != 'undefined' && labelForEmailControl != null){
            labelForEmailControl.style.display = "none";
        } 
    }

    //hide labels as per requirement
    var newPasswordElem = document.getElementById("newPassword");
    if(typeof(newPasswordElem) != 'undefined' && newPasswordElem != null){
        var labelForNewPasswordControl = findLabelForControl(newPasswordElem);
        if(typeof(labelForNewPasswordControl) != 'undefined' && labelForNewPasswordControl != null){
            labelForNewPasswordControl.style.display = "none";
        } 
    }

    var reenterPasswordElem = document.getElementById("reenterPassword");
    if(typeof(reenterPasswordElem) != 'undefined' && reenterPasswordElem != null){
        var labelForReenterPasswordElemControl = findLabelForControl(reenterPasswordElem);
        if(typeof(labelForReenterPasswordElemControl) != 'undefined' && labelForReenterPasswordElemControl != null){
            labelForReenterPasswordElemControl.style.display = "none";
        } 
    }

     //Center the buttons
     var divButtons = document.getElementsByClassName("buttons");
    if(typeof(divButtons) != 'undefined' && divButtons != null){

       for (var i=0; i<divButtons.length; i++) {
            divButtons[i].setAttribute("align", "center");
       }
    }


    //add the terms condition anchor element in the right place.
    var elemAgreeToTermsAndCondition = document.getElementById("AgreedToTermsAndConditions_v1");
    var parentToelemAgreeToTermsAndCondition = elemAgreeToTermsAndCondition.parentElement;
    parentToelemAgreeToTermsAndCondition.prepend(elemAgreeToTermsAndCondition);
    var anchorElement = createAnchorElement();
    parentToelemAgreeToTermsAndCondition.insertBefore(anchorElement, elemAgreeToTermsAndCondition.nextSibling.nextSibling);

    //add breaks to get the effect needed. Can be done by css as well.
    var divEntries = document.getElementsByClassName("attrEntry");
    if(typeof(divEntries) != 'undefined' && divEntries != null){

       for (var i=0; i<divEntries.length; i++) {
           if(i==0 || i==1 || i==3 || i==4 || i==5){
            addBreaksToDivElements(divEntries[i]);
           }
        }
    }

    dialog.close();
}

function addBreaksToDivElements(divElem){
    for (var i=0; i<2; i++) {
    linebreak = document.createElement("br");
    divElem.appendChild(linebreak);
    }
}

function findLabelForControl(el) {
    var idVal = el.id;
    labels = document.getElementsByTagName('label');
    for( var i = 0; i < labels.length; i++ ) {
        if (labels[i].htmlFor == idVal)
            return labels[i];
    }
}

function createAnchorElement(){
     // Create anchor element. 
     var a = document.createElement('a');  
              
    // Create the text node for anchor element. 
    var link = document.createTextNode("Terms And Conditions"); 
    
    // Append the text node to anchor element. 
    a.appendChild(link);  
    
    // Set the title. 
    a.title = "Terms And Conditions";  
    
    // Set the href property. 
    a.href = "#";  
    a.onclick = function() {
        var dialog = document.getElementById('myDialog'); 
        dialog.showModal();
    };
   
   return a;
}