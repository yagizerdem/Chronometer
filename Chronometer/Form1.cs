using System;
using System.Diagnostics;
using System.Timers;
using System.Runtime.InteropServices;
using System.Windows.Forms.VisualStyles;
namespace Chronometer
{
    public partial class Form1 : Form
    {
        private System.Timers.Timer timer;
        double h, m, s;

        DateTime prev = DateTime.Now;
        DateTime current = DateTime.Now;
        bool flag = false;
        bool hasStarted = false;
        public Form1()
        {
            InitializeComponent();
            timer = new System.Timers.Timer();
            timer.Elapsed += OnTimeEvent!;


        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            AllocConsole();

        }
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();

        private void button1_Click(object sender, EventArgs e)
        {
            this.current = DateTime.Now;
            this.prev = DateTime.Now;
            this.h = 0;
            this.m = 0;
            this.s = 0;
            timer.Start();
            this.hasStarted = true;
        }

        private void OnTimeEvent(Object source, ElapsedEventArgs e)
        {
            this.current = e.SignalTime;
            double diffInSeconds = (this.current - this.prev).TotalSeconds;

            this.s += diffInSeconds;

            if (this.s >= 60)
            {
                this.s -= 60;
                this.m++;
            }
            if (this.m >= 60)
            {
                this.m -= 60;
                this.h++;
            }

            this.label1.Text = Math.Floor(this.h).ToString().Length == 1 ? "0"+Math.Floor(this.h).ToString() : Math.Floor(this.h).ToString();
            this.label2.Text = Math.Floor(this.m).ToString().Length == 1 ? "0" + Math.Floor(this.m).ToString() : Math.Floor(this.m).ToString();
            this.label3.Text = Math.Floor(this.s).ToString().Length == 1 ? "0" + Math.Floor(this.s).ToString() : Math.Floor(this.s).ToString();

            this.prev = this.current;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(!this.hasStarted) return;
            if (!flag)
            {
                this.timer.Stop();
                flag = true;
            }
            else
            {
                this.current = DateTime.Now;
                this.prev = DateTime.Now;
                this.timer.Start();
                flag = false;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.h = 0;
            this.m = 0;
            this.s = 0;
            this.timer.Stop();
            this.hasStarted = false;

            this.label1.Text = "00";
            this.label2.Text = "00";
            this.label3.Text = "00";
        }
    }
}
