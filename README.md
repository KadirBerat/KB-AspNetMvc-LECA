# KB-AspNetMvc-LECA
 Logging, error catching and authentication solution for Asp Net Mvc projects. 
 (LECA: Logging - Error Catching - Authentication)

## General Information
 This nuget automatically catches the errors that occur in the AspNet MVC project to which it is added and adds it to the log file of the relevant day, then directs the user to the Index page in the Home controller. </br>

 When any action in any controller is accessed with the LogFilter defined as GlobalFilter in it, it adds this access to the log file of the relevant day. </br>
 
 Again, it ensures that only authorized persons can access the controller or action determined by the AuthFilter included in it, directs the unauthorized persons to the Index page in the Home controller and saves the records of all these operations in the log file of the relevant day. </br>
 > The descriptions of the codes in the project are available as comments line. For detailed information about the codes, please review the project. (https://github.com/KadirBerat/KB-AspNetMvc-LECA.git)

## Tutorial
> There is no need to take any action to keep logs related to error catching and access log, this process is carried out automatically by the system.

#### AuthFilter
> User types </br>
- User (Access Level 0)
- Admin (Access Level 1)
- Master Admin (Access Level 2)
- Tester (Access Level 3)
- Developer (Access Level 4) </br>

When defining the AuthFilter, it takes a UserTypes object to identify the user with the lowest access to the controller or action in which it is defined.</br>

If the filter is defined on the controller, when a request is sent to any action in that controller, authorization is checked.
```csharp
[AuthFilter(UserTypes.Admin)]
public class HomeController : Controller
{
    ...
}
```
If the filter is defined on the action, only when a request comes to that action, the authorization is checked.
```csharp
[AuthFilter(UserTypes.Admin)]
public ActionResult Index()
{
    ...
}
```
While defining authorization cookies for access control, it takes a model containing user data into the cookie. </br>
The definition of this model is as follows.
```csharp
CookieModel model = new CookieModel
{
    ID = 0,
    Username = "admin",
    UserType = UserTypes.Admin
};
```
The process of adding cookies is as follows.
```csharp
AuthenticationHelper.AddAuthCookie(model);
```
The process of deleting cookies is as follows.
```csharp
AuthenticationHelper.DeleteAuthCookie();
```
> The name of the cookie is authcookie.

