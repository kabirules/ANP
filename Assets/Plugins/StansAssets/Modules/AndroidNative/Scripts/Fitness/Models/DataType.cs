////////////////////////////////////////////////////////////////////////////////
//  
// @module Stan's Assets Android Native Fitness
// @author Alexey Yaremenko (Stan's Assets) 
// @support support@stansassets.com
//
////////////////////////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA.Fitness {
	public sealed class DataType {

		/// <summary>
		/// In the com.google.activity.summary data type, each data point represents a summary of all activity segments of a particular activity type over a time interval.
		/// </summary>
		public static DataType AGGREGATE_ACTIVITY_SUMMARY = new DataType("com.google.activity.summary");

		/// <summary>
		/// In the com.google.bmr.summary data type, each data point represents the average, maximum and minimum basal metabolic rate, in kcal per day, over the time interval of the data point.
		/// </summary>
		public static DataType AGGREGATE_BASAL_METABOLIC_RATE_SUMMARY = new DataType("com.google.bmr.summary");

		/// <summary>
		/// In the com.google.body.fat_percentage.summary data type, each data point represents the average, maximum and minimum percentage over the time interval of the data point.
		/// </summary>
		public static DataType AGGREGATE_BODY_FAT_PERCENTAGE_SUMMARY = new DataType("com.google.body.fat_percentage.summary");

		/// <summary>
		/// Aggregate calories expended,in kcal, during a time interval.
		/// </summary>
		public static DataType AGGREGATE_CALORIES_EXPENDED = new DataType("com.google.calories.expended");

		/// <summary>
		/// Aggregate distance, in meters, during a time interval.
		/// </summary>
		public static DataType AGGREGATE_DISTANCE_DELTA = new DataType("com.google.distance.delta");

		/// <summary>
		/// In the com.google.heart_rate.summary data type, each data point represents average, maximum and minimum beats per minute over the time interval of the data point.
		/// </summary>
		public static DataType AGGREGATE_HEART_RATE_SUMMARY = new DataType("com.google.heart_rate.summary");

		/// <summary>
		/// In the com.google.hydration data type, the field_volume in the data point represents the volume, in liters, of water consumed by a user as part of a single drink.
		/// </summary>
		public static DataType AGGREGATE_HYDRATION = new DataType("com.google.hydration");

		/// <summary>
		/// In the com.google.location.bounding_box data type, a data point represents the bounding box computed over user's location data points over a time interval.
		/// </summary>
		public static DataType AGGREGATE_LOCATION_BOUNDING_BOX = new DataType("com.google.location.bounding_box");

		/// <summary>
		/// In the com.google.nutrition.summary data type, each data point represents the sum of all nutrition entries over the time interval of the data point.
		/// </summary>
		public static DataType AGGREGATE_NUTRITION_SUMMARY = new DataType("com.google.nutrition.summary");

		/// <summary>
		/// In the com.google.power.summary data type, each data point represents average, maximum and minimum watts over the time interval of the data point.
		/// </summary>
		public static DataType AGGREGATE_POWER_SUMMARY = new DataType("com.google.power.summary");

		/// <summary>
		/// In the com.google.speed.summary data type, each data point represents the average, maximum and minimum speed over ground, in meters/second, over the time interval of the data point.
		/// </summary>
		public static DataType AGGREGATE_SPEED_SUMMARY = new DataType("com.google.speed.summary");

		/// <summary>
		/// Aggregate number of steps during a time interval.
		/// </summary>
		public static DataType AGGREGATE_STEP_COUNT_DELTA = new DataType("com.google.step_count.delta");

		/// <summary>
		/// In the com.google.weight.summary data type, each data point represents the average, maximum and minimum weight, in kilograms, over the time interval of the data point.
		/// </summary>
		public static DataType AGGREGATE_WEIGHT_SUMMARY = new DataType ("com.google.weight.summary");
		
		/// <summary>
		/// In the com.google.activity.samples data type, each data point represents the instantaneous samples of the current activities.
		/// </summary>
		public static DataType TYPE_ACTIVITY_SAMPLES = new DataType("com.google.activity.samples");

		/// <summary>
		/// In the com.google.activity.segment data type, each data point represents a continuous time interval with a single activity value.
		/// </summary>
		public static DataType TYPE_ACTIVITY_SEGMENT = new DataType ("com.google.activity.segment");

		/// <summary>
		/// In the com.google.calories.bmr data type, each data point represents the basal metabolic rate of energy expenditure at rest of the user at the time of the reading, in kcal per day.
		/// </summary>
		public static DataType TYPE_BASAL_METABOLIC_RATE = new DataType ("com.google.calories.bmr");

		/// <summary>
		/// In the com.google.body.fat.percentage data type, each data point represents a measurement of the total fat mass in a person's body as a percentage of the total body mass.
		/// </summary>
		public static DataType TYPE_BODY_FAT_PERCENTAGE = new DataType ("com.google.body.fat.percentage");

		/// <summary>
		/// In the com.google.calories.expended data type, each data point represents the number of calories expended, in kcal, over the time interval of the data point.
		/// </summary>
		public static DataType TYPE_CALORIES_EXPENDED = new DataType("com.google.calories.expended");

		/// <summary>
		/// In the com.google.cycling.cadence data type, each data point represents an instantaneous measurement of the pedaling rate in crank revolutions per minute.
		/// </summary>
		public static DataType TYPE_CYCLING_PEDALING_CADENCE = new DataType ("com.google.cycling.cadence");

		/// <summary>
		/// In the com.google.cycling.pedaling.cumulative data type, each data point represents the number of rotations taken from the start of the count.
		/// </summary>
		public static DataType TYPE_CYCLING_PEDALING_CUMULATIVE = new DataType("com.google.cycling.pedaling.cumulative");

		/// <summary>
		/// In the com.google.cycling.wheel_revolution.cumulative data type, each data point represents the number of revolutions taken from the start of the count.
		/// </summary>
		public static DataType TYPE_CYCLING_WHEEL_REVOLUTION = new DataType ("com.google.cycling.wheel_revolution.cumulative");

		/// <summary>
		/// In the com.google.cycling.wheel.revolutions data type, each data point represents an instantaneous measurement of the wheel in revolutions per minute.
		/// </summary>
		public static DataType TYPE_CYCLING_WHEEL_RPM = new DataType ("com.google.cycling.wheel.revolutions");

		/// <summary>
		/// In the com.google.distance.delta data type, each data point represents the distance covered, in meters, since the last reading.
		/// </summary>
		public static DataType TYPE_DISTANCE_DELTA = new DataType ("com.google.distance.delta");

		/// <summary>
		/// In the com.google.heart_rate.bpm data type, each data point represents an instantaneous measurement of the heart rate in beats per minute.
		/// </summary>
		public static DataType TYPE_HEART_RATE_BPM = new DataType("com.google.heart_rate.bpm");

		/// <summary>
		/// In the com.google.height data type, each data point represents the height of the user at the time of the reading, in meters.
		/// </summary>
		public static DataType TYPE_HEIGHT = new DataType ("com.google.height");

		/// <summary>
		/// In the com.google.hydration data type, the field_volume in the data point represents the volume, in liters, of water consumed by a user as part of a single drink.
		/// </summary>
		public static DataType TYPE_HYDRATION = new DataType("com.google.hydration");

		/// <summary>
		/// In the com.google.location.sample data type, each data point represents the user's location at a given instant.
		/// </summary>
		public static DataType TYPE_LOCATION_SAMPLE = new DataType ("com.google.location.sample");

		/// <summary>
		/// The com.google.location.track data type represents a location point that is part of a track and which may have inexact timestamps.
		/// </summary>
		public static DataType TYPE_LOCATION_TRACK = new DataType("com.google.location.track");

		/// <summary>
		/// In the com.google.nutrition data type, each data point represents the value of all nutrients consumed as part of a meal or a food item.
		/// </summary>
		public static DataType TYPE_NUTRITION = new DataType("com.google.nutrition");

		/// <summary>
		/// In the com.google.power.sample data type, each data point represents an instantaneous measurement of power in watts.
		/// </summary>
		public static DataType TYPE_POWER_SAMPLE = new DataType ("com.google.power.sample");

		/// <summary>
		/// In the com.google.speed data type, each data point represents the instantaneous speed over ground, in meters/second.
		/// </summary>
		public static DataType TYPE_SPEED = new DataType ("com.google.speed");

		/// <summary>
		/// In the com.google.step_count.cadence data type, each data point represents an instantaneous measurement of the cadence in steps per minute.
		/// </summary>
		public static DataType TYPE_STEP_COUNT_CADENCE = new DataType("com.google.step_count.cadence");

		/// <summary>
		/// In the com.google.step_count.delta data type, each data point represents the number of steps taken since the last reading.
		/// </summary>
		public static DataType TYPE_STEP_COUNT_DELTA = new DataType ("com.google.step_count.delta");

		/// <summary>
		/// In the com.google.weight data type, each data point represents the weight of the user at the time of the reading, in kilograms.
		/// </summary>
		public static DataType TYPE_WEIGHT = new DataType ("com.google.weight");

		/// <summary>
		/// In the com.google.activity.exercise data type, each data point represents a single continuous set of a workout exercise performed by a user.
		/// </summary>
		public static DataType TYPE_WORKOUT_EXERCISE = new DataType ("com.google.activity.exercise");

		private string value = string.Empty;

		private DataType() {
		}

		internal DataType (string dataType) {
			value = dataType;
		}

		public override bool Equals (object obj)
		{
			if (GetType() != obj.GetType()) {
				return false;
			}

			DataType other = obj as DataType;
			return value.Equals (other.Value);
		}

		public override int GetHashCode() {
			return base.GetHashCode ();
		}

		public string Value {
			get {
				return value;
			}
		}
	}
}
