using Microsoft.AspNetCore.Mvc;
using Simpel_Bloggapp.Models;

namespace Simpel_Bloggapp.Controllers
{
    public class BlogController : Controller
    {
        private static List<BlogPost> BlogPosts = new List<BlogPost>();

        [HttpGet]
        public IActionResult Index()
        {
            return View(BlogPosts);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(string title, string content)
        {
            if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(content))
            {
                ViewBag.Error = "Titel och Innehåll är ett måste";
                return View();

            }

            BlogPosts.Add(new BlogPost
            {
                Title = title,
                Content = content,
                Date = DateTime.Now
            });

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (id >= 0 && id < BlogPosts.Count )
            {
                var post = BlogPosts[id];
                ViewBag.Id = id;
                return View(post);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Edit(int id, string title, string content)
        {
            if(id >= 0 && id < BlogPosts.Count && !string.IsNullOrEmpty(title) && !string.IsNullOrEmpty(content))
            {
                BlogPosts[id].Title = title;
                BlogPosts[id].Content = content;
                BlogPosts[id].Date = DateTime.Now;
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            if( id >= 0 && id < BlogPosts.Count)
            {
                BlogPosts.RemoveAt(id);
            }

            return RedirectToAction("Index");
        }
    }
}
