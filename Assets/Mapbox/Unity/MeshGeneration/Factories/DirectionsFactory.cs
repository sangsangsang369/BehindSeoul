namespace Mapbox.Unity.MeshGeneration.Factories
{
	using UnityEngine;
	using Mapbox.Directions;
	using System.Collections.Generic;
	using System.Linq;
	using Mapbox.Unity.Map;
	using Data;
	using Modifiers;
	using Mapbox.Utils;
	using Mapbox.Unity.Utilities;
	using System.Collections;
	using Mapbox.Examples;
	using UnityEngine.UI;

	public class DirectionsFactory : MonoBehaviour
	{
		[SerializeField]
		AbstractMap _map;

		[SerializeField]
		MeshModifier[] MeshModifiers;

		[SerializeField]
		Material _material;

		[SerializeField]
		List<Transform> _waypoints;
		private List<Vector3> _cachedWaypoints;

		[SerializeField]
		[Range(1,10)]
		private float UpdateFrequency = 2;
		SpawnOnMap spawnOnMap;
		[SerializeField]
		Button btn;


		private Directions _directions;
		private int _counter;

		GameObject _directionsGO;
		private bool _recalculateNext;

		protected virtual void Awake()
		{
			if (_map == null)
			{
				_map = FindObjectOfType<AbstractMap>();
			}
		}

		public void Start()
		{


			spawnOnMap = FindObjectOfType<SpawnOnMap>();
			_waypoints.Add(spawnOnMap._spawnedObjects[0].transform);
			
			_cachedWaypoints = new List<Vector3>(_waypoints.Count);
			foreach (var item in _waypoints)
			{
				_cachedWaypoints.Add(item.position);
			}
			_recalculateNext = false;

			foreach (var modifier in MeshModifiers)
			{
				modifier.Initialize();
			}

			btn.onClick.AddListener(DrawRoute);  
		}

		public void DrawRoute()
		{	
			_directions = MapboxAccess.Instance.Directions;
			_map.OnInitialized += Query;
			_map.OnUpdated += Query;

			StartCoroutine(QueryTimer());
		}
		protected virtual void OnDestroy()
		{
			_map.OnInitialized -= Query;
			_map.OnUpdated -= Query;
		}

		void Query()
		{
			var count = _waypoints.Count;
			var wp = new Vector2d[count];
			for (int i = 0; i < count; i++)
			{
				wp[i] = _waypoints[i].GetGeoPosition(_map.CenterMercator, _map.WorldRelativeScale);
			}
			var _directionResource = new DirectionResource(wp, RoutingProfile.Walking);
			_directionResource.Steps = true;
			_directions.Query(_directionResource, HandleDirectionsResponse);
		}

		public IEnumerator QueryTimer()
		{
			while (true)
			{
				yield return new WaitForSeconds(UpdateFrequency);
				
				if (_waypoints[0].position != _cachedWaypoints[0])
				{
					_recalculateNext = true;
					_cachedWaypoints[0] = _waypoints[0].position;
				}
				

				if (_recalculateNext)
				{
					Query();
					_recalculateNext = false;
				}
			}
		}

		void HandleDirectionsResponse(DirectionsResponse response)
		{
			if (response == null || null == response.Routes || response.Routes.Count < 1)
			{
				return;
			}

			var meshData = new MeshData();
			var dat = new List<Vector3>();
			foreach (var point in response.Routes[0].Geometry)
			{
				dat.Add(Conversions.GeoToWorldPosition(point.x, point.y, _map.CenterMercator, _map.WorldRelativeScale).ToVector3xz());
			}

			var feat = new VectorFeatureUnity();
			feat.Points.Add(dat);

			foreach (MeshModifier mod in MeshModifiers.Where(x => x.Active))
			{
				mod.Run(feat, meshData, _map.WorldRelativeScale);
			}

			CreateGameObject(meshData);
		}

		GameObject CreateGameObject(MeshData data)
		{
			//경로 오브젝트가 존재하면
			if (_directionsGO != null)
			{
				//파괴
				_directionsGO.Destroy();
			}
			//겜 오브젝트 새로 만들기
			_directionsGO = new GameObject("direction waypoint " + " entity");
			//디렉션고에 메시필터 넣고 메시필터의 메시 가져오기
			var mesh = _directionsGO.AddComponent<MeshFilter>().mesh;
			//메쉬 안에 있는 서브 매쉬의 수를 
			mesh.subMeshCount = data.Triangles.Count;

			mesh.SetVertices(data.Vertices);
			_counter = data.Triangles.Count;
			for (int i = 0; i < _counter; i++)
			{
				var triangle = data.Triangles[i];
				mesh.SetTriangles(triangle, i);
			}

			_counter = data.UV.Count;
			for (int i = 0; i < _counter; i++)
			{
				var uv = data.UV[i];
				mesh.SetUVs(i, uv);
			}

			mesh.RecalculateNormals();
			_directionsGO.AddComponent<MeshRenderer>().material = _material;
			return _directionsGO;
		}
	}

}
