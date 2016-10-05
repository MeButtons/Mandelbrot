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
        double schaal = 1;
        double a = 0;
        double b = 0;
        double a2;
        double b2;
        int x = -225;
        int y = -225;
        int teller = 0;
        double resultaat;
        double sqrtR;
        double afstand = 0;

        public Mandelbrot()
        {
            this.Text = "Mandelbrot";
            this.Size = new Size(550, 550);
            this.Paint += this.tekenscherm;
        }

        void tekenscherm(object obj, PaintEventArgs pea)
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
                        afstand = 3;                        // afstand groter gemaakt dan 2 om uit de loop te halen
                        teller = 3;
                    }                                       // teller in een kleiner getal verandert
                    a = a2;                                 // tijdelijke a naar de gewone a zetten
                    b = b2;                                 // tijdelijke b naar de gewone b zetten
                }
                
                    resultaat = teller % 2;                 // kijken of getal oneven is
                    if (resultaat == 1)
                    {
                        pea.Graphics.FillRectangle(Brushes.Black, x + 225, y + 225, 1, 1);
                        x = x + 1;
                        teller = 0;
                        afstand = 0;
                        a = 0;
                        b = 0;
                    }
                    else
                    {
                        pea.Graphics.FillRectangle(Brushes.White, x + 225, y + 225, 1, 1);
                        x = x + 1;
                        teller = 0;
                        afstand = 0;
                        a = 0;
                        b = 0;
                    }
                    if (x == 550 && y < 550)
                    {
                        x = -225;
                        y = y + 1;
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
