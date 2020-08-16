namespace Mapbox.Examples
{
	using UnityEngine;
	using Mapbox.Unity.MeshGeneration.Factories;
	using Mapbox.Unity.Utilities;
	using Mapbox.Unity.MeshGeneration.Factories.TerrainStrategies;
	using Mapbox.Unity.Map;
	using ardor; 

	public class SpawnOnGlobeExample : MonoBehaviour
	{
		[SerializeField]
		AbstractMap _map;

		[SerializeField]
		[Geocode]
		string[] _locations;

		[SerializeField]
		float _spawnScale = 100f;

		[SerializeField]
		GameObject _markerPrefab;

		[SerializeField]
		float min;

		[SerializeField]
		float max;

		public createSimpleScriptable locations;

		void Start()
		{
			foreach (geoPeer loc in locations.peers)
			{
				var instance = Instantiate(_markerPrefab);
				var location = Conversions.StringToLatLon(loc.location);
				var earthRadius = ((IGlobeTerrainLayer)_map.Terrain).EarthRadius;
				instance.transform.position = Conversions.GeoToWorldGlobePosition(location, earthRadius+ (Random.Range(min, max)));
				instance.transform.localScale = Vector3.one * _spawnScale;
				instance.transform.SetParent(transform);
				print(earthRadius);
			}
		}
	}
}