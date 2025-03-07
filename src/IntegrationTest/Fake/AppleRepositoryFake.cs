using UnitTests.Apples;

namespace IntegrationTests.Fake;

public class AppleRepositoryFake : IAppleRepository
{
    public List<Apple> GetAllApples()
    {
        return
        [
            new Apple("Fuji", "Red"),
            new Apple("Gala", "Red"),
            new Apple("Granny Smith", "Green"),
            new Apple("Golden Delicious", "Yellow"),
            new Apple("Honeycrisp", "Red"),
            new Apple("Pink Lady", "Pink"),
            new Apple("McIntosh", "Red"),
            new Apple("Braeburn", "Red"),
            new Apple("Jonagold", "Yellow"),
            new Apple("Cortland", "Red"),
        ];
    }
}
