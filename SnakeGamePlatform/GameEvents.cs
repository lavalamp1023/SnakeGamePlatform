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
        GameObject food;

        //This function is called by the game one time on initialization!
        //Here you should define game board resolution and size (x,y).
        //Here you should initialize all variables defined above and create all visual objects on screen.
        //You could also start game background music here.
        //use board Object to add game objects to the game board, play background music, set interval, etc...
        public void GameInit(Board board)
        {

            //Setup board size and resolution!
            Board.resolutionFactor = 1;
            board.XSize = 600;
            board.YSize = 800;

            //Adding a text label to the game board.
            Position labelPosition = new Position(100, 20);
            lblScore = new TextLabel("This is just an example! Use right and left arrows to change direction", labelPosition);
            lblScore.SetFont("Ariel", 14);
            board.AddLabel(lblScore);

            //Adding Game Object
            Position foodPosition = new Position(200, 100);
            food = new GameObject(foodPosition, 20, 20);
            food.SetImage(Properties.Resources.food);
            food.direction = GameObject.Direction.RIGHT;
            board.AddGameObject(food);

            //Play file in loop!
            board.PlayBackgroundMusic(@"\Images\gameSound.wav");
            //Play file once!
            board.PlayShortMusic(@"\Images\eat.wav");


            //Start game timer!
            board.StartTimer(50);
        }
        
        
        //This function is called frequently based on the game board interval that was set when starting the timer!
        //Use this function to move game objects and check collisions
        public void GameClock(Board board)
        {

            Position foodPosition = food.GetPosition();
            if (food.direction == GameObject.Direction.RIGHT)
                foodPosition.Y = foodPosition.Y + 5;
            else if (food.direction == GameObject.Direction.LEFT)
                foodPosition.Y = foodPosition.Y - 5;
            else if (food.direction == GameObject.Direction.UP)
                foodPosition.X = foodPosition.X - 5;
            else if (food.direction == GameObject.Direction.DOWN)
                foodPosition.X = foodPosition.X + 5;
            food.SetPosition(foodPosition);
        }

        //This function is called by the game when the user press a key down on the keyboard.
        //Use this function to check the key that was pressed and change the direction of game objects acordingly.
        //Arrows ascii codes are given by ConsoleKey.LeftArrow and alike
        //Also use this function to handle game pause, showing user messages (like victory) and so on...
        public void KeyDown(Board board, char key)
        {
            if (key == (char)ConsoleKey.LeftArrow)
                food.direction = GameObject.Direction.LEFT;
            if (key == (char)ConsoleKey.RightArrow)
                food.direction = GameObject.Direction.RIGHT;
            if (key == (char)ConsoleKey.UpArrow)
                food.direction = GameObject.Direction.UP;
            if (key == (char)ConsoleKey.DownArrow)
                food.direction = GameObject.Direction.DOWN;
        }
    }
}
