using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LibraryReservedSystem.Data;
using LibraryReservedSystem.Models;
using NuGet.Frameworks;

namespace LibraryReservedSystem.Controllers
{
    public class BooksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BooksController(ApplicationDbContext context)
        {
            _context = context;
        }


        // GET: Books
        public async Task<IActionResult> Index()
        {
            return _context.Book != null ?
                       View(await _context.Book.ToListAsync()) :
                       Problem("Entity set 'ApplicationDbContext.Book'  is null.");
        }

        public async Task<IActionResult> ShowSearchForm()
        {
            return View();

        }
        // Getting the books from the search bar
        public async Task<IActionResult> ShowSearchResults(String SearchTitle)
        {
            if (_context.Book == null)
            {
                return NotFound();
            }

            var book = await _context.Book.Where(b => b.BookTitle.Contains(SearchTitle) || b.ISBN.ToString().Contains(SearchTitle)).ToListAsync();

            if (book == null)
            {
                return NotFound();
            }

            return View("ShowSearchForm", await _context.Book.Where(b => b.BookTitle.Contains(SearchTitle.TrimEnd())).ToListAsync());

        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Book == null)
            {
                return NotFound();
            }

            var book = await _context.Book
                .FirstOrDefaultAsync(m => m.ID == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }


        // GET: Books/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Subject,CourseNumber,CourseTitle,Professor,BookTitle,Edition,ISBN,Author,CallNumber, numCopies")] Book book)
        {
            if (ModelState.IsValid)
            {
                _context.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Book == null)
            {
                return NotFound();
            }

            var book = await _context.Book.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Subject,CourseNumber,CourseTitle,Professor,BookTitle,Edition,ISBN,Author,CallNumber, numCopies")] Book book)
        {
            if (id != book.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(book);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.ID))
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
            return View(book);
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Book == null)
            {
                return NotFound();
            }

            var book = await _context.Book
                .FirstOrDefaultAsync(m => m.ID == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Book == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Book'  is null.");
            }
            var book = await _context.Book.FindAsync(id);
            if (book != null)
            {
                _context.Book.Remove(book);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(int id)
        {
            return (_context.Book?.Any(e => e.ID == id)).GetValueOrDefault();
        }


        // GET: Books/CheckOut/5
        public async Task<IActionResult> CheckOut(int? id)
        {
            if (id == null || _context.Book == null)
            {
                return NotFound();
            }

            var book = await _context.Book.FindAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            // trying to check if the book is already checked out and if it
            // is then it shouldn't go to the checked out page
            if (book.isCheckedOut == true)
            {
                // Creating message for dialog box
                TempData["WarningMessage"] = book.BookTitle + " is already checked-out!";
                return RedirectToAction("Index");
            }

            // THIS IS FOR DEALING WITH THE COPIES OF THE BOOKS STILL WORKING ON HOW THIS WILL BE IMPLEMENTED
            /*    if(book.numCopies < 1)
                {
                    // Creating message for dialog box
                    TempData["WarningMessage"] = book.BookTitle + " is already checked-out!";
                    return RedirectToAction("Index");
                }*/
            return View(book);
        }
        // POST: Books/CheckOut/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CheckOut(int id, [Bind("ID,Subject,CourseNumber,CourseTitle,Professor,BookTitle,Edition,ISBN,Author,CallNumber,numCopies,studIDCheckIO, checkOutDate, checkInDate")] Book book)
        {
            if (id != book.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // getting the book from the ID that was passed
                    var result = _context.Book.SingleOrDefault(b => b.ID == book.ID);
                    // trying to get the student who ID matches with the one that is checked out
                    var studResult = _context.UserProfile.SingleOrDefault(n => n.WVUID == book.studIDCheckIO);
                    if (result != null && studResult != null && studResult.itemCount < 3 && result.numCopies > 0 && book.checkInDate != null && book.checkOutDate != null)
                    {
                        // Set the check out/in dates, the student ID who has the book, 
                        // the student's item count and change the isCheckedOut value 
                        result.checkOutDate = book.checkOutDate;
                        result.checkInDate = book.checkInDate;
                        result.studIDCheckIO = book.studIDCheckIO;
                        result.numCopies -= 1;
                        studResult.itemCount += 1;
                        result.isCheckedOut = true;
                        _context.SaveChanges();
                    }
                    else if (studResult == null || book.checkInDate == null || book.checkOutDate == null)
                    {
                        // Creating message for dialog box
                        TempData["WarningMessage"] = "Invalid Student ID or Date!";
                    }
                    else if (studResult.itemCount > 3)
                    {
                        // Creating message for dialog box
                        TempData["WarningMessage"] = studResult.WVUID + " already has the maximum of 3 items checked-out!";
                    }

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.ID))
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
            return View(book);
        }

        // GET: Books/CheckIn/5
        public async Task<IActionResult> CheckIn(int? id)
        {
            if (id == null || _context.Book == null)
            {
                return NotFound();
            }

            var book = await _context.Book
                .FirstOrDefaultAsync(m => m.ID == id);
            if (book == null)
            {
                return NotFound();
            }

            // trying to check if the book is already checked out and if it not
            // is then it shouldn't go to the checked in page
            if (book.isCheckedOut == false)
            {
                // Creating message for dialog box
                TempData["WarningMessage"] = book.BookTitle + " is not checked-out!";
                return RedirectToAction("Index");
            }
            return View(book);
        }


        // POST: Books/CheckIn/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CheckIn(int id, [Bind("ID,Subject,CourseNumber,CourseTitle,Professor,BookTitle,Edition,ISBN,Author,CallNumber,numCopies,studIDCheckIO, checkOutDate, checkInDate")] Book book)
        {

            if (id != book.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // getting the book from the ID that was passed
                    var result = _context.Book.SingleOrDefault(b => b.ID == book.ID);
                    // trying to get the student who ID matches with the one that is checked out
                    var studResult = _context.UserProfile.SingleOrDefault(n => n.WVUID == result.studIDCheckIO);
                    if (result != null && studResult != null)
                    {
                        // Seeing if the book was checked in on time 
                        if (DateTime.Compare((DateTime)result.checkInDate, DateTime.Now) > 0)
                        {
                            // Creating message for dialog box
                            TempData["SuccessMessage"] = result.BookTitle + " was checked-in!";
                        }
                        else
                        {
                            // Creating message for dialog box
                            TempData["WarningMessage"] = result.BookTitle + " was checked-in late!";
                        }

                        result.checkOutDate = null;
                        result.checkInDate = null;
                        result.studIDCheckIO = null;
                        result.numCopies += 1;
                        studResult.itemCount -= 1;
                        result.isCheckedOut = false;
                        _context.SaveChanges();

                    }

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.ID))
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
            return View(book);
        }

    }
}

