using FactoryAgain.Abstractions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryAgain.Entities
{
    //publikus kell!!
    public class BallFactory : IToyFactory //őse lett egy interfész
    {
        public Color BallColor { get; set; }

        //az építőfv-el a főbbkonfig. ehetőségek tulajdonságai megvannak, tul.állításával a fv-t meghívva az ugyanúgy konfigurált példányok létrehozjatók (pl labda színe)
        public Toy CreateNew() //játék visszatérési érték, mert játékokat hozunk lére, a fv-ben specifikáljuk, hogy az ball most
        {
            {
                return new Ball(BallColor); // és létre is kell hozni az új labdát
            }

        }

    }
}
