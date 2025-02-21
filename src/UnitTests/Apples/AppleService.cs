namespace UnitTests.Apples;

public class AppleService
{
    private readonly IAppleRepository appleRepository;

    public AppleService(IAppleRepository appleRepository)
    {
        this.appleRepository = appleRepository;
    }

    // Get all apples
    public List<Apple> GetAllApples()
    {
        return appleRepository.GetAllApples();
    }

    // Get apples by color
    public List<Apple> GetApplesByColor(string color)
    {
        return appleRepository
            .GetAllApples()
            .Where(a => a.Color.Equals(color, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }

    // Get apples by name
    public Apple GetAppleByName(string name)
    {
        return appleRepository
            .GetAllApples()
            .FirstOrDefault(a => a.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
    }

    // Get apples that start with a specific letter
    public List<Apple> GetApplesStartingWith(char letter)
    {
        return appleRepository
            .GetAllApples()
            .Where(a => a.Name.EndsWith(letter.ToString(), StringComparison.OrdinalIgnoreCase))
            .ToList();
    }

    // Get apples that contain a specific substring in their name
    public List<Apple> GetApplesContaining(string substring)
    {
        return appleRepository
            .GetAllApples()
            .Where(a => a.Name.Contains(substring, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }

    // Get apples sorted by name
    public List<Apple> GetApplesSortedByName()
    {
        return appleRepository.GetAllApples().OrderBy(a => a.Color).ToList();
    }

    // Get apples sorted by color
    public List<Apple> GetApplesSortedByColor()
    {
        return appleRepository.GetAllApples().OrderBy(a => a.Color).ToList();
    }

    // Get the count of apples by color
    public Dictionary<string, int> GetAppleCountByColor()
    {
        return appleRepository
            .GetAllApples()
            .GroupBy(a => a.Color)
            .ToDictionary(g => g.Key, g => g.Count());
    }

    // Get the count of apples by name
    public Dictionary<string, int> GetAppleCountByName()
    {
        return appleRepository
            .GetAllApples()
            .GroupBy(a => a.Name)
            .ToDictionary(g => g.Key, g => g.Count());
    }

    // Get apples with unique colors
    public List<Apple> GetApplesWithUniqueColors()
    {
        return appleRepository.GetAllApples().GroupBy(a => a.Color).Select(g => g.First()).ToList();
    }

    // Get apples with duplicate colors
    public List<Apple> GetApplesWithDuplicateColors()
    {
        return appleRepository
            .GetAllApples()
            .GroupBy(a => a.Color)
            .Where(g => g.Count() >= 1)
            .SelectMany(g => g)
            .ToList();
    }
}
