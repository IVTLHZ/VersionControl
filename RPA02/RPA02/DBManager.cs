using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace RPA02
{
    public static class DBManager
    {

        public static void AddFlightToDb() //kell a static kulcsszó, hogy main-ből használható legyen
        {
            using (RPAEntities context = new RPAEntities())
            {
                Flight_data f = new Flight_data();
                //f. = ;
                
                context.Flight_data.Add(f);
                context.SaveChanges();
            };
        }

      

    }
}
