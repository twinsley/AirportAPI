
function formSubmit() {
    let firstName = document.getElementById("firstName");
    let lastName = document.getElementById("lastName");
    let email = document.getElementById("inputEmail");
    let airport = document.getElementById("airportId");
    let comments = document.getElementById("commentBox")
    let formData = { firstName: firstName.value, lastName: lastName.value, email: email.value, airport: airport.value, comments: comments.value };

    console.log(formData)
    postData("/api/formdata", formData).then((data) => {
        console.log(data); // JSON data parsed by `data.json()` call
    });
}

async function postData(url = "", data = {}) {

    const response = await fetch(url, {
        method: "POST", 
        mode: "cors", 
        cache: "no-cache", 
        credentials: "same-origin", 
        headers: {
            "Content-Type": "application/json",
            
        },
        redirect: "follow",
        referrerPolicy: "no-referrer", 
        body: JSON.stringify(data), 
    });
    return response.json(); 
}

