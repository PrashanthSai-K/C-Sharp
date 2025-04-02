using System;

namespace Task6
{
    class Game
    {
        public int Point
        { get; set; }

        public delegate void AchievementHandler(int points);

        public event AchievementHandler? Achievement;

        public void IncrementPoint()
        {
            Point++;
            Console.WriteLine($"Point : {Point}");

            if (Point == 10 || Point == 15)
            {
                Achievement?.Invoke(Point);
            }
        }
    }
    class Task
    {
        static void CelebrateFirst(int Point)
        {
            Console.WriteLine("\n🥳 Congrats!! for your achievement");
        }

        static void CelebrateSecond(int Point)
        {
            Console.WriteLine("\n🍾 Congrats!! for your great achievement");
        }

        static void Notify(int Point)
        {
            Console.WriteLine($"You have reached 🎯 {Point} points\n");
        }

        static void Main(string[] args)
        {
            Game game = new Game();
            game.Achievement += CelebrateFirst;
            game.Achievement += Notify;

            game.Point = 0;

            var count = 0;
            while (count < 10)
            {
                game.IncrementPoint();
                count++;
            }

            game.Achievement -= CelebrateFirst;
            game.Achievement -= Notify;

            game.Achievement += CelebrateSecond;
            game.Achievement += Notify;


            while (count < 15)
            {
                game.IncrementPoint();
                count++;
            }

        }
    }
}