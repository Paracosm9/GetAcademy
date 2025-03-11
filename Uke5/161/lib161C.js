function isEmail(text){
    console.log(text.length - 1, text, text.indexOf('@') );
    return (text.indexOf('@') > 0 && text.indexOf('@') != text.length - 1);  
}