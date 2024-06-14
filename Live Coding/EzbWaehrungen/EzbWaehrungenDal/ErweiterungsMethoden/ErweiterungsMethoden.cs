using System.Diagnostics.Contracts;

namespace EzbWaehrungenDal.ErweiterungsMethoden;

public static class StringErweiterungen
{
    /// <summary>
    /// Prüft, ob der String numerisch auswertbar ist.
    /// </summary>
    /// <param name="text">Der String.</param>
    /// <returns>True, wenn der String eine Zahl darstellt.</returns>
    public static bool IsNumeric(this string text)
    {
        return double.TryParse(text, out _); // _ ist der sog. "Discard" -> "Lieber Compiler, brauch ich nicht"
    }

}

public static class ListErweiterungen
{

    /// <summary>
    /// Ersetzt ein Element in der Liste durch ein anderes.
    /// </summary>
    /// <typeparam name="T">Der Typ der Liste.</typeparam>
    /// <param name="liste">Die Liste.</param>
    /// <param name="altesElement">Das Element, das ersetzt werden soll.</param>
    /// <param name="neuesElement">Das neue Element, das das alte ersetzen soll.</param>
    /// <remarks>
    /// 
    /// </remarks>
    public static void Replace<T>(this List<T> liste, T altesElement, T neuesElement)
    {

    }

    /// <summary>
    /// Vertauscht die Reihenfolge zweier Elemente in der Liste
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="liste"></param>
    /// <param name="vorgaenger">Das Element, das "nach hinten" soll.</param>
    /// <param name="nachfolger">Das Element, das "nach vorne" soll.</param>
    public static void Exchange<T>(this List<T> liste, T vorgaenger, T nachfolger)
    {

    }

    /// <summary>
    /// Fügt das Element der Liste hinzu, wenn es noch nicht vorhanden ist.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="liste"></param>
    /// <param name="element"></param>
    public static void AddIfNew<T>(this List<T> liste, T element, IEqualityComparer<T> comparer)
    {
        if (!liste.Contains(element, comparer))
        {
            liste.Add(element);
        }
    }
}