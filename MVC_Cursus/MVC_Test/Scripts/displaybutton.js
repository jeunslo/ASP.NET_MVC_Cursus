function toggleClass(el) {
    var login = document.getElementById("login");
    var message = document.getElementById("message");
    if (login.style.display === "none") {
        login.style.display = "block";
    }
    else {
        login.style.display = "none";
    }
    if (message.style.display === "none") {
        message.style.display = "block";
    }
    else {
        message.style.display = "none";
    }
}