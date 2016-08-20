namespace Snake{

    export class GamePlayManager
    {
        constructor(webSocketBroker: Sockets.WebSocketBroker){

            this.WebSocketBroker = webSocketBroker;
        }

        public static Current: GamePlayManager;

        public WebSocketBroker: Sockets.WebSocketBroker;
        public Player: Sockets.ServerDto.PlayerDto;

        public PlayerLoaded(){
            (<HTMLInputElement>document.getElementById('Username')).value = this.Player.Username;

        }

    }
}
