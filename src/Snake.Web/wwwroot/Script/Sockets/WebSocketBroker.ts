namespace Snake.Sockets{

    export class WebSocketBroker
    {
        constructor(serverUrl: string){

            this.WebSocket = new WebSocket(serverUrl);
            this.WebSocket.onopen  = this.OnOpen;
            this.WebSocket.onmessage = this.OnMessage;
            
            window.onbeforeunload = function() {
                this.WebSocket.onclose = function () {}; // disable onclose handler first
                this.WebSocket.close()
            };
        }

        public ServerUrl: string;
        public WebSocket: WebSocket;

        public OnOpen(event: Event){

        }

        public OnMessage(event: MessageEvent){
            
            var response = <string>event.data;
            var typeSeperatorIndex = response.indexOf(':');
            var type = response.substr(0, typeSeperatorIndex);
            var payload = response.substring(typeSeperatorIndex +1, response.length);

            var data = JSON.parse(payload);
            
            switch(type){
                case 'PlayerDto':
                    Handlers.NewUserHandler.Handle(<ServerDto.PlayerDto>data);
                    break;
                    
                case 'PositionUpdate':
                    Handlers.NewUserHandler.Handle(<ServerDto.PlayerDto>data);
                    break;

                default:
                    throw new Error(`Command ${type} is not recognised`);
            }
        }

        public Send<Data>(data: Data){

            this.WebSocket.send(data);
        }
    }
}
