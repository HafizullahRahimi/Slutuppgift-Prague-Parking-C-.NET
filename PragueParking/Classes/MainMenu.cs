using System;
using System.Security.Cryptography;

namespace Classes
{
    public static class MainMenu
    {
        public static bool Show(Parking parkingName)
        {
            Console.Clear();
            Console.WriteLine("Welcome to {0} parking!\n", parkingName.NameParking);
            Console.WriteLine("Choose an option:");

            Console.WriteLine("1) Park new vehicle");
            Console.WriteLine("2) Delivery of vehicle");
            Console.WriteLine("3) Move a vehicle");
            Console.WriteLine("4) Find a vehicle");
            Console.WriteLine("5) Show content of parking places");

            Console.WriteLine("6) Exit");
            Console.Write("\r\nSelect an option: ");

            switch (Console.ReadLine())
            {
                case "1":
                    ParkNewVehicle(parkingName);
                    return true;
                case "2":
                    DeliveryOfVehicle(parkingName);
                    return true;
                case "3":
                    MoveVehicle(parkingName);
                    return true;                
                case "4":
                    FindVehicle(parkingName);
                    return true;

                case "5":
                    ShowContentOfParkingPlaces(parkingName);
                    return true;


                case "6":
                    return false;
                default:
                    return true;
            }
        }

        private static void ParkNewVehicle(Parking parkingName)
        {
            Console.Clear();
            Console.WriteLine("Park New Vehicle \n");

            bool parkNewVehicle = true;
            string owner = "";
            string type = "";
            byte parkingPlace = 0;
            byte parkingPlacePart = 0;
            bool typeIsSuccessful = false;
            bool parkingPlaceIsSuccessful = false;
            bool parkingPlacePartIsSuccessful = false;
            string regNumber = "";

            while (parkNewVehicle)
            {

                Console.Write("Enter your name: ");
                owner = Console.ReadLine();

                while (!typeIsSuccessful)
                {
                    Console.Write("Enter type of vehicle (c: car, m: motorC): ");
                    type = Console.ReadLine().ToLower();

                    if (type == "c" || type == "m")
                    {
                        typeIsSuccessful = true;
                    }
                }

                while (!parkingPlaceIsSuccessful)
                {
                    Console.Write("Enter Parking lot number: ");

                    try
                    {
                        bool isNum = byte.TryParse(Console.ReadLine(), out parkingPlace);

                        if (parkingPlace > parkingName.LengthOfRows)
                        {
                            throw new IndexOutOfRangeException();
                        }

                        if (!isNum) Console.WriteLine("Parking lot number is false!!");
                        else parkingPlaceIsSuccessful = true;

                    }
                    catch (IndexOutOfRangeException)
                    {
                        Console.WriteLine("We have just {0} parking places.", parkingName.LengthOfRows);
                        //throw;
                    }
                    catch (Exception err)
                    {
                        Console.WriteLine(err.Message);
                    }

                }
                if (type == "m")
                {
                    while (!parkingPlacePartIsSuccessful)
                    {
                        Console.Write("Enter Part 1 or 2: ");
                        try
                        {
                            parkingPlacePart = byte.Parse(Console.ReadLine());

                            if (parkingPlacePart == 1 || parkingPlacePart == 2)
                            {
                                parkingPlacePartIsSuccessful = true;
                            }
                            else Console.WriteLine("Parking Place has just 1 Part and 2 Part!!!");

                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Enter just number please");
                            //throw;
                        }
                    }
                }

                if (type == "m") regNumber = parkingName.SubmissionOfVehicle(owner, parkingPlace, parkingPlacePart);
                else regNumber = parkingName.SubmissionOfVehicle(owner, parkingPlace);


                if (regNumber == null)
                {
                    typeIsSuccessful = false;
                    parkingPlaceIsSuccessful = false;
                    parkingPlacePartIsSuccessful = false;

                    Console.Write("\nDo you want try again? y or n : ");
                    string tryAgain = Console.ReadLine().ToLower();
                    parkNewVehicle = (tryAgain == "y") ? true : false;

                    Console.Clear();
                    Console.WriteLine("Park New Vehicle \n");
                }
                else
                {

                    if (type == "m")
                    {

                        Console.WriteLine("\nThe motorc parked.");
                        Console.WriteLine("RegNumber: {0}\n", regNumber);


                    }
                    else
                    {
                        Console.WriteLine("\nThe car parked.");
                        Console.WriteLine("RegNumber: {0}\n", regNumber);

                    }

                    typeIsSuccessful = false;
                    parkingPlaceIsSuccessful = false;
                    parkingPlacePartIsSuccessful = false;

                    Console.Write("Do you want park a new vehicle again? y or n : ");
                    string parkAgain = Console.ReadLine().ToLower();
                    parkNewVehicle = (parkAgain == "y") ? true : false;

                    Console.Clear();
                    Console.WriteLine("Park New Vehicle \n");
                }

            }

        }

        private static void DeliveryOfVehicle(Parking parkingName)
        {
            Console.Clear();
            Console.WriteLine("Delivery Of Vehicle \n");

            bool deliveryOfVehicle = true;
            string regNumber = "";

            while (deliveryOfVehicle)
            {
                Console.Write("Enter RegNumber (Exa: P200): ");
                regNumber = Console.ReadLine();

                bool hasRegNumber = parkingName.DeliveryOfVehicle(regNumber);

                Console.Write("Do you want delivery of vehicle again? y or n : ");
                string parkAgain = Console.ReadLine().ToLower();
                deliveryOfVehicle = (parkAgain == "y") ? true : false;

                Console.Clear();
                Console.WriteLine("Delivery Of Vehicle \n");

            }

        }

        private static void MoveVehicle(Parking parkingName)
        {
            Console.Clear();
            Console.WriteLine("Move the Vehicle \n");

            bool moveVehicle = true;
            bool hasTheRegNumber = false;
            bool parkingPlaceIsSuccessful = false;
            bool newParkingPlacePartIsSuccessful = false;
            byte newParkingPlace = 0;
            byte newParkingPlacePart = 0;
            string regNumber = "";


            while (moveVehicle)
            {
                while (!hasTheRegNumber)
                {
                    Console.Write("Enter RegNumber (Exa: P200): ");
                    regNumber = Console.ReadLine();
                    hasTheRegNumber = parkingName.HasTheRegNumber(regNumber);
                }

                string typeOfVehicle = parkingName.GetTypeOfVehicle(regNumber);

                while (!parkingPlaceIsSuccessful)
                {
                    Console.Write("Enter New Parking lot number: ");

                    try
                    {
                        bool isNum = byte.TryParse(Console.ReadLine(), out newParkingPlace);

                        if (newParkingPlace > parkingName.LengthOfRows)
                        {
                            throw new IndexOutOfRangeException();
                        }

                        if (!isNum) Console.WriteLine("Enter just number Please!!");

                        else parkingPlaceIsSuccessful = true;

                    }
                    catch (IndexOutOfRangeException)
                    {
                        Console.WriteLine("We have just {0} parking places.", parkingName.LengthOfRows);
                        //throw;
                    }
                    catch (Exception err)
                    {
                        Console.WriteLine(err.Message);
                    }

                }
                if (typeOfVehicle == "c")
                {
                    parkingName.MovingTheVehicle(regNumber, newParkingPlace);

                }
                else
                {
                    while (!newParkingPlacePartIsSuccessful)
                    {
                        Console.Write("Enter Part 1 or 2: ");
                        try
                        {
                            newParkingPlacePart = byte.Parse(Console.ReadLine());

                            if (newParkingPlacePart == 1 || newParkingPlacePart == 2)
                            {
                                bool isFreeParkingPlace = parkingName.IsFreeParkingPlace(newParkingPlace, newParkingPlacePart);

                                if (!isFreeParkingPlace) Console.WriteLine("Part {0} is not free!!!", newParkingPlacePart);
                                else newParkingPlacePartIsSuccessful = true;

                            }
                            else Console.WriteLine("Parking Place has just 1 Part and 2 Part!!!");

                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Enter just number please");
                            //throw;
                        }
                    }

                    parkingName.MovingTheVehicle(regNumber, newParkingPlace, newParkingPlacePart);
                }


                Console.Write("Do you want move a vehicle again? y or n : ");
                string moveAgain = Console.ReadLine().ToLower();
                moveVehicle = (moveAgain == "y") ? true : false;

                if (moveVehicle)
                {
                    hasTheRegNumber = false;
                    parkingPlaceIsSuccessful = false;
                    newParkingPlacePartIsSuccessful = false;
                    regNumber = "";
                    Console.Clear();
                    Console.WriteLine("Move the Vehicle \n");

                }


            }
        }

        private static void FindVehicle(Parking parkingName)
        {
            bool hasTheRegNumber = false;
            bool findVehicle = false;
            string regNumber = "";
            Console.Clear();
            Console.WriteLine("Find a Vehicle \n");

            while (!hasTheRegNumber)
            {
                Console.Write("Enter RegNumber (Exa: P200): ");
                regNumber = Console.ReadLine();
                hasTheRegNumber = parkingName.HasTheRegNumber(regNumber);
                parkingName.SearchForVehicle(regNumber);


                Console.Write("\nDo you want find a vehicle again? y or n : ");
                string findAgain = Console.ReadLine().ToLower();
                findVehicle = (findAgain == "y") ? true : false;

                if (findVehicle)
                {
                    hasTheRegNumber = false;
                    regNumber = "";
                    Console.Clear();
                    Console.WriteLine("Find a Vehicle \n");

                }
            }

        }
        private static void ShowContentOfParkingPlaces(Parking parkingName)
        {
            Console.Clear();
            Console.WriteLine("Parking Places \n");
            parkingName.GetContentOfParkingPlaces();
            BackToMenu();
        }

        private static void BackToMenu()
        {
            Console.Write("\r\nPress Enter to Go to Menu ");
            Console.ReadLine();
        }
    }
}
