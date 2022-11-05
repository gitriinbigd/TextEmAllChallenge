namespace TextEmAll.Tests
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using TextEmAll.DataAccess;
    using TextEmAll.Domain;

    public class DomainTests
    {
        private TestSetup _testSetup;

        [SetUp]
        public void Setup()
        {
            // Important Note: typically we would not do this in this way but am doing it here 
            // since this is exclusively for demonstration purposes. I wanted to leave the total 
            // configuration required to a minimum to have this set up work on any platform 
            _testSetup = new TestSetup();
        }

        [Test]
        public void ContextDepartments_ReturnsExpectedResults()
        {
            //arrange 
            Challenge1DbContext ctx= _testSetup.ServiceProvider.GetService<Challenge1DbContext>();

            //act 
            List<Department> results = ctx.Departments.ToList();
            Department sample = ctx.Departments.Where(d => d.DepartmentId == 2).FirstOrDefault();

            //assert 
            Assert.AreEqual(4, results.Count);
            Assert.AreEqual("English", sample.Name);
            Assert.AreEqual(120000M, sample.Budget);
            Assert.AreEqual(6, sample.Administrator);
            Assert.AreEqual(new DateTime(2007, 9, 1), sample.StartDate);
        }

        [Test]
        public void ContextStudents_ReturnsExpectedResults()
        {
            //arrange 
            Challenge1DbContext ctx = _testSetup.ServiceProvider.GetService<Challenge1DbContext>();

            //act 
            List<Student> results = ctx.Students.ToList();
            Student sample = ctx.Students.Where(d => d.PersonId == 2)
                .Include(s => s.Courses)
                .ThenInclude(ce => ce.Course)
                .ThenInclude(c => c.Department)
                .FirstOrDefault();
            CourseEnrollment enrollment = sample.Courses.FirstOrDefault(ce => ce.CourseId == 2030);

            //assert 
            Assert.AreEqual(25, results.Count);
            Assert.AreEqual("Gytis", sample.FirstName);
            Assert.AreEqual("Barzdukas", sample.LastName);
            Assert.AreEqual(new DateTime(2005, 9, 1), sample.EnrollmentDate);
            Assert.AreEqual(2, sample.Courses.Count);
            Assert.AreEqual(3.5, enrollment.Grade);
            Assert.AreEqual("Poetry", enrollment.Course.Title);
            Assert.AreEqual(2, enrollment.Course.Credits);
            Assert.AreEqual("http://www.fineartschool.net/Poetry", (enrollment.Course as OnlineCourse).Url);
            Assert.AreEqual("English", enrollment.Course.Department.Name);
            Assert.AreEqual(120000M, enrollment.Course.Department.Budget);
            Assert.AreEqual(new DateTime(2007, 09, 01), enrollment.Course.Department.StartDate);
            Assert.AreEqual(6, enrollment.Course.Department.Administrator);
        }

        [Test]
        public void ContextInstructors_ReturnsExpectedResults()
        {
            //arrange 
            Challenge1DbContext ctx = _testSetup.ServiceProvider.GetService<Challenge1DbContext>();

            //act 
            List<Instructor> results = ctx.Instructors.ToList();
            Instructor sample = ctx.Instructors.Where(d => d.PersonId == 31)
                .Include(i => i.OfficeAssignment)
                .Include(i => i.Courses)
                .ThenInclude(ci => ci.Course)
                .ThenInclude(c => c.Department)
                .FirstOrDefault();
            CourseInstructor assignment = sample.Courses.FirstOrDefault(ce => ce.CourseId == 1061);

            //assert 
            Assert.AreEqual(9, results.Count);
            Assert.AreEqual("Jasmine", sample.FirstName);
            Assert.AreEqual("Stewart", sample.LastName);
            Assert.AreEqual(new DateTime(1997, 10, 12), sample.HireDate);
            Assert.AreEqual("131 Smith", sample.OfficeAssignment.Location);
            Assert.AreEqual(new byte[] { 0, 0, 0, 0, 0, 0, 7, 215 }, sample.OfficeAssignment.Timestamp);
            Assert.AreEqual(1, sample.Courses.Count);
            Assert.AreEqual("Physics", assignment.Course.Title);
            Assert.AreEqual(4, assignment.Course.Credits);
            Assert.AreEqual("234 Smith", (assignment.Course as OnsiteCourse).Location);
            Assert.AreEqual("TWHF", (assignment.Course as OnsiteCourse).Days);
            Assert.AreEqual("Engineering", assignment.Course.Department.Name);
            Assert.AreEqual(350000M, assignment.Course.Department.Budget);
            Assert.AreEqual(new DateTime(2007, 09, 01), assignment.Course.Department.StartDate);
            Assert.AreEqual(2, assignment.Course.Department.Administrator);
        }

        [Test]
        public void ContextCourses_ReturnsExpectedResults()
        {
            //arrange 
            Challenge1DbContext ctx = _testSetup.ServiceProvider.GetService<Challenge1DbContext>();

            //act 
            List<Course> results = ctx.Courses.ToList();
            Course sample = ctx.Courses.Where(d => d.CourseId == 4022)
                .Include(i => i.Department)
                .Include(i => i.Instructors).ThenInclude(i => i.Instructor)
                .Include(i => i.Students).ThenInclude(i => i.Student)
                .FirstOrDefault();
            CourseInstructor assignment = sample.Instructors.FirstOrDefault(ce => ce.PersonId == 18);
            CourseEnrollment enrollment = sample.Students.FirstOrDefault(ce => ce.StudentId == 16);

            //assert 
            Assert.AreEqual(10, results.Count);
            Assert.AreEqual("Microeconomics", sample.Title);
            Assert.AreEqual(3, sample.Credits);
            Assert.AreEqual("Economics", sample.Department.Name);
            Assert.AreEqual(1, sample.Instructors.Count);
            Assert.AreEqual("Zheng", assignment.Instructor.LastName);
            Assert.AreEqual(8, sample.Students.Count);
            Assert.AreEqual("Jai", enrollment.Student.LastName);
        }

        [Test]
        public void ContextOnlineCourses_ReturnsExpectedResults()
        {
            //arrange 
            Challenge1DbContext ctx = _testSetup.ServiceProvider.GetService<Challenge1DbContext>();

            //act 
            List<OnlineCourse> results = ctx.OnlineCourses.ToList();
            OnlineCourse sample = ctx.OnlineCourses.Where(d => d.CourseId == 2021).FirstOrDefault();

            //assert 
            Assert.AreEqual(4, results.Count);
            Assert.AreEqual("Composition", sample.Title);
            Assert.AreEqual(3, sample.Credits);
            Assert.AreEqual("http://www.fineartschool.net/Composition", sample.Url);
        }

        [Test]
        public void ContextOnsiteCourses_ReturnsExpectedResults()
        {
            //arrange 
            Challenge1DbContext ctx = _testSetup.ServiceProvider.GetService<Challenge1DbContext>();

            //act 
            List<OnsiteCourse> results = ctx.OnsiteCourses.ToList();
            OnsiteCourse sample = ctx.OnsiteCourses.Where(d => d.CourseId == 1050).FirstOrDefault();

            //assert 
            Assert.AreEqual(6, results.Count);
            Assert.AreEqual("Chemistry", sample.Title);
            Assert.AreEqual(4, sample.Credits);
            Assert.AreEqual("MTWH", sample.Days);
            Assert.AreEqual(new TimeSpan(11, 30, 0), sample.Time);
    
        }
    }
}
