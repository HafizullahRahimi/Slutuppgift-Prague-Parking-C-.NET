using System;
using System.Reflection;

namespace Classes
{
    //Parking Class
    public class Parking
    {
        private string _regNumber = "P";
        private uint _number = 200;
        private Vehicle[,] _parkingPlaces;

        public string NameParking { get; private set; }
        public int LengthOfRows { get { return _parkingPlaces.GetLength(0); } }


        //Parking Bill For A Car: 40kr
        private void ParkingBill(int index)
        {
            int hours = 0;
            int minutes = 0;
            int parkingTime;
            decimal bill = 0;

            DateTime now = DateTime.Now;
            DateTime timeVolatile = _parkingPlaces[index, 0].ArrivalTime;

            TimeSpan timeDiff = now - timeVolatile;

            hours = (timeDiff.Days * 24) + timeDiff.Hours;
            minutes = timeDiff.Minutes;
            parkingTime = (hours * 60) + minutes;

            bill = parkingTime * (40m / 60);


            Console.WriteLine("\nRegNumber: {0}", _parkingPlaces[index, 0].RegNumber);
            Console.WriteLine("     Parking Place: {0}", _parkingPlaces[index, 0].ParkPlace);
            Console.WriteLine("     Vehicle: Car");
            Console.WriteLine("     Owner: {0}", _parkingPlaces[index, 0].Owner);
            Console.WriteLine("     Arrival: {0} ", _parkingPlaces[index, 0].ArrivalTime);
            Console.WriteLine("     Departure: {0} ", now);
            Console.WriteLine("     Parking Time: {0}:{1}", hours, minutes);
            Console.WriteLine("     Bill: {0:c}\n", bill);

            _parkingPlaces[index, 0] = null;
            _parkingPlaces[index, 1] = null;
        }

        //Parking Bill For A MotorC: 25kr
        private void ParkingBill(int index, int element)
        {
            int hours = 0;
            int minutes = 0;
            int parkingTime;
            decimal bill = 0;

            DateTime now = DateTime.Now;
            DateTime timeVolatile = _parkingPlaces[index, element].ArrivalTime;

            TimeSpan timeDiff = now - timeVolatile;

            hours = (timeDiff.Days * 24) + timeDiff.Hours;
            minutes = timeDiff.Minutes;
            parkingTime = (hours * 60) + minutes;


            bill = parkingTime * (25m / 60); //25kr

            Console.WriteLine("\nRegNumber: {0}", _parkingPlaces[index, element].RegNumber);
            Console.WriteLine("     Parking Place: {0}", _parkingPlaces[index, element].ParkPlace);
            Console.WriteLine("     Parking Place Part: {0}", _parkingPlaces[index, element].ParkPlacePart);
            Console.WriteLine("     Vehicle: MotorC");
            Console.WriteLine("     Owner: {0}", _parkingPlaces[index, element].Owner);
            Console.WriteLine("     Arrival: {0} ", _parkingPlaces[index, element].ArrivalTime);
            Console.WriteLine("     Departure: {0} ", now);
            Console.WriteLine("     Parking Time: {0}:{1}", hours, minutes);
            Console.WriteLine("     Bill: {0:c}\n", bill);


            _parkingPlaces[index, element] = null;

        }

        // Vehicle Info (Car)
        private bool VehicleInfo(int index)
        {
            Console.WriteLine("\n\n----------------");
            Console.WriteLine("RegNumber: {0}", _parkingPlaces[index, 0].RegNumber);
            Console.WriteLine("     Parking Place: {0}", _parkingPlaces[index, 0].ParkPlace);
            Console.WriteLine("     Vehicle: Car");
            Console.WriteLine("     Owner: {0}", _parkingPlaces[index, 0].Owner);
            Console.WriteLine("     Time of arrival: {0} ", _parkingPlaces[index, 0].ArrivalTime.ToLongTimeString());

            return true;
        }
        // Vehicle Info (MotorC)
        private bool VehicleInfo(int index, int element)
        {
            Console.WriteLine("\n\n----------------");
            Console.WriteLine("RegNumber: {0}", _parkingPlaces[index, element].RegNumber);
            Console.WriteLine("     Parking Place: {0}", _parkingPlaces[index, element].ParkPlace);
            Console.WriteLine("     Parking Place Part: {0}", _parkingPlaces[index, element].ParkPlacePart);
            Console.WriteLine("     Vehicle: MotorC");
            Console.WriteLine("     Owner: {0}", _parkingPlaces[index, element].Owner);
            Console.WriteLine("     Time of arrival: {0} ", _parkingPlaces[index, element].ArrivalTime.ToLongTimeString());

            return true;
        }

        //MoveVehicle Car:
        private bool MoveVehicle(string owner, Byte parkingPlace, DateTime arrivalTime, string regNumber)
        {
            bool result = false;
            try
            {
                if (_parkingPlaces[parkingPlace - 1, 0] == null && _parkingPlaces[parkingPlace - 1, 1] == null)
                {

                    Vehicle car = new Vehicle(owner, parkingPlace, arrivalTime, regNumber);

                    _parkingPlaces[parkingPlace - 1, 0] = car;
                    _parkingPlaces[parkingPlace - 1, 1] = car;

                    result = true;
                }
                else Console.WriteLine("Parkring place {0} is Not Free!!!");

            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("We have just {0} parking places.", LengthOfRows);
                //throw;
            }

            return result;

        }

        //MoveVehicle MotorC: 
        private void MoveVehicle(string owner, Byte parkingPlace, byte parkingPlacePart, DateTime arrivalTime, string regNumber)
        {

            Vehicle motorc = new Vehicle(owner, parkingPlace, parkingPlacePart, arrivalTime, regNumber);
            _parkingPlaces[parkingPlace - 1, parkingPlacePart - 1] = motorc;

        }

        //Moving To New Place : Car
        private bool MovingToNewPlace(int index, byte newPlace)
        {

            var oldCar = _parkingPlaces[index, 0];

            var newCar = MoveVehicle(oldCar.Owner, newPlace,oldCar.ArrivalTime, oldCar.RegNumber);

            Console.WriteLine("Car with regNumber {0} moved to palce {1}", _parkingPlaces[index, 0].RegNumber, newPlace);

            _parkingPlaces[index, 0] = null;
            _parkingPlaces[index, 1] = null;

            return true;
        }
        //Moving To New Place : MotorC
        private bool MovingToNewPlace(byte index, byte part, byte newPlace, byte newPart)
        {
            var oldMotorC = _parkingPlaces[index, part];

            MoveVehicle(oldMotorC.Owner, newPlace,newPart, oldMotorC.ArrivalTime, oldMotorC.RegNumber);

            _parkingPlaces[index, part] = null;


            Console.WriteLine("MotorC with regNumber {0} moved to palce {1} part {2}", oldMotorC.RegNumber, newPlace, newPart);


            return true;
        }



        //Constructor Parking
        public Parking(string nameParking, byte numOfParkringPlaces, byte numOfParkringPlaceParts)
        {
            _parkingPlaces = new Vehicle[numOfParkringPlaces, numOfParkringPlaceParts];
            NameParking = nameParking;
        }


        //Submission of Car
        public string SubmissionOfVehicle(string owner, Byte parkingPlace)
        {
            string result = null;
            try
            {

                if (_parkingPlaces[parkingPlace - 1, 0] == null && _parkingPlaces[parkingPlace - 1, 1] == null)
                {
                    string regNumber = $"{_regNumber}{_number}";

                    DateTime arrivalTime = DateTime.Now;

                    Vehicle car = new Vehicle(owner, parkingPlace, arrivalTime, regNumber);

                    _parkingPlaces[parkingPlace - 1, 0] = car;
                    _parkingPlaces[parkingPlace - 1, 1] = car;

                    _number++;

                    result = regNumber;
                }
                else Console.WriteLine("Parkring place {0} is Not Free!!!", parkingPlace);

            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("We have just {0} parking places.", LengthOfRows);
                //throw;
            }

            return result;

        }

        //Submission of MotorC
        public string SubmissionOfVehicle(string owner, byte parkingPlace, byte parkPlacePart)
        {
            string result = null;
            try
            {

                if (_parkingPlaces[parkingPlace - 1, parkPlacePart - 1] == null)
                {
                    string regNumber = $"{_regNumber}{_number}";


                    DateTime arrivalTime = DateTime.Now;

                    Vehicle motorc = new Vehicle(owner, parkingPlace, parkPlacePart, arrivalTime, regNumber);

                    _parkingPlaces[parkingPlace - 1, parkPlacePart - 1] = motorc;

                    _number++;

                    result = regNumber;
                }
                else
                {
                    Console.WriteLine("Parkring place {0} part {1} is Not Free!!!", parkingPlace, parkPlacePart);
                }
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("We have just {0} parking places.", LengthOfRows);
                //throw;
            }

            return result;

        }

        //Submission of Car: arrivalTime
        public string SubmissionOfVehicle(string owner, Byte parkingPlace, DateTime arrivalTime)
        {
            string result = null;
            try
            {
                if (_parkingPlaces[parkingPlace - 1, 0] == null && _parkingPlaces[parkingPlace - 1, 1] == null)
                {
                    string regNumber = $"{_regNumber}{_number}";

                    Vehicle car = new Vehicle(owner, parkingPlace, arrivalTime, regNumber);

                    _parkingPlaces[parkingPlace - 1, 0] = car;
                    _parkingPlaces[parkingPlace - 1, 1] = car;

                    _number++;

                    result = regNumber;
                }
                else Console.WriteLine("Parkring place {0} is Not Free!!!");
                
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("We have just {0} parking places.", LengthOfRows);
                //throw;
            }

            return result;

        }

        //Submission of MotorC: arrivalTime
        public string SubmissionOfVehicle(string owner, byte parkingPlace, byte parkingPlacePart, DateTime arrivalTime)
        {

            string result = null;
            try
            {
                if (_parkingPlaces[parkingPlace - 1, parkingPlacePart - 1] == null)
                {
                    string regNumber = $"{_regNumber}{_number}";


                    Vehicle motorc = new Vehicle(owner, parkingPlace, parkingPlacePart, arrivalTime, regNumber);

                    _parkingPlaces[parkingPlace - 1, parkingPlacePart - 1] = motorc;

                    _number++;

                    result = regNumber;
                }

                else Console.WriteLine("Parkring place {0} part {1} is Not Free!!!", parkingPlace, parkingPlacePart);


            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("We have just {0} parking places.", LengthOfRows);
                //throw;
            }

            return result;
        }


        //Get Content Of Parking Places
        public void GetContentOfParkingPlaces()
        {
            int Place = 1;
            for (int i = 0; i < LengthOfRows; i++)
            {
                Place = i + 1;
                if (_parkingPlaces[i, 0] == null && _parkingPlaces[i, 1] == null)
                {
                    Console.WriteLine("Parking Place {0}: Free", Place);
                }
                else if (_parkingPlaces[i, 0] == null)
                {
                    Console.WriteLine("Parking Place {0}:", Place);
                    Console.WriteLine("  Part 1: Free");
                    Console.WriteLine("  Part 2:");
                    Console.WriteLine("     RegNumber: {0} ", _parkingPlaces[i, 1].RegNumber);
                    Console.WriteLine("     Vehicle: MotorC");
                    Console.WriteLine("     Owner: {0}", _parkingPlaces[i, 1].Owner);
                    Console.WriteLine("     Time of arrival: {0} ", _parkingPlaces[i, 1].ArrivalTime.ToLongTimeString());
                }
                else if (_parkingPlaces[i, 1] == null)
                {

                    Console.WriteLine("Parking Place {0}:", Place);
                    Console.WriteLine("  Part 1:");
                    Console.WriteLine("     RegNumber: {0} ", _parkingPlaces[i, 0].RegNumber);
                    Console.WriteLine("     Vehicle: MotorC");
                    Console.WriteLine("     Owner: {0}", _parkingPlaces[i, 0].Owner);
                    Console.WriteLine("     Time of arrival: {0} ", _parkingPlaces[i, 0].ArrivalTime.ToLongTimeString());
                    Console.WriteLine("  Part 2: Free ");
                }
                else if( _parkingPlaces[i, 0].RegNumber == _parkingPlaces[i, 1].RegNumber)
                {

                    Console.WriteLine("Parking Place {0}:", Place);
                    Console.WriteLine("  RegNumber: {0} ", _parkingPlaces[i, 0].RegNumber);
                    Console.WriteLine("  Vehicle: Car");
                    Console.WriteLine("  Owner: {0}", _parkingPlaces[i, 0].Owner);
                    Console.WriteLine("  Time of arrival: {0} ", _parkingPlaces[i, 0].ArrivalTime.ToLongTimeString());
                }      

                else 
                {
                    Console.WriteLine("Parking Place {0}:", Place);
                    Console.WriteLine("  Part 1:");
                    Console.WriteLine("     RegNumber: {0} ", _parkingPlaces[i, 0].RegNumber);
                    Console.WriteLine("     Vehicle: MotorC");
                    Console.WriteLine("     Owner: {0}", _parkingPlaces[i, 0].Owner);
                    Console.WriteLine("     Time of arrival: {0} ", _parkingPlaces[i, 0].ArrivalTime.ToLongTimeString());
                    Console.WriteLine("  Part 2:");
                    Console.WriteLine("     RegNumber: {0} ", _parkingPlaces[i, 1].RegNumber);
                    Console.WriteLine("     Vehicle: MotorC");
                    Console.WriteLine("     Owner: {0}", _parkingPlaces[i, 1].Owner);
                    Console.WriteLine("     Time of arrival: {0} ", _parkingPlaces[i, 1].ArrivalTime.ToLongTimeString());

                }

                Console.WriteLine("--------------------------------");
            }
        }

        //Get Free Parking Places
        public void GetFreeParkingPlaces(VehicleType vehicleType)
        {
            int Place = 0;
            if (vehicleType == VehicleType.car)
            {
                Console.WriteLine("CAR ################################");
                for (int i = 0; i < LengthOfRows; i++)
                {
                    Place = i + 1;
                    if (_parkingPlaces[i, 0] == null && _parkingPlaces[i, 1] == null)
                    {
                        Console.WriteLine("Parking Place {0}: Free", Place);
                    }
                }
            }
            else
            {
                Console.WriteLine("Motorc ################################");
                for (int i = 0; i < LengthOfRows; i++)
                {
                    Place = i + 1;
                    if (_parkingPlaces[i, 0] == null && _parkingPlaces[i, 1] == null)
                    {
                        Console.WriteLine("Parking Place {0}:", Place);
                        Console.WriteLine("  Part 1: Free");
                        Console.WriteLine("  Part 2: Free");
                        Console.WriteLine("--------------------------");

                    }
                    else if (_parkingPlaces[i, 0] == null)
                    {
                        //Console.WriteLine("Parking Place {0} part 1  Is free", Place);

                        Console.WriteLine("Parking Place {0}:", Place);
                        Console.WriteLine("  Part 1: Free");

                        Console.WriteLine("--------------------------");
                    }
                    else if (_parkingPlaces[i, 1] == null)
                    {
                        //Console.WriteLine("Parking Place {0} part 2  Is free", Place);

                        Console.WriteLine("Parking Place {0}:", Place);
                        Console.WriteLine("  Part 2: Free");
                        Console.WriteLine("--------------------------");
                    }

                }

            }
        }

        //IsFreeParkingPlace 
        public bool IsFreeParkingPlace(byte parkingPlace)
        {
            if (_parkingPlaces[parkingPlace-1, 0] == null && _parkingPlaces[parkingPlace-1, 1] == null) return true;
            else return false;
        }
        //IsFreeParkingPlace with Part
        public bool IsFreeParkingPlace(byte parkingPlace, byte parkingPlacePart)
        {
            if (_parkingPlaces[parkingPlace - 1, parkingPlacePart - 1] == null) return true;
            else return false;
        }

        //Delivery Of Vehicle
        public bool DeliveryOfVehicle(string regNumber)
        {
            bool hasTheRegNumber = false;

            for (int i = 0; i < LengthOfRows; i++)
            {

                if (_parkingPlaces[i, 0] != null && _parkingPlaces[i, 1] != null)
                {

                    if (_parkingPlaces[i, 0].RegNumber == regNumber && _parkingPlaces[i, 1].RegNumber == regNumber)
                    {
                        ParkingBill(i);
                        hasTheRegNumber = true;
                    }
                    else if (_parkingPlaces[i, 1] == null && _parkingPlaces[i, 0].RegNumber == regNumber)
                    {
                        ParkingBill(i, 0);
                        hasTheRegNumber = true;
                    }
                    else if (_parkingPlaces[i, 0] == null && _parkingPlaces[i, 1].RegNumber == regNumber)
                    {
                        ParkingBill(i, 1);
                        hasTheRegNumber = true;
                    }

                }
                else if (_parkingPlaces[i, 0] != null && _parkingPlaces[i, 1] == null)
                {
                    if (_parkingPlaces[i, 0].RegNumber == regNumber)
                    {
                        ParkingBill(i, 0);
                        hasTheRegNumber = true;

                    }
                }
                else if (_parkingPlaces[i, 1] != null && _parkingPlaces[i, 0] == null)
                {
                    if (_parkingPlaces[i, 1].RegNumber == regNumber)
                    {
                        ParkingBill(i, 1);
                        hasTheRegNumber = true;
                    }
                }
            }

            if (!hasTheRegNumber) Console.WriteLine("We have Note regNumber {0} !!", regNumber);


            return hasTheRegNumber;

        }
        //GetTypeOfVehicle
        public string GetTypeOfVehicle(string regNumber)
        {
            bool hasTheRegNumber = false;
            string result = "";

            for (int i = 0; i < LengthOfRows; i++)
            {

                if (_parkingPlaces[i, 0] != null && _parkingPlaces[i, 1] != null)
                {

                    if (_parkingPlaces[i, 0].RegNumber == regNumber && _parkingPlaces[i, 1].RegNumber == regNumber)
                    {
                        result = "c";
                        hasTheRegNumber = true;
                    }
                    else if (_parkingPlaces[i, 1] == null && _parkingPlaces[i, 0].RegNumber == regNumber)
                    {
                        result = "m";
                        hasTheRegNumber = true;
                    }
                    else if (_parkingPlaces[i, 0] == null && _parkingPlaces[i, 1].RegNumber == regNumber)
                    {
                        result = "m";
                        hasTheRegNumber = true;
                    }

                }
                else if (_parkingPlaces[i, 0] != null && _parkingPlaces[i, 1] == null)
                {
                    if (_parkingPlaces[i, 0].RegNumber == regNumber)
                    {
                        result = "m";
                        hasTheRegNumber = true;

                    }
                }
                else if (_parkingPlaces[i, 1] != null && _parkingPlaces[i, 0] == null)
                {
                    if (_parkingPlaces[i, 1].RegNumber == regNumber)
                    {
                        result = "m";
                        hasTheRegNumber = true;
                    }
                }
            }

            if (!hasTheRegNumber) Console.WriteLine("We have Note regNumber {0} !!", regNumber);


            return result;

        }

        //Has The RegNumber
        public bool HasTheRegNumber(string regNumber)
        {
            bool hasTheRegNumber = false;

            for (int i = 0; i < LengthOfRows; i++)
            {

                if (_parkingPlaces[i, 0] != null && _parkingPlaces[i, 1] != null)
                {

                    if (_parkingPlaces[i, 0].RegNumber == regNumber && _parkingPlaces[i, 1].RegNumber == regNumber)
                    {
                        hasTheRegNumber = true;
                    }
                    else if (_parkingPlaces[i, 1] != null && _parkingPlaces[i, 0].RegNumber == regNumber)
                    {
                        hasTheRegNumber = true;
                    }
                    else if (_parkingPlaces[i, 0] != null && _parkingPlaces[i, 1].RegNumber == regNumber)
                    {
                        hasTheRegNumber = true;
                    }

                }
                else if (_parkingPlaces[i, 0] != null && _parkingPlaces[i, 1] == null)
                {
                    if (_parkingPlaces[i, 0].RegNumber == regNumber)
                    {
                        hasTheRegNumber = true;

                    }
                }
                else if (_parkingPlaces[i, 1] != null && _parkingPlaces[i, 0] == null)
                {
                    if (_parkingPlaces[i, 1].RegNumber == regNumber)
                    {
                        hasTheRegNumber = true;
                    }
                }
            }

            if (!hasTheRegNumber) Console.WriteLine("We have Not regNumber {0} !!", regNumber);


            return hasTheRegNumber;

        }

        //Search For Vehicle
        public void SearchForVehicle(string regNumber)
        {
            bool hasTheRegNumber = false;

            for (int i = 0; i < LengthOfRows; i++)
            {
                if (_parkingPlaces[i, 0] != null && _parkingPlaces[i, 1] != null)
                {
                    if (_parkingPlaces[i, 0].RegNumber == regNumber && _parkingPlaces[i, 1].RegNumber == regNumber)
                    {
                        hasTheRegNumber = VehicleInfo(i);
                    }
                    else if (_parkingPlaces[i, 1] == null && _parkingPlaces[i, 0].RegNumber == regNumber)
                    {
                        hasTheRegNumber = VehicleInfo(i, 0);
                    }
                    else if (_parkingPlaces[i, 0] == null && _parkingPlaces[i, 1].RegNumber == regNumber)
                    {
                        hasTheRegNumber = VehicleInfo(i, 1);
                    }
                }
                else if (_parkingPlaces[i, 0] != null && _parkingPlaces[i, 1] == null)
                {
                    if (_parkingPlaces[i, 0].RegNumber == regNumber)
                    {
                        hasTheRegNumber = VehicleInfo(i, 0);

                    }
                }
                else if (_parkingPlaces[i, 1] != null && _parkingPlaces[i, 0] == null)
                {
                    if (_parkingPlaces[i, 1].RegNumber == regNumber)
                    {
                        hasTheRegNumber = VehicleInfo(i, 1);
                    }
                }

            }

            if (!hasTheRegNumber)
            {
                Console.WriteLine("\n\n----------------");
                Console.WriteLine("{0} Not Found!!", regNumber);
            }

        }

        //Moving The Vehicle: Car
        public void MovingTheVehicle(string regNumber, byte newPlace)
        {
            bool isFreePlace = IsFreeParkingPlace(newPlace);
            bool hasTheRegNumber = false;
            int index = 0;

            if (isFreePlace)
            {

                for (int i = 0; i < LengthOfRows; i++)
                {
                    if (_parkingPlaces[i, 0] != null && _parkingPlaces[i, 1] != null)
                    {
                        if (_parkingPlaces[i, 0].RegNumber == regNumber && _parkingPlaces[i, 1].RegNumber == regNumber)
                        {
                            hasTheRegNumber = true;
                            index = i;
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("Place {0} is not free!!", newPlace);
            }
                            

            if(hasTheRegNumber) MovingToNewPlace(index, newPlace);

        }

        //Moving The Vehicle: MotorC
        public void MovingTheVehicle(string regNumber, byte newPlace, byte newPart)
        {
            bool isFreePlace = IsFreeParkingPlace(newPlace,newPart);
            bool hasTheRegNumber = false;
            byte index = 0;
            byte part = 0;

            if (isFreePlace)
            {
                for (int i = 0; i < LengthOfRows; i++)
                {
                    if (_parkingPlaces[i, 0] != null && _parkingPlaces[i, 1] != null)
                    {
                        if (_parkingPlaces[i, 1] == null && _parkingPlaces[i, 0].RegNumber == regNumber)
                        {

                            //hasTheRegNumber = MovingToNewPlace(i, 0, newPlace, newPart);
                            hasTheRegNumber = true;
                            index = (byte)i;
                            part = 0;

                        }
                        else if (_parkingPlaces[i, 0] == null && _parkingPlaces[i, 1].RegNumber == regNumber)
                        {
                            //hasTheRegNumber = hasTheRegNumber = MovingToNewPlace(i, 1, newPlace, newPart);

                            hasTheRegNumber = true;
                            index = (byte)i;
                            part = 1;
                        }
                    }
                    else if (_parkingPlaces[i, 0] != null && _parkingPlaces[i, 1] == null)
                    {
                        if (_parkingPlaces[i, 0].RegNumber == regNumber)
                        {
                            //hasTheRegNumber = hasTheRegNumber = MovingToNewPlace(i, 0, newPlace, newPart);
                            hasTheRegNumber = true;
                            index = (byte)i;
                            part = 0;
                        }
                    }
                    else if (_parkingPlaces[i, 1] != null && _parkingPlaces[i, 0] == null)
                    {
                        if (_parkingPlaces[i, 1].RegNumber == regNumber)
                        {
                            //hasTheRegNumber = hasTheRegNumber = MovingToNewPlace(i, 0, newPlace, newPart);
                            hasTheRegNumber = true;
                            index = (byte)i;
                            part = 1;
                        }
                    }

                }
            }
            else
            {
                Console.WriteLine("Place {0} part {1} is not free!!", newPlace, newPart);
            }

            if (hasTheRegNumber) MovingToNewPlace(index,part, newPlace,newPart);
        }
    }
}
