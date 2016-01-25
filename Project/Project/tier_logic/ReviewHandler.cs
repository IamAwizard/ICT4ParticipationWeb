using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project
{
    class ReviewHandler
    {
        // Fields
        DatabaseHandler databasehandler;
        private List<Review> reviews; 
        // Constructor
        public ReviewHandler()
        {
            databasehandler = new DatabaseHandler();
        }
        // Properties
        public List<Review> ReviewList
        {
            get
            {
                return reviews;
            }
        }
        // Methodes
        public bool AddReview(Review review)
        {
            if (databasehandler.AddReview(review))
                return true;
            else
                return false;
        }

        public bool DeleteReview(Review review)
        {
            if (databasehandler.DeleteReview(review.ID))
                return true;
            else
                return false;
        }

        public List<Review> GetAllReviews()
        {
            Synchronize();
            return ReviewList;
        }

        private void Synchronize()
        {
            reviews = databasehandler.GetAllReviews();
        }

        public Review GetReviewByIDCached(int reviewid)
        {
            Synchronize();
            try
            {
                return reviews.Find(x => x.ID == reviewid);
            }
            catch (NullReferenceException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return null;
            }
        }
    }
}