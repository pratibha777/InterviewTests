using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationTracker
{
    public partial class GraduationTracker
    {   
        public Tuple<bool, STANDING>  HasGraduated(Diploma diploma, Student student)
        {
            var credits = 0;
            var average = 0;
            var totalMarks = 0;
            bool hasGraduated = false;
        
            //for(int i = 0; i < diploma.Requirements.Length; i++)
            //{
            //    for(int j = 0; j < student.Courses.Length; j++)
            //    {
            //        var requirement = Repository.GetRequirement(diploma.Requirements[i]);

            //        for (int k = 0; k < requirement.Courses.Length; k++)
            //        {
            //            if (requirement.Courses[k] == student.Courses[j].Id)
            //            {
            //                average += student.Courses[j].Mark;
            //                if (student.Courses[j].Mark > requirement.MinimumMark)
            //                {
            //                    credits += requirement.Credits;
            //                }
            //            }
            //        }
            //    }
            //}

            foreach(var requirementId in diploma.Requirements)
            {
                Requirement requirement = Repository.GetRequirement(requirementId);
                foreach(var course in student.Courses)
                {
                    if (requirement.Courses.Any(c => c.Equals(course.Id)))
                    {
                        totalMarks += course.Mark;
                        if (course.Mark >= requirement.MinimumMark)
                            credits += requirement.Credits;
                    }
                }
            }


            //Assumption: if a student does not get required credits for a diploma, his standing will be none

            var standing = STANDING.None;

            if (credits >= diploma.Credits)
            {
                hasGraduated = true;

                average = totalMarks / student.Courses.Length;                

                if (average < 50)
                    standing = STANDING.Remedial;
                else if (average < 80)
                    standing = STANDING.Average;
                else if (average < 95)
                    standing = STANDING.MagnaCumLaude;
                else
                    standing = STANDING.SumaCumLaude; //highest honors
            }

            //student.Standing = standing;
            

            switch (standing)
            {
                case STANDING.Remedial:
                    return new Tuple<bool, STANDING>(hasGraduated, standing);
                case STANDING.Average:
                    return new Tuple<bool, STANDING>(hasGraduated, standing);
                case STANDING.SumaCumLaude:
                    return new Tuple<bool, STANDING>(hasGraduated, standing);
                case STANDING.MagnaCumLaude:
                    return new Tuple<bool, STANDING>(hasGraduated, standing);

                default:
                    return new Tuple<bool, STANDING>(hasGraduated, standing);
            } 
        }
    }
}
