public class Recipe
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Ingredients { get; set; }
    public string Instructions { get; set; }
    public List<string> Categories { get; set; }

    public Recipe(string title, string ingredients, string instructions, List<string> categories)
    {
        Title = title;
        Ingredients = ingredients;
        Instructions = instructions;
        Categories = categories;
    }
}