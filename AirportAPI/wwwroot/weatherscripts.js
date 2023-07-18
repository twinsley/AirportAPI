function processData(response) {
  let counter = 0;
  let parsedData = JSON.parse(response);
  let windSpeeds = 0;
  let windDirections = 0;
  let temperatures = 0;
  let humidityLevels = 0;
  let lastDate;
  let minWindSpeed = -1;
  let maxWindSpeed = 0;
  let avgWindSpeed = 0;
  let avgDirection = 0;
  let avgTemperature = 0;
  let avgHumidity = 0;
  let airport;
  parsedData.forEach((element) => {
    console.log(element);
    windSpeeds += parseFloat(element.windspeed);
    windDirections += parseFloat(element.winddirection);
    temperatures += parseFloat(element.temperature);
    humidityLevels += parseFloat(element.humidity);
    if (element.date > lastDate) {
      lastDate = element.date;
    }

    if (element.windspeed < minWindSpeed || minWindSpeed == -1) {
      minWindSpeed = Math.round(parseFloat(element.windspeed));
    }
    if (element.windspeed > maxWindSpeed) {
      maxWindSpeed = Math.round(parseFloat(element.windspeed));
    }
    airport = element.identifier;
    counter += 1;
  });

  avgDirection = Math.round(windDirections / counter);
  avgWindSpeed = Math.round(windSpeeds / counter);
  avgTemperature = Math.round(temperatures / counter);
  avgHumidity = Math.round(humidityLevels / counter);

  let today = new Date();

  let direction = parseInt(avgDirection) + 90;
  let rotate = document.getElementById("pointer");
  rotate.style.transform = `translate(-50%, -50%) rotate(${direction}deg)`;
  let gusting = document.getElementById("gusting");
  let gustingText = document.getElementById("gustingText");
  let pointer = document.getElementById("pointer");
  if (maxWindSpeed <= 10) {
    pointer.style.backgroundImage =
      "url('images/green-sticker-arrow-3-doubled.png')";
  } else {
    pointer.style.backgroundImage =
      "url('images/red-sticker-arrow-3-doubled.png')";
  }
  if (maxWindSpeed < minWindSpeed + 10) {
    gusting.innerHTML = ``;
    gustingText.innerHTML = `Calm`;
    gustingText.style.backgroundColor = null;
    gusting.style.backgroundColor = null;
  }
  if (maxWindSpeed >= minWindSpeed + 10) {
    if (selectedUnits == "mph") {
      gusting.innerHTML = `${maxWindSpeed}mph`;
    } else if (selectedUnits == "kts") {
      gusting.innerHTML = `${Math.round(
        maxWindSpeed * KTS_CONVERSION_RATE
      )}kts`;
    }
    gustingText.innerHTML = `Gusting`;
    gustingText.style = "background-color: yellow;";
    gusting.style = "background-color: yellow;";
  }
  if (maxWindSpeed >= minWindSpeed + 20) {
    if (selectedUnits == "mph") {
      gusting.innerHTML = `${maxWindSpeed}mph`;
    } else if (selectedUnits == "kts") {
      gusting.innerHTML = `${Math.round(
        maxWindSpeed * KTS_CONVERSION_RATE
      )}kts`;
    }
    gustingText.innerHTML = `Gusting`;
    gustingText.style = "background-color: red;";
    gusting.style = "background-color: red;";
  }
  if (selectedUnits == "mph") {
    document.getElementById("avgWindspeed").innerHTML = `${avgWindSpeed}mph`;
  } else if (selectedUnits == "kts") {
    document.getElementById("avgWindspeed").innerHTML = `${Math.round(
      avgWindSpeed * KTS_CONVERSION_RATE
    )}kts`;
  }
  document.getElementById("windDirection").innerHTML = `${avgDirection}&#176`;
  document.getElementById("temperature").innerHTML = `${avgTemperature}&#x2109`;
  document.getElementById("humidity").innerHTML = `${avgHumidity}%`;
  document.getElementById("airport").innerHTML = airport;
  document.getElementById("timestamp").innerHTML =
    today.getHours() + ":" + today.getMinutes() + ":" + today.getSeconds();
}

updateData();
setInterval(updateData, 30000);
let resJson;
let res;
let selectedUnits = "mph";
const KTS_CONVERSION_RATE = 0.868976;

function switchUnit() {
    currentVal = selectedUnits;
    if (currentVal == "mph") {
        selectedUnits = "kts";
        document.getElementById("unitSwitcher").innerHTML = "Switch to MPH"
    } else if (currentVal == "kts") {
        selectedUnits = "mph";
        document.getElementById("unitSwitcher").innerHTML = "Switch to kts"
    }
    updateData();
}


function updateData() {
    let xhttp = new XMLHttpRequest();
    xhttp.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            res = this.responseText;
            processData(res);

        }
    };
    xhttp.open("GET", "/api/wx/42i", true);

    xhttp.send();
}
