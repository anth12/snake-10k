namespace Snake.Sockets.ServerDto{

    export class PlayerDto
    {
        public Id: string;
        public Token: string;
        public Username: string
        public Snake: Common.Snake;
        public HighScore: number;
    }
}
