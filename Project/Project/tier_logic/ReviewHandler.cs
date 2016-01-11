using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project
{
    class ReviewHandler
    {
        // Fields

        // Constructor
        public ReviewHandler()
        {

        }
        // Properties
        public List<Review> ReviewList { get; set; }
        // Methodes
        public bool AddReview(Review review)
        {
            if (DatabaseHandler.AddReview(review))
                return true;
            else
                return false;
        }

        public bool DeleteReview(Review review)
        {
            if (DatabaseHandler.DeleteReview(review.ID))
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
            ReviewList = DatabaseHandler.GetAllReviews();
        }
    }
}