using Microsoft.AspNetCore.Mvc;
using WebApp4ByJessica.Data;
using WebApp4ByJessica.Models;

namespace WebApp4ByJessica.Controllers
{
    public class BooksController : Controller
    {
        private readonly BookRepository _repo;
        public BooksController(BookRepository repo)
        {
            _repo = repo;
        }

        public IActionResult Index()
        {
            var books = _repo.GetAll();
            return View(books);
        }

        public IActionResult Details(int id)
        {
            var book = _repo.GetById(id);
            if (book == null) return NotFound();
            return View(book);
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Book book)
        {
            if (!ModelState.IsValid) return View(book);
            var newId = _repo.Create(book);
            return RedirectToAction(nameof(Details), new { id = newId });
        }

        public IActionResult Edit(int id)
        {
            var book = _repo.GetById(id);
            if (book == null) return NotFound();
            return View(book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Book book)
        {
            if (id != book.Id) return BadRequest();
            if (!ModelState.IsValid) return View(book);
            _repo.Update(book);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var book = _repo.GetById(id);
            if (book == null) return NotFound();
            return View(book);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _repo.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
