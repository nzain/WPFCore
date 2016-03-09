using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace WPFCore
{
    /// <summary>Extension methods for DependencyObject.</summary>
    public static class DependencyObjectExtensions
    {
        /// <summary>Enumerates all visual children of this dependency object using the visual tree helper.</summary>
        /// <param name="parent">[this] the class to execute the extension method on.</param>
        /// <returns>All visual children.</returns>
        public static IEnumerable<DependencyObject> EnumerateAllVisualChildren(this DependencyObject parent)
        {
            if (parent == null)
            {
                yield break;
            }
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(parent, i);
                if (child != null)
                {
                    yield return child;
                    // recursion
                    foreach (DependencyObject subChild in EnumerateAllVisualChildren(child))
                    {
                        yield return subChild;
                    }
                }
            }
        }

        /// <summary>Enumerates all logical children of this dependency object using the logical tree helper.</summary>
        /// <param name="parent">[this] the class to execute the extension method on.</param>
        /// <returns>All logical children.</returns>
        public static IEnumerable<DependencyObject> EnumerateAllLogicalChildren(this DependencyObject parent)
        {
            if (parent == null)
            {
                yield break;
            }
            foreach (DependencyObject child in LogicalTreeHelper.GetChildren(parent).OfType<DependencyObject>())
            {
                yield return child;
                // recursion
                foreach (DependencyObject subChild in EnumerateAllLogicalChildren(child))
                {
                    yield return subChild;
                }
            }
        }
    }
}