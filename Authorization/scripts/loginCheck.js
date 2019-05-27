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

$(document).ready(function () {
    if (getUrlVars().error != null) {
        if (getUrlVars().error == "emptyfields") {
            $("#1").html("Присутствовали пустые поля!");
            $("#1").toggleClass("errorPresent");
            $("#userID").val(getUrlVars().userID);
        }
        else if (getUrlVars().error == "nosuchuser") {
            $("#0").html("Такого пользователя не существует!");
            $("#0").toggleClass("errorPresent");
            $("#userID").val(getUrlVars().userID);
        }
        else if (getUrlVars().error == "wrongpass") {
            $("#1").html("Неверный пароль!");
            $("#1").toggleClass("errorPresent");
            $("#userID").val(getUrlVars().userID);
        }
        else if (getUrlVars().error == "loginfailed") {
            $("#1").html("Вы не были авторизованы!");
            $("#1").toggleClass("errorPresent");
        }
    }
    else if (getUrlVars().signup == "success") {
        $("#1").html("Регистрация прошла успешно!");
        $("#1").toggleClass("successMessage");
        $("#userID").val(getUrlVars().userID);
    }
});