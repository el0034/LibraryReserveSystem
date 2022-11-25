using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryReservedSystem.Models
{
    public class Book
    {
        public int ID { get; set; }
        public string Subject { get; set; } = string.Empty;

        [Display(Name = "Course Number")]
        public int CourseNumber { get; set; }

        [Display(Name = "Course Title")]
        public string CourseTitle { get; set; } = string.Empty;
        public string Professor { get; set; } = string.Empty;

        [Display(Name = "Book Title")]
        public string BookTitle { get; set; } = string.Empty;
        public string Edition { get; set; } = string.Empty;

        public double ISBN { get; set; }
        public string Author { get; set; } = string.Empty;

        [Display(Name = "Call Number")]
        public string CallNumber { get; set; } = string.Empty;

        // Creating the variable to see if a book is checked out
        [Display(Name = "Is book checked out?")]
        public Boolean isCheckedOut { get; set; }

        // Creating the variable to store the id of the student who has the book
        [Display(Name = "Reservee ID")]
        public double? ReserveeID { get; set; }

        // Currently storing all inputs as Datetime, would it be better as a string? We will see.
        // Creating the variable to see the check out date for the book
        [Display(Name = "Check-Out Date")]
        public DateTime? checkOutDate { get; set; }

        // Creating the variable to see the check in date/time. A item can only be checked out for two hours
        [Display(Name = "Check-In Date")]
        public DateTime? checkInDate { get; set; }

        // Creating the variable for the number of copies
        [Display(Name = "Number of Copies")]
        public int numCopies { get; set; }
        public Book()
        {

        }


    }
}

