# ChalmersBookExchange
## Made for course DAT257/DIT257 Agile Software Projec Management
## [Trello Board](https://trello.com/b/dV6g5Xvv/cello)
## Group Cello

### Members
- Arian Alikashani
- Negin Bakhtiarirad
- Ermin Fazlic
- Sven Kellgren
- Cynthia Kozma
- Petra Nisan

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

For example, If one were to create a post by pressing the submit button on the Create Post page it would send an action to the HomeController which then delegates the to the appropriate controller, in this case the PostController, for doing the interaction with the database. Upon receiving a response the HomeController then redirects the user to an appropriate View.

#### Database Schema
The schema for the relational database can be described with the following two images:

![image](https://user-images.githubusercontent.com/78600091/137588519-dfed90e5-e9aa-4d4d-975d-edfaa116bf69.png)
![image](https://user-images.githubusercontent.com/78600091/137588541-a02e9335-09d1-47f9-a59c-675b3bf85f51.png)

#### Languages
The applications backend is made with C# while the frontend consists of a mixture of HTML, CSS and Javascript (With JQuery).

#### Dependencies
The project uses the following NuGet packages:
- EntityFrame
- Npgsql
- AspNetCore.Identity


