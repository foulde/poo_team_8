﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POO_Projet
{
    class Student:User
    {
        
        public List<string> studentMissing { get; set; }
        public Fees feesDetails { get; set; }
        public string classroom { get; set; }
        public int year { get; set; }

        public Student(string Username, string UserPassword, string name, int age, string status, int ID)
            :base(Username, UserPassword, name, age, status, ID)
        {
            StreamReader LectStudents = new StreamReader("Students.csv");
            while (LectStudents.Peek() > 0)
            {
                string[] datas = LectStudents.ReadLine().Split(';');
                if (Username == datas[0])
                {
                    year = Convert.ToInt32(datas[1]);
                    classroom = datas[2];
                    feesDetails = new Fees(Convert.ToDouble(datas[3]), Convert.ToBoolean(datas[4]), Convert.ToDouble(datas[5]));
                    studentMissing = new List<string>(datas[6].Split(','));
                }
            }
             LectStudents.Close();

        }

        public string studentDetails()
        {
            //ajouter paramètres venant de user
            string s = $"{this.name}, Group {this.classroom}\n Student ID : {this.ID}\n {this.Username}\n Payment status : {this.feesDetails.PaymentStatus}";
            return s;
        }

        public void DisplayTimetable()
        {
            SortedList<string, Course> MondayList = new SortedList<string, Course>();
            SortedList<string, Course> TuesdayList = new SortedList<string, Course>();
            SortedList<string, Course> WednesdayList = new SortedList<string, Course>();
            SortedList<string, Course> ThursdayList = new SortedList<string, Course>();
            SortedList<string, Course> FridayList = new SortedList<string, Course>();

            StreamReader LectCourse = new StreamReader("Courses.csv");

            while (LectCourse.Peek() > 0)
            {
                bool success = false;
                string[] datas = LectCourse.ReadLine().Split(';');
                Course course = new Course(datas[0], datas[1], datas[2], datas[3], datas[4], datas[5], datas[6]);
                if (classroom == course.CourseName)
                {
                    switch (course.CourseDay)       //according to the day
                    {
                        case "monday":
                            success = AddCourseToDay(course, MondayList);
                            break;
                        case "tuesday":
                            success = AddCourseToDay(course, TuesdayList);
                            break;
                        case "wednesday":
                            success = AddCourseToDay(course, WednesdayList);
                            break;
                        case "thursday":
                            success = AddCourseToDay(course, ThursdayList);
                            break;
                        case "friday":
                            success = AddCourseToDay(course, FridayList);
                            break;
                        default:
                            success = false;
                            Console.WriteLine("Wrong day input");       //if the input is neither of the 5 days, the program returns false.
                            break;
                    }
                }
            }
            LectCourse.Close();

            Console.WriteLine("Monday");
            Console.WriteLine();
            for (int n = 0; n < MondayList.Count; n++)
            {
                Console.WriteLine(MondayList.Values[n].ToStringTimetable());
            }
            Console.WriteLine("Tuesday");
            Console.WriteLine();
            for (int n = 0; n < TuesdayList.Count; n++)
            {
                Console.WriteLine(TuesdayList.Values[n].ToStringTimetable());
            }
            Console.WriteLine("Wednesday");
            Console.WriteLine();
            for (int n = 0; n < WednesdayList.Count; n++)
            {
                Console.WriteLine(WednesdayList.Values[n].ToStringTimetable());
            }
            Console.WriteLine("Thursday");
            Console.WriteLine();
            for (int n = 0; n < ThursdayList.Count; n++)
            {
                Console.WriteLine(ThursdayList.Values[n].ToStringTimetable());
            }
            Console.WriteLine("Friday");
            Console.WriteLine();
            for (int n = 0; n < FridayList.Count; n++)
            {
                Console.WriteLine(FridayList.Values[n].ToStringTimetable());
            }
        }
        private bool AddCourseToDay(Course course, SortedList<string, Course> DayList)
        {
            bool success2 = true;
            try
            {
                DayList.Add(course.StartingHour, course);       //starting hours are used as the key of the sorted list, thus the courses are sorted by their starting hour
                if (DayList.Count > 1)  //if there are more than one course a day, we must check if they are not overlapping
                {
                    for (int n = 1; n < DayList.Count; n++)
                    {
                        if (CompareHours(DayList.Values[n - 1].EndingHour, DayList.Values[n].StartingHour) == false)
                        {
                            success2 = false;
                        }
                    }
                    if (success2 == false)
                    {
                        DayList.Remove(course.StartingHour);        //if the addition of the course cause overlapping courses, we remove it.
                        Console.WriteLine("There are overlapping courses.");
                    }
                }
            }
            catch (ArgumentException)       //2 courses cannot have the same starting hour, and 2 elements in a sorted list cannot have the same key
            {
                Console.WriteLine("The starting hour " + course.StartingHour + " is already taken.");
                success2 = false;
            }
            return success2;
        }
        public bool CompareHours(string end1, string start2)
        {
            return (string.Compare(end1, start2) <= 0);
        }

    }
}
