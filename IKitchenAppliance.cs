using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2.ObjektOrienteradProgrammering
{
    public interface IKitchenAppliance
    {
        public int Id { get; set; }
        string Type { get; set; }
        string Brand { get; set; }
        bool IsFunctioning { get; set; }
        string Description { get; }
        void Use();
    }
}
