using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Mandelbrot
{
    class Mandelbrot : Form
    {
        private TextBox invoerx, invoery, invoerschaal;
        private Label labelx, labely, labelschaal;

        double schaal = 0.01;
        double a = 0;
        double b = 0;
        double a2;
        double b2;
        float x = -225;
        float y = -225;
        int teller = 0;
        double resultaat;
        double sqrtR;
        double afstand = 0;

        public Mandelbrot()
        {
            Button knop;

            this.invoerx = new TextBox();
            this.invoerx.Location = new Point(50,12);
            invoerx.Text = x.ToString();

            this.invoery = new TextBox();
            this.invoery.Location = new Point(175, 12);
            invoery.Text = y.ToString();

            this.invoerschaal = new TextBox();
            this.invoerschaal.Location = new Point(325,12);
            invoerschaal.Text = schaal.ToString();

            knop = new Button();
            knop.Size = new Size(85,50);
            knop.Text = "start";
            knop.Location = new Point(450,0);

            this.labelx = new Label();
            this.labelx.Location = new Point(25,12);
            labelx.Text = "X:";

            this.labely = new Label();
            this.labely.Location = new Point(150, 12);
            labely.Text = "Y:";

            this.labelschaal = new Label();
            this.labelschaal.Location = new Point(275, 12);
            labelschaal.Text = "Schaal:";

            this.Text = "Mandelbrot";
            this.Size = new Size(550, 550);
            knop.Click += this.start;
            this.Controls.Add(knop);
            this.Controls.Add(invoerx);
            this.Controls.Add(invoery);
            this.Controls.Add(labelx);
            this.Controls.Add(labely);
            this.Controls.Add(invoerschaal);
            this.Controls.Add(labelschaal);
        }

        private void start(object sender, System.EventArgs e)
        {
            Invalidate();
            this.Paint += this.tekenscherm;

        }
 
        void tekenscherm(object obj, PaintEventArgs pea)
        {
            x = float.Parse(invoerx.Text);
            y = float.Parse(invoery.Text);
            schaal = float.Parse(invoerschaal.Text);

            this.tekenmandelbrot(pea.Graphics, x, y);
            
        }
       
        void tekenmandelbrot(Graphics gr, float x, float y)
        {
            while (x <= 550)
            {
                while (afstand <= 2)                                    // 2 keer while -> kijken of dit netter/beter kan
                {
                    if (teller < 100)                                   // moet if zijn anders loop met altijd het antwoord 3 -> zwart vlak
                    {
                        a2 = (a * a - b * b + (x * schaal));            // nieuwe a berekenen en in tijdelijke a zetten
                        b2 = (2 * a * b + (y * schaal));                // nieuwe b berekenen en in tijdelijke a zetten
                        teller = teller + 1;                            // teller verhogen
                        sqrtR = (a * a) + (b * b);                      // stelling pythagoras a^2 + b^2 = c^2 
                        afstand = Math.Sqrt(sqrtR);                     // stelling pythagoras
                    }
                    if (teller >= 100)
                    {
                        gr.FillRectangle(Brushes.Red, x + 225, y + 275, 1, 1);
                        x = x + 1;
                        teller = 0;
                        afstand = 0;
                        a = 0;
                        b = 0;
                        break;
                    }                                       // teller in een kleiner getal verandert
                    a = a2;                                 // tijdelijke a naar de gewone a zetten
                    b = b2;                                 // tijdelijke b naar de gewone b zetten
                }

                resultaat = teller % 2;                 // kijken of getal oneven is
                if (resultaat == 1)
                {
                    gr.FillRectangle(Brushes.Blue, x + 225, y + 275, 1, 1);
                    x = x + 1;
                    teller = 0;
                    afstand = 0;
                    a = 0;
                    b = 0;
                }
                else
                {
                    gr.FillRectangle(Brushes.Yellow, x + 225, y + 275, 1, 1);
                    x = x + 1;
                    teller = 0;
                    afstand = 0;
                    a = 0;
                    b = 0;
                }
                if (x >= 225 && y < 225)
                {
                    x = -225;
                    y = y + 1;
                }
                if (y >= 225)
                {
                    break;
                }
            }

            
        }
        
    }


        class Program
        {
            static void Main()
            {
                Mandelbrot scherm;
                scherm = new Mandelbrot();
                Application.Run(scherm);
                
            }
        }
    }

