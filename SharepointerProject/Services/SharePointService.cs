using PnP.Framework;
using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;

public class SharePointService
{
    private readonly string siteUrl;
    private readonly string username;
    private readonly string password;

    public SharePointService(string siteUrl, string username, string password)
    {
        this.siteUrl = siteUrl;
        this.username = username;
        this.password = password;
    }

    // Create
    public void Create(string listName, Dictionary<string, object> values)
    {
        using (var context = new AuthenticationManager().GetSharePointOnlineAuthenticatedContextTenant(siteUrl, username, password))
        {
            var list = context.Web.Lists.GetByTitle(listName);
            var itemCreationInfo = new ListItemCreationInformation();
            var newItem = list.AddItem(itemCreationInfo);

            foreach (var kvp in values)
            {
                newItem[kvp.Key] = kvp.Value;
            }

            newItem.Update();
            context.ExecuteQuery();
        }
    }

    // Read
    public List<Dictionary<string, object>> Read(string listName)
    {
        var items = new List<Dictionary<string, object>>();

        using (var context = new AuthenticationManager().GetSharePointOnlineAuthenticatedContextTenant(siteUrl, username, password))
        {
            var list = context.Web.Lists.GetByTitle(listName);
            CamlQuery query = CamlQuery.CreateAllItemsQuery();
            var listItems = list.GetItems(query);
            context.Load(listItems);
            context.ExecuteQuery();

            foreach (var listItem in listItems)
            {
                var itemValues = new Dictionary<string, object>();
                foreach (var field in listItem.FieldValues)
                {
                    itemValues[field.Key] = field.Value;
                }
                items.Add(itemValues);
            }
        }

        return items;
    }

    // Update
    public void Update(string listName, int id, Dictionary<string, object> values)
    {
        using (var context = new AuthenticationManager().GetSharePointOnlineAuthenticatedContextTenant(siteUrl, username, password))
        {
            var list = context.Web.Lists.GetByTitle(listName);
            var listItem = list.GetItemById(id);
            context.Load(listItem);
            context.ExecuteQuery();

            foreach (var kvp in values)
            {
                listItem[kvp.Key] = kvp.Value;
            }

            listItem.Update();
            context.ExecuteQuery();
        }
    }

    // Delete
    public void Delete(string listName, int id)
    {
        using (var context = new AuthenticationManager().GetSharePointOnlineAuthenticatedContextTenant(siteUrl, username, password))
        {
            var list = context.Web.Lists.GetByTitle(listName);
            var listItem = list.GetItemById(id);
            listItem.DeleteObject();
            context.ExecuteQuery();
        }
    }
}
