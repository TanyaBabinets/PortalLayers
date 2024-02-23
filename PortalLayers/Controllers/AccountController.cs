using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PortalLayers.BLL.DTO;
using PortalLayers.BLL.DTO.Interfaces;
using PortalLayers.BLL.Interfaces;
using PortalLayers.BLL.Services;
using PortalLayers.DAL.Entities;
using PortalLayers.Models;
using System.Security.Cryptography;
using System.Text;
namespace PortalLayers.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService userService;
        private readonly ISongService songService;
        private readonly IGenreService genreService;
        IWebHostEnvironment _appEnvironment;
        public AccountController(IUserService us, ISongService ss, IGenreService gs, IWebHostEnvironment appEnvironment)
        {
            userService = us;
            songService = ss;
            genreService = gs;
            _appEnvironment = appEnvironment;
        }
        public ActionResult Login()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

      
public async Task<IActionResult> Login(UserDTO log)
        {
            var userList = await userService.GetUsers();
            var checkUser = userList.FirstOrDefault(e => e.Login == log.Login);
        //    var checkUser = userList.Where(e => e.Login == log.Login).FirstOrDefault();
            if (checkUser == null)
            {
                ModelState.AddModelError("", "Такого пользователя нет");
                return View(log);
            }
            if (ModelState.IsValid)
            {

                if (userService.GetUsers().Result.ToList().Count == 0)
                {
                    ModelState.AddModelError("", "Wrong login or password!");
                    return View(log);
                }
                var users = userService.GetUsers().Result.Where(a => a.Login == log.Login);
                if (users.ToList().Count == 0)
                {
                    ModelState.AddModelError("", "Wrong login or password!");
                    return View(log);
                }
                var user = users.First();
                string salt = user.Salt;
                byte[] password = Encoding.Unicode.GetBytes(salt + log.Password);
                var md5 = MD5.Create();

                byte[] byteHash = md5.ComputeHash(password);

                StringBuilder hash = new StringBuilder(byteHash.Length);
                for (int i = 0; i < byteHash.Length; i++)
                    hash.Append(string.Format("{0:X2}", byteHash[i]));

                if (user.Password != hash.ToString())
                {
                    ModelState.AddModelError("", "Wrong login or password!");
                    return View(log);
                }
                HttpContext.Session.SetString("FirstName", user.FirstName);
                HttpContext.Session.SetString("LastName", user.LastName);
                HttpContext.Session.SetString("Id", user.Id.ToString());
                if (user.Login == "admin")
                {
                    return Approval();

                }
                else if (!user.IsActivated)
                {
                    ModelState.AddModelError("", "Еще не подтвержден");
                    return View(log);
                }
                return RedirectToAction("Index", "Song");
            }
            return View(log);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(UserDTO reg)
        {
            var userList = await userService.GetUsers();
            var checkUser = userList.Where(e => e.Login == reg.Login).FirstOrDefault();

            if (checkUser != null)
            {
                ModelState.AddModelError("Login", "Такой логин уже существует");
                return View(reg);
            }


            if (ModelState.IsValid)
            {
                UserDTO user = new UserDTO();
                user.FirstName = reg.FirstName;
                user.LastName = reg.LastName;
                user.Login = reg.Login;
                user.Email = reg.Email;
                user.Salt=reg.Salt;
                byte[] saltbuf = new byte[16];

                RandomNumberGenerator randomNumberGenerator = RandomNumberGenerator.Create();
                randomNumberGenerator.GetBytes(saltbuf);

                StringBuilder sb = new StringBuilder(16);
                for (int i = 0; i < 16; i++)
                    sb.Append(string.Format("{0:X2}", saltbuf[i]));
                string salt = sb.ToString();
                byte[] password = Encoding.Unicode.GetBytes(salt + reg.Password);
                var md5 = MD5.Create();

                byte[] byteHash = md5.ComputeHash(password);

                StringBuilder hash = new StringBuilder(byteHash.Length);
                for (int i = 0; i < byteHash.Length; i++)
                    hash.Append(string.Format("{0:X2}", byteHash[i]));

                user.Password = hash.ToString();
                user.Salt = salt;
                user.IsActivated = false;
                await userService.CreateUser(user);

                 return RedirectToAction("Index", "Home");
             
            }

            return View(reg);
        }

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserDTO u)
        {
            if (ModelState.IsValid)
            {
                await userService.CreateUser(u);
                return View("~/Views/Song/Index.cshtml", await userService.GetUsers());
            }
            return View(u);
        }
        public IActionResult Approval()
        {
            var usersToApprove = userService.GetUsers().Result.Where(e => e.IsActivated == false).ToList();

            return View("Approval", usersToApprove);
        }
        //[HttpPost]
        //[ValidateAntiForgeryToken]
    

        public async Task<IActionResult> ApproveUser(int userId)

        {
            var user = await userService.GetById(userId);

            if (user != null)
            {
                user.IsActivated = true;
               await userService.UpdateUser(user);
                //await userService.Save();

            }
            var usersToApprove = (await userService.GetUsers()).Where(e => !e.IsActivated).ToList();
            return View("Approval", usersToApprove);

        }
    }


}

