using BookShop1Asm.Models;
using BookShopAsm.Data;
using Microsoft.AspNetCore.Mvc;

namespace BookShop1Asm.Controllers
{
    public class BookController : Controller
    {
        private readonly AppDBContext _dbContext;
        //private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHost;
        
        public BookController(AppDBContext dbContext /*IUnitOfWork unitOfWork*/, IWebHostEnvironment webhost)
        {
            _dbContext = dbContext;
            //_unitOfWork = unitOfWork;
            _webHost = webhost;
        }

        public IActionResult Index()
        {
            List<Book> books = _dbContext.Book.ToList();
            //List<Book> books = _unitOfWork.BookRepository.GetAll("Category").ToList();
            return View(books);
        }

        public IActionResult CreateUpdate(int? id)
        {
            Book book = new Book();
            if (id == null || id == 0)
            {
                //Create new Book
                return View(book);
            }
            else
            {
                //Update a Book
                book = _dbContext.Book.Find(id);
                return View(book);
            }

        }
        [HttpPost]

        public IActionResult CreateUpdate(Book book, IFormFile? file)
        {

            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHost.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string bookPath = Path.Combine(wwwRootPath, "img\\bookcover");
                    if (!String.IsNullOrEmpty(book.Cover))
                    {
                        var oldImagePath = Path.Combine(wwwRootPath, book.Cover.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }
                    using (var fileStream = new FileStream(Path.Combine(bookPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    book.Cover = @"\img\bookcover\" + fileName;
                }
                if (book.Id == 0)
                {
                    _dbContext.Book.Add(book);
                    TempData["success"] = "Book created succesfully";
                }
                else
                {
                    _dbContext.Book.Update(book);
                    TempData["success"] = "Book updated succesfully";
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
            Book? book = _dbContext.Book.Find(id);
            //Book? book = _unitOfWork.BookRepository.Get(book => book.Id == id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }
        [HttpPost]
        public IActionResult Delete(Book book)
        {
            if (!String.IsNullOrEmpty(book.Cover))
            {
                string wwwRootPath = _webHost.WebRootPath;
                var oldImagePath = Path.Combine(wwwRootPath, book.Cover.TrimStart('\\'));
                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }
            }
            _dbContext.Book.Remove(book);
            _dbContext.SaveChanges();
            //_unitOfWork.BookRepository.Remove(book);
            //_unitOfWork.Save();
            TempData["success"] = "Book deleted succesfully";
            return RedirectToAction("Index");
        }
    }
}
