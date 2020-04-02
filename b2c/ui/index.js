function addAnchorElement(){
    var divApiElem = document.getElementById("api");
    var anchorDivElement = createAnchorElement();
    divApiElem.appendChild(anchorDivElement);
}

function createAnchorElement(){
    var divElem = document.createElement('div');  

    divElem.setAttribute("align", "center");
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
   
    divElem.appendChild(a);
   return divElem;
}

function hideElementsOfGivenClass(className){
    var elemsOfClass = document.getElementsByClassName(className);
    if(typeof(elemsOfClass) != 'undefined' 
        && elemsOfClass != null
        && elemsOfClass.length > 0){
        var elemToHide = elemsOfClass[0];
        elemToHide.style.display = "none"; 
    }
}

function moveElementsOfGivenClassAsElement(className){
    var elemsOfClass = document.getElementsByClassName(className);
    if(typeof(elemsOfClass) != 'undefined' 
        && elemsOfClass != null
        && elemsOfClass.length > 0){
        var elemToMove = elemsOfClass[0];
        elemToMove.parentNode.appendChild(elemToMove);
    }
}
function findLableForControl(el) {
    var idVal = el.id;
    labels = document.getElementsByTagName('label');
    for( var i = 0; i < labels.length; i++ ) {
        if (labels[i].htmlFor == idVal)
            return labels[i];
    }
}

function addBreaksToDivElements(divElem){
    for (var i=0; i<2; i++) {
    linebreak = document.createElement("br");
    divElem.appendChild(linebreak);
    }
}