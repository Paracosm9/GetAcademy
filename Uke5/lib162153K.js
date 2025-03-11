     // model
     let numbers = [7, 3, 2, 5, 8];
     let barsSelected = createBarSelectedTable(); // -1
     let chosenBar; // Variabel for hvilken stolpe som er valgt
     let inputValue; // Variabel for hva som er skrevet i input-feltet
     let svgInnerHtml;

     // view
     updateView();
     function updateView() {
    
             svgInnerHtml = '';
             for (let i = 0; i < numbers.length; i++) {
                 svgInnerHtml += createBar(numbers[i], i + 1);
             }
             document.getElementById('content').innerHTML = /*HTML*/`
             <svg id="chart" width="500" viewBox="0 0 80 60">
                 ${svgInnerHtml}
             </svg><br/>
             Valgt stolpe: <i id = "textSelected">${chosenBar > -1 ? (chosenBar + 1) :  "ingen"}</i>
             <br />
             Verdi:
             <input id = "numField" type="number" min="1" max="10" oninput="inputValue = this.value" />

             <button id = "addButton" onclick = "addBar()">Legg til stolpe</button><br />
             <button id = "chButton" onclick = "changeBar()" disabled>Endre valgt stolpe</button><br />
             <button id = "delButton" onclick = "removeBar()" disabled>Fjerne valgt stolpe</button>
         `;
     }

     function createBar(number, barNo) {
         const width = 8;
         const spacing = 2;
         let x = (barNo - 1) * (width + spacing);
         let height = number * 10;
         let y = 60 - height;
         let color = calcColor(1, 10, barNo);

         return /*HTML*/`<rect id = "${barNo}" onclick = "selectBar(${barNo})" width="${width}" height="${height}"
                             x="${x}" y="${y}" fill="${color}"></rect>`;
     }

     function calcColor(min, max, val) {
         var minHue = 240, maxHue = 0;
         var curPercent = (val - min) / (max - min);
         var colString = "hsl(" + ((curPercent * (maxHue - minHue)) + minHue) + ",100%,50%)";
         return colString;
     }

     function updateTextAndButtons(message) {
         chosenBar = returnSelectedBar() + 1; 
         if (chosenBar >= 0 && !message){
             
             document.getElementById("textSelected").innerHTML = chosenBar;
             document.getElementById("chButton").disabled = false; 
             document.getElementById("delButton").disabled = false; 
         }
         else if (chosenBar == -1){
             document.getElementById("textSelected").innerHTML = "ingen"; 
             document.getElementById("chButton").disabled = true; 
             document.getElementById("delButton").disabled = true; 
         }

         if (message) { 
             document.getElementById("textSelected").innerHTML = message; 
         }
     }
     

     // controller (ingenting her ennå)
     function createBarSelectedTable(){
         let arr = [];
         for (let index = 0; index < numbers.length; index++) {
             arr.push(0);
         }
         return arr; 
     }
     function selectBar(barNumber) {
         let selectedRect = document.getElementById(barNumber.toString());
         for (let i = 0; i < numbers.length; i++) {
             if (i == barNumber - 1 && barsSelected[i] != 1 && (returnSelectedBar() < 0)) {
                 barsSelected[i] = 1;
                 selectedRect.style.stroke = "black";
                 continue;
             }
             if (i == barNumber - 1 && barsSelected[i] == 1) {
                 barsSelected[i] = 0;
                 selectedRect.style.stroke = "";
                 continue;
             }
         }
         updateTextAndButtons();
         return barsSelected; 
     }

     function removeBar(){
         let barToRemoveIndex = returnSelectedBar();
         let firstPart = numbers.slice(0,barToRemoveIndex); 
         let secondPart = numbers.slice(barToRemoveIndex + 1); 
         let concatedArray = firstPart.concat(secondPart); 
 
         numbers = [...concatedArray]; 
         barsSelected = createBarSelectedTable(); 
         chosenBar = 0; 
         updateTextAndButtons(); 
         updateView();
         return barsSelected; 
     }

     function changeBar(){   
         if (isInputOutOfBorders()){
             updateTextAndButtons("Error: value should be between 1 and 10"); 
             return;
         }
         let barToChangeIndex = returnSelectedBar();
         numbers[barToChangeIndex] = parseInt(inputValue); 
         barsSelected = createBarSelectedTable(); 
         chosenBar = 0; 
         inputValue = 0;
         updateTextAndButtons(); 
         updateView();
         return numbers; 
     }

     function addBar(){
         if (isInputOutOfBorders()){
             updateTextAndButtons("Error: value should be between 1 and 10");   
             return;
         }
         if(numbers.length > 15){
             updateTextAndButtons("Error: max amount of bars is 16, according to SVG settings."); 
             return;
         }
         numbers.push(parseInt(inputValue)); 
         barsSelected = createBarSelectedTable(); 
         chosenBar = 0; 
         inputValue = 0; //comment to add faster)
         updateTextAndButtons(); 
         updateView();
         return numbers; 
     }
     

     function returnSelectedBar() {
         for (let index = 0; index < barsSelected.length; index++) {
             if (barsSelected[index] > 0) {
                 return index; 
             }
         }
         return -1; 
     }
     function isInputOutOfBorders(){
         return (inputValue <= 0 || inputValue > 10 || !inputValue);  
     }