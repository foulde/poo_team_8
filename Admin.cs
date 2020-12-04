﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project_POO
{
    class Admin : User
    {
        public Admin(string Username, string UserPassword, string name, int age, string status, int ID)
            : base(Username, UserPassword, name, age, status, ID) { }

        public void Modify_Courses(Course course)
        {
            int a = 0;
            while (a == 0)
            {
                Console.WriteLine("What change would you want to do? \r\n 1)Name of the course \r\n 2)The Faculty \r\n 3)The content \r\n 4)The day \r\n 5)The starting hour \r\n 6)The ending hour \r\n 7)Back");
                int choose = Convert.ToInt32(Console.ReadLine());
                Console.Clear();
                switch (choose)
                {
                    case 1:
                        Console.Write("Choose the new name =>");
                        course.CourseName = Console.ReadLine();
                        break;
                    case 2:
                        break;
                    case 3:
                        Console.Write("Choose the new content =>");
                        course.Content = name = Console.ReadLine();
                        break;
                    case 4:
                        Console.Write("Choose the new day =>");
                        course.CourseDay = name = Console.ReadLine();
                        break;
                    case 5:
                        Console.Write("Choose the new starting hour =>");
                        course.StartingHour = name = Console.ReadLine();
                        break;
                    case 6:
                        Console.Write("Choose the new ending hour =>");
                        course.EndingHour = name = Console.ReadLine();
                        break;
                    case 7:
                        a = 1;
                        break;
                    default:
                        Console.WriteLine("Wrong input");
                        break;
                }
                Console.Clear();
            }
        }
        public Course Create_Course()
        {
            Console.Write("choose the name of the course =>");
            string courseName = Console.ReadLine();
            Console.Write("choose the Faculty =>");
            string courseFaculty = Console.ReadLine();
            Console.Write("choose the content =>");
            string courseContent = Console.ReadLine();
            Console.Write("choose the day =>");
            string courseDay = Console.ReadLine();
            Console.Write("choose the start hour =>");
            string courseStartingHour = Console.ReadLine();
            Console.Write("choose the ending hour =>");
            string courseEndingHours = Console.ReadLine();
            Console.Write("choose the classroom =>");
            string courseclassroom = Console.ReadLine();
            return new Course(courseName, courseContent, courseFaculty, courseDay, courseStartingHour, courseEndingHours, courseclassroom);
        }

    }
}
