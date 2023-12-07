using AutoMapper;
using BookShop1Asm.Interfaces;
using BookShop1Asm.Models;
using BookShop1Asm.ViewModels.BookViewModel;
using BookShopAsm.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;

namespace BookShop1Asm.Controllers
{
    public class BookController : Controller
    {
        //private readonly AppDBContext _dbContext;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHost;
        private IMapper _mapper;

        public BookController(/*AppDBContext dbContext*/ IUnitOfWork unitOfWork, IWebHostEnvironment webhost, IMapper mapper)
        {
            //_dbContext = dbContext;
            _unitOfWork = unitOfWork;
            _webHost = webhost;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            //List<Book> books = _dbContext.Book.ToList();
            List<Book> books = _unitOfWork.Book.GetAll();
            return View(books);
        }

        public IActionResult CreateUpdate(int id)
        {
            CreateUpdateVM bookCUvm = new CreateUpdateVM()
            {
                MyCategories = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                Book = new Book()
            };
            if (id == 0)
            {
                //Create new Book
                return View(bookCUvm);
            }
            else
            {
                //Update a Book
                //book = _dbContext.Book.Find(id);
                bookCUvm.Book = _unitOfWork.Book.GetById(id);
                var categories = _unitOfWork.Category.GetAll();
                var selectCategories = bookCUvm.Book.BookCategories.Select(x => new Category()
                {
                    Id = x.Category.Id,
                    Name = x.Category.Name,
                });
                var selectList = new List<SelectListItem>();
                categories.ForEach(i => selectList.Add(new SelectListItem(i.Name, i.Id.ToString(), selectCategories.Select(x => x.Id).Contains(i.Id))));

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
                    if (!String.IsNullOrEmpty(bookCUvm.Book.Cover))
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
                    foreach(var category in bookCUvm.SelectedCategories)
                    {
                        bookCUvm.Book.BookCategories.Add(new BookCategory()
                        {
                            CategoryId = Int32.Parse(category)
                        }) ;
                    }
                    _unitOfWork.Book.Insert(bookCUvm.Book);
                    TempData["success"] = "Book created succesfully";
                }
                else
                {
                    //_dbContext.Book.Update(book);
                    var selectedCategories = bookCUvm.SelectedCategories.Select(x => Int32.Parse(x)).ToList();
                    var existingCategories = bookCUvm.Book.BookCategories.Select(x => x.CategoryId).ToList();
                    var toAdd = selectedCategories.Except(existingCategories).ToList();
                    var toRemove = existingCategories.Except(selectedCategories).ToList();

                    _unitOfWork.Book.Update(bookCUvm.Book);
                    TempData["success"] = "Book updated succesfully";
                }
                //_dbContext.SaveChanges();
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }

            return View();

        }

        public IActionResult Delete(int id)
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
            if (!String.IsNullOrEmpty(book.Cover))
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
