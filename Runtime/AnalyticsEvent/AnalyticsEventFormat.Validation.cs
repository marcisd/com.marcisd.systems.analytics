using System;

/*===============================================================
Project:	Analytics
Developer:	Marci San Diego
Company:	DefaultCompany - marcianosd@dm-ed.com
Date:		24/04/2019 11:23
===============================================================*/

namespace DMED.Systems.AnalyticsSystem
{
	public partial class AnalyticsEventFormat
	{
		[Serializable]
		public abstract class Validation
		{
			public enum Type
			{
				NoServiceDestination,
				InvalidEventName,
				InvalidEventNameFormat,
				InvalidParameterNameFormat,
			}

			public abstract string log { get; }

			public abstract bool Test(AnalyticsEventFormat format);

			public static Validation Factory(Type type)
			{
				switch (type) {
					case Type.NoServiceDestination: return new NoServiceDestination();
					case Type.InvalidEventName: return new InvalidEventName();
					case Type.InvalidEventNameFormat: return new InvalidEventNameFormat();
					case Type.InvalidParameterNameFormat: return new InvalidParameterNameFormat();
					default: return null;
				}
			}

			public static void ForEachType(Action<Type> actionPerType)
			{
				var types = Enum.GetValues(typeof(Type)) as Type[];
				Array.ForEach(types, actionPerType);
			}
		}

		public class NoServiceDestination : Validation
		{
			public override string log => "No Service destination assigned.";

			public override bool Test(AnalyticsEventFormat format) => format._serviceDestination == null;
		}

		public class InvalidEventName : Validation
		{
			public override string log => "Invalid event name.";

			public override bool Test(AnalyticsEventFormat format) => !format._nameFormatter.isValidName;
		}

		public class InvalidEventNameFormat : Validation
		{
			public override string log => "Event name format must have exactly one classification and exacly one verb.";

			public override bool Test(AnalyticsEventFormat format) => !format._nameFormatter.isValidFormat;
		}

		public class InvalidParameterNameFormat : Validation
		{
			public override string log => "Invalid parameter name format for one or more parameters";

			public override bool Test(AnalyticsEventFormat format) => !format._parameterFormatter.isValid;
		}
	}
}
