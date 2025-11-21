using _8_prac_JWT.DatabaseContext;
using _8_prac_JWT.Interfaces;
using _8_prac_JWT.Models;
using _8_prac_JWT.Requests;
using _8_prac_JWT.UniversalMethod;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace _8_prac_JWT.Service
{
    public class UsersService : IUserInterfaces
    {
        private readonly ContextDb _context;
        private readonly JwtGenerator _jwtGenerator;
        private readonly IHttpContextAccessor _httpContext;

        public UsersService(ContextDb context, JwtGenerator jwtGenerator, IHttpContextAccessor httpContext)
        {
            _context = context;
            _jwtGenerator = jwtGenerator;
            _httpContext = httpContext; // для редактирование самого себя профиля в покупателе
        }

        // создание покупателя-пользователя
        public async Task<IActionResult> CreateNewUserAsync(CreateNewUser createNewUser)
        {
            if (string.IsNullOrEmpty(createNewUser.User_fullname))
            {
                return new BadRequestObjectResult(new
                {
                    status = false,
                    message = "Имя и фамилия  не могут быть пустым!"
                });
            }

            if (string.IsNullOrEmpty(createNewUser.Email))
            {
                return new BadRequestObjectResult(new
                {
                    status = false,
                    message = "Емайл не может быть пустым"
                });
            }

            if (string.IsNullOrEmpty(createNewUser.Address))
            {
                return new BadRequestObjectResult(new
                {
                    status = false,
                    message = "Адрес не может быть пустым"
                });
            }

            if (string.IsNullOrEmpty(createNewUser.PhoneNumber))
            {
                return new BadRequestObjectResult(new
                {
                    status = false,
                    message = "Номер телефона не может быть пустым"
                });
            }

            if (string.IsNullOrEmpty(createNewUser.Login_N))
            {
                return new BadRequestObjectResult(new
                {
                    status = false,
                    message = "Логин не может быть пустым"
                });
            }

            if (string.IsNullOrEmpty(createNewUser.Password_N))
            {
                return new BadRequestObjectResult(new
                {
                    status = false,
                    message = "Пароль не может быть пустым"
                });
            }

            var check_email = await _context.Users.FirstOrDefaultAsync(e => e.Email.ToLower() == createNewUser.Email.ToLower());

            if (check_email != null)
            {
                return new BadRequestObjectResult(new
                {
                    message = "Пользователь с таким email уже существует",
                    status = false
                });
            }

            var check_Login = await _context.Logins.FirstOrDefaultAsync(e => e.Login_name.ToLower() == createNewUser.Login_N.ToLower());

            if (check_Login != null)
            {
                return new BadRequestObjectResult(new
                {
                    message = "Пользователь с таким Login уже существует",
                    status = false
                });
            }

            var check_phone_n = await _context.Users.FirstOrDefaultAsync(e => e.PhoneNumber.ToLower() == createNewUser.PhoneNumber.ToLower());

            if (check_phone_n != null)
            {
                return new BadRequestObjectResult(new
                {
                    message = "Пользователь с таким телефонным номером уже существует",
                    status = false
                });
            }

            var login = new Login()
            {
                User = new User()
                {
                    User_fullname = createNewUser.User_fullname,
                    Email = createNewUser.Email,
                    Address = createNewUser.Address,
                    PhoneNumber = createNewUser.PhoneNumber,
                    CreatedAt = DateOnly.FromDateTime(DateTime.Now),
                    UpdatedAt = DateOnly.FromDateTime(DateTime.Now),
                    Role_id = 3
                },
                Login_name = createNewUser.Login_N,
                Password = createNewUser.Password_N,
            };

            await _context.AddAsync(login);
            await _context.SaveChangesAsync();



            var logAction = new LogUserAction()
            {
                Created_at = DateTime.Now,
                User_id = login.User.User_id,
                Action_id = 1 // регистрация в бд
            };

            await _context.AddAsync(logAction);
            await _context.SaveChangesAsync();

            return new OkObjectResult(new
            {
                Status = true,
                message = "Успешно создан пользователь"
            });
        }

        // создание нового работника
        public async Task<IActionResult> CreateNewEmployeeAsync(CreateNewEmployee createNewEmployee)
        {
            if (string.IsNullOrEmpty(createNewEmployee.User_fullname))
            {
                return new BadRequestObjectResult(new
                {
                    status = false,
                    message = "Имя и фамилия  не могут быть пустым!"
                });
            }

            if (string.IsNullOrEmpty(createNewEmployee.Email))
            {
                return new BadRequestObjectResult(new
                {
                    status = false,
                    message = "Емайл не может быть пустым"
                });
            }

            if (string.IsNullOrEmpty(createNewEmployee.Address))
            {
                return new BadRequestObjectResult(new
                {
                    status = false,
                    message = "Адрес не может быть пустым"
                });
            }

            if (string.IsNullOrEmpty(createNewEmployee.PhoneNumber))
            {
                return new BadRequestObjectResult(new
                {
                    status = false,
                    message = "Номер телефона не может быть пустым"
                });
            }

            if (string.IsNullOrEmpty(createNewEmployee.Login_N))
            {
                return new BadRequestObjectResult(new
                {
                    status = false,
                    message = "Логин не может быть пустым"
                });
            }

            if (string.IsNullOrEmpty(createNewEmployee.Password_N))
            {
                return new BadRequestObjectResult(new
                {
                    status = false,
                    message = "Пароль не может быть пустым"
                });
            }

            var check_email = await _context.Users.FirstOrDefaultAsync(e => e.Email.ToLower() == createNewEmployee.Email.ToLower());

            if (check_email != null)
            {
                return new BadRequestObjectResult(new
                {
                    message = "Пользователь с таким email уже существует",
                    status = false
                });
            }

            var check_Login = await _context.Logins.FirstOrDefaultAsync(e => e.Login_name.ToLower() == createNewEmployee.Login_N.ToLower());

            if (check_Login != null)
            {
                return new BadRequestObjectResult(new
                {
                    message = "Пользователь с таким Login уже существует",
                    status = false
                });
            }

            var check_phone_n = await _context.Users.FirstOrDefaultAsync(e => e.PhoneNumber.ToLower() == createNewEmployee.PhoneNumber.ToLower());

            if (check_phone_n != null)
            {
                return new BadRequestObjectResult(new
                {
                    message = "Пользователь с таким телефонным номером уже существует",
                    status = false
                });
            }

            var login = new Login()
            {
                User = new User()
                {
                    User_fullname = createNewEmployee.User_fullname,
                    Email = createNewEmployee.Email,
                    Address = createNewEmployee.Address,
                    PhoneNumber = createNewEmployee.PhoneNumber,
                    CreatedAt = DateOnly.FromDateTime(DateTime.Now),
                    UpdatedAt = DateOnly.FromDateTime(DateTime.Now),
                    Role_id = 2
                },
                Login_name = createNewEmployee.Login_N,
                Password = createNewEmployee.Password_N,
            };

            await _context.AddAsync(login);
            await _context.SaveChangesAsync();

            var logAction = new LogUserAction()
            {
                Created_at = DateTime.Now,
                User_id = login.User.User_id,
                Action_id = 1 // регистрация в бд
            };

            await _context.AddAsync(logAction);
            await _context.SaveChangesAsync();


            return new OkObjectResult(new
            {
                Status = true,
                message = "Успешно создан менеджер"
            });
        }

        // авторизация пользователя  
        public async Task<IActionResult> AuthUserAsync(AuthUserRequest authUserRequest)
        {
            var user = await _context.Logins.Include(u => u.User)
                .FirstOrDefaultAsync(l => l.Login_name == authUserRequest.Login_name && l.Password == authUserRequest.Password);

            if (user == null)
            {
                return new NotFoundObjectResult(new
                {
                    status = false,
                    message = "Пользователь не найден"
                });
            }

            string token = _jwtGenerator.Generatetoken(new LoginPassword()
            {
                Id_user = user.User.User_id,
                Role_id = user.User.Role_id,
            });

            _context.Sessions.Add(new Session()
            {
                Token = token,
                User_id = user.User.User_id,
            });

            _context.LogUsers.Add(new LogUserAction()
            {
                Created_at = DateTime.Now,
                User_id = user.User.User_id,
                Action_id = 2, //авторизация в бд
            });

            await _context.SaveChangesAsync();

            return new OkObjectResult(new
            {
                status = true,
                token
            });
        }

        // удаление покупателя
        public async Task<IActionResult> DeleteUserAsync(DeletedIDUserRequest deletedID)
        {
            if (deletedID.ID_Deleted_User == 0)
            {
                return new BadRequestObjectResult(new
                {
                    status = false,
                    message = "Нет Ид 0"
                });
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Role_id == 3 && u.User_id == deletedID.ID_Deleted_User);

            if (user == null)
            {
                return new NotFoundObjectResult(new
                {
                    status = false,
                    message = $"Нет такого покупателя с таким ИД: {deletedID.ID_Deleted_User}"
                });
            }

            var login = await _context.Logins.FirstOrDefaultAsync(l => l.User_id == deletedID.ID_Deleted_User);



            _context.Users.Remove(user);
            _context.Logins.Remove(login);
            await _context.SaveChangesAsync();

            return new OkObjectResult(new
            {
                status = true
            });
        }

        // удаление менеджера
        public async Task<IActionResult> DeleteEmployeeAsync(DeletedIDEmployeeRequest deletedIDRequest)
        {
            if (deletedIDRequest.ID_Deleted_Empl == 0)
            {
                return new BadRequestObjectResult(new
                {
                    status = false,
                    message = "Проблемы с Id"
                });
            }

            var employee = await _context.Users.FirstOrDefaultAsync(u => u.Role_id == 2 && u.User_id == deletedIDRequest.ID_Deleted_Empl);

            if (employee == null)
            {
                return new NotFoundObjectResult(new
                {
                    status = false,
                    message = $"Нет такого менеджера с таким ИД: {deletedIDRequest.ID_Deleted_Empl}"
                });
            }

            var login = await _context.Logins.FirstOrDefaultAsync(l => l.User_id == deletedIDRequest.ID_Deleted_Empl);

            _context.Users.Remove(employee);
            _context.Logins.Remove(login);
            await _context.SaveChangesAsync();

            return new OkObjectResult(new
            {
                status = true
            });

        }

        // просмотр покупателей
        public async Task<IActionResult> GetAllUserAsync()
        {
            var user = await _context.Users.Where(u => u.Role_id == 3).ToListAsync();

            if (user.Count == 0)
            {
                return new NotFoundObjectResult(new
                {
                    status = false,
                    message = "Нет покупателей"
                });
            }



            return new OkObjectResult(new
            {
                data = new { customers = user },
                status = true
            });


        }

        // просмотр менеджеров
        public async Task<IActionResult> GetAllEmployeeAsync()
        {
            var employee = await _context.Users.Where(u => u.Role_id == 2).ToListAsync();

            if (employee.Count == 0)
            {
                return new NotFoundObjectResult(new
                {
                    status = false,
                    message = "Нет менеджеров"
                });
            }



            return new OkObjectResult(new
            {
                data = new { customers = employee },
                status = true
            });
        }

        // меняем данные с помошью админа или менеджера для покупателя
        public async Task<IActionResult> PutUserAsync(PutUserRequest putUser)
        {
            if (putUser.Id_check == 0)
            {
                return new BadRequestObjectResult(new
                {
                    status = false,
                    message = "Нет такого Ид"
                });
            }

            var user_put = await _context.Users.FirstOrDefaultAsync(b => b.User_id == putUser.Id_check && b.Role_id == 3);

            if (user_put == null)
            {
                return new NotFoundObjectResult(new
                {
                    status = false,
                    message = $"Нет  покупателя с таким id: {putUser.Id_check}"
                });
            }

            if (string.IsNullOrEmpty(putUser.User_fullname))
            {
                return new BadRequestObjectResult(new
                {
                    status = false,
                    message = "Фамилия и имя не могут быть пустыми"
                });
            }

            if (string.IsNullOrEmpty(putUser.Email))
            {
                return new BadRequestObjectResult(new
                {
                    status = false,
                    message = "Почта не может быть пустой"
                });
            }

            if (string.IsNullOrEmpty(putUser.Address))
            {
                return new BadRequestObjectResult(new
                {
                    status = false,
                    message = "Адрес не может быть пустым"
                });
            }

            if (string.IsNullOrEmpty(putUser.PhoneNumber))
            {
                return new BadRequestObjectResult(new
                {
                    status = false,
                    message = "Номер телефона не может быть пустым"
                });
            }

            if (string.IsNullOrEmpty(putUser.Login_N))
            {
                return new BadRequestObjectResult(new
                {
                    status = false,
                    message = "Логин не может быть пустым"
                });
            }

            if (string.IsNullOrEmpty(putUser.Password_N))
            {
                return new BadRequestObjectResult(new
                {
                    status = false,
                    message = "Пароль не может быть пустым"
                });
            }

            var check_email = await _context.Users.FirstOrDefaultAsync(e => e.Email.ToLower() == putUser.Email.ToLower() && e.User_id != putUser.Id_check);

            if (check_email != null)
            {
                return new BadRequestObjectResult(new
                {
                    message = "Пользователь с таким email уже существует",
                    status = false
                });
            }

            var check_Login = await _context.Logins.FirstOrDefaultAsync(e => e.Login_name.ToLower() == putUser.Login_N.ToLower() && e.User_id != putUser.Id_check);

            if (check_Login != null)
            {
                return new BadRequestObjectResult(new
                {
                    message = "Пользователь с таким Login уже существует",
                    status = false
                });
            }

            var check_phone_n = await _context.Users.FirstOrDefaultAsync(e => e.PhoneNumber.ToLower() == putUser.PhoneNumber.ToLower() && e.User_id != putUser.Id_check);

            if (check_phone_n != null)
            {
                return new BadRequestObjectResult(new
                {
                    message = "Пользователь с таким телефонным номером уже существует",
                    status = false
                });
            }

            var login = await _context.Logins.FirstOrDefaultAsync(l => l.User_id == putUser.Id_check);

            user_put.User_fullname = putUser.User_fullname;
            user_put.Email = putUser.Email;
            user_put.Address = putUser.Address;
            user_put.PhoneNumber = putUser.PhoneNumber;
            user_put.UpdatedAt = DateOnly.FromDateTime(DateTime.Now);
            login.Login_name = putUser.Login_N;
            login.Password = putUser.Password_N;

            await _context.SaveChangesAsync();

            return new OkObjectResult(new
            {
                status = true,
                message = "Успешно"
            });


        }

        // изменение менеджера с помощью админа
        public async Task<IActionResult> PutEmployeeAsync(PutEmployeeRequest putEmployee)
        {
            if (putEmployee.Id_check == 0)
            {
                return new BadRequestObjectResult(new
                {
                    status = false,
                    message = "Нет такого Ид"
                });
            }

            var user_put = await _context.Users.FirstOrDefaultAsync(b => b.User_id == putEmployee.Id_check && b.Role_id == 2);

            if (user_put == null)
            {
                return new NotFoundObjectResult(new
                {
                    status = false,
                    message = $"Нет менеджера с таким id: {putEmployee.Id_check}"
                });
            }

            if (string.IsNullOrEmpty(putEmployee.User_fullname))
            {
                return new BadRequestObjectResult(new
                {
                    status = false,
                    message = "Фамилия и имя не могут быть пустыми"
                });
            }

            if (string.IsNullOrEmpty(putEmployee.Email))
            {
                return new BadRequestObjectResult(new
                {
                    status = false,
                    message = "Почта не может быть пустой"
                });
            }

            if (string.IsNullOrEmpty(putEmployee.Address))
            {
                return new BadRequestObjectResult(new
                {
                    status = false,
                    message = "Адрес не может быть пустым"
                });
            }

            if (string.IsNullOrEmpty(putEmployee.PhoneNumber))
            {
                return new BadRequestObjectResult(new
                {
                    status = false,
                    message = "Номер телефона не может быть пустым"
                });
            }

            if (string.IsNullOrEmpty(putEmployee.Login_N))
            {
                return new BadRequestObjectResult(new
                {
                    status = false,
                    message = "Логин не может быть пустым"
                });
            }

            if (string.IsNullOrEmpty(putEmployee.Password_N))
            {
                return new BadRequestObjectResult(new
                {
                    status = false,
                    message = "Пароль не может быть пустым"
                });
            }

            var check_email = await _context.Users.FirstOrDefaultAsync(e => e.Email.ToLower() == putEmployee.Email.ToLower() && e.User_id != putEmployee.Id_check);

            if (check_email != null)
            {
                return new BadRequestObjectResult(new
                {
                    message = "Пользователь с таким email уже существует",
                    status = false
                });
            }

            var check_Login = await _context.Logins.FirstOrDefaultAsync(e => e.Login_name.ToLower() == putEmployee.Login_N.ToLower() && e.User_id != putEmployee.Id_check);

            if (check_Login != null)
            {
                return new BadRequestObjectResult(new
                {
                    message = "Пользователь с таким Login уже существует",
                    status = false
                });
            }

            var check_phone_n = await _context.Users.FirstOrDefaultAsync(e => e.PhoneNumber.ToLower() == putEmployee.PhoneNumber.ToLower() && e.User_id != putEmployee.Id_check);

            if (check_phone_n != null)
            {
                return new BadRequestObjectResult(new
                {
                    message = "Пользователь с таким телефонным номером уже существует",
                    status = false
                });
            }

            var login = await _context.Logins.FirstOrDefaultAsync(l => l.User_id == putEmployee.Id_check);

            user_put.User_fullname = putEmployee.User_fullname;
            user_put.Email = putEmployee.Email;
            user_put.Address = putEmployee.Address;
            user_put.PhoneNumber = putEmployee.PhoneNumber;
            user_put.UpdatedAt = DateOnly.FromDateTime(DateTime.Now);
            login.Login_name = putEmployee.Login_N;
            login.Password = putEmployee.Password_N;

            await _context.SaveChangesAsync();

            return new OkObjectResult(new
            {
                status = true
                , message = "Успешно"
            });
        }

        // изменение профиля админа через админа 
        public async Task<IActionResult> PutMyProfileAdminAsync(PutAdminMyProfilesRequests putAdminMyProfile)
        {
            string? token = _httpContext.HttpContext.Request.Headers["Authorization"].FirstOrDefault(); 
            if (token == null)
            {
                return new BadRequestObjectResult(new { status = false, message = "Неправильный jwt" });
            }

            var userid = await _context.Sessions.FirstOrDefaultAsync(u => u.Token == token); 
            if (userid == null)
            {
                return new NotFoundObjectResult(new { status = false, message = "Ошибка" });
            }

            int adminId = userid.User_id;

            var user_put = await _context.Users.FirstOrDefaultAsync(b => b.User_id == adminId && b.Role_id == 1);

            if (user_put == null)
            {
                return new NotFoundObjectResult(new
                {
                    status = false,
                    message = $"Нет админа с таким id: {adminId}"
                });
            }

            if (string.IsNullOrEmpty(putAdminMyProfile.User_fullname))
            {
                return new BadRequestObjectResult(new
                {
                    status = false,
                    message = "Фамилия и имя не могут быть пустыми"
                });
            }

            if (string.IsNullOrEmpty(putAdminMyProfile.Email))
            {
                return new BadRequestObjectResult(new
                {
                    status = false,
                    message = "Почта не может быть пустой"
                });
            }

            if (string.IsNullOrEmpty(putAdminMyProfile.Address))
            {
                return new BadRequestObjectResult(new
                {
                    status = false,
                    message = "Адрес не может быть пустым"
                });
            }

            if (string.IsNullOrEmpty(putAdminMyProfile.PhoneNumber))
            {
                return new BadRequestObjectResult(new
                {
                    status = false,
                    message = "Номер телефона не может быть пустым"
                });
            }

            if (string.IsNullOrEmpty(putAdminMyProfile.Login_N))
            {
                return new BadRequestObjectResult(new
                {
                    status = false,
                    message = "Логин не может быть пустым"
                });
            }

            if (string.IsNullOrEmpty(putAdminMyProfile.Password_N))
            {
                return new BadRequestObjectResult(new
                {
                    status = false,
                    message = "Пароль не может быть пустым"
                });
            }

            var check_email = await _context.Users.FirstOrDefaultAsync(e => e.Email.ToLower() == putAdminMyProfile.Email.ToLower() && e.User_id != adminId);

            if (check_email != null)
            {
                return new BadRequestObjectResult(new
                {
                    message = "Пользователь с таким email уже существует",
                    status = false
                });
            }

            var check_Login = await _context.Logins.FirstOrDefaultAsync(e => e.Login_name.ToLower() == putAdminMyProfile.Login_N.ToLower() && e.User_id != adminId);

            if (check_Login != null)
            {
                return new BadRequestObjectResult(new
                {
                    message = "Пользователь с таким Login уже существует",
                    status = false
                });
            }

            var check_phone_n = await _context.Users.FirstOrDefaultAsync(e => e.PhoneNumber.ToLower() == putAdminMyProfile.PhoneNumber.ToLower() && e.User_id != adminId);

            if (check_phone_n != null)
            {
                return new BadRequestObjectResult(new
                {
                    message = "Пользователь с таким телефонным номером уже существует",
                    status = false
                });
            }

            var login = await _context.Logins.FirstOrDefaultAsync(l => l.User_id == adminId);

            user_put.User_fullname = putAdminMyProfile.User_fullname;
            user_put.Email = putAdminMyProfile.Email;
            user_put.Address = putAdminMyProfile.Address;
            user_put.PhoneNumber = putAdminMyProfile.PhoneNumber;
            user_put.UpdatedAt = DateOnly.FromDateTime(DateTime.Now);
            login.Login_name = putAdminMyProfile.Login_N;
            login.Password = putAdminMyProfile.Password_N;

            var log = new LogUserAction()
            {
                Created_at = DateTime.Now,
                User_id = adminId,
                Action_id = 4 // обновление в бд
            };
            await _context.AddAsync(log);

            await _context.SaveChangesAsync();

            return new OkObjectResult(new
            {
                status = true,
                message = "Успешно"
            });
        }

        // изменение менеджера самого себя
        public async Task<IActionResult> PutMyProfileEmployeeAsync(PutEmployeeMyProfileRequests putEmployeeMyProfile)
        {
            string? token = _httpContext.HttpContext.Request.Headers["Authorization"].FirstOrDefault(); // получение токена
            if (token == null)
            {
                return new BadRequestObjectResult(new { status = false, message = "Неправильный jwt" });
            }

            var userid = await _context.Sessions.FirstOrDefaultAsync(u => u.Token == token); // поиск в бд
            if (userid == null)
            {
                return new NotFoundObjectResult(new { status = false, message = "Ошибка" });
            }

            // Используем ID менеджера из сессии (из токена)
            int managerId = userid.User_id;

            var user_put = await _context.Users.FirstOrDefaultAsync(b => b.User_id == managerId && b.Role_id == 2);

            if (user_put == null)
            {
                return new NotFoundObjectResult(new
                {
                    status = false,
                    message = $"Нет менеджера с таким id: {managerId}"
                });
            }

            if (string.IsNullOrEmpty(putEmployeeMyProfile.User_fullname))
            {
                return new BadRequestObjectResult(new
                {
                    status = false,
                    message = "Фамилия и имя не могут быть пустыми"
                });
            }

            if (string.IsNullOrEmpty(putEmployeeMyProfile.Email))
            {
                return new BadRequestObjectResult(new
                {
                    status = false,
                    message = "Почта не может быть пустой"
                });
            }

            if (string.IsNullOrEmpty(putEmployeeMyProfile.Address))
            {
                return new BadRequestObjectResult(new
                {
                    status = false,
                    message = "Адрес не может быть пустым"
                });
            }

            if (string.IsNullOrEmpty(putEmployeeMyProfile.PhoneNumber))
            {
                return new BadRequestObjectResult(new
                {
                    status = false,
                    message = "Номер телефона не может быть пустым"
                });
            }

            if (string.IsNullOrEmpty(putEmployeeMyProfile.Login_N))
            {
                return new BadRequestObjectResult(new
                {
                    status = false,
                    message = "Логин не может быть пустым"
                });
            }

            if (string.IsNullOrEmpty(putEmployeeMyProfile.Password_N))
            {
                return new BadRequestObjectResult(new
                {
                    status = false,
                    message = "Пароль не может быть пустым"
                });
            }

            var check_email = await _context.Users.FirstOrDefaultAsync(e => e.Email.ToLower() == putEmployeeMyProfile.Email.ToLower() && e.User_id != managerId);

            if (check_email != null)
            {
                return new BadRequestObjectResult(new
                {
                    message = "Пользователь с таким email уже существует",
                    status = false
                });
            }

            var check_Login = await _context.Logins.FirstOrDefaultAsync(e => e.Login_name.ToLower() == putEmployeeMyProfile.Login_N.ToLower() && e.User_id != managerId);

            if (check_Login != null)
            {
                return new BadRequestObjectResult(new
                {
                    message = "Пользователь с таким Login уже существует",
                    status = false
                });
            }

            var check_phone_n = await _context.Users.FirstOrDefaultAsync(e => e.PhoneNumber.ToLower() == putEmployeeMyProfile.PhoneNumber.ToLower() && e.User_id != managerId);

            if (check_phone_n != null)
            {
                return new BadRequestObjectResult(new
                {
                    message = "Пользователь с таким телефонным номером уже существует",
                    status = false
                });
            }

            var login = await _context.Logins.FirstOrDefaultAsync(l => l.User_id == managerId);

            user_put.User_fullname = putEmployeeMyProfile.User_fullname;
            user_put.Email = putEmployeeMyProfile.Email;
            user_put.Address = putEmployeeMyProfile.Address;
            user_put.PhoneNumber = putEmployeeMyProfile.PhoneNumber;
            user_put.UpdatedAt = DateOnly.FromDateTime(DateTime.Now);
            login.Login_name = putEmployeeMyProfile.Login_N;
            login.Password = putEmployeeMyProfile.Password_N;

            var log = new LogUserAction()
            {
                Created_at = DateTime.Now,
                User_id = managerId,
                Action_id = 4 // обновление в бд
            };
            await _context.AddAsync(log);

            await _context.SaveChangesAsync();

            return new OkObjectResult(new
            {
                status = true,
                message = "Успешно"
            });
        }

        // изменение покупателя самого себя
        public async Task<IActionResult> PutUserMyProfileAsync(PutUserMyProfilesRequests putUserMyProfile)
        {
            string? token = _httpContext.HttpContext.Request.Headers["Authorization"].FirstOrDefault();
            if (token == null)
            {
                return new BadRequestObjectResult(new { status = false, message = "Неправильный jwt" });
            }
            var userid = await _context.Sessions.FirstOrDefaultAsync(u => u.Token == token);
            if (userid == null)
            {
                return new NotFoundObjectResult(new { status = false, message = "Ошибка" });
            }

            int customerId = userid.User_id;

            var user_put = await _context.Users.FirstOrDefaultAsync(b => b.User_id == customerId && b.Role_id == 3);

            if (user_put == null)
            {
                return new NotFoundObjectResult(new
                {
                    status = false,
                    message = $"Нет покупателя с таким id: {customerId}"
                });
            }

            if (string.IsNullOrEmpty(putUserMyProfile.User_fullname))
            {
                return new BadRequestObjectResult(new
                {
                    status = false,
                    message = "Фамилия и имя не могут быть пустыми"
                });
            }

            if (string.IsNullOrEmpty(putUserMyProfile.Email))
            {
                return new BadRequestObjectResult(new
                {
                    status = false,
                    message = "Почта не может быть пустой"
                });
            }

            if (string.IsNullOrEmpty(putUserMyProfile.Address))
            {
                return new BadRequestObjectResult(new
                {
                    status = false,
                    message = "Адрес не может быть пустым"
                });
            }

            if (string.IsNullOrEmpty(putUserMyProfile.PhoneNumber))
            {
                return new BadRequestObjectResult(new
                {
                    status = false,
                    message = "Номер телефона не может быть пустым"
                });
            }

            if (string.IsNullOrEmpty(putUserMyProfile.Login_N))
            {
                return new BadRequestObjectResult(new
                {
                    status = false,
                    message = "Логин не может быть пустыми"
                });
            }

            if (string.IsNullOrEmpty(putUserMyProfile.Password_N))
            {
                return new BadRequestObjectResult(new
                {
                    status = false,
                    message = "Пароль не может быть пустым"
                });
            }

            var check_email = await _context.Users.FirstOrDefaultAsync(e => e.Email.ToLower() == putUserMyProfile.Email.ToLower() && e.User_id != customerId);

            if (check_email != null)
            {
                return new BadRequestObjectResult(new
                {
                    message = "Пользователь с таким email уже существует",
                    status = false
                });
            }

            var check_Login = await _context.Logins.FirstOrDefaultAsync(e => e.Login_name.ToLower() == putUserMyProfile.Login_N.ToLower() && e.User_id != customerId);

            if (check_Login != null)
            {
                return new BadRequestObjectResult(new
                {
                    message = "Пользователь с таким Login уже существует",
                    status = false
                });
            }

            var check_phone_n = await _context.Users.FirstOrDefaultAsync(e => e.PhoneNumber.ToLower() == putUserMyProfile.PhoneNumber.ToLower() && e.User_id != customerId);

            if (check_phone_n != null)
            {
                return new BadRequestObjectResult(new
                {
                    message = "Пользователь с таким телефонным номером уже существует",
                    status = false
                });
            }

            var login = await _context.Logins.FirstOrDefaultAsync(l => l.User_id == customerId);

            user_put.User_fullname = putUserMyProfile.User_fullname;
            user_put.Email = putUserMyProfile.Email;
            user_put.Address = putUserMyProfile.Address;
            user_put.PhoneNumber = putUserMyProfile.PhoneNumber;
            user_put.UpdatedAt = DateOnly.FromDateTime(DateTime.Now);
            login.Login_name = putUserMyProfile.Login_N;
            login.Password = putUserMyProfile.Password_N;

            var log = new LogUserAction()
            {
                Created_at = DateTime.Now,
                User_id = customerId,
                Action_id = 4 // обновление в бд
            };
            await _context.AddAsync(log);
            await _context.SaveChangesAsync();

            return new OkObjectResult(new
            {
                status = true,
                message = "Успешно"
            });
        }

        // изменение роли пользователя через админа
        public async Task<IActionResult> PutUserRoleAsync( PutUserRoleRequest putUserRole)
        {
            if (putUserRole.User_id == 0)
            {
                return new BadRequestObjectResult(new
                {
                    status = false,
                    message = "Ошибка связи с ИД"
                });
            }

            var user = await _context.Users.FirstOrDefaultAsync(b => b.User_id == putUserRole.User_id);

            if (user == null)
            {
                return new NotFoundObjectResult(new
                {
                    status = false,
                    message = $"Нет такого пользователя с таким id: {putUserRole.User_id}"
                });
            }

            if (putUserRole.Role_id == 0)
            {
                return new BadRequestObjectResult(new
                {
                    status = false,
                    message = "У пользователя должна быть роль"
                });
            }

            var role = await _context.Roles.FirstOrDefaultAsync(r => r.Role_id == putUserRole.Role_id);

            if (role == null)
            {
                return new NotFoundObjectResult(new
                {
                    status = false,
                    message = "Нет такой роли"
                });
            }

            user.Role_id = putUserRole.Role_id;

            var log = new LogUserAction()
            {
                Created_at = DateTime.Now,
                User_id = putUserRole.User_id,
                Action_id = 5 // изменение роли
            };
            await _context.AddAsync(log);
            await _context.SaveChangesAsync();

            return new OkObjectResult(new
            {
                status = true,
                message = "Успешно обновлена роль"
            });
        }
    }
}
