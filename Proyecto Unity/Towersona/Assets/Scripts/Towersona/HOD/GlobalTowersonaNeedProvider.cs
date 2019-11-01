using System.Collections.Generic;

//Sé que esto parece horrible... Pero hay varias mecánicas que afectan a todas las towersonas. Prometo no abusar de esta lista. -Aitor
public static class GlobalTowersonaNeedProvider
{
    private static List<TowersonaNeeds> allExisting = new List<TowersonaNeeds>();

    public static IReadOnlyList<TowersonaNeeds> GetAll()
    {
        return allExisting.AsReadOnly();
    }

    /// <summary>
    /// Whenever a TowersonaNeeds is created or destroyed, the event is raised with the new amount of TowersonaNeeds.
    /// </summary>
    public static event System.Action<int> OnNumberChanged;

    public static void Register(TowersonaNeeds needs)
    {
        allExisting.Add(needs);
        OnNumberChanged?.Invoke(allExisting.Count);
    }

    public static void Unregister(TowersonaNeeds needs)
    {
        allExisting.Remove(needs);
        OnNumberChanged?.Invoke(allExisting.Count);
    }
}
