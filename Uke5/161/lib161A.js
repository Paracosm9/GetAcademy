function fixText(text) {
    if (text.charAt(0) == ' ') {
        console.log(1);
        text = sliceText(text, 0);
    }
    if (text.charAt(text.length - 1) == ' ') {
        console.log(2);
        text = sliceText(text, text.length - 1);
    }
    let firstLetter = text.charAt(0).toUpperCase();
    text = sliceText(text, 0);
    text = firstLetter + text.toLowerCase();
    return text;
}

function sliceText(text, indexToRemove) {
    return text = indexToRemove == 0 ? text.slice(indexToRemove + 1) : text.slice(0, indexToRemove)
}

