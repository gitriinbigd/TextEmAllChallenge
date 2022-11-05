namespace TextEmAll.DataAccess
{
    using System.Collections.Generic;
    using System.Linq;
    using TextEmAll.Models;

    public interface IChallenge1Service
    {
        ICollection<Challenge1Model> GetStudentGPA();
    }

    /// <summary>
    /// A simple service to implement our query that returns a calculated GPA. This could be implemented 
    /// in a number of ways - it could be 
    /// </summary>
    public class Challenge1Service : IChallenge1Service
    {
        private readonly Challenge1DbContext _context;
        public Challenge1Service(Challenge1DbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Concrete implementation of the query student GPA requirement 
        /// </summary>
        /// <returns>Collection of records matching our output criteria</returns>
        public ICollection<Challenge1Model> GetStudentGPA()
        {
            return GetStudentGPAUsingQuerySyntax();
        }

        /// <summary>
        /// Implementation of the query student GPA requirement using query syntax not surfaced 
        /// through the interface as a means to demonstrate different ways to extract data 
        /// </summary>
        /// <returns>Collection of records matching our output criteria</returns>
        public ICollection<Challenge1Model> GetStudentGPAUsingQuerySyntax()
        {
            return (from s in _context.Students
                join ce in _context.CourseEnrollments on s.PersonId equals ce.StudentId
                join c in _context.Courses on ce.CourseId equals c.CourseId
                where ce.Grade != null
                group new
                {
                    s.PersonId,
                    s.FirstName,
                    s.LastName,
                    ce.Grade,
                    c.Credits
                }
                by new
                {
                    s.PersonId,
                    s.FirstName,
                    s.LastName
                } into results
                orderby results.Key.LastName ascending, results.Key.FirstName ascending
                select new Challenge1Model()
                {
                    StudentId = results.Key.PersonId,
                    FirstName = results.Key.FirstName,
                    LastName = results.Key.LastName,
                    GPA = results.Sum(g => g.Grade.GetValueOrDefault(0M) * g.Credits) / results.Sum(g => g.Credits)
                })
                .ToList();
        }

        /// <summary>
        /// Implementation of the query student GPA requirement using method syntax not surfaced 
        /// through the interface as a means to demonstrate different ways to extract data 
        /// </summary>
        /// <returns>Collection of records matching our output criteria</returns>
        public ICollection<Challenge1Model> GetStudentGPAUsingMethodSyntax()
        {
            return _context.Students
                .Join(_context.CourseEnrollments, s => s.PersonId, ce => ce.StudentId, (s, ce) => new
                {
                    s.PersonId,
                    s.FirstName,
                    s.LastName,
                    ce.Grade,
                    ce.Course.Credits
                })
                .Where(selected => selected.Grade != null)
                .GroupBy(key => new { key.PersonId, key.FirstName, key.LastName }, elem => new { elem.PersonId, elem.FirstName, elem.LastName, elem.Grade, elem.Credits })
                .Select(results => new Challenge1Model()
                {
                    StudentId = results.Key.PersonId,
                    FirstName = results.Key.FirstName,
                    LastName = results.Key.LastName,
                    GPA = results.Sum(g => g.Grade.GetValueOrDefault(0M) * g.Credits) / results.Sum(g => g.Credits)
                })
                .ToList();
        }
    }
}
