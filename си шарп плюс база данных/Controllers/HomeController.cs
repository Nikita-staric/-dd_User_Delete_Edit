using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using си_шарп_плюс_база_данных.Models;

namespace си_шарп_плюс_база_данных.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationContext db;//Для взаимодействия с базой данных в контроллере определяется переменная контекст данных ApplicationContext db.
        public HomeController(ApplicationContext context)
        {
            db = context;
        }

        //Теперь добавим в контроллер три метода, которые будут добавлять новый объект в базу данных и выводить из нее все объекты:
        public async Task<IActionResult> Index()
        {
            return View(await db.Users.ToListAsync());// С помощью метода db.Users.ToListAsnc() мы будем получать объекты из бд, создавать из них список и передавать в представление.
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(User user)//для данных из объекта user формируется sql-выражение INSERT
        {
            db.Users.Add(user);
            await db.SaveChangesAsync();// выполняет это выражение, тем самым добавляя данные в базу данных.
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id != null)
            {
                User user = await db.Users.FirstOrDefaultAsync(p => p.Id == id);
                if (user != null)
                    return View(user);
            }
            return NotFound();
        }



        public async Task<IActionResult> Edit(int? id)
        {
            if (id != null)
            {
                User user = await db.Users.FirstOrDefaultAsync(p => p.Id == id);
                if (user != null)
                    return View(user);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(User user)
        {
            db.Users.Update(user);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)//принимает параметр id, с помощью которого получаем удаляемый объект из БД и, если он существует, удаляем его с помощью метода db.Users.Remove().
        {
            if (id != null)
            {

                User user = new User { Id = id.Value };
                db.Entry(user).State = EntityState.Deleted;//сгенерирует sql-выражение DELETE.
                //User? user = await db.Users.FirstOrDefaultAsync(p => p.Id==id);
                //if (user != null)
                //{
                //    db.Users.Remove(user);
                //    await db.SaveChangesAsync();
                //    return RedirectToAction("Index");
                //}
            }
            return NotFound();

        }

        //public IActionResult Delete()
        //{
        //    return View();
        //}


        //public IActionResult Index()
        //{
        //    return View();
        //}

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
