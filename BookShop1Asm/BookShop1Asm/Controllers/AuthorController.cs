using BookShop1Asm.Models;
using BookShopAsm.Data;
using Microsoft.AspNetCore.Mvc;

namespace BookShop1Asm.Controllers
{
    public class AuthorController : Controller
    {
        private readonly AppDBContext _dbContext;
        //private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHost;

        public AuthorController(AppDBContext dbContext /*IUnitOfWork unitOfWork*/, IWebHostEnvironment webhost)
        {
            _dbContext = dbContext;
            //_unitOfWork = unitOfWork;
            _webHost = webhost;
        }

        public IActionResult Index()
        {
            List<Author> authors = _dbContext.Author.ToList();
            return View(authors);
        }

        public IActionResult CreateUpdate(int? id)
        {
            Author author = new Author();
            if (id == null || id == 0)
            {
                //Create new Auhtor
                return View(author);
            }
            else
            {
                //Update an Author
                author= _dbContext.Author.Find(id);
                return View(author);
            }

        }
        [HttpPost]
        public IActionResult CreateUpdate(Author author, IFormFile? file)
        {

            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHost.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string authorPath = Path.Combine(wwwRootPath, "img\\authorcover");
                    if (!String.IsNullOrEmpty(author.Photo))
                    {
                        var oldImagePath = Path.Combine(wwwRootPath, author.Photo.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }
                    using (var fileStream = new FileStream(Path.Combine(authorPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    author.Photo = @"\img\authorcover\" + fileName;
                }
                if (author.Id == 0)
                {
                    _dbContext.Author.Add(author);
                    TempData["success"] = "Author created succesfully";
                }
                else
                {
                    _dbContext.Author.Update(author);
                    TempData["success"] = "Author updated succesfully";
                }
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();

        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Author? author = _dbContext.Author.Find(id);
            if (author == null)
            {
                return NotFound();
            }
            return View(author);
        }
        [HttpPost]
        public IActionResult Delete(Author author)
        {
            if (!String.IsNullOrEmpty(author.Photo))
            {
                string wwwRootPath = _webHost.WebRootPath;
                var oldImagePath = Path.Combine(wwwRootPath, author.Photo.TrimStart('\\'));
                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }
            }
            _dbContext.Author.Remove(author);
            _dbContext.SaveChanges();
            TempData["success"] = "Author deleted succesfully";
            return RedirectToAction("Index");
        }
    }
}
