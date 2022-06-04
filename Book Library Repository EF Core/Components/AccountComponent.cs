using Book_Library_Repository_EF_Core.Contexts;
using Book_Library_Repository_EF_Core.Models.Account;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;

namespace Book_Library_Repository_EF_Core.Components
{
    public class AccountComponent
    {
        public int Login(string login, string password)
        {
            var inLogin = new SqlParameter
            {
                ParameterName = "Login",
                Value = login,
                DbType = System.Data.DbType.String,
                Direction = System.Data.ParameterDirection.Input
            };
            var inPassword = new SqlParameter
            {
                ParameterName = "Password",
                Value = password,
                DbType = System.Data.DbType.String,
                Direction = System.Data.ParameterDirection.Input
            };
            var outResult = new SqlParameter
            {
                ParameterName = "Result",
                DbType = System.Data.DbType.Int32,
                Direction = System.Data.ParameterDirection.Output
            };

            var sql = "exec LoginAccount @Login, @Password, @Result OUT";
            using (var dbContext = new BookLibraryContext())
            {
                _ = dbContext.Database.ExecuteSqlRaw(sql, inLogin, inPassword, outResult);
            }
            if (Int32.TryParse(outResult.Value.ToString(), out int accountId))
                if (accountId > 0)
                {
                    return accountId;
                }
            return 0;
        }

        public int Register(string login, string password, string firstName, string lastName, string email)
        {
            var inLogin = new SqlParameter
            {
                ParameterName = "Login",
                Value = login,
                DbType = System.Data.DbType.String,
                Direction = System.Data.ParameterDirection.Input
            };
            var inPassword = new SqlParameter
            {
                ParameterName = "Password",
                Value = password,
                DbType = System.Data.DbType.String,
                Direction = System.Data.ParameterDirection.Input
            };
            var inFirstName = new SqlParameter
            {
                ParameterName = "FirstName",
                Value = firstName,
                DbType = System.Data.DbType.String,
                Direction = System.Data.ParameterDirection.Input
            };
            var inLastName = new SqlParameter
            {
                ParameterName = "LastName",
                Value = lastName,
                DbType = System.Data.DbType.String,
                Direction = System.Data.ParameterDirection.Input
            };
            var inEmail = new SqlParameter
            {
                ParameterName = "Email",
                Value = email,
                DbType = System.Data.DbType.String,
                Direction = System.Data.ParameterDirection.Input
            };
            var outResult = new SqlParameter
            {
                ParameterName = "Result",
                DbType = System.Data.DbType.Int32,
                Direction = System.Data.ParameterDirection.Output
            };

            var sql = "exec RegisterAccount @Login, @Password, @FirstName, @LastName, @Email, @Result OUT";
            using (var dbContext = new BookLibraryContext())
            {
                _ = dbContext.Database.ExecuteSqlRaw(sql, inLogin, inPassword, inFirstName, inLastName, inEmail, outResult);
            }
            if (Int32.TryParse(outResult.Value.ToString(), out int accountId))
                return accountId;
            return 0;
        }

        public DisplayUserModel GetUser(int userId)
        {
            var inAccountId = new SqlParameter
            {
                ParameterName = "AccountId",
                Value = userId,
                DbType = System.Data.DbType.Int32,
                Direction = System.Data.ParameterDirection.Input
            };
            var outLogin = new SqlParameter
            {
                ParameterName = "Login",
                DbType = System.Data.DbType.String,
                Size = 32,
                Direction = System.Data.ParameterDirection.Output
            };
            var outFirstName = new SqlParameter
            {
                ParameterName = "FirstName",
                DbType = System.Data.DbType.String,
                Size = 32,
                Direction = System.Data.ParameterDirection.Output
            };
            var outLastName = new SqlParameter
            {
                ParameterName = "LastName",
                DbType = System.Data.DbType.String,
                Size = 32,
                Direction = System.Data.ParameterDirection.Output
            };
            var outEmail = new SqlParameter
            {
                ParameterName = "Email",
                DbType = System.Data.DbType.String,
                Size = 32,
                Direction = System.Data.ParameterDirection.Output
            };
            var sql = "exec GetUser @AccountId, @Login OUT, @FirstName OUT, @LastName OUT, @Email OUT";
            using (var dbContext = new BookLibraryContext())
            {
                _ = dbContext.Database.ExecuteSqlRaw(sql, inAccountId, outLogin, outFirstName, outLastName, outEmail);
            }
            if (!string.IsNullOrEmpty(outLogin.Value.ToString()))
            {
                return new DisplayUserModel
                {
                    Login = outLogin.Value.ToString(),
                    FirstName = outFirstName.Value.ToString(),
                    LastName = outLastName.Value.ToString(),
                    Email = outEmail.Value.ToString()
                };
            }
            return null;

        }

        public bool ChangeAccountPassword(int accountId, string accountPassword, string newAccountPassword)
        {
            var inAccountId = new SqlParameter
            {
                ParameterName = "AccountId",
                Value = accountId,
                DbType = System.Data.DbType.Int32,
                Direction = System.Data.ParameterDirection.Input
            };
            var inPassword = new SqlParameter
            {
                ParameterName = "Password",
                Value = accountPassword,
                DbType = System.Data.DbType.String,
                Direction = System.Data.ParameterDirection.Input
            };
            var inNewPassword = new SqlParameter
            {
                ParameterName = "NewPassword",
                Value = newAccountPassword,
                DbType = System.Data.DbType.String,
                Direction = System.Data.ParameterDirection.Input
            };
            var outResult = new SqlParameter
            {
                ParameterName = "Result",
                DbType = System.Data.DbType.Boolean,
                Direction = System.Data.ParameterDirection.Output
            };
            var sql = "exec ChangeAccountPassword @AccountId, @Password, @NewPassword, @Result OUT";
            using (var dbContext = new BookLibraryContext())
            {
                _ = dbContext.Database.ExecuteSqlRaw(sql, inAccountId, inPassword, inNewPassword, outResult);
            }
            Boolean.TryParse(outResult.Value.ToString(), out bool result);

            return result;
        }

        public bool DeleteAccount(int accountId, string accountPassword)
        {
            var inAccountId = new SqlParameter
            {
                ParameterName = "AccountId",
                Value = accountId,
                DbType = System.Data.DbType.Int32,
                Direction = System.Data.ParameterDirection.Input
            };
            var inPassword = new SqlParameter
            {
                ParameterName = "Password",
                Value = accountPassword,
                DbType = System.Data.DbType.String,
                Direction = System.Data.ParameterDirection.Input
            };
            var outResult = new SqlParameter
            {
                ParameterName = "Result",
                DbType = System.Data.DbType.Boolean,
                Direction = System.Data.ParameterDirection.Output
            };
            var sql = "exec DeleteAccount @AccountId, @Password, @Result OUT";
            using (var dbContext = new BookLibraryContext())
            {
                _ = dbContext.Database.ExecuteSqlRaw(sql, inAccountId, inPassword, outResult);
            }
            Boolean.TryParse(outResult.Value.ToString(), out bool result);

            return result;

        }
    }
}
