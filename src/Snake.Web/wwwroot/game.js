(function (d, w, $) {

    d.addEventListener("DOMContentLoaded", function () {
        return;
        var webSocket = new WebSocket(location.href.replace('http', 'ws'));

        webSocket.onclose = webSocket.onerror = function(e) {
            alert(e);
        }

        webSocket.onmessage = function (r) {

            switch (r.data.substring(0, 1)) {
                case '':

                    break;
            }

        }
    })

})(document, window, document.querySelectorAll);
