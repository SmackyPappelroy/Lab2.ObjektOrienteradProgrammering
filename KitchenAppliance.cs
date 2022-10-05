using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2.ObjektOrienteradProgrammering
{
    internal class KitchenAppliance : Machine, IKitchenAppliance
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Brand { get; set; }
        public bool IsFunctioning { get; set; }
        public string Description => $"{Type} [{Brand}]";
        public string ListString => $"{Id}. {Type}";

        // Default Constructor
        public KitchenAppliance() { }

        // Constructor
        public KitchenAppliance(string type, string brand, bool isFunctioning, int lastIdNr)
        {
            Type = type;
            Brand = brand;
            IsFunctioning = isFunctioning;
            Id = lastIdNr + 1;
        }

        /// <summary>
        /// Uses the <see cref="KitchenAppliance"/>
        /// </summary>
        public override void Use()
        {
            if (IsFunctioning)
            {
                Console.Clear();
                $"Använder {Type} från {Brand}"
                    .ToConsole(Status.OK);
                Console.WriteLine();
            }
            else
            {
                Console.Clear();
                $"{Type} från {Brand} är trasig"
                    .ToConsole(Status.Error);
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Compare a <see cref="KitchenAppliance"/> object to another <see cref="KitchenAppliance"/> object
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object? obj)
        {
            var appliance = obj as KitchenAppliance;
            if (Type.Equals(appliance.Type) && Brand.Equals(appliance.Brand) && IsFunctioning == appliance.IsFunctioning)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Copied from internet. Not sure what it does. To remove warning from Equals method.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return Type.GetHashCode() ^ Brand.GetHashCode() ^ IsFunctioning.GetHashCode();
        }

        /// <summary>
        /// Returns a string representation of the <see cref="KitchenAppliance"/>
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var returnString = new StringBuilder();
            var functioningString = IsFunctioning ? "Ja" : "Nej";
            returnString.AppendLine($"Id: {Id}");
            returnString.AppendLine($"Typ: {Type}");
            returnString.AppendLine($"Märke: {Brand}");
            returnString.AppendLine($"Fungerar: {functioningString}");
            return returnString.ToString();
        }
    }
}
