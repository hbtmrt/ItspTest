# ITSP Test

## Demonstration

[![Demo](https://github.com/hbtmrt/ItspTest/blob/main/ItspTest/itsp-demo.gif "Demo")](https://github.com/hbtmrt/ItspTest/blob/main/ItspTest/itsp-demo.gif "Demo")

## Projects
1. ItspTest.Api - A web api project
2. ItspTest.Core - A class library which holds the information which is sharable across projects.
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
- Added JWT token based authentication.
- Added role based authentication
- Added angular application for the presentation. (that's the most time consuming task in this test)
- Dockerize the web api.

### Assumptions
- A user can create one collection (this is not a real world, but to make the test simply I have made this assumption.)
- The logout feature was not implemented since it was not required.


### What did I miss in this test.
NOTE: I have missed some of the features with the time constraint.
- Dockerize angular application.
- Unit testings for web api endpoint.
