var isDrawPressed = false;
var globalCoordinateX, globalCoordinateY, context2D, canvas;
let chalk = (x, y, isCursorPressed) => {
    if (isCursorPressed) {
        context2D.beginPath();
        context2D.lineWidth = 10;
        context2D.moveTo(globalCoordinateX, globalCoordinateY);
        context2D.lineTo(x, y);
        context2D.closePath();
        context2D.stroke();
    }
    globalCoordinateX = x; globalCoordinateY = y;
}
(function() {
    connection = new signalR.HubConnectionBuilder().withUrl("/drawBoardHub").build();
    canvas = document.getElementById("canvas");
    context2D = canvas.getContext("2d");

    canvas.addEventListener("mousedown", e => {
        isDrawPressed = true;
        connection.invoke("SendCoordenates", e.pageX - canvas.offsetLeft, e.pageY -  canvas.offsetTop, false);
    }, false);

    canvas.addEventListener("mousemove", e => {
        if (isDrawPressed) {
            connection.invoke("SendCoordenates", e.pageX - canvas.offsetLeft, e.pageY -  canvas.offsetTop, true);
        }
    }, false);

    canvas.addEventListener("mouseup", e => isDrawPressed = false, false);    
    canvas.addEventListener("mouseleave", e => isDrawPressed = false, false);

    document.getElementById("clear").addEventListener("click", e => {
        context2D.clearRect(0, 0, context2D.canvas.width, context2D.canvas.height);
    });

    connection.on("ReceiveCoordenates", function (x, y, isCursorPressed) {
      chalk(x, y, isCursorPressed);
    });

    connection.start().catch(function (err) {
      return console.error(err.toString());
    });
})();