namespace TextEmAll.Tests
{
    using Microsoft.Extensions.DependencyInjection;
    using NUnit.Framework;
    using System.Collections.Generic;
    using System.Linq;
    using TextEmAll.DataAccess;
    using TextEmAll.Models;

    public class Challenge1Tests
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
        public void QueryStudentGPAUsingQuerySyntax_ReturnsExpectedResults()
        {
            //arrange 
            Challenge1Service service = _testSetup.ServiceProvider.GetService<IChallenge1Service>() as Challenge1Service;

            //act 
            ICollection<Challenge1Model> results = service.GetStudentGPAUsingQuerySyntax();

            //assert 
            AssertCollectionIsValid(results);
        }

        [Test]
        public void QueryStudentGPAUsingMethodSyntax_ReturnsExpectedResults()
        {
            //arrange 
            Challenge1Service service = _testSetup.ServiceProvider.GetService<IChallenge1Service>() as Challenge1Service;

            //act 
            ICollection<Challenge1Model> results = service.GetStudentGPAUsingMethodSyntax();

            //assert 
            AssertCollectionIsValid(results);
        }

        private void AssertCollectionIsValid(ICollection<Challenge1Model> results)
        {
            /* Important Note: this is a BAD way to test on static results, however, it demonstrates a dirty way 
             * to build quick unit tests to validate outcomes over known datasets, the kind that might be found 
             * in dev or qa databases 
             * -------------------------------------------------------------------------------------------------
            SELECT 'Assert.AreEqual(' + convert(varchar, COALESCE(SUM([s].[Grade] * CAST([t].[Credits] AS decimal(18,2))), 0.0) / CAST(COALESCE(SUM([t].[Credits]), 0) AS decimal(18,2)))+ ', results.First(r => r.StudentId == ' + convert(varchar, [p].[PersonId])+ ').GPA);'
            FROM [Person] AS [p]
            INNER JOIN [StudentGrade] AS [s] ON [p].[PersonId] = [s].[StudentId]
            INNER JOIN [Course] AS [t] ON [s].[CourseId] = [t].[CourseId]
            WHERE ([p].[Discriminator] = N'Student') AND [s].[Grade] IS NOT NULL
            GROUP BY [p].[PersonId], [p].[FirstName], [p].[LastName]
            ORDER BY [p].[LastName], [p].[FirstName]	
             */
            Assert.AreEqual(3.062500, results.First(r => r.StudentId == 22).GPA);
            Assert.AreEqual(4.000000, results.First(r => r.StudentId == 13).GPA);
            Assert.AreEqual(3.800000, results.First(r => r.StudentId == 2).GPA);
            Assert.AreEqual(3.800000, results.First(r => r.StudentId == 2).GPA);
            Assert.AreEqual(4.000000, results.First(r => r.StudentId == 29).GPA);
            Assert.AreEqual(2.000000, results.First(r => r.StudentId == 21).GPA);
            Assert.AreEqual(2.000000, results.First(r => r.StudentId == 16).GPA);
            Assert.AreEqual(3.400000, results.First(r => r.StudentId == 3).GPA);
            Assert.AreEqual(3.071428, results.First(r => r.StudentId == 6).GPA);
            Assert.AreEqual(2.500000, results.First(r => r.StudentId == 11).GPA);
            Assert.AreEqual(4.000000, results.First(r => r.StudentId == 24).GPA);
            Assert.AreEqual(2.142857, results.First(r => r.StudentId == 23).GPA);
            Assert.AreEqual(3.785714, results.First(r => r.StudentId == 7).GPA);
            Assert.AreEqual(3.000000, results.First(r => r.StudentId == 8).GPA);
            Assert.AreEqual(2.500000, results.First(r => r.StudentId == 15).GPA);
            Assert.AreEqual(3.250000, results.First(r => r.StudentId == 26).GPA);
            Assert.AreEqual(3.750000, results.First(r => r.StudentId == 30).GPA);
            Assert.AreEqual(4.000000, results.First(r => r.StudentId == 20).GPA);
            Assert.AreEqual(3.500000, results.First(r => r.StudentId == 9).GPA);
            Assert.AreEqual(3.000000, results.First(r => r.StudentId == 14).GPA);
            Assert.AreEqual(3.000000, results.First(r => r.StudentId == 28).GPA);

        }
    }

}
