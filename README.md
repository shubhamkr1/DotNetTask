## Intro
This Web App Project is made using 
1. .NET Core 6 MVC template
2. SQL Server Express for data storage

This project was created using EF Core Code First Approach. The profile picture and resume uploaded by users
are stored in local folder  in the project under wwwroot/UserFiles. There are some sample files present in this
folder.

User Registration, Login and Reset Password features are implemented using Microsoft Identity package and 
EF Core.



Dependencies 
1. .NET Core 6 SDK
2. SQL Server Express 2019 or higher
3. Entity Framework Core 6
4. Microsoft Identity for User Registration and Login
5. Bootstrap 5
6. jQuery 3.5.1
    
<!-- GETTING STARTED -->
## How to Run the Project

### Installation

1. Visual Studio 2022 latest version
2. SQL Server Management Studio 2019

### Steps to run

1. After installing necessary tools, download the repo, open the solutions with Visual Studio 2022
2. Adjust the connection string in appsettings.json as per your local SQL database setup. Make sure you
   can connect to yur local database instance.Create the empty database named AuthSystemDB.
3. Run the migration files under Migrations folder of the project to create equired database and tables on your
 local SQL instance using Update-Database command in Package Manager Console
4. Make sure all the Identity tables (starting with AspNet*) and userProfile table is created under the database
5. Run the application. Click on the Register link on top left to register a new User. Enter Email and Password.
   
![register_page](https://github.com/shubhamkr1/DotNetTask/assets/22971721/eb9187fa-4e3d-4660-8c62-ee1ac0c2b1ef)

6. Click on Login link at top right to Login. Enter Email ID and password. Click Login button
7. After logging in, you should see your email ID at top right, along with other options.
![logged_in_user](https://github.com/shubhamkr1/DotNetTask/assets/22971721/8cb8a86e-645a-4e44-8996-2653d6049649)

8. Click on View Profile option at top right to View logged in user profile. This will also show the list of names of files already
   uploaded in local folder.
   
   ![view_profile](https://github.com/shubhamkr1/DotNetTask/assets/22971721/3f1a3459-e467-4e60-b928-fa1990a6671d)
   
9. Click on Add Profile to add other user details like Name, Address, Phone, Email and Profile pics, Resume.
 
    ![add_profile](https://github.com/shubhamkr1/DotNetTask/assets/22971721/2dabdd1e-e98b-44d4-a4f1-612db3f8ff51)
   
10. Click on Access files option to visit page where you can download the uploaded files or delete those files.
    ![download_delete](https://github.com/shubhamkr1/DotNetTask/assets/22971721/f74c29e3-d73c-4deb-98c9-39198306e245)

11. Click on Logout option to logout.
<p align="right">(<a href="#readme-top">back to top</a>)</p>


