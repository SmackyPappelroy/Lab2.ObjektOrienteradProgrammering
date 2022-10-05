namespace Lab2.ObjektOrienteradProgrammering
{
    class Program
    {
        // Flag for quitting application
        private static bool quitApplication = false;

        // Readonly filepath
        private static string filePath => Directory.GetCurrentDirectory() + @"\KitchenAppliances.json";

        // List of all the appliances in the kitchen
        public static List<KitchenAppliance> Appliances = new();

        /// <summary>
        /// Main entry point for the application
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            LoadFromFile();
            // If file not found or no appliances in the file, add some default appliances
            if (Appliances == null || Appliances.Count == 0)
            {
                InitializeList();
            }

            while (!quitApplication)
            {
                var choice = DisplayMainMenu();

                switch (choice)
                {
                    case Choice.NoChoice:
                        break;
                    case Choice.UseKitchenAppliance:
                        UseKitchenAppliance();
                        break;
                    case Choice.AddKitchenAppliance:
                        AddKitchenAppliance();
                        break;
                    case Choice.ListKitchenAppliances:
                        ListKitchenAppliances();
                        break;
                    case Choice.RemoveKitchenAppliance:
                        RemoveKitchenAppliance();
                        break;
                    case Choice.Quit:
                        quitApplication = true;
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// Display main menu selection screen
        /// </summary>
        /// <returns>The user choice</returns>
        private static Choice DisplayMainMenu()
        {
            bool itemsInList = Appliances != null && Appliances.Count > 0;

            var sb = new System.Text.StringBuilder();

            //Console.WriteLine("");
            Console.WriteLine("========KÖKET========");
            Console.WriteLine("1. Lägg till köksapparat");
            if (itemsInList)
            {
                Console.WriteLine("2. Använd köksapparat");
                Console.WriteLine("3. Lista köksapparater");
                Console.WriteLine("4. Ta bort köksapparat");
            }
            Console.WriteLine("5. Avsluta");
            Console.WriteLine("Ange val:");

            Choice choice = Choice.NoChoice;
            while (choice == Choice.NoChoice)
            {
                // The user input
                string input = Console.ReadLine();

                // Try parsing the user input to an enum
                if (Enum.TryParse(input, out choice))
                {
                    // If the user input is not contained in the enum or user entered 0...
                    if (choice == Choice.NoChoice || !Enum.IsDefined(typeof(Choice), choice))
                    {
                        "Felaktigt val, försök igen.".ToConsole(Status.Error);
                        continue;
                    }
                }
                else
                {
                    // Enum parsing f
                    "Felaktigt val, försök igen.".ToConsole(Status.Error);
                    continue;
                }
            }
            return choice;
        }

        /// <summary>
        /// Removes a <see cref="KitchenAppliance"/> from the list
        /// </summary>
        private static void RemoveKitchenAppliance()
        {
            Console.Clear();

            // Enumerate all items in the list and write to the console
            Appliances.ForEach(x => Console.WriteLine(x.ListString));
            while (true)
            {
                Console.WriteLine();
                Console.Write("Ange id på köksapparat som ska tas bort: ");
                var input = Console.ReadLine();

                // Try parsing the user input to an int
                if (int.TryParse(input, out int id))
                {
                    // Is the id in the list?
                    var appliance = Appliances.FirstOrDefault(x => x.Id == id);
                    if (appliance != null)
                    {
                        Appliances.Remove(appliance);
                        Console.Clear();
                        Console.WriteLine($"{appliance.Description} borttagen.");
                        Console.WriteLine();
                        SaveToFile();
                        break;
                    }
                    else
                    {
                        "Köksapparat med angivet id hittades inte."
                            .ToConsole(Status.Error);
                        continue;
                    }
                }
                // Unable to parse
                else
                {
                    "Felaktigt id, försök igen."
                        .ToConsole(Status.Error);
                    continue;
                }
            }
        }

        /// <summary>
        /// Use a <see cref="KitchenAppliance"/> that is added to the list
        /// </summary>
        private static void UseKitchenAppliance()
        {
            Console.Clear();
            ListKitchenAppliances();
            Console.Write("Ange id på köksapparat som ska användas: ");
            var input = Console.ReadLine();
            while (true)
            {
                // Try parsing the user input to an int
                if (int.TryParse(input, out int id))
                {
                    // Find the id in the list...
                    var appliance = Appliances.FirstOrDefault(x => x.Id == id);
                    if (appliance != null)
                    {
                        // If found use the item
                        appliance.Use();
                        break;
                    }
                    else
                    {
                        continue;
                    }
                }
                else
                {
                    "Felaktigt id, försök igen."
                        .ToConsole(Status.Error);
                    continue;
                }
            }
        }

        /// <summary>
        /// Lists all added <see cref="KitchenAppliance"/>
        /// </summary>
        private static void ListKitchenAppliances()
        {
            Console.Clear();
            Appliances.ForEach(x => Console.WriteLine(x));
        }

        /// <summary>
        /// Adds an item to the list of kitchen appliances
        /// </summary>
        private static void AddKitchenAppliance()
        {
            Console.Clear();
            string appliance = string.Empty;
            string brand = string.Empty;
            bool? isFunctioning = null;
            // Loop until the user enters a valid input
            while (appliance == string.Empty)
            {
                Console.WriteLine("Vilken typ av köksapparat vill du lägga till?");
                appliance = Console.ReadLine();
                // If the user enters nothing, write an error message
                if (appliance == string.Empty)
                {
                    Console.WriteLine("Du måste ange en typ av köksapparat.");
                    continue;
                }
            }
            while (brand == string.Empty)
            {
                Console.WriteLine("Vilket märke är köksapparaten?");
                brand = Console.ReadLine();
                // If the user enters nothing, write an error message
                if (brand == string.Empty)
                {
                    Console.WriteLine("Du måste ange ett märke.");
                    continue;
                }
            }
            while (isFunctioning == null)
            {
                Console.WriteLine("Fungerar köksapparaten? (j/n)");
                string input = Console.ReadLine();
                if (input == "j")
                {
                    isFunctioning = true;
                }
                else if (input == "n")
                {
                    isFunctioning = false;
                }
                // The user didn't enter a lower case j or n, display error message
                else
                {
                    Console.WriteLine("Du måste ange j eller n.");
                    continue;
                }
            }
            var newAppliance = new KitchenAppliance
            {
                // Increment the id by 1
                Id = Appliances.Count + 1,
                Type = appliance,
                Brand = brand,
                IsFunctioning = isFunctioning.Value
            };
            // If the item doesn't exist in the list, add it to the list
            if (!Appliances.Any(x => x.Equals(newAppliance)))
            {
                Appliances.Add(newAppliance);
                SaveToFile();
                Console.Clear();
                $"{newAppliance.Description} har lagts till.".ToConsole(Status.OK);
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Initalize the list with default values
        /// </summary>
        private static void InitializeList()
        {
            Appliances = new()
            {
                new KitchenAppliance("Mixer", "Braun", true, 0),
                new KitchenAppliance("Juicemaskin", "Electrolux", false, 1),
                new KitchenAppliance("Blandare", "Siemens", true, 2),
                new KitchenAppliance("Kaffemaskin", "Jura", true, 3),
                new KitchenAppliance("Air fryer", "Bosch", true, 4),
            };
            SaveToFile();
        }

        /// <summary>
        /// Loads the saved kitchen appliances from a file.
        /// </summary>
        /// <returns></returns>
        private static void LoadFromFile()
        {
            List<KitchenAppliance>? appliances = new List<KitchenAppliance>();
            // If the file exists...
            if (File.Exists(filePath))
            {
                try
                {
                    // Read the whole contents in the file
                    var json = File.ReadAllText(filePath);

                    // Deserialize the contents to a List of itchen applicances
                    appliances = System.Text.Json.JsonSerializer.Deserialize<List<KitchenAppliance>>(json);
                    Appliances = appliances ?? new();
                }
                // Unable to read the file or unable to deserialize the file
                catch (Exception ex)
                {
                    Console.WriteLine("Kunde inte läsa in filen eller deserialisera innehållet.");
                    Console.WriteLine(ex.Message);
                }
            }
        }


        /// <summary>
        /// Saves the list of kitchen appliances to a file
        /// </summary>
        private static void SaveToFile()
        {
            try
            {
                // Serialize the list of kitchen appliances to a JSON string
                var json = System.Text.Json.JsonSerializer.Serialize(Appliances);

                // Write the string to the file
                File.WriteAllText(filePath, json);
            }
            // Unable to serialize the string or unable to write to the file
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}

