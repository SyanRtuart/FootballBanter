using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Base.Infrastructure;
using Microsoft.EntityFrameworkCore;


namespace UserAccess.Infrastructure.Persistence
{
    public class UserAccessContextInitializer
    {
        private readonly UserAccessContext _context;

        public UserAccessContextInitializer(UserAccessContext context)
        {
            _context = context;
        }

        public static void Initialize(UserAccessContext context)
        {
            var initializer = new UserAccessContextInitializer(context);
            initializer.SeedEverything();
        }

        public async void SeedEverything()
        {
            if (!_context.Users.Any())
            {
                CreatePermissionsTable();
                CreateRolesToPermissionsTable();
                SeedTestUsers();
                SeedPermission();
                SeedRolesToPermissions();
                //SeedUserRoles();

                await _context.SaveChangesAsync();
            }
        }

        private void CreatePermissionsTable()
        {
            _context.Database.ExecuteSqlRaw(@"CREATE TABLE [user].[RolesToPermissions] (
                                            [RoleCode]       VARCHAR (50) NOT NULL,
                                            [PermissionCode] VARCHAR (50) NOT NULL,
                                            CONSTRAINT [PK_RolesToPermissions_RoleCode_PermissionCode] PRIMARY KEY CLUSTERED ([RoleCode] ASC, [PermissionCode] ASC)
                                            );"
            );
        }

        private void CreateRolesToPermissionsTable()
        {
            _context.Database.ExecuteSqlRaw(@"CREATE TABLE [user].[Permissions] (
                                            [Code]        VARCHAR (50)  NOT NULL,
                                            [Name]        VARCHAR (100) NOT NULL,
                                            [Description] VARCHAR (255) NULL,
                                            CONSTRAINT [PK_user_Permissions_Code] PRIMARY KEY CLUSTERED ([Code] ASC)
                                            );"
            );
        }
        private void SeedTestUsers()
        {
            _context.Database.ExecuteSqlRaw(@"INSERT INTO [user].UserRegistrations (Id, Login, Email, Password, FirstName, LastName, Name, StatusCode, RegisterDate, ConfirmedDate) VALUES 
                                            (
	                                            '2EBFECFC-ED13-43B8-B516-6AC89D51C410',
	                                            'testMember@mail.com',
	                                            'testMember@mail.com',
	                                            'ANO7TKjxh/Dom6LG0dyoQfJciLca+e1itHQ6BZMQYs+aMbKL9MjCv6bq4gy4+MRY0w==', -- testMemberPass
	                                            'John',
	                                            'Doe',
	                                            'John Doe',
	                                            'Confirmed',
	                                            GETDATE(),
	                                            GETDATE()
                                            )

                                            INSERT INTO [user].Users (Id, Login, Email, Password, IsActive, FirstName, LastName, Name) VALUES
                                            (
	                                            '2EBFECFC-ED13-43B8-B516-6AC89D51C410',
	                                            'testMember@mail.com',
	                                            'testMember@mail.com',
	                                            'ANO7TKjxh/Dom6LG0dyoQfJciLca+e1itHQ6BZMQYs+aMbKL9MjCv6bq4gy4+MRY0w==', -- testMemberPass
	                                            1,
	                                            'John',
	                                            'Doe',
	                                            'John Doe'
                                            )

                                            INSERT INTO [user].UserRoles VALUES
                                            ('Member', '2EBFECFC-ED13-43B8-B516-6AC89D51C410')

                                            -- Add Test Administrator
                                            INSERT INTO [user].UserRegistrations (Id, Login, Email, Password, FirstName, LastName, Name, StatusCode, RegisterDate, ConfirmedDate) VALUES 
                                            (
	                                            '4065630E-4A4C-4F01-9142-0BACF6B8C65D',
	                                            'testAdmin@mail.com',
	                                            'testAdmin@mail.com',
	                                            'AK0qplH5peUHwnCVuzW9zy0JGZTTG6/Ji88twX+nw9JdTUwqa3Wol1K4m5aCG9pE2A==', -- testAdminPass
	                                            'Jane',
	                                            'Doe',
	                                            'Jane Doe',
	                                            'Confirmed',
	                                            GETDATE(),
	                                            GETDATE()
                                            )

                                            INSERT INTO [user].Users (Id, Login, Email, Password, IsActive, FirstName, LastName, Name) VALUES
                                            (
	                                            '4065630E-4A4C-4F01-9142-0BACF6B8C65D',
	                                            'testAdmin@mail.com',
	                                            'testAdmin@mail.com',
	                                            'AK0qplH5peUHwnCVuzW9zy0JGZTTG6/Ji88twX+nw9JdTUwqa3Wol1K4m5aCG9pE2A==', -- testAdminPass
	                                            1,
	                                            'Jane',
	                                            'Doe',
	                                            'Jane Doe'
                                            )

                                            INSERT INTO [user].UserRoles VALUES
                                            ('Administrator', '4065630E-4A4C-4F01-9142-0BACF6B8C65D')


                                            "
            );
        }

        private void SeedPermission()
        {
            _context.Database.ExecuteSqlRaw(@"INSERT INTO [user].[Permissions] (Code, Name) VALUES
                                            ('AddBanter', 'AddBanter'),
                                            ('DeleteBanter', 'DeleteBanter')"
            );
        }

        private void SeedRolesToPermissions()
        {
            _context.Database.ExecuteSqlRaw(@"INSERT INTO [user].[RolesToPermissions] VALUES
                                            ('Member', 'AddBanter'),
                                            ('Administrator', 'DeleteBanter')"
            );
        }
    }
}
