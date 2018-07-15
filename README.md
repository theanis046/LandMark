# LandMarks
# Introduction
This application helps user to save notes on map coordinates and view notes saved by other users.

#	Backend Application
Technology Stack: 
*	ASP .NET Core 2.0
*	localdb
*	Entity Framework Core
*	Visual Studio 2017
*	Moq
*	nUnit
Time Spent: 5 hours
.Net core is cross platform and can be deployed on all platforms like windows, linux etc. This layer contains user management, Repositories, DI and tests. 
*User Authentication : Used simple JWT authentication. For simplicity resource layer owns the authentication responsibilty. in actual scenario authentication layer should be a separate layer.
*Tests Layer : nUnit and moq is used for tests. inMemoryDB is used for this layer.

#	Frontend Application
Technology Stack:
*	Angular 5
*	Angular Google Map(AGM) package
*	Bootstrap
*	VScode
Time Spent: 8 hours
More time was spent on this layer. Implemented HttpInterceptors, RouteGuards, Authentication Service etc

#	How to Run?
*	Clone the Git repository.
*	Got to TigerSpikeLandMarks folder and open the TigerSpikeLandMarks.sln file in Visual Studio 2017.
*	Run following from the Package Manager Console to generate the localdb databse.
      * Update-Database
*	Start the project by pressing F5 for debugging Or Ctrl+ F5 without debugging. If this does not start the project, then go to the folder containing TigerSpikeLandMarks.sln. Go to TigerSpikeLandMarks folder. Now open Command line tool in this folder and run following commands one by one:
    *	dotnet build
    *	dotnet run
    
  this will launch the Api project.
*	Go to UI folder from the root directory. Open the folder in Visual Studio Code OR just open Command line tool
*	Open Terminal in VS Code and run following commands
    *	npm install
    *	ng serve
    
  same commands will be used if you are using Command line tool. This will launch the project at port 4200 on localhost.  
  
# Considerations
Backend application by default runs on port 53438. If due to any reason it is not running in this port than please change the port of UI application if environment.ts in environment folder. 