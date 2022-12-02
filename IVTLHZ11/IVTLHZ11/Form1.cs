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
        List<Person> Population = new List<Person>();
        List<BirthProbability> BirthProbabilities = new List<BirthProbability>();
        List<DeathProbability> DeathProbabilities = new List<DeathProbability>();

        public Form1()
        {
            InitializeComponent();
            Population = GetPopulation(@"C:\Temp\nép.csv");
            BirthProbabilities = GetBirthProbabilities(@"C:\Temp\születés.csv");
            DeathProbabilities = GetDeathProbabilities(@"C:\Temp\halál.csv");
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
    }

     
    }

