# TextEmAllChallenge

Coding challenge submission

The challenges can be exercised by running the main project locally and using the Swagger UI to test the two endpoints, one for each challenge.

Simply edit the appsettings.json and modify the connection string accordingly for both unit tests and the Swagger UI to work. The expectation for the embedded connection string is that the database is running locally, is named school, and that you can access the dbo schema using the local Windows identity.

The code provided is indicative of a TDD approach with substantial commenting. Some of the approach is non-standard to the way I'd approach a project since this is a challenge and there are very many unknowns and assumptions. Specifically, I put no energy into error handling and logging since that infrastructure already exists in most places and makes no sense to duplicate. Similarly, I tried to focus on the areas and algorithms that the challenge laid out and the job description has called for. Within that context, I tired to demonstrate the entity framework mapping of various relationships and polymorphic implementations. I would not mix and match methods in a real world scenario unless very specific requirements called for it, however, I thought that made sense here. Further, I did not over-engineer a unit of work pattern or anything similar because of the simplicity of the project. Normally, I'd separate concerns into different classes and potentially even different projects for ease of interoperability with future projects. It was my opinion in developing the code for this challenge that such a level of separation of concern would make the project all the more difficult to evaluate and your time is valuable. I believe I was able to communicate my ability to achieve the objective.

For the record, I had to get some things reinstalled on an old laptop which took a couple of hours. My total work time on this project was about six hours including that time. I know that is not blazing fast but it has been a year and a half since I really sat down and coded properly. My recall will improve dramatically with more time on task.

Finally, this is written and compiled using .NET 5.0 because Visual Studio 2019 does not support .NET 6.0. My more recent experience was with .NET Core so I opted to that rather than the framework version but I don't think that has any material impact on the demonstration of capability.

Please let me know if you have any questions or concerns or have any problems running the challenge code.

Thanks, Roger
