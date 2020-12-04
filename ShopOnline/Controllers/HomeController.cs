using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShopOnline.Models;
using ShopOnline.Models.db;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;

namespace ShopOnline.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;
        }

        SemicolonContext db = new SemicolonContext();

        [HttpPost]
        public ActionResult Login()
        {
        //    //Checking the state of model passed as parameter.
        //    if (ModelState.IsValid)
        //    {

        //        //Validating the user, whether the user is valid or not.
        //        var isValidUser = IsValidUser(user);

        //        //If user is valid & present in database, we are redirecting it to Welcome page.
        //        if (isValidUser != null)
        //        {
        //            FormsAuthentication.SetAuthCookie(user.UserPass, false);
        //            return RedirectToAction("Index");
        //        }
        //        else
        //        {
        //            //If the username and password combination is not present in DB then error message is shown.
        //            ModelState.AddModelError("Failure", "Wrong Username and password combination !");
        //            return View();
        //        }
        //    }
        //    else
        //    {
        //        //If model state is not valid, the model with error message is returned to the View.
        //        return View(user);
        //    }
        //}
        string btnCick = HttpContext.Request.Form["Login"].ToString();
            if (btnCick == "Login")
            {
                string Username = HttpContext.Request.Form["username"].ToString();
                string Password = HttpContext.Request.Form["password"].ToString();
                //var login = (from e in GetLoginUserList()
                //             where e.UserName == Username &&
                //             e.Password == Password
                //             select e).FirstOrDefault();
                // if (login != null)
                // {

                    if (Username == "admin" && Password == "1234")
                    {

                        return RedirectToAction("Index");
                    }
                    else
                    {

                        return RedirectToAction("Register");
                    }
                //}
                // else
                // return View("Index");
            }
            return View();
        }

        public async Task<IActionResult> Index()
        {
            return View(await db.Users.ToListAsync());
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        public async Task<IActionResult> Profile(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await db.Users
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }
        [Authorize]
        public async Task<ViewResult> Register(bool isSuccess = false, int userId = 0)
        {
            var model = new User();

            ViewBag.IsSuccess = isSuccess;
            ViewBag.BookId = userId;
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Obsolete]
        public async Task<IActionResult> Register(User user)
        {
            if (ModelState.IsValid)
            {
                //upload image
                //if (user.UserImage != null && user.UserImage.Length > 0)
                //{
                //    var imagePath = @"\images\";
                //    var uploadPath = _env.WebRootPath + imagePath;

                //    if (!Directory.Exists(uploadPath))
                //    {
                //        Directory.CreateDirectory(uploadPath);
                //    }

                //    var uniqFileName = Guid.NewGuid().ToString();
                //    var filename = Path.GetFileName(uniqFileName + "." + user.UserImage.Split(".")[1].ToLower());
                //    string fullPath = uploadPath + filename;

                //    imagePath = imagePath + @"\";
                //    var filePath = @".." + Path.Combine(imagePath, filename);

                //    using (var fileStream = new FileStream(fullPath, FileMode.Create))
                //    {
                //        await user.CopyToAsync(fileStream);
                //    }

                //    ViewData["FileLocation"] = filePath;
                //}
                //byte[] bytes;
                //using (BinaryReader br = new BinaryReader(image.InputStream))
                //{
                //    bytes = br.ReadBytes(image.ContentLength);
                //}
                //db.Add(user{
                //    user.UserImage = Path.GetFileName(image.FileName),
                //    user.Data = bytes
                //});
                //if (file != null && file.Length > 0)
                //{
                //    string filename = Path.GetFileName(file.FileName);
                //    string Imgpath = Path.Combine(Server.MapPath("~/images/"), filename);
                //    file.SaveAs(Imgpath);
                //}
                if (user.UserImage != null)
                {
                    string folder = "Img/profile/";
                    user.UserPathimg = await UploadImage(folder, user.UserImage);
                }

                db.Add(user);
                await db.SaveChangesAsync();
                if (user.UserId > 0)
                {
                    return RedirectToAction(nameof(Register), new { isSuccess = true, userId = user.UserId });
                }
                return RedirectToAction(nameof(Index));

            }
            return View(user);
        }
        private async Task<string> UploadImage(string folderPath, IFormFile file)
        {

            folderPath += Guid.NewGuid().ToString() + "_" + file.FileName;

            string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folderPath);

            await file.CopyToAsync(new FileStream(serverFolder, FileMode.Create));

            return "/" + folderPath;
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await db.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: CRUD/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserName,UserPass,UserImage,UserAddress,UserEmail,UserPhone,UserUpdate")] User user)
        {
            if (id != user.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(user);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.UserId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: CRUD/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await db.Users
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: CRUD/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await db.Users.FindAsync(id);
            db.Users.Remove(user);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private bool UserExists(int id)
        {
            return db.Users.Any(e => e.UserId == id);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
