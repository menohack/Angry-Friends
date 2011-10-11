var alpha = 0;
var particles = [];
var radius = 20;

function Particle() {
    var x = 400 * Math.random();
    var y = 400 + radius;
    var velocity = 4 * Math.random() + 0.5;
    var opacity = 1.0;

    // Create visual element for the particle
    var domNode = document.createElementNS("http://www.w3.org/2000/svg", "circle");
    var group = document.getElementById('group');
    group.appendChild(domNode);

    // Set initial position to middle of screen
    domNode.setAttribute("cx", x + radius);
    domNode.setAttribute("cy", y);
    domNode.setAttribute("r", radius);

    // Set colour of element
    var chars = "0123456789abcdef";
    var color = "#";
    for (var i = 0; i < 2; i++) {
        var rnd = Math.floor(16 * Math.random());
        color += chars.charAt(rnd);
    }

    color += "0000";

    domNode.setAttribute("fill", color);

    function draw() {
        y -= velocity;

        if (y < -radius) {
            x = 400 * Math.random();
            y = 400 + radius;
            velocity = 4 * Math.random() + 0.5;
            domNode.setAttribute("cx", x + radius);
        }

        opacity = y / 400.0;

        domNode.setAttribute("opacity", opacity);
        domNode.setAttribute("cy", y);
    }

    return {
        draw: draw
    }
}

var previous = [];

function computeFPSSVG() {
    // FPS
    if (previous.length > 60) {
        previous.splice(0, 1);
    }
    var start = (new Date).getTime();
    previous.push(start);
    var sum = 0;

    for (var index = 0; index < previous.length - 1; index++) {
        sum += previous[index + 1] - previous[index];
    }

    var diff = 1000.0 / (sum / previous.length);

    var result = document.querySelector('#svgPerf');

    result.innerHTML = diff.toFixed() + ' fps';
}

function animateSVG() {
    computeFPSSVG();

    // SVG
    var h1 = (200 + 20 * Math.cos(alpha)).toFixed();
    var h2 = (200 - 20 * Math.cos(alpha)).toFixed();

    svgPath.setAttribute("d", "M0 0 L400 0 L400 200 C 350 " + h2 + " 250 " + h2 + " 200 200 C 150 " + h1 + " 50 " + h1 + " 0 200 Z");

    for (var particle in particles) {
        particles[particle].draw();
    }

    alpha += 0.05;
}

var intervalID;

function stopSVG() {
    launchSVG.innerHTML = "Launch";
    window.clearInterval(intervalID);
    launchSVG.onclick = startSVG;
    previous = [];
}

function startSVG() {
    launchSVG.innerHTML = "Stop";
    launchSVG.onclick = stopSVG;

    intervalID = window.setInterval("animateSVG()", 17);
}

for (var i = 0; i < 100; i++) {
    particles.push(new Particle());
}
var launchSVG = document.getElementById('launchSVG');
var svgPath = document.getElementById('svgPath');
launchSVG.onclick = startSVG;