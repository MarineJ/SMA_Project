using System;
using System.Collections.Generic;
using System.Text;
using Mogre;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Timers;

namespace SMA_Project_V1
{
    public class DefaultInputHandler
    {
        // Fields
        private const int INTERVAL = 0x11;
        private bool mLastFocus;
        private Point mLastLocation;
        protected float mRot = -0.2f;
        private bool mRotating;
        private System.Windows.Forms.Timer mTimer = new System.Windows.Forms.Timer();
        protected float mTrans = 10f;
        protected Vector3 mTranslate = Vector3.ZERO;
        protected WindowMaker mWindow;

        // Methods
        public DefaultInputHandler(WindowMaker win)
        {
            this.mWindow = win;
            win.KeyDown += new KeyEventHandler(this.HandleKeyDown);
            win.KeyUp += new KeyEventHandler(this.HandleKeyUp);
            win.MouseDown += new MouseEventHandler(this.HandleMouseDown);
            win.MouseUp += new MouseEventHandler(this.HandleMouseUp);
            win.Disposed += new EventHandler(this.win_Disposed);
            win.LostFocus += new EventHandler(this.win_LostFocus);
            win.GotFocus += new EventHandler(this.win_GotFocus);
            this.mTimer.Interval = 0x11;
            this.mTimer.Enabled = true;
            this.mTimer.Tick += new EventHandler(this.Timer_Tick);
        }

        protected virtual void HandleKeyDown(object sender, KeyEventArgs e)
        {
            float mTrans = this.mTrans;
            switch (e.KeyCode)
            {
                case Keys.Q:
                case Keys.Prior:
                    this.mTranslate.y = mTrans;
                    return;

                case Keys.R:
                case Keys.End:
                case Keys.Home:
                case Keys.B:
                case Keys.C:
                    break;

                case Keys.S:
                case Keys.Down:
                    this.mTranslate.z = mTrans;
                    return;

                case Keys.W:
                case Keys.Up:
                    this.mTranslate.z = -mTrans;
                    return;

                case Keys.Next:
                case Keys.E:
                    this.mTranslate.y = -mTrans;
                    break;

                case Keys.Left:
                case Keys.A:
                    this.mTranslate.x = -mTrans;
                    return;

                case Keys.Right:
                case Keys.D:
                    this.mTranslate.x = mTrans;
                    return;

                default:
                    return;
            }
        }

        protected virtual void HandleKeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Q:
                case Keys.Prior:
                case Keys.Next:
                case Keys.E:
                    this.mTranslate.y = 0f;
                    break;

                case Keys.R:
                case Keys.End:
                case Keys.Home:
                case Keys.B:
                case Keys.C:
                    break;

                case Keys.S:
                case Keys.W:
                case Keys.Up:
                case Keys.Down:
                    this.mTranslate.z = 0f;
                    return;

                case Keys.Left:
                case Keys.Right:
                case Keys.A:
                case Keys.D:
                    this.mTranslate.x = 0f;
                    return;

                default:
                    return;
            }
        }

        protected virtual void HandleMouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                Cursor.Hide();
                this.mRotating = true;
            }
        }

        private void HandleMouseMove(Point delta)
        {
            if (this.mRotating)
            {
                this.mWindow.Camera.Yaw(new Degree(delta.X * this.mRot));
                this.mWindow.Camera.Pitch(new Degree(delta.Y * this.mRot));
            }
        }

        protected virtual void HandleMouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                Cursor.Show();
                this.mRotating = false;
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (this.mLastFocus)
            {
                Point position = Cursor.Position;
                position.X -= this.mLastLocation.X;
                position.Y -= this.mLastLocation.Y;
                this.HandleMouseMove(position);
            }
            this.mLastLocation = Cursor.Position;
            this.mLastFocus = this.mWindow.Focused;
            if (this.mLastFocus)
            {
                Camera camera = this.mWindow.Camera;
                camera.Position += this.mWindow.Camera.Orientation * this.mTranslate;
            }
        }

        private void win_Disposed(object sender, EventArgs e)
        {
            this.mTimer.Enabled = false;
        }

        private void win_GotFocus(object sender, EventArgs e)
        {
            this.mTimer.Enabled = true;
        }

        private void win_LostFocus(object sender, EventArgs e)
        {
            this.mTimer.Enabled = false;
            this.mTranslate = Vector3.ZERO;
        }
    }
}
