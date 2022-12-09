using IVTLHZ11.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IVTLHZ11
{
    public partial class Form1 : Form
    {
        Random rng = new Random(1234); //matematikai képlet segítségével előállít egy olyan számsorozatot, amiben a lehetséges értékek véletlen sorrendben egyenletes eloszlással követik egymást. Ez azt jelenti, hogy a számsorozat tetszőleges pontjától, egy forrástól (Seed) elindulva a sorban következő értékek lényegében véletlen számok lesznek
        //seed órajel alapján / mi is deklarálhatjuk

        List<Person> Population = new List<Person>();
        List<BirthProbability> BirthProbabilities = new List<BirthProbability>();
        List<DeathProbability> DeathProbabilities = new List<DeathProbability>();

        List<int> Ffi = new List<int>();
        List<int> No = new List<int>();

        public Form1()
        {
            InitializeComponent();
           
           
        }

        private void Simulation()
        {
            richTextBox1.Text = "";

            Population = GetPopulation(textBox1.Text);
            BirthProbabilities = GetBirthProbabilities(@"C:\Temp\születés.csv");
            DeathProbabilities = GetDeathProbabilities(@"C:\Temp\halál.csv");

        
            // Végigmegyünk a vizsgált éveken
            for (int year = 2005; year <= numericUpDown1.Value; year++) //2005től vizsgálunk csak
            {
                // Végigmegyünk az összes személyen
                for (int i = 0; i < Population.Count; i++)
                {
                    SimStep(Population[i], year);
                }

                int nbrOfMales = (from x in Population
                                  where x.Gender == Gender.Male && x.IsAlive
                                  select x).Count();
                int nbrOfFemales = (from x in Population
                                    where x.Gender == Gender.Female && x.IsAlive
                                    select x).Count();
                Console.WriteLine(//értékek konzolra iratása
                    string.Format("Év:{0} Fiúk:{1} Lányok:{2}", year, nbrOfMales, nbrOfFemales));
                
                Ffi.Add(nbrOfMales);
                No.Add(nbrOfFemales);

                DisplayResults();
            }
            
        }

        private void SimStep(Person person, int year)
        {
            //Ha halott akkor kihagyjuk, ugrunk a ciklus következő lépésére
            if (!person.IsAlive) return;

            // Letároljuk az életkort, hogy ne kelljen mindenhol újraszámolni
            byte age = (byte)(year - person.BirthYear);

            // Halál kezelése
            // Halálozási valószínűség kikeresése
            double pDeath = (from x in DeathProbabilities
                             where x.Gender == person.Gender && x.Age == age
                             select x.DP).FirstOrDefault();
            // Meghal a személy?
            if (rng.NextDouble() <= pDeath)
                person.IsAlive = false;

            //Születés kezelése - csak az élő nők szülnek
            if (person.IsAlive && person.Gender == Gender.Female)
            {
                //Szülési valószínűség kikeresése
                double pBirth = (from x in BirthProbabilities
                                 where x.Age == age
                                 select x.BP).FirstOrDefault();
                //Születik gyermek?
                if (rng.NextDouble() <= pBirth)
                {
                    Person újszülött = new Person();
                    újszülött.BirthYear = year;
                    újszülött.NbrOfChildren = 0;
                    újszülött.Gender = (Gender)(rng.Next(1, 3)); //véletlenszerű nem, a felső korlát nem megengedő
                    Population.Add(újszülött);
                }
            }

        }


        private List<DeathProbability> GetDeathProbabilities(string v)
        {
            List<DeathProbability> deathProbabilities = new List<DeathProbability>();

            using (StreamReader sr = new StreamReader(v, Encoding.Default)) //olvasásra megnyitás
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine().Split(';'); //iteratívan elemekre bontás tulajdonságok miatt
                    deathProbabilities.Add(new DeathProbability() //megfelelő objektum képzése és listához adása
                    {
                        Gender = (Gender)Enum.Parse(typeof(Gender), line[0]),
                        Age = int.Parse(line[1]),
                        DP = double.Parse(line[2])
                    });
                }
            }

            return deathProbabilities; //adott listát ad vissza
        }

        private List<BirthProbability> GetBirthProbabilities(string v)
        {
            List<BirthProbability> birthProbabilities = new List<BirthProbability>();

            using (StreamReader sr = new StreamReader(v, Encoding.Default)) //olvasásra megnyitás
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine().Split(';'); //iteratívan elemekre bontás tulajdonságok miatt
                    birthProbabilities.Add(new BirthProbability() //megfelelő objektum képzése és listához adása
                    {
                        Age = int.Parse(line[0]),
                        NbrOfChildren = int.Parse(line[1]),
                        BP = double.Parse(line[2])
                    });
                }
            }

            return birthProbabilities; //adott listát ad vissza
        }

        private List<Person> GetPopulation(string csvpath)
        {
            List<Person> population = new List<Person>();

            using (StreamReader sr = new StreamReader(csvpath, Encoding.Default)) //olvasásra megnyitás
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine().Split(';'); //iteratívan elemekre bontás tulajdonságok miatt
                    population.Add(new Person() //megfelelő objektum képzése és listához adása
                    {
                        BirthYear = int.Parse(line[0]),
                        Gender = (Gender)Enum.Parse(typeof(Gender), line[1]),
                        NbrOfChildren = int.Parse(line[2])
                    });
                }
            }

            return population; //adott listát ad vissza
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Simulation();
            
        }

        private void DisplayResults()
        {
            int i = 0;
            for (int year = 2005; year <= numericUpDown1.Value; year++) //2005től vizsgálunk csak
            {
                richTextBox1.Text = String.Format("Szimulációs év: {0}\n\tFiúk: {1}\n\tLányok: {2}\n\n", year.ToString(), Ffi[i].ToString(), No[i].ToString()[i]);
                i++;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = ofd.FileName;
            }
        }
    }
}


