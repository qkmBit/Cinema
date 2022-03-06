$(function(){
    $("#phoneInp").mask("+7 999 999 99 99", {placeholder:""});
});
function authButtonClick(){
    
    if(document.getElementById("phoneInp").value=="" && document.getElementById("passInp").value==""){
        document.getElementById("errNote").innerHTML="Необходимо заполнить поля \"Номер телефона\" и \"Пароль\".";
    }
    else if(document.getElementById("phoneInp").value=="" || document.getElementById("passInp").value==""){
        if(document.getElementById("phoneInp").value=="")
            document.getElementById("errNote").innerHTML="Необходимо заполнить поле \"Номер телефона\".";
        else document.getElementById("errNote").innerHTML="Необходимо заполнить поле \"Пароль\"."
    }
    else{
        document.getElementById("errNote").innerHTML="";
        //Авторизация
    }
}
function registerClick(){
    document.location.href="register.html"
}