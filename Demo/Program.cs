using Demo.Data;

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

        }
    }
}
