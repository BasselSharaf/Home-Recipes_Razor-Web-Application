using System.Text.Json;
class Data
{
    private List<Recipe> _recipes { get; set; } = new();
    private List<string> _categories { get; set; } = new();
    private string _recipesFilePath;
    private string _categoriesFilePath;

    public Data ()
    {
        var systemPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
        _recipesFilePath = Path.Combine(systemPath, "Recipes.json");
        _categoriesFilePath = Path.Combine(systemPath, "Categories.json");
        if (!File.Exists(this._recipesFilePath))
        {
            _recipes = new List<Recipe>();
            File.WriteAllText(this._recipesFilePath, JsonSerializer.Serialize(_recipes));
        }
        if (!File.Exists(this._categoriesFilePath))
        {
            _recipes = new List<Recipe>();
            File.WriteAllText(this._categoriesFilePath, JsonSerializer.Serialize(_recipes));
        }
        using (StreamReader r = new StreamReader(this._recipesFilePath))
        {
            var data = r.ReadToEnd();
            var json = JsonSerializer.Deserialize<List<Recipe>>(data);
            if (json != null)
                _recipes = json;
        }
        using (StreamReader r = new StreamReader(this._categoriesFilePath))
        {
            var data = r.ReadToEnd();
            var json = JsonSerializer.Deserialize<List<string>>(data);
            if (json != null)
                _categories = json;
        }
    }

    public List<Recipe> GetRecipes()
    {
        return _recipes;
    }

    public void AddRecipe(Recipe r)
    {
        _recipes.Add(r);
    }

    public Recipe getRecipe(Guid id)
    {
        var recipe = _recipes.Find(r => r.Id == id);
        if (recipe == null)
            recipe = new Recipe();
        return recipe;
    }

    public void RemoveRecipe(Guid id)
    {
        var recipe = getRecipe(id); 
        _recipes.Remove(recipe);
    }

    public Recipe EditRecipe(Guid id, Recipe newRecipe)
    {
        var recipe = getRecipe(id);
        recipe.Title = newRecipe.Title;
        recipe.Ingredients = newRecipe.Ingredients;
        recipe.Instructions = newRecipe.Instructions;
        recipe.Categories = newRecipe.Categories;
        return recipe;
    }

    public void EditTitle(Guid id, string newTitle)
    {
        var recipe = getRecipe(id);
        recipe.Title = newTitle;
    }

    public void EditIngredients(Guid id, string newIngredients)
    {
        var recipe = getRecipe(id);
        recipe.Ingredients = newIngredients;
    }

    public void EditInstructions(Guid id, string newInstructions)
    {
        var recipe = getRecipe(id);
        recipe.Instructions = newInstructions;
    }
    
    //TODO: The only way to edit a category is from the original Method that will edit it inside all recipes
    public void EditCategory(Guid id, string category, string newCategory)
    {
        RemoveCategoryFromRecipe(id, category);
        AddCategoryToRecipe(id, newCategory);
    }

    public List<string> GetAllCategories()
    {
        return _categories;
    }

    public void AddCategory(string category)
    {
        if(!_categories.Contains(category))
            _categories.Add(category);
    }

    public void EditCategory(string category, string newCategory)
    {
        var index = _categories.IndexOf(category);
        if (index != -1)
        {
            _categories[index] = newCategory;
            _recipes.ForEach(r => r.Categories.ForEach((c) =>
            {
                if (c == category)
                    c = newCategory;
            }));
        }
    }

    public void RemoveCategory(string category)
    {
        if (_categories.Contains(category))
        {
            _categories.Remove(category);
            _recipes.Where(r => r.Categories.Contains(category)).ToList().ForEach(r => r.Categories.Remove(category));
        }
    }

    public void AddCategoryToRecipe(Guid id, string category)
    {
        if (_categories.Contains(category))
        {
            var recipe = getRecipe(id);
            recipe.Categories.Add(category);
        }
    }

    public void RemoveCategoryFromRecipe(Guid id, string category)
    {
        var recipe = getRecipe(id);
        recipe.Categories.Remove(category);
    }

    public void SaveData()
    {
        File.WriteAllText(_recipesFilePath, JsonSerializer.Serialize(_recipes));
        File.WriteAllText(_categoriesFilePath, JsonSerializer.Serialize(_categories));
    }
}