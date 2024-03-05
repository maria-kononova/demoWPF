using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demo
{
    public class Employer
    {

        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime BirthDay { get; set; }
        public string Sex { get; set; }
        public string Education { get; set; }
        public string Post { get; set; }
        public double Salary { get; set; }
        public int Prime { get; set; }

        public Employer()
        {
        }

        public Employer(int id, string name, DateTime birthDay, string sex, string education, string post, double salary, int prime)
        {
            ID = id;
            Name = name;
            BirthDay = birthDay;
            Sex = sex;
            Education = education;
            Post = post;
            Salary = salary;
            Prime = prime;
        }
    }
}
