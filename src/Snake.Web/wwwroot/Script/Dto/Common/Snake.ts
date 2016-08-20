namespace Snake.Sockets.Common{

    export class Snake
    {
        public HeadPoint: Point;
        public Body: Array<Point>;
        public Direction: Direction
        
        public DirectionQueue: Array<Direction>;
        public Length: number;
    }
}
