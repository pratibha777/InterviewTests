using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace GraduationTracker.Tests.Unit
{
    [TestClass]
    public class GraduationTrackerTests
    {
        [TestMethod]
        public void TestHasCredits()
        {
            var tracker = new GraduationTracker();

            var diploma = new Diploma
            {
                Id = 1,
                Credits = 4,
                Requirements = new int[] { 100, 102, 103, 104 }
            };

            var students = new[]
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
            }


            //tracker.HasGraduated()
        };
            
            var graduated = new List<Tuple<bool, STANDING>>();

            foreach(var student in students)
            {
                graduated.Add(tracker.HasGraduated(diploma, student));      
            }

            Assert.IsFalse(graduated.Any(g => g.Item2.Equals(STANDING.Remedial)));

            Assert.IsFalse(tracker.HasGraduated(diploma, students.FirstOrDefault(s=>s.Id.Equals(4))).Item1); //Student with Id=4 has not graduated
            Assert.IsTrue(tracker.HasGraduated(diploma, students.FirstOrDefault(s => s.Id.Equals(1))).Item1); //Student with Id=1 has not graduated
            Assert.IsTrue(tracker.HasGraduated(diploma, students.FirstOrDefault(s => s.Id.Equals(2))).Item1); //Student with Id=2 has not graduated
            Assert.IsTrue(tracker.HasGraduated(diploma, students.FirstOrDefault(s => s.Id.Equals(3))).Item1); //Student with Id=3 has not graduated
            Assert.AreEqual(1, graduated.FindAll(grad =>grad.Item1.Equals(false)).Count); //One student has not graduated
            Assert.AreEqual(3, graduated.FindAll(grad => grad.Item1.Equals(true)).Count); //Three students have graduated
          
        }        
    }
}
