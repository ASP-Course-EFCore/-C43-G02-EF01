using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Data.Models
{
    ///POCO class => Plain Old C# (CLR) Object.
    ///Plain object not contain methods => just Properties represent the table Columns in DB.
    ///
    ///EF Core Support 4 Ways for mapping the code (DBContext Class , Domain Models) to Database Objects(Tables,Views,....)
    /// 1- By Convention (Default Behavior) => EF Core Make Mapping Decisions Based on code/Syntax you write - I Don't do any effort to tell EF How to mapping.
    /// 2- Data Annotations [Set of Attributes]
    /// 3- Fluent API
    /// 
    ///   If EF Core Found in Domain Class => 
    ///     - Public Numeric Property Named as "Id" or "ClassName+Id" => Will Be Mapped As Primary Key column with identity [1,1] on the column.
    ///     - Reference Types "string" property [Not Nullable] => Mapped as column of type "nVarchar(max)" with constraint not allow null[Required].
    ///     - Nullable Reference Types "string?" property => Mapped as column of type "nVarchar(max)" with constraint allow null [optional].
    ///     - Non-Nullable ValueType "double" property => Mapped as column of type "float" with constraint not allow null [Required]
    ///     - Nullable ValueType "int?" property => Mapped as column of type "int" with constraint allow null [optional]
    ///
    ///
    ///So if you need to modify this default behavior of mapping, use another way of mapping - not use the Convention Way.
    ///In convention way, we don't write extra code to tell EF Core how to map, we just write the properties.

    class Employee
    {
        public int Id { get; set; } // Public Numeric Property Named as "Id" or "ClassName+Id" => Will Be Mapped As Primary Key column with identity [1,1] on the column.
        public required string FName { get; set; }// Reference Types "string" property [Not Nullable] => Mapped as column of type "nVarchar(max)" with constraint not allow null[Required].
        public string? LName { get; set; }// Nullable Reference Types "string?" property => Mapped as column of type "nVarchar(max)" with constraint allow null [optional].
        public double Salary { get; set; }// Non-Nullable ValueType "double" property => Mapped as column of type "float" with constraint not allow null [Required]
        public int? Age { get; set; }// Nullable ValueType "int?" property => Mapped as column of type "int" with constraint allow null [optional]
    }
}
