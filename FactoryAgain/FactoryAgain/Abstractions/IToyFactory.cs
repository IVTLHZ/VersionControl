using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryAgain.Abstractions
{
    //class-t hoztam létre, d eúgy lett interfész, hogy csak átírtam a class-t interfészre

    public interface IToyFactory
    { //toy vsszatérési értékű fv építére
        Toy CreateNew(); //fv-t nem fejtün ki itt sem és fix public lesz, nem is kell előtag
    }
}
