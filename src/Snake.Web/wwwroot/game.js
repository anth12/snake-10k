(function (doc, win, $) {

    doc.addEventListener("DOMContentLoaded", function () {
        
        var player,
        $username = $('#U')[0],
        $start = $('#S')[0],
        $form = $('form')[0]
        $canvas = $('canvas')[0],
        context = $canvas.getContext('2d'),
        
        cellWidth = 2,
        cellHeight = 2;

        var webSocket = new WebSocket(location.href.replace('http', 'ws'));

        webSocket.onclose = webSocket.onerror = function(e) {
            alert(e);
        }

        webSocket.onmessage = function (response) {
            var data = JSON.parse(response.data.substring(1, response.data.length));
            
            switch (response.data.substring(0, 1)) {
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
                    context.clearRect(0, 0, $canvas.height, $canvas.width);

                    for (var id in data.snakes) {

                        context.fillStyle = id == player.id ? '#666' : '#fff';

                        for (var index in data.snakes[id]) {
                            var point = data.snakes[id][index]
                            context.fillRect(point.x * cellWidth, point.y * cellHeight, cellWidth, cellHeight);
                        }
                    }
                    break;
            }

        }

        
        $form.addEventListener('submit', function (event) {
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

        window.addEventListener('keyup', function(event) {
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


})(document, window, document.querySelectorAll.bind(document));
