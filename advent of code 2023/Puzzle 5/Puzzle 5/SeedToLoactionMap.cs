using System;

namespace Puzzle_5
{
    internal class SeedToLoactionMap
    {
        // Define a nested class for linked list nodes
        private class MapNode
        {
            public long Value { get; set; } // Remove ? for non-nullable
            public MapNode Next { get; set; }
        }

        // Define the first node of the linked list
        private MapNode seedNode = new MapNode();

        // Constructor to initialize the linked list
        public SeedToLoactionMap()
        {
            // Connect the properties in the desired order
            seedNode.Next = new MapNode(); // Seed -> Soil
            seedNode.Next.Next = new MapNode(); // Soil -> Fertilizer
            seedNode.Next.Next.Next = new MapNode(); // Fertilizer -> Water
            seedNode.Next.Next.Next.Next = new MapNode(); // Water -> Light
            seedNode.Next.Next.Next.Next.Next = new MapNode(); // Light -> Temperature
            seedNode.Next.Next.Next.Next.Next.Next = new MapNode(); // Temperature -> Humidity
            seedNode.Next.Next.Next.Next.Next.Next.Next = new MapNode(); // Humidity -> Location
        }

        // Properties to access the linked list nodes
        public long Seed
        {
            get => seedNode.Value;
            set => seedNode.Value = value;
        }

        public long Soil
        {
            get => seedNode.Next?.Value;
            set => seedNode.Next.Value = value;
        }

        public long Fertilizer
        {
            get => seedNode.Next.Next.Value;
            set => seedNode.Next.Next.Value = value;
        }

        public long Water
        {
            get => seedNode.Next.Next.Next.Value;
            set => seedNode.Next.Next.Next.Value = value;
        }

        public long Light
        {
            get => seedNode.Next.Next.Next.Next.Value;
            set => seedNode.Next.Next.Next.Next.Value = value;
        }

        public long Temperature
        {
            get => seedNode.Next.Next.Next.Next.Next.Value;
            set => seedNode.Next.Next.Next.Next.Next.Value = value;
        }

        public long Humidity
        {
            get => seedNode.Next.Next.Next.Next.Next.Next.Value;
            set => seedNode.Next.Next.Next.Next.Next.Next.Value = value;
        }

        public long Location
        {
            get => seedNode.Next.Next.Next.Next.Next.Next.Next.Value;
            set => seedNode.Next.Next.Next.Next.Next.Next.Next.Value = value;
        }
    }
}
