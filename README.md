# **Marion KcdChallenge** 
I created a user and employee model, although im rethinking this now and to simply have a user model with a a flag for whether a user is a administrator or not.
My logic follows this path:
User can register in the system and get added to the database awaiting approval, this is done with ApiCall https://localhost:44450/api/Registration/user this creates a new user stored in the database with a flag on the user set to false. APi call for this feature is in RegistrationController. Improvements I can if I have time is saving this user somewhere else temporarily to in case of malicious attacks. User is only allowed to register on the system with a new email, duplicate emails are rejected. I can improve this by checking combination of names and email.


I created Employee that has simple admin role with a flag showing whether a employee can do certain things like approving new Users
Wrote multiple API calls to update a user, approve a user, approve/disable a list of users








Used a template (https://github.com/emonney/QuickApp.git) as framework but decided to create my own models, repositories and write my own controllers
Hopefully will find time to remove all the things i dont need tomorrow and maybe get a simple frontend working.

## Documentation for my addition to template
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
> * **Helper Functions**
  >   * PasswordService

## What is not done yet

>   * More unit tests (have done for one controller)
>   * A simple frontend
>   * No authentication on any of the api calls
>   * Swagger
>   * Maybe add a Task to routinely remove inactive users after given amount of time

## How it works/ how to run
>   * Database (localdb)\MSSQLLocalDB
>   * Run QuickApp exe
>   * Api calls done on postman no authentication required for now


## Calls on postman
>   * To register a user - https://localhost:44450/api/Registration/user body example 
>   * {
      "password":"password1234",
      "firstName":"name",
      "middleName":"middle",
      "lastName":"lastname",
      "email":"email@gmail.com",
      "phoneNumber":"TestSCIM00542",
      "country":"Uk",
      "company":"Lalandi",
      "miscellaneous":"Internet"
      }
> 
> 
>   * Get All users https://localhost:44450/api/Registration/users
>   * Update a user https://localhost:44450/api/Registration/user/update?id={userid} body as above
> * Admin api
> * for testing get employees from seeded database https://localhost:44450/api/AccountManager/user/
> * admin will approve user with https://localhost:44450/api/AccountManager/user/approve?id={employeeId}
> * admin to approve a list of users https://localhost:44450/api/AccountManager/users/approve?id={employeeId}
> * admin to deactivate a list of users  https://localhost:44450/api/AccountManager/users/deactivate?id={employeeId}

