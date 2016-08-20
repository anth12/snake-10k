namespace Snake{

    document.addEventListener("DOMContentLoaded", function(){
        
        var canvas = <HTMLCanvasElement>document.getElementById("GameCanvas");
        var webSocketBroker = new Sockets.WebSocketBroker("ws://localhost:55745/");

        GamePlayManager.Current = new GamePlayManager(webSocketBroker);

        setTimeout(()=> {
            var body = document.getElementsByTagName('body')[0];
            body.className = body.className.replace('loading', '');
        }, 2000);
    });

    

}
