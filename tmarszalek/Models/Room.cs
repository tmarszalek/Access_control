using System;
namespace tmarszalek.Models
{
    public class Room
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public int Places { get; set; }
        public string Hours { get; set; }
        public int Free { get; set; }
    }
}
