using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Mandelbrot
{
    class Kleur : Form
    {
        public int[] kleurArray;
        private TextBox r, g, b;
        private Label rLabel, gLabel, bLabel;

        public Kleur()
        {
            kleurArray = new int[3];

            Button klaar;
            klaar = new Button();
            klaar.Click += this.klaar_Click;

            this.r = new TextBox();
            this.r.Location = new Point(50, 10);
            this.rLabel = new Label();
            this.rLabel.Location = new Point(30, 10);
            rLabel.Text = "R";


            this.g = new TextBox();
            this.g.Location = new Point(50, 40);
            this.gLabel = new Label();
            this.gLabel.Location = new Point(30, 40);
            gLabel.Text = "G";

            this.b = new TextBox();
            this.b.Location = new Point(50, 70);
            this.bLabel = new Label();
            this.bLabel.Location = new Point(30, 70);
            bLabel.Text = "B";

            klaar.Location = new Point(290, 30);
            klaar.Size = new Size(80, 60);
            klaar.Text = "Klaar";

            this.Size = new Size(400, 200);

            this.Controls.Add(r);
            this.Controls.Add(g);
            this.Controls.Add(b);
            this.Controls.Add(rLabel);
            this.Controls.Add(gLabel);
            this.Controls.Add(bLabel);
            this.Controls.Add(klaar);

        }

        public void storeRGB()
        {
            kleurArray[0] = int.Parse(r.Text);
            kleurArray[1] = int.Parse(g.Text);
            kleurArray[2] = int.Parse(b.Text);
        }

        public void klaar_Click(object Sender, EventArgs e)
        {
            storeRGB();
            this.Close();

        }
    }


    class Mandelbrot : Form
    {
        private TextBox invoerx, invoery, invoerschaal;
        private Label labelx, labely, labelschaal;
        private SolidBrush kleur1, kleur2, kleur3;

        double schaal = 0.01;
        double a = 0;
        double b = 0;
        double a2;
        double b2;
        float x = -225;
        float y = -225;
        float coordx = -225;
        float coordy = -225;
        float middenx, middeny, nieuwx, nieuwy;
        int teller = 0;
        double resultaat;
        double sqrtR;
        double afstand = 0;

        public Mandelbrot()
        {
            Button knop, knopKleur1, knopKleur2, knopKleur3, lucht, water, aarde, vuur;
            PictureBox tekening = new PictureBox();

            this.invoerx = new TextBox();
            this.invoerx.Location = new Point(50,12);
            invoerx.Text = x.ToString();

            this.invoery = new TextBox();
            this.invoery.Location = new Point(175, 12);
            invoery.Text = y.ToString();

            this.invoerschaal = new TextBox();
            this.invoerschaal.Location = new Point(325,12);
            invoerschaal.Text = schaal.ToString();

            knopKleur1 = new Button();
            knopKleur1.Size = new Size(85,50);
            knopKleur1.Text = "Kleur 1";
            knopKleur1.Location = new Point(450,50);

            knopKleur2 = new Button();
            knopKleur2.Size = new Size(85, 50);
            knopKleur2.Text = "Kleur 2";
            knopKleur2.Location = new Point(450, 100);

            knopKleur3 = new Button();
            knopKleur3.Size = new Size(85, 50);
            knopKleur3.Text = "Kleur 3";
            knopKleur3.Location = new Point(450, 150);

            vuur = new Button();
            vuur.Size = new Size(85, 50);
            vuur.Text = "Vuur";
            vuur.Location = new Point(450, 200);

            water = new Button();
            water.Size = new Size(85, 50);
            water.Text = "Water";
            water.Location = new Point(450, 250);

            aarde = new Button();
            aarde.Size = new Size(85, 50);
            aarde.Text = "Aarde";
            aarde.Location = new Point(450, 300);

            lucht = new Button();
            lucht.Size = new Size(85, 50);
            lucht.Text = "Lucht";
            lucht.Location = new Point(450, 350);

            knop = new Button();
            knop.Size = new Size(85, 50);
            knop.Text = "start";
            knop.Location = new Point(450, 0);

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

            kleur1 = new SolidBrush(Color.FromArgb(255, 0, 0));
            kleur2 = new SolidBrush(Color.FromArgb(0, 255, 0));
            kleur3 = new SolidBrush(Color.FromArgb(0, 0, 255));


            this.Controls.Add(knop);
            this.Controls.Add(knopKleur1);
            this.Controls.Add(knopKleur2);
            this.Controls.Add(knopKleur3);
            this.Controls.Add(invoerx);
            this.Controls.Add(invoery);
            this.Controls.Add(labelx);
            this.Controls.Add(labely);
            this.Controls.Add(invoerschaal);
            this.Controls.Add(labelschaal);
            this.Controls.Add(tekening);
            this.Controls.Add(vuur);
            this.Controls.Add(water);
            this.Controls.Add(aarde);
            this.Controls.Add(lucht);

            this.MouseClick += this.start_click;
            knopKleur1.Click += this.knopKleur1_Click;
            knopKleur2.Click += this.knopKleur2_Click;
            knopKleur3.Click += this.knopKleur3_Click;
            vuur.Click += this.vuur_Click;
            water.Click += this.water_Click;
            aarde.Click += this.aarde_Click;
            lucht.Click += this.lucht_Click;
        }

        private void start_click(object sender, MouseEventArgs mea)
        {



            middenx = mea.X;
            middeny = mea.Y;
            middenx = middenx - 225;
            middeny = middeny - 275;
            coordx = -225;
            coordy = -225;
            invoerx.Text = middenx.ToString();
            invoery.Text = middeny.ToString();
            schaal = schaal * 0.8;
            invoerschaal.Text = schaal.ToString();
            Invalidate();
            this.Paint += this.tekenscherm;


        }

        

        public void knopKleur1_Click(object sender, System.EventArgs e)
        {
            Kleur kleurButton = new Kleur();
            kleurButton.ShowDialog();
            kleur1 = new SolidBrush(Color.FromArgb(kleurButton.kleurArray[0], kleurButton.kleurArray[1], kleurButton.kleurArray[2]));
            Invalidate();
        }

        public void knopKleur2_Click(object sender, System.EventArgs e)
        {
            Kleur kleurButton = new Kleur();
            kleurButton.ShowDialog();
            kleur2 = new SolidBrush(Color.FromArgb(kleurButton.kleurArray[0], kleurButton.kleurArray[1], kleurButton.kleurArray[2]));
            Invalidate();
        }

        public void knopKleur3_Click(object sender, System.EventArgs e)
        {
            Kleur kleurButton = new Kleur();
            kleurButton.ShowDialog();
            kleur3 = new SolidBrush(Color.FromArgb(kleurButton.kleurArray[0], kleurButton.kleurArray[1], kleurButton.kleurArray[2]));
            Invalidate();
        }

        public void vuur_Click(object sender, EventArgs e)
        {
            kleur1 = new SolidBrush(Color.FromArgb(255, 0, 0));
            kleur2 = new SolidBrush(Color.FromArgb(255, 200, 0));
            kleur3 = new SolidBrush(Color.FromArgb(255, 255, 0));
            Invalidate();
        }

        public void water_Click(object sender, EventArgs e)
        {
            kleur1 = new SolidBrush(Color.FromArgb(0, 0, 255));
            kleur2 = new SolidBrush(Color.FromArgb(0, 130, 255));
            kleur3 = new SolidBrush(Color.FromArgb(0, 0, 100));
            Invalidate();
        }

        public void aarde_Click(object sender, EventArgs e)
        {
            kleur1 = new SolidBrush(Color.FromArgb(255, 200, 0));
            kleur2 = new SolidBrush(Color.FromArgb(170, 90, 0));
            kleur3 = new SolidBrush(Color.FromArgb(80, 40, 0));
            Invalidate();
        }

        public void lucht_Click(object sender, EventArgs e)
        {
            kleur1 = new SolidBrush(Color.FromArgb(0, 255, 200));
            kleur2 = new SolidBrush(Color.FromArgb(0, 200, 255));
            kleur3 = new SolidBrush(Color.FromArgb(255, 255, 255));
            Invalidate();
        }

        private void start(object sender, System.EventArgs e)
        {

            Invalidate();
            this.Paint += this.tekenscherm;
            coordx = -225;
            coordy = -225;
        }

        void tekenscherm(object obj, PaintEventArgs pea)
        {
           schaal = float.Parse(invoerschaal.Text);


            nieuwx = x + middenx;
            x = nieuwx;
            nieuwy = y + middeny;
            y = nieuwy;

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
                        gr.FillRectangle(kleur1, coordx + 225, coordy + 275, 1, 1);
                        x = x + 1;
                        coordx = coordx + 1;
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
                    gr.FillRectangle(kleur2, coordx + 225, coordy + 275, 1, 1);
                    x = x + 1;
                    coordx = coordx + 1;
                    teller = 0;
                    afstand = 0;
                    a = 0;
                    b = 0;
                }
                else
                {
                    gr.FillRectangle(kleur3, coordx + 225, coordy + 275, 1, 1);
                    x = x + 1;
                    coordx = coordx + 1;
                    teller = 0;
                    afstand = 0;
                    a = 0;
                    b = 0;
                }
                if (coordx >= 225 && coordy < 225)
                {
                    x = nieuwx;
                    coordx = -225;
                    coordy = coordy + 1;
                    y = y + 1;
                }
                if (coordy >= 225)
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

