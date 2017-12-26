using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace GraduationTracker.Tests.Unit
{
    [TestClass]
    public class GraduationTrackerTestsPratibha
    {
        Diploma diploma = new Diploma
        {
            Id = 1,
            Credits = 4,
            Requirements = new int[] { 100, 102, 103, 104 }
        };

        Student[] students = new[]
        {
               new Student
               {
                   Id = 1,
                   Courses = new Course[]
                   {
                        new Course{Id = 1, Name = "Math", Mark=95 },
                        new Course{Id = 2, Name = "Science", Mark=95 },
                        new Course{Id = 3, Name = "Literature", Mark=95 },
                        new Course{Id = 4, Name = "Physichal Education", Mark=95 }
                   }
               },
               new Student
               {
                   Id = 2,
                   Courses = new Course[]
                   {
                        new Course{Id = 1, Name = "Math", Mark=80 },
                        new Course{Id = 2, Name = "Science", Mark=80 },
                        new Course{Id = 3, Name = "Literature", Mark=80 },
                        new Course{Id = 4, Name = "Physichal Education", Mark=80 }
                   }
               },
            new Student
            {
                Id = 3,
                Courses = new Course[]
                {
                    new Course{Id = 1, Name = "Math", Mark=50 },
                    new Course{Id = 2, Name = "Science", Mark=50 },
                    new Course{Id = 3, Name = "Literature", Mark=50 },
                    new Course{Id = 4, Name = "Physichal Education", Mark=50 }
                }
            },
            new Student
            {
                Id = 4,
                Courses = new Course[]
                {
                    new Course{Id = 1, Name = "Math", Mark=40 },
                    new Course{Id = 2, Name = "Science", Mark=40 },
                    new Course{Id = 3, Name = "Literature", Mark=40 },
                    new Course{Id = 4, Name = "Physichal Education", Mark=40 }
                }
            },

            // A student with only 3 courses
            new Student
            {
                Id = 5,
                Courses = new Course[]
                {
                    new Course{Id = 1, Name = "Math", Mark=40 },
                    new Course{Id = 2, Name = "Science", Mark=40 },
                    new Course{Id = 3, Name = "Literature", Mark=40 }
                }
            }
        };

        [TestMethod]
        public void TestHasGraduated()
        {
            var tracker = new GraduationTracker();

            
            
            var graduated = new List<Tuple<bool, STANDING>>();

            foreach(var student in students)
            {
                graduated.Add(tracker.HasGraduated(diploma, student));      
            }

            Assert.IsFalse(graduated.Any(g => g.Item2.Equals(STANDING.Remedial)));
            Assert.IsFalse(tracker.HasGraduated(diploma, students.FirstOrDefault(s=>s.Id.Equals(4))).Item1); //Student with Id=4 has not graduated
            Assert.IsFalse(tracker.HasGraduated(diploma, students.FirstOrDefault(s => s.Id.Equals(5))).Item1); //Student with Id=5 has not graduated
            Assert.IsTrue(tracker.HasGraduated(diploma, students.FirstOrDefault(s => s.Id.Equals(1))).Item1); //Student with Id=1 has not graduated
            Assert.IsTrue(tracker.HasGraduated(diploma, students.FirstOrDefault(s => s.Id.Equals(2))).Item1); //Student with Id=2 has not graduated
            Assert.IsTrue(tracker.HasGraduated(diploma, students.FirstOrDefault(s => s.Id.Equals(3))).Item1); //Student with Id=3 has not graduated
            Assert.AreEqual(2, graduated.FindAll(grad =>grad.Item1.Equals(false)).Count); //One student has not graduated
            Assert.AreEqual(3, graduated.FindAll(grad => grad.Item1.Equals(true)).Count); //Three students have graduated
        
        }


        [TestMethod]
        public void TestGetStudent()
        {
            var student = Repository.GetStudent(1);
            Assert.AreEqual(1, student.Id);
            Assert.AreEqual(4, student.Courses.Count());
            Assert.IsNotNull(student.Courses[0]);
            Assert.AreEqual(1, student.Courses[0].Id);
            Assert.AreEqual(95, student.Courses[0].Mark);
            Assert.AreEqual("Math", student.Courses[0].Name);
        }


        [TestMethod]
        public void TestGetDiploma()
        {
            var diploma = Repository.GetDiploma(1);
            Assert.AreEqual(1, diploma.Id);
            Assert.AreEqual(4, diploma.Credits);
            Assert.AreEqual(4, diploma.Requirements.Count());
            Assert.IsNotNull(diploma.Requirements[0]);
        }

        [TestMethod]
        public void TestGetRequirement()
        {
            var requirement = Repository.GetRequirement(100);
            Assert.AreEqual(100, requirement.Id);
            Assert.AreEqual(1, requirement.Credits);
            Assert.AreEqual(50, requirement.MinimumMark);
            Assert.AreEqual("Math", requirement.Name);
            Assert.IsTrue(requirement.Courses.Count() > 0);
        }
    }
}
