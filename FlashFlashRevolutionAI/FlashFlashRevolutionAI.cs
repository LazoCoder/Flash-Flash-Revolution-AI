using FlashRevolutionAI;
using ImageProcessing;
using System;
using System.Drawing;
using System.Windows.Forms;
using WindowsAPI;

namespace FlashFlashRevolutionAI
{

    /// <summary>
    /// Plays Flash Flash Revolution in a standalone Flash Player Window.
    /// 
    /// The incoming arrows start from the bottom up. Small strips of images of what each incoming
    /// arrow looks like is stored in the Resources. Then it is searched for on particular places
    /// on the screen to determine which key should be pressed, if any at all. The methods that
    /// determine this are titled like CheckLeft(), CheckRight() etc...
    /// 
    /// When a key is pressed, the player's arrows at the top glows white. The methods titled like
    /// LeftNotAlreadyPressed() check for this. If the user has not pressed that particular key
    /// recently then this method returns true.
    /// 
    /// The methods titled LeftPassed(), RightPassed(), UpPassed(), and DownPassed() check to see
    /// if an incoming arrow has almost passed the player's arrow. This means that that particular
    /// key is overdue to be pressed. It is best to press the button a bit too late than never
    /// at all.
    /// </summary>
    class FlashFlashRevolutionAI
    {
        // Locations of where to check the incoming arrows on the screen.
        // This is how the AI will know what keys to press.
        public static Rectangle rLeft   = new Rectangle(277, 130, 1, 20);
        public static Rectangle rDown   = new Rectangle(357, 130, 1, 20);
        public static Rectangle rUp     = new Rectangle(436, 130, 1, 20);
        public static Rectangle rRight  = new Rectangle(518, 130, 1, 20);

        // The pointer to the game window.
        IntPtr hWnd;

        // For storing screenshots taken from game window.
        Bitmap screen;

        // Used for checking small area on the screen for changes.
        Bitmap temp;

        // Used for checking certain pixels on the screen.
        // If a pixel is white then the user has already pressed that particular key.
        Color p;

        // Percentage to check for similarity when looking for arrows.
        // If something on the screen 90% resembles an arrow, then it is an arrow.
        const int PERCENT = 90;

        // Key pressing delay. 50 is good for most levels, 20 is good for the hardest levels.
        const int DELAY = 50;

        /// <summary>
        /// Construct FlashFlashRevolution.
        /// </summary>
        /// <param name="hWnd">The pointer to the game window.</param>
        public FlashFlashRevolutionAI(IntPtr hWnd)
        {
            this.hWnd = hWnd;
        }

        /// <summary>
        /// Begin playing the game.
        /// </summary>
        public void Play()
        {
            while (true)
            {
                if (Window.GetFocused() != hWnd) break;

                screen = Window.Screenshot(hWnd);

                bool left = LeftPassed() || CheckLeft();
                bool down = DownPassed() || CheckDown();
                bool up = UpPassed() || CheckUp();
                bool right = RightPassed() || CheckRight();

                if (left == false && down == false && up == false && right == false)
                {
                    screen.Dispose();
                    continue;
                }
                else if (left && down && up && right)   PressKeys("{LEFT}{DOWN}{UP}{RIGHT}");
                else if (left && down && up)            PressKeys("{LEFT}{DOWN}{UP}");
                else if (left && down && right)         PressKeys("{LEFT}{DOWN}{RIGHT}");
                else if (left && up && right)           PressKeys("{LEFT}{UP}{RIGHT}");
                else if (down && up && right)           PressKeys("{DOWN}{UP}{RIGHT}");
                else if (left && down)                  PressKeys("{LEFT}{DOWN}");
                else if (left && up)                    PressKeys("{LEFT}{UP}");
                else if (left && right)                 PressKeys("{LEFT}{RIGHT}");
                else if (down && up)                    PressKeys("{DOWN}{UP}");
                else if (down && right)                 PressKeys("{DOWN}{RIGHT}");
                else if (up && right)                   PressKeys("{UP}{RIGHT}");
                else if (left)                          PressKeys("{LEFT}");
                else if (down)                          PressKeys("{DOWN}");
                else if (up)                            PressKeys("{UP}");
                else if (right)                         PressKeys("{RIGHT}");

                screen.Dispose();
            }
        }

        /// <summary>
        /// Helper method for pressing keys.
        /// </summary>
        /// <param name="s">The identifier of the key to press.</param>
        private void PressKeys (string s)
        {
            SendKeys.SendWait(s);
            Wait(DELAY);
        }

        /// <summary>
        /// Check to see if the left arrow should be pressed.
        /// </summary>
        /// <returns>True if the left arrow should be pressed.</returns>
        private bool CheckLeft()
        {
            temp = Tools.Crop(screen, rLeft);
            double result = BasicVision.CompareByPixels(temp, Properties.Resources.left);
            return (result < PERCENT && LeftNotAlreadyPressed()) ? true : false;
        }

        /// <summary>
        /// Check to see if the down arrow should be pressed.
        /// </summary>
        /// <returns>True if the down arrow should be pressed.</returns>
        private bool CheckDown()
        {
            temp = Tools.Crop(screen, rDown);
            double result = BasicVision.CompareByPixels(temp, Properties.Resources.down);
            return (result < PERCENT && DownNotAlreadyPressed()) ? true : false;
        }

        /// <summary>
        /// Check to see if the up arrow should be pressed.
        /// </summary>
        /// <returns>True if the up arrow should be pressed.</returns>
        private bool CheckUp()
        {
            temp = Tools.Crop(screen, rUp);
            double result = BasicVision.CompareByPixels(temp, Properties.Resources.up);
            return (result < PERCENT && UpNotAlreadyPressed()) ? true : false;
        }

        /// <summary>
        /// Check to see if the right arrow should be pressed.
        /// </summary>
        /// <returns>True if the right arrow should be pressed.</returns>
        private bool CheckRight()
        {
            temp = Tools.Crop(screen, rRight);
            double result = BasicVision.CompareByPixels(temp, Properties.Resources.right);
            return (result < PERCENT && RightNotAlreadyPressed()) ? true : false;
        }

        private bool LeftNotAlreadyPressed()
        {
            p = screen.GetPixel(244, 140);
            if (p.R == 0 && p.G == 0 && p.B == 0) return true;
            return false;
        }

        private bool DownNotAlreadyPressed()
        {
            p = screen.GetPixel(324, 135);
            if (p.R == 0 && p.G == 0 && p.B == 0) return true;
            return false;
        }

        private bool UpNotAlreadyPressed()
        {
            p = screen.GetPixel(404, 143);
            if (p.R == 0 && p.G == 0 && p.B == 0) return true;
            return false;
        }

        private bool RightNotAlreadyPressed()
        {
            p = screen.GetPixel(484, 152);
            if (p.R == 0 && p.G == 0 && p.B == 0) return true;
            return false;
        }

        private void PrintDots()
        {
            WindowsAPI.Draw.Rectangle(Color.Yellow, rLeft, 1, hWnd);
            WindowsAPI.Draw.Rectangle(Color.Yellow, rDown, 1, hWnd);
            WindowsAPI.Draw.Rectangle(Color.Yellow, rUp, 1, hWnd);
            WindowsAPI.Draw.Rectangle(Color.Yellow, rRight, 1, hWnd);
        }

        private bool LeftPassed()
        {
            p = screen.GetPixel(282, 95);
            if (p.R == 0 && p.G == 0 && p.B == 0) return false;
            return true; //its not black, so its passed
        }

        private bool DownPassed()
        {
            p = screen.GetPixel(360, 95);
            if (p.R == 0 && p.G == 0 && p.B == 0) return false;
            return true;
        }

        private bool UpPassed()
        {
            p = screen.GetPixel(440, 95);
            if (p.R == 0 && p.G == 0 && p.B == 0) return false;
            return true;
        }

        private bool RightPassed()
        {
            p = screen.GetPixel(520, 95);
            if (p.R == 0 && p.G == 0 && p.B == 0) return false;
            return true;
        }

        private static void Wait(int milliseconds)
        {
            System.Threading.Thread.Sleep(milliseconds);
        }
    }
}
