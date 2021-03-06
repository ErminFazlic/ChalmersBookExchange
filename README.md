# ChalmersBookExchange
## Made for course DAT257/DIT257 Agile Software Project Management
## [Trello Board](https://trello.com/b/dV6g5Xvv/cello)
## Group Cello

### Members
- Arian Alikashani
- Negin Bakhtiarirad
- Ermin Fazlic
- Sven Kellgren
- Cynthia Kozma
- Petra Nisan

### Demonstration
For a demo of the application, watch Demo.mp4

### Description
A CRUD application for buying and selling used course books for Chalmer's students. The project was made with the ASP.NET framework and a Postgresql database.

#### Features
- Registering & Logging in
- Browsing posts made by users
- Searching through all posts
- A chat function where you can message another user
- Editing and deleting your posts
- Creating a post
- Adding a post to favorites

### Structure
The application uses a simple version of the ASP.NET MVC model, where views are the pages displayed, controllers handle actions and database interaction and the models are
the domain for the application.

#### General Flow of the Application
The general flow is:
> View -> HomeController -> OtherController -> HomeController -> View

For example, If one were to create a post by pressing the submit button on the Create Post page it would send an HTTPPost action to the HomeController which then delegates it to the appropriate controller, in this case the PostController, for doing the interaction with the database. Upon receiving a response the HomeController then redirects the user to an appropriate View. As seen in the image below:
![image](https://user-images.githubusercontent.com/78600091/137877414-40931a13-ba2b-4254-8e0e-df9b01aeec40.png)


#### Database Schema
The schema for the relational database can be described with the following image:

![image](https://user-images.githubusercontent.com/78600091/137909089-5ceb041a-c2f1-41d2-8957-49fa69672063.png)

#### Languages
The applications backend is made with C# while the frontend consists of a mixture of HTML, CSS and Javascript (With JQuery).

#### Dependencies
The project uses the following NuGet packages:
- EntityFramework
- Npgsql
- AspNetCore.Identity

### How to run
#### Alternative 1
1. Download publish.zip from release.
2. Make sure there is a Postgres server running on the machine.
3. It can then be deployed on a server or local IIS https://docs.microsoft.com/en-us/aspnet/web-forms/overview/deployment/visual-studio-web-deployment/deploying-to-iis

#### Alternative 2
1. Download Jetbrains Rider or Visual Studio.
2. Download source code from release.
3. Install the dependencies needed.
4. Make sure there is a Postgres server running on the machine.
5. Create a database in Postgres according to the schema, named "ChalmersBookExchange" with user "Admin" with password "123456789".
6. Run the project from your IDE.


