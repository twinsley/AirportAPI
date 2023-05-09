
function formSubmit() {
    let firstName = document.getElementById("firstName");
    let lastName = document.getElementById("lastName");
    let email = document.getElementById("inputEmail");
    let airport = document.getElementById("airportId");
    let comments = document.getElementById("commentBox")
    let formData = { firstName: firstName.value, lastName: lastName.value, email: email.value, airport: airport.value, comments: comments.value };

    console.log(formData)
    postData("/api/form", formData).then((data) => {
        console.log(data); // JSON data parsed by `data.json()` call
    });
}
async function postData(url = "", data = {}) {

    const response = await fetch(url, {
        method: "POST", // *GET, POST, PUT, DELETE, etc.
        mode: "cors", // no-cors, *cors, same-origin
        cache: "no-cache", // *default, no-cache, reload, force-cache, only-if-cached
        credentials: "same-origin", // include, *same-origin, omit
        headers: {
            "Content-Type": "application/json",
            
        },
        redirect: "follow", // manual, *follow, error
        referrerPolicy: "no-referrer", // no-referrer, *no-referrer-when-downgrade, origin, origin-when-cross-origin, same-origin, strict-origin, strict-origin-when-cross-origin, unsafe-url
        body: JSON.stringify(data), // body data type must match "Content-Type" header
    });
    return response.json(); // parses JSON response into native JavaScript objects
}

