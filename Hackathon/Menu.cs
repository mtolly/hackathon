using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class Menu
{
    private List<string> MenuItems;
    private int iterator;
    public string InfoText { get; set; }
    public string Title { get; set; }

	public Menu()

	{
        Title = "Pong Clone";
        MenuItems = new List<string>();
        MenuItems.Add("Single Player");
        MenuItems.Add("Multi Player");
        MenuItems.Add("Exit Game");
        Iterator = 0;
        InfoText = string.Empty;
	}

    public int Iterator
    {
        get
        {
            return iterator;
        }
        set
        {
            iterator = value;
            if (iterator > MenuItems.Count - 1) iterator = MenuItems.Count - 1;
            if (iterator < 0) iterator = 0;
        }
    }
 
}
