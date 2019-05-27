function getUrlVars()
{
    let vars = location.search.substr(1).split("&");
    urlVars = new Object();
    for (let item of vars)
    {
        let key = item.split("=")[0];
        let value = item.split("=")[1];
        urlVars[key] = value;
    }
    return urlVars;
}

// Вывод ошибок после проверки на стороне сервера
$(document).ready(function () {
    if (getUrlVars().error != null) {
        if (getUrlVars().error == "emptyfields") {
            $("#3").html("Присутствовали пустые поля!");
            $("#3").toggleClass("errorPresent");
        }
        else if (getUrlVars().error == "passwordsunequal") {
            $("#3").html("Пароли не совпадают! Повторите ввод!");
            $("#3").toggleClass("errorPresent");
        }
        else if (getUrlVars().error == "invaliduname") {
            $("#0").html("Используйте только латинские буквы любого регистра, цифры и нижнее подчёркивание!");
            $("#0").toggleClass("errorPresent");
        }
        else if (getUrlVars().error == "invalidemail") {
            $("#1").html("Некорректный адрес электронной почты!");
            $("#1").toggleClass("errorPresent");
        }
        else if (getUrlVars().error == "invalidpass") {
            $("#2").html("Некорректный пароль!");
            $("#2").toggleClass("errorPresent");
        }
        else if (getUrlVars().error == "smallpass") {
            $("#2").html("Пароль должен быть длиннее 5 символов!");
            $("#2").toggleClass("errorPresent");
        }
        else if (getUrlVars().error == "nodigits") {
            $("#2").html("Пароль должен содержать цифры 0-9!");
            $("#2").toggleClass("errorPresent");
        }
        else if (getUrlVars().error == "noletters") {
            $("#2").html("Пароль должен содержать буквы A-Z или a-z!");
            $("#2").toggleClass("errorPresent");
        }
        else if (getUrlVars().error == "userexists") {
            $("#3").html("Такой пользователь уже существует, если это вы, пройдите авторизацию!");
            $("#3").toggleClass("errorPresent");
        }
    }
});

$("#submitButton").click(function (event) {
    var username = $("#uname").val();
    var email = $("#email").val();
    var password = $("#pass").val();
    var passwordRetry = $("#retryPass").val();
    var isErrorOccurs = false;

    var inputVals = [];
    $("input").each(function () {
        inputVals.push($(this).val())
    });

    // Сброс ошибок
    for (let i = 0; i < inputVals.length; i++) {
        $("#" + i).html("");
        $("#" + i).removeClass("errorPresent");
    }

    // Проверка пустых полей
    for (var i = 0; i < inputVals.length; i++) {
        if (inputVals[i] == "") {
            $("#" + i).html("Поле должно быть заполнено!");
            $("#" + i).toggleClass("errorPresent");
            isErrorOccurs = true;
        }
    }

    // Проверка совпадения паролей
    if (password != passwordRetry) {
        if (!$("#3").hasClass("errorPresent")) {
            $("#3").html("Пароли не совпадают! Повторите ввод!");
            $("#3").toggleClass("errorPresent");
            isErrorOccurs = true;
        }
    }

    // Проверка имени пользователя
    let usernameRegex = new RegExp("^\\w+$");
    if (!usernameRegex.test(username)) {
        if (!$("#0").hasClass("errorPresent")) {
            $("#0").html("Используйте только латинские буквы любого регистра, цифры и нижнее подчёркивание!");
            $("#0").toggleClass("errorPresent");
            isErrorOccurs = true;
        }
    }

    // Проверка E-mail
    let emailRegex = new RegExp("^[A-Za-z0-9!#$%&'*+\-\/=?^_`{|}~.]+@[a-z.]+$");
    if (!emailRegex.test(email)) {
        if (!$("#1").hasClass("errorPresent")) {
            $("#1").html("Некорректный адрес электронной почты!");
            $("#1").toggleClass("errorPresent");
            isErrorOccurs = true;
        }
    }

    // Проверка сложности пароля
    let passwordRegex = new RegExp("^[A-Za-z0-9!#$%&'*+\-\/=?^_`{|}~.]+$");
    if (passwordRegex.test(password)) {
        let requiredLength = 5;
        if (password.length < requiredLength) {
            if (!$("#2").hasClass("errorPresent")) {
                $("#2").html(`Пароль должен быть длиннее ${requiredLength} символов!`);
                $("#2").toggleClass("errorPresent");
                isErrorOccurs = true;
            }
        }
        else if (!/\d/.test(password)) {
            if (!$("#2").hasClass("errorPresent")) {
                $("#2").html("Пароль должен содержать цифры 0-9!");
                $("#2").toggleClass("errorPresent");
                isErrorOccurs = true;
            }
        }
        else if (!/[A-Za-z]/.test(password)) {
            if (!$("#2").hasClass("errorPresent")) {
                $("#2").html("Пароль должен содержать буквы A-Z или a-z!");
                $("#2").toggleClass("errorPresent");
                isErrorOccurs = true;
            }
        }
    }
    else {
        if (!$("#2").hasClass("errorPresent")) {
            $("#2").html("Некорректный пароль!");
            $("#2").toggleClass("errorPresent");
            isErrorOccurs = true;
        }
    }
    if (isErrorOccurs)
        event.preventDefault();
});