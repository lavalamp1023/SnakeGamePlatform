using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using WMPLib;
using System.IO;

namespace SnakeGamePlatform
{
    public interface IGameEvents
    {
        void GameInit(Board board);
        void GameClock(Board board);
        void KeyDown(Board board, char key);
    }

    public class Board
    {
        private Timer timer;
        private GameBoard board;
        private IGameEvents events;
        const int DEF_RES_FACTOR = 1;
        public static int resolutionFactor = DEF_RES_FACTOR;
        private WindowsMediaPlayer gameSound;
        


        public int XSize
        {
            get
            {
                return board.Height / resolutionFactor;
            }
            set
            {
                board.Height = value * resolutionFactor;
            }
        }
        public int YSize
        {
            get
            {
                return board.Width / resolutionFactor;
            }
            set
            {
                board.Width = value * resolutionFactor;
            }
        }

        public IGameEvents Events
        {
            get
            {
                return events;
            }
        }
        public Board()
        {
            Directory.SetCurrentDirectory(@"..\..\");
            this.events = new GameEvents();
            this.board = new GameBoard(this);
            this.timer = new Timer();
            this.timer.Tick += GameTick;
            gameSound = new WindowsMediaPlayer();
            gameSound.settings.setMode("Loop", true);
            events.GameInit(this);
        }

        public Board(int xSize, int ySize)
        {
            Directory.SetCurrentDirectory(@"..\..\");
            this.events = new GameEvents();
            this.board = new GameBoard(this);
            this.XSize = xSize;
            this.YSize = ySize;
            this.timer = board.GetTimer();
            gameSound = new WindowsMediaPlayer();
            gameSound.settings.setMode("Loop", true);
            events.GameInit(this);
        }

        public Board(IGameEvents events)
        {
            Directory.SetCurrentDirectory(@"..\..\");
            this.board = new GameBoard(this);
            this.events = events;
            gameSound = new WindowsMediaPlayer();
            gameSound.settings.setMode("Loop", true);
            events.GameInit(this);
        }

        public GameBoard GetBoard()
        {
            return this.board;
        }

        public void StartTimer(int interval)
        {
            this.timer.Interval = interval;
            this.timer.Start();
        }

        public void StopTimer()
        {
            this.timer.Stop();
        }

        public void AddLabel(TextLabel lbl)
        {
            board.Controls.Add(lbl.LabelControl);
        }

        public void RemoveLabel(TextLabel lbl)
        {
            board.Controls.Remove(lbl.LabelControl);
            
        }

        public void SendToBack(TextLabel s)
        {
            s.LabelControl.SendToBack();
        }
        public void SendToBack(GameObject s)
        {
            s.PicControl.SendToBack();
        }
        public void AddGameObject(GameObject obj)
        {
            board.Controls.Add(obj.PicControl);
        }

        public void RemoveGameObject(GameObject obj)
        {
            board.Controls.Remove(obj.PicControl);
        }

        private void GameTick(Object sender, EventArgs e)
        {
            events.GameClock(this);
        }

        public void SetBackgroundColor(Color c)
        {
            this.board.BackColor = c;
        }

        public void SetBackgroundImage(Image img)
        {
            this.board.BackgroundImageLayout = ImageLayout.Stretch;
            this.board.BackgroundImage = img; 
        }

        public void PlayBackgroundMusic(string audioFile)
        {
            string path = Directory.GetCurrentDirectory();
            gameSound.URL = path + audioFile;
        }

        public void PlayShortMusic(string audioFile)
        {
            System.Media.SoundPlayer player = new System.Media.SoundPlayer();
            string path = Directory.GetCurrentDirectory();
            player.SoundLocation = path + audioFile;
            player.Load();
            player.Play();
        }
        
        public void SetGameBoardTitle(string title)
        {
            board.Text = title;
        }


    }
}
