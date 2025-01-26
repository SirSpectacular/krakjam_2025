import Cookies from 'js-cookie'

function logSubmit(event) {
    event.preventDefault();
    var username = document.getElementById('username').value

    localStorage.setItem("username", username);

    window.location.href = "controller.html";
}

const form = document.getElementById("form");
form.addEventListener("submit", logSubmit);
