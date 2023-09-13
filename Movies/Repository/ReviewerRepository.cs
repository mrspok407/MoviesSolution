using Microsoft.EntityFrameworkCore;
using Movies.Data;
using Movies.Interfaces;
using Movies.Models;

namespace Movies.Repository
{
    public class ReviewerRepository : IReviewerRepository
    {
        private readonly DataContext _context;
        public ReviewerRepository(DataContext context)
        {
            _context = context;
        }

        public Reviewer GetReviewer(int reviewerId)
        {
            return _context.Reviewers.Include(r => r.Reviews).FirstOrDefault(r => r.Id == reviewerId);
        }

        public ICollection<Reviewer> GetReviewers()
        {
            return _context.Reviewers.OrderBy(r => r.FirstName).Include(r => r.Reviews).ToList();
        }

        public ICollection<Review> GetReviewsByReviewer(int reviewerId)
        {
            return _context.Reviews.Where(r => r.Reviewer.Id == reviewerId).ToList();
        }

        public bool ReviewerExists(int reviewerId)
        {
            return _context.Reviewers.Any(r => r.Id == reviewerId);
        }

        public bool CreateReviewer(Reviewer reviewer)
        {
            _context.Add(reviewer);
            return Save();
        }

        public bool UpdateReviewer(Reviewer reviewer)
        {
            _context.Update(reviewer);
            return Save();
        }
        public bool DeleteReviewer(Reviewer reviewer)
        {
            return _context.Reviewers.Where(r => r.Id == reviewer.Id).Include(r => r.Reviews).ExecuteDelete() > 0;
        }


        public bool Save()
        {
            return _context.SaveChanges() > 0;
        }
    }
}
