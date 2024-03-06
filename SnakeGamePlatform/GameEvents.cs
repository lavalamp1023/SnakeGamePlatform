using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Media;
using WMPLib;

namespace SnakeGamePlatform
{
    
    public class GameEvents:IGameEvents
    {
        //Define game variables here! for example...
        //GameObject [] snake;
        TextLabel lblScore;
        GameObject snake;
        GameObject food;
        Random r;
        int score;
        //This function is called by the game one time on initialization!
        //Here you should define game board resolution and size (x,y).
        //Here you should initialize all variables defined above and create all visual objects on screen.
        //You could also start game background music here.
        //use board Object to add game objects to the game board, play background music, set interval, etc...
        public void GameInit(Board board)
        {
            InitFood(board);

            //Setup board size and resolution!
            Board.resolutionFactor = 1;
            board.XSize = 600;
            board.YSize = 800;

            
            //Adding Game Object
            Position snakePosition = new Position(200, 100);
            snake = new GameObject(snakePosition, 20, 20);
            snake.SetImage(Properties.Resources.food);
            snake.direction = GameObject.Direction.RIGHT;
            board.AddGameObject(snake);

            //Play file in loop!
            board.PlayBackgroundMusic(@"\Images\gameSound.wav");
            


            //Start game timer!
            board.StartTimer(50);
        }
        
        //הפעולה בודקת אם ראש הנחש נוגע באוכל
        //אם נוגע, אז:
        //מעדכנים את הניקוד של השחקן
        // מזיזים את האוכל למקום אחר
        // לנגן מוזיקת אכילה
        void HandleSnakeEatsnake(Board board)
        {
            if(food.IntersectWith(snake))
            {
                score++;
                lblScore.SetText(score.ToString());
                int x = r.Next(40, 500);
                int y = r.Next(40, 700);
                Position foodPosition = new Position(x, y);
                food.SetPosition(foodPosition);
                //Play file once!
                board.PlayShortMusic(@"\Images\eat.wav");

            }

                
        }
        //הפעולה יוצרת את האוכל
        //וממקמת אותו בפעם הראונה

        void InitFood(Board board)
        {
            Position labelPosition = new Position(20, 20);
            lblScore = new TextLabel("0", labelPosition);
            lblScore.SetFont("Ariel", 14);
            board.AddLabel(lblScore);

            r = new Random();
            score = 0;
            int x = r.Next(40, 500);
            int y = r.Next(40, 700);
            Position foodPosition = new Position(x, y);
            food = new GameObject(foodPosition, 20, 20);
            food.SetImage(Properties.Resources.food);
            board.AddGameObject(food);

        }
        //This function is called frequently based on the game board interval that was set when starting the timer!
        //Use this function to move game objects and check collisions
        public void GameClock(Board board)
        {
            HandleSnakeEatsnake(board);
            Position snakePosition = snake.GetPosition();
            if (snake.direction == GameObject.Direction.RIGHT)
                snakePosition.Y = snakePosition.Y + 5;
            else if (snake.direction == GameObject.Direction.LEFT)
                snakePosition.Y = snakePosition.Y - 5;
            else if (snake.direction == GameObject.Direction.UP)
                snakePosition.X = snakePosition.X - 5;
            else if (snake.direction == GameObject.Direction.DOWN)
                snakePosition.X = snakePosition.X + 5;
            snake.SetPosition(snakePosition);
        }

        //This function is called by the game when the user press a key down on the keyboard.
        //Use this function to check the key that was pressed and change the direction of game objects acordingly.
        //Arrows ascii codes are given by ConsoleKey.LeftArrow and alike
        //Also use this function to handle game pause, showing user messages (like victory) and so on...
        public void KeyDown(Board board, char key)
        {
            if (key == (char)ConsoleKey.LeftArrow)
                snake.direction = GameObject.Direction.LEFT;
            if (key == (char)ConsoleKey.RightArrow)
                snake.direction = GameObject.Direction.RIGHT;
            if (key == (char)ConsoleKey.UpArrow)
                snake.direction = GameObject.Direction.UP;
            if (key == (char)ConsoleKey.DownArrow)
                snake.direction = GameObject.Direction.DOWN;
        }
    }
}
