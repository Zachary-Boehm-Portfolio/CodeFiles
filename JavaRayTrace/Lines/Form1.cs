using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lines
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            start_x = Canvas.Width / 2;
            start_y = Canvas.Height / 2;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Line_Number_TextChanged(object sender, EventArgs e)
        {

        }

        private void Label2_Click(object sender, EventArgs e)
        {

        }

        private void Angle_TextChanged(object sender, EventArgs e)
        {

        }

        private void Label3_Click(object sender, EventArgs e)
        {

        }

        private void Length_TextChanged(object sender, EventArgs e)
        {

        }

        private void Label4_Click(object sender, EventArgs e)
        {

        }

        private void Increment_TextChanged(object sender, EventArgs e)
        {

        }

        Pen myPen = new Pen(Color.Black);
        Graphics g;

        static int center_x, center_y;
        static int start_x, start_y;
        static int end_x, end_y;

        static int my_angle = 0;
        static int my_length = 0;
        static int my_increment = 0;
        static int my_lines = 0;
        private void Button1_Click(object sender, EventArgs e)
        {
            start_x = Canvas.Width / 2;
            start_y = Canvas.Height / 2;
            my_length = Int32.Parse(Length.Text);
            my_increment = Int32.Parse(Increment.Text);
            my_angle = Int32.Parse(Angle.Text);
            my_lines = Int32.Parse(Line_Number.Text);

            Canvas.Refresh();
        }

        private void drawingLine()
        {
            Random randomGen = new Random();
            myPen.Color = Color.FromArgb(randomGen.Next(255), randomGen.Next(255), randomGen.Next(255));
            my_angle = my_angle + Int32.Parse(Angle.Text);
            my_length = my_length + Int32.Parse(Increment.Text);
            end_x = (int)(start_x + Math.Cos(my_angle * .017453292519) * my_length);
            end_y = (int)(start_y + Math.Sin(my_angle * .017453292519) * my_length);
            Point[] points =
            {
                new Point(start_x, start_y),
                new Point(end_x, end_y)
            };

            start_x = end_x;
            start_y = end_y;

            g.DrawLines(myPen, points);
        }// end drawingLine
        
        private void Canvas_Paint(object sender, PaintEventArgs e)
        {
            myPen.Width = 1;
            my_length = Int32.Parse(Length.Text);
            g = Canvas.CreateGraphics();
            for(int i = 0; i < Int32.Parse(Line_Number.Text); i++)
            {
                drawingLine();
            }
            

        }
    }
}
