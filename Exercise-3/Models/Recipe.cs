public class Recipe
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Ingredients { get; set; }
    public string Instructions { get; set; }
    public List<string> Categories { get; set; } = new();

    public Recipe()
    {
        Id = Guid.NewGuid();
        Title = "";
        Ingredients = "";
        Instructions = "";
    }
    public Recipe(string title, string ingredients, string instructions, List<string> categories)
    {
        Id = Guid.NewGuid();
        Title = title;
        Ingredients = ingredients;
        Instructions = instructions;
        Categories = categories;
    }
}