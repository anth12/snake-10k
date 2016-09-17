(function (doc, win, $, addEventListener) {

    doc.addEventListener("DOMContentLoaded", function () {
        
        var player,
            $username = $('#U')[0],
            $start = $('#S')[0],
            $form = $('form')[0]
            $canvas = $('canvas')[0],
            context = $canvas.getContext('2d'),
            cellWidth = 10,
            cellHeight = 10,
            gameWidth = 5000,
            gameHeight = 5000;

        $canvas.height = gameHeight;
        $canvas.width = gameWidth;

        var webSocket = new WebSocket('ws://' + location.host);

        webSocket.onclose = webSocket.onerror = function(e) {
            alert(e);
        }

        webSocket.onmessage = function (response) {
            var responseData = response.data;

            var data = JSON.parse(responseData.slice(1));
            
            switch (response.data[0]) {
                case 'U':
                    /**************************************
                    ***** Username changed ****************
                    ***************************************/
                    player = data;
                    $username.value = player.username;

                    break;

                case 'P': // Position Update
                    /**************************************
                    ***** Draw the canvas *****************
                    ***************************************/
                    context.clearRect(0, 0, $canvas.width, $canvas.height);
                    
                    for (var id in data.snakes) {

                        var snake = data.snakes[id];

                        context.fillStyle = '#9e9e9e';

                        if (id == player.id) {
                            context.fillStyle = '#3f51b5';

                            var head = snake[snake.length -1]; // Or slice(-1)[0]
                            var sectionX = Math.floor((head.x * cellWidth) / window.innerWidth)
                            var sectionY = Math.floor((head.y * cellHeight) / window.innerHeight)

                            $canvas.style.marginTop = -window.innerHeight * sectionY + "px";
                            $canvas.style.marginLeft = -window.innerWidth * sectionX + "px";
                        }

                        for (var index in snake) {
                            var point = snake[index]
                            context.fillRect(point.x * cellWidth, point.y * cellHeight, cellWidth, cellHeight);
                        }
                    }
                    break;

                case 'H':
                    /**************************************
                    ***** Highscore update ****************
                    ***************************************/
                    $('table')[0].innerHTML = data.html

                    break;
            }

        }

        
        $form[addEventListener]('submit', function (event) {
            event.preventDefault()
            player.username = $username.value;
            webSocket.send('U' + prepareRequest({ username: player.username }))

            // Start the game
            gameStart()
        })

        function gameEnd() {
            $form.className = ""
            hide($canvas)
            webSocket.send('S' + prepareRequest({ playing: false }))
        }

        function gameStart() {
            hide($form)
            $canvas.className = ""
            webSocket.send('S' + prepareRequest({ playing: true }))
        }

        window[addEventListener]('keyup', function(event) {
            switch (event.keyCode) {
            case 27: // Esc
                gameEnd();
                break;
            default:
                webSocket.send('K' + prepareRequest({ code: event.keyCode }))
            }
        });

        function prepareRequest(data) {
            data.user = player.id
            data.token = player.token
            return JSON.stringify(data)
        }

        function hide(elm) {
            elm.className = 'x';
            setTimeout(function() {
                elm.className = 'X';
            }, 800)
        }
    })


})(document, window, document.querySelectorAll.bind(document), "addEventListener");
