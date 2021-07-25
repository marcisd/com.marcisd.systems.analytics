using System;

/*===============================================================
Project:	Analytics
Developer:	Marci San Diego
Company:	Personal - marcisandiego@gmail.com
Date:		24/04/2019 11:23
===============================================================*/

namespace MSD.Systems.Analytics
{
	public partial class AnalyticsEventFormat
	{
		[Serializable]
		private abstract class Validation
		{
			public enum ErrorType
			{
				NoServiceDestination,
				InvalidEventName,
				InvalidEventContext,
				InvalidParameterNameFormat,
			}

			public abstract string Log { get; }

			public abstract bool Test(AnalyticsEventFormat format);

			public static Validation Factory(ErrorType type) => type switch {
				ErrorType.NoServiceDestination => new NoServiceDestination(),
				ErrorType.InvalidEventName => new InvalidEventName(),
				ErrorType.InvalidEventContext => new InvalidEventContext(),
				ErrorType.InvalidParameterNameFormat => new InvalidParameterNameFormat(),
				_ => null,
			};

			public static void ForEachType(Action<ErrorType> actionPerType)
			{
				ErrorType[] types = Enum.GetValues(typeof(ErrorType)) as ErrorType[];
				Array.ForEach(types, actionPerType);
			}
		}

		private class NoServiceDestination : Validation
		{
			public override string Log => "No Service destination assigned.";

			public override bool Test(AnalyticsEventFormat format) => format._serviceDestination == null;
		}

		private class InvalidEventName : Validation
		{
			public override string Log => "Invalid event name.";

			public override bool Test(AnalyticsEventFormat format) => !format._eventContext.IsValidName;
		}

		private class InvalidEventContext : Validation
		{
			public override string Log => "Event context must have exactly one classification and exacly one verb.";

			public override bool Test(AnalyticsEventFormat format) => !format._eventContext.IsValidFormat;
		}

		private class InvalidParameterNameFormat : Validation
		{
			public override string Log => "Invalid parameter name format for one or more parameters";

			public override bool Test(AnalyticsEventFormat format) => !format._parameterFormatter.IsValid;
		}
	}
}
