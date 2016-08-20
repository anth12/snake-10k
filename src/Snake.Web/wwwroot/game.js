(function (doc, win, $) {

    doc.addEventListener("DOMContentLoaded", function () {
        
        var player,
        $username = $('#U')[0],
        $start = $('#S')[0],
        $form = $('form')[0]
        $canvas = $('canvas')[0],
        context = $canvas.getContext('2d');

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
            }

        }

        
        $form.addEventListener('submit', function (event) {
            event.preventDefault()
            player.username = $username.value;
            webSocket.send('U' + prepareRequest({ username: player.username }))

            // Start the game
            hide($form)
            $canvas.className = "";
        })

        function gameEnd() {
            $form.className = "";
            hide($canvas)
        }

        window.addEventListener('keyup', function(event) {
            switch (event.keyCode) {
            case 27: // Esc
                gameEnd();
                break;
            default:
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
