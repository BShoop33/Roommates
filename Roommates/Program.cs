using Roommates.Models;
using Roommates.Repositories;
using System;
using System.Collections.Generic;

namespace Roommates
{
    class Program
    {
        //  This is the address of the database.
        //  We define it here as a constant since it will never change.
        private const string CONNECTION_STRING = @"server=localhost\SQLExpress;database=Roommates;integrated security=true";

        static void Main(string[] args)
        {
            RoomRepository roomRepo = new RoomRepository(CONNECTION_STRING);
            ChoreRepository choreRepo = new ChoreRepository(CONNECTION_STRING);
            RoommateRepository roommateRepo = new RoommateRepository(CONNECTION_STRING);

            bool runProgram = true;
            while (runProgram)
            {
                string selection = GetMenuSelection();

                switch (selection)
                {
                    case ("Show all rooms"):
                        List<Room> rooms = roomRepo.GetAll();
                        foreach (Room r in rooms)
                        {
                            Console.WriteLine($"{r.Id} - {r.Name} Max Occupancy({r.MaxOccupancy})");
                        }
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;
                    case ("Search for room"):
                        Console.Write("Room Id: ");
                        int id = int.Parse(Console.ReadLine());

                        Room room = roomRepo.GetById(id);

                        Console.WriteLine($"{room.Id} - {room.Name} Max Occupancy({room.MaxOccupancy})");
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;
                    case ("Add a room"):
                        Console.Write("Room name: ");
                        string name = Console.ReadLine();

                        Console.Write("Max occupancy: ");
                        int max = int.Parse(Console.ReadLine());

                        Room roomToAdd = new Room()
                        {
                            Name = name,
                            MaxOccupancy = max
                        };

                        roomRepo.Insert(roomToAdd);

                        Console.WriteLine($"{roomToAdd.Name} has been added and assigned an Id of {roomToAdd.Id}");
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;
                    case ("Show all chores"):
                        List<RoommateChore> chores = choreRepo.GetAll();
                        foreach (RoommateChore c in chores)
                        {
                            Console.WriteLine($"{c.Id} - RoommateID:  {c.RoommateId}, ChoreId: {c.ChoreId}");
                        }
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;
                    case ("Search for a chore"):
                        Console.Write("Chore Id: ");
                        int ident = int.Parse(Console.ReadLine());

                        RoommateChore chore = choreRepo.GetById(ident);

                        Console.WriteLine($"{chore.Id} - RoommateID:  {chore.RoommateId}, ChoreId: {chore.ChoreId}");
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;
                    case ("Add a chore"):
                        Console.Write("Roommate Id: ");
                        int roommateId = int.Parse(Console.ReadLine());

                        Console.Write("Chore Id: ");
                        int choreId = int.Parse(Console.ReadLine());

                        RoommateChore choreToAdd = new RoommateChore()
                        {
                            RoommateId = roommateId,
                            ChoreId = choreId
                        };

                        choreRepo.Insert(choreToAdd);

                        Console.WriteLine($"{choreToAdd.RoommateId} and {choreToAdd.ChoreId} have been added and assigned an Id of {choreToAdd.Id}");
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;

                    case ("Search for Roommate"):
                        Console.Write("Room Id: ");
                        int identity = int.Parse(Console.ReadLine());

                        Roommate roommate = roommateRepo.GetById(identity);

                        Console.WriteLine(  @$"{roommate.Id}
                                            Roommate Name:  {roommate.Firstname} {roommate.Lastname}
                                            Rent Portion:  {roommate.RentPortion}
                                            Room:  {roommate.Room}");
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;


                    case ("Exit"):
                        runProgram = false;
                        break;
                }
            }
        }

        static string GetMenuSelection()
        {
            Console.Clear();

            List<string> options = new List<string>()
            {
                "Show all rooms",
                "Search for room",
                "Add a room",
                "Show all chores",
                "Search for a chore",
                "Add a chore",
                "Search for Roommate",
                "Exit"
            };

            for (int i = 0; i < options.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {options[i]}");
            }

            while (true)
            {
                try
                {
                    Console.WriteLine();
                    Console.Write("Select an option > ");

                    string input = Console.ReadLine();
                    int index = int.Parse(input) - 1;
                    return options[index];
                }
                catch (Exception)
                {

                    continue;
                }
            }
        }
    }
}