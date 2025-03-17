function analyzeText(text) {
    return {
        wordCount: getWordCount(text), // Antall ord i teksten, 
        letterCount: getLetterCount(text), // Antall vanlige bokstaver, 
        nonLetterCount: getNonLetterCount(text), // Antall tegn som ikke er bokstaver, 
        mostCommonLetter: getMostCommonLetter(text), // Hvilken bokstav som er mest brukt, 
        longestWord: getLongestWord(text) // Det lengste ordet
    }
}

function isLetter(character) {
    return character.toLowerCase() != character.toUpperCase();
}

function getWordCount(text) {
    text = clearText(text);
    let splittedText = text.split(' ');
    let amountOfWords = 0; 
    for (word of splittedText) {
        if (word.length > 0){
            amountOfWords++; 
        }
    }
    return amountOfWords; 
}

function getLetterCount(text){
    let letterCount = 0; 
    for (let i = 0; i < text.length; i++){
        if (isLetter(text.charAt(i))){
            letterCount++; 
        }
    }
    return letterCount; 
}

function getNonLetterCount(text){
    let nonLetterCount = 0; 
    for (let i = 0; i < text.length; i++){
        if (!isLetter(text.charAt(i))){
            nonLetterCount++; 
        }
    }
    return nonLetterCount; 
}

function getMostCommonLetter(text){
    let frequenciesOfLetters = createMap(text); 
    let letterThatOccursMost = ['', 0]; 
    let maxAmountOfLetter = letterThatOccursMost[1]; 
    //iterations through map gives us small arrays like letter = ['e',5]; 
    for( let letter of frequenciesOfLetters){     
        if (maxAmountOfLetter < letter[1]){
            maxAmountOfLetter = letter[1]; 
            letterThatOccursMost = letter; 
        }
    }
    return letterThatOccursMost[0]; 
}

function createMap(text){
    //I've decided to choose Map as an storage, because it can hold keys of any type, and err.. just the best structure to
    //hold the letter - maxAmountOfLetters pair, aka key:value. Keys are unique, so are letters for this task.
    //I know, that there was a possibility creating double arrays and calculate frequency and blah-blah-blah, but Map is better. 
    //https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/Map
    let lettersMap = new Map(); 
    for (let i = 0; i < text.length; i++){
        let letterName = text.charAt(i).toLowerCase(); 
        let startAmount = 1; 
        if(isLetter(letterName)){
            if (lettersMap.get(letterName) == undefined){
                lettersMap.set(letterName, startAmount); 
            }
            else {
                lettersMap.set(letterName, lettersMap.get(letterName) + 1); 
            }
        }
    }
    return lettersMap; 
}

function getLongestWord(text){
    text = clearText(text); 
    let splittedText = text.split(' '); 
    let maxLength = 0;
    let index = 0; 
    let indexOfLongestWord = 0; 
    for (word of splittedText){
        if (word.length > maxLength){
            maxLength = word.length; 
            indexOfLongestWord = index; 
        }
        index++; 
    }
    return splittedText[indexOfLongestWord].toLowerCase(); 
}

function clearText(text){
    let characters = text.split('');
    let clearedCharacters = []; 
    for (let i = 0; i < characters.length; i++){
        if (characters[i] == ' ' || isLetter(characters[i])){
            clearedCharacters.push(characters[i]); 
        }
    }
    return clearedCharacters.join(''); 
}
