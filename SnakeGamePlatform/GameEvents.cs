﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Media;
using WMPLib;
using System.Net.Mail;
using System.Net;
using System.Diagnostics;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrayNotify;
using System.Reflection;

namespace SnakeGamePlatform
{

    public class GameEvents : IGameEvents
    {
        //Define game variables here! for example...
        //GameObject [] snake;
        TextLabel failMessage;
        GameObject borderUp;
        GameObject borderRight;
        GameObject borderLeft;
        GameObject borderDown;
        
        GameObject[] snake = new GameObject[14200];
        int snakeSize;
        GameObject backgroundobj;


  
        TextLabel lblScore;
        GameObject food;
        
        Position borderUP = new Position(10, 20);

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


            Position snakePosition = new Position(200, 100);
            snake[0] = new GameObject(snakePosition, 20, 20);
            snake[0].SetImage(Properties.Resources.food);
            snake[0].direction = GameObject.Direction.RIGHT;
            board.AddGameObject(snake[0]);

            Position snake1Position = new Position(200, 80);
            snake[1] = new GameObject(snake1Position, 20, 20);
            snake[1].SetImage(Properties.Resources.food);
            snake[1].direction = GameObject.Direction.RIGHT;
            board.AddGameObject(snake[1]);
            snakeSize = 2;

            Position borderUPpos = new Position(100, 60);
            borderUp = new GameObject(borderUPpos, 650, 10);

            Position borderRightpos = new Position(100, 710);
            borderRight = new GameObject(borderRightpos, 10, 400);

            Position borderLeftpos = new Position(100, 60);
            borderLeft = new GameObject(borderLeftpos, 10, 400);

            Position borderDownpos = new Position(490, 60);
            borderDown = new GameObject(borderDownpos, 650, 10);
            //borderUpp = new GameObject(borderUP, 20, 30);

            borderUp.SetBackgroundColor(Color.Black);
            board.AddGameObject(borderUp);

            borderRight.SetBackgroundColor(Color.Black);
            board.AddGameObject(borderRight);

            borderLeft.SetBackgroundColor(Color.Black);
            board.AddGameObject(borderLeft);

            borderDown.SetBackgroundColor(Color.Black);
            board.AddGameObject(borderDown);

            //Setup board size,background and resolution!
            Board.resolutionFactor = 1;
            board.XSize = 600;
            board.YSize = 800;
            board.SetBackgroundImage(Properties.Resources.jungle_background);
            board.SetBackgroundColor(Color.DarkOliveGreen);
            Position backgroundPosition = new Position(100, 60);
            backgroundobj = new GameObject(backgroundPosition, 650,390);
            backgroundobj.SetImage(Properties.Resources.green_background);
            board.AddGameObject(backgroundobj);

            //Adding Game Object


            //Play file in loop!
            board.PlayBackgroundMusic(@"\Images\background.wav");

            //Start game timer!
            board.StartTimer(100);            

           
        }
        
        //הפעולה בודקת אם ראש הנחש נוגע באוכל
        //אם נוגע, אז:
        //מעדכנים את הניקוד של השחקן
        // מזיזים את האוכל למקום אחר
        // לנגן מוזיקת אכילה
        void HandleSnakeEatsnake(Board board)
        {
            if (food.IntersectWith(snake[0]))
            {
                score++;
                lblScore.SetText($"Yoinks: {score.ToString()}");
                snake[snakeSize] = GenrateNewSnakePart(snake[snakeSize - 1].GetPosition().X, snake[snakeSize -1].GetPosition().Y);
                board.AddGameObject(snake[snakeSize]);
                board.AddGameObject(backgroundobj);
                snakeSize++;
                int x = r.Next(120, 460);
                int y = r.Next(60, 680);
                //for (int i = 1; i < snakeSize; i++)
                //{
                //    while (food.IntersectWith(snake[i]))
                //    {
                //        x = r.Next(120, 450);
                //        y = r.Next(70, 670);
                //    }
                //}
                Position foodPosition = new Position(x, y);
                food.SetPosition(foodPosition);
                //Play file once!
                board.PlayShortMusic(@"\Images\yonik.wav");
            }
        }
        //הפעולה יוצרת את האוכל
        //וממקמת אותו בפעם הראונה

        void InitFood(Board board)
        {
            Position labelPosition = new Position(20, 20);
            lblScore = new TextLabel("Yoinks: 0", labelPosition);
            lblScore.SetFont("Ariel", 30);
            board.AddLabel(lblScore);

            r = new Random();
            score = 0;
            int x = r.Next(120, 460);
            int y = r.Next(60, 680);
            //for (int i = 1; i < snakeSize; i++)
            //{ 
            //    while (food.IntersectWith(snake[i]))
            //    {
            //        x = r.Next(120, 460);
            //        y = r.Next(60, 680);
            //    }
            //}
            Position foodPosition = new Position(x, y);
            food = new GameObject(foodPosition, 20, 20);
            food.SetImage(Properties.Resources.food);
            board.AddGameObject(food);

        }

        //makes a new snake part
        GameObject GenrateNewSnakePart(int lastSnakePartX,int lastSnakePartY)
        {
            GameObject snake;
            Position snakePosition = new Position(lastSnakePartX, lastSnakePartY);
            snake = new GameObject(snakePosition, 20, 20);
            snake.SetImage(Properties.Resources.food);
            snake.direction = GameObject.Direction.RIGHT;
            return snake;
        }

        //moves the snake
        void SnakeMovment()
        {
            for (int i = snakeSize-1;i > 0;i--)
            {
                Position snakePositions = snake[i-1].GetPosition();
                snake[i].SetPosition(snakePositions);
                
            }
            Position snakePosition = snake[0].GetPosition();
            if (snake[0].direction == GameObject.Direction.RIGHT)
                snakePosition.Y = snakePosition.Y + 20;
            if (snake[0].direction == GameObject.Direction.LEFT)
                snakePosition.Y = snakePosition.Y - 20;
            if (snake[0].direction == GameObject.Direction.UP)
                snakePosition.X = snakePosition.X - 20;
            if (snake[0].direction == GameObject.Direction.DOWN)
                snakePosition.X = snakePosition.X + 20;
            snake[0].SetPosition(snakePosition);

        }

        // checks if the player lost
        void LoseCondition(Board board)
        {
            if (borderUp.IntersectWith(snake[0]) || borderRight.IntersectWith(snake[0]) || borderLeft.IntersectWith(snake[0]) || borderDown.IntersectWith(snake[0]))
            {
                Position failPosition = new Position(25, 350);
                failMessage = new TextLabel("you lost", failPosition);
                failMessage.SetFont("Gigi", 25);
                board.AddLabel(failMessage);
                board.StopTimer();
            }

            for(int i = snakeSize -1; i > 1; i--)
            {
                if (snake[0].IntersectWith(snake[i]))
                {
                    Position failPosition = new Position(25, 350);
                    failMessage = new TextLabel("you lost", failPosition);
                    failMessage.SetFont("Gigi", 25);
                    board.AddLabel(failMessage);
                    board.StopTimer();
                }
            }
        }

        //pauseing the game
        void PauseGame(Board board)
        {
            board.StopTimer();
        }

        //Continues the game
        void ContinueGame(Board board)
        {
            board.StartTimer(200);
        }

        //This function is called frequently based on the game board interval that was set when starting the timer!
        //Use this function to move game objects and check collisions
        public void GameClock(Board board)
        {
            LoseCondition(board);
            SnakeMovment();
            HandleSnakeEatsnake(board);
        }

        //This function is called by the game when the user press a key down on the keyboard.
        //Use this function to check the key that was pressed and change the direction of game objects acordingly.
        //Arrows ascii codes are given by ConsoleKey.LeftArrow and alike
        //Also use this function to handle game pause, showing user messages (like victory) and so on...
        public void KeyDown(Board board, char key)
        {
            if (key == (char)ConsoleKey.LeftArrow)
                snake[0].direction = GameObject.Direction.LEFT;
            if (key == (char)ConsoleKey.RightArrow)
                snake[0].direction = GameObject.Direction.RIGHT;
            if (key == (char)ConsoleKey.UpArrow)
                snake[0].direction = GameObject.Direction.UP;
            if (key == (char)ConsoleKey.DownArrow)
                snake[0].direction = GameObject.Direction.DOWN;
            if (key == (char)ConsoleKey.P)
            {
                PauseGame(board);
            }
            if (key == (char)ConsoleKey.O)
            {
                ContinueGame(board);
            }
        }
    }
}