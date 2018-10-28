////////////////////////////////////////////////////////////////////////////////
//  
// @module Android Native Plugin for Unity3D 
// @author Alexey Yaremenko (Stan's Assets) 
// @support stans.assets@gmail.com 
//
////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System;
using System.Collections.Generic;

public class GoogleFitExample : AndroidNativeExampleBase {	
	
	public GameObject avatar;
	public Texture2D pieIcon;
	
	public DefaultPreviewButton connectButton;
	public DefaultPreviewButton scoreSubmit;
	public SA_Label playerLabel;
	
	public DefaultPreviewButton[] ConnectionDependedntButtons;	
	
	public SA_Label a_id;
	public SA_Label a_name;
	public SA_Label a_descr;
	public SA_Label a_type;
	public SA_Label a_state;
	public SA_Label a_steps;
	public SA_Label a_total;	
	
	public SA_Label b_id;
	public SA_Label b_name;
	public SA_Label b_all_time;	

	private List<SA.Fitness.DataSource> dataSources = new List<SA.Fitness.DataSource>();
	private const string SESSION_ID = "9a4104ae-3e43-stepcounter";
	
	void Start() {
		
		playerLabel.text = "Player Disconnected";
		UpdateButtons ();

	}

	private void UpdateButtons() {
		if (SA.Fitness.Connection.Instance.ConnectionState == SA.Fitness.ConnectionState.CONNECTED) {			
			foreach (DefaultPreviewButton btn in ConnectionDependedntButtons) {
				btn.EnabledButton ();
			}			
		} else {
			foreach (DefaultPreviewButton btn in ConnectionDependedntButtons) {
				btn.DisabledButton ();				
			}
		}
	}
	
	private void ConncetButtonPress() {
		SA.Fitness.Connection.Instance.AddApi (SA.Fitness.LoginApi.SENSORS_API);
		SA.Fitness.Connection.Instance.AddApi (SA.Fitness.LoginApi.RECORDING_API);
		SA.Fitness.Connection.Instance.AddApi (SA.Fitness.LoginApi.SESSIONS_API);
		SA.Fitness.Connection.Instance.AddApi (SA.Fitness.LoginApi.HISTORY_API);
		SA.Fitness.Connection.Instance.AddScope (SA.Fitness.LoginScope.SCOPE_LOCATION_READ);
		SA.Fitness.Connection.Instance.AddScope (SA.Fitness.LoginScope.SCOPE_LOCATION_READ_WRITE);
		SA.Fitness.Connection.Instance.AddScope (SA.Fitness.LoginScope.SCOPE_ACTIVITY_READ);
		SA.Fitness.Connection.Instance.AddScope (SA.Fitness.LoginScope.SCOPE_ACTIVITY_READ_WRITE);
		SA.Fitness.Connection.Instance.AddScope (SA.Fitness.LoginScope.SCOPE_BODY_READ);
		SA.Fitness.Connection.Instance.AddScope (SA.Fitness.LoginScope.SCOPE_BODY_READ_WRITE);
		SA.Fitness.Connection.Instance.AddScope (SA.Fitness.LoginScope.SCOPE_NUTRITION_READ);
		SA.Fitness.Connection.Instance.AddScope (SA.Fitness.LoginScope.SCOPE_NUTRITION_READ_WRITE);
		SA.Fitness.Connection.Instance.OnConnectionFinished += SA_Fitness_Connection_Instance_OnConnectionFinished;
		SA.Fitness.Connection.Instance.Connect ();
	}

	private void requestSensors() {
		SA.Fitness.SensorRequest.Builder builder = new SA.Fitness.SensorRequest.Builder ()
				.AddDataType (SA.Fitness.DataType.TYPE_LOCATION_SAMPLE)
				.AddDataType (SA.Fitness.DataType.TYPE_LOCATION_TRACK)
				.AddDataType (SA.Fitness.DataType.TYPE_STEP_COUNT_DELTA)
				.AddDataType (SA.Fitness.DataType.TYPE_DISTANCE_DELTA)
				.AddDataSourceType (SA.Fitness.DataSourceType.RAW)
				.AddDataSourceType (SA.Fitness.DataSourceType.DERIVED);
		SA.Fitness.SensorRequest request = builder.Build ();
		request.OnRequestFinished += Request_OnRequestFinished;
		SA.Fitness.Sensors.Instance.RequestSensors (request);
	}

	private void registerSensorsListeners() {
		int index = 1;
		foreach (SA.Fitness.DataSource source in dataSources) {
			Debug.Log ("Data Source #" + index);
			Debug.Log (source.DataSourceType);
			Debug.Log (source.AppPackageName);
			Debug.Log (source.DataType);
			Debug.Log (source.Device);
			Debug.Log (source.Name);
			Debug.Log (source.StreamId);
			Debug.Log (source.StreamName);
			
			SA.Fitness.SensorListener.Builder builder = new SA.Fitness.SensorListener.Builder ()
				.SetDataType (source.DataType)
					.SetSamplingRate (5U, SA.Fitness.TimeUnit.Seconds);
			SA.Fitness.SensorListener listener = builder.Build ();
			listener.OnRegisterSuccess += Listener_OnRegisterSuccess;
			listener.OnRegisterFail += Listener_OnRegisterFail;
			listener.OnDataPointReceived += Listener_OnDataPointReceived;
			SA.Fitness.Sensors.Instance.RegisterSensorListener (listener);
			
			index++;
		}
	}

	private void listSubscriptions() {
		//List subscriptions
		SA.Fitness.SubscriptionsRequest.Builder builder = new SA.Fitness.SubscriptionsRequest.Builder();
		SA.Fitness.SubscriptionsRequest request = builder.Build ();
		request.OnRequestFinished += Request_OnRequestFinished1;
		SA.Fitness.Recording.Instance.ListSubscriptions (request);
	}

	private void subscribe() {
		//Subscribe
		SA.Fitness.SubscribeRequest.Builder builder = new SA.Fitness.SubscribeRequest.Builder();
		builder.SetDataType (SA.Fitness.DataType.TYPE_STEP_COUNT_DELTA);
		SA.Fitness.SubscribeRequest request = builder.Build ();
		request.OnSubscribeFinished += Request_OnSubscribeFinished;
		SA.Fitness.Recording.Instance.Subscribe(request);
	}

	private void unsubscribe() {
		//Unsubscribe
		SA.Fitness.UnsubscribeRequest.Builder builder = new SA.Fitness.UnsubscribeRequest.Builder();
		builder.SetDataType (SA.Fitness.DataType.TYPE_STEP_COUNT_DELTA);
		SA.Fitness.UnsubscribeRequest request = builder.Build ();
		request.OnUnsubscribeFinished += Request_OnUnsubscribeFinished;
		SA.Fitness.Recording.Instance.Unsubscribe (request);
	}

	public void readSession(){
		DateTime dateTime = DateTime.Now.ToUniversalTime ();
		dateTime = dateTime.AddDays (-5.0f);
		long startTime = (long)dateTime.Subtract (new DateTime (1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;

		long endTime = (long)DateTime.Now.ToUniversalTime ().Subtract (new DateTime (1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;

		SA.Fitness.ReadSessionRequest.Builder builder = new SA.Fitness.ReadSessionRequest.Builder ();
		builder.SetIdentifier (SESSION_ID);
		builder.SetDataTypeToRead (SA.Fitness.DataType.TYPE_STEP_COUNT_DELTA);
		builder.SetTimeinterval (startTime, endTime, SA.Fitness.TimeUnit.Milliseconds);
		SA.Fitness.ReadSessionRequest request = builder.Build ();
		request.OnSessionReadFinished += Request_OnSessionReadFinished;
		SA.Fitness.Sessions.Instance.ReadSession (request);
	}

	public void insertSession(){
		SA.Fitness.Sessions.Instance.InsertSession ();
	}
		

	public void startSession(){
		long milliseconds = (long)DateTime.Now.ToUniversalTime ().Subtract (new DateTime (1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;

		SA.Fitness.StartSessionRequest.Builder builder = new SA.Fitness.StartSessionRequest.Builder ();
		builder.SetName ("session#test");
		builder.SetIdentifier (SESSION_ID);
		builder.SetDescription ("Google Fit Session for Android Native");
		builder.SetStartTime (milliseconds, SA.Fitness.TimeUnit.Milliseconds);
		builder.SetActivity (SA.Fitness.Activity.WALKING);
		SA.Fitness.StartSessionRequest request = builder.Build ();
		request.OnSessionStarted += Request_OnSessionStarted;

		SA.Fitness.Sessions.Instance.StartSession (request);
	}

	public void stopSession(){
		SA.Fitness.StopSessionRequest.Builder builder = new SA.Fitness.StopSessionRequest.Builder ();
		builder.SetIdentifier (SESSION_ID);
		SA.Fitness.StopSessionRequest request = builder.Build ();
		request.OnSessionStopped += Request_OnSessionStopped;

		SA.Fitness.Sessions.Instance.StopSession (request);
	}

	public void readData(){
		/*DateTime dateTime = DateTime.Now.ToUniversalTime ();
		dateTime = dateTime.AddDays (-5.0f);
		long startTime = (long)dateTime.Subtract (new DateTime (1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;

		long endTime = (long)DateTime.Now.ToUniversalTime ().Subtract (new DateTime (1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;

		SA.Fitness.ReadHistoryRequest.Builder builder = new SA.Fitness.ReadHistoryRequest.Builder ();
		builder.SetTimeRange (startTime, endTime, SA.Fitness.TimeUnit.Milliseconds);
		builder.Read (SA.Fitness.DataType.AGGREGATE_STEP_COUNT_DELTA);
		SA.Fitness.ReadHistoryRequest request = builder.Build ();
		request.OnReadFinished += Request_OnReadHistoryFinished;

		SA.Fitness.History.Instance.ReadData (request);*/

		DateTime dateTime = DateTime.Now.ToUniversalTime ();
		dateTime = dateTime.AddDays (-7.0f);
		long startTime = (long)dateTime.Subtract (new DateTime (1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;

		long endTime = (long)DateTime.Now.ToUniversalTime ().Subtract (new DateTime (1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;

		SA.Fitness.ReadHistoryRequest.Builder builder = new SA.Fitness.ReadHistoryRequest.Builder ();
		builder.SetTimeRange (startTime, endTime, SA.Fitness.TimeUnit.Milliseconds);
		builder.Aggregate (SA.Fitness.DataType.TYPE_STEP_COUNT_DELTA, SA.Fitness.DataType.AGGREGATE_STEP_COUNT_DELTA);
		builder.BucketByTime (1, SA.Fitness.TimeUnit.Days);
		SA.Fitness.ReadHistoryRequest request = builder.Build ();
		request.OnReadFinished += Request_OnReadHistoryFinished;

		SA.Fitness.History.Instance.ReadData (request);
	}

	public void readDailyTotal(){
		SA.Fitness.ReadDailyTotalRequest.Builder builder = new SA.Fitness.ReadDailyTotalRequest.Builder ();
		builder.SetDataType (SA.Fitness.DataType.TYPE_STEP_COUNT_DELTA);
		SA.Fitness.ReadDailyTotalRequest request = builder.Build ();
		request.OnRequestFinished += Request_OnReadDailyTotalRequestFinished;
		SA.Fitness.History.Instance.ReadDailyTotal (request);
	}

	public void insertData(){
		SA.Fitness.History.Instance.InsertData ();
	}

	public void updateData(){
		SA.Fitness.History.Instance.UpdateData ();
	}

	public void deleteData(){
		SA.Fitness.History.Instance.DeleteData (null);
	}
	
	//--------------------------------------
	// EVENT HANDLERS
	//--------------------------------------

	void Request_OnReadHistoryFinished (SA.Fitness.ReadHistoryResult result)
	{
		Debug.Log ("[OnReadHistoryFinished] status: " + result.IsSucceeded);
		if (result.IsSucceeded) {
			if (result.IsAggregated) {
				int i = 1;
				foreach (SA.Fitness.Bucket bucket in result.Buckets) {
					Debug.Log ("Bucket #" + i.ToString() +
						"\n\t Type: " + bucket.Bucketing.ToString() +
						"\n\t Start Time: " + bucket.StartTime.ToString() +
						"\n\t End Time: " + bucket.EndTime.ToString()
					);

					int j = 1;
					foreach (SA.Fitness.DataSet dataSet in bucket.DataSets) {
						Debug.Log ("Data Set #" + j.ToString() +
							"\n\t\t Data Type: " + dataSet.DataType.Value
						);

						Debug.Log ("Data Points Count: " + dataSet.DataPoints.Count);
						int k = 1;
						foreach (SA.Fitness.DataPoint dp in dataSet.DataPoints) {
							Debug.Log ("Data Point #" + k.ToString() +
								"\n\t\t\t Data Type: " + dp.DataType.Value + 
								"\n\t\t\t Start Time: " + dp.StartTime.ToString() +
								"\n\t\t\t End Time: " + dp.EndTime.ToString()
							);

							Debug.Log ("Fields Count: " + dp.Fields.Count);
							foreach (KeyValuePair<string, object> pair in dp.Fields) {
								Debug.Log ("\t\t\t\t " + pair.Key + " : " + pair.Value.ToString());
							}

							k++;
						}

						j++;
					}
					i++;
				}
			} else {
				int j = 1;
				foreach (SA.Fitness.DataSet dataSet in result.DataSets) {
					Debug.Log ("Data Set #" + j.ToString() +
						"\n\t\t Data Type: " + dataSet.DataType.Value
					);

					Debug.Log ("Data Points Count: " + dataSet.DataPoints.Count);
					int k = 1;
					foreach (SA.Fitness.DataPoint dp in dataSet.DataPoints) {
						Debug.Log ("Data Point #" + k.ToString() +
							"\n\t\t\t Data Type: " + dp.DataType.Value + 
							"\n\t\t\t Start Time: " + dp.StartTime.ToString() +
							"\n\t\t\t End Time: " + dp.EndTime.ToString()
						);

						Debug.Log ("Fields Count: " + dp.Fields.Count);
						foreach (KeyValuePair<string, object> pair in dp.Fields) {
							Debug.Log ("\t\t\t\t " + pair.Key + " : " + pair.Value.ToString());
						}

						k++;
					}

					j++;
				}
			}
		}
	}

	void Request_OnReadDailyTotalRequestFinished (SA.Fitness.ReadDailyTotalResult result)
	{
		Debug.Log ("[OnReadDailyTotalRequestFinished] result status: " + result.IsSucceeded);
		if (result.IsSucceeded) {
			int k = 0;
			foreach (SA.Fitness.DataPoint dp in result.DataSet.DataPoints) {
				Debug.Log ("Data Point #" + k.ToString() +
					"\n\t Data Type: " + dp.DataType.Value + 
					"\n\t Start Time: " + dp.StartTime.ToString() +
					"\n\t End Time: " + dp.EndTime.ToString()
				);

				Debug.Log ("Fields Count: " + dp.Fields.Count);
				foreach (KeyValuePair<string, object> pair in dp.Fields) {
					Debug.Log ("\t\t\t\t " + pair.Key + " : " + pair.Value.ToString());
				}

				k++;
			}
		}
	}

	void Request_OnSessionReadFinished (SA.Fitness.ReadSessionResult result)
	{
		Debug.Log ("[Request_OnSessionReadFinished] result status: " + result.IsSucceeded);
		if (result.IsSucceeded) {
			Debug.Log ("Sessions Count: " + result.Sessions.Count.ToString());
			int i = 1;
			foreach (SA.Fitness.Session session in result.Sessions) {
				Debug.Log ("Session #" + i.ToString() + 
					"\n\t Id: " + session.Id + 
					"\n\t Name: " + session.Name +
					"\n\t Start Time: " + session.StartTime.ToString() +
					"\n\t End Time: " + session.EndTime.ToString() +
					"\n\t Activity: " + session.Activity.Value + 
					"\n\t App Package Name: " + session.AppPackageName
				);

				Debug.Log ("Data Sets Count: " + session.DataSets.Count.ToString());
				int j = 1;
				foreach (SA.Fitness.DataSet dataSet in session.DataSets) {
					Debug.Log ("Data Set #" + j.ToString() +
						"\n\t\t Data Type: " + dataSet.DataType.Value
					);

					Debug.Log ("Data Points Count: " + dataSet.DataPoints.Count);
					int k = 1;
					foreach (SA.Fitness.DataPoint dp in dataSet.DataPoints) {
						Debug.Log ("Data Point #" + k.ToString() +
							"\n\t\t\t Data Type: " + dp.DataType.Value + 
							"\n\t\t\t Start Time: " + dp.StartTime.ToString() +
							"\n\t\t\t End Time: " + dp.EndTime.ToString()
						);

						Debug.Log ("Fields Count: " + dp.Fields.Count);
						foreach (KeyValuePair<string, object> pair in dp.Fields) {
							Debug.Log ("\t\t\t\t " + pair.Key + " : " + pair.Value.ToString());
						}

						k++;
					}

					j++;
				}

				i++;
			}
		}
	}

	void SA_Fitness_Connection_Instance_OnConnectionFinished (SA.Fitness.ConnectionResult result)
	{
		Debug.Log ("Fitness connection result: " + result.IsSucceeded);
		if (!result.IsSucceeded) {			
			SA_StatusBar.text = "Player Disconnected " + result.Error.Code + " " + result.Error.Message;
			playerLabel.text = "Player Disconnected";
			
			Debug.Log ("connection result: " + result.Error.Code + " " + result.Error.Message);
		} else {
			SA_StatusBar.text = "Player Connected";
			playerLabel.text = "Player Connected";
		}
		UpdateButtons ();
	}
	void Request_OnRequestFinished (SA.Fitness.SensorRequestResult result)
	{
		Debug.Log ("Request_OnRequestFinished " + result.Id);
		if (result.IsSucceeded) {
			dataSources = result.DataSources;
		}
	}
	
	void Listener_OnRegisterSuccess (int id)
	{
		Debug.Log ("[Listener_OnRegisterSuccess] " + id.ToString());
	}
	
	void Listener_OnRegisterFail (int id)
	{
		Debug.Log ("[Listener_OnRegisterFail] " + id.ToString());
	}
	
	void Listener_OnDataPointReceived (int id, SA.Fitness.DataPoint dataPoint)
	{
		Debug.Log ("[Listener_OnDataPointReceived] id " + id.ToString() + " dataPoint type: " + dataPoint.DataType.Value);
		Debug.Log ("FIELDS: " + dataPoint.Fields.Count.ToString() + "#");
		foreach (KeyValuePair<string, object> pair in dataPoint.Fields) {
			Debug.Log ("key:" + pair.Key + " value:" + pair.Value.ToString());
		}
	}

	void Request_OnRequestFinished1 (SA.Fitness.SubscriptionsRequestResult result)
	{
		Debug.Log ("SubscriptionsRequest.ListSubscriptions " + result.IsSucceeded);
		if (result.IsSucceeded) {
			foreach (SA.Fitness.Subscription sub in result.Subscriptions) {
				Debug.Log ("Subscription: " + sub.DataType.Value);
			}
		}
	}

	void Request_OnSubscribeFinished (SA.Common.Models.Result result)
	{
		Debug.Log ("Request_OnSubscribeFinished " + result.IsSucceeded);
	}

	void Request_OnUnsubscribeFinished (SA.Common.Models.Result result)
	{
		Debug.Log ("[Request_OnUnsubscribeFinished] " + result.IsSucceeded);
	}

	void Request_OnSessionStarted (SA.Common.Models.Result result)
	{
		Debug.Log ("[Request_OnSessionStarted] " + result.IsSucceeded.ToString());
	}

	void Request_OnSessionStopped (SA.Fitness.StopSessionResult result)
	{
		Debug.Log ("[Request_OnSessionStopped] " + result.IsSucceeded.ToString());
		if (result.IsSucceeded) {
			Debug.Log ("Sessions Count " + result.Sessions.Count);
			foreach (SA.Fitness.Session session in result.Sessions) {
				Debug.Log ("[Session] Id: " + session.Id
					+ "\n\t Start Time: " + session.StartTime.ToString()
					+ "\n\t End Time: " + session.EndTime.ToString()
					+ "\n\t Name: " + session.Name
					+ "\n\t Description: " + session.Description
					+ "\n\t Activity: " + session.Activity.Value
					+ "\n\t App Package Name: " + session.AppPackageName
				);
			}
		}
	}
}
