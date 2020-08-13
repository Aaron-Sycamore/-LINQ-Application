using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//answer to question 2.1
namespace Assignment_8
{
    class Course
    {
        public string CourseID;
        public string Subject;
        public string CourseCode;
        public string Title;
        public string Location;
        public string Instructor;

        public Course(string ID, string Sub, string Code, string Ti, string Loc, string Inst)
        {
            CourseID = ID;
            Subject = Sub;
            CourseCode = Code;
            Title = Ti;
            Location = Loc;
            Instructor = Inst;
        }
        public void printer()
        {
            Console.WriteLine(CourseID);
            Console.WriteLine(Subject);
            Console.WriteLine(CourseCode);
            Console.WriteLine(Title);
            Console.WriteLine(Location);
            Console.WriteLine(Instructor);
        }
    }
}
