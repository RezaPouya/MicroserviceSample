using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserService.ReadonlyRepositories.Scripts.Users
{
    internal abstract class User_SqlScript
    {
        public const string GetById = "SELECT Id, Fname , Lname FROM dbo.Users WHERE Id = @userId";
    }
}
