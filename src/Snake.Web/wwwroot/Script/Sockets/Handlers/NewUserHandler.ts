namespace Snake.Sockets.Handlers{

    export class NewUserHandler
    {
        public static Handle(player: ServerDto.PlayerDto){

            let starting = GamePlayManager.Current.Player == null;
            GamePlayManager.Current.Player = player;

            if(starting)
                GamePlayManager.Current.PlayerLoaded();
        }

    }
}
