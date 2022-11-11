using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gyak07.Abstractions
{
    //a virtuális elemek az absztraktól úgy térnek el, hogy ki van fejtve, önmagában használható
    //absztarkt fv működése leszármazottban felülírhatja az őst
    //pl a movetoy alakítható lenne úgy, hogy a labda nem scak vízszintesen mozog, pattog isc

    public abstract class Toy : Label
    //absztrakció implementálásával még nincs változás eddigiekben (leszármazottból fejtetünk vissza)
    //be kell építeni még a leszármazottba
    {
        //ballból átmásolás

        public Toy() //hogy tulajdonságot állíthassak rá
        {
            AutoSize = false;
            Width = 50;
            Height = Width;
            Paint += Toy_Paint; //paint += tab tabbal eseménykezelő
        }

        private void Toy_Paint(object sender, PaintEventArgs e)
        {
            DrawImage(e.Graphics); //adott osztályban létrehozott grafikapéldánnyal rajz
            //bármi amit itt a vászonra rajzolunk, megjelenik afelhasználó grafikus felületén
        }

        //private: elem csak osztályon belül kezelhető
        //a protected void-ban a fv kívülről nem férhető hozzá, de a Ball osztály minden leszármazottja használhatja


        //nem írjuk meg a drawimage-t, mert el fog térni mindenhol
        //paint hivatkozik rá, szóval abstract fv kell azért
        //így má kinézet, mint fv-eknél
        //így ki is kényszerítjük, hogy leygen ilyen fv midnen leszármwaottban

        protected abstract void DrawImage(Graphics g);
        //csak lezárt fv, nem kifejtett
        //{
            //vezérlőbe illeszkedő kitöltött kék kör
            //Graphics osztály segít ebben
            
            //g.FillEllipse(new SolidBrush(Color.Blue), 0, 0, Width, Height); //width és height az ellipszis köré rakott téglalapé, ami most a balltól jön(?)
        //}

        //arrébb csuszi 1-gyel
        //virtual void!
        public virtual void MoveToy()
        {
            Left += 1;
        }

    }
}
