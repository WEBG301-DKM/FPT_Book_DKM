﻿using BookShop1Asm.Interfaces;
using BookShop1Asm.Models;
using BookShop1Asm.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace BookShop1Asm.Areas.StoreOwner.Controllers
{
    [Area("StoreOwner")]
    [Authorize(Roles = "StoreOwner")]
    public class AuthorController : Controller
    {
        //private readonly AppDBContext _dbContext;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHost;

        public AuthorController(/*AppDBContext dbContext, */IUnitOfWork unitOfWork, IWebHostEnvironment webhost)
        {
            //_dbContext = dbContext;
            _unitOfWork = unitOfWork;
            _webHost = webhost;
        }

        public IActionResult Index()
        {
            List<Author> authors = _unitOfWork.Author.GetAll();
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
                author = _unitOfWork.Author.GetById(id);
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
                    if (!string.IsNullOrEmpty(author.Photo))
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
                    _unitOfWork.Author.Insert(author);
                  //  TempData["success"] = "Author created succesfully";
                }
                else
                {
                    _unitOfWork.Author.Update(author);
                 //   TempData["success"] = "Author updated succesfully";
                }
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
            Author? author = _unitOfWork.Author.GetById(id);
            if (author == null)
            {
                return NotFound();
            }
            return View(author);
        }
        [HttpPost]
        public IActionResult Delete(Author author)
        {
            if (!string.IsNullOrEmpty(author.Photo))
            {
                string wwwRootPath = _webHost.WebRootPath;
                var oldImagePath = Path.Combine(wwwRootPath, author.Photo.TrimStart('\\'));
                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }
            }
            _unitOfWork.Author.Delete(author);
            _unitOfWork.Save();
       //     TempData["success"] = "Author deleted succesfully";
            return RedirectToAction("Index");
        }
    }
}
