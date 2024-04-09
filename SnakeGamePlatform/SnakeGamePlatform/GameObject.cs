using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Resources;
using System.Drawing;

namespace SnakeGamePlatform
{
    public class GameObject
    {
        public enum Direction
        {
            UP = 0,
            DOWN,
            LEFT,
            RIGHT
        }
        public Direction direction { get; set; }
        private Timer timer;
        private PictureBox picBox;
        public PictureBox PicControl
        {
                get { return this.picBox; }
        }
        public GameObject(Position pos, int width, int height)
        {
            timer = null;
            picBox = new PictureBox();
            picBox.Top = pos.X * Board.resolutionFactor;
            picBox.Left = pos.Y * Board.resolutionFactor;
            picBox.Width = width * Board.resolutionFactor; ;
            picBox.Height = height * Board.resolutionFactor; 
            picBox.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        public void SetPosition(Position p)
        {
            picBox.Top = p.X * Board.resolutionFactor; 
            picBox.Left = p.Y * Board.resolutionFactor;
        }

        public void SetBackgroundColor(Color c)
        {
            this.picBox.BackColor = c;
        }

        public void SetImage(Image img)
        {
            picBox.Image = img;
        }

        public Position GetPosition()
        {
            return new Position(picBox.Top / Board.resolutionFactor, picBox.Left / Board.resolutionFactor);
        }

        public void SetTimer(int interval, EventHandler eventHandler)
        {
            timer = new Timer();
            timer.Interval = interval;
            timer.Tick += eventHandler;
        }

        public void StartTimer()
        {
            if (timer != null)
                timer.Start();
        }

        public void StoptTimer()
        {
            if (timer != null)
                timer.Stop();
        }

        public bool IntersectWith(GameObject obj)
        {
            return (this.picBox.Bounds.IntersectsWith(obj.picBox.Bounds));
        }

        public bool OnScreen(Board board)
        {
            return !(this.picBox.Left + this.picBox.Width > board.GetBoard().ClientRectangle.Width ||
                this.picBox.Top + this.picBox.Height > board.GetBoard().ClientRectangle.Height ||
                !this.picBox.Bounds.IntersectsWith(board.GetBoard().ClientRectangle));
        }
    }
}
