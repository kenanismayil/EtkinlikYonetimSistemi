using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using System;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            ActivityTypeManager activityTypeManager = new ActivityTypeManager(new EfActivityTypeDal());


            //ActivityType activityType = new ActivityType { ActivityTypeName = "Eğitim ve Kariyer" };
            //activityTypeManager.Add(activityType);




            CountryManager countryManager = new CountryManager(new EfCountryDal());
            //Country country = new Country { CountryName = "Azerbeycan" };
            //countryManager.Add(country);




            CityManager cityManager = new CityManager(new EfCityDal());
            //City city = new City { CityName = "Konya", CountryId = 1 };
            //cityManager.Add(city);



            //Activity activity = new Activity
            //{
            //    ActivityName = "Basketbol Etkinliği",
            //    ActivityTypeId = 4,
            //    CityId = 3,
            //    CreatedTime = DateTime.Now,
            //    AppDeadLine = DateTime.Now.AddDays(10),
            //    ActivityDate = DateTime.Now.AddDays(15)
            //};
            //activityManager.Add(activity);







            //UserManager userManager = new UserManager(new EfUserDal());
            //User user = new User 
            //{
            //    FirstName = "Zübeyir", 
            //    LastName = "Aktekin", 
            //    Password = "zub434",
            //    Email = "zubaktekin233@gmail.com",
            //    DateOfBirth = "24.05.1999", 
            //    Phone = "05521567237" 
            //};
            //userManager.Add(user);





            //ModeratorManager moderatorManager = new ModeratorManager(new EfModeratorDal());
            //Moderator moderator = new Moderator { UserId = 2 };
            //moderatorManager.Add(moderator);



            RegistrationManager registrationManager = new RegistrationManager(new EfRegistrationDal());
            //Registration registration = new Registration
            //{
            //    ActivityId = 2,
            //    UserId = 1,
            //    Date = DateTime.Now              
            //};
            //registrationManager.Add(registration);





            CommentManager commentManager = new CommentManager(new EfCommentDal());
            //Comment comment = new Comment
            //{
            //    UserId = 1,
            //    ActivityId = 2,
            //    CommentText = "Faydali bir etkinlik oldu"
            //};
            //commentManager.Add(comment);

            //var result = commentManager.GetAll();
            //foreach (var item in result.Data)
            //{
            //    Console.WriteLine(item.UserId + "->" + item.Content);
            //}




            Console.ReadLine();
        }
    }
}
