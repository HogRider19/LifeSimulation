"use strict";

const ENTITYCOLORS = {
    0: "red",
    1: "green",
    2: "blue",
    3: "yellow",
    4: "brown",
};

const SENSITIVITY = 1;
const BASEPOINTSIZE = 2;

var anchorPointX = 0;
var anchorPointY = 0;
var cameraResolution = null;
var spaceSize = 100;

var connection = new signalR.HubConnectionBuilder().withUrl("/Hub").build();

connection.on("ReceiveSpace", function (map) {

    const canvas = document.getElementById('map-canvas');
    const context = canvas.getContext('2d');

    const data = JSON.parse(map);
    
    console.log(data);
    
    cameraResolution = cameraResolution == null ? spaceSize : cameraResolution;
    cameraResolution = cameraResolution < 10 ? 10 : cameraResolution;
    cameraResolution = cameraResolution > spaceSize ? spaceSize : cameraResolution;

    anchorPointX = anchorPointX < 0 ? 0 : anchorPointX;
    anchorPointY = anchorPointY < 0 ? 0 : anchorPointY;

    anchorPointX = anchorPointX >= spaceSize - cameraResolution ? spaceSize - cameraResolution : anchorPointX;
    anchorPointY = anchorPointY >= spaceSize - cameraResolution ? spaceSize - cameraResolution : anchorPointY;
    
    const cellSize = canvas.width / cameraResolution;
    
    context.clearRect(0, 0, canvas.width, canvas.height);
    
    for(let entityIndex = 0; entityIndex < data.length; entityIndex++)
    {
        const x = (data[entityIndex].X - anchorPointX) * canvas.width / cameraResolution;
        const y = (data[entityIndex].Y - anchorPointY) * canvas.height / cameraResolution;
        const pointSize = BASEPOINTSIZE * (spaceSize / cameraResolution);
        const color = ENTITYCOLORS[data[entityIndex].Type];

        context.beginPath();
        context.arc(x, y, pointSize, 0, 2 * Math.PI)
        context.fillStyle = color;
        context.fill();
    }
});

connection.start().then(function () {}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById('map-canvas').addEventListener("wheel", (event) =>
{
    event.preventDefault();

    var resDelta = SENSITIVITY * (event.deltaY > 0 ? 1 : -1)
    cameraResolution += resDelta;

    anchorPointX -= resDelta / 2;
    anchorPointY -= resDelta / 2;
});


///

var isPessPouse = false;
var prevAnchorPointX = 0;
var prevAnchorPointY = 0;
var lastX = 0;
var lastY = 0;

document.getElementById('map-canvas').addEventListener('mousemove', (event) =>
{
    if (!isPessPouse) return;

    var dx = event.offsetX - lastX;
    var dy = event.offsetY - lastY;

    const canvas = document.getElementById('map-canvas');

    var deltaX = Math.ceil(dx * cameraResolution / canvas.width);
    var deltaY = Math.ceil(dy * cameraResolution / canvas.height);

    anchorPointX = prevAnchorPointX - deltaX;
    anchorPointY = prevAnchorPointY - deltaY;
});

document.getElementById('map-canvas').addEventListener('mousedown', (e) => {
    isPessPouse = true;
    lastX = e.offsetX;
    lastY = e.offsetY;
    prevAnchorPointX = anchorPointX;
    prevAnchorPointY = anchorPointY;
});
document.getElementById('map-canvas').addEventListener('mouseup', () => isPessPouse = false);
document.getElementById('map-canvas').addEventListener('mouseout', () => isPessPouse = false);

///

document.getElementById("start-button").addEventListener('click', async () => {
    let response = await fetch('/simulation/start', {
        method: 'POST',
        headers: {'Content-Type': 'application/json;charset=utf-8'},
    });
    if (!response.ok)
        console.error("Simulation start error");
    else
    {
        let info = await response.json();
        spaceSize = info.SpaceWidth;
        console.log("Simulation running! SpaceSize: " + spaceSize);   
    }
});

document.getElementById("stop-button").addEventListener('click', async () => {
    let response = await fetch('/simulation/stop', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json;charset=utf-8' },
    });
    if (!response.ok)
        console.error("Simulation stop error");
    else
        console.log("Simulation stopped");
});

connection.on("CompletionSimulationWorker", function (jsonExc)
{
    const exc = JSON.parse(jsonExc);
    console.error(exc);
    alert(exc.Message);
});


connection.on("SendSimulationInfo", function (jsonInfo) {
    const info = JSON.parse(jsonInfo);

    document.getElementById("point-count").innerText = 'Point:   ' + info.PointCount;
    document.getElementById("meal-count").innerText = 'Meal:   ' + info.MealCount;
    document.getElementById("waste-count").innerText = 'Waste:   ' + info.WasteCount;
    document.getElementById("restart-count").innerText = 'Restart:   ' + info.RestartingCount;
});

/*document.getElementById("config-button").addEventListener("click", async (event) => {
    var mealPerinterval = document.getElementById("meal-per-interval").value;
    var hpForStep = document.getElementById("hp-for-step").value;

    var dataToSend = new Config(Number(mealPerinterval), Number(hpForStep));

    let response = await fetch(`/simulation/config?MealPerInterval=${mealPerinterval}&HpForStep=${hpForStep}`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json;charset=utf-8' },
    });
    if (!response.ok)
        console.error("Simulation config error");
    else
        console.log("Simulation configuring");
});

class Config {
    constructor(MealPerinterval, HpForStep) {
        this.MealPerInterval = MealPerinterval;
        this.HpForStep = HpForStep;
    }
}*/
