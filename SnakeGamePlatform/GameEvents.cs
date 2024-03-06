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
        
        GameObject[] snake = new GameObject[256];
        TextLabel lblScore;
        GameObject food;
        
        Position borderUP = new Position(10, 20);

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

            //Adding Game Object


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

            //Play file in loop!
            board.PlayBackgroundMusic(@"\Images\gameSound.wav");
            //Play file once!
            board.PlayShortMusic(@"\Images\eat.wav");


            //Start game timer!
            board.StartTimer(50);
            Position borderUPpos = new Position(100, 100);
            borderUp = new GameObject(borderUPpos, 300, 10);

            Position borderRightpos = new Position( 100,500);
            borderRight = new GameObject(borderRightpos, 10, 300);

            Position borderLeftpos = new Position(500, 100);
            borderLeft = new GameObject(borderLeftpos, 300, 10);

            Position borderDownpos = new Position(500, 100);
            borderDown = new GameObject(borderDownpos, 300, 10);
            //borderUpp = new GameObject(borderUP, 20, 30);
        }

        GameObject GenrateNewSnakePart(int lastSnakePartX,int lastSnakePartY)
        {
            GameObject snake;
            Position snakePosition = new Position(lastSnakePartX, lastSnakePartY - 20);
            snake = new GameObject(snakePosition, 20, 20);
            snake.SetImage(Properties.Resources.food);
            snake.direction = GameObject.Direction.RIGHT;
            return snake;
        }

        //This function is called frequently based on the game board interval that was set when starting the timer!
        //Use this function to move game objects and check collisions
        public void GameClock(Board board)
        {

            borderUp.SetBackgroundColor(Color.Black);
            board.AddGameObject(borderUp);

            borderRight.SetBackgroundColor(Color.Black);
            board.AddGameObject(borderRight);

            borderLeft.SetBackgroundColor(Color.Black);
            board.AddGameObject(borderLeft);

            borderDown.SetBackgroundColor(Color.Black);
            board.AddGameObject(borderDown);



            if (borderUp.IntersectWith(snake[0]) || borderRight.IntersectWith(snake[0]) || borderLeft.IntersectWith(snake[0]) || borderDown.IntersectWith(snake[0]))
            {
                Position failPosition = new Position(150, 50);
                failMessage = new TextLabel("you lost", failPosition);
                failMessage.SetFont("Ariel", 14);
                board.AddLabel(failMessage);
                board.StopTimer();
            }

            Position snakePosition = snake[0].GetPosition();
            Position mem = snake[0].GetPosition();
            if (snake[0].direction == GameObject.Direction.RIGHT)
                snakePosition.Y = snakePosition.Y + 20;
            if (snake[0].direction == GameObject.Direction.LEFT)
                snakePosition.Y = snakePosition.Y - 20;
            if (snake[0].direction == GameObject.Direction.UP)
                snakePosition.X = snakePosition.X - 20;
            if (snake[0].direction == GameObject.Direction.DOWN)
                snakePosition.X = snakePosition.X + 20;
            snake[0].SetPosition(snakePosition);
            snake[1].SetPosition(mem);
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
        }


    }
}