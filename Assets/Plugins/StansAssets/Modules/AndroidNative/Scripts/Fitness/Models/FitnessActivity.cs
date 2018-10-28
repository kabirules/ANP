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
	public sealed class Activity {

		public static Activity AEROBICS = 							new Activity ("aerobics");
		public static Activity ARCHERY = 							new Activity ("archery");
		public static Activity BADMINTON = 							new Activity ("badminton");
		public static Activity BASEBALL = 							new Activity ("baseball");
		public static Activity BASKETBALL = 						new Activity ("basketball");
		public static Activity BIATHLON = 							new Activity ("biathlon");
		public static Activity BIKING = 							new Activity ("biking");
		public static Activity BIKING_HAND = 						new Activity ("biking.hand");
		public static Activity BIKING_MOUNTAIN = 					new Activity ("biking.mountain");
		public static Activity BIKING_ROAD = 						new Activity ("biking.road");
		public static Activity BIKING_SPINNING = 					new Activity ("biking.spinning");
		public static Activity BIKING_STATIONARY = 					new Activity ("biking.stationary");
		public static Activity BIKING_UTILITY = 					new Activity ("biking.utility");
		public static Activity BOXING = 							new Activity ("boxing");
		public static Activity CALISTHENICS = 						new Activity ("calisthenics");
		public static Activity CIRCUIT_TRAINING = 					new Activity ("circuit_training");
		public static Activity CRICKET = 							new Activity ("cricket");
		public static Activity CROSSFIT = 							new Activity ("crossfit");
		public static Activity CURLING = 							new Activity ("curling");
		public static Activity DANCING = 							new Activity ("dancing");
		public static Activity DIVING = 							new Activity ("diving");
		public static Activity ELEVATOR = 							new Activity ("elevator");
		public static Activity ELLIPTICAL = 						new Activity ("elliptical");
		public static Activity ERGOMETER = 							new Activity ("ergometer");
		public static Activity ESCALATOR = 							new Activity ("escalator");
		public static Activity FENCING = 							new Activity ("fencing");
		public static Activity FOOTBALL_AMERICAN = 					new Activity ("football.american");
		public static Activity FOOTBALL_AUSTRALIAN = 				new Activity ("football.australian");
		public static Activity FOOTBALL_SOCCER = 					new Activity ("football.soccer");
		public static Activity FRISBEE_DISC = 						new Activity ("frisbee_disc");
		public static Activity GARDENING = 							new Activity ("gardening");
		public static Activity GOLF = 								new Activity ("golf");
		public static Activity GYMNASTICS = 						new Activity ("gymnastics");
		public static Activity HANDBALL = 							new Activity ("handball");
		public static Activity HIGH_INTENSITY_INTERVAL_TRAINING = 	new Activity ("interval_training.high_intensity");
		public static Activity HIKING = 							new Activity ("hiking");
		public static Activity HOCKEY = 							new Activity ("hockey");
		public static Activity HORSEBACK_RIDING = 					new Activity ("horseback_riding");
		public static Activity HOUSEWORK = 							new Activity ("housework");
		public static Activity ICE_SKATING = 						new Activity ("ice_skating");
		public static Activity IN_VEHICLE = 						new Activity ("in_vehicle");
		public static Activity INTERVAL_TRAINING = 					new Activity ("interval_training");
		public static Activity JUMP_ROPE = 							new Activity ("jump_rope");
		public static Activity KAYAKING = 							new Activity ("kayaking");
		public static Activity KETTLEBELL_TRAINING = 				new Activity ("kettlebell_training");
		public static Activity KICK_SCOOTER = 						new Activity ("kick_scooter");
		public static Activity KICKBOXING = 						new Activity ("kickboxing");
		public static Activity KITESURFING = 						new Activity ("kitesurfing");
		public static Activity MARTIAL_ARTS = 						new Activity ("martial_arts");
		public static Activity MEDITATION = 						new Activity ("meditation");
		public static Activity MIXED_MARTIAL_ARTS = 				new Activity ("martial_arts.mixed");
		public static Activity ON_FOOT = 							new Activity ("on_foot");
		public static Activity OTHER = 								new Activity ("other");
		public static Activity P90X = 								new Activity ("p90x");
		public static Activity PARAGLIDING = 						new Activity ("paragliding");
		public static Activity PILATES = 							new Activity ("pilates");
		public static Activity POLO = 								new Activity ("polo");
		public static Activity RACQUETBALL = 						new Activity ("racquetball");
		public static Activity ROCK_CLIMBING = 						new Activity ("rock_climbing");
		public static Activity ROWING = 							new Activity ("rowing");
		public static Activity ROWING_MACHINE = 					new Activity ("rowing.machine");
		public static Activity RUGBY = 								new Activity ("rugby");
		public static Activity RUNNING = 							new Activity ("running");
		public static Activity RUNNING_JOGGING = 					new Activity ("running.jogging");
		public static Activity RUNNING_SAND = 						new Activity ("running.sand");
		public static Activity RUNNING_TREADMILL = 					new Activity ("running.treadmill");
		public static Activity SAILING = 							new Activity ("sailing");
		public static Activity SCUBA_DIVING = 						new Activity ("scuba_diving");
		public static Activity SKATEBOARDING = 						new Activity ("skateboarding");
		public static Activity SKATING = 							new Activity ("skating");
		public static Activity SKATING_CROSS = 						new Activity ("skating.cross");
		public static Activity SKATING_INDOOR = 					new Activity ("skating.indoor");
		public static Activity SKATING_INLINE = 					new Activity ("skating.inline");
		public static Activity SKIING = 							new Activity ("skiing");
		public static Activity SKIING_BACK_COUNTRY = 				new Activity ("skiing.back_country");
		public static Activity SKIING_CROSS_COUNTRY = 				new Activity ("skiing.cross_country");
		public static Activity SKIING_DOWNHILL = 					new Activity ("skiing.downhill");
		public static Activity SKIING_KITE = 						new Activity ("skiing.kite");
		public static Activity SKIING_ROLLER = 						new Activity ("skiing.roller");
		public static Activity SLEDDING = 							new Activity ("sledding");
		public static Activity SLEEP = 								new Activity ("sleep");
		public static Activity SLEEP_LIGHT = 						new Activity ("sleep.light");
		public static Activity SLEEP_DEEP = 						new Activity ("sleep.deep");
		public static Activity SLEEP_REM = 							new Activity ("sleep.rem");
		public static Activity SLEEP_AWAKE = 						new Activity ("sleep.awake");
		public static Activity SNOWBOARDING = 						new Activity ("snowboarding");
		public static Activity SNOWMOBILE = 						new Activity ("snowmobile");
		public static Activity SNOWSHOEING = 						new Activity ("snowshoeing");
		public static Activity SQUASH = 							new Activity ("squash");
		public static Activity STAIR_CLIMBING = 					new Activity ("stair_climbing");
		public static Activity STAIR_CLIMBING_MACHINE = 			new Activity ("stair_climbing.machine");
		public static Activity STANDUP_PADDLEBOARDING = 			new Activity ("standup_paddleboarding");
		public static Activity STILL = 								new Activity ("still");
		public static Activity STRENGTH_TRAINING = 					new Activity ("strength_training");
		public static Activity SURFING = 							new Activity ("surfing");
		public static Activity SWIMMING = 							new Activity ("swimming");
		public static Activity SWIMMING_POOL = 						new Activity ("swimming.pool");
		public static Activity SWIMMING_OPEN_WATER = 				new Activity ("swimming.open_water");
		public static Activity TABLE_TENNIS = 						new Activity ("table_tennis");
		public static Activity TEAM_SPORTS = 						new Activity ("team_sports");
		public static Activity TENNIS = 							new Activity ("tennis");
		public static Activity TILTING = 							new Activity ("tilting");
		public static Activity TREADMILL = 							new Activity ("treadmill");
		public static Activity UNKNOWN = 							new Activity ("unknown");
		public static Activity VOLLEYBALL = 						new Activity ("volleyball");
		public static Activity VOLLEYBALL_BEACH = 					new Activity ("volleyball.beach");
		public static Activity VOLLEYBALL_INDOOR = 					new Activity ("volleyball.indoor");
		public static Activity WAKEBOARDING = 						new Activity ("wakeboarding");
		public static Activity WALKING = 							new Activity ("walking");
		public static Activity WALKING_FITNESS = 					new Activity ("walking.fitness");
		public static Activity WALKING_NORDIC = 					new Activity ("walking.nordic");
		public static Activity WALKING_TREADMILL = 					new Activity ("walking.treadmill");
		public static Activity WALKING_STROLLER = 					new Activity ("walking.stroller");
		public static Activity WATER_POLO = 						new Activity ("water_polo");
		public static Activity WEIGHTLIFTING = 						new Activity ("weightlifting");
		public static Activity WHEELCHAIR = 						new Activity ("wheelchair");
		public static Activity WINDSURFING = 						new Activity ("windsurfing");
		public static Activity YOGA = 								new Activity ("yoga");
		public static Activity ZUMBA = 								new Activity ("zumba");

		private string value = string.Empty;

		private Activity() {
		}

		internal Activity (string alias) {
			value = alias;
		}

		public override bool Equals (object obj)
		{
			if (GetType() != obj.GetType()) {
				return false;
			}

			Activity other = obj as Activity;
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
