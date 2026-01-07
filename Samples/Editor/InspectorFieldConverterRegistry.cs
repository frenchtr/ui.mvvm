using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace TravisRFrench.UI.MVVM.Samples.Editor
{
	public static class InspectorFieldConverterRegistry
	{
		private static List<IInspectorFieldConverter>? converters;

		public static IInspectorFieldConverter? GetConverter(Type valueType)
		{
			EnsureLoaded();
			return converters!.FirstOrDefault(c => c.CanConvert(valueType));
		}

		private static void EnsureLoaded()
		{
			if (converters != null)
			{
				return;
			}

			converters = new List<IInspectorFieldConverter>();

			var types = AppDomain.CurrentDomain.GetAssemblies()
				.SelectMany(SafeGetTypes)
				.Where(t =>
					t is { IsAbstract: false, IsInterface: false } &&
					typeof(IInspectorFieldConverter).IsAssignableFrom(t) &&
					t.GetConstructor(Type.EmptyTypes) != null);

			foreach (var t in types)
			{
				try
				{
					converters.Add((IInspectorFieldConverter)Activator.CreateInstance(t)!);
				}
				catch
				{
					// ignore bad converters
				}
			}

			converters = converters.OrderBy(c => c.GetType().Name).ToList();
		}

		private static IEnumerable<Type> SafeGetTypes(Assembly a)
		{
			try { return a.GetTypes(); }
			catch (ReflectionTypeLoadException e) { return e.Types.Where(x => x != null)!; }
		}
	}
}