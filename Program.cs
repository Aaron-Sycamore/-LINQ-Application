using System;
using System.IO;
using System.Xml;
using System.Web;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Linq;

namespace Assignment_8
{
    class Program
    {

        static void Main(string[] args)
        {
            string fLocation = Directory.GetCurrentDirectory() + "/App_Data/Course.xml";
            using (var textReader = new StreamReader("Courses.csv"))
            {
                //string fLocation = Path.Combine(Request.PhysicalApplicationPath, @"App_Data\Courses.xml");
                string line = textReader.ReadLine();
                int skipCount = 0;
                while (line != null && skipCount < 1)
                {
                    line = textReader.ReadLine();
                    skipCount++;
                }
                while (line != null)
                {
                    //question 2.2 as this is were I creat an array of Courses
                    string[] columns = line.Split(',');
                    //perform your logic
                    Course newCourse = new Course(columns[2], columns[0], columns[1], columns[3], columns[9], columns[10]);

                    if (File.Exists(fLocation))
                    {
                        XDocument xDoc = XDocument.Load(fLocation);
                        XElement messages = xDoc.Element("Courses");
                        messages.Add(new XElement("Course", new XElement("CourseID", newCourse.CourseID), new XElement("Subject", newCourse.Subject), new XElement("CourseCode", newCourse.CourseCode), new XElement("Location", newCourse.Location), new XElement("Instructor", newCourse.Instructor)));
                        xDoc.Save(fLocation);

                    }
                    else
                    {
                        FileStream fState = null;
                        try
                        {
                            fState = new FileStream(fLocation, FileMode.CreateNew);
                            XmlTextWriter writer = new XmlTextWriter(fState, System.Text.Encoding.Unicode);
                            writer.Formatting = Formatting.Indented;
                            writer.WriteStartDocument();
                            writer.WriteStartElement("Courses");
                            writer.WriteStartElement("Course");
                            writer.WriteElementString("CourseID", newCourse.CourseID);
                            writer.WriteElementString("Subject", newCourse.Subject);
                            writer.WriteElementString("Title", newCourse.Title);
                            writer.WriteElementString("CourseCode", newCourse.CourseCode);
                            writer.WriteElementString("Location", newCourse.Location);
                            writer.WriteElementString("Instructor", newCourse.Instructor);
                            writer.WriteEndElement();
                            writer.WriteEndElement();
                            writer.WriteEndDocument();
                            writer.Close();
                            fState.Close();
                        }
                        finally
                        {
                            if (fState != null) fState.Close();

                        }
                    }

                    line = textReader.ReadLine();
                }
            }
            //end of answer to question 2.2
            Console.WriteLine("");
            Console.WriteLine("Question 2.3 A startes here");
            Console.WriteLine("");
            //Question 2.3 A startes here        
            XElement myCourses = XElement.Load(fLocation);

            IEnumerable<XElement> courseQuery =
                from c in myCourses.Elements("Course")
                where Int32.Parse((string)c.Element("CourseCode").Value) >= 200 &&
                (string)c.Element("Subject") == "CPI"
                orderby (string)c.Element("Instructor")
                select c;

            foreach (XElement c in courseQuery)
            {
                Console.WriteLine("Course Code Title: " + (string)c.Element("CourseCode").Value + " Course Instructor: " + (string)c.Element("Instructor").Value);
            }
            //end of answer to question 2.3 A
            Console.WriteLine("");
            Console.WriteLine("Question 2.3 B startes here");
            Console.WriteLine("");
            //Question 2.3 B startes here  

            var courseQuery2 =
                 from c in myCourses.Elements("Course")
                 group c by (string)c.Element("Subject").Value into sub
                 select new
                 {
                     sub.Key,
                     Count = sub.Count(),
                     SubGroup = from c in sub
                                group c by Int32.Parse((string)c.Element("CourseCode").Value) into sub2
                                select sub2
                 };

            foreach (var c in courseQuery2)
            {
                if (c.Count >= 2)
                {
                    foreach (var sub in c.SubGroup)
                    {
                        Console.WriteLine(c.Key + " " + sub.Key);
                    }
                }
            }
            //end of answer to question 2.3 B
            Console.WriteLine("");
            Console.WriteLine("Question 2.4 startes here");
            Console.WriteLine("");
            //Question 2.4 startes here




            List<Instructor> Instructors = new List<Instructor>();
            using (var textReader = new StreamReader("Instructors.csv"))
            {
                string line = textReader.ReadLine();
                int skipCount = 0;
                while (line != null && skipCount < 1)
                {
                    line = textReader.ReadLine();
                    skipCount++;
                }
                while (line != null)
                {
                    //question 1.4 as this is were I creat an List of Instructor
                    string[] columns = line.Split(',');
                    //perform your logic
                    Instructor newInstructor = new Instructor(columns[0], columns[1], columns[2]);
                    Instructors.Add(newInstructor);
                    
                    line = textReader.ReadLine();
                }
            }


            XElement myCourses2 = XElement.Load(fLocation);



            XElement courseQuery3 = new XElement("Collection",
                from c in myCourses.Elements("Course")
                join Instructor Instruc in Instructors on (string)c.Element("Instructor").Value equals Instruc.Name into sub3
                select new XElement("Answer",
                                    new XElement("CourseCode", (string)c.Element("CourseCode").Value),
                                    new XElement("Subject", (string)c.Element("Subject").Value),
                                      from subset in sub3
                                      select new XElement("Email", subset.Email)));



            IEnumerable<XElement> courseQuery4 =
                from c in courseQuery3.Elements("Answer")
                where Int32.Parse((string)c.Element("CourseCode").Value) >= 200 
                orderby (string)c.Element("CourseCode")
                select c;

            foreach (var c in courseQuery4)
            {
                if (Int32.Parse((string)c.Element("CourseCode")) >= 200 && Int32.Parse((string)c.Element("CourseCode")) < 300)
                {
                    Console.WriteLine(c);
                }
            }


            if (File.Exists(fLocation))
            {
                File.Delete(fLocation);
                
            }
        }
    }
}
