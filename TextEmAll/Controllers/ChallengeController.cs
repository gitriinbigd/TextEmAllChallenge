namespace TextEmAll.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Net;
    using TextEmAll.DataAccess;
    using TextEmAll.Models;

    /// <summary>
    /// The controller to manage our API. I have not included any real error handling or logging here as with most shops, 
    /// there is already a model for handling that be it ELK, Splunk, or some other custom tooling. Rather than spend 
    /// time developing infrastructure that would realistically never be used, I opted to simply note the ommission 
    /// </summary>
    [ApiController]
    public class ChallengeController : ControllerBase
    {
        private readonly IChallenge1Service _challenge1Service;

        /// <summary>
        /// This is a controller that surfaces two API endpoints - one to exercise each challenge presented 
        /// </summary>
        /// <param name="challenge1Service">a service that handles the first challenge injected since it has dependencies</param>
        public ChallengeController(IChallenge1Service challenge1Service)
        {
            _challenge1Service = challenge1Service;
        }

        /// <summary>
        /// Test endpoint to provide a way to exercise Challenge 1
        /// </summary>
        /// <returns>a json object containing a collection of student info showing id, name, and gpa</returns>
        [HttpGet]
        [Route("/students")]
        [ProducesResponseType(typeof(ICollection<Challenge1Model>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult StudentGpaList()
        {
            try
            {
                return Ok(_challenge1Service.GetStudentGPA());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Test endpoint to provide a way to exercise Challenge 2
        /// </summary>
        /// <param name="input">The string whose "max distance" we want to calculate</param>
        /// <returns>a json object containing the MaxDistance we calculated</returns>
        [HttpGet]
        [Route("/getmaxdistance/{input}")]
        [ProducesResponseType(typeof(Challenge2Model), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public IActionResult GetMaxDistance(string input)
        {
            // create our service (not injected via DI since it has no dependencies and is cheap to instantiate) 
            IChallenge2Service service = new Challenge2Service();

            // use our service to calculate a distance 
            int distance = service.MaxDistance(input);

            // if we got no result from our "service" return an HTTP 404 not found 
            if (distance == IChallenge2Service.NO_RESULT)
            {
                return NotFound();
            }

            // otherwise, let's create an anonymous object and return the max distance we calculated 
            return Ok(new Challenge2Model()
            {
                MaxDistance = distance
            });
        }
    }
}
