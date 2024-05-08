# **Marion KcdChallenge** 
I created a user and employee model, although im rethinking this now and to simply have a user model with a a flag for whether a user is a administrator or not.
My logic follows this path:
User can register in the system and get added to the database awaiting approval, this is done with ApiCall https://localhost:44450/api/Registration/user this creates a new user stored in the database with a flag on the user set to false. APi call for this feature is in RegistrationController. Improvements I can if I have time is saving this user somewhere else temporarily to in case of malicious attacks. User is only allowed to register on the system with a new email, duplicate emails are rejected. I can improve this by checking combination of names and email.


I created Employee that has simple admin role with a flag showing whether a employee can do certain things like approving new Users
Wrote multiple API calls to update a user, approve a user, approve/disable a list of users








Used a template (https://github.com/emonney/QuickApp.git) as framework but decided to create my own models, repositories and write my own controllers
Hopefully will find time to remove all the things i dont need tomorrow and maybe get a simple frontend working.

## Documentation
> * **Models**
>   * User
>   * Employee
> * **Repositories**
>   * UserRepository
>   * EmployeeRepository
>   * UnitOfRepository
> * **Controllers**
>   * AccountManagerController
>   * RegistrationController
> * **Tests**
  >   * RegistrationControllerTests

## What is not done yet

>   * More unit tests - figure out how to mock the database for tests
>   * More api calls to update a user - for example changing user details like name etc
>   * A simple front
>   * No authentication on any of the api calls
>   * Swagger
>   * Maybe add a Task to routinely remove inactive users after given amount of time

## How it works/ how to run
>   * Database (localdb)\MSSQLLocalDB
>   * Run QuickApp exe

