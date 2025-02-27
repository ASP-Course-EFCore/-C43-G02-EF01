using Demo.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Collections.Generic;
using System.Text;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Demo
{
    internal class Program
    {
        static void Main(string[] args)
        {

            #region Part 01 What is EF Core

            //Done In NoteBook.

            #endregion

            #region Part 02 EF Core Vs Dapper Vs ADO.NET

            //Done In NoteBook.

            #endregion

            #region Part 03 What is DbContext Class
            ///CodeFirst Approach
            ///1st Feature in EF Core => Automatic Schema Migration [Mapping]. 
            ///We always first make folder called Data Contain => 
            /// 1- DBContext Class => Name of it must end with "DBContext"
            /// 2- POCO Classes || Domain Models|| Entities || Classes Represent structure of Tables,Views of Database
            ///     Domain Models => Means The Models Specific to business domain
            ///     Represent Models/Classes That will be Tables in the database of the busineess
            ///     Like SuperMarket Business Has DomainModels Like => [Customers - Products - Sales] Tables
            ///     So, objects that we retrieve it from DB called business objects
            ///
            ///     POCOClasses => each POCOClass represent table in DB - and Only Contain Properties, each property represent column in database 
            ///
            /// 3- Migrations 
            ///
            ///We need this "DBContext" class to have set of properties & methods
            ///but we don't write it, we inherit it from base class "DBContext"
            ///it will be the base class of any "DBContext" we will create.
            ///This base Class "DBContext" is built in class in "EF Core", so we need first to install "EF Core" package and use namespace of it => "using Microsoft.EntityFrameworkCore;"
            ///There are 2 ways to install it :
            /// 1- By UI of VS => Throw "npm" of .NET which is "NuGet.org"
            /// 2- Throw Package Manager Console => Install-Package Microsoft.EntityFrameworkCore.SqlServer [This will install the latest version of the package]
            ///                                     Install-Package Microsoft.EntityFrameworkCore.SqlServer -v "8.0.15" [To install specific version].
            ///Note => Version of "EF Core" package must be same like version of ".NET Core" 
            ///After Install, You will found it in "Dependencies/Packages/Microsoft.EntityFrameworkCore.SqlServer (9.0.2)"
            ///
            ///The Important method that class "CompanyDBContext" inherit it from class "DBContext" is => "OnConfiguring" method.
            ///Throw it we can specify the ConnectionString 
            ///
            ///When Connect on DB throw App:
            ///- We make object from class "CompanyDBContext" that has empty parameterless constructor by default.
            ///- We use try-finally because connection with DB is unmanaged by CLR, so we need to manage this resource using try-finally
            ///  to release this resourse after finishing.
            ///
            ///CompanyDBContext context01 = new CompanyDBContext();
            ///try
            ///{
            ///    //Code
            ///}
            ///finally
            ///{
            ///    //Release The Resourse [Connection].
            ///    context01.Dispose();
            ///}
            ///
            ///Or Use using block => using(){ } => it's syntax suger for try-finally.
            ///
            ///using (CompanyDBContext context02 = new CompanyDBContext())//It automatically release this object after finish.
            ///{
            ///    //Code.
            ///}
            ///
            ///Or This another syntax sugar for using => using without curly brackets
            ///
            ///using CompanyDBContext context03 = new CompanyDBContext();
            ///Code.[الكود دا مش مقيد ب اسكوب معين]
            ///

            //using CompanyDBContext context = new CompanyDBContext();

            ///We can Create object from class "CompanyDBContext" using Dependency Injection Approach when i need this object in many place in code in same request because if i make throw parameterless constructor, every need to it i make different and new object.
            ///this when i need this object like in 10 different places in program,
            ///i will not every time i need this object, make new object from "CompanyDBContext" using parameterless constructor.
            ///i will let CLR to create the object in run time when i need it, and every time i need object from class "CompanyDBContext"
            ///i ask CLR to return it to me, but return the first object CLR create. 
            ///So I have 10 place in program/App depend on object from class "CompanyDBContext" => Dependency.
            ///And Run Time provide those objects to me throw injection => Injection.


            //1st Step Summary => Connect App to DB
            //- Create object from class that responsible for dealing with DB "CompanyDBContext" that inherit from base class "DBContext"
            //- When make object from class "CompanyDBContext" using parameterless constructor of class "CompanyDBContext"
            //  we found that the parameterless constructor of child class "CompanyDBContext" chain on parameterless constructor of base class "DBContext"
            //- And Found that the parameterless constructor of base class "DBContext" also make chain on Constructor called "DBContext(DbContextOption options)"
            //  that take parameter [object of type class DbContextOption]
            //-And to make object from this class "DbContextOption", we must make override on method "DBContext.OnConfiguring(DBContextOptionsBuilder optionsBuilder)"
            // In our child class "CompanyDBContext" to specify the "optionBuilder" which is the "connectionString" by calling the method that represent the db provider throw optionsBuilder object.
            // optionsBuilder.UseSqlServer("Server = .; Database = Company; Trusted_Connection = true");

            #endregion

            #region Part 04 Mapping Ways - 1. By Convention
            ///Now We need to make the Domain Models => Classes Represent Structure of DB Entities-Tables
            ///So make new folder inside "Data" folder called "Models" To Add Domain Models Classes inside it.
            ///And for each domain model, there is property of type "DBSet<DomainModel> DomainModels" inside the class "CompanyDBContext" That responsible for dealing with DB.
            ///
            ///EF Core Support 4 Ways for mapping the code (DBContext Class , Domain Models) to Database Objects(Tables,Views,....)
            /// 1- By Convention (Default Behavior) => EF Core Make Mapping Decisions Based on code/Syntax you write - I Don't do any effort to tell EF How to mapping.
            ///
            ///   If EF Core Found in Domain Class => 
            ///     - Public Numeric Property Named as "Id" or "ClassName+Id" => Will Be Mapped As Primary Key column with identity [1,1] on the column.
            ///     - Reference Types "string" property [Not Nullable] => Mapped as column of type "nVarchar(max)" with constraint not allow null[Required].
            ///     - Nullable Reference Types "string?" property => Mapped as column of type "nVarchar(max)" with constraint allow null [optional].
            ///     - Non-Nullable ValueType "double" property => Mapped as column of type "float" with constraint not allow null [Required]
            ///     - Nullable ValueType "int?" property => Mapped as column of type "int" with constraint allow null [optional]
            ///


            //Next step is => using EF Core To Generate DB Objects [Tables - Views - ... ] throw My Code
            //This Will Done Using "Migration".
            #endregion

            #region Part 05 What is Migration

            ///Migration Is the way that let us Apply changes when work CodeFirst to the DataBase.
            ///We first Divide our project into modules and work on project module by module
            ///cause we work codeFirst, we make class diagram, every module represent class to put in it tables that i need it in this module.
            ///Like Working on E-Commerce that have some modules Like [Payment Module].
            ///I Work on this module and make Domain models and relations and so on
            ///Then Apply changes done on this module to DataBase throw migration.
            ///
            ///Migration Also Represent The state between [Code - DB].
            ///First Migration always called => "InitialCreate".
            ///That Will Apply the structure Of project to DB.
            ///
            ///To create Migration there are 2 ways =>
            /// 1- By Wizard => throw install "EFCore Power Tools" Extension Tool.
            /// 2- By Command => throw Package Manager Console [But Install Package First].
            ///
            ///To Apply Changes Regardless You Work "DBFirst" or "CodeFirst"
            ///You need to install the package "Microsoft.EntityFrameWorkCore.Tools"
            ///To Let You Use Commands Of Migrations Like => "Add-Migration - Drop-Migration - Scaffold-DbContext".
            ///
            ///If You Work CodeFirst Approach "With Command" => 
            /// [Add-Migration "MigrationName"]
            ///Note => If You Have More than one "DBContext" class, mean you will have more than one DataBase
            ///And you need to Add-Migration, You need to specify the context/Db to which you need to add the migration.
            /// [Add-Migration "InitialCreate" -Context "CompanyDbContext"] or [Add-Migration "InitialCreate" -C "CompanyDbContext"]
            ///
            ///If You Work DBFirst Approach "With Command" => 
            ///You need to generate "DBContext" Class And "Domain Models" for database and tables you create
            ///So You Write This Command: [Scaffold-DbContext "ConnectionString"]
            ///
            ///After Make The First Migration =>
            ///The Default is Folder "Migration" Created in your project 
            ///If You need this folder to be inside specific folder like folder "Data", write this command => 
            /// [Add-Migration "InitialCreate" -C "CompanyDbContext" -OutputDir "Data/Migrations"]
            /// [Add-Migration "InitialCreate" -C "CompanyDbContext" -Output "Data/Migrations"]
            /// [Add-Migration "InitialCreate" -C "CompanyDbContext" -O "Data/Migrations"]
            ///EFCore Make Class Named "DBContextSnapShot" That Contain Copy/ScreenShot of LatestCode when make Migration - It represents the latest state of your database model.
            ///And If You make another migration, EFCore put Only Changes done on the code in the this second migration
            ///By compare code of second migration with code in the snapShot
            ///And then Update SnapShot Class With Code of latest Migration.
            ///After this, if you make any new migration, the file will be in Data/Migrations.
            ///
            ///After Make Any Migration =>
            ///EfCore make class Named With The "TimeStamp+MigrationName" like "20250226121108_InitialCreate" that contains 2 Functions => 
            /// Up()   => Apply/Add Changes on the code To DB
            /// Down() => Drop/RollBack Changes
            ///EFCore Add TimeStamp to migrationFile Name To make it Unique.
            ///
            ///In the Up() , it no create the dataBase, it only create the DataBase objects like tables - it only contain changes that i make in code and this code not in snapshot - if changed is already in snapshot, Up() method body will be empty and down() body will empty also
            ///But To Execute The Code Inside The Up() which contain changes that i need to Applying to DB
            ///You need first to write command [Update-DataBase].
            ///And this Command Apply All pending migrations => mean execute the method Up() of all pending migrations.
            ///It know the this migration is applied or not throw table in Database called "EF_Migration_History"
            ///That contain Name of Migration Files That i Applied it, and the Name of the Migration File is the primary key column of the table
            ///So this explain why EFCore Name any new migration file with the TimeStamp + MigrationName.
            ///So if not found this file in this table in DB, the apply this migration.
            ///
            ///Up() not contain creating DataBase
            ///So When Execute The Command => [Update-DataBase]
            ///EFCore Execute Method Called "DataBaseEnsureCreated()" that create the DB if not exist.
            ///
            ///If you need to drop the dataBase you need to execute the command => 
            /// [Drop-Database]
            ///That's because if you roll back all migrations [Execute down() of first migration by execute first migration] by write command [Update-Database "20250226121108_InitialCreate"]
            ///The database is still exist,because now the state of DB is like state of first migration, because down() only drop dataBase objects not DB itself. 
            ///
            ///
            /// [Remove-Migration]
            ///Remove only one migration If This migration Not Applied in DB
            ///The EFCore Will Delete The File  Of This Migration
            ///And Revert The SnapShot File Class to update the code in it to the code of previous migration.
            ///In case this is the First migration, The SnapShot file will deleted also because it only has this migration code that i need to remove.
            ///
            ///But When Try To Connect on Sql Server service to see if this migration Applied on DB or not
            ///Error occurred =>
            ///A connection was successfully established with the server,
            ///but then an error occurred during the login process.
            ///(provider: SSL Provider, error: 0 - The certificate chain was issued by an authority that is not trusted.)
            ///This error because The Request between VS code and Sql Server service is pass throw https Network protocol
            ///That use "SSL" Layer for encryption to encrypt the request body
            ///The Request that sent from "VS" to "sql server service" is pass throw https protocol mean that it use SSL certificate but we don't buy SSL certificate to use it
            ///don't worry, the "VS" has it's own SSL Certificate but "sql server service" not trust this certificate.
            ///mean that the server "VS" must has valid certificate and client "sql server service" Trust it
            ///But Now "VS" has certificate but "Sql server service" not trust it.
            ///
            ///So You Have 2 solutions:
            /// 1- Make the protocol to be http not https by make  "Encrypt = False" [Default is true] => optionsBuilder.UseSqlServer("Server = .; Database = Company; Trusted_Connection = true; Encrypt = False");
            /// 2- Make client "sql server service" trust the SSL certificate of VS by make "TrustServerCertificate = true" => optionsBuilder.UseSqlServer("Server = .; Database = Company; Trusted_Connection = true; Encrypt = True; TrustServerCertificate = true");
            ///
            ///Note => If You Buy SSL Certificate And Put Credential of it in the ConnectionString => You don't need to add this attribute "TrustServerCertificate = true"
            ///Because You buy certificate Already and don't use the server "VS" certificate which is not trusted from client "sql server service".
            ///
            /// [Remove-Migration] 
            ///Only Remove one migration if not applied to DB
            ///So, if you have like 3 migrations and you need to remove the 3 migrations files
            ///You need to remove migration by migration. 
            ///
            /// [Update-Database] -> This Command
            ///Make object from the class DBContext which i created "CompanyDBContext" => using CompanyDBContext context = new CompanyDBContext();
            ///And invoke the method "Migrate()" throw "Database" "property=> context.Database.Migrate();"
            ///This "Migrate()" method => Applies any pending migrations for the context to the database.And will create the database if it doesn't already exist.
            ///Throw method "EnsureCreated()".
            ///
            ///Or you can write this code and run the program:
            /// CompanyDBContext context = new CompanyDBContext();
            /// context.Database.Migrate();
            ///
            ///If there are many created DBContext, you need to specify the context name to tell it to create object from this context.
            /// Update-Database -Context "EmployeeDBContext"
            ///Or writeCode & run =>
            /// EmployeeDBContext context = new EmployeeDBContext();
            /// context.DataBase.Migrate();
            ///
            ///We writeCode of migration in the main function only in one case:
            ///- When we finish our project like webApp and publish it and deploy it on production server
            ///- But production server not have "VS Code" to write command "Update-Database"
            ///- To Apply all pending migrations. 
            ///- So we write this code in the main function, so with the first run of project
            ///- the database will created and migrations applied
            ///
            ///Now We Create The Database Throw Migrations Throw ORM EFCore With CodeFirst Approach.
            ///So We Can Query The data into database now throw LinQ operators against remote sequence "Employees"
            ///that is property of type DbSet<Employee> inside the "CompanyDBContext" class, so we need to make object from this class first to access it's properties. 
            ///
            ///using CompanyDBContext context = new CompanyDBContext();
            ///
            ///var Employee = context.Employees.Where(e => e.FName == "Ahmed").ToList().FirstOrDefault();
            ///Console.WriteLine($"{Employee?.FName}, {Employee?.LName}, {Employee?.Salary}, {Employee?.Age}");//Ahmed, Khaled, 10000, 22 => This Data Added Manually to test only.
            ///
            ///Note => If You Make Any Change in code Like (Rename Column - Add Column - Add Table - ...)
            ///- You Must Make migration for this change and update the database to reflect changes on code to database
            ///- because if you like renamed column in code and don't update the database and try to query the database and return result based on this column
            ///- you will found error "Invalid Column name" in database.
            ///
            ///Note => The Snapshot File Always Updated With The Last Migration Regardless this migration applied or not.
            ///With any new Added migration => there is comparing between the migration code and the snapshot file code
            ///If there is change => update the snapshot with the new changes
            ///
            ///Now, I Rename The Column/property "FNAme" in class/table "Employee" to "FirstName"
            ///So i must Migrate This Change And Apply it
            /// [Add-Migration "RenameColumnFNameInEmployeeTable"] -> Create New File "20250226152551_RenameColumnFNameInEmployeeTable.cs"
            ///And Compare changes between this file and The DBContext SnapShot File "CompanyDBContextModelSnapshot" to modify it to last version if there is change.
            /// [Update-Database] -> Create object from class "CompanyDBContext" and invoke method "Migrate()" throw property "Database" throw the object
            ///And Call Method "EnsureCreated()" throw "Migrate()" to check if the database exists - if not create it.
            ///And Then Apply The Changes in the Method "Up()" in the migrate File "20250226152551_RenameColumnFNameInEmployeeTable.cs"
            ///Which is Renaming the column FName -> FirstName.
            ///
            ///What if i need to return again to the last Name "FName" after applying the changes on the database?
            ///- You can't do [Remove-Migration] Direct Because you Apply the changes Already.
            ///Error => The migration '20250226152551_RenameColumnFNameInEmployeeTable' has already been applied to the database.
            ///         Revert it and try again. If the migration has been applied to other databases,
            ///         consider reverting its changes using a new migration instead.
            ///
            ///The only way to return to the last change "FName" is to update database to the previous Migration before Migration of Renaming the column.
            /// [Update-Database -Migration "InitialCreate"]
            ///So now You Revert the migration "20250226152551_RenameColumnFNameInEmployeeTable"
            ///Mean that it's pending need to be Applied
            ///So now the Migration file still exists not deleted
            ///And still the DbContext SnapShot File updated with code of this migration "20250226152551_RenameColumnFNameInEmployeeTable"
            ///So, Now We need to Remove this migration [Remove-Migration]
            ///So now the file "20250226152551_RenameColumnFNameInEmployeeTable" will be removed and DbContext snapshot file will be updated also with the latest migration which is "InitialCreate".
            ///*Note* => it will not change the class Employee to rename the column "FirstName" again to "FName"
            ///So you must do it by yourSelf.
            ///
            ///*Note* => The Only case to make RollBack for migration - update-Database to Specific migration
            ///- If The current  migration is the last migration and you don't need changed done in it and need to execute "Down()" 
            ///  Method of this migration. 
            ///
            ///If you have 3 migrations (1 - 2 - 3)
            ///And you need to rollback migration 2 to update database to migration 1
            ///Make new Migration 4 and let the "Up()" method of it contain "Down()" of migration 2.
            ///Because if i update data base to migration 1 direct, you will loose migration 3 and 2.
            ///
            ///If the migration is applied, and you need to remove it
            ///You can use command [Remove-Migration -Force]
            ///This command Revert current Migration and update DB to the previous migration and Remove current migration.
            ///
            ///If the current migration is the first migration "InitialCreate"
            ///And you Applied it to DB, But You need to remove it using "Remove-Migration -Force"
            /// 1- The Migration "InitialCreate" will be reverting mean that it's pending now and DB updated with the previous Migration
            ///  And there is no previous migrations so DB now empty and MigrationHistory column in DB is empty.
            /// 2- The migration "InitialCreate" will Removed
            /// 3- The snapShot Model will updated tho the current migration, and there is no migrations
            ///    so it removed also.
            ///
            ///"Note" => The DB still exist because The "Up() & Down()" of migrations filed not have dropping DB.
            ///So => [Drop-Database] is command for dropping database.
            ///
            ///
            /// if i have 3 migrations
            /// 1 - initialCreate
            /// 2 - AddProductTable
            /// 3 - AddEmployeeTable
            /// 
            /// and make Update - Database "initialCreate"
            /// what happen in this case, the down() of each migration will executed ?
            ///
            ///Yes! When you run: Update-Database InitialCreate
            ///What Happens?
            ///1- EF Core identifies that the current migration is InitialCreate, meaning it needs to rollback all migrations that came after it.
            ///2- The Down() method of each migration is executed in reverse order:
            ///   Down() of AddEmployeeTable runs → Removes Employee table.
            ///   Down() of AddProductTable runs → Removes Product table.
            ///3- The __EFMigrationsHistory table is updated, removing AddEmployeeTable and AddProductTable.
            ///4- The database schema reverts back to the state of InitialCreate.
            ///
            ///Important Notes:
            ///- Data Loss: If the Down() method drops a column or table, all data in those tables will be lost permanently.
            ///   Model Snapshot Desync: Since ModelSnapshot.cs is not updated, it will still contain the latest model state including AddProductTable and AddEmployeeTable.
            ///  - If you now run Add - Migration Fix, EF Core might generate incorrect changes because the snapshot is out of sync.
            ///  - To fix this, you should manually remove migrations using Remove-Migration before rolling back.


            #endregion

        }
    }
}
