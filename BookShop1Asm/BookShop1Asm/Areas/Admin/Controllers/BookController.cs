using BookShop1Asm.Interfaces;
using BookShop1Asm.Models;
using BookShop1Asm.ViewModels.BookViewModel;
using BookShop1Asm.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;

namespace BookShop1Asm.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BookController : Controller
    {
        //private readonly AppDBContext _dbContext;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHost;

        public BookController(/*AppDBContext dbContext,*/ IUnitOfWork unitOfWork, IWebHostEnvironment webhost)
        {
            //_dbContext = dbContext;
            _unitOfWork = unitOfWork;
            _webHost = webhost;
        }

        /*public IActionResult Index()
        {
            //List<Book> books = _dbContext.Book.ToList();
            List<Book> books = _unitOfWork.Book.GetAll();
            return View(books);
        }*/

        public IActionResult Index(string search, int catId=0)
        {
            //List<Book> books = _dbContext.Book.ToList();

            ViewBag.catId = new SelectList(_unitOfWork.Category.GetAll(), "Id", "Name");
            List<Book> books = _unitOfWork.Book.GetAll();
            if (!string.IsNullOrEmpty(search))
            {
                books = _unitOfWork.Book.Search(search);
            }
            if (catId != 0)
            {
                books= books.Where(v => v.BookCategories.Select(c => c.CategoryId).Contains(catId)).ToList();
            }
            return View(books);
        }

        public IActionResult Details(int? id)
        {
            Book book = _unitOfWork.Book.GetById(id);
            return View(book);
        }

        public IActionResult CreateUpdate(int? id)
        {
            CreateUpdateVM bookCUvm = new CreateUpdateVM()
            {
                MyAuthors = _unitOfWork.Author.GetAll().Select(a => new SelectListItem
                {
                    Text = a.Alias,
                    Value = a.Id.ToString()
                }),
                MyCategories = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                Book = new Book()
            };
            List<int> auIds = new List<int>();
            List<int> catIds = new List<int>();
            if (id == null ||id == 0)
            {
                //Create new Book
                return View(bookCUvm);
            }
            else
            {
                //Update a Book
                //book = _dbContext.Book.Find(id);
                bookCUvm.Book = _unitOfWork.Book.GetById(id);
                bookCUvm.Book.BookAuthors.ToList().ForEach(res => auIds.Add(res.AuthorId));
                bookCUvm.Book.BookCategories.ToList().ForEach(res => catIds.Add(res.CategoryId));
                bookCUvm.AuIDs = auIds.ToArray();
                bookCUvm.CatIDs = catIds.ToArray();

                return View(bookCUvm);
            }

        }
        [HttpPost]
        public IActionResult CreateUpdate(CreateUpdateVM bookCUvm, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHost.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string bookPath = Path.Combine(wwwRootPath, "img\\bookcover");
                    if (!string.IsNullOrEmpty(bookCUvm.Book.Cover))
                    {
                        var oldImagePath = Path.Combine(wwwRootPath, bookCUvm.Book.Cover.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }
                    using (var fileStream = new FileStream(Path.Combine(bookPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    bookCUvm.Book.Cover = @"\img\bookcover\" + fileName;
                }
                if (bookCUvm.Book.Id == 0)
                {
                    //_dbContext.Book.Add(book);
                    if (bookCUvm.AuIDs != null && bookCUvm.AuIDs.Length > 0)
                    {
                        foreach (var author in bookCUvm.AuIDs)
                        {
                            bookCUvm.Book.BookAuthors.Add(new BookAuthor()
                            {

                                AuthorId = author
                            });
                        }
                    }
                    if (bookCUvm.CatIDs != null && bookCUvm.CatIDs.Length > 0)
                    {
                        foreach (var category in bookCUvm.CatIDs)
                        {
                            bookCUvm.Book.BookCategories.Add(new BookCategory()
                            {

                                CategoryId = category
                            });
                        }
                    }
                    _unitOfWork.Book.Insert(bookCUvm.Book);
                    TempData["success"] = "Book created succesfully";
                }
                else
                {
                    List<BookAuthor> oldBookAuthors = new List<BookAuthor>();
                    bookCUvm.Book.BookAuthors.ToList().ForEach(res => oldBookAuthors.Add(res));
                    _unitOfWork.Book.ResetAuthor(bookCUvm.Book);

                    List<BookCategory> oldBookCategories = new List<BookCategory>();
                    bookCUvm.Book.BookCategories.ToList().ForEach(res => oldBookCategories.Add(res));
                    _unitOfWork.Book.ResetCategory(bookCUvm.Book);

                    _unitOfWork.Book.Update(bookCUvm.Book);

                    if (bookCUvm.AuIDs != null && bookCUvm.AuIDs.Length > 0)
                    {
                        List<BookAuthor> newBookAuthors = new List<BookAuthor>();
                        foreach (var author in bookCUvm.AuIDs)
                        {
                            newBookAuthors.Add(new BookAuthor()
                            {
                                BookId = bookCUvm.Book.Id,
                                AuthorId = author
                            });
                        }
                        _unitOfWork.AddRange(newBookAuthors);
                    }

                    if (bookCUvm.CatIDs != null && bookCUvm.CatIDs.Length > 0)
                    {
                        List<BookCategory> newBookCategories = new List<BookCategory>();
                        foreach (var category in bookCUvm.CatIDs)
                        {
                            newBookCategories.Add(new BookCategory()
                            {
                                CategoryId = category,
                                BookId = bookCUvm.Book.Id
                            });
                        }
                        _unitOfWork.AddRange(newBookCategories);
                    }
                    TempData["success"] = "Book updated succesfully";
                }
                //_dbContext.SaveChanges();
                _unitOfWork.Save();
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
            //Book? book = _dbContext.Book.Find(id);
            Book? book = _unitOfWork.Book.GetById(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }
        [HttpPost]
        public IActionResult Delete(Book book)
        {
            if (!string.IsNullOrEmpty(book.Cover))
            {
                string wwwRootPath = _webHost.WebRootPath;
                var oldImagePath = Path.Combine(wwwRootPath, book.Cover.TrimStart('\\'));
                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }
            }
            //_dbContext.Book.Remove(book);
            //_dbContext.SaveChanges();
            _unitOfWork.Book.Delete(book);
            _unitOfWork.Save();
            TempData["success"] = "Book deleted succesfully";
            return RedirectToAction("Index");
        }
    }
}
