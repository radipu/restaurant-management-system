using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TomaFoodRestaurant.Model
{
   public  class Reservation
    {

        public int SL { set; get; }
        public int restaurantId { set; get; }
        public DateTime reservedDate { set; get; }
        public string FirstName { set; get; }
        public string LastName { set; get; }
        public string Email { set; get; }
        public string Phone { set; get; }
        public int noOfPeople { set; get; }
        public string specialNotes { set; get; }
        public string Type { set; get; }
        public int Status { set; get; }
        public int ReserveId { set; get; }
        public DateTime ReservationDate { set; get; }
        public int online_reservation_id { set; get; }
        public int RestaurantId { get; set; }
        public string EmailText { set; get; }
        public string Arrived { set; get; }

        public string encryptedLink { set; get; }
    }
}
