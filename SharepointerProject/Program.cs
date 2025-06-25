namespace SharepointerProject
{
    using System;
    using System.Collections.Generic;

    class Program
    {
        static void Main(string[] args)
        {
            // Replace with your SharePoint site URL, username, and password
            string siteUrl = "https://yourtenant.sharepoint.com/sites/yoursite";
            string username = "yourusername@yourtenant.onmicrosoft.com";
            string password = "yourpassword";

            // Create an instance of SharePointService
            SharePointService sharePointService = new SharePointService(siteUrl, username, password);

            // Example: Create a new item
            var newItemValues = new Dictionary<string, object>
            {
                { "Title", "New Item Title" },
                { "Description", "This is a description of the new item." }
            };
            sharePointService.Create("YourListName", newItemValues);
            Console.WriteLine("Item created successfully.");

            // Example: Read items
            var items = sharePointService.Read("YourListName");
            Console.WriteLine("Items in the list:");
            foreach (var item in items)
            {
                Console.WriteLine($"- {item["Title"]}: {item["Description"]}");
            }

            // Example: Update an item (assuming you know the ID of the item to update)
            int itemIdToUpdate = 1; // Replace with the actual ID of the item you want to update
            var updatedValues = new Dictionary<string, object>
            {
                { "Title", "Updated Item Title" },
                { "Description", "This is an updated description." }
            };
            sharePointService.Update("YourListName", itemIdToUpdate, updatedValues);
            Console.WriteLine("Item updated successfully.");

            // Example: Delete an item (assuming you know the ID of the item to delete)
            int itemIdToDelete = 1; // Replace with the actual ID of the item you want to delete
            sharePointService.Delete("YourListName", itemIdToDelete);
            Console.WriteLine("Item deleted successfully.");
        }
    }
}
