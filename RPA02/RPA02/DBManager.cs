using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPA02
{
    public class DBManager
    {
        public static void AddPhone() //kell a static kulcsszó, hogy main-ből használható legyen
        {
            using (RPAEntities1 context = new RPAEntities1())
            {
                Phone t = new Phone();
                t.name = "iPhone";
                t.state = "használt";
                t.date = DateTime.Now;
                t.price = 200000;

                context.Phones.Add(t);
                context.SaveChanges();
            }; 
        }

        public static string GetPhoneNameById(int id)
        {
            RPAEntities1 context = new RPAEntities1();

            var query = from Phone in context.Phones
                        where Phone.id == id
                        select Phone;
            //List<Phone> phone = query.ToList(); ha lista lenne
            var result = query.FirstOrDefault();
            return result.name;
        }

    }
}
