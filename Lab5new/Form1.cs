using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab5new
    /**
    *I, Nhat Minh Dang, 000746305 certify that this material is my original work. No other person's work has been used without due acknowledgement.
    */
{
    public partial class Form1 : Form
    {
        private ColorDialog ColorBox = new ColorDialog(); // create Color box
        private Graphics g; // create Graphic object
        private Color c = Color.Black; 
        private Pen p; //create a pen
        private SolidBrush b; 
        private int defaultInterval = 150; //set default Interval of Timer to 150 miliseconds
        private int count = 1; //ticks counter
        private int previousCount;
        private Timer timer = new Timer();
        bool filled;
        public Form1()
        {
            InitializeComponent();
            this.Paint += new PaintEventHandler(Form1_Paint);
            timer.Tick += new EventHandler(timer_Tick); // Everytime timer ticks, timer_Tick will be called
            timer.Interval = defaultInterval;            // Timer will tick every
            b = new SolidBrush(c); //Intansitate black brush
            filled = false;

        }

        private void timer_Tick(object sender, EventArgs e)
        {
            g = this.CreateGraphics(); //create Graphics object
            g.DrawLine(p, 101, 450 - count, 299, 450 - count); //Each time timer tick draw a new line inside the bucket representing the liquid filling
            count++; //increase ticks counter
            if (count == 150) //if bucket is filled
            {
                filled = true; //set bool to true
                //turn off timer
                timer.Enabled = false; 
                timer.Stop();
                previousCount = count; //store count to previousCount
                count = 1; //set tick counter back to 1
                timer.Interval = defaultInterval; //set interval back to default
                trackBar1.Value = 0; //set trackbar to default
                g.DrawLine(new Pen(Color.Black, 20), 120, 270, 120, 450 - previousCount + 1); //turn off the tap
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            g = e.Graphics;
            p = new Pen(c);
            Pen tempPen = new Pen(Color.White);
            //draw lines representing bucket borders
            g.DrawLine(tempPen, 100, 300, 100, 450);
            g.DrawLine(tempPen, 100, 450, 300, 450);
            g.DrawLine(tempPen, 300, 300, 300, 450);

        }
        private void Colors_Click(object sender, EventArgs e)
        {
            ColorBox.Color = c; 
            ColorBox.ShowDialog(); //show Colordialog
            c = ColorBox.Color;  
            p = new Pen(c);
            if (timer.Enabled) g.DrawLine(new Pen(ColorBox.Color, 20), 120, 270, 120, 450 - count); //draw new filling liquid if the color is selected and the timer is running

        }


        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            int trackBarVal = trackBar1.Value;
            g = this.CreateGraphics();
            //if trackbar val  is 0 stop timer and stop the faucet
            if (trackBarVal == 0)
            {
                timer.Stop();
                g.DrawLine(new Pen(Color.Black, 20), 120, 270, 120, 450 - count);
            }
            else
            {
                //if the bucket is filled , empty the bucket 
                if (filled)
                {
                    g.FillRectangle(b, 101, 299, 198, 149); //empty the bucket
                    filled = false; 
                }
                //turn on the faucet and modify interval of the timer
                g.DrawLine(new Pen(ColorBox.Color, 20), 120, 270, 120, 450 - count);
                if (!timer.Enabled) timer.Start(); 
                if(trackBarVal >= 1 && trackBarVal <= 3)
                {
                    timer.Interval = defaultInterval - 50;
                }else if(trackBarVal >= 4 && trackBarVal <= 7)
                {
                    timer.Interval = defaultInterval - 100;
                }
                else
                {
                    timer.Interval = defaultInterval - 145;
                }
            }


        }



        private void Close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        private void Form1_Paint_1(object sender, PaintEventArgs e)
        {

        }

    }
}
