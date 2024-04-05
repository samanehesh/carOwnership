using System;
using System.Linq;

namespace CarOwnership
{
    internal class Menu
    {
        public void MainMenu()
        {
            bool exit = false;
            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("\n\tMain Menu:");
                Console.WriteLine("\t1. List All Cars");
                Console.WriteLine("\t2. Car Details");
                Console.WriteLine("\t3. Add Car");
                Console.WriteLine("\t4. Update Car");
                Console.WriteLine("\t5. Delete Car");
                Console.WriteLine("\t6. Exit");
                Console.Write("\tEnter your choice: ");

                using (CarOwnershipEntities1 db = new CarOwnershipEntities1())
                {
                    switch (Console.ReadLine())
                    {
                        case "1":
                            {
                                ListAllCars(db);
                                break;
                            }
                        case "2":
                            {
                                CarDetails(db);
                                break;
                            }
                        case "3":
                            {
                                AddCar(db);
                                break;
                            }
                        case "4":
                            {
                                UpdateCar(db);
                                break;
                            }
                        case "5":
                            {
                                // Delete car
                                DeleteCar(db);
                                break;
                            }
                        case "6":
                            {
                                // Exit
                                exit = true;
                                Console.WriteLine("\n\tThank you for using the Car App.");
                                break;
                            }
                        default:
                            {
                                Console.WriteLine("\n\tInvalid choice. Please try again.");
                                break;
                            }
                    }
                }
                Console.Write("\n\tPress any key to continue...");
                Console.ReadKey();
            }
        }

        private void ListAllCars(CarOwnershipEntities1 db)
        {
            DbDisplayAllCars(db);
        }

        private void CarDetails(CarOwnershipEntities1 db)
        {
            string registrationNumber = GetRegistrationNumber();
            Car car = DbGetCar(db, registrationNumber);

            if (car != null)
            {
                DisplayCarDetails(car);
            }
            else
            {
                Console.WriteLine("\n\tCar not found, please try again.");
            }
        }

        private void AddCar(CarOwnershipEntities1 db)
        {
            Console.WriteLine("\n\t Enter New Car Details");
            Console.WriteLine("\t------------------------");
            string registrationNumber = GetRegistrationNumber();

            Car car = GetCarInfoFromUser();
            car.pkCarRegistrationNumber = registrationNumber;

            DbAddCar(db, car);
            DbDisplayAllCars(db);
        }

        private void UpdateCar(CarOwnershipEntities1 db)
        {
            string registrationNumber = GetRegistrationNumber();
            Car car = DbGetCar(db, registrationNumber);

            if (car != null)
            {
                DisplayCarDetails(car);
                Console.WriteLine("\tEnter new car details.");

                Car newCarInfo = GetCarInfoFromUser();
                newCarInfo.pkCarRegistrationNumber = registrationNumber;

                DbUpdateCar(db, newCarInfo);
                DisplayCarDetails(car);
            }
            else
            {
                Console.WriteLine("\n\tCar not found, please try again.");
            }
        }

        private void DeleteCar(CarOwnershipEntities1 db)
        {
            string registrationNumber = GetRegistrationNumber();
            Car car = DbGetCar(db, registrationNumber);

            if (car != null)
            {
                DbDeleteCar(db, car);
                DbDisplayAllCars(db);
            }
            else
            {
                Console.WriteLine("\n\tAn error occurred processing your request, please try again.");
            }
        }

        private string GetRegistrationNumber()
        {
            Console.Clear();
            Console.Write("\n\tEnter the Car's registration: ");
            
            return Console.ReadLine();
        }

        private void DisplayCarDetails(Car car)
        {
            Console.WriteLine($"\n\t   {car.pkCarRegistrationNumber} Car Details");
            Console.WriteLine("\t------------------------");
            Console.WriteLine($"\tModel           : {car.model}");
            Console.WriteLine($"\tManufacture Year: {car.yearManufactured}");
            Console.WriteLine($"\tColour          : {car.color}");
            Console.WriteLine($"\tEngine Type     : {car.engineType}\n");
        }

        private Car GetCarInfoFromUser()
        {
            Car car = new Car();

            Console.Write("\n\tModel: ");
            car.model = Console.ReadLine();

            Console.Write("\tManufacture Year: ");
            bool isValidYear = Int32.TryParse(Console.ReadLine(), out int manufacturedYear);
            if (isValidYear && manufacturedYear >= 0 && manufacturedYear <= DateTime.Now.Year)
            {
                car.yearManufactured = manufacturedYear;
            }
            else
            {
                Console.WriteLine($"\n\tInvalid year entered, {DateTime.Now.Year} will be used.\n");
                car.yearManufactured = DateTime.Now.Year;
            }

            Console.Write("\tColour: ");
            car.color = Console.ReadLine();

            Console.Write("\tEngine Type: ");
            car.engineType = Console.ReadLine();
            car.fkManufacturerID = 1;
            return car;
        }

        private void DbAddCar(CarOwnershipEntities1 db, Car car)
        {
            try
            {
                db.Cars.Add(car);
                db.SaveChanges();
                Console.WriteLine("\n\tCar added successfully.");

            }
            catch (Exception ex)
            {
                Console.WriteLine("\n\tCar can't be added.");
            }
            
        }

        private void DbUpdateCar(CarOwnershipEntities1 db, Car newCarInfo)
        {
            Car car = (from c in db.Cars
                               where c.pkCarRegistrationNumber == newCarInfo.pkCarRegistrationNumber
                               select c).FirstOrDefault();

            try
            {
                car.model = newCarInfo.model;
                car.yearManufactured = newCarInfo.yearManufactured;
                car.color = newCarInfo.color;
                car.engineType = newCarInfo.engineType;
                db.SaveChanges();
                Console.WriteLine("\n\tCar updated successfully.");

            }

            catch 
            {
                Console.WriteLine("\tCan't edit car details.");
            }   
            

        }

        private void DbDeleteCar(CarOwnershipEntities1 db, Car car)
        {
            try
            {
                db.Cars.Remove(car);
                db.SaveChanges();
                Console.WriteLine("\n\tCar deleted successfully.");

            }
            catch 
            {
                Console.WriteLine("\tCan't delete car.");
            }
            
        }

        private void DbDisplayAllCars(CarOwnershipEntities1 db)
        {
            var query = from c in db.Cars
                        select new
                        {
                            c.pkCarRegistrationNumber,
                            c.model,
                            c.yearManufactured,
                            c.color,
                        };

            Console.WriteLine("\n\t          All Cars");
            Console.WriteLine("\t-----------------------------");


            foreach (var car in query)
            {
                Console.WriteLine($"\t{car.pkCarRegistrationNumber,-6}: {car.yearManufactured,-7}{car.model,-8}{car.color}");
            }

        }

        private Car DbGetCar(CarOwnershipEntities1 db, string registrationNumber)
        {
            Car car = (from c in db.Cars
                                  where c.pkCarRegistrationNumber == registrationNumber
                                  select c).FirstOrDefault();
                                    
                                  


            return car;
        }
    }
}
