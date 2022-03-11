# ITSP Test

## Demonstration

[![Demo](https://github.com/hbtmrt/ItspTest/blob/main/ItspTest/itsp-demo.gif "Demo")](https://github.com/hbtmrt/ItspTest/blob/main/ItspTest/itsp-demo.gif "Demo")

## Projects
1. ItspTest.Api - A web API project
2. ItspTest.Core - A class library that holds the information sharable across projects.
3. ItspTest.Presentation -An angular project for the presentation purpose

## Endpoints
- Register 
POST /api/account/register

- Login
POST /api/account/authenticate

- Get all the collections
GET /api/collections

- Add a collection
POST /api/collections

- Get a collection
GET /api/collection/:id

- Search movies in a collection
GET /api/collections/:collectionId/movies?search=somesearchtext

- Get available movies to add for a collection
GET /api/collections/:id/availableMovies

- Add movie list to a collection
POST /api/collections/:id/movies/add-range

- Add a movie to a collection
POST /api/collecitons/:id/movies
**NOTE**: User can add movies only his collections.

- Delete a movie from a collection
DELETE /api/collections/:id/movies/:movieId
**NOTE**: User can delete movies only from his collections.

### Some highlights
- Added JWT token-based authentication.
- Added role-based authentication
- Added angular application for the presentation. (that's the most time-consuming task in this test)
- Dockerize the web API.
- Added Log4Net
- I have added custom exceptions to handle some specific scenarios.
- I have used Automapper to convert Entities to models and vice versa
- **Since I used angular, it took me a long time to develop a single screen. To be honest, this is a bad choice for this task. But long run, this is a good choice.**

### Assumptions
- A user can create one collection (this is not real-world, but I have made this assumption to take the test.)
- The logout feature was not implemented since it was not required.

### What did I miss in this test.
NOTE: I have missed some of the features with the time constraint.
- Dockerize angular application.
- Unit testings for web API endpoint.

### How to run
- Run the docker-compose file (since this create two docker containers; one for the web API; the other for SQL server) but make sure to give the port for the web API as 61005 (usually this has to be configurable, but I couldn't do it with the time constraint)
- run the angular application.
- We have to add some data to the database,
		Add some movies (later, this can be done by an administrator having admin role authorization)
		Add some collections and map them to the user (I wanted to do with this task, but with the time constraint, I had to skip this)
